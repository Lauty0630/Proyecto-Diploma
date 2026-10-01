using System;

namespace BE
{
    public class AdicionalReserva_GV42
    {
        #region Propiedades

        public int Id { get; set; }
        public TipoAdicional_GV42 TipoAdicional { get; set; }
        public int Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }

        public decimal Subtotal
        {
            get { return Cantidad * CostoUnitario; }
        }

        public string TipoNombre
        {
            get { return TipoAdicional != null ? TipoAdicional.Nombre : string.Empty; }
        }

        #endregion
    }
}
