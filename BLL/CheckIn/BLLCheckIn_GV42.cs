using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BLL
{
    // RFN 2 - Check-in.
    //  - Presencial: el Encargado de Check-in (patente CheckIn.Realizar) atiende en el mostrador:
    //    verifica la reserva, despacha el equipaje (con cobro del exceso), valida o cambia el asiento,
    //    emite la tarjeta de embarque y cierra el check-in.
    //  - Online: el cliente autogestionado (patente CheckIn.RealizarPropio) hace el check-in de los
    //    pasajeros de SUS reservas: valida o cambia el asiento y obtiene la tarjeta. No despacha
    //    valijas; si tiene equipaje lo entrega después en el mostrador (despacho posterior).
    //  - El asiento ya se eligió al reservar (CUN10 incluido en Reservar vuelo): acá se valida y,
    //    si el pasajero lo pide, se cambia (CUN10 como extensión del check-in).
    public class BLLCheckIn_GV42
    {
        #region Constantes

        // El check-in se habilita desde 48 hs antes de la salida y se cierra 60 minutos antes.
        public const int HORAS_APERTURA_CHECKIN = 48;
        public const int MINUTOS_CIERRE_CHECKIN = 60;

        // Hora límite de embarque impresa en la tarjeta: 30 minutos antes de la salida.
        public const int MINUTOS_LIMITE_EMBARQUE = 30;

        // Equipaje por valija (como en las aerolíneas):
        //  - cada clase incluye valijas de hasta VueloClase.FranquiciaEquipajeKg cada una:
        //    Económica 1, Ejecutiva 2 y Primera 2;
        //  - cada "Equipaje extra" comprado en la reserva agrega una valija de hasta 23 kg;
        //  - ninguna valija se acepta por encima de 32 kg (hay que redistribuir el contenido);
        //  - entre la franquicia de la valija y los 32 kg se cobra el exceso por kilo.
        public const decimal KG_POR_EQUIPAJE_EXTRA = 23m;
        public const decimal MAX_PESO_POR_BULTO = 32m;

        public static int BultosIncluidos(ClaseVuelo_GV42 clase)
        {
            return clase == ClaseVuelo_GV42.Economica ? 1 : 2;
        }

        public const string PATENTE_MOSTRADOR = "CheckIn.Realizar";
        public const string PATENTE_ONLINE = "CheckIn.RealizarPropio";

        #endregion

        #region Campos

        private readonly DALCheckIn_GV42 _dalCheckIn;
        private readonly DALAsiento_GV42 _dalAsiento;
        private readonly DALEquipaje_GV42 _dalEquipaje;
        private readonly DALTarjetaEmbarque_GV42 _dalTarjeta;

        #endregion

        #region Constructor

        public BLLCheckIn_GV42()
        {
            _dalCheckIn = new DALCheckIn_GV42();
            _dalAsiento = new DALAsiento_GV42();
            _dalEquipaje = new DALEquipaje_GV42();
            _dalTarjeta = new DALTarjetaEmbarque_GV42();
        }

        #endregion

        #region Permisos

        // Encargado de Check-in en el mostrador.
        public bool PuedeAtenderMostrador() { return BLLNegocioUtil_GV42.TienePatente(PATENTE_MOSTRADOR); }

        // Cliente autogestionado: check-in online de sus propias reservas.
        public bool PuedeHacerCheckInOnline() { return BLLNegocioUtil_GV42.TienePatente(PATENTE_ONLINE); }

        private void ExigirAcceso(CheckIn_GV42 ci)
        {
            BLLNegocioUtil_GV42.LoginActual();
            if (PuedeAtenderMostrador()) return;
            if (!PuedeHacerCheckInOnline())
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.sinPermiso"));

            // El cliente solo opera sobre reservas propias (es el titular o es uno de los pasajeros).
            string dni = DniSesion();
            bool propia = string.Equals(ci.DniTitular ?? "", dni, StringComparison.OrdinalIgnoreCase)
                       || (ci.Pasajero != null && string.Equals(ci.Pasajero.DNI ?? "", dni, StringComparison.OrdinalIgnoreCase));
            if (!propia)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.reservaAjena"));
        }

        private CanalVenta_GV42 CanalActual()
        {
            return PuedeAtenderMostrador() ? CanalVenta_GV42.Presencial : CanalVenta_GV42.Autogestion;
        }

        private static string DniSesion()
        {
            Usuario_GV42 u = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            return u != null ? (u.DNI ?? string.Empty).Trim() : string.Empty;
        }

        #endregion

        #region Buscar y verificar (pasos 1 a 4)

        // Todos los pasajeros de la reserva con el estado de su check-in (para elegir a quién atender).
        // En ida y vuelta devuelve un check-in por pasajero y por tramo (primero la ida, después la
        // vuelta): cada vuelo tiene su propia ventana de check-in y su tarjeta de embarque.
        // No valida la ventana ni el estado: la pantalla los muestra y decide qué se puede hacer.
        public List<CheckIn_GV42> ListarPasajeros(string numeroReserva)
        {
            numeroReserva = (numeroReserva ?? string.Empty).Trim().ToUpper();
            if (numeroReserva.Length == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.indiqueNumero"));

            var lista = new List<CheckIn_GV42>();
            foreach (string dni in _dalCheckIn.ListarDnisPasajeros(numeroReserva))
            {
                CheckIn_GV42 ci = _dalCheckIn.BuscarPorReservaYDni(numeroReserva, dni, Reserva_GV42.TRAMO_IDA);
                if (ci == null) continue;
                lista.Add(ci);
                if (!ci.ReservaConVuelta) continue;
                CheckIn_GV42 vuelta = _dalCheckIn.BuscarPorReservaYDni(numeroReserva, dni, Reserva_GV42.TRAMO_VUELTA);
                if (vuelta != null) lista.Add(vuelta);
            }
            lista = lista.OrderBy(c => c.Tramo).ToList();
            if (lista.Count == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.reservaNoExiste"));

            if (PuedeAtenderMostrador()) return lista;

            // Cliente online: el titular de la reserva hace el check-in de todos sus pasajeros; un
            // acompañante solo ve y hace el suyo.
            BLLNegocioUtil_GV42.LoginActual();
            string dniSesion = DniSesion();
            if (!string.Equals(lista[0].DniTitular ?? "", dniSesion, StringComparison.OrdinalIgnoreCase))
                lista = lista.Where(c => c.Pasajero != null && string.Equals(c.Pasajero.DNI ?? "", dniSesion, StringComparison.OrdinalIgnoreCase)).ToList();
            if (lista.Count == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.reservaAjena"));
            ExigirAcceso(lista[0]);
            return lista;
        }

        // Devuelve el check-in con los datos que hay que verificar (pasajero, vuelo, servicios adicionales,
        // estado de la reserva y del check-in). Lanza NegocioException_GV42 si la reserva no está confirmada,
        // si el check-in ya se hizo o si está fuera de la ventana de check-in.
        public CheckIn_GV42 IniciarCheckIn(string numeroReserva, string dniPasajero)
        {
            return IniciarCheckIn(numeroReserva, dniPasajero, Reserva_GV42.TRAMO_IDA);
        }

        // tramo: 1 = ida, 2 = vuelta.
        public CheckIn_GV42 IniciarCheckIn(string numeroReserva, string dniPasajero, int tramo)
        {
            CheckIn_GV42 ci = Buscar(numeroReserva, dniPasajero, tramo);
            ExigirAcceso(ci);
            ValidarPuedeHacerCheckIn(ci);
            return ci;
        }

        // Check-in ya realizado (para volver a ver o imprimir la tarjeta de embarque y las etiquetas).
        public CheckIn_GV42 BuscarRealizado(string numeroReserva, string dniPasajero)
        {
            return BuscarRealizado(numeroReserva, dniPasajero, Reserva_GV42.TRAMO_IDA);
        }

        public CheckIn_GV42 BuscarRealizado(string numeroReserva, string dniPasajero, int tramo)
        {
            CheckIn_GV42 ci = Buscar(numeroReserva, dniPasajero, tramo);
            ExigirAcceso(ci);
            if (ci.Estado != EstadoCheckIn_GV42.Realizado || ci.TarjetaEmbarque == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.noRealizado"));
            return ci;
        }

        // Descripción del motivo por el que no se puede hacer el check-in (null si se puede).
        // La pantalla lo usa para mostrar el estado de cada pasajero sin lanzar excepciones.
        public string MotivoNoDisponible(CheckIn_GV42 ci)
        {
            try { ValidarPuedeHacerCheckIn(ci); return null; }
            catch (NegocioException_GV42 ex) { return ex.Message; }
        }

        private CheckIn_GV42 Buscar(string numeroReserva, string dniPasajero, int tramo)
        {
            numeroReserva = (numeroReserva ?? string.Empty).Trim().ToUpper();
            dniPasajero = (dniPasajero ?? string.Empty).Trim();

            if (numeroReserva.Length == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.indiqueNumero"));
            if (!Validaciones_GV42.EsDniValido(dniPasajero))
                throw new NegocioException_GV42(Validaciones_GV42.MENSAJE_DNI);

            CheckIn_GV42 ci = _dalCheckIn.BuscarPorReservaYDni(numeroReserva, dniPasajero, tramo);
            if (ci == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.noEncontrado"));
            return ci;
        }

        private void ValidarPuedeHacerCheckIn(CheckIn_GV42 ci)
        {
            if (ci.EstadoReserva != EstadoReserva_GV42.Confirmada || ci.Id <= 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.noConfirmada", ci.NumeroReserva, ci.EstadoReservaTexto));

            if (ci.Estado == EstadoCheckIn_GV42.Realizado)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.yaRealizado"));

            ValidarVentana(ci);
        }

        private static void ValidarVentana(CheckIn_GV42 ci)
        {
            DateTime salida = ci.Vuelo.FechaHoraSalida;
            DateTime ahora = DateTime.Now;
            if (ahora < salida.AddHours(-HORAS_APERTURA_CHECKIN) || ahora > salida.AddMinutes(-MINUTOS_CIERRE_CHECKIN))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.fueraDeVentana", HORAS_APERTURA_CHECKIN, MINUTOS_CIERRE_CHECKIN));
        }

        // Recarga el check-in y verifica que todavía se pueda operar sobre él.
        private CheckIn_GV42 ObtenerPendiente(int idCheckIn)
        {
            CheckIn_GV42 ci = _dalCheckIn.BuscarPorId(idCheckIn);
            if (ci == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.noExiste"));

            ExigirAcceso(ci);
            ValidarPuedeHacerCheckIn(ci);
            return ci;
        }

        #endregion

        #region Equipaje (pasos 5 a 7)

        // Franquicia del pasajero: las valijas de su clase más las valijas de equipaje extra que se
        // compraron PARA ÉL al reservar (cada valija extra tiene dueño: no la puede usar otro pasajero).
        public FranquiciaEquipaje_GV42 ObtenerFranquicia(int idCheckIn)
        {
            return CalcularFranquicia(ObtenerParaEquipaje(idCheckIn));
        }

        private static FranquiciaEquipaje_GV42 CalcularFranquicia(CheckIn_GV42 ci)
        {
            int extra = Math.Max(0, ci.EquipajeExtraComprado);
            return new FranquiciaEquipaje_GV42
            {
                // Valijas incluidas: las que define la tarifa de la reserva (Light 0, Plus 1, Top 2).
                BultosIncluidos = Math.Max(0, ci.ValijasIncluidas),
                KgPorBultoIncluido = ci.VueloClase.FranquiciaEquipajeKg,
                KgPorUnidadExtra = KG_POR_EQUIPAJE_EXTRA,
                UnidadesExtraDisponibles = extra,
                UnidadesExtraMaxPasajero = extra,
                PesoMaximoPorBulto = MAX_PESO_POR_BULTO,
                CostoKiloExceso = ci.Vuelo.CostoKiloExceso
            };
        }

        // Calcula el cargo por exceso sin guardar nada (para mostrárselo al pasajero antes de cobrar).
        // 'pesosPorBulto' trae el peso de cada valija. Si no hay exceso, KilosExceso e ImporteCargo valen 0.
        public CargoExcesoEquipaje_GV42 CalcularCargoExceso(int idCheckIn, IList<decimal> pesosPorBulto)
        {
            CheckIn_GV42 ci = ObtenerParaEquipaje(idCheckIn);
            return CalcularCargo(CalcularFranquicia(ci), pesosPorBulto, ci.TarifaNombre ?? ci.VueloClase.ClaseTexto);
        }

        // Valida cantidad y peso de cada valija y calcula el exceso. A cada valija se le asigna una
        // franquicia (primero las incluidas en la clase y después las de equipaje extra): las valijas
        // más pesadas se cubren con las franquicias más grandes para cobrar lo menos posible.
        private static CargoExcesoEquipaje_GV42 CalcularCargo(FranquiciaEquipaje_GV42 f, IList<decimal> pesos, string claseTexto)
        {
            if (pesos == null || pesos.Count == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.bultosMinimo"));
            if (pesos.Count > f.BultosPermitidos)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.bultosPermitidos",
                    f.BultosPermitidos, f.BultosIncluidos, claseTexto ?? "", f.UnidadesExtraAplicables));

            for (int i = 0; i < pesos.Count; i++)
            {
                if (pesos[i] <= 0)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.pesoBultoCero", i + 1));
                if (pesos[i] > f.PesoMaximoPorBulto)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.pesoBultoMaximo",
                        i + 1, pesos[i].ToString("0.##"), f.PesoMaximoPorBulto.ToString("0.##")));
            }

            int incluidas = Math.Min(pesos.Count, f.BultosIncluidos);
            int extras = pesos.Count - incluidas;
            var franquicias = Enumerable.Repeat(f.KgPorBultoIncluido, incluidas)
                .Concat(Enumerable.Repeat(f.KgPorUnidadExtra, extras))
                .OrderByDescending(x => x).ToList();
            var ordenados = pesos.OrderByDescending(x => x).ToList();

            decimal exceso = 0m;
            for (int i = 0; i < ordenados.Count; i++)
                exceso += Math.Max(0m, ordenados[i] - franquicias[i]);

            return new CargoExcesoEquipaje_GV42
            {
                FranquiciaKg = franquicias.Sum(),
                UnidadesExtraUsadas = extras,
                KilosExceso = exceso,
                CostoPorKilo = f.CostoKiloExceso,
                ImporteCargo = Math.Round(exceso * f.CostoKiloExceso, 2)
            };
        }

        // Registra el equipaje despachado y genera una etiqueta por bulto. Si el peso supera la franquicia
        // también registra el cargo por exceso y su cobro: con tarjeta se validan los datos (Luhn,
        // vencimiento, código) y se genera el código de autorización; con transferencia se pide el número
        // de operación; en efectivo el número lo genera el sistema. Solo en el mostrador.
        public Equipaje_GV42 RegistrarEquipaje(int idCheckIn, IList<decimal> pesosPorBulto,
                                               MedioPago_GV42? medioCobro, string numeroOperacion, DatosTarjeta_GV42 tarjeta)
        {
            if (!PuedeAtenderMostrador())
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.equipajeSoloMostrador"));

            CheckIn_GV42 ci = ObtenerParaEquipaje(idCheckIn);
            if (ci.Equipaje != null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.yaDespacho"));

            FranquiciaEquipaje_GV42 franquicia = CalcularFranquicia(ci);
            CargoExcesoEquipaje_GV42 cargo = CalcularCargo(franquicia, pesosPorBulto, ci.VueloClase.ClaseTexto);
            int cantidadBultos = pesosPorBulto.Count;
            decimal pesoTotalKg = pesosPorBulto.Sum();
            int unidadesUsadas = cargo.UnidadesExtraUsadas;
            decimal franquiciaAplicada = cargo.FranquiciaKg;

            if (cargo.TieneExceso)
            {
                if (!medioCobro.HasValue)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.excesoSinMedio",
                        cargo.KilosExceso.ToString("0.##"), BLLNegocioUtil_GV42.Dinero(cargo.ImporteCargo)));

                cargo.MedioPago = medioCobro;
                cargo.NumeroTransaccion = NumeroTransaccionCobro(medioCobro.Value, numeroOperacion, tarjeta);
            }
            else
            {
                cargo = null;
            }

            var equipaje = new Equipaje_GV42
            {
                IdCheckIn = ci.Id,
                CantidadBultos = cantidadBultos,
                PesoTotalKg = pesoTotalKg,
                FranquiciaKg = franquiciaAplicada,
                UnidadesExtra = unidadesUsadas,
                CargoExceso = cargo
            };

            // Códigos de equipaje: EQ + Nº de check-in + Nº de bulto (ej: EQ000012-01).
            for (int i = 1; i <= cantidadBultos; i++)
            {
                equipaje.Etiquetas.Add("EQ" + ci.Id.ToString("D6") + "-" + i.ToString("D2"));
                equipaje.PesosKg.Add(Math.Round(pesosPorBulto[i - 1], 2));
            }

            Equipaje_GV42 guardado = _dalEquipaje.Registrar(equipaje);

            string detalle = ci.NumeroReserva + " - DNI " + ci.Pasajero.DNI + " - " + cantidadBultos + " bulto(s), " +
                             pesoTotalKg.ToString("0.##") + " kg";
            if (unidadesUsadas > 0) detalle += " - equipaje extra x" + unidadesUsadas;
            if (cargo != null) detalle += " - exceso " + BLLNegocioUtil_GV42.Dinero(cargo.ImporteCargo) + " (" + cargo.MedioPago + ")";
            if (ci.Estado == EstadoCheckIn_GV42.Realizado) detalle += " - despacho posterior al check-in online";
            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_CHECKIN, "Equipaje despachado", detalle, "Baja");

            return guardado;
        }

        private static string NumeroTransaccionCobro(MedioPago_GV42 medio, string numeroOperacion, DatosTarjeta_GV42 tarjeta)
        {
            numeroOperacion = (numeroOperacion ?? string.Empty).Trim();
            switch (medio)
            {
                case MedioPago_GV42.Efectivo:
                    return "EFE-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                case MedioPago_GV42.Transferencia:
                    if (numeroOperacion.Length == 0)
                        throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.faltaTransaccion"));
                    if (!Regex.IsMatch(numeroOperacion, Validaciones_GV42.REGEX_TX_TRANSFERENCIA))
                        throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.transferenciaInvalida"));
                    return numeroOperacion;
                default:
                    return BLLNegocioUtil_GV42.AutorizarTarjeta(tarjeta, medio);
            }
        }

        // El equipaje se despacha durante el check-in en el mostrador o, si el pasajero ya hizo el
        // check-in online, después en el mostrador (mientras la ventana siga abierta).
        private CheckIn_GV42 ObtenerParaEquipaje(int idCheckIn)
        {
            CheckIn_GV42 ci = _dalCheckIn.BuscarPorId(idCheckIn);
            if (ci == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.noExiste"));
            ExigirAcceso(ci);

            if (ci.Estado == EstadoCheckIn_GV42.Realizado && ci.EsOnline && PuedeAtenderMostrador())
            {
                ValidarVentana(ci);
                return ci;
            }
            ValidarPuedeHacerCheckIn(ci);
            return ci;
        }

        // Pasajeros que hicieron el check-in online y todavía pueden despachar valijas en el mostrador.
        public bool PuedeDespacharPosterior(CheckIn_GV42 ci)
        {
            if (ci == null || !ci.EsOnline || ci.Estado != EstadoCheckIn_GV42.Realizado || ci.Equipaje != null) return false;
            if (!PuedeAtenderMostrador()) return false;
            try { ValidarVentana(ci); return true; }
            catch (NegocioException_GV42) { return false; }
        }

        #endregion

        #region Asiento (paso 8: validar o cambiar)

        // Mapa de la clase del pasajero. Las butacas preferenciales solo se pueden elegir si el pasajero
        // ya tenía una (la pagó al reservar): el recargo no se cobra en el check-in.
        public List<AsientoDisponibilidad_GV42> ObtenerMapaAsientos(int idCheckIn)
        {
            CheckIn_GV42 ci = ObtenerPendiente(idCheckIn);
            return _dalAsiento.ListarMapa(ci.Vuelo.Id, ci.VueloClase.Clase);
        }

        public bool PuedeElegirPreferencial(CheckIn_GV42 ci)
        {
            // Butaca preferencial: si ya la tiene (la pagó al reservar) o si su tarifa la incluye (Top).
            return ci != null && ((ci.Asiento != null && ci.Asiento.EsPreferencial) || ci.TarifaIncluyePreferencial);
        }

        // Elegir o cambiar el asiento en el check-in: solo si la tarifa lo incluye o si se pagó la
        // selección al reservar. Si no, el asiento lo asigna el sistema (no se cobra en el check-in).
        public bool PuedeElegirAsiento(CheckIn_GV42 ci)
        {
            return ci != null && ci.PuedeElegirAsiento;
        }

        // Confirma el asiento que el pasajero eligió al reservar. Si no tiene (tarifa Light sin la
        // selección paga, o reservas viejas), se le asigna uno libre de su clase elegido al azar.
        public Asiento_GV42 ValidarAsiento(int idCheckIn)
        {
            CheckIn_GV42 ci = ObtenerPendiente(idCheckIn);
            if (ci.Asiento != null) return ci.Asiento;

            Asiento_GV42 libre = _dalAsiento.BuscarLibreAlAzar(ci.Vuelo.Id, ci.VueloClase.Clase);
            if (libre == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.sinAsientosClase", ci.VueloClase.ClaseTexto.ToLower()));

            _dalAsiento.Asignar(ci.Id, libre.Id);
            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_CHECKIN, "Asiento asignado",
                ci.NumeroReserva + " - DNI " + ci.Pasajero.DNI + " - asiento " + libre.NumeroAsiento, "Baja");
            return libre;
        }

        // El pasajero pide otro asiento (CUN10 Seleccionar asiento, extensión del check-in).
        public Asiento_GV42 CambiarAsiento(int idCheckIn, int idAsientoNuevo)
        {
            CheckIn_GV42 ci = ObtenerPendiente(idCheckIn);
            if (!PuedeElegirAsiento(ci))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.seleccionNoPagada", ci.TarifaNombre));

            Asiento_GV42 asiento = _dalAsiento.BuscarPorId(idAsientoNuevo);
            if (asiento == null || asiento.IdVuelo != ci.Vuelo.Id)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.asientoNoExiste", idAsientoNuevo));
            if (asiento.Clase != ci.VueloClase.Clase)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.asientoOtraClase",
                    asiento.NumeroAsiento, asiento.ClaseTexto.ToLower(), ci.VueloClase.ClaseTexto.ToLower()));
            if (ci.Asiento != null && ci.Asiento.Id == asiento.Id) return asiento;
            if (asiento.EsPreferencial && !PuedeElegirPreferencial(ci))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.preferencialNoPagado", asiento.NumeroAsiento));
            if (_dalAsiento.EstaOcupadoPorOtro(asiento.Id, ci.Id))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.asientoOcupado", asiento.NumeroAsiento));

            string anterior = ci.Asiento != null ? ci.Asiento.NumeroAsiento : "-";
            _dalAsiento.Asignar(ci.Id, asiento.Id);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_CHECKIN, "Asiento asignado",
                ci.NumeroReserva + " - DNI " + ci.Pasajero.DNI + " - cambio de asiento " + anterior + " -> " + asiento.NumeroAsiento, "Baja");
            return asiento;
        }

        #endregion

        #region Tarjeta de embarque y cierre (pasos 9 y 10)

        // Emite la tarjeta de embarque (si todavía no existe) y cambia el estado del check-in a
        // "Realizado". Devuelve el check-in completo para mostrar e imprimir la tarjeta y las etiquetas.
        public CheckIn_GV42 ConfirmarCheckIn(int idCheckIn)
        {
            string login = BLLNegocioUtil_GV42.LoginActual();
            CheckIn_GV42 ci = ObtenerPendiente(idCheckIn);

            if (ci.Asiento == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.primeroAsiento"));

            TarjetaEmbarque_GV42 tarjeta = ci.TarjetaEmbarque;
            if (tarjeta == null)
            {
                DateTime horaLimite = ci.Vuelo.FechaHoraSalida.AddMinutes(-MINUTOS_LIMITE_EMBARQUE);
                tarjeta = _dalTarjeta.Generar(ci.Id, ci.Vuelo.PuertaEmbarque, horaLimite);

                BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_CHECKIN, "Tarjeta de embarque emitida",
                    tarjeta.NumeroTarjeta + " - " + ci.NumeroReserva + " - asiento " + tarjeta.NumeroAsiento, "Baja");
            }

            CanalVenta_GV42 canal = CanalActual();
            _dalCheckIn.MarcarRealizado(ci.Id, login, canal);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_CHECKIN, "Check-in realizado",
                ci.NumeroReserva + " - DNI " + ci.Pasajero.DNI + " - " + (canal == CanalVenta_GV42.Presencial ? "mostrador" : "online"), "Media");

            return _dalCheckIn.BuscarPorId(ci.Id);
        }

        // Vuelve a leer un check-in por su Id (para refrescar la pantalla después de cada paso).
        public CheckIn_GV42 Obtener(int idCheckIn)
        {
            CheckIn_GV42 ci = _dalCheckIn.BuscarPorId(idCheckIn);
            if (ci == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.checkin.noExiste"));
            ExigirAcceso(ci);
            return ci;
        }

        #endregion
    }
}
