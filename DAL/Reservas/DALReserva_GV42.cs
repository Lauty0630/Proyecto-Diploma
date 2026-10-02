using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALReserva_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        private const string SELECT_LISTADO =
            "SELECT R.Id AS IdReserva, R.NumeroReserva, R.IdTipoViaje, R.FechaRegreso, R.IdVueloVuelta, R.IdClaseVuelta, " +
            "       R.ImporteBase, R.SubtotalAdicionales, R.Impuestos, R.ImporteTotal, " +
            "       R.IdEstadoReserva, R.FechaRealizacion, R.LoginVendedor, " +
            "       R.FechaCancelacion, R.MontoPenalidadCancelacion, R.FechaVencimiento, R.VencidaSinPago, R.IdTarifa, R.IdCanalVenta, " +
            "       C.DNI AS CliDNI, C.Nombre AS CliNombre, C.Apellido AS CliApellido, " +
            "       C.Email AS CliEmail, C.Telefono AS CliTelefono, " +
            DALUtil_GV42.COLUMNAS_VUELO_CLASE + " " +
            "FROM Reserva R " +
            "INNER JOIN Pasajero C ON C.DNI = R.DniCliente " +
            "INNER JOIN Vuelo V ON V.Id = R.IdVuelo " +
            "INNER JOIN VueloClase VC ON VC.IdVuelo = R.IdVuelo AND VC.IdClase = R.IdClase" +
            DALUtil_GV42.JOINS_VUELO;

        #endregion

        #region Constructor

        public DALReserva_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Métodos públicos

        // Guarda la reserva completa (cabecera, pasajeros, adicionales) y descuenta la disponibilidad
        // de asientos, todo en una sola transacción. Devuelve la reserva con Id, NumeroReserva y
        // FechaRealizacion completados.
        public Reserva_GV42 Crear(Reserva_GV42 r)
        {
            try
            {
                // Lugares que ocupa en cada vuelo (los infantes viajan en brazos).
                int asientos = r.CantidadAsientos;

                return _acceso.EjecutarEnTransaccion(tx =>
                {
                    // 1) Reservar los asientos. La condición evita vender de más si dos vendedores compiten.
                    int filas = _acceso.escribir(tx,
                        "UPDATE VueloClase SET AsientosReservados = AsientosReservados + @Cant " +
                        "WHERE IdVuelo = @IdVuelo AND IdClase = @IdClase " +
                        "AND (CapacidadAsientos - AsientosReservados) >= @Cant " +
                        // El vuelo tiene que seguir activo y sin salir en el momento exacto de reservar
                        // (la BLL lo valida antes, pero fuera de esta transacción).
                        "AND EXISTS (SELECT 1 FROM Vuelo V WHERE V.Id = @IdVuelo AND V.BorradoLogico = 0 AND V.FechaHoraSalida > GETDATE())",
                        new[] {
                        new SqlParameter("@Cant",    asientos),
                        new SqlParameter("@IdVuelo", r.Vuelo.Id),
                        new SqlParameter("@IdClase", (int)r.Clase)
                        });

                    if (filas == 0)
                        throw new NegocioException_GV42("No se pudo reservar: no hay asientos suficientes en esa clase, " +
                                                        "o el vuelo ya salió o fue dado de baja.");

                    // 1.b) Ida y vuelta: se reserva también el cupo del vuelo de regreso.
                    if (r.TieneVuelta)
                    {
                        filas = _acceso.escribir(tx,
                            "UPDATE VueloClase SET AsientosReservados = AsientosReservados + @Cant " +
                            "WHERE IdVuelo = @IdVuelo AND IdClase = @IdClase " +
                            "AND (CapacidadAsientos - AsientosReservados) >= @Cant " +
                            "AND EXISTS (SELECT 1 FROM Vuelo V WHERE V.Id = @IdVuelo AND V.BorradoLogico = 0 AND V.FechaHoraSalida > GETDATE())",
                            new[] {
                            new SqlParameter("@Cant",    asientos),
                            new SqlParameter("@IdVuelo", r.VueloClaseVuelta.Vuelo.Id),
                            new SqlParameter("@IdClase", (int)r.VueloClaseVuelta.Clase)
                            });
                        if (filas == 0)
                            throw new NegocioException_GV42("No se pudo reservar: el vuelo de regreso no tiene asientos suficientes " +
                                                            "en esa clase, ya salió o fue dado de baja.");
                    }

                    // 2) Cabecera de la reserva.
                    object idObj = _acceso.leerEscalar(tx,
                        "INSERT INTO Reserva (DniCliente, IdVuelo, IdClase, IdVueloVuelta, IdClaseVuelta, IdTipoViaje, FechaRegreso, CantidadPasajeros, " +
                        "                     CantidadAsientos, IdTarifa, FechaVencimiento, " +
                        "                     ImporteBase, SubtotalAdicionales, Impuestos, ImporteTotal, IdEstadoReserva, LoginVendedor, IdCanalVenta) " +
                        "VALUES (@DniCliente, @IdVuelo, @IdClase, @IdVueloVuelta, @IdClaseVuelta, @IdTipoViaje, @FechaRegreso, @Cantidad, " +
                        "        @Asientos, @IdTarifa, @Vencimiento, " +
                        "        @ImporteBase, @Subtotal, @Impuestos, @Total, @IdEstado, @Login, @IdCanal); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new[] {
                        new SqlParameter("@DniCliente",  r.Cliente.DNI),
                        new SqlParameter("@IdVuelo",     r.Vuelo.Id),
                        new SqlParameter("@IdClase",     (int)r.Clase),
                        new SqlParameter("@IdVueloVuelta", r.TieneVuelta ? (object)r.VueloClaseVuelta.Vuelo.Id : DBNull.Value),
                        new SqlParameter("@IdClaseVuelta", r.TieneVuelta ? (object)(int)r.VueloClaseVuelta.Clase : DBNull.Value),
                        new SqlParameter("@IdTipoViaje", (int)r.TipoViaje),
                        new SqlParameter("@FechaRegreso", DALUtil_GV42.ADb(r.FechaRegreso)),
                        new SqlParameter("@Cantidad",    r.CantidadPasajeros),
                        new SqlParameter("@Asientos",    asientos),
                        new SqlParameter("@IdTarifa",    r.Tarifa != null ? (object)r.Tarifa.Id : DBNull.Value),
                        new SqlParameter("@Vencimiento", SqlDbType.DateTime2) { Value = DALUtil_GV42.ADb(r.FechaVencimiento) },
                        new SqlParameter("@ImporteBase", r.ImporteBase),
                        new SqlParameter("@Subtotal",    r.SubtotalAdicionales),
                        new SqlParameter("@Impuestos",   r.Impuestos),
                        new SqlParameter("@Total",       r.ImporteTotal),
                        new SqlParameter("@IdEstado",    (int)r.Estado),
                        new SqlParameter("@Login",       r.LoginVendedor),
                        // Antes no se guardaba: todas las reservas quedaban como "Presencial" (default 1).
                        new SqlParameter("@IdCanal",     (int)r.CanalVenta)
                        });
                    int idReserva = Convert.ToInt32(idObj);

                    // 3) Pasajeros: se registran si no existen. Se vinculan a la reserva en el paso 3.5, con
                    //    una fila por tramo (ida / vuelta) que lleva el asiento y el equipaje extra de ese tramo.
                    //    Si el DNI ya estaba registrado NO se modifican sus datos: antes se hacía un UPDATE y una
                    //    reserva podía pisar el nombre, email y teléfono de otra persona con solo tipear su DNI.
                    //    (La BLL ya verificó que el nombre y apellido coincidan con los registrados.)
                    foreach (Pasajero_GV42 p in r.Pasajeros)
                    {
                        _acceso.escribir(tx,
                            "IF NOT EXISTS (SELECT 1 FROM Pasajero WHERE DNI = @DNI) " +
                            "    INSERT INTO Pasajero (DNI, Nombre, Apellido, Email, Telefono, FechaNacimiento) VALUES (@DNI, @Nombre, @Apellido, @Email, @Telefono, @Nacimiento) " +
                            // Pasajeros registrados antes de que se pidiera la fecha de nacimiento: se completa.
                            "ELSE IF @Nacimiento IS NOT NULL " +
                            "    UPDATE Pasajero SET FechaNacimiento = @Nacimiento WHERE DNI = @DNI AND FechaNacimiento IS NULL",
                            new[] {
                            new SqlParameter("@Nacimiento", SqlDbType.Date) { Value = DALUtil_GV42.ADb(p.FechaNacimiento) },
                            new SqlParameter("@DNI",      p.DNI),
                            new SqlParameter("@Nombre",   p.Nombre),
                            new SqlParameter("@Apellido", p.Apellido),
                            new SqlParameter("@Email",    DALUtil_GV42.Cifrar(p.Email)),
                            new SqlParameter("@Telefono", p.Telefono)
                            });
                    }

                    // 4) Servicios adicionales.
                    foreach (AdicionalReserva_GV42 a in r.Adicionales)
                    {
                        _acceso.escribir(tx,
                            "INSERT INTO ReservaAdicional (IdReserva, IdTipoAdicional, Cantidad, CostoUnitario, Tramo) " +
                            "VALUES (@IdReserva, @IdTipo, @Cantidad, @Costo, @Tramo)",
                            new[] {
                            new SqlParameter("@Tramo",     a.Tramo == 2 ? 2 : 1),
                            new SqlParameter("@IdReserva", idReserva),
                            new SqlParameter("@IdTipo",    a.TipoAdicional.Id),
                            new SqlParameter("@Cantidad",  a.Cantidad),
                            new SqlParameter("@Costo",     a.CostoUnitario)
                            });
                    }

                    // 3.5) Una fila por pasajero y por tramo, con el asiento elegido (selección estilo cine), su
                    // equipaje extra, su tipo (adulto / niño / infante) y la asistencia especial que pidió.
                    // El asiento puede quedar sin elegir (tarifa Light o infante): se asigna en el check-in.
                    // El índice único filtrado UX_ReservaPasajero_Asiento asegura que dos pasajeros no se
                    // queden con el mismo asiento aunque compitan al mismo tiempo.
                    foreach (AsientoPasajero_GV42 ap in r.AsientosPorPasajero)
                    {
                        Pasajero_GV42 pasajero = r.Pasajeros.Find(x => x.DNI == ap.DniPasajero);
                        _acceso.escribir(tx,
                            "INSERT INTO ReservaPasajero (IdReserva, DniPasajero, Tramo, IdAsiento, EquipajeExtra, IdTipoPasajero, IdAsistencia) " +
                            "VALUES (@IdReserva, @DNI, @Tramo, @IdAsiento, @Extra, @Tipo, @Asistencia)",
                            new[] {
                            new SqlParameter("@Tipo",       pasajero != null ? (int)pasajero.Tipo : (int)TipoPasajero_GV42.Adulto),
                            new SqlParameter("@Asistencia", pasajero != null ? (int)pasajero.Asistencia : 0),
                            new SqlParameter("@Tramo",     ap.Tramo == 2 ? 2 : 1),
                            new SqlParameter("@Extra",     Math.Max(0, ap.EquipajeExtra)),
                            new SqlParameter("@IdAsiento", ap.Asiento != null ? (object)ap.Asiento.Id : DBNull.Value),
                            new SqlParameter("@IdReserva", idReserva),
                            new SqlParameter("@DNI",       ap.DniPasajero)
                            });
                    }

                    // 5) Datos autogenerados por la base.
                    DataTable dt = _acceso.leer(tx,
                        "SELECT NumeroReserva, FechaRealizacion FROM Reserva WHERE Id = @Id",
                        new[] { new SqlParameter("@Id", idReserva) });

                    r.Id = idReserva;
                    r.NumeroReserva = DALUtil_GV42.Str(dt.Rows[0], "NumeroReserva");
                    r.FechaRealizacion = DALUtil_GV42.Fecha(dt.Rows[0], "FechaRealizacion");
                    return r;
                });
            }
            catch (SqlException ex)
            {
                if ((ex.Number == 2601 || ex.Number == 2627) && ex.Message.Contains("UX_ReservaPasajero_Asiento"))
                    throw new NegocioException_GV42("Uno de los asientos elegidos ya fue tomado por otro pasajero. Volvé a elegir el asiento.", ex);
                if (ex.Number == 2601 || ex.Number == 2627)
                    throw new NegocioException_GV42("Otro usuario registró al mismo tiempo a uno de los pasajeros. Volvé a confirmar la reserva.", ex);
                if (ex.Number == 547)
                    throw new NegocioException_GV42("Los datos de la reserva no cumplen una regla de la base (cupo, importes o vuelo). Revisalos y volvé a intentar.", ex);
                throw;
            }
        }

        public Reserva_GV42 BuscarPorNumero(string numeroReserva)
        {
            string query =
                "SELECT R.Id AS IdReserva, R.NumeroReserva, R.IdTipoViaje, R.FechaRegreso, R.IdVueloVuelta, R.IdClaseVuelta, " +
                "       R.ImporteBase, R.SubtotalAdicionales, R.Impuestos, R.ImporteTotal, " +
                "       R.IdEstadoReserva, R.FechaRealizacion, R.LoginVendedor, " +
                "       R.FechaCancelacion, R.MontoPenalidadCancelacion, R.FechaVencimiento, R.VencidaSinPago, R.IdTarifa, R.IdCanalVenta, " +
                "       C.DNI AS CliDNI, C.Nombre AS CliNombre, C.Apellido AS CliApellido, " +
                "       C.Email AS CliEmail, C.Telefono AS CliTelefono, " +
                DALUtil_GV42.COLUMNAS_VUELO_CLASE + " " +
                "FROM Reserva R " +
                "INNER JOIN Pasajero C ON C.DNI = R.DniCliente " +
                "INNER JOIN Vuelo V ON V.Id = R.IdVuelo " +
                "INNER JOIN VueloClase VC ON VC.IdVuelo = R.IdVuelo AND VC.IdClase = R.IdClase" +
                DALUtil_GV42.JOINS_VUELO + " " +
                "WHERE R.NumeroReserva = @Numero";

            DataTable dt = _acceso.leer(query, new[] { new SqlParameter("@Numero", numeroReserva) });
            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];
            var cliente = new Pasajero_GV42();
            DALUtil_GV42.LlenarPersona(cliente, row, "Cli");

            var reserva = new Reserva_GV42
            {
                Id = DALUtil_GV42.Int(row, "IdReserva"),
                NumeroReserva = DALUtil_GV42.Str(row, "NumeroReserva"),
                Cliente = cliente,
                VueloClase = DALUtil_GV42.MapearVueloClase(row),
                VueloClaseVuelta = BuscarVueloClaseVuelta(row),
                TipoViaje = (TipoViaje_GV42)DALUtil_GV42.Int(row, "IdTipoViaje"),
                FechaRegreso = DALUtil_GV42.FechaNull(row, "FechaRegreso"),
                ImporteBase = DALUtil_GV42.Dec(row, "ImporteBase"),
                SubtotalAdicionales = DALUtil_GV42.Dec(row, "SubtotalAdicionales"),
                Impuestos = DALUtil_GV42.Dec(row, "Impuestos"),
                ImporteTotal = DALUtil_GV42.Dec(row, "ImporteTotal"),
                Estado = (EstadoReserva_GV42)DALUtil_GV42.Int(row, "IdEstadoReserva"),
                FechaRealizacion = DALUtil_GV42.Fecha(row, "FechaRealizacion"),
                LoginVendedor = DALUtil_GV42.Str(row, "LoginVendedor")
            };
            LlenarDatosComunes(reserva, row);

            reserva.Pasajeros = ListarPasajeros(reserva.Id);
            reserva.AsientosPorPasajero = ListarAsientosPorPasajero(reserva.Id);
            reserva.Adicionales = ListarAdicionales(reserva.Id);
            reserva.Pago = new DALPago_GV42().BuscarPorReserva(reserva.Id);
            reserva.Reembolso = BuscarReembolso(reserva.Id);
            return reserva;
        }

        // "Mis reservas" del cliente autogestionado.
        // Reservas de una persona: las que sacó (titular) y aquellas en las que viaja como pasajero
        // (antes el acompañante no veía en su cuenta la reserva que le había sacado otro).
        public List<Reserva_GV42> ListarPorPersona(string dni)
        {
            string query = SELECT_LISTADO +
                " WHERE R.DniCliente = @DNI" +
                "    OR EXISTS (SELECT 1 FROM ReservaPasajero RP WHERE RP.IdReserva = R.Id AND RP.DniPasajero = @DNI)" +
                " ORDER BY R.FechaRealizacion DESC";
            DataTable dt = _acceso.leer(query, new[] { new SqlParameter("@DNI", dni) });
            var lista = new List<Reserva_GV42>();
            foreach (DataRow r in dt.Rows) lista.Add(MapearListado(r));
            return lista;
        }

        // Consulta del vendedor: sin texto trae las últimas reservas; con texto filtra por número,
        // DNI o apellido del cliente.
        public List<Reserva_GV42> Buscar(string textoLibre)
        {
            string query = SELECT_LISTADO;
            SqlParameter[] p = null;

            if (!string.IsNullOrWhiteSpace(textoLibre))
            {
                query += " WHERE R.NumeroReserva LIKE @Texto OR C.DNI LIKE @Texto OR C.Apellido LIKE @Texto";
                p = new[] { new SqlParameter("@Texto", "%" + Servicios.Validaciones_GV42.EscaparLike(textoLibre.Trim()) + "%") };
            }
            query += " ORDER BY R.FechaRealizacion DESC";

            DataTable dt = _acceso.leer(query, p);
            var lista = new List<Reserva_GV42>();
            foreach (DataRow r in dt.Rows) lista.Add(MapearListado(r));
            return lista;
        }

        // Cancela la reserva, libera los asientos que tenían sus pasajeros y descuenta el cupo
        // ocupado del vuelo, todo en una sola transacción.
        // Si corresponde devolverle plata al cliente (reserva paga), 'reembolso' trae el importe y el
        // medio de pago: queda registrado como pendiente en la misma transacción.
        public Reserva_GV42 Cancelar(int idReserva, decimal montoPenalidad, Reembolso_GV42 reembolso)
        {
            return _acceso.EjecutarEnTransaccion(tx =>
            {
                DataTable dtRes = _acceso.leer(tx,
                    "SELECT IdVuelo, IdClase, IdVueloVuelta, IdClaseVuelta, ISNULL(CantidadAsientos, CantidadPasajeros) AS CantidadPasajeros, IdEstadoReserva FROM Reserva WHERE Id = @Id",
                    new[] { new SqlParameter("@Id", idReserva) });
                if (dtRes.Rows.Count == 0)
                    throw new NegocioException_GV42("La reserva no existe.");
                if (DALUtil_GV42.Int(dtRes.Rows[0], "IdEstadoReserva") == (int)EstadoReserva_GV42.Cancelada)
                    throw new NegocioException_GV42("La reserva ya estaba cancelada.");

                int idVuelo = DALUtil_GV42.Int(dtRes.Rows[0], "IdVuelo");
                int idClase = DALUtil_GV42.Int(dtRes.Rows[0], "IdClase");
                int cantidad = DALUtil_GV42.Int(dtRes.Rows[0], "CantidadPasajeros");

                // Condicional: si otra operación la canceló (o confirmó) entre la lectura y este UPDATE,
                // no se descuenta el cupo dos veces.
                int filas = _acceso.escribir(tx,
                    "UPDATE Reserva SET IdEstadoReserva = @Cancelada, FechaCancelacion = GETDATE(), MontoPenalidadCancelacion = @Monto " +
                    "WHERE Id = @Id AND IdEstadoReserva = @EstadoLeido",
                    new[] {
                        new SqlParameter("@Cancelada", (int)EstadoReserva_GV42.Cancelada),
                        new SqlParameter("@Monto",     montoPenalidad),
                        new SqlParameter("@Id",        idReserva),
                        new SqlParameter("@EstadoLeido", DALUtil_GV42.Int(dtRes.Rows[0], "IdEstadoReserva"))
                    });
                if (filas == 0)
                    throw new NegocioException_GV42("La reserva cambió de estado mientras se cancelaba. Volvé a consultarla.");

                // Libera los asientos que tenían los pasajeros de esta reserva (también los del check-in).
                _acceso.escribir(tx,
                    "UPDATE ReservaPasajero SET IdAsiento = NULL WHERE IdReserva = @Id",
                    new[] { new SqlParameter("@Id", idReserva) });
                _acceso.escribir(tx,
                    "UPDATE CheckIn SET IdAsiento = NULL WHERE IdReserva = @Id",
                    new[] { new SqlParameter("@Id", idReserva) });

                _acceso.escribir(tx,
                    "UPDATE VueloClase SET AsientosReservados = CASE WHEN AsientosReservados >= @Cant " +
                    "       THEN AsientosReservados - @Cant ELSE 0 END " +
                    "WHERE IdVuelo = @IdVuelo AND IdClase = @IdClase",
                    new[] {
                        new SqlParameter("@Cant",    cantidad),
                        new SqlParameter("@IdVuelo", idVuelo),
                        new SqlParameter("@IdClase", idClase)
                    });

                // Ida y vuelta: también se devuelve el cupo del vuelo de regreso.
                if (dtRes.Rows[0]["IdVueloVuelta"] != DBNull.Value)
                    _acceso.escribir(tx,
                        "UPDATE VueloClase SET AsientosReservados = CASE WHEN AsientosReservados >= @Cant " +
                        "       THEN AsientosReservados - @Cant ELSE 0 END " +
                        "WHERE IdVuelo = @IdVuelo AND IdClase = @IdClase",
                        new[] {
                            new SqlParameter("@Cant",    cantidad),
                            new SqlParameter("@IdVuelo", DALUtil_GV42.Int(dtRes.Rows[0], "IdVueloVuelta")),
                            new SqlParameter("@IdClase", DALUtil_GV42.Int(dtRes.Rows[0], "IdClaseVuelta"))
                        });

                if (reembolso != null && reembolso.Importe > 0)
                    _acceso.escribir(tx,
                        "INSERT INTO Reembolso (IdReserva, Importe, IdMedioPago) VALUES (@IdReserva, @Importe, @IdMedio)",
                        new[] {
                            new SqlParameter("@IdReserva", idReserva),
                            new SqlParameter("@Importe",   reembolso.Importe),
                            new SqlParameter("@IdMedio",   (int)reembolso.MedioPago)
                        });

                DataTable dt = _acceso.leer(tx, SELECT_LISTADO + " WHERE R.Id = @Id", new[] { new SqlParameter("@Id", idReserva) });
                return MapearListado(dt.Rows[0]);
            });
        }

        #region Vencimiento, reembolsos y cambio de vuelo

        // Cancela las reservas pendientes cuyo plazo de pago ya pasó (libera asientos y cupo) y
        // devuelve cuántas vencieron. Se llama dentro de cada operación que consulta disponibilidad o
        // reservas: así el vencimiento rige desde la hora exacta, sin depender de un proceso programado.
        public int LiberarVencidas()
        {
            object r = _acceso.leerEscalar("EXEC dbo.LiberarReservasVencidas_GV42", null);
            return r == null || r == DBNull.Value ? 0 : Convert.ToInt32(r);
        }

        public Reembolso_GV42 BuscarReembolso(int idReserva)
        {
            DataTable dt = _acceso.leer(
                "SELECT Id, IdReserva, Importe, IdMedioPago, IdEstado, FechaSolicitud, FechaProceso, LoginProceso " +
                "FROM Reembolso WHERE IdReserva = @Id", new[] { new SqlParameter("@Id", idReserva) });
            if (dt.Rows.Count == 0) return null;

            DataRow r = dt.Rows[0];
            return new Reembolso_GV42
            {
                Id = DALUtil_GV42.Int(r, "Id"),
                IdReserva = DALUtil_GV42.Int(r, "IdReserva"),
                Importe = DALUtil_GV42.Dec(r, "Importe"),
                MedioPago = (MedioPago_GV42)DALUtil_GV42.Int(r, "IdMedioPago"),
                Estado = (EstadoReembolso_GV42)DALUtil_GV42.Int(r, "IdEstado"),
                FechaSolicitud = DALUtil_GV42.Fecha(r, "FechaSolicitud"),
                FechaProceso = DALUtil_GV42.FechaNull(r, "FechaProceso"),
                LoginProceso = DALUtil_GV42.Str(r, "LoginProceso")
            };
        }

        // Marca el reembolso como procesado (ya se le devolvió la plata al cliente). False si no
        // existía o ya estaba procesado.
        public bool ProcesarReembolso(int idReserva, string login)
        {
            int filas = _acceso.escribir(
                "UPDATE Reembolso SET IdEstado = @Procesado, FechaProceso = GETDATE(), LoginProceso = @Login " +
                "WHERE IdReserva = @Id AND IdEstado = @Pendiente",
                new[] {
                    new SqlParameter("@Procesado", (int)EstadoReembolso_GV42.Procesado),
                    new SqlParameter("@Pendiente", (int)EstadoReembolso_GV42.Pendiente),
                    new SqlParameter("@Login",     login),
                    new SqlParameter("@Id",        idReserva)
                });
            return filas > 0;
        }

        // Cambia el vuelo de un tramo de una reserva confirmada, todo en una transacción:
        // devuelve el cupo del vuelo anterior, toma el del nuevo, reemplaza los asientos de los pasajeros
        // (en la reserva y en su check-in pendiente), actualiza la reserva y sus importes y deja el
        // historial del cambio con lo cobrado. Los boletos no cambian de número: muestran el vuelo nuevo.
        // 'asientos': un elemento por pasajero del tramo; Asiento null = se asigna en el check-in.
        public void CambiarVuelo(Reserva_GV42 reserva, int tramo, VueloClase_GV42 nuevo, List<AsientoPasajero_GV42> asientos,
                                 CotizacionCambio_GV42 cotizacion, MedioPago_GV42? medioPago, string numeroTransaccion, string login)
        {
            VueloClase_GV42 anterior = reserva.VueloClaseDeTramo(tramo);
            int cantidad = reserva.CantidadAsientos;
            bool esVuelta = tramo == Reserva_GV42.TRAMO_VUELTA;

            try
            {
                _acceso.EjecutarEnTransaccion(tx =>
                {
                    // 1) Cupo del vuelo nuevo (con la misma condición que al reservar).
                    int filas = _acceso.escribir(tx,
                        "UPDATE VueloClase SET AsientosReservados = AsientosReservados + @Cant " +
                        "WHERE IdVuelo = @IdVuelo AND IdClase = @IdClase " +
                        "AND (CapacidadAsientos - AsientosReservados) >= @Cant " +
                        "AND EXISTS (SELECT 1 FROM Vuelo V WHERE V.Id = @IdVuelo AND V.BorradoLogico = 0 AND V.FechaHoraSalida > GETDATE())",
                        new[] {
                            new SqlParameter("@Cant",    cantidad),
                            new SqlParameter("@IdVuelo", nuevo.Vuelo.Id),
                            new SqlParameter("@IdClase", (int)nuevo.Clase)
                        });
                    if (filas == 0)
                        throw new NegocioException_GV42("No se pudo cambiar: el vuelo nuevo no tiene asientos suficientes en esa clase, ya salió o fue dado de baja.");

                    // 2) La reserva pasa al vuelo nuevo. Condicional: tiene que seguir confirmada y en el vuelo que se leyó.
                    filas = _acceso.escribir(tx,
                        (esVuelta
                            ? "UPDATE Reserva SET IdVueloVuelta = @IdVuelo, IdClaseVuelta = @IdClase, FechaRegreso = @Fecha, "
                            : "UPDATE Reserva SET IdVuelo = @IdVuelo, IdClase = @IdClase, ") +
                        "       ImporteBase = ImporteBase + @Diferencia, Impuestos = Impuestos + @ImpDif, " +
                        "       ImporteTotal = ImporteTotal + @Diferencia + @ImpDif " +
                        "WHERE Id = @Id AND IdEstadoReserva = @Confirmada AND " +
                        (esVuelta ? "IdVueloVuelta = @IdVueloAnterior" : "IdVuelo = @IdVueloAnterior"),
                        new[] {
                            new SqlParameter("@IdVuelo",    nuevo.Vuelo.Id),
                            new SqlParameter("@IdClase",    (int)nuevo.Clase),
                            new SqlParameter("@Fecha",      SqlDbType.Date) { Value = nuevo.Vuelo.FechaHoraSalida.Date },
                            new SqlParameter("@Diferencia", cotizacion.DiferenciaTarifa),
                            new SqlParameter("@ImpDif",     cotizacion.ImpuestosDiferencia),
                            new SqlParameter("@Id",         reserva.Id),
                            new SqlParameter("@Confirmada", (int)EstadoReserva_GV42.Confirmada),
                            new SqlParameter("@IdVueloAnterior", anterior.Vuelo.Id)
                        });
                    if (filas == 0)
                        throw new NegocioException_GV42("La reserva cambió mientras se hacía el cambio de vuelo. Volvé a consultarla.");

                    // 3) Se devuelve el cupo del vuelo anterior.
                    _acceso.escribir(tx,
                        "UPDATE VueloClase SET AsientosReservados = CASE WHEN AsientosReservados >= @Cant " +
                        "       THEN AsientosReservados - @Cant ELSE 0 END " +
                        "WHERE IdVuelo = @IdVuelo AND IdClase = @IdClase",
                        new[] {
                            new SqlParameter("@Cant",    cantidad),
                            new SqlParameter("@IdVuelo", anterior.Vuelo.Id),
                            new SqlParameter("@IdClase", (int)anterior.Clase)
                        });

                    // 4) Asientos: primero se liberan los del vuelo anterior y después se asignan los nuevos
                    //    (en la reserva y en el check-in pendiente de ese tramo).
                    Func<SqlParameter[]> pTramo = () => new[] { new SqlParameter("@Id", reserva.Id), new SqlParameter("@Tramo", tramo) };
                    _acceso.escribir(tx, "UPDATE ReservaPasajero SET IdAsiento = NULL WHERE IdReserva = @Id AND Tramo = @Tramo", pTramo());
                    _acceso.escribir(tx, "UPDATE CheckIn SET IdAsiento = NULL WHERE IdReserva = @Id AND Tramo = @Tramo", pTramo());

                    foreach (AsientoPasajero_GV42 ap in asientos)
                    {
                        if (ap.Asiento == null) continue;
                        Func<SqlParameter[]> p = () => new[] {
                            new SqlParameter("@IdAsiento", ap.Asiento.Id),
                            new SqlParameter("@Id",        reserva.Id),
                            new SqlParameter("@Tramo",     tramo),
                            new SqlParameter("@DNI",       ap.DniPasajero)
                        };
                        _acceso.escribir(tx,
                            "UPDATE ReservaPasajero SET IdAsiento = @IdAsiento WHERE IdReserva = @Id AND Tramo = @Tramo AND DniPasajero = @DNI", p());
                        _acceso.escribir(tx,
                            "UPDATE CheckIn SET IdAsiento = @IdAsiento WHERE IdReserva = @Id AND Tramo = @Tramo AND DniPasajero = @DNI", p());
                    }

                    // 5) Historial del cambio con lo cobrado.
                    _acceso.escribir(tx,
                        "INSERT INTO CambioReserva (IdReserva, Tramo, IdVueloAnterior, IdClaseAnterior, IdVueloNuevo, IdClaseNuevo, " +
                        "                           Penalidad, DiferenciaTarifa, ImporteCobrado, IdMedioPago, NumeroTransaccion, LoginUsuario) " +
                        "VALUES (@Id, @Tramo, @VA, @CA, @VN, @CN, @Penalidad, @Diferencia, @Cobrado, @IdMedio, @NumTx, @Login)",
                        new[] {
                            new SqlParameter("@Id",         reserva.Id),
                            new SqlParameter("@Tramo",      tramo),
                            new SqlParameter("@VA",         anterior.Vuelo.Id),
                            new SqlParameter("@CA",         (int)anterior.Clase),
                            new SqlParameter("@VN",         nuevo.Vuelo.Id),
                            new SqlParameter("@CN",         (int)nuevo.Clase),
                            new SqlParameter("@Penalidad",  cotizacion.Penalidad),
                            new SqlParameter("@Diferencia", cotizacion.DiferenciaTarifa),
                            new SqlParameter("@Cobrado",    cotizacion.Total),
                            new SqlParameter("@IdMedio",    medioPago.HasValue ? (object)(int)medioPago.Value : DBNull.Value),
                            new SqlParameter("@NumTx",      DALUtil_GV42.ADb(numeroTransaccion)),
                            new SqlParameter("@Login",      login)
                        });
                    return true;
                });
            }
            catch (SqlException ex)
            {
                if ((ex.Number == 2601 || ex.Number == 2627) && (ex.Message.Contains("UX_ReservaPasajero_Asiento") || ex.Message.Contains("UX_CheckIn_Asiento")))
                    throw new NegocioException_GV42("Uno de los asientos elegidos ya fue tomado por otro pasajero. Volvé a elegir el asiento.", ex);
                throw;
            }
        }

        // ¿El tramo tiene algún check-in ya hecho? (entonces ese vuelo ya no se puede cambiar)
        public bool TieneCheckInRealizado(int idReserva, int tramo)
        {
            object r = _acceso.leerEscalar(
                "SELECT COUNT(1) FROM CheckIn WHERE IdReserva = @Id AND Tramo = @Tramo AND IdEstadoCheckIn = @Realizado",
                new[] { new SqlParameter("@Id", idReserva), new SqlParameter("@Tramo", tramo),
                        new SqlParameter("@Realizado", (int)EstadoCheckIn_GV42.Realizado) });
            return r != null && Convert.ToInt32(r) > 0;
        }

        #endregion

        // ¿Algún pasajero de la reserva ya hizo el check-in? (entonces ya no se puede cancelar)
        public bool TieneCheckInRealizado(int idReserva)
        {
            object r = _acceso.leerEscalar(
                "SELECT COUNT(1) FROM CheckIn WHERE IdReserva = @Id AND IdEstadoCheckIn = @Realizado",
                new[] { new SqlParameter("@Id", idReserva), new SqlParameter("@Realizado", (int)EstadoCheckIn_GV42.Realizado) });
            return r != null && Convert.ToInt32(r) > 0;
        }

        public List<Pasajero_GV42> ListarPasajeros(int idReserva)
        {
            string query =
                "SELECT P.DNI, P.Nombre, P.Apellido, P.Email, P.Telefono, P.FechaNacimiento, RP.IdTipoPasajero, RP.IdAsistencia " +
                "FROM ReservaPasajero RP INNER JOIN Pasajero P ON P.DNI = RP.DniPasajero " +
                // Cada pasajero tiene una fila por tramo: se listan una sola vez (todos están en la ida).
                "WHERE RP.IdReserva = @Id AND RP.Tramo = 1 ORDER BY P.Apellido, P.Nombre";

            DataTable dt = _acceso.leer(query, new[] { new SqlParameter("@Id", idReserva) });
            var lista = new List<Pasajero_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                var p = new Pasajero_GV42();
                DALUtil_GV42.LlenarPersona(p, r, "");
                p.FechaNacimiento = DALUtil_GV42.FechaNull(r, "FechaNacimiento");
                p.Tipo = (TipoPasajero_GV42)DALUtil_GV42.Int(r, "IdTipoPasajero");
                p.Asistencia = (AsistenciaEspecial_GV42)DALUtil_GV42.Int(r, "IdAsistencia");
                lista.Add(p);
            }
            return lista;
        }

        // Una fila por pasajero y tramo. Asiento queda en null si todavía no tiene (tarifa Light sin
        // asiento elegido, infante, o reserva cancelada que ya liberó sus asientos).
        public List<AsientoPasajero_GV42> ListarAsientosPorPasajero(int idReserva)
        {
            string query =
                "SELECT RP.DniPasajero, RP.Tramo, RP.EquipajeExtra, A.Id, A.IdVuelo, A.Fila, A.Letra, A.NumeroAsiento, A.IdClase, A.Ubicacion, A.EsPreferencial " +
                "FROM ReservaPasajero RP LEFT JOIN Asiento A ON A.Id = RP.IdAsiento " +
                "WHERE RP.IdReserva = @Id ORDER BY RP.Tramo, RP.DniPasajero";

            DataTable dt = _acceso.leer(query, new[] { new SqlParameter("@Id", idReserva) });
            var lista = new List<AsientoPasajero_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                Asiento_GV42 asiento = r["Id"] == DBNull.Value ? null : new Asiento_GV42
                {
                    Id = DALUtil_GV42.Int(r, "Id"),
                    IdVuelo = DALUtil_GV42.Int(r, "IdVuelo"),
                    NumeroAsiento = DALUtil_GV42.Str(r, "NumeroAsiento"),
                    Clase = (ClaseVuelo_GV42)DALUtil_GV42.Int(r, "IdClase"),
                    Ubicacion = DALUtil_GV42.Str(r, "Ubicacion"),
                    EsPreferencial = Convert.ToBoolean(r["EsPreferencial"])
                };
                lista.Add(new AsientoPasajero_GV42(DALUtil_GV42.Str(r, "DniPasajero"), asiento)
                {
                    EquipajeExtra = DALUtil_GV42.Int(r, "EquipajeExtra"),
                    Tramo = DALUtil_GV42.Int(r, "Tramo")
                });
            }
            return lista;
        }

        public List<AdicionalReserva_GV42> ListarAdicionales(int idReserva)
        {
            string query =
                "SELECT RA.Id, RA.Cantidad, RA.CostoUnitario, RA.Tramo, TA.Id AS IdTipo, TA.Nombre AS TipoNombre, TA.Codigo AS TipoCodigo " +
                "FROM ReservaAdicional RA INNER JOIN TipoAdicional TA ON TA.Id = RA.IdTipoAdicional " +
                "WHERE RA.IdReserva = @Id ORDER BY RA.Tramo, RA.Id";

            DataTable dt = _acceso.leer(query, new[] { new SqlParameter("@Id", idReserva) });
            var lista = new List<AdicionalReserva_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                lista.Add(new AdicionalReserva_GV42
                {
                    Id = DALUtil_GV42.Int(r, "Id"),
                    Cantidad = DALUtil_GV42.Int(r, "Cantidad"),
                    CostoUnitario = DALUtil_GV42.Dec(r, "CostoUnitario"),
                    Tramo = DALUtil_GV42.Int(r, "Tramo"),
                    TipoAdicional = new TipoAdicional_GV42
                    {
                        Id = DALUtil_GV42.Int(r, "IdTipo"),
                        Nombre = DALUtil_GV42.Str(r, "TipoNombre"),
                        Codigo = r["TipoCodigo"] == DBNull.Value ? null : DALUtil_GV42.Str(r, "TipoCodigo")
                    }
                });
            }
            return lista;
        }

        #endregion

        #region Métodos privados

        // Vuelo y clase de la vuelta (tramo 2) de la fila de reserva; null si es solo ida.
        private static VueloClase_GV42 BuscarVueloClaseVuelta(DataRow row)
        {
            if (row["IdVueloVuelta"] == DBNull.Value || row["IdClaseVuelta"] == DBNull.Value) return null;
            return new DALVuelo_GV42().BuscarVueloClaseSinFiltro(
                DALUtil_GV42.Int(row, "IdVueloVuelta"), (ClaseVuelo_GV42)DALUtil_GV42.Int(row, "IdClaseVuelta"));
        }

        private Reserva_GV42 MapearListado(DataRow row)
        {
            var cliente = new Pasajero_GV42();
            DALUtil_GV42.LlenarPersona(cliente, row, "Cli");

            var reserva = new Reserva_GV42
            {
                Id = DALUtil_GV42.Int(row, "IdReserva"),
                NumeroReserva = DALUtil_GV42.Str(row, "NumeroReserva"),
                Cliente = cliente,
                VueloClase = DALUtil_GV42.MapearVueloClase(row),
                VueloClaseVuelta = BuscarVueloClaseVuelta(row),
                TipoViaje = (TipoViaje_GV42)DALUtil_GV42.Int(row, "IdTipoViaje"),
                FechaRegreso = DALUtil_GV42.FechaNull(row, "FechaRegreso"),
                ImporteBase = DALUtil_GV42.Dec(row, "ImporteBase"),
                SubtotalAdicionales = DALUtil_GV42.Dec(row, "SubtotalAdicionales"),
                Impuestos = DALUtil_GV42.Dec(row, "Impuestos"),
                ImporteTotal = DALUtil_GV42.Dec(row, "ImporteTotal"),
                Estado = (EstadoReserva_GV42)DALUtil_GV42.Int(row, "IdEstadoReserva"),
                FechaRealizacion = DALUtil_GV42.Fecha(row, "FechaRealizacion"),
                LoginVendedor = DALUtil_GV42.Str(row, "LoginVendedor"),
                FechaCancelacion = DALUtil_GV42.FechaNull(row, "FechaCancelacion"),
                MontoPenalidadCancelacion = row["MontoPenalidadCancelacion"] == DBNull.Value ? (decimal?)null : DALUtil_GV42.Dec(row, "MontoPenalidadCancelacion")
            };
            LlenarDatosComunes(reserva, row);
            // En los listados se necesita saber si hay un reembolso pendiente (columna y botón).
            if (reserva.Estado == EstadoReserva_GV42.Cancelada && !reserva.VencidaSinPago)
                reserva.Reembolso = BuscarReembolso(reserva.Id);
            return reserva;
        }

        // Vencimiento, tarifa, canal y cancelación: columnas que traen tanto el listado como el detalle.
        private static void LlenarDatosComunes(Reserva_GV42 reserva, DataRow row)
        {
            reserva.FechaVencimiento = DALUtil_GV42.FechaNull(row, "FechaVencimiento");
            reserva.VencidaSinPago = row["VencidaSinPago"] != DBNull.Value && Convert.ToBoolean(row["VencidaSinPago"]);
            reserva.CanalVenta = (CanalVenta_GV42)DALUtil_GV42.Int(row, "IdCanalVenta");
            reserva.FechaCancelacion = DALUtil_GV42.FechaNull(row, "FechaCancelacion");
            reserva.MontoPenalidadCancelacion = row["MontoPenalidadCancelacion"] == DBNull.Value ? (decimal?)null : DALUtil_GV42.Dec(row, "MontoPenalidadCancelacion");
            if (row["IdTarifa"] != DBNull.Value)
                reserva.Tarifa = new DALTarifa_GV42().BuscarPorId(DALUtil_GV42.Int(row, "IdTarifa"));
        }

        #endregion
    }
}
