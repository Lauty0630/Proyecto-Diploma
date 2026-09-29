using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BLL
{
    // Gestión de vuelos (modificación y baja/reactivación lógica). Cada cambio que llega a la
    // tabla Vuelo queda registrado en la bitácora Vuelo_C por el trigger de la base.
    public class BLLVuelo_GV42
    {
        private readonly DALVuelo_GV42 _dalVuelo = new DALVuelo_GV42();
        private readonly DALAeropuerto_GV42 _dalAeropuerto = new DALAeropuerto_GV42();

        public List<Vuelo_GV42> Listar()
        {
            BLLNegocioUtil_GV42.ExigirPatente("Vuelos.Gestionar", "No tenés permiso para gestionar vuelos.");
            return _dalVuelo.ListarTodos();
        }

        public List<Aerolinea_GV42> ListarAerolineas() { return _dalVuelo.ListarAerolineas(); }
        public List<Aeropuerto_GV42> ListarAeropuertos() { return _dalAeropuerto.ListarTodos(); }

        public void Modificar(Vuelo_GV42 v)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Vuelos.Gestionar", "No tenés permiso para gestionar vuelos.");
            if (v == null || v.Id <= 0)
                throw new NegocioException_GV42("Seleccioná el vuelo a modificar.");

            v.CodigoVuelo = (v.CodigoVuelo ?? string.Empty).Trim().ToUpperInvariant();
            v.PuertaEmbarque = (v.PuertaEmbarque ?? string.Empty).Trim().ToUpperInvariant();

            if (!Regex.IsMatch(v.CodigoVuelo, @"^[A-Z0-9]{3,10}$"))
                throw new NegocioException_GV42("El código de vuelo debe tener entre 3 y 10 letras o números (ej: AR1500).");
            if (!Regex.IsMatch(v.PuertaEmbarque, @"^[A-Z0-9]{1,10}$"))
                throw new NegocioException_GV42("La puerta de embarque debe tener entre 1 y 10 letras o números (ej: A4).");
            if (v.Aerolinea == null || v.Origen == null || v.Destino == null)
                throw new NegocioException_GV42("Indicá la aerolínea, el origen y el destino.");
            if (v.Origen.Id == v.Destino.Id)
                throw new NegocioException_GV42("El origen y el destino no pueden ser el mismo aeropuerto.");
            if (v.FechaHoraLlegada <= v.FechaHoraSalida)
                throw new NegocioException_GV42("La llegada debe ser posterior a la salida.");
            if (v.CostoKiloExceso < 0)
                throw new NegocioException_GV42("El costo por kilo de exceso no puede ser negativo.");
            if (_dalVuelo.ExisteCodigo(v.CodigoVuelo, v.Id))
                throw new NegocioException_GV42("Ya existe otro vuelo con el código " + v.CodigoVuelo + ".");

            _dalVuelo.Modificar(v);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_VUELOS, "Vuelo modificado", v.CodigoVuelo, "Media");
        }

        // Borrado lógico: el vuelo deja de ofrecerse, pero la fila y su historial se conservan.
        public void DarDeBaja(Vuelo_GV42 v)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Vuelos.Gestionar", "No tenés permiso para gestionar vuelos.");
            if (v == null || v.Id <= 0)
                throw new NegocioException_GV42("Seleccioná el vuelo a dar de baja.");
            if (v.BorradoLogico)
                throw new NegocioException_GV42("El vuelo " + v.CodigoVuelo + " ya está dado de baja.");
            if (_dalVuelo.ContarReservasVigentes(v.Id) > 0)
                throw new NegocioException_GV42("El vuelo " + v.CodigoVuelo + " tiene reservas vigentes: cancelalas antes de darlo de baja.");

            _dalVuelo.CambiarBorradoLogico(v.Id, true);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_VUELOS, "Vuelo dado de baja", v.CodigoVuelo, "Media");
        }

        public void Reactivar(Vuelo_GV42 v)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Vuelos.Gestionar", "No tenés permiso para gestionar vuelos.");
            if (v == null || v.Id <= 0)
                throw new NegocioException_GV42("Seleccioná el vuelo a reactivar.");
            if (!v.BorradoLogico)
                throw new NegocioException_GV42("El vuelo " + v.CodigoVuelo + " ya está activo.");

            _dalVuelo.CambiarBorradoLogico(v.Id, false);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_VUELOS, "Vuelo reactivado", v.CodigoVuelo, "Media");
        }
    }
}
