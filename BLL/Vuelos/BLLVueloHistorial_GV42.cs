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
        public List<VueloCambio_GV42> Consultar(string codigoVuelo, string nombre, DateTime? fechaIni, DateTime? fechaFin, bool soloConCambios)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Vuelos.Bitacora", IdiomaManager_GV42.T("neg.vueloHist.sinPermisoVer"));

            if (fechaIni.HasValue && fechaFin.HasValue && fechaIni.Value.Date > fechaFin.Value.Date)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vueloHist.fechasInvertidas"));

            return _dal.Listar(
                string.IsNullOrWhiteSpace(codigoVuelo) ? null : codigoVuelo.Trim(),
                string.IsNullOrWhiteSpace(nombre) ? null : nombre.Trim(),
                fechaIni, fechaFin, soloConCambios);
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

            // Se puede activar cualquier registro del historial. La única regla que se mantiene es la que
            // protege a los pasajeros: con reservas vigentes no se cambia la aerolínea ni la ruta del vuelo
            // (y la capa de datos no deja activar un registro "dado de baja" si hay reservas vigentes).
            var bllVuelo = new BLLVuelo_GV42();
            Vuelo_GV42 actual = bllVuelo.BuscarActual(cambio.IdVuelo);
            if (actual == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vuelo.noExiste"));
            string rutaActual = actual.Origen.CodigoIata + " -> " + actual.Destino.CodigoIata;
            bool cambiaIdentidad = !string.Equals(cambio.Aerolinea, actual.Aerolinea.Nombre, StringComparison.OrdinalIgnoreCase)
                                || !string.Equals((cambio.Nombre ?? string.Empty).Replace(" ", ""), rutaActual.Replace(" ", ""), StringComparison.OrdinalIgnoreCase);
            if (cambiaIdentidad && bllVuelo.ContarReservasVigentes(actual.Id) > 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.vueloHist.reservasVigentesIdentidad", actual.CodigoVuelo));

            // Solo se asigna Act = 1 al registro elegido: el trigger de la base actualiza la tabla Vuelo.
            string codigo = _dal.ActivarVersion(cambio.Id);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_VUELOS, "Version de vuelo activada",
                codigo + " - registro " + cambio.Fecha.ToString("dd/MM/yyyy") + " " + cambio.Hora.ToString(@"hh\:mm"), "Media");
        }

        #endregion
    }
}
