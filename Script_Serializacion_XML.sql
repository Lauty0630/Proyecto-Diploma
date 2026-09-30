/* =====================================================================================
   Serialización XML del maestro de usuarios
   ---------------------------------------------------------------------------------
   Ejecutar UNA vez sobre la base existente "Gestion Usuario" (SSMS o LocalDB).
   (Si la base se crea de cero con EsquemaCompleto.sql no hace falta: ya lo incluye.)

   - Agrega los tipos de evento de bitácora que usa la serialización.
   - Actualiza el dígito verificador (DVH de las filas nuevas y DVV de TipoEvento)
     para que la verificación de integridad del login no marque la tabla como alterada.
   - Es idempotente: se puede correr más de una vez sin duplicar nada.
   ===================================================================================== */
USE [Gestion Usuario];
GO

SET NOCOUNT ON;

DECLARE @Nuevos TABLE (Id INT, Nombre NVARCHAR(100));
INSERT INTO @Nuevos (Id, Nombre) VALUES
    (40, N'Usuarios serializados a XML'),
    (41, N'Usuarios deserializados desde XML');

DECLARE @Id INT, @Nombre NVARCHAR(100);
DECLARE c CURSOR LOCAL FAST_FORWARD FOR SELECT Id, Nombre FROM @Nuevos;
OPEN c;
FETCH NEXT FROM c INTO @Id, @Nombre;
WHILE @@FETCH_STATUS = 0
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.TipoEvento WHERE Nombre = @Nombre)
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM dbo.TipoEvento WHERE Id = @Id)
        BEGIN
            SET IDENTITY_INSERT dbo.TipoEvento ON;
            INSERT INTO dbo.TipoEvento (Id, Nombre) VALUES (@Id, @Nombre);
            SET IDENTITY_INSERT dbo.TipoEvento OFF;
        END
        ELSE
            INSERT INTO dbo.TipoEvento (Nombre) VALUES (@Nombre);
    END
    FETCH NEXT FROM c INTO @Id, @Nombre;
END
CLOSE c;
DEALLOCATE c;

/* DVH = SHA-256( "Id|Nombre|" ) en hexadecimal minúscula (igual que CalculadorIntegridad_GV42).
   Los dos nombres nuevos son ASCII, así que VARCHAR produce los mismos bytes que UTF-8. */
DECLARE @DVH TABLE (IdRegistro NVARCHAR(50), DVH NVARCHAR(64));
INSERT INTO @DVH (IdRegistro, DVH)
SELECT CAST(T.Id AS NVARCHAR(50)),
       LOWER(CONVERT(VARCHAR(64),
             HASHBYTES('SHA2_256', CAST(CONCAT(T.Id, '|', T.Nombre, '|') AS VARCHAR(400))), 2))
FROM dbo.TipoEvento T
INNER JOIN @Nuevos N ON N.Nombre = T.Nombre;

DELETE H
FROM dbo.IntegridadDVH H
INNER JOIN @DVH D ON D.IdRegistro = H.IdRegistro
WHERE H.NombreTabla = N'TipoEvento';

INSERT INTO dbo.IntegridadDVH (NombreTabla, IdRegistro, DVH)
SELECT N'TipoEvento', IdRegistro, DVH FROM @DVH;

/* DVV = SHA-256( concatenación de todos los DVH de la tabla ordenados en forma ordinal ) */
DECLARE @Concat VARCHAR(MAX);
SELECT @Concat = STRING_AGG(CAST(DVH AS VARCHAR(MAX)), '')
                 WITHIN GROUP (ORDER BY DVH COLLATE Latin1_General_BIN2)
FROM dbo.IntegridadDVH
WHERE NombreTabla = N'TipoEvento';

DECLARE @DVV NVARCHAR(64) = LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', ISNULL(@Concat, '')), 2));

IF EXISTS (SELECT 1 FROM dbo.IntegridadDVV WHERE NombreTabla = N'TipoEvento')
    UPDATE dbo.IntegridadDVV SET DVV = @DVV, FechaCalculo = GETDATE() WHERE NombreTabla = N'TipoEvento';
ELSE
    INSERT INTO dbo.IntegridadDVV (NombreTabla, DVV) VALUES (N'TipoEvento', @DVV);

SELECT Id, Nombre FROM dbo.TipoEvento WHERE Nombre IN (SELECT Nombre FROM @Nuevos);
PRINT 'Tipos de evento de serialización XML listos.';
GO
