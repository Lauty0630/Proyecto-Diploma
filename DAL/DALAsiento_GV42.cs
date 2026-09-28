using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALAsiento_GV42
    {
        private readonly Acceso _acceso;

        private const string SELECT_BASE =
            "SELECT A.Id, A.IdVuelo, A.NumeroAsiento, A.IdClase, A.Ubicacion FROM Asiento A";

        public DALAsiento_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        // Asientos de la clase que todavía no fueron asignados a ningún check-in.
        public List<Asiento_GV42> ListarLibres(int idVuelo, ClaseVuelo_GV42 clase)
        {
            string query = SELECT_BASE +
                " WHERE A.IdVuelo = @IdVuelo AND A.IdClase = @IdClase" +
                "   AND NOT EXISTS (SELECT 1 FROM CheckIn CI WHERE CI.IdAsiento = A.Id)" +
                " ORDER BY A.Fila, A.Letra";

            DataTable dt = _acceso.leer(query, new[] {
                new SqlParameter("@IdVuelo", idVuelo),
                new SqlParameter("@IdClase", (int)clase)
            });

            var lista = new List<Asiento_GV42>();
            foreach (DataRow r in dt.Rows) lista.Add(Mapear(r));
            return lista;
        }

        public Asiento_GV42 BuscarPorNumero(int idVuelo, string numeroAsiento)
        {
            string query = SELECT_BASE + " WHERE A.IdVuelo = @IdVuelo AND A.NumeroAsiento = @Numero";
            DataTable dt = _acceso.leer(query, new[] {
                new SqlParameter("@IdVuelo", idVuelo),
                new SqlParameter("@Numero",  numeroAsiento)
            });
            return dt.Rows.Count == 0 ? null : Mapear(dt.Rows[0]);
        }

        public Asiento_GV42 BuscarPorId(int idAsiento)
        {
            DataTable dt = _acceso.leer(SELECT_BASE + " WHERE A.Id = @Id", new[] { new SqlParameter("@Id", idAsiento) });
            return dt.Rows.Count == 0 ? null : Mapear(dt.Rows[0]);
        }

        public bool EstaOcupado(int idAsiento)
        {
            object r = _acceso.leerEscalar("SELECT COUNT(1) FROM CheckIn WHERE IdAsiento = @Id",
                new[] { new SqlParameter("@Id", idAsiento) });
            return r != null && Convert.ToInt32(r) > 0;
        }

        // Asigna el asiento al check-in (solo mientras el check-in siga pendiente).
        public void Asignar(int idCheckIn, int idAsiento)
        {
            int filas;
            try
            {
                filas = _acceso.escribir(
                    "UPDATE CheckIn SET IdAsiento = @IdAsiento WHERE Id = @IdCheckIn AND IdEstadoCheckIn = @Pendiente",
                    new[] {
                        new SqlParameter("@IdAsiento", idAsiento),
                        new SqlParameter("@IdCheckIn", idCheckIn),
                        new SqlParameter("@Pendiente", (int)EstadoCheckIn_GV42.Pendiente)
                    });
            }
            catch (Exception ex)
            {
                // El índice único filtrado UX_CheckIn_Asiento impide asignar el mismo asiento dos veces.
                if (ex.Message.Contains("UX_CheckIn_Asiento"))
                    throw new NegocioException_GV42("El asiento ya fue asignado a otro pasajero.", ex);
                throw;
            }

            if (filas == 0)
                throw new NegocioException_GV42("No se pudo asignar el asiento: el check-in no existe o ya fue realizado.");
        }

        private Asiento_GV42 Mapear(DataRow r)
        {
            return new Asiento_GV42
            {
                Id = DALUtil_GV42.Int(r, "Id"),
                IdVuelo = DALUtil_GV42.Int(r, "IdVuelo"),
                NumeroAsiento = DALUtil_GV42.Str(r, "NumeroAsiento"),
                Clase = (ClaseVuelo_GV42)DALUtil_GV42.Int(r, "IdClase"),
                Ubicacion = DALUtil_GV42.Str(r, "Ubicacion")
            };
        }
    }
}
