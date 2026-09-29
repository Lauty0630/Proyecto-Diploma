﻿﻿using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALReserva_GV42
    {
        private readonly Acceso _acceso;

        public DALReserva_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        // Guarda la reserva completa (cabecera, pasajeros, adicionales) y descuenta la disponibilidad
        // de asientos, todo en una sola transacción. Devuelve la reserva con Id, NumeroReserva y
        // FechaRealizacion completados.
        public Reserva_GV42 Crear(Reserva_GV42 r)
        {
            try
            {
                return _acceso.EjecutarEnTransaccion(tx =>
                {
                // 1) Reservar los asientos. La condición evita vender de más si dos vendedores compiten.
                int filas = _acceso.escribir(tx,
                    "UPDATE VueloClase SET AsientosReservados = AsientosReservados + @Cant " +
                    "WHERE IdVuelo = @IdVuelo AND IdClase = @IdClase " +
                    "AND (CapacidadAsientos - AsientosReservados) >= @Cant",
                    new[] {
                        new SqlParameter("@Cant",    r.CantidadPasajeros),
                        new SqlParameter("@IdVuelo", r.Vuelo.Id),
                        new SqlParameter("@IdClase", (int)r.Clase)
                    });

                if (filas == 0)
                    throw new NegocioException_GV42("No hay asientos disponibles suficientes en esa clase para la cantidad de pasajeros indicada.");

                // 2) Cabecera de la reserva.
                object idObj = _acceso.leerEscalar(tx,
                    "INSERT INTO Reserva (DniCliente, IdVuelo, IdClase, IdTipoViaje, FechaRegreso, CantidadPasajeros, " +
                    "                     ImporteBase, SubtotalAdicionales, Impuestos, ImporteTotal, IdEstadoReserva, LoginVendedor) " +
                    "VALUES (@DniCliente, @IdVuelo, @IdClase, @IdTipoViaje, @FechaRegreso, @Cantidad, " +
                    "        @ImporteBase, @Subtotal, @Impuestos, @Total, @IdEstado, @Login); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    new[] {
                        new SqlParameter("@DniCliente",  r.Cliente.DNI),
                        new SqlParameter("@IdVuelo",     r.Vuelo.Id),
                        new SqlParameter("@IdClase",     (int)r.Clase),
                        new SqlParameter("@IdTipoViaje", (int)r.TipoViaje),
                        new SqlParameter("@FechaRegreso", DALUtil_GV42.ADb(r.FechaRegreso)),
                        new SqlParameter("@Cantidad",    r.CantidadPasajeros),
                        new SqlParameter("@ImporteBase", r.ImporteBase),
                        new SqlParameter("@Subtotal",    r.SubtotalAdicionales),
                        new SqlParameter("@Impuestos",   r.Impuestos),
                        new SqlParameter("@Total",       r.ImporteTotal),
                        new SqlParameter("@IdEstado",    (int)r.Estado),
                        new SqlParameter("@Login",       r.LoginVendedor)
                    });
                int idReserva = Convert.ToInt32(idObj);

                // 3) Pasajeros: se registran si no existen o se actualizan sus datos, y se vinculan a la reserva.
                foreach (Pasajero_GV42 p in r.Pasajeros)
                {
                    _acceso.escribir(tx,
                        "IF EXISTS (SELECT 1 FROM Pasajero WHERE DNI = @DNI) " +
                        "    UPDATE Pasajero SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Telefono = @Telefono WHERE DNI = @DNI " +
                        "ELSE " +
                        "    INSERT INTO Pasajero (DNI, Nombre, Apellido, Email, Telefono) VALUES (@DNI, @Nombre, @Apellido, @Email, @Telefono)",
                        new[] {
                            new SqlParameter("@DNI",      p.DNI),
                            new SqlParameter("@Nombre",   p.Nombre),
                            new SqlParameter("@Apellido", p.Apellido),
                            new SqlParameter("@Email",    DALUtil_GV42.Cifrar(p.Email)),
                            new SqlParameter("@Telefono", p.Telefono)
                        });

                    _acceso.escribir(tx,
                        "INSERT INTO ReservaPasajero (IdReserva, DniPasajero) VALUES (@IdReserva, @DNI)",
                        new[] {
                            new SqlParameter("@IdReserva", idReserva),
                            new SqlParameter("@DNI",       p.DNI)
                        });
                }

                // 4) Servicios adicionales.
                foreach (AdicionalReserva_GV42 a in r.Adicionales)
                {
                    _acceso.escribir(tx,
                        "INSERT INTO ReservaAdicional (IdReserva, IdTipoAdicional, Cantidad, CostoUnitario) " +
                        "VALUES (@IdReserva, @IdTipo, @Cantidad, @Costo)",
                        new[] {
                            new SqlParameter("@IdReserva", idReserva),
                            new SqlParameter("@IdTipo",    a.TipoAdicional.Id),
                            new SqlParameter("@Cantidad",  a.Cantidad),
                            new SqlParameter("@Costo",     a.CostoUnitario)
                        });
                }

                // 3.5) Asiento elegido por cada pasajero (selección estilo cine). El índice único
                // filtrado UX_ReservaPasajero_Asiento asegura que dos pasajeros no se queden con el mismo
                // asiento aunque compitan al mismo tiempo.
                foreach (AsientoPasajero_GV42 ap in r.AsientosPorPasajero)
                {
                    _acceso.escribir(tx,
                        "UPDATE ReservaPasajero SET IdAsiento = @IdAsiento " +
                        "WHERE IdReserva = @IdReserva AND DniPasajero = @DNI",
                        new[] {
                            new SqlParameter("@IdAsiento", ap.Asiento.Id),
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
                if (ex.Number == 2601 || ex.Number == 2627)
                    throw new NegocioException_GV42("Uno de los asientos elegidos ya fue tomado por otro pasajero. Volvé a elegir el asiento.", ex);
                throw;
            }
        }

        public Reserva_GV42 BuscarPorNumero(string numeroReserva)
        {
            string query =
                "SELECT R.Id AS IdReserva, R.NumeroReserva, R.IdTipoViaje, R.FechaRegreso, " +
                "       R.ImporteBase, R.SubtotalAdicionales, R.Impuestos, R.ImporteTotal, " +
                "       R.IdEstadoReserva, R.FechaRealizacion, R.LoginVendedor, " +
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

            reserva.Pasajeros = ListarPasajeros(reserva.Id);
            reserva.AsientosPorPasajero = ListarAsientosPorPasajero(reserva.Id);
            reserva.Adicionales = ListarAdicionales(reserva.Id);
            reserva.Pago = new DALPago_GV42().BuscarPorReserva(reserva.Id);
            return reserva;
        }

        private const string SELECT_LISTADO =
            "SELECT R.Id AS IdReserva, R.NumeroReserva, R.IdTipoViaje, R.FechaRegreso, " +
            "       R.ImporteBase, R.SubtotalAdicionales, R.Impuestos, R.ImporteTotal, " +
            "       R.IdEstadoReserva, R.FechaRealizacion, R.LoginVendedor, " +
            "       R.FechaCancelacion, R.MontoPenalidadCancelacion, " +
            "       C.DNI AS CliDNI, C.Nombre AS CliNombre, C.Apellido AS CliApellido, " +
            "       C.Email AS CliEmail, C.Telefono AS CliTelefono, " +
            DALUtil_GV42.COLUMNAS_VUELO_CLASE + " " +
            "FROM Reserva R " +
            "INNER JOIN Pasajero C ON C.DNI = R.DniCliente " +
            "INNER JOIN Vuelo V ON V.Id = R.IdVuelo " +
            "INNER JOIN VueloClase VC ON VC.IdVuelo = R.IdVuelo AND VC.IdClase = R.IdClase" +
            DALUtil_GV42.JOINS_VUELO;

        private Reserva_GV42 MapearListado(DataRow row)
        {
            var cliente = new Pasajero_GV42();
            DALUtil_GV42.LlenarPersona(cliente, row, "Cli");

            return new Reserva_GV42
            {
                Id = DALUtil_GV42.Int(row, "IdReserva"),
                NumeroReserva = DALUtil_GV42.Str(row, "NumeroReserva"),
                Cliente = cliente,
                VueloClase = DALUtil_GV42.MapearVueloClase(row),
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
        }

        // "Mis reservas" del cliente autogestionado.
        public List<Reserva_GV42> ListarPorCliente(string dni)
        {
            string query = SELECT_LISTADO + " WHERE R.DniCliente = @DNI ORDER BY R.FechaRealizacion DESC";
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
                p = new[] { new SqlParameter("@Texto", "%" + textoLibre.Trim() + "%") };
            }
            query += " ORDER BY R.FechaRealizacion DESC";

            DataTable dt = _acceso.leer(query, p);
            var lista = new List<Reserva_GV42>();
            foreach (DataRow r in dt.Rows) lista.Add(MapearListado(r));
            return lista;
        }

        // Cancela la reserva, libera los asientos que tenían sus pasajeros y descuenta el cupo
        // ocupado del vuelo, todo en una sola transacción.
        public Reserva_GV42 Cancelar(int idReserva, decimal montoPenalidad)
        {
            return _acceso.EjecutarEnTransaccion(tx =>
            {
                DataTable dtRes = _acceso.leer(tx,
                    "SELECT IdVuelo, IdClase, CantidadPasajeros, IdEstadoReserva FROM Reserva WHERE Id = @Id",
                    new[] { new SqlParameter("@Id", idReserva) });
                if (dtRes.Rows.Count == 0)
                    throw new NegocioException_GV42("La reserva no existe.");
                if (DALUtil_GV42.Int(dtRes.Rows[0], "IdEstadoReserva") == (int)EstadoReserva_GV42.Cancelada)
                    throw new NegocioException_GV42("La reserva ya estaba cancelada.");

                int idVuelo = DALUtil_GV42.Int(dtRes.Rows[0], "IdVuelo");
                int idClase = DALUtil_GV42.Int(dtRes.Rows[0], "IdClase");
                int cantidad = DALUtil_GV42.Int(dtRes.Rows[0], "CantidadPasajeros");

                _acceso.escribir(tx,
                    "UPDATE Reserva SET IdEstadoReserva = @Cancelada, FechaCancelacion = GETDATE(), MontoPenalidadCancelacion = @Monto " +
                    "WHERE Id = @Id",
                    new[] {
                        new SqlParameter("@Cancelada", (int)EstadoReserva_GV42.Cancelada),
                        new SqlParameter("@Monto",     montoPenalidad),
                        new SqlParameter("@Id",        idReserva)
                    });

                // Libera los asientos que tenían los pasajeros de esta reserva.
                _acceso.escribir(tx,
                    "UPDATE ReservaPasajero SET IdAsiento = NULL WHERE IdReserva = @Id",
                    new[] { new SqlParameter("@Id", idReserva) });

                _acceso.escribir(tx,
                    "UPDATE VueloClase SET AsientosReservados = AsientosReservados - @Cant " +
                    "WHERE IdVuelo = @IdVuelo AND IdClase = @IdClase",
                    new[] {
                        new SqlParameter("@Cant",    cantidad),
                        new SqlParameter("@IdVuelo", idVuelo),
                        new SqlParameter("@IdClase", idClase)
                    });

                DataTable dt = _acceso.leer(tx, SELECT_LISTADO + " WHERE R.Id = @Id", new[] { new SqlParameter("@Id", idReserva) });
                return MapearListado(dt.Rows[0]);
            });
        }

        public List<Pasajero_GV42> ListarPasajeros(int idReserva)
        {
            string query =
                "SELECT P.DNI, P.Nombre, P.Apellido, P.Email, P.Telefono " +
                "FROM ReservaPasajero RP INNER JOIN Pasajero P ON P.DNI = RP.DniPasajero " +
                "WHERE RP.IdReserva = @Id ORDER BY P.Apellido, P.Nombre";

            DataTable dt = _acceso.leer(query, new[] { new SqlParameter("@Id", idReserva) });
            var lista = new List<Pasajero_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                var p = new Pasajero_GV42();
                DALUtil_GV42.LlenarPersona(p, r, "");
                lista.Add(p);
            }
            return lista;
        }

        public List<AsientoPasajero_GV42> ListarAsientosPorPasajero(int idReserva)
        {
            string query =
                "SELECT RP.DniPasajero, A.Id, A.IdVuelo, A.Fila, A.Letra, A.NumeroAsiento, A.IdClase, A.Ubicacion " +
                "FROM ReservaPasajero RP INNER JOIN Asiento A ON A.Id = RP.IdAsiento " +
                "WHERE RP.IdReserva = @Id";

            DataTable dt = _acceso.leer(query, new[] { new SqlParameter("@Id", idReserva) });
            var lista = new List<AsientoPasajero_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                var asiento = new Asiento_GV42
                {
                    Id = DALUtil_GV42.Int(r, "Id"),
                    IdVuelo = DALUtil_GV42.Int(r, "IdVuelo"),
                    NumeroAsiento = DALUtil_GV42.Str(r, "NumeroAsiento"),
                    Clase = (ClaseVuelo_GV42)DALUtil_GV42.Int(r, "IdClase"),
                    Ubicacion = DALUtil_GV42.Str(r, "Ubicacion")
                };
                lista.Add(new AsientoPasajero_GV42(DALUtil_GV42.Str(r, "DniPasajero"), asiento));
            }
            return lista;
        }

        public List<AdicionalReserva_GV42> ListarAdicionales(int idReserva)
        {
            string query =
                "SELECT RA.Id, RA.Cantidad, RA.CostoUnitario, TA.Id AS IdTipo, TA.Nombre AS TipoNombre " +
                "FROM ReservaAdicional RA INNER JOIN TipoAdicional TA ON TA.Id = RA.IdTipoAdicional " +
                "WHERE RA.IdReserva = @Id ORDER BY RA.Id";

            DataTable dt = _acceso.leer(query, new[] { new SqlParameter("@Id", idReserva) });
            var lista = new List<AdicionalReserva_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                lista.Add(new AdicionalReserva_GV42
                {
                    Id = DALUtil_GV42.Int(r, "Id"),
                    Cantidad = DALUtil_GV42.Int(r, "Cantidad"),
                    CostoUnitario = DALUtil_GV42.Dec(r, "CostoUnitario"),
                    TipoAdicional = new TipoAdicional_GV42
                    {
                        Id = DALUtil_GV42.Int(r, "IdTipo"),
                        Nombre = DALUtil_GV42.Str(r, "TipoNombre")
                    }
                });
            }
            return lista;
        }
    }
}
