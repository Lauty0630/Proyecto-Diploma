using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALVuelo_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        private const string SELECT_BASE =
            "SELECT " + DALUtil_GV42.COLUMNAS_VUELO_CLASE + " " +
            "FROM Vuelo V " +
            "INNER JOIN VueloClase VC ON VC.IdVuelo = V.Id" +
            DALUtil_GV42.JOINS_VUELO;

        // ---------------------------------------------------------------- ABM de vuelos

        private const string SELECT_ABM =
            "SELECT V.Id, V.CodigoVuelo, V.FechaHoraSalida, V.FechaHoraLlegada, V.PuertaEmbarque, V.CostoKiloExceso, V.BorradoLogico, " +
            "       AL.Id AS IdAerolinea, AL.Nombre AS AerolineaNombre, " +
            "       O.Id AS IdOrigen, O.CodigoIata AS OrigenIata, O.Nombre AS OrigenNombre, O.Ciudad AS OrigenCiudad, O.Pais AS OrigenPais, " +
            "       D.Id AS IdDestino, D.CodigoIata AS DestinoIata, D.Nombre AS DestinoNombre, D.Ciudad AS DestinoCiudad, D.Pais AS DestinoPais " +
            "FROM Vuelo V " +
            "INNER JOIN Aerolinea AL ON AL.Id = V.IdAerolinea " +
            "INNER JOIN Aeropuerto O ON O.Id = V.IdOrigen " +
            "INNER JOIN Aeropuerto D ON D.Id = V.IdDestino";

        #endregion

        #region Constructor

        public DALVuelo_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Métodos públicos

        // Vuelos (uno por clase) que salen en la fecha indicada, entre origen y destino,
        // con asientos suficientes para la cantidad de pasajeros. Solo vuelos futuros.
        public List<VueloClase_GV42> BuscarDisponibles(CriterioBusquedaVuelo_GV42 criterio)
        {
            string query = SELECT_BASE +
                " WHERE V.BorradoLogico = 0" +
                "   AND V.IdOrigen = @IdOrigen" +
                "   AND V.IdDestino = @IdDestino" +
                "   AND CAST(V.FechaHoraSalida AS DATE) = @Fecha" +
                "   AND V.FechaHoraSalida > GETDATE()" +
                "   AND (VC.CapacidadAsientos - VC.AsientosReservados) >= @Cantidad" +
                (criterio.Clase.HasValue ? "   AND VC.IdClase = @IdClase" : "") +
                " ORDER BY V.FechaHoraSalida, VC.IdClase";

            var parametros = new System.Collections.Generic.List<SqlParameter> {
                new SqlParameter("@IdOrigen",  criterio.IdOrigen),
                new SqlParameter("@IdDestino", criterio.IdDestino),
                new SqlParameter("@Fecha",     criterio.FechaSalida.Date),
                new SqlParameter("@Cantidad",  criterio.CantidadPasajeros)
            };
            if (criterio.Clase.HasValue)
                parametros.Add(new SqlParameter("@IdClase", (int)criterio.Clase.Value));

            DataTable dt = _acceso.leer(query, parametros.ToArray());
            var lista = new List<VueloClase_GV42>();
            foreach (DataRow r in dt.Rows)
                lista.Add(DALUtil_GV42.MapearVueloClase(r));
            return lista;
        }

        // Fechas flexibles: precio base más barato de cada día del rango para la ruta (solo días con
        // vuelos futuros y con lugar para la cantidad de pasajeros).
        public Dictionary<DateTime, decimal> PreciosMinimosPorFecha(CriterioBusquedaVuelo_GV42 criterio, DateTime desde, DateTime hasta)
        {
            string query =
                "SELECT CAST(V.FechaHoraSalida AS DATE) AS Fecha, MIN(VC.PrecioBase) AS Precio " +
                "FROM Vuelo V INNER JOIN VueloClase VC ON VC.IdVuelo = V.Id " +
                "WHERE V.BorradoLogico = 0 AND V.IdOrigen = @IdOrigen AND V.IdDestino = @IdDestino" +
                "  AND CAST(V.FechaHoraSalida AS DATE) BETWEEN @Desde AND @Hasta" +
                "  AND V.FechaHoraSalida > GETDATE()" +
                "  AND (VC.CapacidadAsientos - VC.AsientosReservados) >= @Cantidad" +
                (criterio.Clase.HasValue ? "  AND VC.IdClase = @IdClase" : "") +
                " GROUP BY CAST(V.FechaHoraSalida AS DATE)";

            var parametros = new List<SqlParameter> {
                new SqlParameter("@IdOrigen",  criterio.IdOrigen),
                new SqlParameter("@IdDestino", criterio.IdDestino),
                new SqlParameter("@Desde",     desde.Date),
                new SqlParameter("@Hasta",     hasta.Date),
                new SqlParameter("@Cantidad",  criterio.CantidadPasajeros)
            };
            if (criterio.Clase.HasValue)
                parametros.Add(new SqlParameter("@IdClase", (int)criterio.Clase.Value));

            var precios = new Dictionary<DateTime, decimal>();
            foreach (DataRow r in _acceso.leer(query, parametros.ToArray()).Rows)
                precios[DALUtil_GV42.Fecha(r, "Fecha").Date] = DALUtil_GV42.Dec(r, "Precio");
            return precios;
        }

        public VueloClase_GV42 BuscarVueloClase(int idVuelo, ClaseVuelo_GV42 clase)
        {
            string query = SELECT_BASE + " WHERE V.Id = @IdVuelo AND VC.IdClase = @IdClase AND V.BorradoLogico = 0";
            SqlParameter[] p = {
                new SqlParameter("@IdVuelo", idVuelo),
                new SqlParameter("@IdClase", (int)clase)
            };

            DataTable dt = _acceso.leer(query, p);
            return dt.Rows.Count == 0 ? null : DALUtil_GV42.MapearVueloClase(dt.Rows[0]);
        }

        // Igual que BuscarVueloClase pero sin excluir vuelos dados de baja: se usa para mostrar el vuelo
        // de regreso de una reserva ya hecha.
        public VueloClase_GV42 BuscarVueloClaseSinFiltro(int idVuelo, ClaseVuelo_GV42 clase)
        {
            DataTable dt = _acceso.leer(SELECT_BASE + " WHERE V.Id = @IdVuelo AND VC.IdClase = @IdClase", new[] {
                new SqlParameter("@IdVuelo", idVuelo),
                new SqlParameter("@IdClase", (int)clase)
            });
            return dt.Rows.Count == 0 ? null : DALUtil_GV42.MapearVueloClase(dt.Rows[0]);
        }

        // Todos los vuelos, incluidos los dados de baja (para la pantalla de gestión).
        public List<Vuelo_GV42> ListarTodos()
        {
            DataTable dt = _acceso.leer(SELECT_ABM + " ORDER BY V.FechaHoraSalida, V.CodigoVuelo", null);
            var lista = new List<Vuelo_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                lista.Add(new Vuelo_GV42
                {
                    Id = DALUtil_GV42.Int(r, "Id"),
                    CodigoVuelo = DALUtil_GV42.Str(r, "CodigoVuelo"),
                    FechaHoraSalida = DALUtil_GV42.Fecha(r, "FechaHoraSalida"),
                    FechaHoraLlegada = DALUtil_GV42.Fecha(r, "FechaHoraLlegada"),
                    PuertaEmbarque = DALUtil_GV42.Str(r, "PuertaEmbarque"),
                    CostoKiloExceso = DALUtil_GV42.Dec(r, "CostoKiloExceso"),
                    BorradoLogico = Convert.ToBoolean(r["BorradoLogico"]),
                    Aerolinea = new Aerolinea_GV42 { Id = DALUtil_GV42.Int(r, "IdAerolinea"), Nombre = DALUtil_GV42.Str(r, "AerolineaNombre") },
                    Origen = new Aeropuerto_GV42
                    {
                        Id = DALUtil_GV42.Int(r, "IdOrigen"),
                        CodigoIata = DALUtil_GV42.Str(r, "OrigenIata"),
                        Nombre = DALUtil_GV42.Str(r, "OrigenNombre"),
                        Ciudad = DALUtil_GV42.Str(r, "OrigenCiudad"),
                        Pais = DALUtil_GV42.Str(r, "OrigenPais")
                    },
                    Destino = new Aeropuerto_GV42
                    {
                        Id = DALUtil_GV42.Int(r, "IdDestino"),
                        CodigoIata = DALUtil_GV42.Str(r, "DestinoIata"),
                        Nombre = DALUtil_GV42.Str(r, "DestinoNombre"),
                        Ciudad = DALUtil_GV42.Str(r, "DestinoCiudad"),
                        Pais = DALUtil_GV42.Str(r, "DestinoPais")
                    }
                });
            }
            return lista;
        }

        public List<Aerolinea_GV42> ListarAerolineas()
        {
            DataTable dt = _acceso.leer("SELECT Id, Nombre FROM Aerolinea ORDER BY Nombre", null);
            var lista = new List<Aerolinea_GV42>();
            foreach (DataRow r in dt.Rows)
                lista.Add(new Aerolinea_GV42 { Id = DALUtil_GV42.Int(r, "Id"), Nombre = DALUtil_GV42.Str(r, "Nombre") });
            return lista;
        }

        // ¿Hay otro vuelo (distinto de idExcluido) con ese código?
        public bool ExisteCodigo(string codigo, int idExcluido)
        {
            object r = _acceso.leerEscalar("SELECT COUNT(1) FROM Vuelo WHERE CodigoVuelo = @Cod AND Id <> @Id",
                new[] { new SqlParameter("@Cod", codigo), new SqlParameter("@Id", idExcluido) });
            return r != null && Convert.ToInt32(r) > 0;
        }

        // Reservas del vuelo que todavía no están canceladas.
        public int ContarReservasVigentes(int idVuelo)
        {
            object r = _acceso.leerEscalar("SELECT COUNT(1) FROM Reserva WHERE (IdVuelo = @Id OR IdVueloVuelta = @Id) AND IdEstadoReserva <> @Cancelada",
                new[] { new SqlParameter("@Id", idVuelo), new SqlParameter("@Cancelada", (int)EstadoReserva_GV42.Cancelada) });
            return r == null ? 0 : Convert.ToInt32(r);
        }

        // Check-ins ya realizados en ese vuelo (de reservas vigentes): esos pasajeros ya tienen su
        // tarjeta de embarque emitida con el horario actual.
        public int ContarCheckInsRealizados(int idVuelo)
        {
            object r = _acceso.leerEscalar(
                "SELECT COUNT(1) FROM CheckIn C INNER JOIN Reserva R ON R.Id = C.IdReserva " +
                "WHERE C.IdEstadoCheckIn = @Realizado AND R.IdEstadoReserva <> @Cancelada " +
                "  AND ((C.Tramo = 1 AND R.IdVuelo = @Id) OR (C.Tramo = 2 AND R.IdVueloVuelta = @Id))",
                new[] { new SqlParameter("@Id", idVuelo), new SqlParameter("@Realizado", (int)EstadoCheckIn_GV42.Realizado),
                        new SqlParameter("@Cancelada", (int)EstadoReserva_GV42.Cancelada) });
            return r == null ? 0 : Convert.ToInt32(r);
        }

        // Reservas vigentes de ida y vuelta que quedarían incoherentes si el vuelo pasara a ese horario:
        // las que lo usan de ida y su vuelta saldría antes de que llegue, y las que lo usan de vuelta y
        // saldría antes de que llegue su ida.
        public int ContarReservasIncoherentes(int idVuelo, DateTime salida, DateTime llegada)
        {
            object r = _acceso.leerEscalar(
                "SELECT COUNT(1) FROM Reserva R WHERE R.IdEstadoReserva <> @Cancelada AND R.IdVueloVuelta IS NOT NULL AND (" +
                "   (R.IdVuelo = @Id AND EXISTS (SELECT 1 FROM Vuelo V WHERE V.Id = R.IdVueloVuelta AND V.FechaHoraSalida <= @Llegada)) " +
                "OR (R.IdVueloVuelta = @Id AND EXISTS (SELECT 1 FROM Vuelo V WHERE V.Id = R.IdVuelo AND V.FechaHoraLlegada >= @Salida)))",
                new[] { new SqlParameter("@Id", idVuelo), new SqlParameter("@Salida", salida), new SqlParameter("@Llegada", llegada),
                        new SqlParameter("@Cancelada", (int)EstadoReserva_GV42.Cancelada) });
            return r == null ? 0 : Convert.ToInt32(r);
        }

        // Alta de un vuelo con sus clases y su mapa de asientos, todo en una transacción. Devuelve el Id.
        // El trigger TR_Vuelo_Historial de la base genera el primer registro del vuelo en Vuelo_C.
        // Mapa de asientos: 6 butacas por fila (A-F); las primeras filas son de Primera, las siguientes
        // de Ejecutiva y el resto de Económica (el mismo mapa que usan los vuelos ya cargados).
        public int Crear(Vuelo_GV42 v, List<VueloClase_GV42> clases, int filasPrimera, int filasEjecutiva, int filasTotales)
        {
            return _acceso.EjecutarEnTransaccion(tx =>
            {
                // SCOPE_IDENTITY y no @@IDENTITY: el trigger de Vuelo inserta en Vuelo_C, que también tiene identidad.
                object nuevo = _acceso.leerEscalar(tx,
                    "INSERT INTO Vuelo (CodigoVuelo, IdAerolinea, IdOrigen, IdDestino, FechaHoraSalida, FechaHoraLlegada, " +
                    "                   PuertaEmbarque, CostoKiloExceso, BorradoLogico) " +
                    "VALUES (@Cod, @IdAerolinea, @IdOrigen, @IdDestino, @Salida, @Llegada, @Puerta, @Costo, 0); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    new[] {
                        new SqlParameter("@Cod", v.CodigoVuelo),
                        new SqlParameter("@IdAerolinea", v.Aerolinea.Id),
                        new SqlParameter("@IdOrigen", v.Origen.Id),
                        new SqlParameter("@IdDestino", v.Destino.Id),
                        new SqlParameter("@Salida", v.FechaHoraSalida),
                        new SqlParameter("@Llegada", v.FechaHoraLlegada),
                        new SqlParameter("@Puerta", v.PuertaEmbarque),
                        new SqlParameter("@Costo", v.CostoKiloExceso)
                    });
                int idVuelo = Convert.ToInt32(nuevo);

                foreach (VueloClase_GV42 c in clases)
                {
                    _acceso.escribir(tx,
                        "INSERT INTO VueloClase (IdVuelo, IdClase, PrecioBase, CapacidadAsientos, AsientosReservados, FranquiciaEquipajeKg) " +
                        "VALUES (@IdVuelo, @IdClase, @Precio, @Capacidad, 0, @Franquicia)",
                        new[] {
                            new SqlParameter("@IdVuelo", idVuelo),
                            new SqlParameter("@IdClase", (int)c.Clase),
                            new SqlParameter("@Precio", c.PrecioBase),
                            new SqlParameter("@Capacidad", c.CapacidadAsientos),
                            new SqlParameter("@Franquicia", c.FranquiciaEquipajeKg)
                        });
                }

                _acceso.escribir(tx,
                    "INSERT INTO Asiento (IdVuelo, Fila, Letra, IdClase, Ubicacion) " +
                    "SELECT @IdVuelo, F.Fila, L.Letra, " +
                    "       CASE WHEN F.Fila <= @FilasPrimera THEN @Primera WHEN F.Fila <= @FilasPrimera + @FilasEjecutiva THEN @Ejecutiva ELSE @Economica END, " +
                    "       L.Ubicacion " +
                    "FROM (SELECT TOP (@Filas) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS Fila FROM sys.all_objects) AS F " +
                    "CROSS JOIN (VALUES ('A', N'Ventana'), ('B', N'Central'), ('C', N'Pasillo'), " +
                    "                   ('D', N'Pasillo'), ('E', N'Central'), ('F', N'Ventana')) AS L (Letra, Ubicacion)",
                    new[] {
                        new SqlParameter("@IdVuelo", idVuelo),
                        new SqlParameter("@FilasPrimera", filasPrimera),
                        new SqlParameter("@FilasEjecutiva", filasEjecutiva),
                        new SqlParameter("@Filas", filasTotales),
                        new SqlParameter("@Primera", (int)ClaseVuelo_GV42.Primera),
                        new SqlParameter("@Ejecutiva", (int)ClaseVuelo_GV42.Ejecutiva),
                        new SqlParameter("@Economica", (int)ClaseVuelo_GV42.Economica)
                    });

                return idVuelo;
            });
        }

        // El trigger TR_Vuelo_Historial de la base genera el registro nuevo en Vuelo_C.
        // El código de vuelo no se modifica: es la identificación del vuelo y de su historial.
        public void Modificar(Vuelo_GV42 v)
        {
            _acceso.escribir(
                "UPDATE Vuelo SET IdAerolinea = @IdAerolinea, IdOrigen = @IdOrigen, IdDestino = @IdDestino, " +
                "                 FechaHoraSalida = @Salida, FechaHoraLlegada = @Llegada, PuertaEmbarque = @Puerta, CostoKiloExceso = @Costo " +
                "WHERE Id = @Id",
                new[] {
                    new SqlParameter("@IdAerolinea", v.Aerolinea.Id),
                    new SqlParameter("@IdOrigen", v.Origen.Id),
                    new SqlParameter("@IdDestino", v.Destino.Id),
                    new SqlParameter("@Salida", v.FechaHoraSalida),
                    new SqlParameter("@Llegada", v.FechaHoraLlegada),
                    new SqlParameter("@Puerta", v.PuertaEmbarque),
                    new SqlParameter("@Costo", v.CostoKiloExceso),
                    new SqlParameter("@Id", v.Id)
                });
        }

        // Baja / reactivación lógica: nunca se borra la fila.
        public void CambiarBorradoLogico(int idVuelo, bool baja)
        {
            _acceso.escribir("UPDATE Vuelo SET BorradoLogico = @Baja WHERE Id = @Id",
                new[] { new SqlParameter("@Baja", baja), new SqlParameter("@Id", idVuelo) });
        }

        #endregion
    }
}
