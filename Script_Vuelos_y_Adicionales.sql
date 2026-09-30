/* =====================================================================================
   Reservas: precio de lista de adicionales + vuelos adicionales
   ---------------------------------------------------------------------------------
   Ejecutar UNA vez sobre la base existente "Gestion Usuario" (SSMS o LocalDB).
   Si la base se crea de cero con EsquemaCompleto.sql no hace falta: ya lo incluye.
   Cada vuelo tiene código único: aerolínea + frecuencia + fecha (ej. AR05261015).
   Se puede volver a correr sin duplicar nada (por ejemplo, más adelante, para
   generar vuelos de las semanas siguientes).

   1) TipoAdicional.PrecioUnitario: precio de lista de cada servicio adicional.
      OBLIGATORIO para esta versión del sistema (la pantalla de reserva lo lee).
   2) Vuelos para los próximos @Dias días (a partir de mañana) entre TODOS los aeropuertos:
      130 rutas (todas las combinaciones de los 12 aeropuertos, salvo AEP <-> EZE, que están
      en la misma ciudad), 160 vuelos por día, 3 aerolíneas. Las rutas troncales tienen
      varias frecuencias; el resto, un vuelo diario por sentido (duración y precio según
      la distancia). Cada vuelo con sus 3 clases (Económica 84 / Ejecutiva 24 / Primera 12
      asientos) y sus 120 asientos (filas 1-2 Primera, 3-6 Ejecutiva, 7-20 Económica,
      letras A-F), igual que los vuelos existentes. Agrega 6 aeropuertos: SLA, NQN, USH,
      TUC, MDQ, FTE.
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
    Slot INT,                  -- número de frecuencia por aerolínea (00-99), forma parte del código de vuelo
    Codigo NVARCHAR(10), Aerolinea NVARCHAR(80), Origen CHAR(3), Destino CHAR(3),
    Hora TIME(0), DuracionMin INT, PrecioEconomica DECIMAL(12,2), CostoKilo DECIMAL(10,2), Puerta NVARCHAR(10));

INSERT INTO @Frecuencias (Slot, Codigo, Aerolinea, Origen, Destino, Hora, DuracionMin, PrecioEconomica, CostoKilo, Puerta) VALUES
    (0, N'AR2000', N'Aerolíneas Argentinas', 'AEP', 'COR', '07:00', 80, 95000.00, 3500.00, N'A1'),
    (1, N'AR2001', N'Aerolíneas Argentinas', 'COR', 'AEP', '09:10', 80, 95000.00, 3500.00, N'E1'),
    (2, N'FB2002', N'Flybondi', 'AEP', 'COR', '12:30', 80, 95000.00, 3500.00, N'A2'),
    (3, N'FB2003', N'Flybondi', 'COR', 'AEP', '14:40', 80, 95000.00, 3500.00, N'E2'),
    (4, N'AR2004', N'Aerolíneas Argentinas', 'AEP', 'COR', '18:40', 80, 95000.00, 3500.00, N'A3'),
    (5, N'AR2005', N'Aerolíneas Argentinas', 'COR', 'AEP', '20:50', 80, 95000.00, 3500.00, N'E3'),
    (6, N'AR2006', N'Aerolíneas Argentinas', 'AEP', 'MDZ', '08:15', 115, 110000.00, 3600.00, N'B2'),
    (7, N'AR2007', N'Aerolíneas Argentinas', 'MDZ', 'AEP', '11:00', 115, 110000.00, 3600.00, N'F2'),
    (8, N'FB2008', N'Flybondi', 'AEP', 'MDZ', '14:00', 115, 110000.00, 3600.00, N'B3'),
    (9, N'FB2009', N'Flybondi', 'MDZ', 'AEP', '16:45', 115, 110000.00, 3600.00, N'F3'),
    (10, N'JA2010', N'JetSMART', 'AEP', 'MDZ', '20:10', 115, 110000.00, 3600.00, N'B4'),
    (11, N'JA2011', N'JetSMART', 'MDZ', 'AEP', '22:55', 115, 110000.00, 3600.00, N'F4'),
    (12, N'AR2012', N'Aerolíneas Argentinas', 'AEP', 'BRC', '09:00', 140, 140000.00, 3800.00, N'C3'),
    (13, N'AR2013', N'Aerolíneas Argentinas', 'BRC', 'AEP', '12:10', 140, 140000.00, 3800.00, N'G3'),
    (14, N'JA2014', N'JetSMART', 'AEP', 'BRC', '12:00', 140, 140000.00, 3800.00, N'C4'),
    (15, N'JA2015', N'JetSMART', 'BRC', 'AEP', '15:10', 140, 140000.00, 3800.00, N'G4'),
    (16, N'AR2016', N'Aerolíneas Argentinas', 'AEP', 'BRC', '16:30', 140, 140000.00, 3800.00, N'C5'),
    (17, N'AR2017', N'Aerolíneas Argentinas', 'BRC', 'AEP', '19:40', 140, 140000.00, 3800.00, N'G5'),
    (18, N'FB2018', N'Flybondi', 'AEP', 'IGR', '07:20', 110, 120000.00, 3700.00, N'D4'),
    (19, N'FB2019', N'Flybondi', 'IGR', 'AEP', '10:00', 110, 120000.00, 3700.00, N'H4'),
    (20, N'AR2020', N'Aerolíneas Argentinas', 'AEP', 'IGR', '10:10', 110, 120000.00, 3700.00, N'D5'),
    (21, N'AR2021', N'Aerolíneas Argentinas', 'IGR', 'AEP', '12:50', 110, 120000.00, 3700.00, N'H5'),
    (22, N'AR2022', N'Aerolíneas Argentinas', 'AEP', 'IGR', '17:40', 110, 120000.00, 3700.00, N'D6'),
    (23, N'AR2023', N'Aerolíneas Argentinas', 'IGR', 'AEP', '20:20', 110, 120000.00, 3700.00, N'H6'),
    (24, N'AR2024', N'Aerolíneas Argentinas', 'AEP', 'SLA', '07:45', 130, 125000.00, 3700.00, N'A5'),
    (25, N'AR2025', N'Aerolíneas Argentinas', 'SLA', 'AEP', '10:45', 130, 125000.00, 3700.00, N'E5'),
    (26, N'JA2026', N'JetSMART', 'AEP', 'SLA', '13:30', 130, 125000.00, 3700.00, N'A6'),
    (27, N'JA2027', N'JetSMART', 'SLA', 'AEP', '16:30', 130, 125000.00, 3700.00, N'E6'),
    (28, N'AR2028', N'Aerolíneas Argentinas', 'AEP', 'NQN', '08:30', 115, 115000.00, 3600.00, N'B6'),
    (29, N'AR2029', N'Aerolíneas Argentinas', 'NQN', 'AEP', '11:15', 115, 115000.00, 3600.00, N'F6'),
    (30, N'JA2030', N'JetSMART', 'AEP', 'NQN', '18:45', 115, 115000.00, 3600.00, N'B7'),
    (31, N'JA2031', N'JetSMART', 'NQN', 'AEP', '21:30', 115, 115000.00, 3600.00, N'F7'),
    (32, N'AR2032', N'Aerolíneas Argentinas', 'AEP', 'USH', '06:40', 215, 190000.00, 4200.00, N'C7'),
    (33, N'AR2033', N'Aerolíneas Argentinas', 'USH', 'AEP', '11:05', 215, 190000.00, 4200.00, N'G7'),
    (34, N'AR2034', N'Aerolíneas Argentinas', 'AEP', 'USH', '13:15', 215, 190000.00, 4200.00, N'C8'),
    (35, N'AR2035', N'Aerolíneas Argentinas', 'USH', 'AEP', '17:40', 215, 190000.00, 4200.00, N'G8'),
    (36, N'AR2036', N'Aerolíneas Argentinas', 'AEP', 'TUC', '09:40', 115, 118000.00, 3600.00, N'D8'),
    (37, N'AR2037', N'Aerolíneas Argentinas', 'TUC', 'AEP', '12:25', 115, 118000.00, 3600.00, N'H8'),
    (38, N'FB2038', N'Flybondi', 'AEP', 'TUC', '20:30', 115, 118000.00, 3600.00, N'D9'),
    (39, N'FB2039', N'Flybondi', 'TUC', 'AEP', '23:15', 115, 118000.00, 3600.00, N'H9'),
    (40, N'AR2040', N'Aerolíneas Argentinas', 'AEP', 'MDQ', '11:00', 65, 70000.00, 3000.00, N'A9'),
    (41, N'AR2041', N'Aerolíneas Argentinas', 'MDQ', 'AEP', '12:55', 65, 70000.00, 3000.00, N'E9'),
    (42, N'FB2042', N'Flybondi', 'AEP', 'MDQ', '19:50', 65, 70000.00, 3000.00, N'A1'),
    (43, N'FB2043', N'Flybondi', 'MDQ', 'AEP', '21:45', 65, 70000.00, 3000.00, N'E1'),
    (44, N'AR2044', N'Aerolíneas Argentinas', 'AEP', 'FTE', '07:10', 195, 185000.00, 4200.00, N'B1'),
    (45, N'AR2045', N'Aerolíneas Argentinas', 'FTE', 'AEP', '11:15', 195, 185000.00, 4200.00, N'F1'),
    (46, N'JA2046', N'JetSMART', 'AEP', 'FTE', '11:40', 195, 185000.00, 4200.00, N'B2'),
    (47, N'JA2047', N'JetSMART', 'FTE', 'AEP', '15:45', 195, 185000.00, 4200.00, N'F2'),
    (48, N'AR2048', N'Aerolíneas Argentinas', 'EZE', 'IGR', '12:40', 110, 125000.00, 3800.00, N'C2'),
    (49, N'AR2049', N'Aerolíneas Argentinas', 'IGR', 'EZE', '15:20', 110, 125000.00, 3800.00, N'G2'),
    (50, N'FB2050', N'Flybondi', 'EZE', 'BRC', '10:20', 140, 145000.00, 3800.00, N'D3'),
    (51, N'FB2051', N'Flybondi', 'BRC', 'EZE', '13:30', 140, 145000.00, 3800.00, N'H3'),
    (52, N'JA2052', N'JetSMART', 'EZE', 'MDZ', '15:10', 115, 108000.00, 3600.00, N'A4'),
    (53, N'JA2053', N'JetSMART', 'MDZ', 'EZE', '17:55', 115, 108000.00, 3600.00, N'E4'),
    (54, N'AR2054', N'Aerolíneas Argentinas', 'COR', 'MDZ', '10:30', 75, 80000.00, 3200.00, N'B5'),
    (55, N'AR2055', N'Aerolíneas Argentinas', 'MDZ', 'COR', '12:35', 75, 80000.00, 3200.00, N'F5'),
    (56, N'JA2056', N'JetSMART', 'COR', 'MDZ', '17:30', 75, 80000.00, 3200.00, N'B6'),
    (57, N'JA2057', N'JetSMART', 'MDZ', 'COR', '19:35', 75, 80000.00, 3200.00, N'F6'),
    (58, N'JA2058', N'JetSMART', 'COR', 'BRC', '09:50', 120, 120000.00, 3500.00, N'C6'),
    (59, N'JA2059', N'JetSMART', 'BRC', 'COR', '12:40', 120, 120000.00, 3500.00, N'G6'),
    (60, N'AR2060', N'Aerolíneas Argentinas', 'COR', 'SLA', '14:20', 90, 95000.00, 3300.00, N'D7'),
    (61, N'AR2061', N'Aerolíneas Argentinas', 'SLA', 'COR', '16:40', 90, 95000.00, 3300.00, N'H7'),
    (62, N'FB2062', N'Flybondi', 'COR', 'IGR', '11:15', 105, 110000.00, 3400.00, N'A8'),
    (63, N'FB2063', N'Flybondi', 'IGR', 'COR', '13:50', 105, 110000.00, 3400.00, N'E8'),
    (64, N'JA2064', N'JetSMART', 'MDZ', 'BRC', '12:20', 110, 115000.00, 3500.00, N'B9'),
    (65, N'JA2065', N'JetSMART', 'BRC', 'MDZ', '15:00', 110, 115000.00, 3500.00, N'F9'),
    (66, N'AR2066', N'Aerolíneas Argentinas', 'BRC', 'FTE', '15:30', 80, 105000.00, 3600.00, N'C1'),
    (67, N'AR2067', N'Aerolíneas Argentinas', 'FTE', 'BRC', '17:40', 80, 105000.00, 3600.00, N'G1'),
    (68, N'AR2068', N'Aerolíneas Argentinas', 'USH', 'FTE', '16:40', 70, 95000.00, 3600.00, N'D2'),
    (69, N'AR2069', N'Aerolíneas Argentinas', 'FTE', 'USH', '18:40', 70, 95000.00, 3600.00, N'H2'),
    (70, N'AR2070', N'Aerolíneas Argentinas', 'EZE', 'COR', '06:50', 75, 96000.00, 3300.00, N'A1'),
    (71, N'AR2071', N'Aerolíneas Argentinas', 'COR', 'EZE', '08:55', 75, 96000.00, 3300.00, N'E1'),
    (70, N'FB2070', N'Flybondi', 'EZE', 'SLA', '08:20', 125, 131000.00, 3600.00, N'B2'),
    (71, N'FB2071', N'Flybondi', 'SLA', 'EZE', '11:15', 125, 131000.00, 3600.00, N'F2'),
    (70, N'JA2070', N'JetSMART', 'EZE', 'NQN', '09:35', 100, 113000.00, 3500.00, N'C3'),
    (71, N'JA2071', N'JetSMART', 'NQN', 'EZE', '12:05', 100, 113000.00, 3500.00, N'G3'),
    (72, N'AR2072', N'Aerolíneas Argentinas', 'EZE', 'USH', '11:05', 205, 189000.00, 4200.00, N'D4'),
    (73, N'AR2073', N'Aerolíneas Argentinas', 'USH', 'EZE', '15:20', 205, 189000.00, 4200.00, N'H4'),
    (72, N'FB2072', N'Flybondi', 'EZE', 'TUC', '12:45', 110, 120000.00, 3500.00, N'A5'),
    (73, N'FB2073', N'Flybondi', 'TUC', 'EZE', '15:25', 110, 120000.00, 3500.00, N'E5'),
    (72, N'JA2072', N'JetSMART', 'EZE', 'MDQ', '14:10', 50, 80000.00, 3200.00, N'B6'),
    (73, N'JA2073', N'JetSMART', 'MDQ', 'EZE', '15:50', 50, 80000.00, 3200.00, N'F6'),
    (74, N'AR2074', N'Aerolíneas Argentinas', 'EZE', 'FTE', '15:40', 180, 172000.00, 4000.00, N'C7'),
    (75, N'AR2075', N'Aerolíneas Argentinas', 'FTE', 'EZE', '19:30', 180, 172000.00, 4000.00, N'G7'),
    (74, N'FB2074', N'Flybondi', 'COR', 'NQN', '17:15', 95, 111000.00, 3500.00, N'D8'),
    (75, N'FB2075', N'Flybondi', 'NQN', 'COR', '19:40', 95, 111000.00, 3500.00, N'H8'),
    (74, N'JA2074', N'JetSMART', 'COR', 'USH', '06:50', 230, 205000.00, 4300.00, N'A9'),
    (75, N'JA2075', N'JetSMART', 'USH', 'COR', '11:30', 230, 205000.00, 4300.00, N'E9'),
    (76, N'AR2076', N'Aerolíneas Argentinas', 'COR', 'TUC', '08:20', 65, 88000.00, 3300.00, N'B1'),
    (77, N'AR2077', N'Aerolíneas Argentinas', 'TUC', 'COR', '10:15', 65, 88000.00, 3300.00, N'F1'),
    (76, N'FB2076', N'Flybondi', 'COR', 'MDQ', '09:35', 100, 112000.00, 3500.00, N'C2'),
    (77, N'FB2077', N'Flybondi', 'MDQ', 'COR', '12:05', 100, 112000.00, 3500.00, N'G2'),
    (76, N'JA2076', N'JetSMART', 'COR', 'FTE', '11:05', 195, 181000.00, 4100.00, N'D3'),
    (77, N'JA2077', N'JetSMART', 'FTE', 'COR', '15:10', 195, 181000.00, 4100.00, N'H3'),
    (78, N'AR2078', N'Aerolíneas Argentinas', 'MDZ', 'IGR', '12:45', 150, 148000.00, 3800.00, N'A4'),
    (79, N'AR2079', N'Aerolíneas Argentinas', 'IGR', 'MDZ', '16:05', 150, 148000.00, 3800.00, N'E4'),
    (78, N'FB2078', N'Flybondi', 'MDZ', 'SLA', '14:10', 100, 112000.00, 3500.00, N'B5'),
    (79, N'FB2079', N'Flybondi', 'SLA', 'MDZ', '16:40', 100, 112000.00, 3500.00, N'F5'),
    (78, N'JA2078', N'JetSMART', 'MDZ', 'NQN', '15:40', 80, 98000.00, 3300.00, N'C6'),
    (79, N'JA2079', N'JetSMART', 'NQN', 'MDZ', '17:50', 80, 98000.00, 3300.00, N'G6'),
    (80, N'AR2080', N'Aerolíneas Argentinas', 'MDZ', 'USH', '17:15', 215, 195000.00, 4200.00, N'D7'),
    (81, N'AR2081', N'Aerolíneas Argentinas', 'USH', 'MDZ', '21:40', 215, 195000.00, 4200.00, N'H7'),
    (80, N'FB2080', N'Flybondi', 'MDZ', 'TUC', '06:50', 85, 102000.00, 3400.00, N'A8'),
    (81, N'FB2081', N'Flybondi', 'TUC', 'MDZ', '09:05', 85, 102000.00, 3400.00, N'E8'),
    (80, N'JA2080', N'JetSMART', 'MDZ', 'MDQ', '08:20', 115, 124000.00, 3600.00, N'B9'),
    (81, N'JA2081', N'JetSMART', 'MDQ', 'MDZ', '11:05', 115, 124000.00, 3600.00, N'F9'),
    (82, N'AR2082', N'Aerolíneas Argentinas', 'MDZ', 'FTE', '09:35', 175, 168000.00, 4000.00, N'C1'),
    (83, N'AR2083', N'Aerolíneas Argentinas', 'FTE', 'MDZ', '13:20', 175, 168000.00, 4000.00, N'G1'),
    (82, N'FB2082', N'Flybondi', 'BRC', 'IGR', '11:05', 200, 187000.00, 4200.00, N'D2'),
    (83, N'FB2083', N'Flybondi', 'IGR', 'BRC', '15:15', 200, 187000.00, 4200.00, N'H2'),
    (82, N'JA2082', N'JetSMART', 'BRC', 'SLA', '12:45', 170, 164000.00, 3900.00, N'A3'),
    (83, N'JA2083', N'JetSMART', 'SLA', 'BRC', '16:25', 170, 164000.00, 3900.00, N'E3'),
    (84, N'AR2084', N'Aerolíneas Argentinas', 'BRC', 'NQN', '14:10', 50, 79000.00, 3200.00, N'B4'),
    (85, N'AR2085', N'Aerolíneas Argentinas', 'NQN', 'BRC', '15:50', 50, 79000.00, 3200.00, N'F4'),
    (84, N'FB2084', N'Flybondi', 'BRC', 'USH', '15:40', 145, 145000.00, 3800.00, N'C5'),
    (85, N'FB2085', N'Flybondi', 'USH', 'BRC', '18:55', 145, 145000.00, 3800.00, N'G5'),
    (84, N'JA2084', N'JetSMART', 'BRC', 'TUC', '17:15', 155, 153000.00, 3800.00, N'D6'),
    (85, N'JA2085', N'JetSMART', 'TUC', 'BRC', '20:40', 155, 153000.00, 3800.00, N'H6'),
    (86, N'AR2086', N'Aerolíneas Argentinas', 'BRC', 'MDQ', '06:50', 120, 127000.00, 3600.00, N'A7'),
    (87, N'AR2087', N'Aerolíneas Argentinas', 'MDQ', 'BRC', '09:40', 120, 127000.00, 3600.00, N'E7'),
    (86, N'FB2086', N'Flybondi', 'IGR', 'SLA', '08:20', 110, 121000.00, 3600.00, N'B8'),
    (87, N'FB2087', N'Flybondi', 'SLA', 'IGR', '11:00', 110, 121000.00, 3600.00, N'F8'),
    (86, N'JA2086', N'JetSMART', 'IGR', 'NQN', '09:35', 175, 167000.00, 4000.00, N'C9'),
    (87, N'JA2087', N'JetSMART', 'NQN', 'IGR', '13:20', 175, 167000.00, 4000.00, N'G9'),
    (88, N'AR2088', N'Aerolíneas Argentinas', 'IGR', 'USH', '11:05', 290, 249000.00, 4700.00, N'D1'),
    (89, N'AR2089', N'Aerolíneas Argentinas', 'USH', 'IGR', '16:45', 290, 249000.00, 4700.00, N'H1'),
    (88, N'FB2088', N'Flybondi', 'IGR', 'TUC', '12:45', 105, 119000.00, 3500.00, N'A2'),
    (89, N'FB2089', N'Flybondi', 'TUC', 'IGR', '15:20', 105, 119000.00, 3500.00, N'E2'),
    (88, N'JA2088', N'JetSMART', 'IGR', 'MDQ', '14:10', 130, 136000.00, 3700.00, N'B3'),
    (89, N'JA2089', N'JetSMART', 'MDQ', 'IGR', '17:10', 130, 136000.00, 3700.00, N'F3'),
    (90, N'AR2090', N'Aerolíneas Argentinas', 'IGR', 'FTE', '15:40', 265, 231000.00, 4600.00, N'C4'),
    (91, N'AR2091', N'Aerolíneas Argentinas', 'FTE', 'IGR', '20:55', 265, 231000.00, 4600.00, N'G4'),
    (90, N'FB2090', N'Flybondi', 'SLA', 'NQN', '17:15', 145, 147000.00, 3800.00, N'D5'),
    (91, N'FB2091', N'Flybondi', 'NQN', 'SLA', '20:30', 145, 147000.00, 3800.00, N'H5'),
    (90, N'JA2090', N'JetSMART', 'SLA', 'USH', '06:50', 280, 244000.00, 4700.00, N'A6'),
    (91, N'JA2091', N'JetSMART', 'USH', 'SLA', '12:20', 280, 244000.00, 4700.00, N'E6'),
    (92, N'AR2092', N'Aerolíneas Argentinas', 'SLA', 'TUC', '08:20', 40, 72000.00, 3100.00, N'B7'),
    (93, N'AR2093', N'Aerolíneas Argentinas', 'TUC', 'SLA', '09:50', 40, 72000.00, 3100.00, N'F7'),
    (92, N'FB2092', N'Flybondi', 'SLA', 'MDQ', '09:35', 150, 150000.00, 3800.00, N'C8'),
    (93, N'FB2093', N'Flybondi', 'MDQ', 'SLA', '12:55', 150, 150000.00, 3800.00, N'G8'),
    (92, N'JA2092', N'JetSMART', 'SLA', 'FTE', '11:05', 245, 219000.00, 4400.00, N'D9'),
    (93, N'JA2093', N'JetSMART', 'FTE', 'SLA', '16:00', 245, 219000.00, 4400.00, N'H9'),
    (94, N'AR2094', N'Aerolíneas Argentinas', 'NQN', 'USH', '12:45', 160, 157000.00, 3900.00, N'A1'),
    (95, N'AR2095', N'Aerolíneas Argentinas', 'USH', 'NQN', '16:15', 160, 157000.00, 3900.00, N'E1'),
    (94, N'FB2094', N'Flybondi', 'NQN', 'TUC', '14:10', 130, 136000.00, 3700.00, N'B2'),
    (95, N'FB2095', N'Flybondi', 'TUC', 'NQN', '17:10', 130, 136000.00, 3700.00, N'F2'),
    (94, N'JA2094', N'JetSMART', 'NQN', 'MDQ', '15:40', 95, 111000.00, 3500.00, N'C3'),
    (95, N'JA2095', N'JetSMART', 'MDQ', 'NQN', '18:05', 95, 111000.00, 3500.00, N'G3'),
    (96, N'AR2096', N'Aerolíneas Argentinas', 'NQN', 'FTE', '17:15', 125, 131000.00, 3600.00, N'D4'),
    (97, N'AR2097', N'Aerolíneas Argentinas', 'FTE', 'NQN', '20:10', 125, 131000.00, 3600.00, N'H4'),
    (96, N'FB2096', N'Flybondi', 'USH', 'TUC', '06:50', 265, 232000.00, 4600.00, N'A5'),
    (97, N'FB2097', N'Flybondi', 'TUC', 'USH', '12:05', 265, 232000.00, 4600.00, N'E5'),
    (96, N'JA2096', N'JetSMART', 'USH', 'MDQ', '08:20', 180, 173000.00, 4000.00, N'B6'),
    (97, N'JA2097', N'JetSMART', 'MDQ', 'USH', '12:10', 180, 173000.00, 4000.00, N'F6'),
    (98, N'AR2098', N'Aerolíneas Argentinas', 'TUC', 'MDQ', '09:35', 135, 138000.00, 3700.00, N'C7'),
    (99, N'AR2099', N'Aerolíneas Argentinas', 'MDQ', 'TUC', '12:40', 135, 138000.00, 3700.00, N'G7'),
    (98, N'FB2098', N'Flybondi', 'TUC', 'FTE', '11:05', 230, 207000.00, 4300.00, N'D8'),
    (99, N'FB2099', N'Flybondi', 'FTE', 'TUC', '15:45', 230, 207000.00, 4300.00, N'H8'),
    (98, N'JA2098', N'JetSMART', 'MDQ', 'FTE', '12:45', 165, 158000.00, 3900.00, N'A9'),
    (99, N'JA2099', N'JetSMART', 'FTE', 'MDQ', '16:20', 165, 158000.00, 3900.00, N'E9');

/* ---------- Plan de vuelos: frecuencias x días ---------- */
IF OBJECT_ID('tempdb..#Plan') IS NOT NULL DROP TABLE #Plan;

;WITH Dias AS (
    SELECT TOP (@Dias) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS N
    FROM sys.all_objects
)
SELECT
    -- CodigoVuelo es UNIQUE en la base: cada vuelo necesita su propio código.
    -- Formato (10 caracteres): aerolínea (2) + frecuencia (2) + fecha aammdd (6). Ej: AR05261015 = frecuencia 05 del 15/10/2026
    CAST(LEFT(F.Codigo, 2) + RIGHT('0' + CAST(F.Slot AS VARCHAR(2)), 2)
         + CONVERT(CHAR(6), DATEADD(DAY, Dias.N, CAST(GETDATE() AS DATE)), 12) AS NVARCHAR(10)) AS Codigo,
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

-- No duplicar: si el código ya existe (vuelo generado en una corrida anterior o cargado a mano), se saltea.
DELETE P FROM #Plan P
WHERE EXISTS (SELECT 1 FROM dbo.Vuelo V WHERE V.CodigoVuelo = P.Codigo);

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
