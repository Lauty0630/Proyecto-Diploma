using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    // Maestro de aeropuertos (ABM): el catálogo que usan los vuelos como origen y destino.
    public class BLLAeropuerto_GV42
    {
        #region Campos

        public const string PATENTE_GESTIONAR = "Aeropuertos.Gestionar";

        public const int LARGO_CODIGO = 3;     // Aeropuerto.CodigoIata char(3)
        public const int MAX_NOMBRE = 100;     // Aeropuerto.Nombre nvarchar(100)
        public const int MAX_CIUDAD = 60;      // Aeropuerto.Ciudad nvarchar(60)
        public const int MAX_PAIS = 60;        // Aeropuerto.Pais nvarchar(60)

        private readonly DALAeropuerto_GV42 _dal = new DALAeropuerto_GV42();

        #endregion

        #region Consultas

        public List<Aeropuerto_GV42> Listar()
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_GESTIONAR, IdiomaManager_GV42.T("neg.aeropuerto.sinPermiso"));
            return _dal.ListarTodos();
        }

        #endregion

        #region ABM

        public void Crear(Aeropuerto_GV42 aeropuerto)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_GESTIONAR, IdiomaManager_GV42.T("neg.aeropuerto.sinPermiso"));
            Validar(aeropuerto, true);

            if (_dal.ExisteCodigo(aeropuerto.CodigoIata, 0))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.aeropuerto.codigoDuplicado", aeropuerto.CodigoIata));

            _dal.Insertar(aeropuerto);
            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_VUELOS, "Aeropuerto creado",
                aeropuerto.CodigoIata + " - " + aeropuerto.Nombre, "Media");
        }

        public void Modificar(Aeropuerto_GV42 aeropuerto)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_GESTIONAR, IdiomaManager_GV42.T("neg.aeropuerto.sinPermiso"));
            Validar(aeropuerto, false);

            Aeropuerto_GV42 actual = _dal.ListarTodos().FirstOrDefault(a => a.Id == aeropuerto.Id);
            if (actual == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.aeropuerto.noExiste"));

            // El código IATA no cambia: se conserva el que ya tiene.
            aeropuerto.CodigoIata = actual.CodigoIata;
            _dal.Modificar(aeropuerto);
            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_VUELOS, "Aeropuerto modificado",
                aeropuerto.CodigoIata + " - " + aeropuerto.Nombre, "Media");
        }

        // Solo se elimina un aeropuerto que ningún vuelo usa (ni los vigentes ni el historial de cambios).
        public void Eliminar(Aeropuerto_GV42 aeropuerto)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_GESTIONAR, IdiomaManager_GV42.T("neg.aeropuerto.sinPermiso"));
            if (aeropuerto == null || aeropuerto.Id <= 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.aeropuerto.noExiste"));
            if (_dal.ContarVuelos(aeropuerto.Id) > 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.aeropuerto.tieneVuelos", aeropuerto.CodigoIata));

            _dal.Eliminar(aeropuerto.Id);
            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_VUELOS, "Aeropuerto eliminado",
                aeropuerto.CodigoIata + " - " + aeropuerto.Nombre, "Alta");
        }

        #endregion

        #region Métodos privados

        private static void Validar(Aeropuerto_GV42 a, bool validarCodigo)
        {
            if (a == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.aeropuerto.faltanDatos"));

            a.CodigoIata = (a.CodigoIata ?? string.Empty).Trim().ToUpperInvariant();
            a.Nombre = Validaciones_GV42.NormalizarEspacios(a.Nombre);
            a.Ciudad = Validaciones_GV42.NormalizarEspacios(a.Ciudad);
            a.Pais = Validaciones_GV42.NormalizarEspacios(a.Pais);

            if (validarCodigo && (a.CodigoIata.Length != LARGO_CODIGO || !a.CodigoIata.All(c => c >= 'A' && c <= 'Z')))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.aeropuerto.codigoInvalido"));
            ExigirTexto(a.Nombre, MAX_NOMBRE, "neg.aeropuerto.nombreInvalido");
            ExigirTexto(a.Ciudad, MAX_CIUDAD, "neg.aeropuerto.ciudadInvalida");
            ExigirTexto(a.Pais, MAX_PAIS, "neg.aeropuerto.paisInvalido");
        }

        private static void ExigirTexto(string valor, int maximo, string claveMensaje)
        {
            if (string.IsNullOrWhiteSpace(valor) || valor.Length < 2 || valor.Length > maximo)
                throw new NegocioException_GV42(IdiomaManager_GV42.T(claveMensaje, maximo));
        }

        #endregion
    }
}
