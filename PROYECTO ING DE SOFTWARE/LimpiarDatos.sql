/* =====================================================================================
   LimpiarDatos.sql  -  deja la base recién instalada sin datos de prueba
   ---------------------------------------------------------------------------------
   Lo ejecuta el instalador después de EsquemaCompleto.sql y ActualizacionBD.sql, solo en una
   instalación nueva (o una reinstalación). Nunca se ejecuta sobre una base con datos reales.

   Se conserva lo que el sistema necesita para funcionar:
     - seguridad: patentes, familias, roles y sus permisos;
     - un solo usuario administrador (lautaro212; si no existiera, el primer usuario con rol Admin);
     - tablas del sistema: módulos y tipos de evento de la bitácora, clases de vuelo, estados,
       tipos de viaje, canales de venta, medios de pago, familias tarifarias y los servicios
       adicionales que usa el sistema (los que tienen Codigo: equipaje extra, asiento preferencial
       y selección de asiento);
     - aerolíneas y aeropuertos: los borra LimpiarCatalogos.sql (solo si no se eligieron los datos
       de demostración).
   Todo lo demás se borra: vuelos, historial de vuelos, asientos, clientes, reservas, pagos,
   boletos, check-in, equipaje, cambios, reembolsos y la bitácora de eventos.

   Los dígitos verificadores los recalcula el sistema (tareas "RecalcularDV:<tabla>").
   ===================================================================================== */
USE [Gestion Usuario];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @Admin NVARCHAR(50) = (
    SELECT TOP 1 U.UserName
    FROM dbo.Usuario U
    INNER JOIN dbo.Roles R ON R.Id = U.IdRol
    WHERE R.Nombre = N'Admin'
    ORDER BY CASE WHEN U.UserName = N'lautaro212' THEN 0 ELSE 1 END, U.UserName);

IF @Admin IS NULL
    THROW 50010, N'No hay ningún usuario con el rol Admin: no se pueden limpiar los datos.', 1;

-- Tablas que se conservan completas (o con su propio filtro más abajo).
DECLARE @Conservar TABLE (Nombre SYSNAME PRIMARY KEY);
INSERT INTO @Conservar (Nombre) VALUES
    (N'Patente'), (N'Familia'), (N'FamiliaPatente'), (N'FamiliaIntegrada'),
    (N'Roles'), (N'RolPatente'), (N'RolFamilia'), (N'Usuario'),
    (N'Modulo'), (N'TipoEvento'),
    (N'ClaseVuelo'), (N'EstadoReserva'), (N'EstadoCheckIn'), (N'TipoViaje'), (N'CanalVenta'),
    (N'MedioPago'), (N'TarifaFamilia'), (N'TipoAdicional'),
    (N'Aerolinea'), (N'Aeropuerto'),
    (N'IntegridadDVH'), (N'IntegridadDVV'), (N'VersionBD_GV42'), (N'TareaPendiente_GV42');

DECLARE @Tablas TABLE (Nombre SYSNAME PRIMARY KEY);
INSERT INTO @Tablas (Nombre)
SELECT T.name FROM sys.tables T
WHERE T.schema_id = SCHEMA_ID(N'dbo') AND T.is_ms_shipped = 0
  AND T.name NOT IN (SELECT Nombre FROM @Conservar);

DECLARE @sql NVARCHAR(MAX);

BEGIN TRANSACTION;

-- Mientras se borra: sin controles de claves foráneas y sin triggers (los de Vuelo y Vuelo_C
-- convierten el borrado en baja lógica o lo rechazan).
SELECT @sql = STRING_AGG(CONVERT(NVARCHAR(MAX),
           N'ALTER TABLE dbo.' + QUOTENAME(name) + N' NOCHECK CONSTRAINT ALL; ' +
           N'DISABLE TRIGGER ALL ON dbo.' + QUOTENAME(name) + N';'), NCHAR(10))
FROM sys.tables WHERE schema_id = SCHEMA_ID(N'dbo') AND is_ms_shipped = 0;
EXEC sys.sp_executesql @sql;

-- Datos de prueba: todas las filas de las tablas que no se conservan.
SELECT @sql = STRING_AGG(CONVERT(NVARCHAR(MAX), N'DELETE FROM dbo.' + QUOTENAME(Nombre) + N';'), NCHAR(10))
FROM @Tablas;
EXEC sys.sp_executesql @sql;

-- Usuarios: queda solo el administrador.
DELETE FROM dbo.Usuario WHERE UserName <> @Admin;

-- Servicios adicionales: quedan solo los que usa el sistema (tienen Codigo).
DELETE FROM dbo.TipoAdicional WHERE Codigo IS NULL;

-- Se vuelven a activar los controles (WITH CHECK: verifica que no haya quedado nada colgado) y los triggers.
SELECT @sql = STRING_AGG(CONVERT(NVARCHAR(MAX),
           N'ALTER TABLE dbo.' + QUOTENAME(name) + N' WITH CHECK CHECK CONSTRAINT ALL; ' +
           N'ENABLE TRIGGER ALL ON dbo.' + QUOTENAME(name) + N';'), NCHAR(10))
FROM sys.tables WHERE schema_id = SCHEMA_ID(N'dbo') AND is_ms_shipped = 0;
EXEC sys.sp_executesql @sql;

-- Los identificadores de las tablas vaciadas vuelven a empezar en 1.
SELECT @sql = STRING_AGG(CONVERT(NVARCHAR(MAX),
           N'DBCC CHECKIDENT (''dbo.' + REPLACE(QUOTENAME(T.Nombre), N'''', N'''''') + N''', RESEED, 0) WITH NO_INFOMSGS;'), NCHAR(10))
FROM @Tablas T
INNER JOIN sys.identity_columns IC ON IC.object_id = OBJECT_ID(N'dbo.' + QUOTENAME(T.Nombre))
WHERE IC.last_value IS NOT NULL;
IF @sql IS NOT NULL EXEC sys.sp_executesql @sql;

-- Dígitos verificadores: los recalcula el sistema para todas las tablas controladas.
INSERT INTO dbo.TareaPendiente_GV42 (Nombre)
SELECT N'RecalcularDV:' + V.NombreTabla
FROM dbo.IntegridadDVV V
WHERE NOT EXISTS (SELECT 1 FROM dbo.TareaPendiente_GV42 P WHERE P.Nombre = N'RecalcularDV:' + V.NombreTabla);

COMMIT TRANSACTION;
GO
