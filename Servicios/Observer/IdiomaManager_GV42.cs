using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Servicios
{

    public class IdiomaManager_GV42
    {
        #region Campos

        private static IdiomaManager_GV42 _instancia;

        private readonly List<IObservadorIdioma_GV42> _observadores;

        private Dictionary<string, string> _traducciones;
        private string _idiomaActual;

        public const string ES = "es";
        public const string EN = "en";

        public const string IDIOMA_POR_DEFECTO = ES;

        #endregion

        #region Constructor

        private IdiomaManager_GV42()
        {
            _observadores = new List<IObservadorIdioma_GV42>();
            _traducciones = new Dictionary<string, string>();
            _idiomaActual = IDIOMA_POR_DEFECTO;
        }

        #endregion

        #region Propiedades

        public static IdiomaManager_GV42 Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new IdiomaManager_GV42();
                return _instancia;
            }
        }

        public string IdiomaActual => _idiomaActual;

        public bool EsIngles => _idiomaActual == EN;

        #endregion

        #region Métodos públicos

        public void Suscribir(IObservadorIdioma_GV42 observador)
        {
            if (observador == null) return;
            if (!_observadores.Contains(observador))
                _observadores.Add(observador);
        }

        public void Desuscribir(IObservadorIdioma_GV42 observador)
        {
            if (observador == null) return;
            _observadores.Remove(observador);
        }

        public void CambiarIdioma(string codigoIdioma)
        {
            if (string.IsNullOrWhiteSpace(codigoIdioma)) return;
            codigoIdioma = codigoIdioma.ToLower();
            if (codigoIdioma != ES && codigoIdioma != EN) return;

            Dictionary<string, string> nuevas = CargarDesdeArchivo(codigoIdioma);
            if (nuevas == null) return;

            _traducciones = nuevas;
            _idiomaActual = codigoIdioma;
            Notificar();
        }

        public string Traducir(string clave)
        {
            if (string.IsNullOrEmpty(clave)) return clave;
            return _traducciones.TryGetValue(clave, out string valor) ? valor : clave;
        }

        public static string T(string clave) => Instancia.Traducir(clave);

        // Traducción con valores: T("reserva.quedan", 3) con "Quedan {0} asientos" -> "Quedan 3 asientos".
        public static string T(string clave, params object[] valores)
        {
            string texto = Instancia.Traducir(clave);
            if (valores == null || valores.Length == 0) return texto;
            try { return string.Format(texto, valores); }
            catch (FormatException) { return texto; }
        }

        // Igual que T, pero si la clave no está en el archivo de idioma devuelve el texto por defecto
        // (sirve para datos que vienen de la base, como el nombre de un servicio adicional).
        public static string TConDefecto(string clave, string porDefecto)
        {
            return Instancia.Existe(clave) ? Instancia.Traducir(clave) : porDefecto;
        }

        public bool Existe(string clave) => !string.IsNullOrEmpty(clave) && _traducciones.ContainsKey(clave);

        #endregion

        #region Métodos privados

        private void Notificar()
        {
            foreach (IObservadorIdioma_GV42 obs in _observadores.ToArray())
            {
                try { obs.ActualizarIdioma(); }
                catch { }
            }
        }

        private Dictionary<string, string> CargarDesdeArchivo(string codigoIdioma)
        {
            try
            {
                string ruta = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Idiomas",
                    codigoIdioma + ".json");

                if (!File.Exists(ruta)) return null;

                string contenido = File.ReadAllText(ruta);
                return ParsearJsonPlano(contenido);
            }
            catch
            {
                return null;
            }
        }

        private Dictionary<string, string> ParsearJsonPlano(string json)
        {
            var dic = new Dictionary<string, string>();
            var rx = new Regex(@"""((?:[^""\\]|\\.)*)""\s*:\s*""((?:[^""\\]|\\.)*)""");
            foreach (Match m in rx.Matches(json))
            {
                string clave = Desescapar(m.Groups[1].Value);
                string valor = Desescapar(m.Groups[2].Value);
                dic[clave] = valor;
            }
            return dic;
        }

        private string Desescapar(string s)
        {
            return s
                .Replace("\\\"", "\"")
                .Replace("\\\\", "\\")
                .Replace("\\n", "\n")
                .Replace("\\t", "\t");
        }

        #endregion
    }
}
