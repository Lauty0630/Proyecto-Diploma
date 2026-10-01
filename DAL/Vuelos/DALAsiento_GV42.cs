using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALAsiento_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        private const string SELECT_BASE =
            "SELECT A.Id, A.IdVuelo, A.NumeroAsiento, A.IdClase, A.Ubicacion, A.EsPreferencial FROM Asiento A";

        #endregion

        #region Constructor

        public DALAsiento_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Métodos públicos

        // Asientos de la clase que todavía no fueron asignados a ningún check-in.
        public List<Asiento_GV42> ListarLibres(int idVuelo, ClaseVuelo_GV42 clase)
        {
            string query = SELECT_BASE +
                " WHERE A.IdVuelo = @IdVuelo AND A.IdClase = @IdClase" +
                "   AND NOT EXISTS (SELECT 1 FROM CheckIn CI WHERE CI.IdAsiento = A.Id)" +
                "   AND NOT EXISTS (SELECT 1 FROM ReservaPasajero RP WHERE RP.IdAsiento = A.Id)" +
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

        // Mapa completo de la clase para la pantalla de selección estilo cine: todos los
        // asientos de esa clase en ese vuelo, indicando cuáles ya están elegidos por otro pasajero.
        public List<AsientoDisponibilidad_GV42> ListarMapa(int idVuelo, ClaseVuelo_GV42 clase)
        {
            string query =
                "SELECT A.Id, A.IdVuelo, A.Fila, A.Letra, A.NumeroAsiento, A.IdClase, A.Ubicacion, A.EsPreferencial, " +
                // Ocupado si lo eligió un pasajero al reservar o si quedó asignado en un check-in.
                "       CASE WHEN EXISTS (SELECT 1 FROM ReservaPasajero RP WHERE RP.IdAsiento = A.Id) " +
                "              OR EXISTS (SELECT 1 FROM CheckIn CI WHERE CI.IdAsiento = A.Id) THEN 1 ELSE 0 END AS Ocupado " +
                "FROM Asiento A " +
                "WHERE A.IdVuelo = @IdVuelo AND A.IdClase = @IdClase " +
                "ORDER BY A.Fila, A.Letra";

            DataTable dt = _acceso.leer(query, new[] {
                new SqlParameter("@IdVuelo", idVuelo),
                new SqlParameter("@IdClase", (int)clase)
            });

            var lista = new List<AsientoDisponibilidad_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                lista.Add(new AsientoDisponibilidad_GV42
                {
                    Asiento = Mapear(r),
                    Fila = DALUtil_GV42.Int(r, "Fila"),
                    Letra = DALUtil_GV42.Str(r, "Letra"),
                    Ocupado = DALUtil_GV42.Int(r, "Ocupado") == 1
                });
            }
            return lista;
        }

        // Ocupado a nivel reserva (elegido por un pasajero al reservar), no a nivel check-in.
        public bool EstaReservado(int idAsiento)
        {
            object r = _acceso.leerEscalar(
                "SELECT (SELECT COUNT(1) FROM ReservaPasajero WHERE IdAsiento = @Id) + (SELECT COUNT(1) FROM CheckIn WHERE IdAsiento = @Id)",
                new[] { new SqlParameter("@Id", idAsiento) });
            return r != null && Convert.ToInt32(r) > 0;
        }

        // Ocupado por OTRO pasajero (en su reserva o en su check-in). Antes solo se miraba la tabla
        // CheckIn y en el check-in se podía elegir un asiento que otro pasajero ya tenía reservado.
        public bool EstaOcupadoPorOtro(int idAsiento, int idCheckIn)
        {
            object r = _acceso.leerEscalar(
                "SELECT (SELECT COUNT(1) FROM ReservaPasajero RP " +
                "        WHERE RP.IdAsiento = @Id AND NOT EXISTS (SELECT 1 FROM CheckIn CI " +
                "              WHERE CI.Id = @IdCheckIn AND CI.IdReserva = RP.IdReserva AND CI.DniPasajero = RP.DniPasajero)) " +
                "     + (SELECT COUNT(1) FROM CheckIn WHERE IdAsiento = @Id AND Id <> @IdCheckIn)",
                new[] { new SqlParameter("@Id", idAsiento), new SqlParameter("@IdCheckIn", idCheckIn) });
            return r != null && Convert.ToInt32(r) > 0;
        }

        // Asigna el asiento al check-in (solo mientras el check-in siga pendiente).
        public void Asignar(int idCheckIn, int idAsiento)
        {
            int filas;
            try
            {
                filas = _acceso.escribir(
                    // Se actualiza también el asiento de la reserva: así el mapa de asientos y el check-in
                    // usan la misma información (antes el asiento viejo seguía figurando como ocupado).
                    "UPDATE CheckIn SET IdAsiento = @IdAsiento WHERE Id = @IdCheckIn AND IdEstadoCheckIn = @Pendiente; " +
                    "IF @@ROWCOUNT > 0 " +
                    "    UPDATE RP SET IdAsiento = @IdAsiento FROM ReservaPasajero RP " +
                    "    INNER JOIN CheckIn CI ON CI.IdReserva = RP.IdReserva AND CI.DniPasajero = RP.DniPasajero " +
                    "    WHERE CI.Id = @IdCheckIn AND (RP.IdAsiento IS NULL OR RP.IdAsiento <> @IdAsiento);",
                    new[] {
                        new SqlParameter("@IdAsiento", idAsiento),
                        new SqlParameter("@IdCheckIn", idCheckIn),
                        new SqlParameter("@Pendiente", (int)EstadoCheckIn_GV42.Pendiente)
                    });
            }
            catch (Exception ex)
            {
                // El índice único filtrado UX_CheckIn_Asiento impide asignar el mismo asiento dos veces.
                if (ex.Message.Contains("UX_CheckIn_Asiento") || ex.Message.Contains("UX_ReservaPasajero_Asiento"))
                    throw new NegocioException_GV42(Servicios.IdiomaManager_GV42.T("neg.checkin.asientoTomado"), ex);
                throw;
            }

            if (filas == 0)
                throw new NegocioException_GV42(Servicios.IdiomaManager_GV42.T("neg.checkin.noSePudoAsignar"));
        }

        #endregion

        #region Métodos privados

        private Asiento_GV42 Mapear(DataRow r)
        {
            return new Asiento_GV42
            {
                Id = DALUtil_GV42.Int(r, "Id"),
                IdVuelo = DALUtil_GV42.Int(r, "IdVuelo"),
                NumeroAsiento = DALUtil_GV42.Str(r, "NumeroAsiento"),
                Clase = (ClaseVuelo_GV42)DALUtil_GV42.Int(r, "IdClase"),
                Ubicacion = DALUtil_GV42.Str(r, "Ubicacion"),
                // Las consultas de otras clases que traen el asiento pueden no incluir la columna.
                EsPreferencial = r.Table.Columns.Contains("EsPreferencial") && r["EsPreferencial"] != DBNull.Value
                                 && Convert.ToBoolean(r["EsPreferencial"])
            };
        }

        #endregion
    }
}
