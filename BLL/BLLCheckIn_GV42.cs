using BE;
using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    // RFN 2 - Check-in presencial (Encargado de Check-in + Cliente/Pasajero).
    public class BLLCheckIn_GV42
    {
        // El check-in se habilita desde 48 hs antes de la salida y se cierra 60 minutos antes.
        public const int HORAS_APERTURA_CHECKIN = 48;
        public const int MINUTOS_CIERRE_CHECKIN = 60;

        // Hora límite de embarque impresa en la tarjeta: 30 minutos antes de la salida.
        public const int MINUTOS_LIMITE_EMBARQUE = 30;

        private readonly DALCheckIn_GV42 _dalCheckIn;
        private readonly DALAsiento_GV42 _dalAsiento;
        private readonly DALEquipaje_GV42 _dalEquipaje;
        private readonly DALTarjetaEmbarque_GV42 _dalTarjeta;

        public BLLCheckIn_GV42()
        {
            _dalCheckIn = new DALCheckIn_GV42();
            _dalAsiento = new DALAsiento_GV42();
            _dalEquipaje = new DALEquipaje_GV42();
            _dalTarjeta = new DALTarjetaEmbarque_GV42();
        }

        // ---- Pasos 1 a 4: iniciar y verificar la reserva ----

        // Devuelve el check-in con los datos que el encargado debe verificar (pasajero, vuelo, servicios
        // adicionales, estado de la reserva y del check-in). Lanza NegocioException_GV42 si la reserva no
        // está confirmada, si el check-in ya se hizo o si está fuera de la ventana de check-in.
        public CheckIn_GV42 IniciarCheckIn(string numeroReserva, string dniPasajero)
        {
            numeroReserva = (numeroReserva ?? string.Empty).Trim();
            dniPasajero = (dniPasajero ?? string.Empty).Trim();

            if (numeroReserva.Length == 0)
                throw new NegocioException_GV42("Debe indicar el número de reserva.");
            if (!Servicios.Validaciones_GV42.EsDniValido(dniPasajero))
                throw new NegocioException_GV42(Servicios.Validaciones_GV42.MENSAJE_DNI);

            CheckIn_GV42 ci = _dalCheckIn.BuscarPorReservaYDni(numeroReserva, dniPasajero);
            if (ci == null)
                throw new NegocioException_GV42("No se encontró la reserva, o el DNI no corresponde a un pasajero de esa reserva.");

            ValidarPuedeHacerCheckIn(ci);
            return ci;
        }

        private void ValidarPuedeHacerCheckIn(CheckIn_GV42 ci)
        {
            if (ci.EstadoReserva != EstadoReserva_GV42.Confirmada)
                throw new NegocioException_GV42("La reserva " + ci.NumeroReserva + " no está confirmada (estado: " +
                    ci.EstadoReservaTexto + "). No se puede continuar con el check-in.");

            if (ci.Estado == EstadoCheckIn_GV42.Realizado)
                throw new NegocioException_GV42("El check-in de este pasajero ya fue realizado.");

            DateTime salida = ci.Vuelo.FechaHoraSalida;
            DateTime ahora = DateTime.Now;
            if (ahora < salida.AddHours(-HORAS_APERTURA_CHECKIN) || ahora > salida.AddMinutes(-MINUTOS_CIERRE_CHECKIN))
                throw new NegocioException_GV42("El check-in no puede realizarse: está fuera de la ventana permitida (se habilita " +
                    HORAS_APERTURA_CHECKIN + " hs antes de la salida y se cierra " + MINUTOS_CIERRE_CHECKIN + " minutos antes).");
        }

        // Recarga el check-in y verifica que todavía se pueda operar sobre él.
        private CheckIn_GV42 ObtenerPendiente(int idCheckIn)
        {
            CheckIn_GV42 ci = _dalCheckIn.BuscarPorId(idCheckIn);
            if (ci == null)
                throw new NegocioException_GV42("No existe el check-in indicado.");

            ValidarPuedeHacerCheckIn(ci);
            return ci;
        }

        // ---- Pasos 5 a 7: equipaje ----

        // Calcula el cargo por exceso sin guardar nada. Sirve para mostrarle el importe al pasajero
        // antes de cobrarle. Si no hay exceso, KilosExceso e ImporteCargo valen 0.
        public CargoExcesoEquipaje_GV42 CalcularCargoExceso(int idCheckIn, decimal pesoTotalKg)
        {
            if (pesoTotalKg <= 0)
                throw new NegocioException_GV42("El peso total debe ser mayor a 0 kg.");

            return CalcularCargo(ObtenerPendiente(idCheckIn), pesoTotalKg);
        }

        private CargoExcesoEquipaje_GV42 CalcularCargo(CheckIn_GV42 ci, decimal pesoTotalKg)
        {
            decimal exceso = Math.Max(0m, pesoTotalKg - ci.VueloClase.FranquiciaEquipajeKg);
            decimal costoKilo = ci.Vuelo.CostoKiloExceso;

            return new CargoExcesoEquipaje_GV42
            {
                KilosExceso = exceso,
                CostoPorKilo = costoKilo,
                ImporteCargo = Math.Round(exceso * costoKilo, 2)
            };
        }

        // Registra el equipaje despachado y genera una etiqueta por bulto. Si el peso supera la franquicia
        // también registra el cargo por exceso y su cobro: en ese caso 'medioCobro' es obligatorio, y el
        // número de transacción lo es salvo en efectivo.
        public const int MAX_BULTOS = 10;
        public const decimal MAX_PESO_KG = 500m;

        public Equipaje_GV42 RegistrarEquipaje(int idCheckIn, int cantidadBultos, decimal pesoTotalKg,
                                               MedioPago_GV42? medioCobro, string numeroTransaccion)
        {
            if (cantidadBultos < 1)
                throw new NegocioException_GV42("La cantidad de bultos debe ser al menos 1.");
            // Topes razonables (y dentro de las columnas decimal(7,2) de la base).
            if (cantidadBultos > MAX_BULTOS)
                throw new NegocioException_GV42("Se pueden despachar como máximo " + MAX_BULTOS + " bultos por pasajero.");
            if (pesoTotalKg > MAX_PESO_KG)
                throw new NegocioException_GV42("El peso total no puede superar los " + MAX_PESO_KG + " kg.");
            if (pesoTotalKg <= 0)
                throw new NegocioException_GV42("El peso total debe ser mayor a 0 kg.");

            CheckIn_GV42 ci = ObtenerPendiente(idCheckIn);
            if (ci.Equipaje != null)
                throw new NegocioException_GV42("El pasajero ya despachó equipaje en este check-in.");

            CargoExcesoEquipaje_GV42 cargo = CalcularCargo(ci, pesoTotalKg);
            if (cargo.TieneExceso)
            {
                if (!medioCobro.HasValue)
                    throw new NegocioException_GV42("El equipaje excede la franquicia en " + cargo.KilosExceso.ToString("0.##") +
                        " kg (cargo " + BLLNegocioUtil_GV42.Dinero(cargo.ImporteCargo) + "). Debe indicar el medio de cobro.");

                numeroTransaccion = (numeroTransaccion ?? string.Empty).Trim();
                if (numeroTransaccion.Length == 0 && medioCobro.Value != MedioPago_GV42.Efectivo)
                    throw new NegocioException_GV42("Debe indicar el número de transacción del cobro por exceso de equipaje.");
                if (numeroTransaccion.Length > Servicios.Validaciones_GV42.MAX_NUMERO_TRANSACCION)
                    throw new NegocioException_GV42("El número de transacción no puede superar los 40 caracteres.");

                cargo.MedioPago = medioCobro;
                cargo.NumeroTransaccion = numeroTransaccion.Length == 0 ? null : numeroTransaccion;
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
                FranquiciaKg = ci.VueloClase.FranquiciaEquipajeKg,
                CargoExceso = cargo
            };

            // Códigos de equipaje: EQ + Nº de check-in + Nº de bulto (ej: EQ000012-01).
            for (int i = 1; i <= cantidadBultos; i++)
                equipaje.Etiquetas.Add("EQ" + ci.Id.ToString("D6") + "-" + i.ToString("D2"));

            Equipaje_GV42 guardado = _dalEquipaje.Registrar(equipaje);

            string detalle = ci.NumeroReserva + " - DNI " + ci.Pasajero.DNI + " - " + cantidadBultos + " bulto(s), " + pesoTotalKg.ToString("0.##") + " kg";
            if (cargo != null) detalle += " - exceso " + BLLNegocioUtil_GV42.Dinero(cargo.ImporteCargo);
            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_CHECKIN, "Equipaje despachado", detalle, "Baja");

            return guardado;
        }

        // ---- Paso 8: validar o asignar asiento ----

        public List<Asiento_GV42> ListarAsientosLibres(int idCheckIn)
        {
            CheckIn_GV42 ci = ObtenerPendiente(idCheckIn);
            return _dalAsiento.ListarLibres(ci.Vuelo.Id, ci.VueloClase.Clase);
        }

        // Si 'numeroAsiento' viene vacío, mantiene el asiento que ya tenía el pasajero o le asigna el primero
        // libre de su clase. Si viene informado, valida que exista, sea de la clase reservada y esté libre.
        public Asiento_GV42 AsignarAsiento(int idCheckIn, string numeroAsiento)
        {
            CheckIn_GV42 ci = ObtenerPendiente(idCheckIn);
            numeroAsiento = (numeroAsiento ?? string.Empty).Trim().ToUpper();

            Asiento_GV42 asiento;
            if (numeroAsiento.Length == 0)
            {
                if (ci.Asiento != null) return ci.Asiento;

                List<Asiento_GV42> libres = _dalAsiento.ListarLibres(ci.Vuelo.Id, ci.VueloClase.Clase);
                if (libres.Count == 0)
                    throw new NegocioException_GV42("No quedan asientos libres en clase " + ci.VueloClase.ClaseTexto.ToLower() + ".");
                asiento = libres[0];
            }
            else
            {
                asiento = _dalAsiento.BuscarPorNumero(ci.Vuelo.Id, numeroAsiento);
                if (asiento == null)
                    throw new NegocioException_GV42("El asiento " + numeroAsiento + " no existe en este vuelo.");
                if (asiento.Clase != ci.VueloClase.Clase)
                    throw new NegocioException_GV42("El asiento " + numeroAsiento + " es de clase " + asiento.ClaseTexto.ToLower() +
                        " y la reserva es de clase " + ci.VueloClase.ClaseTexto.ToLower() + ".");
                if (ci.Asiento != null && ci.Asiento.Id == asiento.Id) return asiento;
                if (_dalAsiento.EstaOcupado(asiento.Id))
                    throw new NegocioException_GV42("El asiento " + numeroAsiento + " ya está ocupado.");
            }

            _dalAsiento.Asignar(ci.Id, asiento.Id);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_CHECKIN, "Asiento asignado",
                ci.NumeroReserva + " - DNI " + ci.Pasajero.DNI + " - asiento " + asiento.NumeroAsiento, "Baja");

            return asiento;
        }

        // ---- Paso 9: generar la tarjeta de embarque ----

        public TarjetaEmbarque_GV42 GenerarTarjetaEmbarque(int idCheckIn)
        {
            CheckIn_GV42 ci = ObtenerPendiente(idCheckIn);

            if (ci.Asiento == null)
                throw new NegocioException_GV42("Primero debe validar o asignar el asiento del pasajero.");
            if (ci.TarjetaEmbarque != null)
                return ci.TarjetaEmbarque;

            DateTime horaLimite = ci.Vuelo.FechaHoraSalida.AddMinutes(-MINUTOS_LIMITE_EMBARQUE);
            TarjetaEmbarque_GV42 tarjeta = _dalTarjeta.Generar(ci.Id, ci.Vuelo.PuertaEmbarque, horaLimite);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_CHECKIN, "Tarjeta de embarque emitida",
                tarjeta.NumeroTarjeta + " - " + ci.NumeroReserva + " - asiento " + tarjeta.NumeroAsiento, "Baja");

            return tarjeta;
        }

        // ---- Paso 10: entregar la tarjeta y cerrar el check-in ----

        // Cambia el estado del check-in a "Realizado". Requiere que la tarjeta ya esté emitida.
        // Devuelve el check-in completo para imprimir/entregar.
        public CheckIn_GV42 FinalizarCheckIn(int idCheckIn)
        {
            string login = BLLNegocioUtil_GV42.LoginActual();

            CheckIn_GV42 ci = ObtenerPendiente(idCheckIn);
            if (ci.TarjetaEmbarque == null)
                throw new NegocioException_GV42("Primero debe generarse la tarjeta de embarque.");

            _dalCheckIn.MarcarRealizado(ci.Id, login);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_CHECKIN, "Check-in realizado",
                ci.NumeroReserva + " - DNI " + ci.Pasajero.DNI, "Media");

            return _dalCheckIn.BuscarPorId(ci.Id);
        }
    }
}
