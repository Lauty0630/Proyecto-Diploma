using BE;
using DAL;
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
        private readonly DALCliente_GV42 _dalCliente;
        private readonly DALReserva_GV42 _dalReserva;
        private readonly DALPago_GV42 _dalPago;
        private readonly DALBoleto_GV42 _dalBoleto;

        public BLLReserva_GV42()
        {
            _dalAeropuerto = new DALAeropuerto_GV42();
            _dalTipoAdicional = new DALTipoAdicional_GV42();
            _dalVuelo = new DALVuelo_GV42();
            _dalCliente = new DALCliente_GV42();
            _dalReserva = new DALReserva_GV42();
            _dalPago = new DALPago_GV42();
            _dalBoleto = new DALBoleto_GV42();
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

        // ---- Pasos 6 y 7: cliente ----

        // Devuelve null si el DNI no está registrado.
        public Cliente_GV42 BuscarCliente(string dni)
        {
            dni = (dni ?? string.Empty).Trim();
            if (!Servicios.Validaciones_GV42.EsDniValido(dni))
                throw new NegocioException_GV42(Servicios.Validaciones_GV42.MENSAJE_DNI);
            return _dalCliente.BuscarPorDni(dni);
        }

        public void RegistrarCliente(Cliente_GV42 cliente)
        {
            BLLNegocioUtil_GV42.ValidarPersona(cliente, "Cliente");

            if (_dalCliente.ExisteDni(cliente.DNI))
                throw new NegocioException_GV42("Ya existe un cliente registrado con el DNI " + cliente.DNI + ".");

            _dalCliente.Insertar(cliente);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Cliente registrado",
                "DNI " + cliente.DNI, "Baja");
        }

        // ---- Pasos 8 a 10: generar la reserva en estado "Pendiente de Pago" ----

        // 'borrador' debe traer: Cliente, VueloClase (con Vuelo.Id y Clase), TipoViaje, FechaRegreso (si es
        // ida y vuelta), Pasajeros y Adicionales (opcional). Importes, estado y vendedor los completa esta clase.
        public Reserva_GV42 GenerarReserva(Reserva_GV42 borrador)
        {
            if (borrador == null)
                throw new NegocioException_GV42("Faltan los datos de la reserva.");
            if (borrador.Cliente == null || string.IsNullOrWhiteSpace(borrador.Cliente.DNI))
                throw new NegocioException_GV42("Debe indicar el cliente de la reserva.");
            if (borrador.VueloClase == null || borrador.VueloClase.Vuelo == null || borrador.VueloClase.Vuelo.Id <= 0)
                throw new NegocioException_GV42("Debe seleccionar el vuelo y la clase.");

            string login = BLLNegocioUtil_GV42.LoginActual();

            if (!_dalCliente.ExisteDni(borrador.Cliente.DNI.Trim()))
                throw new NegocioException_GV42("El cliente no está registrado. Regístrelo antes de generar la reserva.");

            ValidarPasajeros(borrador.Pasajeros);
            ValidarAdicionales(borrador.Adicionales);

            // El precio y la disponibilidad se toman siempre de la base, no de lo que traiga la pantalla.
            VueloClase_GV42 vc = _dalVuelo.BuscarVueloClase(borrador.VueloClase.Vuelo.Id, borrador.VueloClase.Clase);
            if (vc == null)
                throw new NegocioException_GV42("El vuelo no ofrece la clase seleccionada.");
            if (vc.Vuelo.FechaHoraSalida <= DateTime.Now)
                throw new NegocioException_GV42("El vuelo seleccionado ya salió.");
            if (vc.AsientosDisponibles < borrador.CantidadPasajeros)
                throw new NegocioException_GV42("Solo quedan " + vc.AsientosDisponibles + " asiento(s) disponible(s) en esa clase.");

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

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Reserva generada",
                creada.NumeroReserva + " - vuelo " + vc.CodigoVuelo + " - total " + BLLNegocioUtil_GV42.Dinero(creada.ImporteTotal), "Media");

            return creada;
        }

        private void ValidarPasajeros(List<Pasajero_GV42> pasajeros)
        {
            if (pasajeros == null || pasajeros.Count == 0)
                throw new NegocioException_GV42("Debe registrar al menos un pasajero.");

            foreach (Pasajero_GV42 p in pasajeros)
                BLLNegocioUtil_GV42.ValidarPersona(p, "Pasajero");

            var repetido = pasajeros.GroupBy(p => p.DNI).FirstOrDefault(g => g.Count() > 1);
            if (repetido != null)
                throw new NegocioException_GV42("El pasajero con DNI " + repetido.Key + " está cargado más de una vez.");
        }

        private void ValidarAdicionales(List<AdicionalReserva_GV42> adicionales)
        {
            if (adicionales == null) return;

            foreach (AdicionalReserva_GV42 a in adicionales)
            {
                if (a.TipoAdicional == null || a.TipoAdicional.Id <= 0)
                    throw new NegocioException_GV42("Cada servicio adicional debe tener un tipo.");
                if (a.Cantidad < 1)
                    throw new NegocioException_GV42("La cantidad de '" + a.TipoNombre + "' debe ser al menos 1.");
                if (a.CostoUnitario < 0)
                    throw new NegocioException_GV42("El costo unitario de '" + a.TipoNombre + "' no puede ser negativo.");
            }
        }

        // Devuelve null si el número de reserva no existe.
        public Reserva_GV42 BuscarReserva(string numeroReserva)
        {
            numeroReserva = (numeroReserva ?? string.Empty).Trim();
            if (numeroReserva.Length == 0)
                throw new NegocioException_GV42("Debe indicar el número de reserva.");
            return _dalReserva.BuscarPorNumero(numeroReserva);
        }

        // ---- Pasos 11 a 13: registrar el pago y confirmar la reserva ----

        // Para efectivo el número de transacción es opcional (se autogenera); para tarjeta y
        // transferencia es obligatorio. El importe debe coincidir con el total de la reserva.
        public Pago_GV42 RegistrarPago(string numeroReserva, MedioPago_GV42 medioPago, decimal importeAbonado, string numeroTransaccion)
        {
            string login = BLLNegocioUtil_GV42.LoginActual();

            Reserva_GV42 reserva = BuscarReserva(numeroReserva);
            if (reserva == null)
                throw new NegocioException_GV42("No existe una reserva con el número indicado.");
            if (reserva.Estado != EstadoReserva_GV42.PendienteDePago)
                throw new NegocioException_GV42("La reserva " + reserva.NumeroReserva + " ya está " + reserva.EstadoTexto.ToLower() + ".");

            if (Math.Round(importeAbonado, 2) != reserva.ImporteTotal)
                throw new NegocioException_GV42("El importe abonado (" + BLLNegocioUtil_GV42.Dinero(importeAbonado) +
                    ") debe coincidir con el total de la reserva (" + BLLNegocioUtil_GV42.Dinero(reserva.ImporteTotal) + ").");

            numeroTransaccion = (numeroTransaccion ?? string.Empty).Trim();
            if (numeroTransaccion.Length == 0)
            {
                if (medioPago == MedioPago_GV42.Efectivo)
                    numeroTransaccion = "EFE-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                else
                    throw new NegocioException_GV42("Debe indicar el número de transacción del pago.");
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

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Pago registrado",
                reserva.NumeroReserva + " - " + medioPago.Texto() + " - " + BLLNegocioUtil_GV42.Dinero(registrado.ImporteTotalAbonado), "Media");

            return registrado;
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
