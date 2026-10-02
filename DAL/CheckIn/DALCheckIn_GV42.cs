using BE;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALCheckIn_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        #endregion

        #region Constructor

        public DALCheckIn_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Métodos públicos

        // Tramo 1 = ida, 2 = vuelta: en ida y vuelta cada pasajero tiene un check-in por tramo.
        // Arma el check-in completo de un pasajero de una reserva: reserva, vuelo, servicios adicionales,
        // asiento, equipaje y tarjeta de embarque. Devuelve null si la reserva no existe o el DNI no
        // pertenece a ninguno de sus pasajeros.
        // Si la reserva todavía no está confirmada no existe registro en CheckIn: Id queda en 0.
        public CheckIn_GV42 BuscarPorReservaYDni(string numeroReserva, string dni)
        {
            return BuscarPorReservaYDni(numeroReserva, dni, 1);
        }

        public CheckIn_GV42 BuscarPorReservaYDni(string numeroReserva, string dni, int tramo)
        {
            string query =
                "SELECT ISNULL(CI.Id, 0) AS IdCheckIn, ISNULL(CI.IdEstadoCheckIn, @Pendiente) AS IdEstadoCheckIn, " +
                "       CI.FechaHoraCheckIn, CI.LoginEncargado, CI.IdAsiento, CI.IdCanal, " +
                "       R.Id AS IdReserva, R.NumeroReserva, R.IdEstadoReserva, R.DniCliente, R.IdTipoViaje, R.IdVueloVuelta, RP.EquipajeExtra, RP.Tramo, RP.IdTipoPasajero, RP.IdAsistencia, " +
                "       TF.Nombre AS TarifaNombre, TF.ValijasIncluidas, TF.AsientoIncluido, " +
                "       P.DNI AS PasDNI, P.Nombre AS PasNombre, P.Apellido AS PasApellido, " +
                "       P.Email AS PasEmail, P.Telefono AS PasTelefono, " +
                DALUtil_GV42.COLUMNAS_VUELO_CLASE + " " +
                "FROM ReservaPasajero RP " +
                "INNER JOIN Reserva R ON R.Id = RP.IdReserva " +
                "INNER JOIN Pasajero P ON P.DNI = RP.DniPasajero " +
                "LEFT JOIN TarifaFamilia TF ON TF.Id = R.IdTarifa " +
                "LEFT JOIN CheckIn CI ON CI.IdReserva = RP.IdReserva AND CI.DniPasajero = RP.DniPasajero AND CI.Tramo = RP.Tramo" +
                // Vuelo y clase del tramo (el de regreso si es la vuelta).
                DALUtil_GV42.JoinVueloDeTramo("RP") +
                DALUtil_GV42.JOINS_VUELO + " " +
                "WHERE R.NumeroReserva = @Numero AND RP.DniPasajero = @DNI AND RP.Tramo = @Tramo";

            DataTable dt = _acceso.leer(query, new[] {
                new SqlParameter("@Pendiente", (int)EstadoCheckIn_GV42.Pendiente),
                new SqlParameter("@Numero",    numeroReserva),
                new SqlParameter("@DNI",       dni),
                new SqlParameter("@Tramo",     tramo == 2 ? 2 : 1)
            });
            if (dt.Rows.Count == 0) return null;

            DataRow r = dt.Rows[0];
            var pasajero = new Pasajero_GV42();
            DALUtil_GV42.LlenarPersona(pasajero, r, "Pas");

            var ci = new CheckIn_GV42
            {
                Id = DALUtil_GV42.Int(r, "IdCheckIn"),
                IdReserva = DALUtil_GV42.Int(r, "IdReserva"),
                NumeroReserva = DALUtil_GV42.Str(r, "NumeroReserva"),
                Tramo = DALUtil_GV42.Int(r, "Tramo"),
                ReservaConVuelta = r["IdVueloVuelta"] != DBNull.Value,
                Pasajero = pasajero,
                VueloClase = DALUtil_GV42.MapearVueloClase(r),
                EstadoReserva = (EstadoReserva_GV42)DALUtil_GV42.Int(r, "IdEstadoReserva"),
                Estado = (EstadoCheckIn_GV42)DALUtil_GV42.Int(r, "IdEstadoCheckIn"),
                FechaHoraCheckIn = DALUtil_GV42.FechaNull(r, "FechaHoraCheckIn"),
                LoginEncargado = DALUtil_GV42.Str(r, "LoginEncargado"),
                Canal = r["IdCanal"] == DBNull.Value ? (CanalVenta_GV42?)null : (CanalVenta_GV42)DALUtil_GV42.Int(r, "IdCanal"),
                DniTitular = DALUtil_GV42.Str(r, "DniCliente"),
                TipoViaje = (TipoViaje_GV42)DALUtil_GV42.Int(r, "IdTipoViaje"),
                EquipajeExtraComprado = DALUtil_GV42.Int(r, "EquipajeExtra"),
                TipoPasajero = (TipoPasajero_GV42)DALUtil_GV42.Int(r, "IdTipoPasajero"),
                Asistencia = (AsistenciaEspecial_GV42)DALUtil_GV42.Int(r, "IdAsistencia"),
                TarifaNombre = DALUtil_GV42.Str(r, "TarifaNombre")
            };
            ci.VueloClase = DALUtil_GV42.MapearVueloClase(r);
            // Valijas incluidas: las de la tarifa. Reservas sin tarifa (no debería pasar): como antes, por clase.
            ci.ValijasIncluidas = r["ValijasIncluidas"] != DBNull.Value
                ? DALUtil_GV42.Int(r, "ValijasIncluidas")
                : (ci.VueloClase.Clase == ClaseVuelo_GV42.Economica ? 1 : 2);
            ci.TarifaIncluyePreferencial = r["AsientoIncluido"] != DBNull.Value
                && DALUtil_GV42.Int(r, "AsientoIncluido") == TarifaFamilia_GV42.ASIENTO_CUALQUIERA_GRATIS;
            ci.Infantes = ListarInfantes(ci.IdReserva);

            // Solo los servicios contratados para este tramo.
            ci.ServiciosAdicionales = new DALReserva_GV42().ListarAdicionales(ci.IdReserva).FindAll(a => a.Tramo == ci.Tramo);

            if (ci.Id > 0)
            {
                if (r["IdAsiento"] != DBNull.Value)
                    ci.Asiento = new DALAsiento_GV42().BuscarPorId(DALUtil_GV42.Int(r, "IdAsiento"));

                ci.Equipaje = new DALEquipaje_GV42().BuscarPorCheckIn(ci.Id);
                ci.TarjetaEmbarque = new DALTarjetaEmbarque_GV42().BuscarPorCheckIn(ci.Id);
            }

            return ci;
        }

        // DNI de todos los pasajeros de una reserva (para elegir a quién hacerle el check-in).
        public System.Collections.Generic.List<string> ListarDnisPasajeros(string numeroReserva)
        {
            DataTable dt = _acceso.leer(
                "SELECT RP.DniPasajero FROM ReservaPasajero RP INNER JOIN Reserva R ON R.Id = RP.IdReserva " +
                // Los infantes no hacen check-in propio (viajan en brazos de un adulto).
                "WHERE R.NumeroReserva = @Numero AND RP.Tramo = 1 AND RP.IdTipoPasajero <> 3 ORDER BY RP.DniPasajero",
                new[] { new SqlParameter("@Numero", numeroReserva) });
            var lista = new System.Collections.Generic.List<string>();
            foreach (DataRow r in dt.Rows) lista.Add(DALUtil_GV42.Str(r, "DniPasajero"));
            return lista;
        }

        // Nombres de los infantes de la reserva (viajan en brazos; se informan en el check-in).
        public System.Collections.Generic.List<string> ListarInfantes(int idReserva)
        {
            DataTable dt = _acceso.leer(
                "SELECT P.Nombre, P.Apellido FROM ReservaPasajero RP INNER JOIN Pasajero P ON P.DNI = RP.DniPasajero " +
                "WHERE RP.IdReserva = @Id AND RP.Tramo = 1 AND RP.IdTipoPasajero = 3 ORDER BY P.Apellido, P.Nombre",
                new[] { new SqlParameter("@Id", idReserva) });
            var lista = new System.Collections.Generic.List<string>();
            foreach (DataRow r in dt.Rows)
                lista.Add((DALUtil_GV42.Str(r, "Nombre") + " " + DALUtil_GV42.Str(r, "Apellido")).Trim());
            return lista;
        }

        public CheckIn_GV42 BuscarPorId(int idCheckIn)
        {
            DataTable dt = _acceso.leer(
                "SELECT R.NumeroReserva, CI.DniPasajero, CI.Tramo FROM CheckIn CI " +
                "INNER JOIN Reserva R ON R.Id = CI.IdReserva WHERE CI.Id = @Id",
                new[] { new SqlParameter("@Id", idCheckIn) });
            if (dt.Rows.Count == 0) return null;

            return BuscarPorReservaYDni(
                DALUtil_GV42.Str(dt.Rows[0], "NumeroReserva"),
                DALUtil_GV42.Str(dt.Rows[0], "DniPasajero"),
                DALUtil_GV42.Int(dt.Rows[0], "Tramo"));
        }

        // Cierra el check-in: estado Realizado, con fecha/hora y encargado.
        public void MarcarRealizado(int idCheckIn, string loginEncargado, CanalVenta_GV42 canal)
        {
            int filas = _acceso.escribir(
                "UPDATE CheckIn SET IdEstadoCheckIn = @Realizado, FechaHoraCheckIn = GETDATE(), LoginEncargado = @Login, IdCanal = @Canal " +
                "WHERE Id = @Id AND IdEstadoCheckIn = @Pendiente",
                new[] {
                    new SqlParameter("@Canal",     (int)canal),
                    new SqlParameter("@Realizado", (int)EstadoCheckIn_GV42.Realizado),
                    new SqlParameter("@Pendiente", (int)EstadoCheckIn_GV42.Pendiente),
                    new SqlParameter("@Login",     loginEncargado),
                    new SqlParameter("@Id",        idCheckIn)
                });

            if (filas == 0)
                throw new NegocioException_GV42(Servicios.IdiomaManager_GV42.T("neg.checkin.noExisteORealizado"));
        }

        #endregion
    }
}
