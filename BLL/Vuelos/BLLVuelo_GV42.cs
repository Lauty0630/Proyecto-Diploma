using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BLL
{
    // Gestión de vuelos (alta, modificación y baja/reactivación lógica). Cada cambio que llega a la
    // tabla Vuelo queda registrado en la bitácora Vuelo_C por el trigger de la base.
    // El código de vuelo se define en el alta y no se modifica.
    public class BLLVuelo_GV42
    {
        #region Constantes

        public const int MAX_HORAS_VUELO = 24;
        public const decimal MAX_COSTO_KILO = 99999999.99m;   // Vuelo.CostoKiloExceso decimal(10,2)
        public const decimal MAX_PRECIO_BASE = 9999999999.99m; // VueloClase.PrecioBase decimal(12,2)

        // Mapa de asientos de un vuelo nuevo (el mismo de los vuelos ya cargados): 6 butacas por fila;
        // filas 1-2 Primera, 3-6 Ejecutiva y 7-20 Económica.
        public const int ASIENTOS_POR_FILA = 6;
        public const int FILAS_PRIMERA = 2;
        public const int FILAS_EJECUTIVA = 4;
        public const int FILAS_ECONOMICA = 14;

        // Kilos de equipaje sin cargo de cada clase.
        public const decimal FRANQUICIA_ECONOMICA = 15m;
        public const decimal FRANQUICIA_EJECUTIVA = 23m;
        public const decimal FRANQUICIA_PRIMERA = 32m;

        #endregion

        #region Campos

        private readonly DALVuelo_GV42 _dalVuelo = new DALVuelo_GV42();
        private readonly DALAeropuerto_GV42 _dalAeropuerto = new DALAeropuerto_GV42();

        #endregion

        #region Consultas

        public List<Vuelo_GV42> Listar()
        {
            BLLNegocioUtil_GV42.ExigirPatente("Vuelos.Gestionar", IdiomaManager_GV42.T("neg.vuelo.sinPermiso"));
            return _dalVuelo.ListarTodos();
        }

        public List<Aerolinea_GV42> ListarAerolineas() { return _dalVuelo.ListarAerolineas(); }

        public List<Aeropuerto_GV42> ListarAeropuertos() { return _dalAeropuerto.ListarTodos(); }

        internal Vuelo_GV42 BuscarActual(int idVuelo) => _dalVuelo.ListarTodos().Find(x => x.Id == idVuelo);

        internal int ContarReservasVigentes(int idVuelo) => _dalVuelo.ContarReservasVigentes(idVuelo);

        #endregion

        #region Validaciones

        // Reglas comunes para modificar un vuelo o activar una versión anterior de sus datos.
        internal void ValidarCambio(Vuelo_GV42 actual, string codigo, string aerolinea, string ruta,
                                    DateTime salida, DateTime llegada, decimal costoKilo)
        {
            if (actual == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.noExiste"));
            if (actual.BorradoLogico)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.dadoDeBajaModificar", actual.CodigoVuelo));
            if (actual.FechaHoraSalida <= DateTime.Now)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.salioModificar", actual.CodigoVuelo));
            if (salida <= DateTime.Now)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.salidaFutura"));
            if (llegada <= salida)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.llegadaPosterior"));
            if ((llegada - salida).TotalHours > MAX_HORAS_VUELO)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.duracionMaxima", MAX_HORAS_VUELO));
            if (costoKilo < 0 || costoKilo > MAX_COSTO_KILO)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.costoKiloRango", MAX_COSTO_KILO.ToString("N2")));

            // Con reservas vigentes no se cambia la identidad del vuelo (código, aerolínea ni ruta):
            // los pasajeros compraron ese vuelo. Sí se puede reprogramar el horario, la puerta o el costo.
            if (_dalVuelo.ContarReservasVigentes(actual.Id) > 0)
            {
                string rutaActual = actual.Origen.CodigoIata + " -> " + actual.Destino.CodigoIata;
                if (!string.Equals(codigo, actual.CodigoVuelo, StringComparison.OrdinalIgnoreCase) ||
                    !string.Equals(aerolinea, actual.Aerolinea.Nombre, StringComparison.OrdinalIgnoreCase) ||
                    (ruta != null && !string.Equals(ruta.Replace(" ", ""), rutaActual.Replace(" ", ""), StringComparison.OrdinalIgnoreCase)))
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.reservasVigentesIdentidad", actual.CodigoVuelo));
            }
        }

        #endregion

        #region Alta

        // Crea el vuelo con sus tres clases (precio base de cada una) y su mapa de asientos estándar.
        // Devuelve el Id del vuelo creado. El primer registro de su historial lo genera el trigger.
        public int Crear(Vuelo_GV42 v, decimal precioEconomica, decimal precioEjecutiva, decimal precioPrimera)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Vuelos.Gestionar", IdiomaManager_GV42.T("neg.vuelo.sinPermiso"));
            if (v == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.faltanDatos"));

            v.CodigoVuelo = (v.CodigoVuelo ?? string.Empty).Trim().ToUpperInvariant();
            if (!Regex.IsMatch(v.CodigoVuelo, @"^[A-Z0-9]{3,10}$"))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.codigoInvalido"));
            ValidarDatos(v);

            if (v.FechaHoraSalida <= DateTime.Now)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.salidaFutura"));
            if ((v.FechaHoraLlegada - v.FechaHoraSalida).TotalHours > MAX_HORAS_VUELO)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.duracionMaxima", MAX_HORAS_VUELO));
            if (v.CostoKiloExceso > MAX_COSTO_KILO)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.costoKiloRango", MAX_COSTO_KILO.ToString("N2")));

            foreach (decimal precio in new[] { precioEconomica, precioEjecutiva, precioPrimera })
                if (precio <= 0 || precio > MAX_PRECIO_BASE)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.precioInvalido"));
            // Una clase superior no puede costar menos que la inferior.
            if (precioEjecutiva < precioEconomica || precioPrimera < precioEjecutiva)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.preciosOrden"));

            if (_dalVuelo.ExisteCodigo(v.CodigoVuelo, 0))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.codigoDuplicado", v.CodigoVuelo));

            var clases = new List<VueloClase_GV42>
            {
                new VueloClase_GV42 { Clase = ClaseVuelo_GV42.Economica, PrecioBase = precioEconomica,
                                      CapacidadAsientos = FILAS_ECONOMICA * ASIENTOS_POR_FILA, FranquiciaEquipajeKg = FRANQUICIA_ECONOMICA },
                new VueloClase_GV42 { Clase = ClaseVuelo_GV42.Ejecutiva, PrecioBase = precioEjecutiva,
                                      CapacidadAsientos = FILAS_EJECUTIVA * ASIENTOS_POR_FILA, FranquiciaEquipajeKg = FRANQUICIA_EJECUTIVA },
                new VueloClase_GV42 { Clase = ClaseVuelo_GV42.Primera, PrecioBase = precioPrimera,
                                      CapacidadAsientos = FILAS_PRIMERA * ASIENTOS_POR_FILA, FranquiciaEquipajeKg = FRANQUICIA_PRIMERA }
            };

            v.Id = _dalVuelo.Crear(v, clases, FILAS_PRIMERA, FILAS_EJECUTIVA, FILAS_PRIMERA + FILAS_EJECUTIVA + FILAS_ECONOMICA);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_VUELOS, "Vuelo creado", v.CodigoVuelo, "Media");
            return v.Id;
        }

        // Datos que se cargan a mano, comunes al alta y a la modificación.
        private void ValidarDatos(Vuelo_GV42 v)
        {
            v.PuertaEmbarque = (v.PuertaEmbarque ?? string.Empty).Trim().ToUpperInvariant();

            if (!Regex.IsMatch(v.PuertaEmbarque, @"^[A-Z0-9]{1,10}$"))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.puertaInvalida"));
            if (v.Aerolinea == null || v.Origen == null || v.Destino == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.faltanDatos"));
            if (v.Origen.Id == v.Destino.Id)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.mismoAeropuerto"));
            if (v.FechaHoraLlegada <= v.FechaHoraSalida)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.llegadaPosterior"));
            if (v.CostoKiloExceso < 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.costoNegativo"));
        }

        #endregion

        #region Modificación

        public void Modificar(Vuelo_GV42 v)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Vuelos.Gestionar", IdiomaManager_GV42.T("neg.vuelo.sinPermiso"));
            if (v == null || v.Id <= 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.seleccioneModificar"));

            Vuelo_GV42 actual = BuscarActual(v.Id);
            if (actual == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.noExiste"));

            // El código identifica al vuelo y a su historial: se define en el alta y no se cambia.
            string codigo = (v.CodigoVuelo ?? string.Empty).Trim().ToUpperInvariant();
            if (codigo.Length > 0 && !string.Equals(codigo, actual.CodigoVuelo, StringComparison.OrdinalIgnoreCase))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.codigoFijo", actual.CodigoVuelo));
            v.CodigoVuelo = actual.CodigoVuelo;

            ValidarDatos(v);

            ValidarCambio(actual, v.CodigoVuelo, v.Aerolinea.Nombre,
                          v.Origen.CodigoIata + " -> " + v.Destino.CodigoIata,
                          v.FechaHoraSalida, v.FechaHoraLlegada, v.CostoKiloExceso);

            _dalVuelo.Modificar(v);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_VUELOS, "Vuelo modificado", v.CodigoVuelo, "Media");
        }

        #endregion

        #region Baja y reactivación

        // Borrado lógico: el vuelo deja de ofrecerse, pero la fila y su historial se conservan.
        public void DarDeBaja(Vuelo_GV42 v)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Vuelos.Gestionar", IdiomaManager_GV42.T("neg.vuelo.sinPermiso"));
            if (v == null || v.Id <= 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.seleccioneBaja"));
            if (v.BorradoLogico)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.yaDeBaja", v.CodigoVuelo));
            if (_dalVuelo.ContarReservasVigentes(v.Id) > 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.reservasVigentesBaja", v.CodigoVuelo));

            _dalVuelo.CambiarBorradoLogico(v.Id, true);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_VUELOS, "Vuelo dado de baja", v.CodigoVuelo, "Media");
        }

        public void Reactivar(Vuelo_GV42 v)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Vuelos.Gestionar", IdiomaManager_GV42.T("neg.vuelo.sinPermiso"));
            if (v == null || v.Id <= 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.seleccioneReactivar"));
            if (!v.BorradoLogico)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.yaActivo", v.CodigoVuelo));
            if (v.FechaHoraSalida <= DateTime.Now)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.salioReactivar", v.CodigoVuelo));

            _dalVuelo.CambiarBorradoLogico(v.Id, false);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_VUELOS, "Vuelo reactivado", v.CodigoVuelo, "Media");
        }

        #endregion
    }
}
