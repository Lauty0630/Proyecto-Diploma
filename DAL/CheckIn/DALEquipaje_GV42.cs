using BE;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALEquipaje_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        #endregion

        #region Constructor

        public DALEquipaje_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Métodos públicos

        // Guarda el equipaje, sus etiquetas y (si corresponde) el cargo por exceso con su cobro,
        // todo en una sola transacción.
        // 'idReserva' y 'unidadesExtraCompradas' controlan, dentro de la misma transacción, que entre todos
        // los pasajeros de la reserva no se usen más unidades de equipaje extra que las compradas.
        public Equipaje_GV42 Registrar(Equipaje_GV42 e, int idReserva, int unidadesExtraCompradas)
        {
            return _acceso.EjecutarEnTransaccion(tx =>
            {
                if (e.UnidadesExtra > 0)
                {
                    object usadasObj = _acceso.leerEscalar(tx,
                        "SELECT ISNULL(SUM(E.UnidadesExtra), 0) FROM Equipaje E WITH (UPDLOCK, HOLDLOCK) " +
                        "INNER JOIN CheckIn CI ON CI.Id = E.IdCheckIn WHERE CI.IdReserva = @IdReserva",
                        new[] { new SqlParameter("@IdReserva", idReserva) });
                    if (Convert.ToInt32(usadasObj) + e.UnidadesExtra > unidadesExtraCompradas)
                        throw new NegocioException_GV42(Servicios.IdiomaManager_GV42.T("neg.checkin.extraYaUsado"));
                }

                object idObj = _acceso.leerEscalar(tx,
                    "INSERT INTO Equipaje (IdCheckIn, CantidadBultos, PesoTotalKg, FranquiciaKg, UnidadesExtra) " +
                    "VALUES (@IdCheckIn, @Bultos, @Peso, @Franquicia, @Extra); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    new[] {
                        new SqlParameter("@IdCheckIn",  e.IdCheckIn),
                        new SqlParameter("@Bultos",     e.CantidadBultos),
                        new SqlParameter("@Peso",       e.PesoTotalKg),
                        new SqlParameter("@Franquicia", e.FranquiciaKg),
                        new SqlParameter("@Extra",      e.UnidadesExtra)
                    });
                e.Id = Convert.ToInt32(idObj);

                foreach (string codigo in e.Etiquetas)
                {
                    _acceso.escribir(tx,
                        "INSERT INTO EtiquetaEquipaje (IdEquipaje, CodigoEquipaje) VALUES (@IdEquipaje, @Codigo)",
                        new[] {
                            new SqlParameter("@IdEquipaje", e.Id),
                            new SqlParameter("@Codigo",     codigo)
                        });
                }

                if (e.CargoExceso != null && e.CargoExceso.TieneExceso)
                {
                    CargoExcesoEquipaje_GV42 c = e.CargoExceso;
                    _acceso.escribir(tx,
                        "INSERT INTO CargoExcesoEquipaje (IdEquipaje, KilosExceso, CostoPorKilo, ImporteCargo, IdMedioPago, NumeroTransaccion) " +
                        "VALUES (@IdEquipaje, @Kilos, @CostoKilo, @Importe, @IdMedio, @NumTx)",
                        new[] {
                            new SqlParameter("@IdEquipaje", e.Id),
                            new SqlParameter("@Kilos",      c.KilosExceso),
                            new SqlParameter("@CostoKilo",  c.CostoPorKilo),
                            new SqlParameter("@Importe",    c.ImporteCargo),
                            new SqlParameter("@IdMedio",    (int)c.MedioPago.Value),
                            new SqlParameter("@NumTx",      DALUtil_GV42.ADb(c.NumeroTransaccion))
                        });

                    DataTable dt = _acceso.leer(tx,
                        "SELECT FechaHoraCobro FROM CargoExcesoEquipaje WHERE IdEquipaje = @Id",
                        new[] { new SqlParameter("@Id", e.Id) });
                    c.FechaHoraCobro = DALUtil_GV42.Fecha(dt.Rows[0], "FechaHoraCobro");
                }

                return e;
            });
        }

        // Unidades de equipaje extra que ya usaron los otros pasajeros de la reserva.
        public int UnidadesExtraUsadas(int idReserva, int idCheckInExcluido)
        {
            object r = _acceso.leerEscalar(
                "SELECT ISNULL(SUM(E.UnidadesExtra), 0) FROM Equipaje E " +
                "INNER JOIN CheckIn CI ON CI.Id = E.IdCheckIn WHERE CI.IdReserva = @IdReserva AND CI.Id <> @IdCheckIn",
                new[] { new SqlParameter("@IdReserva", idReserva), new SqlParameter("@IdCheckIn", idCheckInExcluido) });
            return r == null || r == DBNull.Value ? 0 : Convert.ToInt32(r);
        }

        // Devuelve null si el pasajero no despachó equipaje.
        public Equipaje_GV42 BuscarPorCheckIn(int idCheckIn)
        {
            DataTable dt = _acceso.leer(
                "SELECT Id, IdCheckIn, CantidadBultos, PesoTotalKg, FranquiciaKg, UnidadesExtra FROM Equipaje WHERE IdCheckIn = @Id",
                new[] { new SqlParameter("@Id", idCheckIn) });
            if (dt.Rows.Count == 0) return null;

            DataRow r = dt.Rows[0];
            var e = new Equipaje_GV42
            {
                Id = DALUtil_GV42.Int(r, "Id"),
                IdCheckIn = DALUtil_GV42.Int(r, "IdCheckIn"),
                CantidadBultos = DALUtil_GV42.Int(r, "CantidadBultos"),
                PesoTotalKg = DALUtil_GV42.Dec(r, "PesoTotalKg"),
                FranquiciaKg = DALUtil_GV42.Dec(r, "FranquiciaKg"),
                UnidadesExtra = DALUtil_GV42.Int(r, "UnidadesExtra")
            };

            DataTable etiquetas = _acceso.leer(
                "SELECT CodigoEquipaje FROM EtiquetaEquipaje WHERE IdEquipaje = @Id ORDER BY Id",
                new[] { new SqlParameter("@Id", e.Id) });
            foreach (DataRow er in etiquetas.Rows)
                e.Etiquetas.Add(DALUtil_GV42.Str(er, "CodigoEquipaje"));

            DataTable cargo = _acceso.leer(
                "SELECT KilosExceso, CostoPorKilo, ImporteCargo, IdMedioPago, NumeroTransaccion, FechaHoraCobro " +
                "FROM CargoExcesoEquipaje WHERE IdEquipaje = @Id",
                new[] { new SqlParameter("@Id", e.Id) });
            if (cargo.Rows.Count > 0)
            {
                DataRow cr = cargo.Rows[0];
                e.CargoExceso = new CargoExcesoEquipaje_GV42
                {
                    KilosExceso = DALUtil_GV42.Dec(cr, "KilosExceso"),
                    CostoPorKilo = DALUtil_GV42.Dec(cr, "CostoPorKilo"),
                    ImporteCargo = DALUtil_GV42.Dec(cr, "ImporteCargo"),
                    MedioPago = (MedioPago_GV42)DALUtil_GV42.Int(cr, "IdMedioPago"),
                    NumeroTransaccion = DALUtil_GV42.Str(cr, "NumeroTransaccion"),
                    FechaHoraCobro = DALUtil_GV42.FechaNull(cr, "FechaHoraCobro")
                };
            }

            return e;
        }

        #endregion
    }
}
