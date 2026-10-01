using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public class BLLBitacora_GV42
    {
        #region Campos

        private static BLLBitacora_GV42 _Instancia;
        private readonly DALBitacora_GV42 _DALBitacora;

        #endregion

        #region Constructor

        private BLLBitacora_GV42()
        {
            _DALBitacora = new DALBitacora_GV42();
        }

        #endregion

        #region Propiedades

        public static BLLBitacora_GV42 Instancia
        {
            get
            {
                if (_Instancia == null)
                    _Instancia = new BLLBitacora_GV42();
                return _Instancia;
            }
        }

        #endregion

        #region Registro

        // Lo que se guarda es dato: módulo, tipo de evento, detalle y criticidad quedan en español.
        public void RegistrarEvento(string login, string modulo, string tipoEvento, string detalle, string criticidad)
        {
            var registro = new Bitacora_GV42(login, modulo, tipoEvento, detalle, criticidad, DateTime.Now);
            _DALBitacora.Guardar(registro);
        }

        #endregion

        #region Consultas

        public List<Bitacora_GV42> Listar()
        {
            return _DALBitacora.Listar();
        }
        public List<Bitacora_GV42> Filtrar(string login, string modulo, string tipoEvento, string criticidad, DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaFin.Date < fechaInicio.Date)
                throw new Exception(IdiomaManager_GV42.T("bitacora.fechaInvalida"));
            if (fechaInicio.Date > DateTime.Today)
                throw new Exception(IdiomaManager_GV42.T("neg.bitacora.inicioFuturo"));
            if (login != null && login.Trim().Length > 50)
                throw new Exception(IdiomaManager_GV42.T("neg.bitacora.usuarioLargo"));
            return _DALBitacora.Filtrar(login, modulo, tipoEvento, criticidad, fechaInicio, fechaFin);
        }

        public List<string> ListarModulos() => _DALBitacora.ListarModulos();
        public List<string> ListarTiposEvento() => _DALBitacora.ListarTiposEvento();
        // Valores guardados en la base (en español): la pantalla traduce lo que muestra.
        public List<string> ListarCriticidades()
        {
            return new List<string> { "Alta", "Media", "Baja" };
        }

        #endregion
    }
}
