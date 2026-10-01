using System;

namespace BE
{
    // Los valores numéricos coinciden con los Id de las tablas catálogo de la base de datos.

    public enum ClaseVuelo_GV42 { Economica = 1, Ejecutiva = 2, Primera = 3 }

    public enum TipoViaje_GV42 { Ida = 1, IdaYVuelta = 2 }

    public enum EstadoReserva_GV42 { PendienteDePago = 1, Confirmada = 2, Cancelada = 3 }

    public enum MedioPago_GV42 { TarjetaDebito = 1, TarjetaCredito = 2, Transferencia = 3, Efectivo = 4 }

    public enum EstadoCheckIn_GV42 { Pendiente = 1, Realizado = 2 }

    // Presencial: lo genera un Vendedor. Autogestion: el propio Cliente reserva desde su cuenta.
    public enum CanalVenta_GV42 { Presencial = 1, Autogestion = 2 }

    public static class Textos_GV42
    {
        #region Propiedades

        // BE no conoce el servicio de idiomas: la UI conecta acá el traductor al arrancar
        // (Textos_GV42.Traductor = IdiomaManager_GV42.TConDefecto). Sin traductor, queda en español.
        public static Func<string, string, string> Traductor { get; set; }

        #endregion

        #region Métodos públicos

        public static string Texto(this ClaseVuelo_GV42 v)
        {
            switch (v)
            {
                case ClaseVuelo_GV42.Economica: return Tr("enum.clase.economica", "Económica");
                case ClaseVuelo_GV42.Ejecutiva: return Tr("enum.clase.ejecutiva", "Ejecutiva");
                default: return Tr("enum.clase.primera", "Primera clase");
            }
        }

        public static string Texto(this TipoViaje_GV42 v)
        {
            return v == TipoViaje_GV42.Ida ? Tr("enum.viaje.ida", "Ida") : Tr("enum.viaje.idaVuelta", "Ida y Vuelta");
        }

        public static string Texto(this EstadoReserva_GV42 v)
        {
            switch (v)
            {
                case EstadoReserva_GV42.PendienteDePago: return Tr("enum.estado.pendiente", "Pendiente de Pago");
                case EstadoReserva_GV42.Confirmada: return Tr("enum.estado.confirmada", "Confirmada");
                default: return Tr("enum.estado.cancelada", "Cancelada");
            }
        }

        public static string Texto(this MedioPago_GV42 v)
        {
            switch (v)
            {
                case MedioPago_GV42.TarjetaDebito: return Tr("enum.medio.debito", "Tarjeta de débito");
                case MedioPago_GV42.TarjetaCredito: return Tr("enum.medio.credito", "Tarjeta de crédito");
                case MedioPago_GV42.Transferencia: return Tr("enum.medio.transferencia", "Transferencia");
                default: return Tr("enum.medio.efectivo", "Efectivo");
            }
        }

        public static string Texto(this EstadoCheckIn_GV42 v)
        {
            return v == EstadoCheckIn_GV42.Pendiente ? Tr("enum.checkin.pendiente", "Pendiente") : Tr("enum.checkin.realizado", "Realizado");
        }

        public static string Texto(this CanalVenta_GV42 v)
        {
            return v == CanalVenta_GV42.Presencial ? Tr("enum.canal.presencial", "Presencial") : Tr("enum.canal.autogestion", "Autogestión");
        }

        #endregion

        #region Métodos privados

        private static string Tr(string clave, string enEspanol)
        {
            Func<string, string, string> t = Traductor;
            return t == null ? enEspanol : t(clave, enEspanol);
        }

        #endregion
    }
}
