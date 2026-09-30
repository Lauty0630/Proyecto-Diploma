/* =====================================================================================
   Reservas: precio de lista de adicionales + vuelos adicionales
   ---------------------------------------------------------------------------------
   Ejecutar UNA vez sobre la base existente "Gestion Usuario" (SSMS o LocalDB).
   Si la base se crea de cero con EsquemaCompleto.sql no hace falta: ya lo incluye.
   Se puede volver a correr sin duplicar nada (por ejemplo, más adelante, para
   generar vuelos de las semanas siguientes).

   1) TipoAdicional.PrecioUnitario: precio de lista de cada servicio adicional.
      OBLIGATORIO para esta versión del sistema (la pantalla de reserva lo lee).
   2) Vuelos para los próximos @Dias días (a partir de mañana): 20 rutas de ida y
      vuelta, 70 vuelos por día, 3 aerolíneas, cada uno con sus 3 clases
      (Económica 84 / Ejecutiva 24 / Primera 12 asientos) y sus 120 asientos
      (filas 1-2 Primera, 3-6 Ejecutiva, 7-20 Económica, letras A-F), igual que los
      vuelos existentes. Agrega 6 aeropuertos: SLA, NQN, USH, TUC, MDQ, FTE.
      Precio: Ejecutiva = 2,2 x Económica y Primera = 3,5 x Económica; +15 % viernes
      y domingos y una variación de ±10 % según el día.
      Vuelo, VueloClase y Asiento no llevan dígito verificador; el trigger
      TR_Vuelo_Historial registra cada alta en Vuelo_C como cualquier alta de vuelo.
   ===================================================================================== */
USE [Gestion Usuario];
GO
/* ---------- 1) Precio de lista de los servicios adicionales ---------- */
IF COL_LENGTH('dbo.TipoAdicional', 'PrecioUnitario') IS NULL
    ALTER TABLE dbo.TipoAdicional ADD PrecioUnitario DECIMAL(12,2) NOT NULL
        CONSTRAINT DF_TipoAdicional_PrecioUnitario DEFAULT (0)
        CONSTRAINT CK_TipoAdicional_PrecioUnitario CHECK (PrecioUnitario >= 0);
GO
UPDATE dbo.TipoAdicional
SET PrecioUnitario = CASE Nombre
        WHEN N'Equipaje extra'         THEN 25000
        WHEN N'Asiento preferencial'   THEN 12000
        WHEN N'Comida especial'        THEN  8000
        WHEN N'Asistencia prioritaria' THEN 15000
        ELSE 10000 END
WHERE PrecioUnitario = 0;
GO

/* ---------- 2) Vuelos adicionales ---------- */
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @Dias INT = 60;   -- cantidad de días hacia adelante a cubrir

BEGIN TRANSACTION;

/* ---------- Aeropuertos nuevos ---------- */
INSERT INTO dbo.Aeropuerto (CodigoIata, Nombre, Ciudad, Pais)
SELECT N.CodigoIata, N.Nombre, N.Ciudad, N'Argentina'
FROM (VALUES
    ('SLA', N'Aeropuerto Internacional Martín Miguel de Güemes', N'Salta'),
    ('NQN', N'Aeropuerto Internacional Presidente Perón', N'Neuquén'),
    ('USH', N'Aeropuerto Internacional Malvinas Argentinas', N'Ushuaia'),
    ('TUC', N'Aeropuerto Internacional Teniente Benjamín Matienzo', N'San Miguel de Tucumán'),
    ('MDQ', N'Aeropuerto Internacional Astor Piazzolla', N'Mar del Plata'),
    ('FTE', N'Aeropuerto Internacional Comandante Armando Tola', N'El Calafate')
) AS N (CodigoIata, Nombre, Ciudad)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Aeropuerto A WHERE A.CodigoIata = N.CodigoIata);

/* ---------- Frecuencias (una fila = un vuelo diario) ---------- */
DECLARE @Frecuencias TABLE (
    Codigo NVARCHAR(10), Aerolinea NVARCHAR(80), Origen CHAR(3), Destino CHAR(3),
    Hora TIME(0), DuracionMin INT, PrecioEconomica DECIMAL(12,2), CostoKilo DECIMAL(10,2), Puerta NVARCHAR(10));

INSERT INTO @Frecuencias (Codigo, Aerolinea, Origen, Destino, Hora, DuracionMin, PrecioEconomica, CostoKilo, Puerta) VALUES
    (N'AR2000', N'Aerolíneas Argentinas', 'AEP', 'COR', '07:00', 80, 95000.00, 3500.00, N'A1'),
    (N'AR2001', N'Aerolíneas Argentinas', 'COR', 'AEP', '09:10', 80, 95000.00, 3500.00, N'E1'),
    (N'FB2002', N'Flybondi', 'AEP', 'COR', '12:30', 80, 95000.00, 3500.00, N'A2'),
    (N'FB2003', N'Flybondi', 'COR', 'AEP', '14:40', 80, 95000.00, 3500.00, N'E2'),
    (N'AR2004', N'Aerolíneas Argentinas', 'AEP', 'COR', '18:40', 80, 95000.00, 3500.00, N'A3'),
    (N'AR2005', N'Aerolíneas Argentinas', 'COR', 'AEP', '20:50', 80, 95000.00, 3500.00, N'E3'),
    (N'AR2020', N'Aerolíneas Argentinas', 'AEP', 'MDZ', '08:15', 115, 110000.00, 3600.00, N'B2'),
    (N'AR2021', N'Aerolíneas Argentinas', 'MDZ', 'AEP', '11:00', 115, 110000.00, 3600.00, N'F2'),
    (N'FB2022', N'Flybondi', 'AEP', 'MDZ', '14:00', 115, 110000.00, 3600.00, N'B3'),
    (N'FB2023', N'Flybondi', 'MDZ', 'AEP', '16:45', 115, 110000.00, 3600.00, N'F3'),
    (N'JA2024', N'JetSMART', 'AEP', 'MDZ', '20:10', 115, 110000.00, 3600.00, N'B4'),
    (N'JA2025', N'JetSMART', 'MDZ', 'AEP', '22:55', 115, 110000.00, 3600.00, N'F4'),
    (N'AR2040', N'Aerolíneas Argentinas', 'AEP', 'BRC', '09:00', 140, 140000.00, 3800.00, N'C3'),
    (N'AR2041', N'Aerolíneas Argentinas', 'BRC', 'AEP', '12:10', 140, 140000.00, 3800.00, N'G3'),
    (N'JA2042', N'JetSMART', 'AEP', 'BRC', '12:00', 140, 140000.00, 3800.00, N'C4'),
    (N'JA2043', N'JetSMART', 'BRC', 'AEP', '15:10', 140, 140000.00, 3800.00, N'G4'),
    (N'AR2044', N'Aerolíneas Argentinas', 'AEP', 'BRC', '16:30', 140, 140000.00, 3800.00, N'C5'),
    (N'AR2045', N'Aerolíneas Argentinas', 'BRC', 'AEP', '19:40', 140, 140000.00, 3800.00, N'G5'),
    (N'FB2060', N'Flybondi', 'AEP', 'IGR', '07:20', 110, 120000.00, 3700.00, N'D4'),
    (N'FB2061', N'Flybondi', 'IGR', 'AEP', '10:00', 110, 120000.00, 3700.00, N'H4'),
    (N'AR2062', N'Aerolíneas Argentinas', 'AEP', 'IGR', '10:10', 110, 120000.00, 3700.00, N'D5'),
    (N'AR2063', N'Aerolíneas Argentinas', 'IGR', 'AEP', '12:50', 110, 120000.00, 3700.00, N'H5'),
    (N'AR2064', N'Aerolíneas Argentinas', 'AEP', 'IGR', '17:40', 110, 120000.00, 3700.00, N'D6'),
    (N'AR2065', N'Aerolíneas Argentinas', 'IGR', 'AEP', '20:20', 110, 120000.00, 3700.00, N'H6'),
    (N'AR2080', N'Aerolíneas Argentinas', 'AEP', 'SLA', '07:45', 130, 125000.00, 3700.00, N'A5'),
    (N'AR2081', N'Aerolíneas Argentinas', 'SLA', 'AEP', '10:45', 130, 125000.00, 3700.00, N'E5'),
    (N'JA2082', N'JetSMART', 'AEP', 'SLA', '13:30', 130, 125000.00, 3700.00, N'A6'),
    (N'JA2083', N'JetSMART', 'SLA', 'AEP', '16:30', 130, 125000.00, 3700.00, N'E6'),
    (N'AR2100', N'Aerolíneas Argentinas', 'AEP', 'NQN', '08:30', 115, 115000.00, 3600.00, N'B6'),
    (N'AR2101', N'Aerolíneas Argentinas', 'NQN', 'AEP', '11:15', 115, 115000.00, 3600.00, N'F6'),
    (N'JA2102', N'JetSMART', 'AEP', 'NQN', '18:45', 115, 115000.00, 3600.00, N'B7'),
    (N'JA2103', N'JetSMART', 'NQN', 'AEP', '21:30', 115, 115000.00, 3600.00, N'F7'),
    (N'AR2120', N'Aerolíneas Argentinas', 'AEP', 'USH', '06:40', 215, 190000.00, 4200.00, N'C7'),
    (N'AR2121', N'Aerolíneas Argentinas', 'USH', 'AEP', '11:05', 215, 190000.00, 4200.00, N'G7'),
    (N'AR2122', N'Aerolíneas Argentinas', 'AEP', 'USH', '13:15', 215, 190000.00, 4200.00, N'C8'),
    (N'AR2123', N'Aerolíneas Argentinas', 'USH', 'AEP', '17:40', 215, 190000.00, 4200.00, N'G8'),
    (N'AR2140', N'Aerolíneas Argentinas', 'AEP', 'TUC', '09:40', 115, 118000.00, 3600.00, N'D8'),
    (N'AR2141', N'Aerolíneas Argentinas', 'TUC', 'AEP', '12:25', 115, 118000.00, 3600.00, N'H8'),
    (N'FB2142', N'Flybondi', 'AEP', 'TUC', '20:30', 115, 118000.00, 3600.00, N'D9'),
    (N'FB2143', N'Flybondi', 'TUC', 'AEP', '23:15', 115, 118000.00, 3600.00, N'H9'),
    (N'AR2160', N'Aerolíneas Argentinas', 'AEP', 'MDQ', '11:00', 65, 70000.00, 3000.00, N'A9'),
    (N'AR2161', N'Aerolíneas Argentinas', 'MDQ', 'AEP', '12:55', 65, 70000.00, 3000.00, N'E9'),
    (N'FB2162', N'Flybondi', 'AEP', 'MDQ', '19:50', 65, 70000.00, 3000.00, N'A1'),
    (N'FB2163', N'Flybondi', 'MDQ', 'AEP', '21:45', 65, 70000.00, 3000.00, N'E1'),
    (N'AR2180', N'Aerolíneas Argentinas', 'AEP', 'FTE', '07:10', 195, 185000.00, 4200.00, N'B1'),
    (N'AR2181', N'Aerolíneas Argentinas', 'FTE', 'AEP', '11:15', 195, 185000.00, 4200.00, N'F1'),
    (N'JA2182', N'JetSMART', 'AEP', 'FTE', '11:40', 195, 185000.00, 4200.00, N'B2'),
    (N'JA2183', N'JetSMART', 'FTE', 'AEP', '15:45', 195, 185000.00, 4200.00, N'F2'),
    (N'AR2200', N'Aerolíneas Argentinas', 'EZE', 'IGR', '12:40', 110, 125000.00, 3800.00, N'C2'),
    (N'AR2201', N'Aerolíneas Argentinas', 'IGR', 'EZE', '15:20', 110, 125000.00, 3800.00, N'G2'),
    (N'FB2220', N'Flybondi', 'EZE', 'BRC', '10:20', 140, 145000.00, 3800.00, N'D3'),
    (N'FB2221', N'Flybondi', 'BRC', 'EZE', '13:30', 140, 145000.00, 3800.00, N'H3'),
    (N'JA2240', N'JetSMART', 'EZE', 'MDZ', '15:10', 115, 108000.00, 3600.00, N'A4'),
    (N'JA2241', N'JetSMART', 'MDZ', 'EZE', '17:55', 115, 108000.00, 3600.00, N'E4'),
    (N'AR2260', N'Aerolíneas Argentinas', 'COR', 'MDZ', '10:30', 75, 80000.00, 3200.00, N'B5'),
    (N'AR2261', N'Aerolíneas Argentinas', 'MDZ', 'COR', '12:35', 75, 80000.00, 3200.00, N'F5'),
    (N'JA2262', N'JetSMART', 'COR', 'MDZ', '17:30', 75, 80000.00, 3200.00, N'B6'),
    (N'JA2263', N'JetSMART', 'MDZ', 'COR', '19:35', 75, 80000.00, 3200.00, N'F6'),
    (N'JA2280', N'JetSMART', 'COR', 'BRC', '09:50', 120, 120000.00, 3500.00, N'C6'),
    (N'JA2281', N'JetSMART', 'BRC', 'COR', '12:40', 120, 120000.00, 3500.00, N'G6'),
    (N'AR2300', N'Aerolíneas Argentinas', 'COR', 'SLA', '14:20', 90, 95000.00, 3300.00, N'D7'),
    (N'AR2301', N'Aerolíneas Argentinas', 'SLA', 'COR', '16:40', 90, 95000.00, 3300.00, N'H7'),
    (N'FB2320', N'Flybondi', 'COR', 'IGR', '11:15', 105, 110000.00, 3400.00, N'A8'),
    (N'FB2321', N'Flybondi', 'IGR', 'COR', '13:50', 105, 110000.00, 3400.00, N'E8'),
    (N'JA2340', N'JetSMART', 'MDZ', 'BRC', '12:20', 110, 115000.00, 3500.00, N'B9'),
    (N'JA2341', N'JetSMART', 'BRC', 'MDZ', '15:00', 110, 115000.00, 3500.00, N'F9'),
    (N'AR2360', N'Aerolíneas Argentinas', 'BRC', 'FTE', '15:30', 80, 105000.00, 3600.00, N'C1'),
    (N'AR2361', N'Aerolíneas Argentinas', 'FTE', 'BRC', '17:40', 80, 105000.00, 3600.00, N'G1'),
    (N'AR2380', N'Aerolíneas Argentinas', 'USH', 'FTE', '16:40', 70, 95000.00, 3600.00, N'D2'),
    (N'AR2381', N'Aerolíneas Argentinas', 'FTE', 'USH', '18:40', 70, 95000.00, 3600.00, N'H2');

/* ---------- Plan de vuelos: frecuencias x días ---------- */
IF OBJECT_ID('tempdb..#Plan') IS NOT NULL DROP TABLE #Plan;

;WITH Dias AS (
    SELECT TOP (@Dias) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS N
    FROM sys.all_objects
)
SELECT
    F.Codigo,
    AL.Id AS IdAerolinea,
    O.Id  AS IdOrigen,
    D.Id  AS IdDestino,
    CAST(DATEADD(MINUTE, DATEDIFF(MINUTE, CAST('00:00' AS TIME(0)), F.Hora),
         CAST(DATEADD(DAY, Dias.N, CAST(GETDATE() AS DATE)) AS DATETIME2(0))) AS DATETIME2(0)) AS Salida,
    F.DuracionMin,
    F.Puerta,
    F.CostoKilo,
    -- Precio con variación: +15 % viernes y domingos, y entre -10 % y +10 % según el día (determinístico).
    CAST(ROUND(F.PrecioEconomica
         * (CASE WHEN DATEDIFF(DAY, '19000101', DATEADD(DAY, Dias.N, CAST(GETDATE() AS DATE))) % 7 IN (4, 6)
                 THEN 1.15 ELSE 1.00 END)   -- 4 = viernes, 6 = domingo (independiente del idioma del servidor)
         * (0.90 + ABS(CHECKSUM(F.Codigo, Dias.N) % 21) / 100.0), -2) AS DECIMAL(12,2)) AS PrecioEconomica
INTO #Plan
FROM @Frecuencias F
CROSS JOIN Dias
INNER JOIN dbo.Aerolinea  AL ON AL.Nombre = F.Aerolinea
INNER JOIN dbo.Aeropuerto O  ON O.CodigoIata = F.Origen
INNER JOIN dbo.Aeropuerto D  ON D.CodigoIata = F.Destino;

-- No duplicar vuelos ya cargados (mismo código y misma salida).
DELETE P FROM #Plan P
WHERE EXISTS (SELECT 1 FROM dbo.Vuelo V WHERE V.CodigoVuelo = P.Codigo AND V.FechaHoraSalida = P.Salida);

/* ---------- Vuelos ---------- */
INSERT INTO dbo.Vuelo (CodigoVuelo, IdAerolinea, IdOrigen, IdDestino, FechaHoraSalida, FechaHoraLlegada,
                       PuertaEmbarque, CostoKiloExceso, BorradoLogico)
SELECT Codigo, IdAerolinea, IdOrigen, IdDestino, Salida, DATEADD(MINUTE, DuracionMin, Salida),
       Puerta, CostoKilo, 0
FROM #Plan;

/* ---------- Clases de cada vuelo ---------- */
INSERT INTO dbo.VueloClase (IdVuelo, IdClase, PrecioBase, CapacidadAsientos, AsientosReservados, FranquiciaEquipajeKg)
SELECT V.Id, C.IdClase, CAST(ROUND(P.PrecioEconomica * C.Factor, -2) AS DECIMAL(12,2)), C.Capacidad, 0, C.Franquicia
FROM #Plan P
INNER JOIN dbo.Vuelo V ON V.CodigoVuelo = P.Codigo AND V.FechaHoraSalida = P.Salida
CROSS JOIN (VALUES (1, 1.0, 84, 15.00), (2, 2.2, 24, 23.00), (3, 3.5, 12, 32.00))
     AS C (IdClase, Factor, Capacidad, Franquicia)
WHERE NOT EXISTS (SELECT 1 FROM dbo.VueloClase X WHERE X.IdVuelo = V.Id AND X.IdClase = C.IdClase);

/* ---------- Asientos (mismo mapa que los vuelos existentes) ---------- */
INSERT INTO dbo.Asiento (IdVuelo, Fila, Letra, IdClase, Ubicacion)
SELECT V.Id, F.Fila, L.Letra,
       CASE WHEN F.Fila <= 2 THEN 3 WHEN F.Fila <= 6 THEN 2 ELSE 1 END,
       L.Ubicacion
FROM #Plan P
INNER JOIN dbo.Vuelo V ON V.CodigoVuelo = P.Codigo AND V.FechaHoraSalida = P.Salida
CROSS JOIN (SELECT TOP (20) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS Fila FROM sys.all_objects) AS F
CROSS JOIN (VALUES ('A', N'Ventana'), ('B', N'Central'), ('C', N'Pasillo'),
                   ('D', N'Pasillo'), ('E', N'Central'), ('F', N'Ventana')) AS L (Letra, Ubicacion)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Asiento X WHERE X.IdVuelo = V.Id AND X.Fila = F.Fila AND X.Letra = L.Letra);

DECLARE @Creados INT = (SELECT COUNT(*) FROM #Plan);
DROP TABLE #Plan;

COMMIT TRANSACTION;

PRINT CONCAT('Vuelos nuevos creados: ', @Creados);
GO
