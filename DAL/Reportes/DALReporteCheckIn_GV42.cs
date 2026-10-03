using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    // Datos del reporte de check-in (RFN 2). Solo lectura.
    // Una fila por pasajero y por tramo: cada tramo es un vuelo distinto con su propio check-in.
    // Solo figuran las reservas confirmadas (son las que generan check-in); los infantes no tienen
    // check-in propio (viajan en brazos) y por eso no aparecen.
    public class DALReporteCheckIn_GV42
    {
        #region Campos

        private readonly Acceso _acceso = Acceso.Instancia;

        #endregion

        #region Métodos públicos

        public List<ReporteCheckIn_GV42> Listar(FiltroReporteCheckIn_GV42 filtro)
        {
            if (filtro == null) filtro = new FiltroReporteCheckIn_GV42();

            string query =
                "SELECT CI.Id, CI.IdEstadoCheckIn, CI.FechaHoraCheckIn," +
                "       R.NumeroReserva, CASE WHEN CI.Tramo = 2 THEN R.IdClaseVuelta ELSE R.IdClase END AS IdClase," +
                "       P.DNI, P.Nombre, P.Apellido," +
                "       V.CodigoVuelo, V.FechaHoraSalida, V.PuertaEmbarque AS PuertaVuelo," +
                "       O.CodigoIata AS OrigenIata, O.Ciudad AS OrigenCiudad," +
                "       D.CodigoIata AS DestinoIata, D.Ciudad AS DestinoCiudad," +
                "       A.NumeroAsiento, A.Ubicacion," +
                "       T.NumeroTarjeta, T.PuertaEmbarque AS PuertaTarjeta, T.HoraLimiteEmbarque" +
                " FROM CheckIn CI" +
                " INNER JOIN Reserva R ON R.Id = CI.IdReserva" +
                " INNER JOIN Pasajero P ON P.DNI = CI.DniPasajero" +
                " INNER JOIN Vuelo V ON V.Id = CASE WHEN CI.Tramo = 2 THEN R.IdVueloVuelta ELSE R.IdVuelo END" +
                " INNER JOIN Aeropuerto O ON O.Id = V.IdOrigen" +
                " INNER JOIN Aeropuerto D ON D.Id = V.IdDestino" +
                " LEFT JOIN Asiento A ON A.Id = CI.IdAsiento" +
                " LEFT JOIN TarjetaEmbarque T ON T.IdCheckIn = CI.Id" +
                " WHERE R.IdEstadoReserva = @Confirmada" + ArmarWhere(filtro) +
                " ORDER BY V.FechaHoraSalida, V.CodigoVuelo, P.Apellido, P.Nombre";

            DataTable dt = _acceso.leer(query, Parametros(filtro));

            var lista = new List<ReporteCheckIn_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                var fila = new ReporteCheckIn_GV42
                {
                    IdCheckIn = DALUtil_GV42.Int(r, "Id"),
                    Estado = (EstadoCheckIn_GV42)DALUtil_GV42.Int(r, "IdEstadoCheckIn"),
                    FechaHoraCheckIn = DALUtil_GV42.FechaNull(r, "FechaHoraCheckIn"),
                    NumeroReserva = DALUtil_GV42.Str(r, "NumeroReserva"),
                    Clase = (ClaseVuelo_GV42)DALUtil_GV42.Int(r, "IdClase"),
                    PasajeroDni = DALUtil_GV42.Str(r, "DNI"),
                    PasajeroNombre = DALUtil_GV42.Str(r, "Nombre"),
                    PasajeroApellido = DALUtil_GV42.Str(r, "Apellido"),
                    CodigoVuelo = DALUtil_GV42.Str(r, "CodigoVuelo"),
                    FechaHoraSalida = DALUtil_GV42.Fecha(r, "FechaHoraSalida"),
                    OrigenIata = DALUtil_GV42.Str(r, "OrigenIata"),
                    OrigenCiudad = DALUtil_GV42.Str(r, "OrigenCiudad"),
                    DestinoIata = DALUtil_GV42.Str(r, "DestinoIata"),
                    DestinoCiudad = DALUtil_GV42.Str(r, "DestinoCiudad"),
                    NumeroAsiento = r["NumeroAsiento"] == DBNull.Value ? null : DALUtil_GV42.Str(r, "NumeroAsiento"),
                    Ubicacion = r["Ubicacion"] == DBNull.Value ? null : DALUtil_GV42.Str(r, "Ubicacion"),
                    NumeroTarjeta = r["NumeroTarjeta"] == DBNull.Value ? null : DALUtil_GV42.Str(r, "NumeroTarjeta"),
                    // La puerta y la hora límite quedan fijas en la tarjeta al emitirla; antes de eso
                    // se informa la puerta del vuelo (la hora límite la calcula la BLL).
                    PuertaEmbarque = r["PuertaTarjeta"] != DBNull.Value ? DALUtil_GV42.Str(r, "PuertaTarjeta")
                                   : r["PuertaVuelo"] != DBNull.Value ? DALUtil_GV42.Str(r, "PuertaVuelo") : null
                };
                if (r["HoraLimiteEmbarque"] != DBNull.Value) fila.HoraLimiteEmbarque = DALUtil_GV42.Fecha(r, "HoraLimiteEmbarque");
                lista.Add(fila);
            }
            return lista;
        }

        #endregion

        #region Métodos privados

        private static string ArmarWhere(FiltroReporteCheckIn_GV42 f)
        {
            var sb = new StringBuilder();
            if (f.FechaDesde.HasValue) sb.Append(" AND V.FechaHoraSalida >= @Desde");
            if (f.FechaHasta.HasValue) sb.Append(" AND V.FechaHoraSalida < @Hasta");
            if (!string.IsNullOrWhiteSpace(f.CodigoVuelo)) sb.Append(" AND V.CodigoVuelo LIKE @Vuelo");
            if (f.Estado.HasValue) sb.Append(" AND CI.IdEstadoCheckIn = @Estado");
            if (!string.IsNullOrWhiteSpace(f.Pasajero))
                sb.Append(" AND (P.DNI LIKE @Pasajero OR P.Nombre LIKE @Pasajero OR P.Apellido LIKE @Pasajero" +
                          " OR (P.Nombre + ' ' + P.Apellido) LIKE @Pasajero OR (P.Apellido + ' ' + P.Nombre) LIKE @Pasajero)");
            return sb.ToString();
        }

        private static SqlParameter[] Parametros(FiltroReporteCheckIn_GV42 f)
        {
            var p = new List<SqlParameter> { new SqlParameter("@Confirmada", (int)EstadoReserva_GV42.Confirmada) };
            if (f.FechaDesde.HasValue) p.Add(new SqlParameter("@Desde", SqlDbType.DateTime2) { Value = f.FechaDesde.Value.Date });
            if (f.FechaHasta.HasValue) p.Add(new SqlParameter("@Hasta", SqlDbType.DateTime2) { Value = f.FechaHasta.Value.Date.AddDays(1) });
            if (!string.IsNullOrWhiteSpace(f.CodigoVuelo)) p.Add(new SqlParameter("@Vuelo", "%" + Escapar(f.CodigoVuelo.Trim()) + "%"));
            if (f.Estado.HasValue) p.Add(new SqlParameter("@Estado", (int)f.Estado.Value));
            if (!string.IsNullOrWhiteSpace(f.Pasajero)) p.Add(new SqlParameter("@Pasajero", "%" + Escapar(f.Pasajero.Trim()) + "%"));
            return p.ToArray();
        }

        // Los comodines de LIKE escritos por el usuario se buscan en forma literal.
        private static string Escapar(string texto)
        {
            return texto.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");
        }

        #endregion
    }
}
