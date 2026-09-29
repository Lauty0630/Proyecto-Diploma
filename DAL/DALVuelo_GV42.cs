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
                " WHERE V.IdOrigen = @IdOrigen" +
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
            string query = SELECT_BASE + " WHERE V.Id = @IdVuelo AND VC.IdClase = @IdClase";
            SqlParameter[] p = {
                new SqlParameter("@IdVuelo", idVuelo),
                new SqlParameter("@IdClase", (int)clase)
            };

            DataTable dt = _acceso.leer(query, p);
            return dt.Rows.Count == 0 ? null : DALUtil_GV42.MapearVueloClase(dt.Rows[0]);
        }
    }
}
