using BE;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALBoleto_GV42
    {
        private readonly Acceso _acceso;

        public DALBoleto_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        public List<Boleto_GV42> ListarPorReserva(string numeroReserva)
        {
            string query =
                "SELECT B.Id, B.NumeroBoleto, B.FechaEmision, R.NumeroReserva, V.CodigoVuelo, " +
                "       P.DNI, P.Nombre, P.Apellido, P.Email, P.Telefono " +
                "FROM Boleto B " +
                "INNER JOIN Reserva R ON R.Id = B.IdReserva " +
                "INNER JOIN Vuelo V ON V.Id = R.IdVuelo " +
                "INNER JOIN Pasajero P ON P.DNI = B.DniPasajero " +
                "WHERE R.NumeroReserva = @Numero ORDER BY B.Id";

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
                    Pasajero = pasajero
                });
            }
            return lista;
        }
    }
}
