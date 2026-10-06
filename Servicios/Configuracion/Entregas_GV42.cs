namespace Servicios
{
    // Interruptor de las funciones de la 3ra entrega. Están programadas pero no se muestran al
    // ejecutar el sistema hasta que llegue esa entrega.
    //
    // Con ENTREGA_3_ACTIVA = false (como está ahora) el sistema se ve y funciona igual que en la
    // 2da entrega: no aparece nada de lo que sigue.
    // Con ENTREGA_3_ACTIVA = true aparecen:
    //   - Reportes > Reporte de millas   (reporte inteligente, Rep. 3)
    //   - Menú Ayuda y tecla F1          (ayuda en línea)
    //   - "Reinstalador" en el login     (restaurar un backup o reinstalar la base)
    //
    // Aparte de este interruptor, las patentes propias del reporte de millas también están sin
    // activar: bloque comentado al final de ActualizacionBD.sql.
    //
    // Para probarlo: cambiar false por true en la línea de abajo, recompilar la solución y ejecutar.
    // Para ocultarlo otra vez: volver a poner false y recompilar. No toca la base de datos.
    public static class Entregas_GV42
    {
        public static readonly bool ENTREGA_3_ACTIVA = false;
    }
}
