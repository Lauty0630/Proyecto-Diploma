using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    // RFN 1 - Reserva de vuelo (atención presencial: Vendedor + Cliente).
    public class BLLReserva_GV42
    {
        // Tasa de impuestos aplicada sobre (importe base + adicionales). Ajustar según el enunciado.
        public const decimal TASA_IMPUESTOS = 0.21m;

        private readonly DALAeropuerto_GV42 _dalAeropuerto;
        private readonly DALTipoAdicional_GV42 _dalTipoAdicional;
        private readonly DALVuelo_GV42 _dalVuelo;
        private readonly DALPasajero_GV42 _dalPasajero;
        private readonly DALReserva_GV42 _dalReserva;
        private readonly DALPago_GV42 _dalPago;
        private readonly DALBoleto_GV42 _dalBoleto;
        private readonly DALAsiento_GV42 _dalAsiento;
        private readonly BLLIntegridad_GV42 _bllIntegridad;

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
            _bllIntegridad = new BLLIntegridad_GV42();
        }

        // Recalcula el dígito verificador de una tabla de negocio protegida. Nunca interrumpe la
        // operación si falla (igual criterio que BLLUsuario_GV42 con la tabla Usuario).
        private void RecalcularIntegridad(string tabla)
        {
            if (BLLIntegridad_GV42.IntegridadConocidamenteRota) return;
            try { _bllIntegridad.RecalcularTabla(tabla); } catch { }
        }

        // ---- Catálogos para armar los combos de la pantalla ----

        public List<Aeropuerto_GV42> ListarAeropuertos()
        {
            return _dalAeropuerto.ListarTodos();
        }

        public List<TipoAdicional_GV42> ListarTiposAdicional()
        {
            return _dalTipoAdicional.ListarActivos();
        }

        // ---- Pasos 1 a 4: buscar vuelos disponibles ----

        public List<VueloClase_GV42> BuscarVuelosDisponibles(CriterioBusquedaVuelo_GV42 criterio)
        {
            ValidarCriterio(criterio);
            return _dalVuelo.BuscarDisponibles(criterio);
        }

        private void ValidarCriterio(CriterioBusquedaVuelo_GV42 c)
        {
            if (c == null)
                throw new NegocioException_GV42("Faltan los datos de búsqueda del vuelo.");
            if (c.IdOrigen <= 0)
                throw new NegocioException_GV42("Debe seleccionar el origen.");
            if (c.IdDestino <= 0)
                throw new NegocioException_GV42("Debe seleccionar el destino.");
            if (c.IdOrigen == c.IdDestino)
                throw new NegocioException_GV42("El origen y el destino no pueden ser el mismo.");
            if (c.FechaSalida.Date < DateTime.Today)
                throw new NegocioException_GV42("La fecha de salida no puede ser anterior a hoy.");
            if (c.CantidadPasajeros < 1)
                throw new NegocioException_GV42("La cantidad de pasajeros debe ser al menos 1.");

            if (c.TipoViaje == TipoViaje_GV42.IdaYVuelta)
            {
                if (!c.FechaRegreso.HasValue)
                    throw new NegocioException_GV42("Para un viaje de ida y vuelta debe indicar la fecha de regreso.");
                if (c.FechaRegreso.Value.Date < c.FechaSalida.Date)
                    throw new NegocioException_GV42("La fecha de regreso no puede ser anterior a la fecha de salida.");
            }
        }

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
                throw new NegocioException_GV42("No tenés permiso para buscar personas por DNI.");
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
        // exponer datos de terceros a partir de un DNI).
        private void VerificarIdentidad(Persona_GV42 p, string rol, bool mostrarRegistrado)
        {
            Pasajero_GV42 registrado = PersonaRegistrada(p.DNI, out bool _);
            if (registrado == null) return;

            bool coincide = Servicios.Validaciones_GV42.MismoTexto(p.Nombre, registrado.Nombre) &&
                            Servicios.Validaciones_GV42.MismoTexto(p.Apellido, registrado.Apellido);
            if (coincide) return;

            if (mostrarRegistrado)
                throw new NegocioException_GV42(rol + ": el DNI " + p.DNI + " ya está registrado a nombre de " +
                    registrado.Nombre + " " + registrado.Apellido + ". Verificá el DNI o usá los datos registrados.");

            throw new NegocioException_GV42(rol + ": el DNI " + p.DNI + " ya está registrado en el sistema con otro " +
                "nombre y apellido. Verificá que el DNI y los datos sean correctos.");
        }

        public void RegistrarPasajero(Pasajero_GV42 pasajero)
        {
            BLLNegocioUtil_GV42.ValidarPersona(pasajero, "Cliente");

            if (_dalPasajero.ExisteDni(pasajero.DNI))
                throw new NegocioException_GV42("Ya existe una persona registrada con el DNI " + pasajero.DNI + ".");

            // Si tiene cuenta de usuario, el nombre y apellido deben ser los de esa cuenta.
            VerificarIdentidad(pasajero, "Cliente", true);

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
            BLLNegocioUtil_GV42.ValidarPersona(cliente, "Cliente");

            if (contrasenaPlana != confirmarContrasena)
                throw new NegocioException_GV42("Las contraseñas no coinciden.");

            // Alguien que ya viajó (lo cargó un vendedor) puede crear su cuenta: se reutiliza su fila de
            // Pasajero siempre que el nombre y apellido coincidan. Si ya tiene cuenta, lo rechaza el alta
            // del usuario (DNI duplicado en Usuario).
            VerificarIdentidad(cliente, "Cliente", false);
            bool yaEraPasajero = _dalPasajero.ExisteDni(cliente.DNI);

            // Si ya viajó (lo cargó un vendedor), además del nombre tiene que coincidir el email que
            // dejó registrado: si no, cualquiera que conozca DNI, nombre y apellido podría crear la
            // cuenta de otra persona y ver o cancelar sus reservas.
            if (yaEraPasajero)
            {
                Pasajero_GV42 registrado = _dalPasajero.BuscarPorDni(cliente.DNI);
                if (registrado != null && !string.IsNullOrWhiteSpace(registrado.Email) &&
                    !string.Equals(registrado.Email.Trim(), (cliente.Email ?? "").Trim(), StringComparison.OrdinalIgnoreCase))
                    throw new NegocioException_GV42("El DNI " + cliente.DNI + " ya tiene reservas a su nombre. Para crear la cuenta " +
                        "usá el mismo email que dejaste al reservar (o pedile al vendedor que lo actualice).");
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

        // ---- Pasos 8 a 10: generar la reserva en estado "Pendiente de Pago" ----

        // 'borrador' debe traer: Cliente, VueloClase (con Vuelo.Id y Clase), TipoViaje, FechaRegreso (si es
        // ida y vuelta), Pasajeros y Adicionales (opcional). Importes, estado y vendedor los completa esta clase.
        // Asientos libres de una clase de un vuelo, para pintar la grilla de selección estilo cine.
        public List<AsientoDisponibilidad_GV42> ObtenerMapaAsientos(int idVuelo, ClaseVuelo_GV42 clase)
        {
            if (_dalVuelo.BuscarVueloClase(idVuelo, clase) == null)
                throw new NegocioException_GV42("El vuelo no ofrece la clase seleccionada.");
            return _dalAsiento.ListarMapa(idVuelo, clase);
        }

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

        // Datos del usuario logueado como pasajero titular: nombre, apellido y email salen de la
        // sesión; el teléfono, si ya reservó antes, de su fila en Pasajero (Usuario no lo guarda).
        public Pasajero_GV42 ObtenerTitularDeSesion()
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            if (actual == null)
                throw new NegocioException_GV42("No hay una sesión activa.");

            string dni = (actual.DNI ?? string.Empty).Trim();
            Pasajero_GV42 guardado = _dalPasajero.BuscarPorDni(dni);
            return new Pasajero_GV42(dni, actual.Nombre, actual.Apellido, actual.Email,
                                     guardado != null ? guardado.Telefono : string.Empty);
        }

        public Reserva_GV42 GenerarReserva(Reserva_GV42 borrador)
        {
            if (borrador == null)
                throw new NegocioException_GV42("Faltan los datos de la reserva.");
            if (borrador.VueloClase == null || borrador.VueloClase.Vuelo == null || borrador.VueloClase.Vuelo.Id <= 0)
                throw new NegocioException_GV42("Debe seleccionar el vuelo y la clase.");

            string login = BLLNegocioUtil_GV42.LoginActual();
            CanalVenta_GV42 canal = CanalSegunSesion();
            borrador.CanalVenta = canal;

            var patentes = PatentesActuales();
            if (!patentes.Contains("Reservas.Generar") && !patentes.Contains("Reservas.GenerarPropia"))
                throw new NegocioException_GV42("No tenés permiso para generar reservas.");

            if (canal == CanalVenta_GV42.Autogestion)
            {
                // El cliente reserva para sí mismo: el cliente de la reserva sale siempre de su sesión,
                // nunca de lo que venga (o no) de la pantalla, para que nadie reserve "a nombre de" otro DNI.
                Pasajero_GV42 titular = ObtenerTitularDeSesion();
                if (string.IsNullOrWhiteSpace(titular.Telefono))
                {
                    // Primera reserva: el único dato que la sesión no tiene es el teléfono, que se pide en pantalla.
                    Pasajero_GV42 enPantalla = (borrador.Pasajeros ?? new List<Pasajero_GV42>())
                        .FirstOrDefault(x => x != null && string.Equals((x.DNI ?? string.Empty).Trim(), titular.DNI, StringComparison.OrdinalIgnoreCase));
                    if (enPantalla != null) titular.Telefono = enPantalla.Telefono;
                }
                BLLNegocioUtil_GV42.ValidarPersona(titular, "Pasajero");
                if (!_dalPasajero.ExisteDni(titular.DNI))
                    _dalPasajero.Insertar(titular);
                borrador.Cliente = titular;
            }
            else
            {
                if (borrador.Cliente == null || string.IsNullOrWhiteSpace(borrador.Cliente.DNI))
                    throw new NegocioException_GV42("Debe indicar el cliente de la reserva.");
                if (!_dalPasajero.ExisteDni(borrador.Cliente.DNI.Trim()))
                    throw new NegocioException_GV42("El cliente no está registrado. Regístrelo antes de generar la reserva.");
            }

            ValidarPasajerosParaReserva(borrador.Pasajeros);
            ValidarAdicionales(borrador.Adicionales, canal);

            // El precio y la disponibilidad se toman siempre de la base, no de lo que traiga la pantalla.
            VueloClase_GV42 vc = _dalVuelo.BuscarVueloClase(borrador.VueloClase.Vuelo.Id, borrador.VueloClase.Clase);
            if (vc == null)
                throw new NegocioException_GV42("El vuelo no ofrece la clase seleccionada.");
            if (vc.Vuelo.FechaHoraSalida <= DateTime.Now)
                throw new NegocioException_GV42("El vuelo seleccionado ya salió.");
            if (vc.AsientosDisponibles < borrador.CantidadPasajeros)
                throw new NegocioException_GV42("Solo quedan " + vc.AsientosDisponibles + " asiento(s) disponible(s) en esa clase.");

            ValidarAsientos(borrador, vc);

            if (borrador.TipoViaje == TipoViaje_GV42.IdaYVuelta)
            {
                if (!borrador.FechaRegreso.HasValue)
                    throw new NegocioException_GV42("Para un viaje de ida y vuelta debe indicar la fecha de regreso.");
                if (borrador.FechaRegreso.Value.Date < vc.Vuelo.FechaHoraSalida.Date)
                    throw new NegocioException_GV42("La fecha de regreso no puede ser anterior a la fecha de salida.");
            }
            else
            {
                borrador.FechaRegreso = null;
            }

            borrador.VueloClase = vc;
            borrador.LoginVendedor = login;
            borrador.Estado = EstadoReserva_GV42.PendienteDePago;

            borrador.ImporteBase = Math.Round(vc.PrecioBase * borrador.CantidadPasajeros, 2);
            borrador.SubtotalAdicionales = Math.Round(borrador.Adicionales.Sum(a => a.Subtotal), 2);
            borrador.Impuestos = Math.Round((borrador.ImporteBase + borrador.SubtotalAdicionales) * TASA_IMPUESTOS, 2);
            borrador.ImporteTotal = borrador.ImporteBase + borrador.SubtotalAdicionales + borrador.Impuestos;

            Reserva_GV42 creada = _dalReserva.Crear(borrador);
            RecalcularIntegridad("Reserva");

            string asientosTexto = string.Join(", ", creada.AsientosPorPasajero.Select(a => a.Asiento.NumeroAsiento));
            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Reserva generada",
                creada.NumeroReserva + " - vuelo " + vc.CodigoVuelo + " - canal " + canal.Texto() +
                " - total " + BLLNegocioUtil_GV42.Dinero(creada.ImporteTotal), "Media");

            if (asientosTexto.Length > 0)
                BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Asiento seleccionado",
                    creada.NumeroReserva + ": " + asientosTexto, "Baja");

            return creada;
        }

        // Selección de asiento estilo cine: obligatoria, un asiento por pasajero, de la clase reservada,
        // sin repetir entre sí. La disponibilidad final la garantiza el índice único de la base
        // (dos personas no pueden quedarse con el mismo asiento aunque reserven al mismo tiempo).
        private void ValidarAsientos(Reserva_GV42 borrador, VueloClase_GV42 vc)
        {
            List<AsientoPasajero_GV42> asientos = borrador.AsientosPorPasajero ?? new List<AsientoPasajero_GV42>();

            if (asientos.Count != borrador.Pasajeros.Count)
                throw new NegocioException_GV42("Debe elegir un asiento para cada pasajero.");

            var dnisPasajeros = new HashSet<string>(borrador.Pasajeros.Select(p => p.DNI));
            foreach (AsientoPasajero_GV42 ap in asientos)
            {
                if (ap.Asiento == null || ap.Asiento.Id <= 0)
                    throw new NegocioException_GV42("Falta el asiento de uno de los pasajeros.");
                // Se confirma contra la base que el asiento exista y sea de este vuelo y de esta clase
                // (no se confía en los datos que trae la pantalla).
                Asiento_GV42 enBase = _dalAsiento.BuscarPorId(ap.Asiento.Id);
                if (enBase == null || enBase.IdVuelo != vc.Vuelo.Id)
                    throw new NegocioException_GV42("Uno de los asientos elegidos no pertenece al vuelo seleccionado.");
                ap.Asiento = enBase;
                if (!dnisPasajeros.Contains(ap.DniPasajero))
                    throw new NegocioException_GV42("El asiento " + ap.Asiento.NumeroAsiento + " no corresponde a ningún pasajero de esta reserva.");
                if (ap.Asiento.Clase != vc.Clase)
                    throw new NegocioException_GV42("El asiento " + ap.Asiento.NumeroAsiento + " no es de la clase reservada.");
                if (_dalAsiento.EstaReservado(ap.Asiento.Id))
                    throw new NegocioException_GV42("El asiento " + ap.Asiento.NumeroAsiento + " ya fue elegido por otro pasajero.");
            }

            var repetido = asientos.GroupBy(a => a.Asiento.Id).FirstOrDefault(g => g.Count() > 1);
            if (repetido != null)
                throw new NegocioException_GV42("No se puede asignar el mismo asiento a más de un pasajero.");

            var dniRepetido = asientos.GroupBy(a => a.DniPasajero).FirstOrDefault(g => g.Count() > 1);
            if (dniRepetido != null)
                throw new NegocioException_GV42("El pasajero con DNI " + dniRepetido.Key + " tiene más de un asiento asignado.");
        }

        // Pública para que la pantalla valide al pasar del paso "Pasajeros" (antes solo se controlaba
        // que no hubiera campos vacíos y los errores recién aparecían al confirmar, después de elegir asientos).
        // GenerarReserva la vuelve a ejecutar igual: la pantalla no es la única barrera.
        public void ValidarPasajerosParaReserva(List<Pasajero_GV42> pasajeros)
        {
            if (pasajeros == null || pasajeros.Count == 0)
                throw new NegocioException_GV42("Debe registrar al menos un pasajero.");
            if (pasajeros.Count > MAX_PASAJEROS_POR_RESERVA)
                throw new NegocioException_GV42("Una reserva puede tener como máximo " + MAX_PASAJEROS_POR_RESERVA + " pasajeros.");

            for (int i = 0; i < pasajeros.Count; i++)
                BLLNegocioUtil_GV42.ValidarPersona(pasajeros[i], "Pasajero " + (i + 1));

            var repetido = pasajeros.GroupBy(p => p.DNI).FirstOrDefault(g => g.Count() > 1);
            if (repetido != null)
                throw new NegocioException_GV42("El DNI " + repetido.Key + " está cargado en más de un pasajero.");

            bool esVendedor = PatentesActuales().Contains("Reservas.Generar");
            string dniSesion = DniSesion();
            for (int i = 0; i < pasajeros.Count; i++)
            {
                Pasajero_GV42 p = pasajeros[i];
                // El titular autogestionado ya está identificado por su sesión (sus datos salen de ahí).
                if (!esVendedor && string.Equals(p.DNI, dniSesion, StringComparison.OrdinalIgnoreCase))
                    continue;
                VerificarIdentidad(p, "Pasajero " + (i + 1), esVendedor);
            }
        }

        public const int MAX_PASAJEROS_POR_RESERVA = 9;

        public const int MAX_CANTIDAD_ADICIONAL = 20;
        public const decimal MAX_COSTO_ADICIONAL = 999999m;

        // El tipo tiene que existir y estar activo, sin repetir. El precio sale del catálogo
        // (TipoAdicional.PrecioUnitario): el cliente autogestionado no puede cambiarlo (antes podía
        // cargar cualquier costo, incluso $0); el vendedor puede ajustarlo, pero debe ser mayor a 0.
        private void ValidarAdicionales(List<AdicionalReserva_GV42> adicionales, CanalVenta_GV42 canal)
        {
            if (adicionales == null) return;

            Dictionary<int, TipoAdicional_GV42> catalogo = _dalTipoAdicional.ListarActivos().ToDictionary(t => t.Id);

            foreach (AdicionalReserva_GV42 a in adicionales)
            {
                if (a.TipoAdicional == null || a.TipoAdicional.Id <= 0)
                    throw new NegocioException_GV42("Cada servicio adicional debe tener un tipo.");
                if (!catalogo.TryGetValue(a.TipoAdicional.Id, out TipoAdicional_GV42 tipo))
                    throw new NegocioException_GV42("El servicio adicional '" + a.TipoNombre + "' no existe o ya no está disponible.");
                a.TipoAdicional = tipo;

                if (a.Cantidad < 1 || a.Cantidad > MAX_CANTIDAD_ADICIONAL)
                    throw new NegocioException_GV42("La cantidad de '" + tipo.Nombre + "' debe estar entre 1 y " + MAX_CANTIDAD_ADICIONAL + ".");

                if (canal == CanalVenta_GV42.Autogestion)
                    a.CostoUnitario = tipo.PrecioUnitario;

                a.CostoUnitario = Math.Round(a.CostoUnitario, 2);
                if (a.CostoUnitario <= 0)
                    throw new NegocioException_GV42("El costo unitario de '" + tipo.Nombre + "' debe ser mayor a $ 0.");
                if (a.CostoUnitario > MAX_COSTO_ADICIONAL)
                    throw new NegocioException_GV42("El costo unitario de '" + tipo.Nombre + "' es demasiado alto.");
            }

            var repetido = adicionales.GroupBy(a => a.TipoAdicional.Id).FirstOrDefault(g => g.Count() > 1);
            if (repetido != null)
                throw new NegocioException_GV42("El servicio '" + repetido.First().TipoNombre + "' está cargado más de una vez.");
        }

        // Devuelve null si el número de reserva no existe.
        public Reserva_GV42 BuscarReserva(string numeroReserva)
        {
            numeroReserva = (numeroReserva ?? string.Empty).Trim();
            if (numeroReserva.Length == 0)
                throw new NegocioException_GV42("Debe indicar el número de reserva.");
            Reserva_GV42 reserva = _dalReserva.BuscarPorNumero(numeroReserva);

            // Quien solo tiene permisos "propios" no puede ver reservas de otras personas.
            var p = PatentesActuales();
            bool veAjenas = p.Contains("Reservas.Consultar") || p.Contains("Pagos.Registrar") || p.Contains("Reservas.Cancelar");
            if (reserva != null && !veAjenas && !EsDeLaSesion(reserva))
                throw new NegocioException_GV42("La reserva indicada no es tuya.");
            return reserva;
        }

        // ---- Pasos 11 a 13: registrar el pago y confirmar la reserva ----

        // Para efectivo el número de transacción es opcional (se autogenera); para tarjeta y
        // transferencia es obligatorio. El importe debe coincidir con el total de la reserva.
        public Pago_GV42 RegistrarPago(string numeroReserva, MedioPago_GV42 medioPago, decimal importeAbonado, string numeroTransaccion)
        {
            string login = BLLNegocioUtil_GV42.LoginActual();

            var patentesPago = PatentesActuales();
            bool pagaCualquiera = patentesPago.Contains("Pagos.Registrar");
            if (!pagaCualquiera && !patentesPago.Contains("Pagos.RegistrarPropio"))
                throw new NegocioException_GV42("No tenés permiso para registrar pagos.");

            Reserva_GV42 reserva = BuscarReserva(numeroReserva);
            if (reserva == null)
                throw new NegocioException_GV42("No existe una reserva con el número indicado.");
            if (!pagaCualquiera && !EsDeLaSesion(reserva))
                throw new NegocioException_GV42("Solo podés pagar tus propias reservas.");
            if (reserva.Estado != EstadoReserva_GV42.PendienteDePago)
                throw new NegocioException_GV42("La reserva " + reserva.NumeroReserva + " ya está " + reserva.EstadoTexto.ToLower() + ".");
            if (reserva.Vuelo.FechaHoraSalida <= DateTime.Now)
                throw new NegocioException_GV42("El vuelo de la reserva " + reserva.NumeroReserva + " ya salió: no se puede registrar el pago.");

            // El efectivo lo cobra un vendedor en el mostrador: el cliente autogestionado no puede
            // declararse pagado en efectivo (quedaba confirmado sin haber pagado nada).
            if (!pagaCualquiera && medioPago == MedioPago_GV42.Efectivo)
                throw new NegocioException_GV42("El pago en efectivo solo lo puede registrar un vendedor. Elegí tarjeta o transferencia.");

            if (Math.Round(importeAbonado, 2) != reserva.ImporteTotal)
                throw new NegocioException_GV42("El importe abonado (" + BLLNegocioUtil_GV42.Dinero(importeAbonado) +
                    ") debe coincidir con el total de la reserva (" + BLLNegocioUtil_GV42.Dinero(reserva.ImporteTotal) + ").");

            numeroTransaccion = (numeroTransaccion ?? string.Empty).Trim();
            if (medioPago == MedioPago_GV42.Efectivo)
            {
                // En efectivo no hay número externo: se genera uno interno.
                numeroTransaccion = "EFE-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            }
            else if (numeroTransaccion.Length == 0)
            {
                throw new NegocioException_GV42("Debe indicar el número de transacción del pago.");
            }
            else if (medioPago == MedioPago_GV42.Transferencia)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(numeroTransaccion, Validaciones_GV42.REGEX_TX_TRANSFERENCIA))
                    throw new NegocioException_GV42("El número de operación de la transferencia debe tener entre 6 y 40 letras, números o guiones.");
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(numeroTransaccion, Validaciones_GV42.REGEX_TX_TARJETA))
            {
                throw new NegocioException_GV42("El código de autorización de la tarjeta debe tener entre 6 y 20 dígitos.");
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
                reserva.NumeroReserva + " - " + medioPago.Texto() + " - " + BLLNegocioUtil_GV42.Dinero(registrado.ImporteTotalAbonado), "Media");

            return registrado;
        }

        // ---- Consultar reservas ----

        // "Mis reservas" del cliente autogestionado (usa el DNI de la sesión, no lo que venga de la UI).
        public List<Reserva_GV42> ListarMisReservas()
        {
            if (!PatentesActuales().Contains("Reservas.ConsultarPropia"))
                throw new NegocioException_GV42("No tenés permiso para consultar tus reservas.");
            return _dalReserva.ListarPorCliente(DniSesion());
        }

        // Consulta del vendedor: sin texto trae las últimas reservas; con texto filtra por
        // número de reserva, DNI o apellido del cliente.
        public List<Reserva_GV42> BuscarReservas(string textoLibre)
        {
            if (!PatentesActuales().Contains("Reservas.Consultar"))
                throw new NegocioException_GV42("No tenés permiso para consultar todas las reservas.");
            return _dalReserva.Buscar(textoLibre);
        }

        // ---- Cancelar reserva ----

        // Reglas de penalidad según el tiempo que falta para la salida (ajustable si la cátedra
        // pide otros porcentajes u horas de corte):
        //   72 hs o más antes de la salida -> sin cargo.
        //   entre 24 y 72 hs                -> 30% del importe total.
        //   menos de 24 hs                  -> 100% del importe total (sin reembolso).
        public const int HORAS_SIN_PENALIDAD = 72;
        public const int HORAS_PENALIDAD_PARCIAL = 24;
        public const decimal PORCENTAJE_PENALIDAD_PARCIAL = 0.30m;
        public const decimal PORCENTAJE_PENALIDAD_TOTAL = 1.00m;

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
                throw new NegocioException_GV42("No tenés permiso para cancelar reservas.");

            Reserva_GV42 reserva = BuscarReserva(numeroReserva);
            if (reserva == null)
                throw new NegocioException_GV42("No existe una reserva con el número indicado.");
            if (reserva.Estado == EstadoReserva_GV42.Cancelada)
                throw new NegocioException_GV42("La reserva ya estaba cancelada.");

            // Con la patente "propia" solo se cancelan las reservas del propio usuario.
            if (!cancelaCualquiera && !EsDeLaSesion(reserva))
                throw new NegocioException_GV42("No podés cancelar una reserva que no es tuya.");

            if (reserva.Vuelo.FechaHoraSalida <= DateTime.Now)
                throw new NegocioException_GV42("El vuelo ya salió: la reserva no se puede cancelar.");
            if (_dalReserva.TieneCheckInRealizado(reserva.Id))
                throw new NegocioException_GV42("Algún pasajero ya hizo el check-in: la reserva no se puede cancelar.");

            // Una reserva pendiente de pago no cobró nada: se cancela sin penalidad.
            decimal porcentaje = reserva.Estado == EstadoReserva_GV42.PendienteDePago
                ? 0m
                : CalcularPorcentajePenalidad(reserva.Vuelo.FechaHoraSalida);
            decimal monto = Math.Round(reserva.ImporteTotal * porcentaje, 2);

            Reserva_GV42 cancelada = _dalReserva.Cancelar(reserva.Id, monto);
            RecalcularIntegridad("Reserva");

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Reserva cancelada",
                cancelada.NumeroReserva + " - penalidad " + BLLNegocioUtil_GV42.Dinero(monto) +
                " (" + (porcentaje * 100) + "%)", "Media");

            return cancelada;
        }

        // ---- Paso 14: boletos para entregar al cliente (uno por pasajero) ----

        public List<Boleto_GV42> ObtenerBoletos(string numeroReserva)
        {
            Reserva_GV42 reserva = BuscarReserva(numeroReserva);
            if (reserva == null)
                throw new NegocioException_GV42("No existe una reserva con el número indicado.");
            if (reserva.Estado != EstadoReserva_GV42.Confirmada)
                throw new NegocioException_GV42("La reserva todavía no está confirmada: no hay boletos para entregar.");

            return _dalBoleto.ListarPorReserva(reserva.NumeroReserva);
        }
    }
}
