using BE;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALPago_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        #endregion

        #region Constructor

        public DALPago_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Métodos públicos

        // En una sola transacción: registra el pago, pasa la reserva a "Confirmada",
        // emite un boleto por pasajero y por tramo (ida / vuelta) y deja creado el check-in (Pendiente)
        // de cada pasajero en cada tramo.
        public Pago_GV42 RegistrarPagoYConfirmar(Pago_GV42 pago)
        {
            try
            {
                return _acceso.EjecutarEnTransaccion(tx =>
                {
                    int filas = _acceso.escribir(tx,
                        "UPDATE R SET IdEstadoReserva = @Confirmada FROM Reserva R " +
                        "WHERE R.Id = @IdReserva AND R.IdEstadoReserva = @Pendiente " +
                        // Una reserva cuyo plazo de pago ya venció no se puede pagar.
                        "AND (R.FechaVencimiento IS NULL OR R.FechaVencimiento > GETDATE()) " +
                        // No se confirma el pago de un vuelo que ya salió.
                        "AND EXISTS (SELECT 1 FROM Vuelo V WHERE V.Id = R.IdVuelo AND V.FechaHoraSalida > GETDATE())",
                        new[] {
                            new SqlParameter("@Confirmada", (int)EstadoReserva_GV42.Confirmada),
                            new SqlParameter("@Pendiente",  (int)EstadoReserva_GV42.PendienteDePago),
                            new SqlParameter("@IdReserva",  pago.IdReserva)
                        });

                    if (filas == 0)
                        throw new NegocioException_GV42("La reserva no está pendiente de pago, venció el plazo para pagarla o el vuelo ya salió.");

                    object idObj = _acceso.leerEscalar(tx,
                        "INSERT INTO Pago (IdReserva, ImporteTotalAbonado, IdMedioPago, NumeroTransaccion, LoginVendedor) " +
                        "VALUES (@IdReserva, @Importe, @IdMedio, @NumTx, @Login); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new[] {
                            new SqlParameter("@IdReserva", pago.IdReserva),
                            new SqlParameter("@Importe",   pago.ImporteTotalAbonado),
                            new SqlParameter("@IdMedio",   (int)pago.MedioPago),
                            new SqlParameter("@NumTx",     pago.NumeroTransaccion),
                            new SqlParameter("@Login",     pago.LoginVendedor)
                        });
                    pago.Id = Convert.ToInt32(idObj);

                    SqlParameter[] pReserva = { new SqlParameter("@IdReserva", pago.IdReserva) };

                    _acceso.escribir(tx,
                        "INSERT INTO Boleto (IdReserva, DniPasajero, Tramo) " +
                        "SELECT IdReserva, DniPasajero, Tramo FROM ReservaPasajero WHERE IdReserva = @IdReserva " +
                        "ORDER BY Tramo, DniPasajero",
                        pReserva);

                    _acceso.escribir(tx,
                        // El check-in arranca con el asiento que el pasajero eligió al reservar
                        // (antes nacía sin asiento y el check-in podía darle uno que ya era de otro).
                        "INSERT INTO CheckIn (IdReserva, DniPasajero, Tramo, IdAsiento) " +
                        // Los infantes viajan en brazos: no tienen asiento ni check-in propio.
                        "SELECT IdReserva, DniPasajero, Tramo, IdAsiento FROM ReservaPasajero WHERE IdReserva = @IdReserva AND IdTipoPasajero <> 3",
                        new[] { new SqlParameter("@IdReserva", pago.IdReserva) });

                    DataTable dt = _acceso.leer(tx,
                        "SELECT FechaHoraPago FROM Pago WHERE Id = @Id",
                        new[] { new SqlParameter("@Id", pago.Id) });
                    pago.FechaHoraPago = DALUtil_GV42.Fecha(dt.Rows[0], "FechaHoraPago");
                    return pago;
                });
            }
            catch (SqlException ex)
            {
                // 2601 / 2627: violación de índice o clave única (número de transacción repetido).
                if (ex.Number == 2601 || ex.Number == 2627)
                    throw new NegocioException_GV42("Ya existe un pago registrado con ese número de transacción.", ex);
                throw;
            }
        }

        // Devuelve null si la reserva todavía no tiene pago.
        public Pago_GV42 BuscarPorReserva(int idReserva)
        {
            string query =
                "SELECT P.Id, P.IdReserva, R.NumeroReserva, P.ImporteTotalAbonado, P.IdMedioPago, " +
                "       P.NumeroTransaccion, P.FechaHoraPago, P.LoginVendedor " +
                "FROM Pago P INNER JOIN Reserva R ON R.Id = P.IdReserva " +
                "WHERE P.IdReserva = @Id";

            DataTable dt = _acceso.leer(query, new[] { new SqlParameter("@Id", idReserva) });
            if (dt.Rows.Count == 0) return null;

            DataRow r = dt.Rows[0];
            return new Pago_GV42
            {
                Id = DALUtil_GV42.Int(r, "Id"),
                IdReserva = DALUtil_GV42.Int(r, "IdReserva"),
                NumeroReserva = DALUtil_GV42.Str(r, "NumeroReserva"),
                ImporteTotalAbonado = DALUtil_GV42.Dec(r, "ImporteTotalAbonado"),
                MedioPago = (MedioPago_GV42)DALUtil_GV42.Int(r, "IdMedioPago"),
                NumeroTransaccion = DALUtil_GV42.Str(r, "NumeroTransaccion"),
                FechaHoraPago = DALUtil_GV42.Fecha(r, "FechaHoraPago"),
                LoginVendedor = DALUtil_GV42.Str(r, "LoginVendedor")
            };
        }

        #endregion
    }
}
