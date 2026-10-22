using System.Collections.Generic;

namespace BE
{
    // Por qué se le ofrece algo al cliente en la pantalla de reserva.
    public enum MotivoOferta_GV42
    {
        Millas = 0,          // lo que le suma este viaje a su categoría de millas
        CompraAsociada = 1,  // "quienes contratan A también contratan B"
        Categoria = 2,       // lo que más contratan los pasajeros de su misma categoría
        Habitual = 3         // lo que el propio cliente contrata casi siempre
    }

    // Un servicio adicional contratado en una reserva confirmada. Es la materia prima del análisis de
    // compras asociadas (el "carrito" es la reserva; los "productos", los servicios adicionales).
    public class ContratacionAdicional_GV42
    {
        public int IdReserva { get; set; }
        public int IdTipoAdicional { get; set; }
        public string DniCliente { get; set; }
    }

    // Regla de asociación "si contrata A, también contrata B", con las tres medidas clásicas:
    //  - Soporte:   en qué porcentaje de todas las reservas aparecen A y B juntos.
    //  - Confianza: de las reservas que tienen A, en qué porcentaje también tienen B.
    //  - Lift:      cuánto más probable es B cuando hay A que en general (mayor a 1 = hay relación).
    public class ReglaAsociacion_GV42
    {
        public int IdAntecedente { get; set; }
        public int IdConsecuente { get; set; }
        public int ReservasJuntos { get; set; }
        public decimal Soporte { get; set; }
        public decimal Confianza { get; set; }
        public decimal Lift { get; set; }
    }

    // Una oferta que se le muestra al cliente mientras reserva.
    public class OfertaCliente_GV42
    {
        public MotivoOferta_GV42 Motivo { get; set; }

        // Servicio que se ofrece. Null en las ofertas de millas, que son solo informativas.
        public TipoAdicional_GV42 Tipo { get; set; }

        // Texto armado por la BLL en el idioma actual.
        public string Titulo { get; set; }
        public string Detalle { get; set; }
    }

    // Todo lo que se le ofrece a un cliente en un momento de la reserva.
    public class OfertasCliente_GV42
    {
        public string Dni { get; set; }
        public CategoriaMillas_GV42 Categoria { get; set; }
        public int MillasUltimos12Meses { get; set; }

        // Texto armado por la BLL: categoría y millas actuales del cliente.
        public string Resumen { get; set; }
        public List<OfertaCliente_GV42> Ofertas { get; set; } = new List<OfertaCliente_GV42>();
    }
}
