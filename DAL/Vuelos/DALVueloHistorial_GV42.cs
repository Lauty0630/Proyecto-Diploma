using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // Lectura de la bitácora de cambios de vuelos (tabla Vuelo_C) y activación de versiones.
    // Los registros nuevos NO se insertan desde acá: los genera el trigger TR_Vuelo_Historial
    // cada vez que cambia algo en la tabla Vuelo.
    public class DALVueloHistorial_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        private const string NOMBRE_RUTA = "O.CodigoIata + N' -> ' + D.CodigoIata";

        #endregion

        #region Constructor

        public DALVueloHistorial_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Métodos públicos

        // Filtros opcionales: código de vuelo, nombre (ruta) y rango de fechas del cambio.
        public List<VueloCambio_GV42> Listar(string codigoVuelo, string nombre, DateTime? fechaIni, DateTime? fechaFin)
        {
            string query =
                "SELECT C.Id, C.IdVuelo, C.CodigoVuelo, C.Fecha, C.Hora, " + NOMBRE_RUTA + " AS Nombre, " +
                "       AL.Nombre AS Aerolinea, C.FechaHoraSalida, C.FechaHoraLlegada, C.PuertaEmbarque, " +
                "       C.CostoKiloExceso, C.BorradoLogico, C.Act " +
                "FROM Vuelo_C C " +
                "INNER JOIN Aerolinea AL ON AL.Id = C.IdAerolinea " +
                "INNER JOIN Aeropuerto O ON O.Id = C.IdOrigen " +
                "INNER JOIN Aeropuerto D ON D.Id = C.IdDestino " +
                "WHERE (@Cod IS NULL OR C.CodigoVuelo = @Cod) " +
                "  AND (@Nombre IS NULL OR " + NOMBRE_RUTA + " = @Nombre) " +
                "  AND (@Ini IS NULL OR C.Fecha >= @Ini) " +
                "  AND (@Fin IS NULL OR C.Fecha <= @Fin) " +
                "ORDER BY C.CodigoVuelo, C.Fecha, C.Hora, C.Id";

            SqlParameter[] p = {
                new SqlParameter("@Cod", SqlDbType.NVarChar, 10) { Value = DALUtil_GV42.ADb(codigoVuelo) },
                new SqlParameter("@Nombre", SqlDbType.NVarChar, 20) { Value = DALUtil_GV42.ADb(nombre) },
                new SqlParameter("@Ini", SqlDbType.Date) { Value = DALUtil_GV42.ADb(fechaIni.HasValue ? (object)fechaIni.Value.Date : null) },
                new SqlParameter("@Fin", SqlDbType.Date) { Value = DALUtil_GV42.ADb(fechaFin.HasValue ? (object)fechaFin.Value.Date : null) }
            };

            DataTable dt = _acceso.leer(query, p);
            var lista = new List<VueloCambio_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                lista.Add(new VueloCambio_GV42
                {
                    Id = DALUtil_GV42.Int(r, "Id"),
                    IdVuelo = DALUtil_GV42.Int(r, "IdVuelo"),
                    CodigoVuelo = DALUtil_GV42.Str(r, "CodigoVuelo"),
                    Fecha = DALUtil_GV42.Fecha(r, "Fecha"),
                    Hora = (TimeSpan)r["Hora"],
                    Nombre = DALUtil_GV42.Str(r, "Nombre"),
                    Aerolinea = DALUtil_GV42.Str(r, "Aerolinea"),
                    FechaHoraSalida = DALUtil_GV42.Fecha(r, "FechaHoraSalida"),
                    FechaHoraLlegada = DALUtil_GV42.Fecha(r, "FechaHoraLlegada"),
                    PuertaEmbarque = DALUtil_GV42.Str(r, "PuertaEmbarque"),
                    CostoKiloExceso = DALUtil_GV42.Dec(r, "CostoKiloExceso"),
                    BorradoLogico = Convert.ToBoolean(r["BorradoLogico"]),
                    Act = Convert.ToBoolean(r["Act"])
                });
            }
            return lista;
        }

        public List<string> ListarCodigos()
        {
            DataTable dt = _acceso.leer("SELECT DISTINCT CodigoVuelo FROM Vuelo_C ORDER BY CodigoVuelo", null);
            var lista = new List<string>();
            foreach (DataRow r in dt.Rows) lista.Add(DALUtil_GV42.Str(r, "CodigoVuelo"));
            return lista;
        }

        public List<string> ListarNombres()
        {
            DataTable dt = _acceso.leer(
                "SELECT DISTINCT " + NOMBRE_RUTA + " AS Nombre FROM Vuelo_C C " +
                "INNER JOIN Aeropuerto O ON O.Id = C.IdOrigen INNER JOIN Aeropuerto D ON D.Id = C.IdDestino ORDER BY 1", null);
            var lista = new List<string>();
            foreach (DataRow r in dt.Rows) lista.Add(DALUtil_GV42.Str(r, "Nombre"));
            return lista;
        }

        // Deja como único registro activo (Act = 1) del vuelo al indicado y actualiza la tabla Vuelo
        // con sus datos, todo en una transacción. Devuelve el código del vuelo afectado.
        // El trigger de Vuelo no genera otra versión porque los datos ya coinciden con el registro activo.
        public string ActivarVersion(int idCambio)
        {
            return _acceso.EjecutarEnTransaccion(tx =>
            {
                DataTable dt = _acceso.leer(tx,
                    "SELECT IdVuelo, CodigoVuelo, BorradoLogico, Act FROM Vuelo_C WHERE Id = @Id",
                    new[] { new SqlParameter("@Id", idCambio) });
                if (dt.Rows.Count == 0)
                    throw new NegocioException_GV42("El registro seleccionado no existe.");

                DataRow fila = dt.Rows[0];
                int idVuelo = DALUtil_GV42.Int(fila, "IdVuelo");
                string codigo = DALUtil_GV42.Str(fila, "CodigoVuelo");
                bool baja = Convert.ToBoolean(fila["BorradoLogico"]);

                if (Convert.ToBoolean(fila["Act"]))
                    throw new NegocioException_GV42("Ese registro ya es el activo del vuelo " + codigo + ".");

                object otro = _acceso.leerEscalar(tx,
                    "SELECT COUNT(1) FROM Vuelo WHERE CodigoVuelo = @Cod AND Id <> @IdVuelo",
                    new[] { new SqlParameter("@Cod", codigo), new SqlParameter("@IdVuelo", idVuelo) });
                if (otro != null && Convert.ToInt32(otro) > 0)
                    throw new NegocioException_GV42("No se puede activar: otro vuelo ya usa el código " + codigo + ".");

                if (baja)
                {
                    object vigentes = _acceso.leerEscalar(tx,
                        "SELECT COUNT(1) FROM Reserva WHERE (IdVuelo = @IdVuelo OR IdVueloVuelta = @IdVuelo) AND IdEstadoReserva <> @Cancelada",
                        new[] { new SqlParameter("@IdVuelo", idVuelo), new SqlParameter("@Cancelada", (int)EstadoReserva_GV42.Cancelada) });
                    if (vigentes != null && Convert.ToInt32(vigentes) > 0)
                        throw new NegocioException_GV42("Esa versión da de baja el vuelo " + codigo + ", pero tiene reservas vigentes.");
                }

                _acceso.escribir(tx, "UPDATE Vuelo_C SET Act = 0 WHERE IdVuelo = @IdVuelo AND Act = 1",
                    new[] { new SqlParameter("@IdVuelo", idVuelo) });
                _acceso.escribir(tx, "UPDATE Vuelo_C SET Act = 1 WHERE Id = @Id",
                    new[] { new SqlParameter("@Id", idCambio) });

                _acceso.escribir(tx,
                    "UPDATE V SET V.CodigoVuelo = C.CodigoVuelo, V.IdAerolinea = C.IdAerolinea, V.IdOrigen = C.IdOrigen, " +
                    "             V.IdDestino = C.IdDestino, V.FechaHoraSalida = C.FechaHoraSalida, V.FechaHoraLlegada = C.FechaHoraLlegada, " +
                    "             V.PuertaEmbarque = C.PuertaEmbarque, V.CostoKiloExceso = C.CostoKiloExceso, V.BorradoLogico = C.BorradoLogico " +
                    "FROM Vuelo V INNER JOIN Vuelo_C C ON C.IdVuelo = V.Id WHERE C.Id = @Id",
                    new[] { new SqlParameter("@Id", idCambio) });

                return codigo;
            });
        }

        #endregion
    }
}
