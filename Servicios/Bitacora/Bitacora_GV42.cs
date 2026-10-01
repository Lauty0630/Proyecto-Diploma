using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{

    public class Bitacora_GV42
    {
        #region Constructor

        public Bitacora_GV42() { }

        public Bitacora_GV42(string login, string modulo, string tipoEvento, string detalle, string criticidad, DateTime fechaHora)
        {
            Login = login;
            Modulo = string.IsNullOrEmpty(modulo) ? null : new Modulo_GV42 { Nombre = modulo };
            TipoEvento = string.IsNullOrEmpty(tipoEvento) ? null : new TipoEvento_GV42 { Nombre = tipoEvento };
            Detalle = detalle;
            Criticidad = criticidad;
            FechaHora = fechaHora;
        }

        #endregion

        #region Propiedades

        public string Login { get; set; }

        public Modulo_GV42 Modulo { get; set; }

        public TipoEvento_GV42 TipoEvento { get; set; }

        public string Detalle { get; set; }

        public string Criticidad { get; set; }
        public DateTime FechaHora { get; set; }

        public string ModuloNombre => Modulo != null ? Modulo.Nombre : string.Empty;
        public string TipoEventoNombre => TipoEvento != null ? TipoEvento.Nombre : string.Empty;

        public string Evento
        {
            get
            {
                string tipo = TipoEventoNombre;
                if (string.IsNullOrWhiteSpace(Detalle)) return tipo;
                return tipo + " — " + Detalle;
            }
        }

        #endregion
    }
}
