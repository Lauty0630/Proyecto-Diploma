using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    // RFN 1 - Reserva de vuelo (atención presencial: Vendedor + Cliente).
    // Los mensajes que llegan al usuario salen del archivo de idioma (claves "neg.");
    // lo que se guarda en la bitácora queda en español porque es dato.
    public class BLLReserva_GV42
    {
        #region Constantes

        // Tasa de impuestos aplicada sobre (importe base + adicionales). Ajustar según el enunciado.
        public const decimal TASA_IMPUESTOS = 0.21m;

        public const int MAX_PASAJEROS_POR_RESERVA = 9;

        // Tope absoluto por servicio (además del tope por pasajero de MaximoAdicional).
        public const int MAX_CANTIDAD_ADICIONAL = 20;
        public const decimal MAX_COSTO_ADICIONAL = 999999m;

        // Reglas de penalidad según el tiempo que falta para la salida (ajustable si la cátedra
        // pide otros porcentajes u horas de corte):
        //   72 hs o más antes de la salida -> sin cargo.
        //   entre 24 y 72 hs                -> 30% del importe total.
        //   menos de 24 hs                  -> 100% del importe total (sin reembolso).
        public const int HORAS_SIN_PENALIDAD = 72;
        public const int HORAS_PENALIDAD_PARCIAL = 24;
        public const decimal PORCENTAJE_PENALIDAD_PARCIAL = 0.30m;
        public const decimal PORCENTAJE_PENALIDAD_TOTAL = 1.00m;

        // Plazo para pagar una reserva pendiente. Pasado ese plazo (o la salida del vuelo, lo que ocurra
        // primero) la reserva vence: se cancela sola y libera los asientos.
        public const int MINUTOS_VENCIMIENTO_AUTOGESTION = 30;   // la sacó el cliente desde su cuenta
        public const int HORAS_VENCIMIENTO_PRESENCIAL = 24;      // la sacó un vendedor

        // Tarifa según el tipo de pasajero: el niño (2 a 11 años) paga con descuento y el infante
        // (menor de 2, viaja en brazos y no ocupa asiento) una fracción de la tarifa.
        public const decimal DESCUENTO_NINO = 0.25m;
        public const decimal PORCENTAJE_TARIFA_INFANTE = 0.10m;

        // Un vuelo se puede cambiar hasta estas horas antes de su salida.
        public const int HORAS_LIMITE_CAMBIO = 24;

        // Fechas flexibles: días que se muestran antes y después de la fecha buscada.
        public const int DIAS_FECHAS_FLEXIBLES = 3;

        #endregion

        #region Campos

        private readonly DALAeropuerto_GV42 _dalAeropuerto;
        private readonly DALTipoAdicional_GV42 _dalTipoAdicional;
        private readonly DALVuelo_GV42 _dalVuelo;
        private readonly DALPasajero_GV42 _dalPasajero;
        private readonly DALReserva_GV42 _dalReserva;
        private readonly DALPago_GV42 _dalPago;
        private readonly DALBoleto_GV42 _dalBoleto;
        private readonly DALAsiento_GV42 _dalAsiento;
        private readonly DALTarifa_GV42 _dalTarifa;
        private readonly BLLIntegridad_GV42 _bllIntegridad;


        #endregion

        #region Constructor

        public BLLReserva_GV42()
        {
            _dalAeropuerto = new DALAeropuerto_GV42();
            _dalTipoAdicional = new DALTipoAdicional_GV42();
            _dalVuelo = new DALVuelo_GV42();
            _dalPasajero = new DALPasajero_GV42();
            _dalReserva = new DALReserva_GV42();
            _dalPago = new DALPago_GV42();
            _dalBoleto = new DALBoleto_GV42();
            _dalAsiento = new DALAsiento_GV42();
            _dalTarifa = new DALTarifa_GV42();
            _bllIntegridad = new BLLIntegridad_GV42();
        }

        #endregion

        #region Catálogos

        // ---- Catálogos para armar los combos de la pantalla ----

        public List<Aeropuerto_GV42> ListarAeropuertos()
        {
            return _dalAeropuerto.ListarTodos();
        }

        // Servicios que se eligen a mano en el paso "Adicionales". El recargo por butaca preferencial
        // no aparece: lo agrega el sistema según las butacas elegidas.
        public List<TipoAdicional_GV42> ListarTiposAdicional()
        {
            return _dalTipoAdicional.ListarActivos().Where(t => t.SeleccionManual).ToList();
        }

        // Servicio "Equipaje extra" del catálogo (precio y tope de valijas por pasajero). Se elige por
        // pasajero: cada valija extra queda a nombre de quien la va a despachar. Null si no está configurado.
        public TipoAdicional_GV42 ObtenerServicioEquipajeExtra()
        {
            return _dalTipoAdicional.ListarActivos().FirstOrDefault(t => t.EsEquipajeExtra);
        }

        // Familias tarifarias (Light / Plus / Top), de la más económica a la más completa.
        public List<TarifaFamilia_GV42> ListarTarifas()
        {
            return _dalTarifa.ListarActivas();
        }

        // Servicio que cobra la elección de asiento en las tarifas donde no está incluida (Light).
        public TipoAdicional_GV42 ObtenerServicioSeleccionAsiento()
        {
            return _dalTipoAdicional.ListarActivos().FirstOrDefault(t => t.EsSeleccionAsiento);
        }

        // Cuánto de la tarifa paga cada tipo de pasajero (1 = completa).
        public static decimal FactorTarifa(TipoPasajero_GV42 tipo)
        {
            switch (tipo)
            {
                case TipoPasajero_GV42.Nino: return 1m - DESCUENTO_NINO;
                case TipoPasajero_GV42.Infante: return PORCENTAJE_TARIFA_INFANTE;
                default: return 1m;
            }
        }

        // Servicio "Asiento preferencial" del catálogo (precio del recargo). Null si no está configurado.
        public TipoAdicional_GV42 ObtenerServicioAsientoPreferencial()
        {
            return _dalTipoAdicional.ListarActivos().FirstOrDefault(t => t.EsAsientoPreferencial);
        }

        #endregion

        #region Búsqueda de vuelos

        // ---- Pasos 1 a 4: buscar vuelos disponibles ----

        public List<VueloClase_GV42> BuscarVuelosDisponibles(CriterioBusquedaVuelo_GV42 criterio)
        {
            ValidarCriterio(criterio);
            AplicarVencimientos();
            return _dalVuelo.BuscarDisponibles(criterio);
        }

        // Fechas flexibles: para los días cercanos a la fecha buscada (3 antes y 3 después, sin pasar
        // de hoy), el precio más barato de la ruta. La pantalla los muestra para elegir otro día sin
        // tener que buscar de nuevo. 'desdeMinimo': no se ofrecen días anteriores (ej.: para la vuelta,
        // el día en que llega la ida).
        public List<PrecioFecha_GV42> PreciosPorFecha(CriterioBusquedaVuelo_GV42 criterio, DateTime? desdeMinimo = null)
        {
            if (criterio == null || criterio.IdOrigen <= 0 || criterio.IdDestino <= 0 || criterio.IdOrigen == criterio.IdDestino)
                return new List<PrecioFecha_GV42>();

            DateTime minimo = desdeMinimo.HasValue && desdeMinimo.Value.Date > DateTime.Today ? desdeMinimo.Value.Date : DateTime.Today;
            DateTime centro = criterio.FechaSalida.Date < minimo ? minimo : criterio.FechaSalida.Date;
            DateTime desde = centro.AddDays(-DIAS_FECHAS_FLEXIBLES);
            if (desde < minimo) desde = minimo;
            DateTime hasta = desde.AddDays(2 * DIAS_FECHAS_FLEXIBLES);

            AplicarVencimientos();
            Dictionary<DateTime, decimal> precios = _dalVuelo.PreciosMinimosPorFecha(criterio, desde, hasta);

            var lista = new List<PrecioFecha_GV42>();
            for (DateTime d = desde; d <= hasta; d = d.AddDays(1))
                lista.Add(new PrecioFecha_GV42 { Fecha = d, HayVuelos = precios.ContainsKey(d), PrecioMinimo = precios.ContainsKey(d) ? precios[d] : 0m });
            return lista;
        }

        // Ida y vuelta: vuelos de regreso para el vuelo de ida elegido. Es la misma ruta invertida, en la
        // fecha de regreso, y tiene que salir después de que llegue la ida. La clase puede ser otra
        // (como en los sitios de venta de pasajes, cada tramo tiene su propia tarifa).
        public List<VueloClase_GV42> BuscarVuelosDeRegreso(VueloClase_GV42 ida, DateTime fechaRegreso,
                                                           int cantidadPasajeros, ClaseVuelo_GV42? clase)
        {
            if (ida == null || ida.Vuelo == null || ida.Vuelo.Origen == null || ida.Vuelo.Destino == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.seleccioneVueloClase"));
            if (fechaRegreso.Date < ida.Vuelo.FechaHoraLlegada.Date)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.regresoAnterior"));

            var criterio = new CriterioBusquedaVuelo_GV42
            {
                IdOrigen = ida.Vuelo.Destino.Id,
                IdDestino = ida.Vuelo.Origen.Id,
                FechaSalida = fechaRegreso.Date,
                CantidadPasajeros = cantidadPasajeros,
                TipoViaje = TipoViaje_GV42.Ida,
                Clase = clase
            };
            ValidarCriterio(criterio);
            AplicarVencimientos();
            return _dalVuelo.BuscarDisponibles(criterio)
                            .Where(v => v.Vuelo.FechaHoraSalida > ida.Vuelo.FechaHoraLlegada)
                            .ToList();
        }

        private void ValidarCriterio(CriterioBusquedaVuelo_GV42 c)
        {
            if (c == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.faltanDatosBusqueda"));
            if (c.IdOrigen <= 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.seleccioneOrigen"));
            if (c.IdDestino <= 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.seleccioneDestino"));
            if (c.IdOrigen == c.IdDestino)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.origenIgualDestino"));
            if (c.FechaSalida.Date < DateTime.Today)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.salidaAnteriorHoy"));
            if (c.CantidadPasajeros < 1)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.pasajerosMinimo"));

            if (c.TipoViaje == TipoViaje_GV42.IdaYVuelta)
            {
                if (!c.FechaRegreso.HasValue)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.faltaRegreso"));
                if (c.FechaRegreso.Value.Date < c.FechaSalida.Date)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.regresoAnterior"));
            }
        }

        #endregion

        #region Clientes y pasajeros

        // ---- Pasos 6 y 7: cliente (la tabla Pasajero guarda tanto al que reserva como al que viaja) ----

        // Devuelve null si el DNI no está registrado como pasajero.
        public Pasajero_GV42 BuscarPasajero(string dni)
        {
            ExigirVendedorParaBuscarPersonas();
            dni = (dni ?? string.Empty).Trim();
            if (!Servicios.Validaciones_GV42.EsDniValido(dni))
                throw new NegocioException_GV42(Servicios.Validaciones_GV42.MENSAJE_DNI);
            return _dalPasajero.BuscarPorDni(dni);
        }

        // Si el DNI no está en Pasajero pero sí tiene una cuenta de Usuario, devuelve sus datos
        // (sin teléfono) para precargar el formulario de alta. Null si tampoco tiene cuenta.
        public Pasajero_GV42 PrecargarDesdeUsuario(string dni)
        {
            ExigirVendedorParaBuscarPersonas();
            dni = (dni ?? string.Empty).Trim();
            if (!Servicios.Validaciones_GV42.EsDniValido(dni))
                throw new NegocioException_GV42(Servicios.Validaciones_GV42.MENSAJE_DNI);
            return _dalPasajero.BuscarDatosEnUsuario(dni);
        }

        // Buscar personas por DNI devuelve datos personales (email, teléfono) de terceros:
        // solo lo puede hacer quien vende para terceros, nunca un cliente autogestionado.
        private static void ExigirVendedorParaBuscarPersonas()
        {
            if (!PatentesActuales().Contains("Reservas.Generar"))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.sinPermisoBuscarPersonas"));
        }

        // ---- Identidad de las personas ----
        // Un DNI identifica a UNA persona en todo el sistema: puede estar en Usuario (tiene cuenta),
        // en Pasajero (reservó o viajó) o en las dos. Usuario y Pasajero siguen siendo tablas distintas
        // (hay usuarios que nunca viajaron y pasajeros sin cuenta), pero si el DNI ya existe en
        // cualquiera de las dos, el nombre y apellido cargados tienen que coincidir con los registrados.
        // Antes esto no se controlaba: se podía cargar como pasajero un DNI existente con otro nombre
        // y, al confirmar, la reserva PISABA los datos de esa persona en la tabla Pasajero.

        // Datos registrados para un DNI: primero Pasajero y, si no está, la cuenta de Usuario.
        private Pasajero_GV42 PersonaRegistrada(string dni, out bool esPasajero)
        {
            Pasajero_GV42 p = _dalPasajero.BuscarPorDni(dni);
            esPasajero = p != null;
            return p ?? _dalPasajero.BuscarDatosEnUsuario(dni);
        }

        // Lanza excepción si el DNI ya pertenece a otra persona. 'mostrarRegistrado' indica si el
        // mensaje puede decir a nombre de quién está (vendedor sí; cliente autogestionado no, para no
        // exponer datos de terceros a partir de un DNI). 'rol' ya llega traducido.
        private void VerificarIdentidad(Persona_GV42 p, string rol, bool mostrarRegistrado)
        {
            Pasajero_GV42 registrado = PersonaRegistrada(p.DNI, out bool _);
            if (registrado == null) return;

            bool coincide = Servicios.Validaciones_GV42.MismoTexto(p.Nombre, registrado.Nombre) &&
                            Servicios.Validaciones_GV42.MismoTexto(p.Apellido, registrado.Apellido);
            if (coincide) return;

            if (mostrarRegistrado)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.dniOtroNombre",
                    rol, p.DNI, registrado.Nombre, registrado.Apellido));

            throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.dniOtroNombreOculto", rol, p.DNI));
        }

        public void RegistrarPasajero(Pasajero_GV42 pasajero)
        {
            BLLNegocioUtil_GV42.ValidarPersona(pasajero, IdiomaManager_GV42.T("neg.rol.cliente"));

            if (_dalPasajero.ExisteDni(pasajero.DNI))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.personaDniExiste", pasajero.DNI));

            // Si tiene cuenta de usuario, el nombre y apellido deben ser los de esa cuenta.
            VerificarIdentidad(pasajero, IdiomaManager_GV42.T("neg.rol.cliente"), true);

            _dalPasajero.Insertar(pasajero);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Cliente registrado",
                "DNI " + pasajero.DNI, "Baja");
        }

        // El propio cliente se registra y crea su cuenta para reservar sin pasar por un vendedor
        // (RFN 1 - CUN02, canal Autogestión). Da de alta Pasajero y Usuario (rol "Cliente") juntos;
        // si falla la creación del usuario, deshace el alta del pasajero.
        public Usuario_GV42 RegistrarClienteAutogestionado(Pasajero_GV42 cliente, string login,
                                                            string contrasenaPlana, string confirmarContrasena)
        {
            BLLNegocioUtil_GV42.ValidarPersona(cliente, IdiomaManager_GV42.T("neg.rol.cliente"));

            if (contrasenaPlana != confirmarContrasena)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.contrasenasNoCoinciden"));

            // Alguien que ya viajó (lo cargó un vendedor) puede crear su cuenta: se reutiliza su fila de
            // Pasajero siempre que el nombre y apellido coincidan. Si ya tiene cuenta, lo rechaza el alta
            // del usuario (DNI duplicado en Usuario).
            VerificarIdentidad(cliente, IdiomaManager_GV42.T("neg.rol.cliente"), false);
            bool yaEraPasajero = _dalPasajero.ExisteDni(cliente.DNI);

            // Si ya viajó (lo cargó un vendedor), además del nombre tiene que coincidir el email que
            // dejó registrado: si no, cualquiera que conozca DNI, nombre y apellido podría crear la
            // cuenta de otra persona y ver o cancelar sus reservas.
            if (yaEraPasajero)
            {
                Pasajero_GV42 registrado = _dalPasajero.BuscarPorDni(cliente.DNI);
                if (registrado != null && !string.IsNullOrWhiteSpace(registrado.Email) &&
                    !string.Equals(registrado.Email.Trim(), (cliente.Email ?? "").Trim(), StringComparison.OrdinalIgnoreCase))
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.dniConReservasEmail", cliente.DNI));
            }

            if (!yaEraPasajero)
                _dalPasajero.Insertar(cliente);

            try
            {
                Usuario_GV42 usuario = new BLLUsuario_GV42().CrearUsuarioAutogestionado(
                    cliente.DNI, cliente.Nombre, cliente.Apellido, cliente.Email, login, contrasenaPlana);
                return usuario;
            }
            catch (Exception)
            {
                // Compensación: sin usuario no hay cómo loguearse, así que no dejamos el cliente huérfano
                // (solo si lo acabamos de crear; un pasajero que ya existía queda como estaba).
                if (!yaEraPasajero)
                {
                    try { _dalPasajero.Eliminar(cliente.DNI); } catch { }
                }
                throw;
            }
        }

        #endregion

        #region Permisos de reservas

        // ---- Permisos de reservas ----
        // El modo de cada pantalla (vendedor / pasajero) se decide por las PATENTES del rol de la
        // sesión, no por el nombre del rol: "X" (general) = opera sobre cualquier reserva y para
        // cualquier cliente; "XPropia" = solo sobre las suyas y a su propio nombre.

        private static HashSet<string> PatentesActuales()
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            Rol_GV42 rol = actual?.Rol != null ? new BLLPermisos_GV42().ObtenerArbolRol(actual.Rol.Id) : null;
            var claves = rol != null
                ? rol.ObtenerPatentes().Select(p => p.DataKey ?? string.Empty)
                : Enumerable.Empty<string>();
            return new HashSet<string>(claves);
        }

        private static string DniSesion()
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            return actual != null ? (actual.DNI ?? string.Empty).Trim() : string.Empty;
        }

        private static bool EsDeLaSesion(Reserva_GV42 r)
        {
            return r != null && r.Cliente != null &&
                   string.Equals((r.Cliente.DNI ?? string.Empty).Trim(), DniSesion(), StringComparison.OrdinalIgnoreCase);
        }

        private static bool EsPasajeroDeLaSesion(Reserva_GV42 r)
        {
            string dni = DniSesion();
            return r != null && r.Pasajeros != null && dni.Length > 0 &&
                   r.Pasajeros.Any(p => p != null && string.Equals((p.DNI ?? string.Empty).Trim(), dni, StringComparison.OrdinalIgnoreCase));
        }

        // true si el usuario logueado es el titular (quien sacó la reserva). Un acompañante la ve en
        // "Mis reservas", puede ver su boleto y hacer su check-in, pero no pagarla ni cancelarla.
        public bool EsTitularEnSesion(Reserva_GV42 r) { return EsDeLaSesion(r); }

        // DNI del usuario si en esta reserva es solo un pasajero (no el titular ni un empleado que ve
        // todas las reservas): en ese caso solo se le muestra SU boleto. Null en los demás casos.
        public string DniSiEsSoloPasajero(Reserva_GV42 r)
        {
            var p = PatentesActuales();
            bool veAjenas = p.Contains("Reservas.Consultar") || p.Contains("Pagos.Registrar") || p.Contains("Reservas.Cancelar");
            if (veAjenas || EsDeLaSesion(r) || !EsPasajeroDeLaSesion(r)) return null;
            return DniSesion();
        }

        // Reservar para terceros: la pantalla incluye buscar/registrar al cliente (vendedor).
        public bool PuedeGenerarParaTerceros() { return PatentesActuales().Contains("Reservas.Generar"); }

        // Consultar todas las reservas (vendedor). Sin esta patente solo ve las propias.
        public bool PuedeConsultarTodas() { return PatentesActuales().Contains("Reservas.Consultar"); }
        public bool PuedeRegistrarPagoDeTerceros() { return PatentesActuales().Contains("Pagos.Registrar"); }

        public bool PuedeCancelar()
        {
            var p = PatentesActuales();
            return p.Contains("Reservas.Cancelar") || p.Contains("Reservas.CancelarPropia");
        }

        // Autogestión = solo tiene la patente "propia" para generar (el vendedor tiene la general).
        private CanalVenta_GV42 CanalSegunSesion()
        {
            var p = PatentesActuales();
            bool autogestion = !p.Contains("Reservas.Generar") && p.Contains("Reservas.GenerarPropia");
            return autogestion ? CanalVenta_GV42.Autogestion : CanalVenta_GV42.Presencial;
        }

        #endregion

        #region Generar reserva

        // ---- Pasos 8 a 10: generar la reserva en estado "Pendiente de Pago" ----

        // 'borrador' debe traer: Cliente, VueloClase (con Vuelo.Id y Clase), TipoViaje, VueloClaseVuelta (si es
        // ida y vuelta), Pasajeros, AsientosPorPasajero (uno por pasajero y por tramo) y Adicionales (opcional,
        // con su tramo). Importes, fecha de regreso, estado y vendedor los completa esta clase.
        // Asientos libres de una clase de un vuelo, para pintar la grilla de selección estilo cine.
        public List<AsientoDisponibilidad_GV42> ObtenerMapaAsientos(int idVuelo, ClaseVuelo_GV42 clase)
        {
            if (_dalVuelo.BuscarVueloClase(idVuelo, clase) == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.claseNoOfrecida"));
            AplicarVencimientos();   // los asientos de reservas vencidas ya figuran libres
            return _dalAsiento.ListarMapa(idVuelo, clase);
        }

        // Datos del usuario logueado como pasajero titular: nombre, apellido y email salen de la
        // sesión; el teléfono, si ya reservó antes, de su fila en Pasajero (Usuario no lo guarda).
        public Pasajero_GV42 ObtenerTitularDeSesion()
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            if (actual == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.sinSesion"));

            string dni = (actual.DNI ?? string.Empty).Trim();
            Pasajero_GV42 guardado = _dalPasajero.BuscarPorDni(dni);
            return new Pasajero_GV42(dni, actual.Nombre, actual.Apellido, actual.Email,
                                     guardado != null ? guardado.Telefono : string.Empty)
            {
                // Usuario no guarda fecha de nacimiento: sale de su fila de Pasajero si ya reservó.
                FechaNacimiento = guardado != null ? guardado.FechaNacimiento : null
            };
        }

        public Reserva_GV42 GenerarReserva(Reserva_GV42 borrador)
        {
            if (borrador == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.faltanDatos"));
            if (borrador.VueloClase == null || borrador.VueloClase.Vuelo == null || borrador.VueloClase.Vuelo.Id <= 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.seleccioneVueloClase"));

            string login = BLLNegocioUtil_GV42.LoginActual();
            CanalVenta_GV42 canal = CanalSegunSesion();
            borrador.CanalVenta = canal;

            var patentes = PatentesActuales();
            if (!patentes.Contains("Reservas.Generar") && !patentes.Contains("Reservas.GenerarPropia"))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.sinPermisoGenerar"));

            // Las reservas impagas que ya vencieron dejan de ocupar cupo y asientos.
            AplicarVencimientos();

            // El precio y la disponibilidad se toman siempre de la base, no de lo que traiga la pantalla.
            VueloClase_GV42 vc = _dalVuelo.BuscarVueloClase(borrador.VueloClase.Vuelo.Id, borrador.VueloClase.Clase);
            if (vc == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.claseNoOfrecida"));
            if (vc.Vuelo.FechaHoraSalida <= DateTime.Now)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.vueloSalio"));

            // Familia tarifaria: también de la base.
            TarifaFamilia_GV42 tarifa = borrador.Tarifa != null ? _dalTarifa.BuscarPorId(borrador.Tarifa.Id) : null;
            if (tarifa == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.faltaTarifa"));
            borrador.Tarifa = tarifa;

            if (canal == CanalVenta_GV42.Autogestion)
            {
                // El cliente reserva para sí mismo: el cliente de la reserva sale siempre de su sesión,
                // nunca de lo que venga (o no) de la pantalla, para que nadie reserve "a nombre de" otro DNI.
                Pasajero_GV42 titular = ObtenerTitularDeSesion();
                Pasajero_GV42 enPantalla = (borrador.Pasajeros ?? new List<Pasajero_GV42>())
                    .FirstOrDefault(x => x != null && string.Equals((x.DNI ?? string.Empty).Trim(), titular.DNI, StringComparison.OrdinalIgnoreCase));
                // Primera reserva: lo que la sesión no tiene (teléfono y fecha de nacimiento) se pide en pantalla.
                if (enPantalla != null && string.IsNullOrWhiteSpace(titular.Telefono)) titular.Telefono = enPantalla.Telefono;
                if (enPantalla != null && !titular.FechaNacimiento.HasValue) titular.FechaNacimiento = enPantalla.FechaNacimiento;
                BLLNegocioUtil_GV42.ValidarPersona(titular, IdiomaManager_GV42.T("neg.rol.pasajero"));
                if (!_dalPasajero.ExisteDni(titular.DNI))
                    _dalPasajero.Insertar(titular);
                borrador.Cliente = titular;
            }
            else
            {
                if (borrador.Cliente == null || string.IsNullOrWhiteSpace(borrador.Cliente.DNI))
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.indiqueCliente"));
                if (!_dalPasajero.ExisteDni(borrador.Cliente.DNI.Trim()))
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.clienteNoRegistrado"));
            }

            // Datos, identidad, fecha de nacimiento y tipo (adulto / niño / infante) de cada pasajero.
            ValidarPasajerosParaReserva(borrador.Pasajeros, vc.Vuelo.FechaHoraSalida);

            // Los infantes viajan en brazos: no ocupan asiento.
            int asientosNecesarios = borrador.CantidadAsientos;
            if (vc.AsientosDisponibles < asientosNecesarios)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.quedanAsientos", vc.AsientosDisponibles));

            // Tramos del viaje: 1 = ida; 2 = vuelta (solo en ida y vuelta, con su propio vuelo).
            bool idaYVuelta = borrador.TipoViaje == TipoViaje_GV42.IdaYVuelta;
            if (idaYVuelta && (borrador.VueloClaseVuelta == null || borrador.VueloClaseVuelta.Vuelo == null || borrador.VueloClaseVuelta.Vuelo.Id <= 0))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.faltaVueloVuelta"));
            if (!idaYVuelta) borrador.VueloClaseVuelta = null;
            int tramos = idaYVuelta ? 2 : 1;

            if (borrador.AsientosPorPasajero == null) borrador.AsientosPorPasajero = new List<AsientoPasajero_GV42>();
            if (borrador.Adicionales == null) borrador.Adicionales = new List<AdicionalReserva_GV42>();
            borrador.AsientosPorPasajero.RemoveAll(a => a == null);
            borrador.Adicionales.RemoveAll(a => a == null);
            foreach (AsientoPasajero_GV42 a in borrador.AsientosPorPasajero)
                a.Tramo = a.Tramo == Reserva_GV42.TRAMO_VUELTA ? Reserva_GV42.TRAMO_VUELTA : Reserva_GV42.TRAMO_IDA;
            foreach (AdicionalReserva_GV42 a in borrador.Adicionales)
                a.Tramo = a.Tramo == Reserva_GV42.TRAMO_VUELTA ? Reserva_GV42.TRAMO_VUELTA : Reserva_GV42.TRAMO_IDA;
            if (!idaYVuelta && (borrador.AsientosPorPasajero.Any(a => a.Tramo != Reserva_GV42.TRAMO_IDA)
                             || borrador.Adicionales.Any(a => a.Tramo != Reserva_GV42.TRAMO_IDA)))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.tramoSinVuelta"));

            // Los servicios automáticos (recargo por butaca preferencial, selección de asiento, equipaje
            // extra) los calcula el sistema: se descarta lo que haya mandado la pantalla para esos tipos.
            var automaticos = new HashSet<int>(_dalTipoAdicional.ListarActivos().Where(t => !t.SeleccionManual).Select(t => t.Id));
            borrador.Adicionales.RemoveAll(a => a.TipoAdicional != null && automaticos.Contains(a.TipoAdicional.Id));
            for (int tramo = 1; tramo <= tramos; tramo++)
                ValidarAdicionales(borrador.Adicionales.Where(a => a.Tramo == tramo).ToList(), canal, asientosNecesarios);

            // Vuelo de regreso: también se toma de la base. Tiene que ser la misma ruta invertida y salir
            // después de que llegue la ida.
            VueloClase_GV42 vcVuelta = null;
            if (idaYVuelta)
            {
                vcVuelta = _dalVuelo.BuscarVueloClase(borrador.VueloClaseVuelta.Vuelo.Id, borrador.VueloClaseVuelta.Clase);
                if (vcVuelta == null)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.claseNoOfrecida"));
                if (vcVuelta.Vuelo.Id == vc.Vuelo.Id
                    || vcVuelta.Vuelo.Origen.Id != vc.Vuelo.Destino.Id || vcVuelta.Vuelo.Destino.Id != vc.Vuelo.Origen.Id)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.vueltaRutaInvalida"));
                if (vcVuelta.Vuelo.FechaHoraSalida <= vc.Vuelo.FechaHoraLlegada)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.vueltaAntesDeLlegar"));
                if (vcVuelta.AsientosDisponibles < asientosNecesarios)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.vueltaSinCupo", vcVuelta.AsientosDisponibles));
                borrador.FechaRegreso = vcVuelta.Vuelo.FechaHoraSalida.Date;
            }
            else
            {
                borrador.FechaRegreso = null;
            }
            borrador.VueloClaseVuelta = vcVuelta;

            // Asientos, cargo por elegir asiento, recargo por butaca preferencial y equipaje extra:
            // por tramo (son vuelos distintos) y según lo que incluye la tarifa.
            for (int tramo = 1; tramo <= tramos; tramo++)
            {
                ValidarAsientos(borrador, tramo == Reserva_GV42.TRAMO_VUELTA ? vcVuelta : vc, tramo);
                AgregarSeleccionAsiento(borrador, tramo);
                AgregarRecargoPreferencial(borrador, tramo);
                AgregarEquipajeExtra(borrador, tramo);
            }

            borrador.VueloClase = vc;
            borrador.LoginVendedor = login;
            borrador.Estado = EstadoReserva_GV42.PendienteDePago;

            // Plazo para pagar: 30 minutos si reservó el cliente, 24 hs si reservó un vendedor, y nunca
            // después de la salida del vuelo.
            DateTime vencimiento = canal == CanalVenta_GV42.Autogestion
                ? DateTime.Now.AddMinutes(MINUTOS_VENCIMIENTO_AUTOGESTION)
                : DateTime.Now.AddHours(HORAS_VENCIMIENTO_PRESENCIAL);
            if (vencimiento > vc.Vuelo.FechaHoraSalida) vencimiento = vc.Vuelo.FechaHoraSalida;
            borrador.FechaVencimiento = vencimiento;

            // Tarifa: precio de la familia tarifaria para cada tramo, por lo que paga cada pasajero
            // según su tipo (adulto completa, niño con descuento, infante una fracción).
            borrador.ImporteBase = CalcularTarifaTramo(tarifa, vc, borrador.Pasajeros)
                                 + (vcVuelta != null ? CalcularTarifaTramo(tarifa, vcVuelta, borrador.Pasajeros) : 0m);
            borrador.SubtotalAdicionales = Math.Round(borrador.Adicionales.Sum(a => a.Subtotal), 2);
            borrador.Impuestos = Math.Round((borrador.ImporteBase + borrador.SubtotalAdicionales) * TASA_IMPUESTOS, 2);
            borrador.ImporteTotal = borrador.ImporteBase + borrador.SubtotalAdicionales + borrador.Impuestos;

            Reserva_GV42 creada = _dalReserva.Crear(borrador);
            RecalcularIntegridad("Reserva");

            string asientosTexto = string.Join(", ", creada.AsientosPorPasajero.Where(a => a.Asiento != null).Select(a => a.Asiento.NumeroAsiento));
            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Reserva generada",
                creada.NumeroReserva + " - vuelo " + vc.CodigoVuelo +
                (vcVuelta != null ? " - regreso " + vcVuelta.CodigoVuelo : "") + " - tarifa " + tarifa.Nombre + " - canal " + canal.ToString() +
                " - total " + BLLNegocioUtil_GV42.Dinero(creada.ImporteTotal), "Media");

            if (asientosTexto.Length > 0)
                BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Asiento seleccionado",
                    creada.NumeroReserva + ": " + asientosTexto, "Baja");

            return creada;
        }

        // Tarifa de un tramo para todos los pasajeros: precio de la familia tarifaria por lo que paga
        // cada uno según su tipo. Pública para que la pantalla muestre el mismo importe que se va a cobrar.
        public static decimal CalcularTarifaTramo(TarifaFamilia_GV42 tarifa, VueloClase_GV42 vc, IEnumerable<Pasajero_GV42> pasajeros)
        {
            decimal precio = tarifa != null ? tarifa.PrecioPara(vc.PrecioBase) : vc.PrecioBase;
            decimal factores = (pasajeros ?? Enumerable.Empty<Pasajero_GV42>()).Where(p => p != null).Sum(p => FactorTarifa(p.Tipo));
            return Math.Round(precio * factores, 2);
        }

        // Se confirma contra la base que el asiento exista, sea de ese vuelo y de esa clase y esté libre
        // (no se confía en los datos que trae la pantalla). Devuelve el asiento leído de la base.
        private Asiento_GV42 ValidarAsientoDeVuelo(Asiento_GV42 elegido, VueloClase_GV42 vc)
        {
            if (elegido == null || elegido.Id <= 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.faltaAsiento"));
            Asiento_GV42 enBase = _dalAsiento.BuscarPorId(elegido.Id);
            if (enBase == null || enBase.IdVuelo != vc.Vuelo.Id)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.asientoOtroVuelo"));
            if (enBase.Clase != vc.Clase)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.asientoOtraClase", enBase.NumeroAsiento));
            if (_dalAsiento.EstaReservado(enBase.Id))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.asientoOcupado", enBase.NumeroAsiento));
            return enBase;
        }

        // Selección de asiento estilo cine, por tramo. Cada pasajero queda con una fila en el tramo:
        //  - los infantes no llevan asiento (viajan en brazos);
        //  - si la tarifa incluye elegir asiento, todos los demás tienen que elegir el suyo;
        //  - si elegir asiento es pago (Light), se puede no elegir ninguno (se asignan gratis en el
        //    check-in) o elegirlos todos, pagando por cada uno.
        // La disponibilidad final la garantiza el índice único de la base (dos personas no pueden
        // quedarse con el mismo asiento aunque reserven al mismo tiempo).
        private void ValidarAsientos(Reserva_GV42 borrador, VueloClase_GV42 vc, int tramo)
        {
            var dnisPasajeros = new HashSet<string>(borrador.Pasajeros.Select(p => p.DNI));
            List<AsientoPasajero_GV42> delTramo = borrador.AsientosPorPasajero.Where(a => a.Tramo == tramo).ToList();

            AsientoPasajero_GV42 ajeno = delTramo.FirstOrDefault(a => !dnisPasajeros.Contains(a.DniPasajero));
            if (ajeno != null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.asientoSinPasajero",
                    ajeno.Asiento != null ? ajeno.Asiento.NumeroAsiento : "-"));
            var dniRepetido = delTramo.GroupBy(a => a.DniPasajero).FirstOrDefault(g => g.Count() > 1);
            if (dniRepetido != null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.pasajeroVariosAsientos", dniRepetido.Key));

            // Una fila por pasajero en el tramo (si la pantalla no la mandó, queda sin asiento).
            foreach (Pasajero_GV42 p in borrador.Pasajeros)
            {
                AsientoPasajero_GV42 fila = delTramo.FirstOrDefault(a => a.DniPasajero == p.DNI);
                if (fila == null)
                {
                    fila = new AsientoPasajero_GV42(p.DNI, null) { Tramo = tramo };
                    borrador.AsientosPorPasajero.Add(fila);
                    delTramo.Add(fila);
                }
                if (p.EsInfante) fila.Asiento = null;
            }

            var infantes = new HashSet<string>(borrador.Pasajeros.Where(p => p.EsInfante).Select(p => p.DNI));
            List<AsientoPasajero_GV42> conLugar = delTramo.Where(a => !infantes.Contains(a.DniPasajero)).ToList();
            int elegidos = conLugar.Count(a => a.Asiento != null && a.Asiento.Id > 0);

            if (borrador.Tarifa.ElegirAsientoEsPago)
            {
                if (elegidos != 0 && elegidos != conLugar.Count)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.asientosTodosONinguno"));
            }
            else if (elegidos != conLugar.Count)
            {
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.asientoPorPasajero"));
            }

            foreach (AsientoPasajero_GV42 ap in conLugar)
            {
                if (ap.Asiento == null || ap.Asiento.Id <= 0) { ap.Asiento = null; continue; }
                ap.Asiento = ValidarAsientoDeVuelo(ap.Asiento, vc);
            }

            var repetido = conLugar.Where(a => a.Asiento != null).GroupBy(a => a.Asiento.Id).FirstOrDefault(g => g.Count() > 1);
            if (repetido != null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.asientoRepetido"));
        }

        // Tarifas donde elegir asiento es pago (Light): una unidad del servicio "Selección de asiento"
        // por cada asiento elegido en el tramo, al precio del catálogo.
        private void AgregarSeleccionAsiento(Reserva_GV42 borrador, int tramo)
        {
            if (!borrador.Tarifa.ElegirAsientoEsPago) return;
            int cantidad = borrador.AsientosPorPasajero.Count(a => a.Tramo == tramo && a.Asiento != null);
            if (cantidad == 0) return;

            TipoAdicional_GV42 servicio = ObtenerServicioSeleccionAsiento();
            if (servicio == null || servicio.PrecioUnitario <= 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.seleccionSinPrecio"));

            borrador.Adicionales.Add(new AdicionalReserva_GV42
            {
                TipoAdicional = servicio,
                Tramo = tramo,
                Cantidad = cantidad,
                CostoUnitario = Math.Round(servicio.PrecioUnitario, 2)
            });
        }

        // Una unidad del servicio "Asiento preferencial" por cada butaca preferencial elegida, con el
        // precio del catálogo (igual para vendedor y cliente). No se cobra si la tarifa las incluye (Top).
        private void AgregarRecargoPreferencial(Reserva_GV42 borrador, int tramo)
        {
            if (borrador.Tarifa.IncluyePreferencial) return;
            int cantidad = borrador.AsientosPorPasajero
                .Count(a => a.Tramo == tramo && a.Asiento != null && a.Asiento.EsPreferencial);
            if (cantidad == 0) return;

            TipoAdicional_GV42 servicio = ObtenerServicioAsientoPreferencial();
            if (servicio == null || servicio.PrecioUnitario <= 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.preferencialSinPrecio"));

            borrador.Adicionales.Add(new AdicionalReserva_GV42
            {
                TipoAdicional = servicio,
                Tramo = tramo,
                Cantidad = cantidad,
                CostoUnitario = Math.Round(servicio.PrecioUnitario, 2)
            });
        }

        // Equipaje extra: cada pasajero tiene sus valijas en cada tramo (AsientoPasajero.EquipajeExtra). El
        // adicional del tramo es la suma de las valijas de sus pasajeros, al precio del catálogo.
        // Los infantes no despachan equipaje propio.
        private void AgregarEquipajeExtra(Reserva_GV42 borrador, int tramo)
        {
            List<AsientoPasajero_GV42> porPasajero = borrador.AsientosPorPasajero.Where(a => a.Tramo == tramo).ToList();
            if (porPasajero.Any(a => a.EquipajeExtra < 0))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.equipajeNegativo"));

            var infantes = new HashSet<string>(borrador.Pasajeros.Where(p => p.EsInfante).Select(p => p.DNI));
            if (porPasajero.Any(a => a.EquipajeExtra > 0 && infantes.Contains(a.DniPasajero)))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.equipajeInfante"));

            int total = porPasajero.Sum(a => a.EquipajeExtra);
            if (total == 0) return;

            TipoAdicional_GV42 servicio = ObtenerServicioEquipajeExtra();
            if (servicio == null || servicio.PrecioUnitario <= 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.equipajeSinPrecio"));

            int maximo = Math.Max(1, servicio.MaxPorPasajero);
            AsientoPasajero_GV42 excedido = porPasajero.FirstOrDefault(a => a.EquipajeExtra > maximo);
            if (excedido != null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.equipajeMaximo", excedido.DniPasajero, maximo));

            borrador.Adicionales.Add(new AdicionalReserva_GV42
            {
                TipoAdicional = servicio,
                Tramo = tramo,
                Cantidad = total,
                CostoUnitario = Math.Round(servicio.PrecioUnitario, 2)
            });
        }

        // Pública para que la pantalla valide al pasar del paso "Pasajeros" (antes solo se controlaba
        // que no hubiera campos vacíos y los errores recién aparecían al confirmar, después de elegir asientos).
        // GenerarReserva la vuelve a ejecutar igual: la pantalla no es la única barrera.
        // 'fechaVuelo': salida del vuelo de ida; con la fecha de nacimiento define el tipo de cada
        // pasajero (adulto, niño o infante), que queda cargado en Pasajero.Tipo.
        public void ValidarPasajerosParaReserva(List<Pasajero_GV42> pasajeros, DateTime fechaVuelo)
        {
            if (pasajeros == null || pasajeros.Count == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.minimoPasajeros"));
            if (pasajeros.Count > MAX_PASAJEROS_POR_RESERVA)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.maximoPasajeros", MAX_PASAJEROS_POR_RESERVA));

            for (int i = 0; i < pasajeros.Count; i++)
                BLLNegocioUtil_GV42.ValidarPersona(pasajeros[i], IdiomaManager_GV42.T("neg.rol.pasajeroN", i + 1));

            var repetido = pasajeros.GroupBy(p => p.DNI).FirstOrDefault(g => g.Count() > 1);
            if (repetido != null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.dniRepetido", repetido.Key));

            bool esVendedor = PatentesActuales().Contains("Reservas.Generar");
            string dniSesion = DniSesion();
            for (int i = 0; i < pasajeros.Count; i++)
            {
                Pasajero_GV42 p = pasajeros[i];
                // El titular autogestionado ya está identificado por su sesión (sus datos salen de ahí).
                if (!esVendedor && string.Equals(p.DNI, dniSesion, StringComparison.OrdinalIgnoreCase))
                    continue;
                VerificarIdentidad(p, IdiomaManager_GV42.T("neg.rol.pasajeroN", i + 1), esVendedor);
            }

            // Fecha de nacimiento y tipo de pasajero. Si la persona ya está registrada con su fecha, vale
            // la registrada (no se puede "rejuvenecer" a alguien para pagar tarifa de niño).
            for (int i = 0; i < pasajeros.Count; i++)
            {
                Pasajero_GV42 p = pasajeros[i];
                Pasajero_GV42 registrado = _dalPasajero.BuscarPorDni(p.DNI);
                if (registrado != null && registrado.FechaNacimiento.HasValue)
                    p.FechaNacimiento = registrado.FechaNacimiento;

                string rol = IdiomaManager_GV42.T("neg.rol.pasajeroN", i + 1);
                if (!p.FechaNacimiento.HasValue)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pasajero.faltaNacimiento", rol));
                DateTime nacimiento = p.FechaNacimiento.Value.Date;
                if (nacimiento > DateTime.Today || nacimiento < DateTime.Today.AddYears(-120))
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pasajero.nacimientoInvalido", rol));

                p.FechaNacimiento = nacimiento;
                p.Tipo = p.TipoEn(fechaVuelo);
            }

            // Como en las aerolíneas: tiene que viajar al menos un adulto, y cada infante va en brazos
            // de un adulto distinto.
            int adultos = pasajeros.Count(x => x.Tipo == TipoPasajero_GV42.Adulto);
            int infantes = pasajeros.Count(x => x.Tipo == TipoPasajero_GV42.Infante);
            if (adultos == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pasajero.sinAdulto", Pasajero_GV42.EDAD_MINIMA_ADULTO));
            if (infantes > adultos)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pasajero.infantesPorAdulto"));
        }

        // El tipo tiene que existir y estar activo, sin repetir. El precio sale del catálogo
        // (TipoAdicional.PrecioUnitario): el cliente autogestionado no puede cambiarlo (antes podía
        // cargar cualquier costo, incluso $0); el vendedor puede ajustarlo, pero debe ser mayor a 0.
        // Tope de un servicio adicional EN UN TRAMO: lo que permite cada pasajero
        // (TipoAdicional.MaxPorPasajero) x cantidad de pasajeros. En ida y vuelta los servicios se eligen
        // por separado para la ida y para la vuelta, cada tramo con este mismo tope.
        // Ej.: 1 pasajero -> como máximo 1 comida especial por tramo (antes se podían pedir 20).
        public int MaximoAdicional(TipoAdicional_GV42 tipo, int cantidadPasajeros)
        {
            if (tipo == null || cantidadPasajeros <= 0) return 0;
            int porPasajero = Math.Max(1, tipo.MaxPorPasajero);
            return Math.Min(MAX_CANTIDAD_ADICIONAL, porPasajero * cantidadPasajeros);
        }

        // Valida los servicios de UN tramo.
        private void ValidarAdicionales(List<AdicionalReserva_GV42> adicionales, CanalVenta_GV42 canal,
                                        int cantidadPasajeros)
        {
            if (adicionales == null) return;

            Dictionary<int, TipoAdicional_GV42> catalogo = _dalTipoAdicional.ListarActivos().ToDictionary(t => t.Id);

            foreach (AdicionalReserva_GV42 a in adicionales)
            {
                if (a.TipoAdicional == null || a.TipoAdicional.Id <= 0)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.adicional.sinTipo"));
                if (!catalogo.TryGetValue(a.TipoAdicional.Id, out TipoAdicional_GV42 tipo))
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.adicional.noDisponible",
                        BLLNegocioUtil_GV42.NombreAdicional(a.TipoNombre)));
                a.TipoAdicional = tipo;

                string nombre = BLLNegocioUtil_GV42.NombreAdicional(tipo.Nombre);
                if (a.Cantidad < 1)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.adicional.cantidadMinima", nombre));
                int maximo = MaximoAdicional(tipo, cantidadPasajeros);
                if (a.Cantidad > maximo)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.adicional.maximo",
                        cantidadPasajeros, maximo, nombre, Math.Max(1, tipo.MaxPorPasajero)));

                if (canal == CanalVenta_GV42.Autogestion)
                    a.CostoUnitario = tipo.PrecioUnitario;

                a.CostoUnitario = Math.Round(a.CostoUnitario, 2);
                if (a.CostoUnitario <= 0)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.adicional.costoCero", nombre));
                if (a.CostoUnitario > MAX_COSTO_ADICIONAL)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.adicional.costoAlto", nombre));
            }

            var repetido = adicionales.GroupBy(a => a.TipoAdicional.Id).FirstOrDefault(g => g.Count() > 1);
            if (repetido != null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.adicional.repetido",
                    BLLNegocioUtil_GV42.NombreAdicional(repetido.First().TipoNombre)));
        }

        #endregion

        #region Consultas

        // Devuelve null si el número de reserva no existe.
        public Reserva_GV42 BuscarReserva(string numeroReserva)
        {
            numeroReserva = (numeroReserva ?? string.Empty).Trim();
            if (numeroReserva.Length == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.indiqueNumero"));
            AplicarVencimientos();
            Reserva_GV42 reserva = _dalReserva.BuscarPorNumero(numeroReserva);

            // Quien solo tiene permisos "propios" no puede ver reservas de otras personas.
            var p = PatentesActuales();
            bool veAjenas = p.Contains("Reservas.Consultar") || p.Contains("Pagos.Registrar") || p.Contains("Reservas.Cancelar");
            // El titular o cualquiera de sus pasajeros puede verla (pagar y cancelar, solo el titular).
            if (reserva != null && !veAjenas && !EsDeLaSesion(reserva) && !EsPasajeroDeLaSesion(reserva))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.noEsTuya"));
            return reserva;
        }

        // "Mis reservas" del cliente autogestionado (usa el DNI de la sesión, no lo que venga de la UI).
        public List<Reserva_GV42> ListarMisReservas()
        {
            if (!PatentesActuales().Contains("Reservas.ConsultarPropia"))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.consulta.sinPermisoPropias"));
            // Incluye las reservas en las que la persona viaja como pasajero aunque las haya sacado otro.
            AplicarVencimientos();
            return _dalReserva.ListarPorPersona(DniSesion());
        }

        // Consulta del vendedor: sin texto trae las últimas reservas; con texto filtra por
        // número de reserva, DNI o apellido del cliente.
        public List<Reserva_GV42> BuscarReservas(string textoLibre)
        {
            if (!PatentesActuales().Contains("Reservas.Consultar"))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.consulta.sinPermisoTodas"));
            AplicarVencimientos();
            return _dalReserva.Buscar(textoLibre);
        }

        // ---- Paso 14: boletos para entregar al cliente (uno por pasajero) ----

        public List<Boleto_GV42> ObtenerBoletos(string numeroReserva)
        {
            Reserva_GV42 reserva = BuscarReserva(numeroReserva);
            if (reserva == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.noExiste"));
            if (reserva.Estado != EstadoReserva_GV42.Confirmada)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.boletos.noConfirmada"));

            List<Boleto_GV42> boletos = _dalBoleto.ListarPorReserva(reserva.NumeroReserva);
            string soloDni = DniSiEsSoloPasajero(reserva);
            if (soloDni != null)
                boletos = boletos.Where(b => string.Equals(b.PasajeroDni, soloDni, StringComparison.OrdinalIgnoreCase)).ToList();
            return boletos;
        }

        #endregion

        #region Pago

        // ---- Pasos 11 a 13: registrar el pago y confirmar la reserva ----

        // Para efectivo el número de transacción es opcional (se autogenera); para tarjeta y
        // transferencia es obligatorio. El importe debe coincidir con el total de la reserva.
        public Pago_GV42 RegistrarPago(string numeroReserva, MedioPago_GV42 medioPago, decimal importeAbonado, string numeroTransaccion)
        {
            return RegistrarPago(numeroReserva, medioPago, importeAbonado, numeroTransaccion, null);
        }

        // Con tarjeta (crédito o débito) se exigen los datos de la tarjeta: número válido según el
        // algoritmo de Luhn, titular, vencimiento no pasado y código de seguridad. El banco es teórico:
        // si los datos son válidos el sistema genera el código de autorización. De la tarjeta solo
        // se guarda la marca y los últimos 4 dígitos (nunca el número completo ni el código).
        public Pago_GV42 RegistrarPago(string numeroReserva, MedioPago_GV42 medioPago, decimal importeAbonado,
                                       string numeroTransaccion, DatosTarjeta_GV42 tarjeta)
        {
            string login = BLLNegocioUtil_GV42.LoginActual();

            var patentesPago = PatentesActuales();
            bool pagaCualquiera = patentesPago.Contains("Pagos.Registrar");
            if (!pagaCualquiera && !patentesPago.Contains("Pagos.RegistrarPropio"))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pago.sinPermiso"));

            Reserva_GV42 reserva = BuscarReserva(numeroReserva);
            if (reserva == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.noExiste"));
            if (!pagaCualquiera && !EsDeLaSesion(reserva))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pago.soloPropias"));
            // Venció el plazo de pago: la reserva se canceló sola y sus asientos se liberaron.
            if (reserva.VencidaSinPago ||
                (reserva.Estado == EstadoReserva_GV42.PendienteDePago && reserva.FechaVencimiento.HasValue && reserva.FechaVencimiento.Value <= DateTime.Now))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pago.vencida", reserva.NumeroReserva));
            if (reserva.Estado != EstadoReserva_GV42.PendienteDePago)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pago.reservaYaEsta",
                    reserva.NumeroReserva, reserva.EstadoTexto.ToLower()));
            if (reserva.Vuelo.FechaHoraSalida <= DateTime.Now)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pago.vueloSalio", reserva.NumeroReserva));

            // El efectivo lo cobra un vendedor en el mostrador: el cliente autogestionado no puede
            // declararse pagado en efectivo (quedaba confirmado sin haber pagado nada).
            if (!pagaCualquiera && medioPago == MedioPago_GV42.Efectivo)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pago.efectivoSoloVendedor"));

            if (Math.Round(importeAbonado, 2) != reserva.ImporteTotal)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pago.importeNoCoincide",
                    BLLNegocioUtil_GV42.Dinero(importeAbonado), BLLNegocioUtil_GV42.Dinero(reserva.ImporteTotal)));

            numeroTransaccion = (numeroTransaccion ?? string.Empty).Trim();
            if (medioPago == MedioPago_GV42.Efectivo)
            {
                // En efectivo no hay número externo: se genera uno interno.
                numeroTransaccion = "EFE-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            }
            else if (medioPago == MedioPago_GV42.Transferencia)
            {
                if (numeroTransaccion.Length == 0)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pago.faltaOperacion"));
                if (!System.Text.RegularExpressions.Regex.IsMatch(numeroTransaccion, Validaciones_GV42.REGEX_TX_TRANSFERENCIA))
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pago.operacionInvalida"));
            }
            else
            {
                numeroTransaccion = BLLNegocioUtil_GV42.AutorizarTarjeta(tarjeta, medioPago);
            }

            var pago = new Pago_GV42
            {
                IdReserva = reserva.Id,
                NumeroReserva = reserva.NumeroReserva,
                ImporteTotalAbonado = reserva.ImporteTotal,
                MedioPago = medioPago,
                NumeroTransaccion = numeroTransaccion,
                LoginVendedor = login
            };

            Pago_GV42 registrado = _dalPago.RegistrarPagoYConfirmar(pago);
            RecalcularIntegridad("Pago");
            RecalcularIntegridad("Reserva");

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Pago registrado",
                reserva.NumeroReserva + " - " + medioPago.ToString() + " - " + BLLNegocioUtil_GV42.Dinero(registrado.ImporteTotalAbonado), "Media");

            return registrado;
        }

        #endregion

        #region Cancelación

        // Penalidad de cancelar una reserva, según su estado y su tarifa:
        //  - pendiente de pago: no cobró nada, sin penalidad;
        //  - tarifa no reembolsable (Light): 100%;
        //  - tarifa con reembolso total (Top): 0%;
        //  - el resto (Plus): según la anticipación con que se cancela.
        public decimal CalcularPorcentajePenalidad(Reserva_GV42 reserva)
        {
            if (reserva == null || reserva.Estado == EstadoReserva_GV42.PendienteDePago) return 0m;
            if (reserva.Tarifa != null && reserva.Tarifa.TipoReembolso == TarifaFamilia_GV42.REEMBOLSO_NO) return PORCENTAJE_PENALIDAD_TOTAL;
            if (reserva.Tarifa != null && reserva.Tarifa.TipoReembolso == TarifaFamilia_GV42.REEMBOLSO_TOTAL) return 0m;
            return CalcularPorcentajePenalidad(reserva.Vuelo.FechaHoraSalida);
        }

        public decimal CalcularPorcentajePenalidad(DateTime fechaHoraSalida)
        {
            double horasRestantes = (fechaHoraSalida - DateTime.Now).TotalHours;
            if (horasRestantes >= HORAS_SIN_PENALIDAD) return 0m;
            if (horasRestantes >= HORAS_PENALIDAD_PARCIAL) return PORCENTAJE_PENALIDAD_PARCIAL;
            return PORCENTAJE_PENALIDAD_TOTAL;
        }

        public Reserva_GV42 CancelarReserva(string numeroReserva)
        {
            BLLNegocioUtil_GV42.LoginActual();

            // No alcanza con ocultar el botón en la pantalla: el permiso se vuelve a chequear acá.
            var patentes = PatentesActuales();
            bool cancelaCualquiera = patentes.Contains("Reservas.Cancelar");
            if (!cancelaCualquiera && !patentes.Contains("Reservas.CancelarPropia"))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cancelar.sinPermiso"));

            Reserva_GV42 reserva = BuscarReserva(numeroReserva);
            if (reserva == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.noExiste"));
            if (reserva.Estado == EstadoReserva_GV42.Cancelada)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cancelar.yaCancelada"));

            // Con la patente "propia" solo se cancelan las reservas del propio usuario.
            if (!cancelaCualquiera && !EsDeLaSesion(reserva))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cancelar.noEsTuya"));

            if (reserva.Vuelo.FechaHoraSalida <= DateTime.Now)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cancelar.vueloSalio"));
            if (_dalReserva.TieneCheckInRealizado(reserva.Id))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cancelar.conCheckIn"));

            // Una reserva pendiente de pago no cobró nada: se cancela sin penalidad. Si está paga, la
            // penalidad depende de la tarifa (no reembolsable / según anticipación / reembolso total).
            decimal porcentaje = CalcularPorcentajePenalidad(reserva);
            decimal monto = Math.Round(reserva.ImporteTotal * porcentaje, 2);

            // Reembolso: lo que pagó menos la penalidad, por el mismo medio de pago. Queda pendiente
            // hasta que un vendedor lo procese.
            Reembolso_GV42 reembolso = null;
            if (reserva.Estado == EstadoReserva_GV42.Confirmada && reserva.Pago != null)
            {
                decimal aDevolver = Math.Round(reserva.Pago.ImporteTotalAbonado - monto, 2);
                if (aDevolver > 0)
                    reembolso = new Reembolso_GV42 { IdReserva = reserva.Id, Importe = aDevolver, MedioPago = reserva.Pago.MedioPago };
            }

            Reserva_GV42 cancelada = _dalReserva.Cancelar(reserva.Id, monto, reembolso);
            RecalcularIntegridad("Reserva");

            if (reembolso != null)
                BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Reembolso generado",
                    cancelada.NumeroReserva + " - " + BLLNegocioUtil_GV42.Dinero(reembolso.Importe) + " - " + reembolso.MedioPago.ToString(), "Media");

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Reserva cancelada",
                cancelada.NumeroReserva + " - penalidad " + BLLNegocioUtil_GV42.Dinero(monto) +
                " (" + (porcentaje * 100) + "%)", "Media");

            return cancelada;
        }

        #endregion

        #region Reembolsos

        public bool PuedeProcesarReembolsos() { return PatentesActuales().Contains("Reembolsos.Procesar"); }

        // Marca como procesado el reembolso de una reserva cancelada (ya se le devolvió la plata al cliente).
        public Reembolso_GV42 ProcesarReembolso(string numeroReserva)
        {
            string login = BLLNegocioUtil_GV42.LoginActual();
            if (!PuedeProcesarReembolsos())
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reembolso.sinPermiso"));

            Reserva_GV42 reserva = BuscarReserva(numeroReserva);
            if (reserva == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.noExiste"));
            if (reserva.Reembolso == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reembolso.noTiene"));
            if (reserva.Reembolso.Estado == EstadoReembolso_GV42.Procesado || !_dalReserva.ProcesarReembolso(reserva.Id, login))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reembolso.yaProcesado"));

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Reembolso procesado",
                reserva.NumeroReserva + " - " + BLLNegocioUtil_GV42.Dinero(reserva.Reembolso.Importe), "Media");
            return _dalReserva.BuscarReembolso(reserva.Id);
        }

        #endregion

        #region Cambio de vuelo

        // Cambio de fecha / vuelo de un tramo de una reserva confirmada (como en las aerolíneas):
        // misma ruta, hasta 24 hs antes de la salida y sin check-in hecho en ese tramo. Se cobra la
        // penalidad de la tarifa más la diferencia de tarifa si el vuelo nuevo es más caro (si es
        // más barato no se devuelve). Se vuelven a elegir los asientos.

        public bool PuedeCambiarVuelo()
        {
            var p = PatentesActuales();
            return p.Contains("Reservas.Cambiar") || p.Contains("Reservas.CambiarPropia");
        }

        // Motivo por el que NO se puede cambiar el vuelo de ese tramo (null si se puede). La pantalla
        // lo usa para habilitar el botón y explicar por qué no.
        public string MotivoNoSePuedeCambiar(Reserva_GV42 reserva, int tramo)
        {
            try { ValidarPuedeCambiar(reserva, tramo); return null; }
            catch (NegocioException_GV42 ex) { return ex.Message; }
        }

        private void ValidarPuedeCambiar(Reserva_GV42 reserva, int tramo)
        {
            var patentes = PatentesActuales();
            bool cambiaCualquiera = patentes.Contains("Reservas.Cambiar");
            if (!cambiaCualquiera && !patentes.Contains("Reservas.CambiarPropia"))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cambio.sinPermiso"));
            if (reserva == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.noExiste"));
            if (!cambiaCualquiera && !EsDeLaSesion(reserva))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cambio.noEsTuya"));
            if (reserva.Estado != EstadoReserva_GV42.Confirmada)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cambio.noConfirmada"));

            VueloClase_GV42 actual = reserva.VueloClaseDeTramo(tramo);
            if (actual == null || actual.Vuelo == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cambio.sinTramo"));
            if (actual.Vuelo.FechaHoraSalida <= DateTime.Now.AddHours(HORAS_LIMITE_CAMBIO))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cambio.fueraDePlazo", HORAS_LIMITE_CAMBIO));
            if (_dalReserva.TieneCheckInRealizado(reserva.Id, tramo))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cambio.conCheckIn"));
        }

        // El vuelo nuevo tiene que ser coherente con el otro tramo: la ida llega antes de que salga la
        // vuelta y la vuelta sale después de que llega la ida.
        private static bool EsCoherenteConOtroTramo(Reserva_GV42 reserva, int tramo, Vuelo_GV42 nuevo)
        {
            if (!reserva.TieneVuelta) return true;
            return tramo == Reserva_GV42.TRAMO_IDA
                ? nuevo.FechaHoraLlegada < reserva.VueloClaseVuelta.Vuelo.FechaHoraSalida
                : nuevo.FechaHoraSalida > reserva.VueloClase.Vuelo.FechaHoraLlegada;
        }

        // Vuelos a los que se puede cambiar el tramo en la fecha indicada: misma ruta, con lugar para
        // los pasajeros de la reserva y coherentes con el otro tramo. No incluye el vuelo actual.
        public List<VueloClase_GV42> BuscarVuelosParaCambio(string numeroReserva, int tramo, DateTime fecha, ClaseVuelo_GV42? clase)
        {
            Reserva_GV42 reserva = BuscarReserva(numeroReserva);
            ValidarPuedeCambiar(reserva, tramo);
            if (fecha.Date < DateTime.Today)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.salidaAnteriorHoy"));

            VueloClase_GV42 actual = reserva.VueloClaseDeTramo(tramo);
            var criterio = new CriterioBusquedaVuelo_GV42
            {
                IdOrigen = actual.Vuelo.Origen.Id,
                IdDestino = actual.Vuelo.Destino.Id,
                FechaSalida = fecha.Date,
                CantidadPasajeros = Math.Max(1, reserva.CantidadAsientos),
                TipoViaje = TipoViaje_GV42.Ida,
                Clase = clase
            };
            return _dalVuelo.BuscarDisponibles(criterio)
                .Where(v => !(v.Vuelo.Id == actual.Vuelo.Id && v.Clase == actual.Clase))
                .Where(v => EsCoherenteConOtroTramo(reserva, tramo, v.Vuelo))
                .ToList();
        }

        // Cuánto cuesta el cambio: penalidad de la tarifa (porcentaje sobre la tarifa del tramo que se
        // cambia) + diferencia de tarifa con impuestos si el vuelo nuevo es más caro.
        public CotizacionCambio_GV42 CotizarCambio(string numeroReserva, int tramo, int idVueloNuevo, ClaseVuelo_GV42 claseNueva)
        {
            Reserva_GV42 reserva = BuscarReserva(numeroReserva);
            ValidarPuedeCambiar(reserva, tramo);
            VueloClase_GV42 nuevo = _dalVuelo.BuscarVueloClase(idVueloNuevo, claseNueva);
            if (nuevo == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.claseNoOfrecida"));
            return Cotizar(reserva, tramo, nuevo);
        }

        private static CotizacionCambio_GV42 Cotizar(Reserva_GV42 reserva, int tramo, VueloClase_GV42 nuevo)
        {
            VueloClase_GV42 actual = reserva.VueloClaseDeTramo(tramo);
            decimal tarifaActual = CalcularTarifaTramo(reserva.Tarifa, actual, reserva.Pasajeros);
            decimal tarifaNueva = CalcularTarifaTramo(reserva.Tarifa, nuevo, reserva.Pasajeros);
            decimal porcentaje = reserva.Tarifa != null ? reserva.Tarifa.PorcentajePenalidadCambio : 0m;
            decimal diferencia = Math.Max(0m, tarifaNueva - tarifaActual);

            return new CotizacionCambio_GV42
            {
                TarifaActual = tarifaActual,
                TarifaNueva = tarifaNueva,
                PorcentajePenalidad = porcentaje,
                Penalidad = Math.Round(tarifaActual * porcentaje / 100m, 2),
                DiferenciaTarifa = diferencia,
                ImpuestosDiferencia = Math.Round(diferencia * TASA_IMPUESTOS, 2)
            };
        }

        // ¿En el vuelo nuevo hay que elegir asientos? Sí, salvo que la tarifa cobre la elección (Light) y
        // los pasajeros no la hayan pagado en ese tramo: en ese caso se asignan en el check-in.
        public bool CambioRequiereAsientos(Reserva_GV42 reserva, int tramo)
        {
            if (reserva == null) return false;
            if (reserva.Tarifa == null || !reserva.Tarifa.ElegirAsientoEsPago) return true;
            return reserva.AsientosPorPasajero.Any(a => a.Tramo == tramo && a.Asiento != null);
        }

        // Hace el cambio. 'asientos': DNI del pasajero y asiento elegido en el vuelo nuevo (uno por cada
        // pasajero que ocupa asiento, si CambioRequiereAsientos). Si hay algo que cobrar se indica el
        // medio de pago (con tarjeta, sus datos; con transferencia, el número de operación).
        public CotizacionCambio_GV42 CambiarVuelo(string numeroReserva, int tramo, int idVueloNuevo, ClaseVuelo_GV42 claseNueva,
                                                  List<AsientoPasajero_GV42> asientos, MedioPago_GV42? medioPago,
                                                  string numeroOperacion, DatosTarjeta_GV42 tarjeta)
        {
            string login = BLLNegocioUtil_GV42.LoginActual();
            Reserva_GV42 reserva = BuscarReserva(numeroReserva);
            ValidarPuedeCambiar(reserva, tramo);

            VueloClase_GV42 actual = reserva.VueloClaseDeTramo(tramo);
            VueloClase_GV42 nuevo = _dalVuelo.BuscarVueloClase(idVueloNuevo, claseNueva);
            if (nuevo == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.claseNoOfrecida"));
            if (nuevo.Vuelo.Id == actual.Vuelo.Id && nuevo.Clase == actual.Clase)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cambio.mismoVuelo"));
            if (nuevo.Vuelo.Origen.Id != actual.Vuelo.Origen.Id || nuevo.Vuelo.Destino.Id != actual.Vuelo.Destino.Id)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cambio.otraRuta"));
            if (nuevo.Vuelo.FechaHoraSalida <= DateTime.Now)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.vueloSalio"));
            if (!EsCoherenteConOtroTramo(reserva, tramo, nuevo.Vuelo))
                throw new NegocioException_GV42(IdiomaManager_GV42.T(tramo == Reserva_GV42.TRAMO_IDA ? "neg.cambio.idaDespuesDeVuelta" : "neg.reserva.vueltaAntesDeLlegar"));
            if (nuevo.AsientosDisponibles < reserva.CantidadAsientos)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.quedanAsientos", nuevo.AsientosDisponibles));

            // Asientos del vuelo nuevo.
            asientos = (asientos ?? new List<AsientoPasajero_GV42>()).Where(a => a != null && a.Asiento != null).ToList();
            List<Pasajero_GV42> conLugar = reserva.Pasajeros.Where(p => !p.EsInfante).ToList();
            var nuevos = new List<AsientoPasajero_GV42>();
            if (CambioRequiereAsientos(reserva, tramo))
            {
                foreach (Pasajero_GV42 p in conLugar)
                {
                    AsientoPasajero_GV42 elegido = asientos.FirstOrDefault(a => a.DniPasajero == p.DNI);
                    if (elegido == null)
                        throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.asientoPorPasajero"));
                    Asiento_GV42 asiento = ValidarAsientoDeVuelo(elegido.Asiento, nuevo);
                    // Las butacas preferenciales llevan recargo: en un cambio solo se ofrecen si la tarifa las incluye.
                    if (asiento.EsPreferencial && (reserva.Tarifa == null || !reserva.Tarifa.IncluyePreferencial))
                        throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cambio.preferencialNoIncluido", asiento.NumeroAsiento));
                    nuevos.Add(new AsientoPasajero_GV42(p.DNI, asiento) { Tramo = tramo });
                }
                if (nuevos.GroupBy(a => a.Asiento.Id).Any(g => g.Count() > 1))
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.asientoRepetido"));
            }

            // Cobro de la penalidad y la diferencia (si hay algo que cobrar).
            CotizacionCambio_GV42 cotizacion = Cotizar(reserva, tramo, nuevo);
            string numeroTransaccion = null;
            if (cotizacion.Total > 0)
            {
                if (!medioPago.HasValue)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cambio.faltaMedioPago"));
                bool esVendedor = PatentesActuales().Contains("Reservas.Cambiar");
                numeroOperacion = (numeroOperacion ?? string.Empty).Trim();
                if (medioPago.Value == MedioPago_GV42.Efectivo)
                {
                    // El efectivo lo cobra un vendedor en el mostrador.
                    if (!esVendedor)
                        throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pago.efectivoSoloVendedor"));
                    numeroTransaccion = "EFE-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                }
                else if (medioPago.Value == MedioPago_GV42.Transferencia)
                {
                    if (numeroOperacion.Length == 0)
                        throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pago.faltaOperacion"));
                    if (!System.Text.RegularExpressions.Regex.IsMatch(numeroOperacion, Validaciones_GV42.REGEX_TX_TRANSFERENCIA))
                        throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pago.operacionInvalida"));
                    numeroTransaccion = numeroOperacion;
                }
                else
                {
                    numeroTransaccion = BLLNegocioUtil_GV42.AutorizarTarjeta(tarjeta, medioPago.Value);
                }
            }
            else
            {
                medioPago = null;
            }

            _dalReserva.CambiarVuelo(reserva, tramo, nuevo, nuevos, cotizacion, medioPago, numeroTransaccion, login);
            RecalcularIntegridad("Reserva");

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Vuelo cambiado",
                reserva.NumeroReserva + " - " + (tramo == Reserva_GV42.TRAMO_VUELTA ? "vuelta" : "ida") + ": " +
                actual.CodigoVuelo + " -> " + nuevo.CodigoVuelo + " - cobrado " + BLLNegocioUtil_GV42.Dinero(cotizacion.Total), "Media");

            return cotizacion;
        }

        #endregion

        #region Métodos privados

        // Vencimiento exacto de las reservas impagas: antes de cualquier operación que mire
        // disponibilidad o reservas, las que ya pasaron su plazo de pago se cancelan y liberan sus
        // asientos. Así vencen a la hora que les corresponde, sin depender de un proceso programado
        // (LocalDB no tiene SQL Server Agent) ni de que alguien tenga el sistema abierto.
        private void AplicarVencimientos()
        {
            int vencidas;
            try { vencidas = _dalReserva.LiberarVencidas(); }
            catch { return; }   // no impide la operación principal; el pago igual controla la fecha de vencimiento
            if (vencidas <= 0) return;

            RecalcularIntegridad("Reserva");
            try
            {
                BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Reservas vencidas",
                    vencidas + " reserva(s) sin pagar vencieron y liberaron sus asientos", "Baja");
            }
            catch { }
        }

        // Recalcula el dígito verificador de una tabla de negocio protegida. Nunca interrumpe la
        // operación si falla (igual criterio que BLLUsuario_GV42 con la tabla Usuario).
        private void RecalcularIntegridad(string tabla)
        {
            if (BLLIntegridad_GV42.IntegridadConocidamenteRota) return;
            try { _bllIntegridad.RecalcularTabla(tabla); } catch { }
        }

        #endregion
    }
}
