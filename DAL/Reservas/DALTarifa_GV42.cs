using BE;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // Familias tarifarias (Light / Plus / Top).
    public class DALTarifa_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        private const string SELECT_BASE =
            "SELECT Id, Codigo, Nombre, PorcentajeRecargo, ValijasIncluidas, AsientoIncluido, " +
            "       PorcentajePenalidadCambio, TipoReembolso FROM TarifaFamilia";

        #endregion

        #region Constructor

        public DALTarifa_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Métodos públicos

        // De la más económica a la más completa.
        public List<TarifaFamilia_GV42> ListarActivas()
        {
            DataTable dt = _acceso.leer(SELECT_BASE + " WHERE Activo = 1 ORDER BY PorcentajeRecargo, Id", null);
            var lista = new List<TarifaFamilia_GV42>();
            foreach (DataRow r in dt.Rows) lista.Add(Mapear(r));
            return lista;
        }

        public TarifaFamilia_GV42 BuscarPorId(int id)
        {
            DataTable dt = _acceso.leer(SELECT_BASE + " WHERE Id = @Id", new[] { new SqlParameter("@Id", id) });
            return dt.Rows.Count == 0 ? null : Mapear(dt.Rows[0]);
        }

        #endregion

        #region Métodos privados

        private static TarifaFamilia_GV42 Mapear(DataRow r)
        {
            return new TarifaFamilia_GV42
            {
                Id = DALUtil_GV42.Int(r, "Id"),
                Codigo = DALUtil_GV42.Str(r, "Codigo"),
                Nombre = DALUtil_GV42.Str(r, "Nombre"),
                PorcentajeRecargo = DALUtil_GV42.Dec(r, "PorcentajeRecargo"),
                ValijasIncluidas = DALUtil_GV42.Int(r, "ValijasIncluidas"),
                AsientoIncluido = DALUtil_GV42.Int(r, "AsientoIncluido"),
                PorcentajePenalidadCambio = DALUtil_GV42.Dec(r, "PorcentajePenalidadCambio"),
                TipoReembolso = DALUtil_GV42.Int(r, "TipoReembolso")
            };
        }

        #endregion
    }
}
