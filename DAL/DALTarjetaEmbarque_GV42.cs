using BE;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALTarjetaEmbarque_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        #endregion

        #region Constructor

        public DALTarjetaEmbarque_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Métodos públicos

        // Emite la tarjeta de embarque. Solo se emite si el check-in sigue pendiente y ya tiene asiento.
        public TarjetaEmbarque_GV42 Generar(int idCheckIn, string puertaEmbarque, DateTime horaLimiteEmbarque)
        {
            int filas = _acceso.escribir(
                "INSERT INTO TarjetaEmbarque (IdCheckIn, PuertaEmbarque, HoraLimiteEmbarque) " +
                "SELECT @IdCheckIn, @Puerta, @HoraLimite " +
                "WHERE EXISTS (SELECT 1 FROM CheckIn WHERE Id = @IdCheckIn AND IdAsiento IS NOT NULL AND IdEstadoCheckIn = @Pendiente)",
                new[] {
                    new SqlParameter("@IdCheckIn",  idCheckIn),
                    new SqlParameter("@Puerta",     puertaEmbarque),
                    new SqlParameter("@HoraLimite", horaLimiteEmbarque),
                    new SqlParameter("@Pendiente",  (int)EstadoCheckIn_GV42.Pendiente)
                });

            if (filas == 0)
                throw new NegocioException_GV42("No se puede generar la tarjeta de embarque: el check-in no tiene asiento asignado o ya fue realizado.");

            return BuscarPorCheckIn(idCheckIn);
        }

        // Devuelve null si todavía no se emitió la tarjeta.
        public TarjetaEmbarque_GV42 BuscarPorCheckIn(int idCheckIn)
        {
            string query =
                "SELECT T.Id, T.NumeroTarjeta, T.PuertaEmbarque, T.HoraLimiteEmbarque, T.FechaHoraEmision, " +
                "       R.NumeroReserva, R.IdClase, P.DNI, P.Nombre, P.Apellido, " +
                "       V.CodigoVuelo, V.FechaHoraSalida, " +
                "       O.Ciudad AS OrigenCiudad, O.CodigoIata AS OrigenIata, " +
                "       D.Ciudad AS DestinoCiudad, D.CodigoIata AS DestinoIata, " +
                "       A.NumeroAsiento " +
                "FROM TarjetaEmbarque T " +
                "INNER JOIN CheckIn CI ON CI.Id = T.IdCheckIn " +
                "INNER JOIN Reserva R ON R.Id = CI.IdReserva " +
                "INNER JOIN Pasajero P ON P.DNI = CI.DniPasajero " +
                "INNER JOIN Vuelo V ON V.Id = R.IdVuelo " +
                "INNER JOIN Aeropuerto O ON O.Id = V.IdOrigen " +
                "INNER JOIN Aeropuerto D ON D.Id = V.IdDestino " +
                "INNER JOIN Asiento A ON A.Id = CI.IdAsiento " +
                "WHERE T.IdCheckIn = @IdCheckIn";

            DataTable dt = _acceso.leer(query, new[] { new SqlParameter("@IdCheckIn", idCheckIn) });
            if (dt.Rows.Count == 0) return null;

            DataRow r = dt.Rows[0];
            return new TarjetaEmbarque_GV42
            {
                Id = DALUtil_GV42.Int(r, "Id"),
                NumeroTarjeta = DALUtil_GV42.Str(r, "NumeroTarjeta"),
                NumeroReserva = DALUtil_GV42.Str(r, "NumeroReserva"),
                NombreApellido = (DALUtil_GV42.Str(r, "Nombre") + " " + DALUtil_GV42.Str(r, "Apellido")).Trim(),
                DNI = DALUtil_GV42.Str(r, "DNI"),
                CodigoVuelo = DALUtil_GV42.Str(r, "CodigoVuelo"),
                Origen = DALUtil_GV42.Str(r, "OrigenCiudad") + " (" + DALUtil_GV42.Str(r, "OrigenIata") + ")",
                Destino = DALUtil_GV42.Str(r, "DestinoCiudad") + " (" + DALUtil_GV42.Str(r, "DestinoIata") + ")",
                FechaHoraSalida = DALUtil_GV42.Fecha(r, "FechaHoraSalida"),
                NumeroAsiento = DALUtil_GV42.Str(r, "NumeroAsiento"),
                Clase = (ClaseVuelo_GV42)DALUtil_GV42.Int(r, "IdClase"),
                PuertaEmbarque = DALUtil_GV42.Str(r, "PuertaEmbarque"),
                HoraLimiteEmbarque = DALUtil_GV42.Fecha(r, "HoraLimiteEmbarque"),
                FechaHoraEmision = DALUtil_GV42.Fecha(r, "FechaHoraEmision")
            };
        }

        #endregion
    }
}
