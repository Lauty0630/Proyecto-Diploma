/* =====================================================================================
   LimpiarCatalogos.sql  -  instalación sin datos: también sin aerolíneas ni aeropuertos
   ---------------------------------------------------------------------------------
   Lo ejecuta el instalador después de LimpiarDatos.sql cuando NO se eligen los datos de
   demostración. Las aerolíneas se cargan en Maestros > Aerolíneas y los aeropuertos en
   Maestros > Aeropuertos.
   También apaga el generador automático de vuelos (usp_GenerarVuelos_GV42): en una base sin
   datos los vuelos los crea el usuario en Maestros > Gestión de vuelos.
   ===================================================================================== */
USE [Gestion Usuario];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRANSACTION;

-- Ya no hay vuelos (LimpiarDatos.sql), así que nada referencia a aerolíneas ni aeropuertos.
DELETE FROM dbo.Aerolinea;
DELETE FROM dbo.Aeropuerto;
DBCC CHECKIDENT ('dbo.Aerolinea', RESEED, 0) WITH NO_INFOMSGS;
DBCC CHECKIDENT ('dbo.Aeropuerto', RESEED, 0) WITH NO_INFOMSGS;

-- Generador de vuelos apagado (ver ActualizacionBD.sql, propiedad FLYSAFE_GenerarVuelos).
IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE class = 0 AND name = N'FLYSAFE_GenerarVuelos')
    EXEC sys.sp_updateextendedproperty @name = N'FLYSAFE_GenerarVuelos', @value = N'0';
ELSE
    EXEC sys.sp_addextendedproperty @name = N'FLYSAFE_GenerarVuelos', @value = N'0';

COMMIT TRANSACTION;
GO
