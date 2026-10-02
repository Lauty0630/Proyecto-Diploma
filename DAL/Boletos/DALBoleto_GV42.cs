using BE;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALBoleto_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        #endregion

        #region Constructor

        public DALBoleto_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Métodos públicos

        public List<Boleto_GV42> ListarPorReserva(string numeroReserva)
        {
            string query =
                "SELECT B.Id, B.NumeroBoleto, B.FechaEmision, B.Tramo, R.NumeroReserva, V.CodigoVuelo, " +
                "       P.DNI, P.Nombre, P.Apellido, P.Email, P.Telefono " +
                "FROM Boleto B " +
                "INNER JOIN Reserva R ON R.Id = B.IdReserva " +
                // El vuelo del boleto es el de su tramo (ida o vuelta).
                "INNER JOIN Vuelo V ON V.Id = CASE WHEN B.Tramo = 2 THEN R.IdVueloVuelta ELSE R.IdVuelo END " +
                "INNER JOIN Pasajero P ON P.DNI = B.DniPasajero " +
                "WHERE R.NumeroReserva = @Numero ORDER BY B.Tramo, B.Id";

            DataTable dt = _acceso.leer(query, new[] { new SqlParameter("@Numero", numeroReserva) });
            var lista = new List<Boleto_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                var pasajero = new Pasajero_GV42();
                DALUtil_GV42.LlenarPersona(pasajero, r, "");

                lista.Add(new Boleto_GV42
                {
                    Id = DALUtil_GV42.Int(r, "Id"),
                    NumeroBoleto = DALUtil_GV42.Str(r, "NumeroBoleto"),
                    NumeroReserva = DALUtil_GV42.Str(r, "NumeroReserva"),
                    CodigoVuelo = DALUtil_GV42.Str(r, "CodigoVuelo"),
                    FechaEmision = DALUtil_GV42.Fecha(r, "FechaEmision"),
                    Tramo = DALUtil_GV42.Int(r, "Tramo"),
                    Pasajero = pasajero
                });
            }
            return lista;
        }

        #endregion
    }
}
