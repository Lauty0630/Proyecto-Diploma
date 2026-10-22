using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    // Ofertas para el cliente (reporte inteligente, 3ra entrega). Es el mismo motor del reporte de
    // millas visto del lado del cliente: el reporte le dice al vendedor qué ofrecerle y esta clase
    // se lo muestra al cliente en su pantalla de reserva, mientras elige sus servicios adicionales.
    //
    // Qué se le ofrece (cada oferta lleva su motivo, ver MotivoOferta_GV42):
    //  1) Millas:          lo que le suma este viaje y cuánto le falta para la categoría siguiente. Sale de
    //                      las reglas de acumulación del reporte de millas (BLLReporteMillas_GV42).
    //  2) Compra asociada: "quienes contratan A también contratan B". Es un análisis de canasta: cada
    //                      reserva confirmada es una canasta y cada servicio adicional, un producto. Una
    //                      regla A -> B se acepta si aparece en al menos MIN_RESERVAS_REGLA reservas, con
    //                      confianza >= MIN_CONFIANZA y lift >= MIN_LIFT.
    //  3) Habitual:        el servicio que el propio cliente contrata en casi todas sus reservas.
    //  4) Categoría:       lo que más contratan los pasajeros de su misma categoría de millas.
    //
    // Es solo una sugerencia: no cambia precios ni agrega nada solo. El cliente decide si lo agrega.
    // Un cliente solo puede ver sus propias ofertas; el vendedor puede ver las del cliente que atiende.
    public class BLLOfertasCliente_GV42
    {
        #region Constantes

        // Mínimo de reservas en las que tienen que aparecer A y B juntos para considerar la regla.
        public const int MIN_RESERVAS_REGLA = 2;
        public const decimal MIN_CONFIANZA = 0.30m;
        public const decimal MIN_LIFT = 1m;

        // Participación mínima de un servicio entre las reservas con adicionales de una categoría.
        public const decimal MIN_PARTICIPACION_CATEGORIA = 0.25m;

        // Veces que el cliente tiene que haber contratado un servicio para considerarlo "habitual".
        public const int MIN_RESERVAS_HABITUAL = 2;

        public const int MAX_OFERTAS_ASOCIADAS = 2;
        public const int MAX_OFERTAS_CATEGORIA = 2;

        #endregion

        #region Campos

        private readonly DALReporteMillas_GV42 _dal = new DALReporteMillas_GV42();
        private readonly DALTipoAdicional_GV42 _dalTipo = new DALTipoAdicional_GV42();
        private readonly BLLReserva_GV42 _reservas = new BLLReserva_GV42();

        // Datos leídos de la base. Se guardan mientras el cliente tilda servicios en la pantalla para no
        // volver a consultar con cada clic; se vuelven a leer al entrar al paso (recargar = true).
        private List<ReporteMillas_GV42> _perfiles;
        private List<ContratacionAdicional_GV42> _contrataciones;
        private List<TipoAdicional_GV42> _catalogo;

        #endregion

        #region Consulta (pantalla de reserva)

        // dni: cliente al que se le arman las ofertas (vacío si todavía no se sabe).
        // vuelo: vuelo de ida elegido (puede ser null).
        // idsElegidos: servicios adicionales que el cliente ya tildó en este momento.
        // recargar: vuelve a leer la base (al entrar al paso); false reutiliza lo leído en la consulta anterior.
        public OfertasCliente_GV42 ObtenerOfertas(string dni, VueloClase_GV42 vuelo, IEnumerable<int> idsElegidos, bool recargar)
        {
            dni = (dni ?? string.Empty).Trim();
            ValidarAcceso(dni);

            if (recargar || _perfiles == null)
            {
                _perfiles = BLLReporteMillas_GV42.Calcular(_dal.ListarVuelosVolados(), null, DateTime.Now);
                _contrataciones = _dal.ListarContrataciones();
                _catalogo = Catalogo();
            }
            return Armar(dni, vuelo, idsElegidos, _catalogo, _perfiles, _contrataciones);
        }

        #endregion

        #region Reporte de millas (lado del vendedor)

        // Agrega a la recomendación de cada fila del reporte los servicios que conviene ofrecerle a ese
        // pasajero. Usa el mismo cálculo que ve el cliente en su pantalla.
        public void AgregarServiciosSugeridos(List<ReporteMillas_GV42> filas, List<VueloVolado_GV42> volados, DateTime ahora)
        {
            if (filas == null || filas.Count == 0) return;

            List<ReporteMillas_GV42> perfiles = BLLReporteMillas_GV42.Calcular(volados, null, ahora);
            List<ContratacionAdicional_GV42> contrataciones = _dal.ListarContrataciones();
            List<TipoAdicional_GV42> catalogo = Catalogo();

            foreach (ReporteMillas_GV42 fila in filas)
            {
                OfertasCliente_GV42 ofertas = Armar(fila.Dni, null, null, catalogo, perfiles, contrataciones);
                List<string> nombres = ofertas.Ofertas.Where(o => o.Tipo != null).Select(o => o.Titulo).ToList();
                if (nombres.Count > 0)
                    fila.Recomendacion += " " + IdiomaManager_GV42.T("millas.recServicios", string.Join(", ", nombres));
            }
        }

        #endregion

        #region Cálculo (sin acceso a datos)

        // Arma las ofertas con los datos ya leídos. Es pública y estática para poder probarla sin base.
        public static OfertasCliente_GV42 Armar(string dni, VueloClase_GV42 vuelo, IEnumerable<int> idsElegidos,
            List<TipoAdicional_GV42> catalogo, List<ReporteMillas_GV42> perfiles, List<ContratacionAdicional_GV42> contrataciones)
        {
            dni = (dni ?? string.Empty).Trim();
            catalogo = catalogo ?? new List<TipoAdicional_GV42>();
            perfiles = perfiles ?? new List<ReporteMillas_GV42>();
            contrataciones = contrataciones ?? new List<ContratacionAdicional_GV42>();
            var elegidos = new HashSet<int>(idsElegidos ?? Enumerable.Empty<int>());

            ReporteMillas_GV42 perfil = perfiles.FirstOrDefault(p => p.Dni == dni);
            int millas12 = perfil != null ? perfil.MillasUltimos12Meses : 0;
            CategoriaMillas_GV42 categoria = BLLReporteMillas_GV42.CategoriaPara(millas12);

            var resultado = new OfertasCliente_GV42
            {
                Dni = dni,
                Categoria = categoria,
                MillasUltimos12Meses = millas12,
                Resumen = IdiomaManager_GV42.T("ofertas.resumen", BLLReporteMillas_GV42.TextoCategoria(categoria),
                                               BLLReporteMillas_GV42.Numero(millas12))
            };

            // 1) Millas del viaje.
            OfertaCliente_GV42 oferta = OfertaMillas(vuelo, categoria, millas12);
            if (oferta != null) resultado.Ofertas.Add(oferta);

            // Lo que ya se ofreció o ya está elegido no se repite en las ofertas siguientes.
            var yaOfrecidos = new HashSet<int>(elegidos);
            Dictionary<int, TipoAdicional_GV42> porId = catalogo.ToDictionary(t => t.Id);

            // 2) Compra asociada: por cada servicio elegido, los que suelen ir con él.
            var reglas = CalcularReglas(contrataciones);
            foreach (ReglaAsociacion_GV42 regla in reglas
                .Where(r => elegidos.Contains(r.IdAntecedente) && porId.ContainsKey(r.IdConsecuente) && porId.ContainsKey(r.IdAntecedente))
                .OrderByDescending(r => r.Confianza).ThenByDescending(r => r.Lift))
            {
                if (yaOfrecidos.Contains(regla.IdConsecuente)) continue;
                if (resultado.Ofertas.Count(o => o.Motivo == MotivoOferta_GV42.CompraAsociada) >= MAX_OFERTAS_ASOCIADAS) break;

                TipoAdicional_GV42 ofrecido = porId[regla.IdConsecuente];
                resultado.Ofertas.Add(new OfertaCliente_GV42
                {
                    Motivo = MotivoOferta_GV42.CompraAsociada,
                    Tipo = ofrecido,
                    Titulo = BLLNegocioUtil_GV42.NombreAdicional(ofrecido.Nombre),
                    Detalle = IdiomaManager_GV42.T("ofertas.asociada", BLLNegocioUtil_GV42.NombreAdicional(porId[regla.IdAntecedente].Nombre),
                                                   Porcentaje(regla.Confianza))
                });
                yaOfrecidos.Add(regla.IdConsecuente);
            }

            // 3) Habitual: lo que este cliente contrata casi siempre.
            if (dni.Length > 0)
            {
                var suyas = contrataciones.Where(c => c.DniCliente == dni).ToList();
                int misReservas = suyas.Select(c => c.IdReserva).Distinct().Count();
                var habitual = suyas.GroupBy(c => c.IdTipoAdicional)
                    .Select(g => new { Id = g.Key, Veces = g.Select(c => c.IdReserva).Distinct().Count() })
                    .Where(x => x.Veces >= MIN_RESERVAS_HABITUAL && porId.ContainsKey(x.Id) && !yaOfrecidos.Contains(x.Id))
                    .OrderByDescending(x => x.Veces).ThenBy(x => x.Id)
                    .FirstOrDefault();
                if (habitual != null)
                {
                    TipoAdicional_GV42 tipo = porId[habitual.Id];
                    resultado.Ofertas.Add(new OfertaCliente_GV42
                    {
                        Motivo = MotivoOferta_GV42.Habitual,
                        Tipo = tipo,
                        Titulo = BLLNegocioUtil_GV42.NombreAdicional(tipo.Nombre),
                        Detalle = IdiomaManager_GV42.T("ofertas.habitual", BLLNegocioUtil_GV42.NombreAdicional(tipo.Nombre),
                                                       habitual.Veces, misReservas)
                    });
                    yaOfrecidos.Add(habitual.Id);
                }
            }

            // 4) Categoría: lo que más contratan los pasajeros de su misma categoría de millas.
            Dictionary<string, CategoriaMillas_GV42> categoriaPorDni = perfiles
                .Where(p => !string.IsNullOrEmpty(p.Dni)).GroupBy(p => p.Dni).ToDictionary(g => g.Key, g => g.First().Categoria);
            // Un cliente que nunca voló no figura en el reporte: es Bronce (0 millas).
            Func<string, CategoriaMillas_GV42> categoriaDe = d =>
            {
                CategoriaMillas_GV42 c;
                return d != null && categoriaPorDni.TryGetValue(d, out c) ? c : CategoriaMillas_GV42.Bronce;
            };

            var delaCategoria = contrataciones.Where(c => categoriaDe(c.DniCliente) == categoria).ToList();
            int reservasCategoria = delaCategoria.Select(c => c.IdReserva).Distinct().Count();
            if (reservasCategoria >= MIN_RESERVAS_REGLA)
            {
                var populares = delaCategoria.GroupBy(c => c.IdTipoAdicional)
                    .Select(g => new { Id = g.Key, Reservas = g.Select(c => c.IdReserva).Distinct().Count() })
                    .Where(x => x.Reservas >= MIN_RESERVAS_REGLA && porId.ContainsKey(x.Id) && !yaOfrecidos.Contains(x.Id)
                                && x.Reservas / (decimal)reservasCategoria >= MIN_PARTICIPACION_CATEGORIA)
                    .OrderByDescending(x => x.Reservas).ThenBy(x => x.Id)
                    .Take(MAX_OFERTAS_CATEGORIA);
                foreach (var x in populares)
                {
                    TipoAdicional_GV42 tipo = porId[x.Id];
                    resultado.Ofertas.Add(new OfertaCliente_GV42
                    {
                        Motivo = MotivoOferta_GV42.Categoria,
                        Tipo = tipo,
                        Titulo = BLLNegocioUtil_GV42.NombreAdicional(tipo.Nombre),
                        Detalle = IdiomaManager_GV42.T("ofertas.categoria", BLLReporteMillas_GV42.TextoCategoria(categoria),
                                                       Porcentaje(x.Reservas / (decimal)reservasCategoria))
                    });
                }
            }

            return resultado;
        }

        // Reglas "si contrata A, también contrata B" a partir de las reservas confirmadas.
        public static List<ReglaAsociacion_GV42> CalcularReglas(List<ContratacionAdicional_GV42> contrataciones)
        {
            var reglas = new List<ReglaAsociacion_GV42>();
            if (contrataciones == null || contrataciones.Count == 0) return reglas;

            // Una canasta por reserva, con los servicios distintos que tiene.
            List<HashSet<int>> canastas = contrataciones.GroupBy(c => c.IdReserva)
                .Select(g => new HashSet<int>(g.Select(c => c.IdTipoAdicional))).ToList();
            int total = canastas.Count;

            var frecuencia = new Dictionary<int, int>();
            var juntos = new Dictionary<Tuple<int, int>, int>();
            foreach (HashSet<int> canasta in canastas)
            {
                foreach (int a in canasta)
                {
                    frecuencia[a] = (frecuencia.ContainsKey(a) ? frecuencia[a] : 0) + 1;
                    foreach (int b in canasta)
                    {
                        if (a == b) continue;
                        var par = Tuple.Create(a, b);
                        juntos[par] = (juntos.ContainsKey(par) ? juntos[par] : 0) + 1;
                    }
                }
            }

            foreach (KeyValuePair<Tuple<int, int>, int> par in juntos)
            {
                int a = par.Key.Item1, b = par.Key.Item2, conjunto = par.Value;
                if (conjunto < MIN_RESERVAS_REGLA) continue;

                decimal confianza = conjunto / (decimal)frecuencia[a];
                decimal lift = confianza / (frecuencia[b] / (decimal)total);
                if (confianza < MIN_CONFIANZA || lift < MIN_LIFT) continue;

                reglas.Add(new ReglaAsociacion_GV42
                {
                    IdAntecedente = a,
                    IdConsecuente = b,
                    ReservasJuntos = conjunto,
                    Soporte = conjunto / (decimal)total,
                    Confianza = confianza,
                    Lift = lift
                });
            }

            return reglas.OrderByDescending(r => r.Confianza).ThenByDescending(r => r.Lift)
                         .ThenByDescending(r => r.ReservasJuntos).ToList();
        }

        // Millas que suma un vuelo en una clase, con la misma regla de acumulación del reporte.
        public static int MillasDelViaje(Vuelo_GV42 vuelo, ClaseVuelo_GV42 clase)
        {
            if (vuelo == null || vuelo.Origen == null || vuelo.Destino == null) return 0;
            return BLLReporteMillas_GV42.MillasDelVuelo(new VueloVolado_GV42
            {
                OrigenPais = vuelo.Origen.Pais,
                DestinoPais = vuelo.Destino.Pais,
                Clase = clase
            });
        }

        #endregion

        #region Métodos privados

        // Oferta informativa: millas que suma este viaje y qué falta para la categoría siguiente.
        private static OfertaCliente_GV42 OfertaMillas(VueloClase_GV42 vuelo, CategoriaMillas_GV42 categoria, int millas12)
        {
            if (vuelo == null || vuelo.Vuelo == null) return null;

            int millasViaje = MillasDelViaje(vuelo.Vuelo, vuelo.Clase);
            string detalle;

            if ((int)categoria >= (int)CategoriaMillas_GV42.Diamante)
            {
                detalle = IdiomaManager_GV42.T("ofertas.millasMaxima", BLLReporteMillas_GV42.Numero(millasViaje),
                                               BLLReporteMillas_GV42.TextoCategoria(categoria));
            }
            else
            {
                CategoriaMillas_GV42 siguiente = (CategoriaMillas_GV42)((int)categoria + 1);
                int faltan = BLLReporteMillas_GV42.UmbralDe(siguiente) - millas12;
                string nombreSiguiente = BLLReporteMillas_GV42.TextoCategoria(siguiente);

                if (millasViaje >= faltan)
                {
                    detalle = IdiomaManager_GV42.T("ofertas.millasAlcanza", BLLReporteMillas_GV42.Numero(millasViaje), nombreSiguiente);
                }
                else
                {
                    detalle = IdiomaManager_GV42.T("ofertas.millasViaje", BLLReporteMillas_GV42.Numero(millasViaje),
                                                   BLLReporteMillas_GV42.Numero(faltan), nombreSiguiente);

                    // ¿Con una clase más alta en este mismo vuelo llegaría a la categoría siguiente?
                    for (int c = (int)vuelo.Clase + 1; c <= (int)ClaseVuelo_GV42.Primera; c++)
                    {
                        var mejor = (ClaseVuelo_GV42)c;
                        int millasMejor = MillasDelViaje(vuelo.Vuelo, mejor);
                        if (millasMejor >= faltan)
                        {
                            detalle += " " + IdiomaManager_GV42.T("ofertas.millasClase", mejor.Texto().ToLower(),
                                                                  BLLReporteMillas_GV42.Numero(millasMejor), nombreSiguiente);
                            break;
                        }
                    }
                }
            }

            return new OfertaCliente_GV42
            {
                Motivo = MotivoOferta_GV42.Millas,
                Titulo = IdiomaManager_GV42.T("ofertas.tituloMillas"),
                Detalle = detalle
            };
        }

        // Servicios que se pueden ofrecer: los activos, menos el cargo por elegir asiento (lo agrega el
        // sistema según la tarifa y no es algo que el cliente decida contratar).
        private List<TipoAdicional_GV42> Catalogo()
        {
            return _dalTipo.ListarActivos().Where(t => !t.EsSeleccionAsiento).ToList();
        }

        // Un cliente solo ve sus propias ofertas; el vendedor las del cliente que está atendiendo.
        private void ValidarAcceso(string dni)
        {
            if (_reservas.PuedeGenerarParaTerceros()) return;

            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            string propio = actual != null ? (actual.DNI ?? string.Empty).Trim() : string.Empty;
            if (propio.Length == 0 || !string.Equals(propio, dni, StringComparison.OrdinalIgnoreCase))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.ofertas.sinPermiso"));
        }

        private static string Porcentaje(decimal proporcion)
        {
            return Math.Round(proporcion * 100m, 0).ToString("0");
        }

        #endregion
    }
}
