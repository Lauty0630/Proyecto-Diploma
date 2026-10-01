using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;

namespace BLL
{
    // Bitácora de cambios de vuelos (Vuelo_C): consulta con filtros y activación de versiones.
    public class BLLVueloHistorial_GV42
    {
        #region Campos

        private readonly DALVueloHistorial_GV42 _dal = new DALVueloHistorial_GV42();

        #endregion

        #region Permisos

        public bool PuedeActivar()
        {
            return BLLNegocioUtil_GV42.TienePatente("Vuelos.Activar");
        }

        #endregion

        #region Consultas

        public List<string> ListarCodigos() { return _dal.ListarCodigos(); }
        public List<string> ListarNombres() { return _dal.ListarNombres(); }

        // Cualquier filtro en null/vacío no se aplica.
        public List<VueloCambio_GV42> Consultar(string codigoVuelo, string nombre, DateTime? fechaIni, DateTime? fechaFin)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Vuelos.Bitacora", IdiomaManager_GV42.T("neg.vueloHist.sinPermisoVer"));

            if (fechaIni.HasValue && fechaFin.HasValue && fechaIni.Value.Date > fechaFin.Value.Date)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vueloHist.fechasInvertidas"));

            return _dal.Listar(
                string.IsNullOrWhiteSpace(codigoVuelo) ? null : codigoVuelo.Trim(),
                string.IsNullOrWhiteSpace(nombre) ? null : nombre.Trim(),
                fechaIni, fechaFin);
        }

        #endregion

        #region Activar versión

        // Pasa a ser el registro activo de ese vuelo y la tabla Vuelo queda con esos datos.
        public void ActivarVersion(VueloCambio_GV42 cambio)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Vuelos.Activar", IdiomaManager_GV42.T("neg.vueloHist.sinPermisoActivar"));
            if (cambio == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vueloHist.seleccioneRegistro"));
            if (cambio.Act)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vueloHist.yaActivo", cambio.CodigoVuelo));

            // Activar una versión vieja es un cambio más del vuelo: pasa por las mismas reglas que
            // "Modificar" (antes se restauraba cualquier versión, incluso con la salida en el pasado).
            var bllVuelo = new BLLVuelo_GV42();
            bllVuelo.ValidarCambio(bllVuelo.BuscarActual(cambio.IdVuelo), cambio.CodigoVuelo, cambio.Aerolinea, cambio.Nombre,
                                   cambio.FechaHoraSalida, cambio.FechaHoraLlegada, cambio.CostoKiloExceso);

            string codigo = _dal.ActivarVersion(cambio.Id);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_VUELOS, "Version de vuelo activada",
                codigo + " - registro " + cambio.Fecha.ToString("dd/MM/yyyy") + " " + cambio.Hora.ToString(@"hh\:mm"), "Media");
        }

        #endregion
    }
}
