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

        public const int MAX_HORAS_VUELO = 24;
        public const decimal MAX_COSTO_KILO = 99999999.99m;   // Vuelo.CostoKiloExceso decimal(10,2)

        internal Vuelo_GV42 BuscarActual(int idVuelo) => _dalVuelo.ListarTodos().Find(x => x.Id == idVuelo);

        // Reglas comunes para modificar un vuelo o activar una versión anterior de sus datos.
        internal void ValidarCambio(Vuelo_GV42 actual, string codigo, string aerolinea, string ruta,
                                    DateTime salida, DateTime llegada, decimal costoKilo)
        {
            if (actual == null)
                throw new NegocioException_GV42("El vuelo ya no existe.");
            if (actual.BorradoLogico)
                throw new NegocioException_GV42("El vuelo " + actual.CodigoVuelo + " está dado de baja: reactivalo antes de modificarlo.");
            if (actual.FechaHoraSalida <= DateTime.Now)
                throw new NegocioException_GV42("El vuelo " + actual.CodigoVuelo + " ya salió: no se puede modificar.");
            if (salida <= DateTime.Now)
                throw new NegocioException_GV42("La fecha y hora de salida tiene que ser futura.");
            if (llegada <= salida)
                throw new NegocioException_GV42("La llegada debe ser posterior a la salida.");
            if ((llegada - salida).TotalHours > MAX_HORAS_VUELO)
                throw new NegocioException_GV42("La duración del vuelo no puede superar las " + MAX_HORAS_VUELO + " horas.");
            if (costoKilo < 0 || costoKilo > MAX_COSTO_KILO)
                throw new NegocioException_GV42("El costo por kilo de exceso debe estar entre 0 y " + MAX_COSTO_KILO.ToString("N2") + ".");

            // Con reservas vigentes no se cambia la identidad del vuelo (código, aerolínea ni ruta):
            // los pasajeros compraron ese vuelo. Sí se puede reprogramar el horario, la puerta o el costo.
            if (_dalVuelo.ContarReservasVigentes(actual.Id) > 0)
            {
                string rutaActual = actual.Origen.CodigoIata + " -> " + actual.Destino.CodigoIata;
                if (!string.Equals(codigo, actual.CodigoVuelo, StringComparison.OrdinalIgnoreCase) ||
                    !string.Equals(aerolinea, actual.Aerolinea.Nombre, StringComparison.OrdinalIgnoreCase) ||
                    (ruta != null && !string.Equals(ruta.Replace(" ", ""), rutaActual.Replace(" ", ""), StringComparison.OrdinalIgnoreCase)))
                    throw new NegocioException_GV42("El vuelo " + actual.CodigoVuelo + " tiene reservas vigentes: no se puede cambiar " +
                        "el código, la aerolínea ni la ruta (sí el horario, la puerta o el costo).");
            }
        }
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

            Vuelo_GV42 actual = BuscarActual(v.Id);
            ValidarCambio(actual, v.CodigoVuelo, v.Aerolinea.Nombre,
                          v.Origen.CodigoIata + " -> " + v.Destino.CodigoIata,
                          v.FechaHoraSalida, v.FechaHoraLlegada, v.CostoKiloExceso);

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
            if (v.FechaHoraSalida <= DateTime.Now)
                throw new NegocioException_GV42("El vuelo " + v.CodigoVuelo + " ya salió: no tiene sentido reactivarlo.");

            _dalVuelo.CambiarBorradoLogico(v.Id, false);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_VUELOS, "Vuelo reactivado", v.CodigoVuelo, "Media");
        }
    }
}
