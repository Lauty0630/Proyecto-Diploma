using BE;
using Servicios;
using System;
using System.Data;

namespace DAL
{
    // Utilidades compartidas por las clases DAL del negocio (no es parte de la API pública).
    internal static class DALUtil_GV42
    {
        public static object ADb(object valor)
        {
            return valor ?? DBNull.Value;
        }

        public static string Str(DataRow r, string columna)
        {
            return r[columna] == DBNull.Value ? null : Convert.ToString(r[columna]);
        }

        public static int Int(DataRow r, string columna)
        {
            return Convert.ToInt32(r[columna]);
        }

        public static decimal Dec(DataRow r, string columna)
        {
            return Convert.ToDecimal(r[columna]);
        }

        public static DateTime Fecha(DataRow r, string columna)
        {
            return Convert.ToDateTime(r[columna]);
        }

        public static DateTime? FechaNull(DataRow r, string columna)
        {
            return r[columna] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r[columna]);
        }

        // El email de clientes y pasajeros se guarda cifrado, igual que el de Usuario.
        public static string Cifrar(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return texto;
            return EncriptadorReversible_GV42.Instancia.Encriptar(texto);
        }

        public static string Descifrar(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return texto;
            try { return EncriptadorReversible_GV42.Instancia.Desencriptar(texto); }
            catch { return texto; }
        }

        // Completa una persona leyendo columnas con el prefijo indicado: <pref>DNI, <pref>Nombre, etc.
        public static void LlenarPersona(Persona_GV42 p, DataRow r, string pref)
        {
            p.DNI = Str(r, pref + "DNI");
            p.Nombre = Str(r, pref + "Nombre");
            p.Apellido = Str(r, pref + "Apellido");
            p.Email = Descifrar(Str(r, pref + "Email"));
            p.Telefono = Str(r, pref + "Telefono");
        }

        // ---- Vuelo + clase: SELECT y mapeo compartidos por varias consultas ----

        public const string COLUMNAS_VUELO_CLASE =
            "V.Id AS IdVuelo, V.CodigoVuelo, V.FechaHoraSalida, V.FechaHoraLlegada, V.PuertaEmbarque, V.CostoKiloExceso, " +
            "AL.Id AS IdAerolinea, AL.Nombre AS AerolineaNombre, " +
            "O.Id AS IdOrigen, O.CodigoIata AS OrigenIata, O.Nombre AS OrigenNombre, O.Ciudad AS OrigenCiudad, O.Pais AS OrigenPais, " +
            "D.Id AS IdDestino, D.CodigoIata AS DestinoIata, D.Nombre AS DestinoNombre, D.Ciudad AS DestinoCiudad, D.Pais AS DestinoPais, " +
            "VC.IdClase, VC.PrecioBase, VC.CapacidadAsientos, VC.AsientosReservados, VC.FranquiciaEquipajeKg";

        // Requiere que la consulta ya tenga Vuelo con alias V.
        public const string JOINS_VUELO =
            " INNER JOIN Aerolinea AL ON AL.Id = V.IdAerolinea" +
            " INNER JOIN Aeropuerto O ON O.Id = V.IdOrigen" +
            " INNER JOIN Aeropuerto D ON D.Id = V.IdDestino";

        public static VueloClase_GV42 MapearVueloClase(DataRow r)
        {
            var vuelo = new Vuelo_GV42
            {
                Id = Int(r, "IdVuelo"),
                CodigoVuelo = Str(r, "CodigoVuelo"),
                FechaHoraSalida = Fecha(r, "FechaHoraSalida"),
                FechaHoraLlegada = Fecha(r, "FechaHoraLlegada"),
                PuertaEmbarque = Str(r, "PuertaEmbarque"),
                CostoKiloExceso = Dec(r, "CostoKiloExceso"),
                Aerolinea = new Aerolinea_GV42
                {
                    Id = Int(r, "IdAerolinea"),
                    Nombre = Str(r, "AerolineaNombre")
                },
                Origen = new Aeropuerto_GV42
                {
                    Id = Int(r, "IdOrigen"),
                    CodigoIata = Str(r, "OrigenIata"),
                    Nombre = Str(r, "OrigenNombre"),
                    Ciudad = Str(r, "OrigenCiudad"),
                    Pais = Str(r, "OrigenPais")
                },
                Destino = new Aeropuerto_GV42
                {
                    Id = Int(r, "IdDestino"),
                    CodigoIata = Str(r, "DestinoIata"),
                    Nombre = Str(r, "DestinoNombre"),
                    Ciudad = Str(r, "DestinoCiudad"),
                    Pais = Str(r, "DestinoPais")
                }
            };

            return new VueloClase_GV42
            {
                Vuelo = vuelo,
                Clase = (ClaseVuelo_GV42)Int(r, "IdClase"),
                PrecioBase = Dec(r, "PrecioBase"),
                CapacidadAsientos = Int(r, "CapacidadAsientos"),
                AsientosReservados = Int(r, "AsientosReservados"),
                FranquiciaEquipajeKg = Dec(r, "FranquiciaEquipajeKg")
            };
        }
    }
}
