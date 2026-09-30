using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    // Datos del reporte de reservas (RFN 1). Solo lectura.
    public class DALReporteReserva_GV42
    {
        private readonly Acceso _acceso = Acceso.Instancia;

        private const string FROM_RESERVAS =
            " FROM Reserva R" +
            " INNER JOIN Pasajero C ON C.DNI = R.DniCliente" +
            " INNER JOIN Vuelo V ON V.Id = R.IdVuelo" +
            " INNER JOIN Aeropuerto O ON O.Id = V.IdOrigen" +
            " INNER JOIN Aeropuerto D ON D.Id = V.IdDestino";

        public List<ReporteReserva_GV42> Listar(FiltroReporteReservas_GV42 filtro)
        {
            if (filtro == null) filtro = new FiltroReporteReservas_GV42();
            string where = ArmarWhere(filtro);

            string query =
                "SELECT R.Id, R.NumeroReserva, R.FechaRealizacion, R.IdClase, R.CantidadPasajeros," +
                "       R.ImporteBase, R.SubtotalAdicionales, R.Impuestos, R.ImporteTotal, R.IdEstadoReserva," +
                "       C.DNI, C.Nombre, C.Apellido, C.Email, C.Telefono," +
                "       V.CodigoVuelo, V.FechaHoraSalida, V.FechaHoraLlegada," +
                "       O.CodigoIata AS OrigenIata, O.Ciudad AS OrigenCiudad," +
                "       D.CodigoIata AS DestinoIata, D.Ciudad AS DestinoCiudad" +
                FROM_RESERVAS + where +
                " ORDER BY R.FechaRealizacion DESC, R.Id DESC";

            DataTable dt = _acceso.leer(query, Parametros(filtro));

            var lista = new List<ReporteReserva_GV42>();
            var porId = new Dictionary<int, ReporteReserva_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                var fila = new ReporteReserva_GV42
                {
                    IdReserva = DALUtil_GV42.Int(r, "Id"),
                    NumeroReserva = DALUtil_GV42.Str(r, "NumeroReserva"),
                    FechaRealizacion = DALUtil_GV42.Fecha(r, "FechaRealizacion"),
                    PasajeroDni = DALUtil_GV42.Str(r, "DNI"),
                    PasajeroNombre = DALUtil_GV42.Str(r, "Nombre"),
                    PasajeroApellido = DALUtil_GV42.Str(r, "Apellido"),
                    Email = DALUtil_GV42.Descifrar(DALUtil_GV42.Str(r, "Email")),
                    Telefono = DALUtil_GV42.Str(r, "Telefono"),
                    CodigoVuelo = DALUtil_GV42.Str(r, "CodigoVuelo"),
                    OrigenIata = DALUtil_GV42.Str(r, "OrigenIata"),
                    OrigenCiudad = DALUtil_GV42.Str(r, "OrigenCiudad"),
                    DestinoIata = DALUtil_GV42.Str(r, "DestinoIata"),
                    DestinoCiudad = DALUtil_GV42.Str(r, "DestinoCiudad"),
                    FechaHoraSalida = DALUtil_GV42.Fecha(r, "FechaHoraSalida"),
                    FechaHoraLlegada = DALUtil_GV42.Fecha(r, "FechaHoraLlegada"),
                    Clase = (ClaseVuelo_GV42)DALUtil_GV42.Int(r, "IdClase"),
                    CantidadPasajeros = DALUtil_GV42.Int(r, "CantidadPasajeros"),
                    ImporteBase = DALUtil_GV42.Dec(r, "ImporteBase"),
                    SubtotalAdicionales = DALUtil_GV42.Dec(r, "SubtotalAdicionales"),
                    Impuestos = DALUtil_GV42.Dec(r, "Impuestos"),
                    ImporteTotal = DALUtil_GV42.Dec(r, "ImporteTotal"),
                    Estado = (EstadoReserva_GV42)DALUtil_GV42.Int(r, "IdEstadoReserva")
                };
                lista.Add(fila);
                porId[fila.IdReserva] = fila;
            }

            if (lista.Count > 0) CargarAdicionales(filtro, where, porId);
            return lista;
        }

        // Adicionales de todas las reservas del reporte en una sola consulta (mismos filtros).
        private void CargarAdicionales(FiltroReporteReservas_GV42 filtro, string where,
                                       Dictionary<int, ReporteReserva_GV42> porId)
        {
            string query =
                "SELECT RA.IdReserva, TA.Nombre AS Tipo, RA.Cantidad, RA.CostoUnitario, RA.Subtotal" +
                " FROM ReservaAdicional RA" +
                " INNER JOIN TipoAdicional TA ON TA.Id = RA.IdTipoAdicional" +
                " WHERE RA.IdReserva IN (SELECT R.Id" + FROM_RESERVAS + where + ")" +
                " ORDER BY RA.IdReserva, RA.Id";

            DataTable dt = _acceso.leer(query, Parametros(filtro));
            foreach (DataRow r in dt.Rows)
            {
                ReporteReserva_GV42 fila;
                if (!porId.TryGetValue(DALUtil_GV42.Int(r, "IdReserva"), out fila)) continue;
                fila.Adicionales.Add(new AdicionalReporte_GV42
                {
                    Tipo = DALUtil_GV42.Str(r, "Tipo"),
                    Cantidad = DALUtil_GV42.Int(r, "Cantidad"),
                    CostoUnitario = DALUtil_GV42.Dec(r, "CostoUnitario"),
                    Subtotal = DALUtil_GV42.Dec(r, "Subtotal")
                });
            }
        }

        private static string ArmarWhere(FiltroReporteReservas_GV42 f)
        {
            var sb = new StringBuilder();
            string columnaFecha = f.TipoFecha == FechaReporte_GV42.Salida ? "V.FechaHoraSalida" : "R.FechaRealizacion";

            if (f.FechaDesde.HasValue) sb.Append(" AND " + columnaFecha + " >= @Desde");
            if (f.FechaHasta.HasValue) sb.Append(" AND " + columnaFecha + " < @Hasta");
            if (!string.IsNullOrWhiteSpace(f.CodigoVuelo)) sb.Append(" AND V.CodigoVuelo LIKE @Vuelo");
            if (f.Clase.HasValue) sb.Append(" AND R.IdClase = @Clase");
            if (f.Estado.HasValue) sb.Append(" AND R.IdEstadoReserva = @Estado");
            if (!string.IsNullOrWhiteSpace(f.Pasajero))
            {
                // Titular o cualquiera de los pasajeros de la reserva (el email está cifrado,
                // por eso se busca por DNI, nombre y apellido).
                sb.Append(" AND (C.DNI LIKE @Pasajero OR C.Nombre LIKE @Pasajero OR C.Apellido LIKE @Pasajero" +
                          " OR (C.Nombre + ' ' + C.Apellido) LIKE @Pasajero OR (C.Apellido + ' ' + C.Nombre) LIKE @Pasajero" +
                          " OR EXISTS (SELECT 1 FROM ReservaPasajero RP INNER JOIN Pasajero P ON P.DNI = RP.DniPasajero" +
                          "            WHERE RP.IdReserva = R.Id AND (P.DNI LIKE @Pasajero OR P.Nombre LIKE @Pasajero" +
                          "            OR P.Apellido LIKE @Pasajero OR (P.Nombre + ' ' + P.Apellido) LIKE @Pasajero" +
                          "            OR (P.Apellido + ' ' + P.Nombre) LIKE @Pasajero)))");
            }
            return sb.Length == 0 ? string.Empty : " WHERE 1 = 1" + sb;
        }

        // Se crean parámetros nuevos en cada llamada: un SqlParameter no puede pertenecer a dos comandos.
        private static SqlParameter[] Parametros(FiltroReporteReservas_GV42 f)
        {
            var p = new List<SqlParameter>();
            if (f.FechaDesde.HasValue) p.Add(new SqlParameter("@Desde", SqlDbType.DateTime2) { Value = f.FechaDesde.Value.Date });
            if (f.FechaHasta.HasValue) p.Add(new SqlParameter("@Hasta", SqlDbType.DateTime2) { Value = f.FechaHasta.Value.Date.AddDays(1) });
            if (!string.IsNullOrWhiteSpace(f.CodigoVuelo)) p.Add(new SqlParameter("@Vuelo", "%" + Escapar(f.CodigoVuelo.Trim()) + "%"));
            if (f.Clase.HasValue) p.Add(new SqlParameter("@Clase", (int)f.Clase.Value));
            if (f.Estado.HasValue) p.Add(new SqlParameter("@Estado", (int)f.Estado.Value));
            if (!string.IsNullOrWhiteSpace(f.Pasajero)) p.Add(new SqlParameter("@Pasajero", "%" + Escapar(f.Pasajero.Trim()) + "%"));
            return p.Count == 0 ? null : p.ToArray();
        }

        // Los comodines de LIKE escritos por el usuario se buscan en forma literal.
        private static string Escapar(string texto)
        {
            return texto.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");
        }
    }
}
