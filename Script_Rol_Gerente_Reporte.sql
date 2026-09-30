/* =====================================================================================
   Rol Gerente + Reporte de reservas (RFN 1)
   ---------------------------------------------------------------------------------
   Ejecutar UNA vez sobre la base existente "Gestion Usuario" (SSMS o LocalDB).
   (Si la base se crea de cero con EsquemaCompleto.sql no hace falta: ya lo incluye.)

   - Patentes nuevas:
       31 Reportes - Reservas              (Reportes.Reservas)            -> ver el reporte
       32 Reportes - Reservas Exportar PDF (Reportes.ReservasExportarPDF) -> exportarlo a PDF
   - Rol nuevo "Gerente": ve y exporta el reporte, más las patentes de sesión
     (cambiar clave, re-login, cerrar sesión, cambiar idioma). El rol Admin también recibe
     las dos patentes del reporte.
   - Tipos de evento de bitácora: 42 "Reporte de reservas consultado" y
     43 "Reporte de reservas exportado a PDF".
   - Actualiza el dígito verificador (DVH de las filas nuevas y DVV de Patente, Roles,
     RolPatente y TipoEvento) para que la verificación de integridad del login no falle.
   - Es idempotente: se puede correr más de una vez sin duplicar nada.
   ===================================================================================== */
USE [Gestion Usuario];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRANSACTION;

/* ---------- 1) Patentes ---------- */
DECLARE @Patentes TABLE (Id INT, Nombre NVARCHAR(100), DataKey NVARCHAR(100));
INSERT INTO @Patentes (Id, Nombre, DataKey) VALUES
    (31, N'Reportes - Reservas',              N'Reportes.Reservas'),
    (32, N'Reportes - Reservas Exportar PDF', N'Reportes.ReservasExportarPDF');

SET IDENTITY_INSERT dbo.Patente ON;
INSERT INTO dbo.Patente (Id, Nombre, DataKey)
SELECT P.Id, P.Nombre, P.DataKey FROM @Patentes P
WHERE NOT EXISTS (SELECT 1 FROM dbo.Patente X WHERE X.DataKey = P.DataKey)
  AND NOT EXISTS (SELECT 1 FROM dbo.Patente X WHERE X.Id = P.Id);
SET IDENTITY_INSERT dbo.Patente OFF;

-- Si el Id sugerido ya estaba ocupado, se inserta con el siguiente Id libre.
INSERT INTO dbo.Patente (Nombre, DataKey)
SELECT P.Nombre, P.DataKey FROM @Patentes P
WHERE NOT EXISTS (SELECT 1 FROM dbo.Patente X WHERE X.DataKey = P.DataKey);

/* ---------- 2) Tipos de evento de bitácora ---------- */
DECLARE @Eventos TABLE (Id INT, Nombre NVARCHAR(100));
INSERT INTO @Eventos (Id, Nombre) VALUES
    (42, N'Reporte de reservas consultado'),
    (43, N'Reporte de reservas exportado a PDF');

SET IDENTITY_INSERT dbo.TipoEvento ON;
INSERT INTO dbo.TipoEvento (Id, Nombre)
SELECT E.Id, E.Nombre FROM @Eventos E
WHERE NOT EXISTS (SELECT 1 FROM dbo.TipoEvento X WHERE X.Nombre = E.Nombre)
  AND NOT EXISTS (SELECT 1 FROM dbo.TipoEvento X WHERE X.Id = E.Id);
SET IDENTITY_INSERT dbo.TipoEvento OFF;

INSERT INTO dbo.TipoEvento (Nombre)
SELECT E.Nombre FROM @Eventos E
WHERE NOT EXISTS (SELECT 1 FROM dbo.TipoEvento X WHERE X.Nombre = E.Nombre);

/* ---------- 3) Rol Gerente ---------- */
IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Nombre = N'Gerente')
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Id = 28)
    BEGIN
        SET IDENTITY_INSERT dbo.Roles ON;
        INSERT INTO dbo.Roles (Id, Nombre) VALUES (28, N'Gerente');
        SET IDENTITY_INSERT dbo.Roles OFF;
    END
    ELSE
        INSERT INTO dbo.Roles (Nombre) VALUES (N'Gerente');
END

DECLARE @IdGerente INT = (SELECT Id FROM dbo.Roles WHERE Nombre = N'Gerente');
DECLARE @IdAdmin INT = (SELECT Id FROM dbo.Roles WHERE Nombre = N'Admin');

/* ---------- 4) Patentes de cada rol ---------- */
DECLARE @Asignar TABLE (IdRol INT, DataKey NVARCHAR(100));
INSERT INTO @Asignar (IdRol, DataKey) VALUES
    (@IdGerente, N'Reportes.Reservas'),
    (@IdGerente, N'Reportes.ReservasExportarPDF'),
    (@IdGerente, N'Sesion.CambiarClave'),
    (@IdGerente, N'Sesion.ReLogin'),
    (@IdGerente, N'Sesion.Logout'),
    (@IdGerente, N'Sesion.CambiarIdioma');
IF @IdAdmin IS NOT NULL
    INSERT INTO @Asignar (IdRol, DataKey) VALUES
        (@IdAdmin, N'Reportes.Reservas'),
        (@IdAdmin, N'Reportes.ReservasExportarPDF');

INSERT INTO dbo.RolPatente (IdRol, IdPatente)
SELECT A.IdRol, P.Id
FROM @Asignar A
INNER JOIN dbo.Patente P ON P.DataKey = A.DataKey
WHERE NOT EXISTS (SELECT 1 FROM dbo.RolPatente X WHERE X.IdRol = A.IdRol AND X.IdPatente = P.Id);

/* ---------- 5) Dígito verificador horizontal de las filas nuevas ----------
   DVH = SHA-256("campo|campo|...|") en hexadecimal minúscula (igual que CalculadorIntegridad_GV42).
   Todos los textos nuevos son ASCII, así que VARCHAR da los mismos bytes que UTF-8.
   Se recalcula también para filas que ya existían (por si el script se corre de nuevo).   */
DECLARE @DVH TABLE (NombreTabla NVARCHAR(100), IdRegistro NVARCHAR(50), DVH NVARCHAR(64));

INSERT INTO @DVH (NombreTabla, IdRegistro, DVH)
SELECT N'Patente', CAST(P.Id AS NVARCHAR(50)),
       LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256',
             CAST(CONCAT(P.Id, '|', P.Nombre, '|', P.DataKey, '|') AS VARCHAR(400))), 2))
FROM dbo.Patente P INNER JOIN @Patentes N ON N.DataKey = P.DataKey;

INSERT INTO @DVH (NombreTabla, IdRegistro, DVH)
SELECT N'TipoEvento', CAST(T.Id AS NVARCHAR(50)),
       LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256',
             CAST(CONCAT(T.Id, '|', T.Nombre, '|') AS VARCHAR(400))), 2))
FROM dbo.TipoEvento T INNER JOIN @Eventos N ON N.Nombre = T.Nombre;

INSERT INTO @DVH (NombreTabla, IdRegistro, DVH)
SELECT N'Roles', CAST(R.Id AS NVARCHAR(50)),
       LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256',
             CAST(CONCAT(R.Id, '|', R.Nombre, '|') AS VARCHAR(400))), 2))
FROM dbo.Roles R WHERE R.Id = @IdGerente;

INSERT INTO @DVH (NombreTabla, IdRegistro, DVH)
SELECT N'RolPatente', CONCAT(RP.IdRol, '_', RP.IdPatente),
       LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256',
             CAST(CONCAT(RP.IdRol, '|', RP.IdPatente, '|') AS VARCHAR(400))), 2))
FROM dbo.RolPatente RP
INNER JOIN dbo.Patente P ON P.Id = RP.IdPatente
INNER JOIN @Asignar A ON A.IdRol = RP.IdRol AND A.DataKey = P.DataKey;

DELETE H
FROM dbo.IntegridadDVH H
INNER JOIN @DVH D ON D.NombreTabla = H.NombreTabla AND D.IdRegistro = H.IdRegistro;

INSERT INTO dbo.IntegridadDVH (NombreTabla, IdRegistro, DVH)
SELECT NombreTabla, IdRegistro, DVH FROM @DVH;

/* ---------- 6) Dígito verificador vertical ----------
   DVV = SHA-256(concatenación de todos los DVH de la tabla en orden ordinal). */
DECLARE @Tabla NVARCHAR(100), @Concat VARCHAR(MAX), @DVV NVARCHAR(64);
DECLARE cTablas CURSOR LOCAL FAST_FORWARD FOR
    SELECT T FROM (VALUES (N'Patente'), (N'TipoEvento'), (N'Roles'), (N'RolPatente')) AS X(T);
OPEN cTablas;
FETCH NEXT FROM cTablas INTO @Tabla;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @Concat = NULL;
    SELECT @Concat = STRING_AGG(CAST(DVH AS VARCHAR(MAX)), '')
                     WITHIN GROUP (ORDER BY DVH COLLATE Latin1_General_BIN2)
    FROM dbo.IntegridadDVH
    WHERE NombreTabla = @Tabla;

    SET @DVV = LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', ISNULL(@Concat, '')), 2));

    IF EXISTS (SELECT 1 FROM dbo.IntegridadDVV WHERE NombreTabla = @Tabla)
        UPDATE dbo.IntegridadDVV SET DVV = @DVV, FechaCalculo = GETDATE() WHERE NombreTabla = @Tabla;
    ELSE
        INSERT INTO dbo.IntegridadDVV (NombreTabla, DVV) VALUES (@Tabla, @DVV);

    FETCH NEXT FROM cTablas INTO @Tabla;
END
CLOSE cTablas;
DEALLOCATE cTablas;

COMMIT TRANSACTION;

SELECT R.Id AS IdRol, R.Nombre AS Rol, P.Id AS IdPatente, P.DataKey
FROM dbo.RolPatente RP
INNER JOIN dbo.Roles R ON R.Id = RP.IdRol
INNER JOIN dbo.Patente P ON P.Id = RP.IdPatente
WHERE R.Nombre = N'Gerente' OR P.DataKey LIKE N'Reportes.%'
ORDER BY R.Nombre, P.Id;

PRINT 'Rol Gerente y reporte de reservas listos.';
GO
