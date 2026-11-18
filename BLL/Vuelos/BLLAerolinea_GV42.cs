using BE;
using DAL;
using Servicios;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    // Maestro de aerolíneas (ABM): el catálogo de aerolíneas que operan los vuelos.
    public class BLLAerolinea_GV42
    {
        #region Campos

        public const string PATENTE_GESTIONAR = "Aerolineas.Gestionar";

        public const int MAX_NOMBRE = 80;      // Aerolinea.Nombre nvarchar(80)

        private readonly DALAerolinea_GV42 _dal = new DALAerolinea_GV42();

        #endregion

        #region Consultas

        public List<Aerolinea_GV42> Listar()
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_GESTIONAR, IdiomaManager_GV42.T("neg.aerolinea.sinPermiso"));
            return _dal.ListarTodos();
        }

        #endregion

        #region ABM

        public void Crear(Aerolinea_GV42 aerolinea)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_GESTIONAR, IdiomaManager_GV42.T("neg.aerolinea.sinPermiso"));
            Validar(aerolinea);
            ExigirNombreUnico(aerolinea);

            _dal.Insertar(aerolinea);
            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_VUELOS, "Aerolínea creada", aerolinea.Nombre, "Media");
        }

        public void Modificar(Aerolinea_GV42 aerolinea)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_GESTIONAR, IdiomaManager_GV42.T("neg.aerolinea.sinPermiso"));
            Validar(aerolinea);

            Aerolinea_GV42 actual = _dal.ListarTodos().FirstOrDefault(a => a.Id == aerolinea.Id);
            if (actual == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.aerolinea.noExiste"));

            ExigirNombreUnico(aerolinea);
            _dal.Modificar(aerolinea);
            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_VUELOS, "Aerolínea modificada",
                actual.Nombre + " -> " + aerolinea.Nombre, "Media");
        }

        // Solo se elimina una aerolínea que ningún vuelo usa (ni los vigentes ni el historial de cambios).
        public void Eliminar(Aerolinea_GV42 aerolinea)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_GESTIONAR, IdiomaManager_GV42.T("neg.aerolinea.sinPermiso"));
            if (aerolinea == null || aerolinea.Id <= 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.aerolinea.noExiste"));
            if (_dal.ContarVuelos(aerolinea.Id) > 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.aerolinea.tieneVuelos", aerolinea.Nombre));

            _dal.Eliminar(aerolinea.Id);
            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_VUELOS, "Aerolínea eliminada", aerolinea.Nombre, "Alta");
        }

        #endregion

        #region Métodos privados

        private static void Validar(Aerolinea_GV42 a)
        {
            if (a == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.aerolinea.nombreInvalido", MAX_NOMBRE));

            a.Nombre = Validaciones_GV42.NormalizarEspacios(a.Nombre);
            if (string.IsNullOrWhiteSpace(a.Nombre) || a.Nombre.Length < 2 || a.Nombre.Length > MAX_NOMBRE)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.aerolinea.nombreInvalido", MAX_NOMBRE));
        }

        // No puede haber dos aerolíneas con el mismo nombre (sin distinguir mayúsculas ni acentos).
        private void ExigirNombreUnico(Aerolinea_GV42 a)
        {
            if (_dal.ListarTodos().Any(x => x.Id != a.Id && Validaciones_GV42.MismoTexto(x.Nombre, a.Nombre)))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.aerolinea.nombreDuplicado", a.Nombre));
        }

        #endregion
    }
}
