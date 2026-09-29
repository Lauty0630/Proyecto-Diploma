﻿using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALVuelo_GV42
    {
        private readonly Acceso _acceso;

        private const string SELECT_BASE =
            "SELECT " + DALUtil_GV42.COLUMNAS_VUELO_CLASE + " " +
            "FROM Vuelo V " +
            "INNER JOIN VueloClase VC ON VC.IdVuelo = V.Id" +
            DALUtil_GV42.JOINS_VUELO;

        public DALVuelo_GV42()
        {
            _acceso = Acceso.Instancia;
        }

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
                        Id = DALUtil_GV42.Int(r, "IdOrigen"), CodigoIata = DALUtil_GV42.Str(r, "OrigenIata"),
                        Nombre = DALUtil_GV42.Str(r, "OrigenNombre"), Ciudad = DALUtil_GV42.Str(r, "OrigenCiudad"), Pais = DALUtil_GV42.Str(r, "OrigenPais")
                    },
                    Destino = new Aeropuerto_GV42
                    {
                        Id = DALUtil_GV42.Int(r, "IdDestino"), CodigoIata = DALUtil_GV42.Str(r, "DestinoIata"),
                        Nombre = DALUtil_GV42.Str(r, "DestinoNombre"), Ciudad = DALUtil_GV42.Str(r, "DestinoCiudad"), Pais = DALUtil_GV42.Str(r, "DestinoPais")
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
            object r = _acceso.leerEscalar("SELECT COUNT(1) FROM Reserva WHERE IdVuelo = @Id AND IdEstadoReserva <> @Cancelada",
                new[] { new SqlParameter("@Id", idVuelo), new SqlParameter("@Cancelada", (int)EstadoReserva_GV42.Cancelada) });
            return r == null ? 0 : Convert.ToInt32(r);
        }

        // El trigger TR_Vuelo_Historial de la base genera el registro nuevo en Vuelo_C.
        public void Modificar(Vuelo_GV42 v)
        {
            _acceso.escribir(
                "UPDATE Vuelo SET CodigoVuelo = @Cod, IdAerolinea = @IdAerolinea, IdOrigen = @IdOrigen, IdDestino = @IdDestino, " +
                "                 FechaHoraSalida = @Salida, FechaHoraLlegada = @Llegada, PuertaEmbarque = @Puerta, CostoKiloExceso = @Costo " +
                "WHERE Id = @Id",
                new[] {
                    new SqlParameter("@Cod", v.CodigoVuelo),
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
    }
}
