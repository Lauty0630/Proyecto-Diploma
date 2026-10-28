using BE;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // Datos para el reporte inteligente de millas: los vuelos que cada pasajero ya voló
    // (check-in realizado), con la ruta y la clase. Integra CheckIn, Reserva, Pasajero, Vuelo y Aeropuerto.
    public class DALReporteMillas_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        #endregion

        #region Constructor

        public DALReporteMillas_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Métodos públicos

        public List<VueloVolado_GV42> ListarVuelosVolados()
        {
            string query =
                "SELECT P.DNI, P.Nombre, P.Apellido, P.Email, P.Telefono," +
                "       V.CodigoVuelo, V.FechaHoraSalida," +
                "       CASE WHEN CI.Tramo = 2 THEN R.IdClaseVuelta ELSE R.IdClase END AS IdClase," +
                "       O.CodigoIata AS OrigenIata, O.Ciudad AS OrigenCiudad, O.Pais AS OrigenPais," +
                "       D.CodigoIata AS DestinoIata, D.Ciudad AS DestinoCiudad, D.Pais AS DestinoPais" +
                " FROM CheckIn CI" +
                " INNER JOIN Reserva R ON R.Id = CI.IdReserva" +
                " INNER JOIN Pasajero P ON P.DNI = CI.DniPasajero" +
                " INNER JOIN Vuelo V ON V.Id = CASE WHEN CI.Tramo = 2 THEN R.IdVueloVuelta ELSE R.IdVuelo END" +
                " INNER JOIN Aeropuerto O ON O.Id = V.IdOrigen" +
                " INNER JOIN Aeropuerto D ON D.Id = V.IdDestino" +
                " WHERE CI.IdEstadoCheckIn = @Realizado AND R.IdEstadoReserva = @Confirmada" +
                " ORDER BY P.DNI, V.FechaHoraSalida";

            DataTable dt = _acceso.leer(query, new[] {
                new SqlParameter("@Realizado", (int)EstadoCheckIn_GV42.Realizado),
                new SqlParameter("@Confirmada", (int)EstadoReserva_GV42.Confirmada)
            });

            var lista = new List<VueloVolado_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                lista.Add(new VueloVolado_GV42
                {
                    Dni = DALUtil_GV42.Str(r, "DNI"),
                    Nombre = DALUtil_GV42.Str(r, "Nombre"),
                    Apellido = DALUtil_GV42.Str(r, "Apellido"),
                    Email = DALUtil_GV42.Descifrar(DALUtil_GV42.Str(r, "Email")),
                    Telefono = DALUtil_GV42.Str(r, "Telefono"),
                    CodigoVuelo = DALUtil_GV42.Str(r, "CodigoVuelo"),
                    Fecha = DALUtil_GV42.Fecha(r, "FechaHoraSalida"),
                    Clase = (ClaseVuelo_GV42)DALUtil_GV42.Int(r, "IdClase"),
                    OrigenIata = DALUtil_GV42.Str(r, "OrigenIata"),
                    OrigenCiudad = DALUtil_GV42.Str(r, "OrigenCiudad"),
                    OrigenPais = DALUtil_GV42.Str(r, "OrigenPais"),
                    DestinoIata = DALUtil_GV42.Str(r, "DestinoIata"),
                    DestinoCiudad = DALUtil_GV42.Str(r, "DestinoCiudad"),
                    DestinoPais = DALUtil_GV42.Str(r, "DestinoPais")
                });
            }
            return lista;
        }

        // Los servicios adicionales de todas las reservas confirmadas, con el cliente de cada reserva.
        // Con esto la BLL arma las compras asociadas ("quienes contratan A también contratan B").
        public List<ContratacionAdicional_GV42> ListarContrataciones()
        {
            string query =
                "SELECT DISTINCT RA.IdReserva, RA.IdTipoAdicional, R.DniCliente" +
                " FROM ReservaAdicional RA" +
                " INNER JOIN Reserva R ON R.Id = RA.IdReserva" +
                " WHERE R.IdEstadoReserva = @Confirmada";

            DataTable dt = _acceso.leer(query, new[] {
                new SqlParameter("@Confirmada", (int)EstadoReserva_GV42.Confirmada)
            });

            var lista = new List<ContratacionAdicional_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                lista.Add(new ContratacionAdicional_GV42
                {
                    IdReserva = DALUtil_GV42.Int(r, "IdReserva"),
                    IdTipoAdicional = DALUtil_GV42.Int(r, "IdTipoAdicional"),
                    DniCliente = DALUtil_GV42.Str(r, "DniCliente")
                });
            }
            return lista;
        }

        #endregion
    }
}
