﻿﻿using System;

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
        public static string Texto(this ClaseVuelo_GV42 v)
        {
            switch (v)
            {
                case ClaseVuelo_GV42.Economica: return "Económica";
                case ClaseVuelo_GV42.Ejecutiva: return "Ejecutiva";
                default: return "Primera clase";
            }
        }

        public static string Texto(this TipoViaje_GV42 v)
        {
            return v == TipoViaje_GV42.Ida ? "Ida" : "Ida y Vuelta";
        }

        public static string Texto(this EstadoReserva_GV42 v)
        {
            switch (v)
            {
                case EstadoReserva_GV42.PendienteDePago: return "Pendiente de Pago";
                case EstadoReserva_GV42.Confirmada: return "Confirmada";
                default: return "Cancelada";
            }
        }

        public static string Texto(this MedioPago_GV42 v)
        {
            switch (v)
            {
                case MedioPago_GV42.TarjetaDebito: return "Tarjeta de débito";
                case MedioPago_GV42.TarjetaCredito: return "Tarjeta de crédito";
                case MedioPago_GV42.Transferencia: return "Transferencia";
                default: return "Efectivo";
            }
        }

        public static string Texto(this EstadoCheckIn_GV42 v)
        {
            return v == EstadoCheckIn_GV42.Pendiente ? "Pendiente" : "Realizado";
        }

        public static string Texto(this CanalVenta_GV42 v)
        {
            return v == CanalVenta_GV42.Presencial ? "Presencial" : "Autogestión";
        }
    }
}
