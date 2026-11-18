/* =====================================================================================
   DatosDemostracion.sql  -  carga datos completos de prueba en la base de FLY SAFE
   ---------------------------------------------------------------------------------
   Para qué: la base que crea el sistema al ejecutarlo desde el código (o el instalador sin
   datos de demostración) queda vacía: solo los roles y el administrador lautaro212. Este script
   la llena con datos realistas para poder probar todo apenas se abre el sistema.

   Cómo se usa:
     - Automático: cuando el sistema se ejecuta desde el código (por ejemplo, el zip de Git) y no
       encuentra la base, la crea y ejecuta este script. No hay que hacer nada.
     - A mano: sobre una base vacía, abrir este archivo en SQL Server Management Studio, ejecutarlo
       (F5) y volver a abrir el sistema.
     En los dos casos, al iniciar el sistema cifra los emails y recalcula los dígitos verificadores
     de las tablas cargadas (tareas pendientes), así la integridad da bien.
     El instalador no lo usa: instala la base sin datos (o con sus propios datos de demostración).

   Qué carga (todas las fechas son relativas al día en que se ejecuta):
     - Aerolíneas (4) y aeropuertos (15, incluidos Santiago de Chile, Montevideo y San Pablo).
     - Servicios adicionales: comida especial, asistencia prioritaria, embarque prioritario y
       seguro de viaje (además de los que ya trae el sistema).
     - Vuelos de los próximos 30 días (los genera el procedimiento del sistema) y vuelos pasados
       de los últimos dos años para el historial de los clientes.
     - 16 clientes con perfiles distintos para el reporte de millas: Diamante, Oro, Plata y Bronce,
       con riesgo de abandono, a punto de bajar de categoría, en aumento, con millas por vencer, etc.
     - Reservas confirmadas, pendientes de pago, canceladas con penalidad y reembolso, y vencidas
       sin pago; pagos, boletos, check-in realizados, pendientes y ausentes, y tarjetas de embarque.
     - Vuelos para hacer check-in ya mismo (mañana) y un cambio de puerta en dos vuelos para
       que la bitácora de cambios (Vuelo_C) tenga historial.
     - Usuarios de prueba, uno por rol (la contraseña de cada uno es igual a su nombre de usuario):
         vendedor1, vendedor2 (Vendedor) · checkin1 (EncargadoCheckIn) · gerente1 (Gerente)
         supervisor1 (Supervisor) · lfernandez (Cliente, es la clienta Laura Fernández)

   Se puede ejecutar una sola vez: si la base ya tiene reservas, no hace nada.
   ===================================================================================== */
USE [Gestion Usuario];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

/* ---------- 0) Controles ---------- */
IF OBJECT_ID(N'dbo.Reserva', N'U') IS NULL OR OBJECT_ID(N'dbo.TarifaFamilia', N'U') IS NULL
   OR OBJECT_ID(N'dbo.TareaPendiente_GV42', N'U') IS NULL
    THROW 50100, N'La base no tiene el negocio completo. Abrí primero el sistema para que la cree o la actualice.', 1;

IF EXISTS (SELECT 1 FROM dbo.Reserva)
    THROW 50101, N'La base ya tiene reservas: este script es solo para una base vacía. No se cargó nada.', 1;

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Nombre = N'Vendedor')
   OR NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Nombre = N'EncargadoCheckIn')
    THROW 50102, N'Faltan los roles del sistema (Vendedor, EncargadoCheckIn, ...). Abrí primero el sistema para que actualice la base.', 1;

DECLARE @Hoy   DATE         = CAST(GETDATE() AS DATE);
DECLARE @Ahora DATETIME2(0) = CAST(SYSDATETIME() AS DATETIME2(0));

BEGIN TRANSACTION;

/* =====================================================================================
   1) Catálogos
   ===================================================================================== */
INSERT INTO dbo.Aerolinea (Nombre)
SELECT A.Nombre
FROM (VALUES (N'Aerolíneas Argentinas'), (N'Flybondi'), (N'JetSMART'), (N'LATAM Airlines')) AS A (Nombre)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Aerolinea X WHERE X.Nombre = A.Nombre);

INSERT INTO dbo.Aeropuerto (CodigoIata, Nombre, Ciudad, Pais)
SELECT A.Iata, A.Nombre, A.Ciudad, A.Pais
FROM (VALUES
    ('AEP', N'Aeroparque Jorge Newbery',                           N'Buenos Aires',          N'Argentina'),
    ('EZE', N'Aeropuerto Internacional Ezeiza',                    N'Buenos Aires',          N'Argentina'),
    ('COR', N'Aeropuerto Internacional Pajas Blancas',             N'Córdoba',               N'Argentina'),
    ('MDZ', N'Aeropuerto Internacional El Plumerillo',             N'Mendoza',               N'Argentina'),
    ('BRC', N'Aeropuerto Internacional Teniente Candelaria',       N'Bariloche',             N'Argentina'),
    ('IGR', N'Aeropuerto Internacional Cataratas del Iguazú',      N'Puerto Iguazú',         N'Argentina'),
    ('SLA', N'Aeropuerto Internacional Martín Miguel de Güemes',   N'Salta',                 N'Argentina'),
    ('NQN', N'Aeropuerto Internacional Presidente Perón',          N'Neuquén',               N'Argentina'),
    ('USH', N'Aeropuerto Internacional Malvinas Argentinas',       N'Ushuaia',               N'Argentina'),
    ('TUC', N'Aeropuerto Internacional Teniente Benjamín Matienzo', N'San Miguel de Tucumán', N'Argentina'),
    ('MDQ', N'Aeropuerto Internacional Astor Piazzolla',           N'Mar del Plata',         N'Argentina'),
    ('FTE', N'Aeropuerto Internacional Comandante Armando Tola',   N'El Calafate',           N'Argentina'),
    ('SCL', N'Aeropuerto Internacional Arturo Merino Benítez',     N'Santiago',              N'Chile'),
    ('MVD', N'Aeropuerto Internacional de Carrasco',               N'Montevideo',            N'Uruguay'),
    ('GRU', N'Aeropuerto Internacional de Guarulhos',              N'San Pablo',             N'Brasil')
) AS A (Iata, Nombre, Ciudad, Pais)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Aeropuerto X WHERE X.CodigoIata = A.Iata);

-- Servicios adicionales que se eligen a mano en la reserva (los del sistema ya existen).
INSERT INTO dbo.TipoAdicional (Nombre, Activo, PrecioUnitario, MaxPorPasajero, SeleccionManual)
SELECT T.Nombre, 1, T.Precio, 1, 1
FROM (VALUES (N'Comida especial', 8000.00), (N'Asistencia prioritaria', 15000.00),
             (N'Embarque prioritario', 9000.00), (N'Seguro de viaje', 12000.00)) AS T (Nombre, Precio)
WHERE NOT EXISTS (SELECT 1 FROM dbo.TipoAdicional X WHERE X.Nombre = T.Nombre);

/* =====================================================================================
   2) Vuelos disponibles de los próximos 30 días (generador del sistema)
   La base sin datos tiene el generador apagado: se enciende y se ejecuta.
   ===================================================================================== */
IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE class = 0 AND name = N'FLYSAFE_GenerarVuelos')
    EXEC sys.sp_updateextendedproperty @name = N'FLYSAFE_GenerarVuelos', @value = N'1';
ELSE
    EXEC sys.sp_addextendedproperty @name = N'FLYSAFE_GenerarVuelos', @value = N'1';

IF OBJECT_ID(N'dbo.usp_GenerarVuelos_GV42', N'P') IS NOT NULL
    EXEC dbo.usp_GenerarVuelos_GV42 @Dias = 30;

/* =====================================================================================
   3) Rutas de los vuelos de la demostración
   Código de vuelo (10 caracteres, único): aerolínea (2) + frecuencia (2) + fecha aammdd (6).
   Se usan frecuencias 80 a 93, que el generador del sistema no usa.
   ===================================================================================== */
DECLARE @Rutas TABLE (
    Ruta CHAR(7) PRIMARY KEY, Aerolinea NVARCHAR(80), Prefijo CHAR(2), Slot INT,
    Origen CHAR(3), Destino CHAR(3), Hora TIME(0), DuracionMin INT,
    Precio DECIMAL(12,2), CostoKilo DECIMAL(10,2), Puerta NVARCHAR(10));

INSERT INTO @Rutas VALUES
    ('AEP-COR', N'Aerolíneas Argentinas', 'AR', 80, 'AEP', 'COR', '07:30',  80,  95000, 3500, N'A4'),
    ('COR-AEP', N'Aerolíneas Argentinas', 'AR', 81, 'COR', 'AEP', '19:30',  80,  95000, 3500, N'E4'),
    ('AEP-MDZ', N'Aerolíneas Argentinas', 'AR', 82, 'AEP', 'MDZ', '08:45', 115, 110000, 3600, N'B5'),
    ('MDZ-AEP', N'Aerolíneas Argentinas', 'AR', 83, 'MDZ', 'AEP', '20:15', 115, 110000, 3600, N'F5'),
    ('AEP-BRC', N'Aerolíneas Argentinas', 'AR', 84, 'AEP', 'BRC', '10:00', 140, 140000, 3800, N'C6'),
    ('BRC-AEP', N'Aerolíneas Argentinas', 'AR', 85, 'BRC', 'AEP', '17:30', 140, 140000, 3800, N'G6'),
    ('AEP-IGR', N'Flybondi',              'FB', 86, 'AEP', 'IGR', '08:00', 110, 120000, 3700, N'D7'),
    ('IGR-AEP', N'Flybondi',              'FB', 87, 'IGR', 'AEP', '18:20', 110, 120000, 3700, N'H7'),
    ('AEP-USH', N'Aerolíneas Argentinas', 'AR', 88, 'AEP', 'USH', '06:50', 215, 190000, 4200, N'C9'),
    ('USH-AEP', N'Aerolíneas Argentinas', 'AR', 89, 'USH', 'AEP', '16:00', 215, 190000, 4200, N'G9'),
    ('AEP-SLA', N'Aerolíneas Argentinas', 'AR', 92, 'AEP', 'SLA', '07:15', 130, 125000, 3700, N'A6'),
    ('SLA-AEP', N'Aerolíneas Argentinas', 'AR', 93, 'SLA', 'AEP', '19:00', 130, 125000, 3700, N'E6'),
    ('AEP-MVD', N'Aerolíneas Argentinas', 'AR', 90, 'AEP', 'MVD', '12:00',  55, 150000, 4000, N'A10'),
    ('MVD-AEP', N'Aerolíneas Argentinas', 'AR', 91, 'MVD', 'AEP', '18:10',  55, 150000, 4000, N'B10'),
    ('EZE-SCL', N'LATAM Airlines',        'LA', 80, 'EZE', 'SCL', '10:30', 135, 240000, 4500, N'A12'),
    ('SCL-EZE', N'LATAM Airlines',        'LA', 81, 'SCL', 'EZE', '16:40', 140, 240000, 4500, N'B12'),
    ('EZE-GRU', N'LATAM Airlines',        'LA', 82, 'EZE', 'GRU', '08:40', 170, 280000, 4800, N'A14'),
    ('GRU-EZE', N'LATAM Airlines',        'LA', 83, 'GRU', 'EZE', '14:30', 175, 280000, 4800, N'B14');

/* =====================================================================================
   4) Clientes (tabla Pasajero). Los emails se guardan en texto plano: el sistema los cifra
   al iniciar (tarea "CifrarEmailsPlanos").
   ===================================================================================== */
DECLARE @Clientes TABLE (DNI NVARCHAR(20) PRIMARY KEY, Nombre NVARCHAR(60), Apellido NVARCHAR(60),
                         Email NVARCHAR(200), Telefono NVARCHAR(30), Nacimiento DATE, AltaDias INT);
INSERT INTO @Clientes VALUES
    (N'27654321', N'Laura',     N'Fernández', N'laura.fernandez@mail.com',   N'1145671234', DATEADD(YEAR, -41, @Hoy), 760),
    (N'31456789', N'Martín',    N'Gómez',     N'martin.gomez@mail.com',      N'1156782345', DATEADD(YEAR, -36, @Hoy), 400),
    (N'33987654', N'Sofía',     N'Martínez',  N'sofia.martinez@mail.com',    N'1167893456', DATEADD(YEAR, -33, @Hoy), 300),
    (N'29876543', N'Diego',     N'López',     N'diego.lopez@mail.com',       N'1178904567', DATEADD(YEAR, -39, @Hoy), 450),
    (N'35123456', N'Valentina', N'Ruiz',      N'valentina.ruiz@mail.com',    N'1189015678', DATEADD(YEAR, -30, @Hoy), 380),
    (N'38765432', N'Lucas',     N'Pereyra',   N'lucas.pereyra@mail.com',     N'1190126789', DATEADD(YEAR, -27, @Hoy), 280),
    (N'26543210', N'Carolina',  N'Sosa',      N'carolina.sosa@mail.com',     N'1123457890', DATEADD(YEAR, -45, @Hoy), 420),
    (N'40112233', N'Federico',  N'Díaz',      N'federico.diaz@mail.com',     N'1134568901', DATEADD(YEAR, -24, @Hoy), 180),
    (N'42334455', N'Camila',    N'Torres',    N'camila.torres@mail.com',     N'1145679012', DATEADD(YEAR, -22, @Hoy),  60),
    (N'28333444', N'Pablo',     N'Romero',    N'pablo.romero@mail.com',      N'1156780123', DATEADD(YEAR, -40, @Hoy), 330),
    (N'29444555', N'Ana',       N'Romero',    N'ana.romero@mail.com',        N'1156780124', DATEADD(YEAR, -38, @Hoy), 330),
    (N'50111222', N'Tomás',     N'Romero',    N'tomas.romero@mail.com',      N'1156780125', DATEADD(YEAR,  -8, @Hoy), 330),
    (N'32222333', N'Juan',      N'Pérez',     N'juan.perez@mail.com',        N'1167891234', DATEADD(YEAR, -35, @Hoy), 230),
    (N'36666777', N'Gabriela',  N'Medina',    N'gabriela.medina@mail.com',   N'1178902345', DATEADD(YEAR, -29, @Hoy),   3),
    (N'34555666', N'Rodrigo',   N'Silva',     N'rodrigo.silva@mail.com',     N'1189013456', DATEADD(YEAR, -32, @Hoy), 130),
    (N'37888999', N'Natalia',   N'Herrera',   N'natalia.herrera@mail.com',   N'1190124567', DATEADD(YEAR, -28, @Hoy),  40);

INSERT INTO dbo.Pasajero (DNI, Nombre, Apellido, Email, Telefono, FechaAlta, FechaNacimiento)
SELECT C.DNI, C.Nombre, C.Apellido, C.Email, C.Telefono,
       DATEADD(HOUR, 11, CAST(DATEADD(DAY, -C.AltaDias, @Hoy) AS DATETIME2(0))), C.Nacimiento
FROM @Clientes C
WHERE NOT EXISTS (SELECT 1 FROM dbo.Pasajero P WHERE P.DNI = C.DNI);

-- Acompañantes: viajan en todas las reservas del titular (la familia Romero).
DECLARE @Acompanantes TABLE (DniTitular NVARCHAR(20), Dni NVARCHAR(20), IdTipoPasajero TINYINT);
INSERT INTO @Acompanantes VALUES (N'28333444', N'29444555', 1), (N'28333444', N'50111222', 2);

/* =====================================================================================
   5) Usuarios de prueba (uno por rol). Contraseña = nombre de usuario, en el formato
   anterior (SHA-256): el sistema la pasa sola al formato nuevo en el primer ingreso.
   ===================================================================================== */
DECLARE @Usuarios TABLE (DNI NVARCHAR(20), Nombre NVARCHAR(60), Apellido NVARCHAR(60),
                         UserName NVARCHAR(50), Email NVARCHAR(200), Rol NVARCHAR(50));
INSERT INTO @Usuarios VALUES
    (N'30111222', N'Marcela', N'Quiroga', N'vendedor1',   N'vendedor1@flysafe.com',   N'Vendedor'),
    (N'30222333', N'Ramiro',  N'Benítez', N'vendedor2',   N'vendedor2@flysafe.com',   N'Vendedor'),
    (N'30333444', N'Julieta', N'Acosta',  N'checkin1',    N'checkin1@flysafe.com',    N'EncargadoCheckIn'),
    (N'30444555', N'Hernán',  N'Castro',  N'gerente1',    N'gerente1@flysafe.com',    N'Gerente'),
    (N'30555666', N'Paula',   N'Ibarra',  N'supervisor1', N'supervisor1@flysafe.com', N'Supervisor'),
    (N'27654321', N'Laura',   N'Fernández', N'lfernandez', N'laura.fernandez@mail.com', N'Cliente');

INSERT INTO dbo.Usuario (DNI, Apellido, Nombre, UserName, Contrasena, Email, Bloqueo, Activo, IdRol,
                         IntentosFallidos, DebeCambiarContrasena, Idioma)
SELECT U.DNI, U.Apellido, U.Nombre, U.UserName,
       LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', CONVERT(VARCHAR(250), U.UserName)), 2)),
       U.Email, 0, 1, R.Id, 0, 0, 'es'
FROM @Usuarios U
INNER JOIN dbo.Roles R ON R.Nombre = U.Rol
WHERE NOT EXISTS (SELECT 1 FROM dbo.Usuario X WHERE X.UserName = U.UserName OR X.DNI = U.DNI);

/* =====================================================================================
   6) Viajes de cada cliente
   Patrones (historial): un viaje cada "Cada" días, desde hace "Desde" días hasta hace "Hasta" días.
   Estadía = días entre la ida y la vuelta (NULL = solo ida).
   Clase: 1 Económica, 2 Ejecutiva, 3 Primera. Tarifa: 1 Light, 2 Plus, 3 Top. Canal: 1 presencial, 2 autogestión.
   ===================================================================================== */
DECLARE @Patrones TABLE (Dni NVARCHAR(20), RutaIda CHAR(7), RutaVuelta CHAR(7) NULL, Estadia INT NULL,
                         IdClase INT, IdTarifa INT, Desde INT, Hasta INT, Cada INT, Canal INT);
INSERT INTO @Patrones VALUES
    -- Laura: viaja por trabajo a Córdoba todos los meses en Ejecutiva (Diamante; sus millas más viejas vencen pronto).
    (N'27654321', 'AEP-COR', 'COR-AEP', 1, 2, 3, 715,  10, 30, 2),
    -- Martín: Mendoza cada 45 días y, en los últimos meses, cada 25 (tendencia en aumento).
    (N'31456789', 'AEP-MDZ', 'MDZ-AEP', 3, 1, 2, 330, 100, 45, 1),
    (N'31456789', 'AEP-MDZ', 'MDZ-AEP', 3, 1, 2,  80,   5, 25, 1),
    -- Sofía: un viaje a Santiago y uno a Córdoba (Plata, cerca de Oro).
    (N'33987654', 'EZE-SCL', 'SCL-EZE', 5, 1, 1, 240, 240, 999, 1),
    (N'33987654', 'AEP-COR', NULL,   NULL, 1, 1, 120, 120, 999, 1),
    -- Diego: Bariloche todos los meses hasta hace seis meses (riesgo de abandono alto).
    (N'29876543', 'AEP-BRC', 'BRC-AEP', 4, 1, 2, 400, 190, 30, 1),
    -- Valentina: tres viajes en Ejecutiva hace casi un año (está por bajar de categoría).
    (N'35123456', 'AEP-IGR', 'IGR-AEP', 3, 2, 2, 350, 310, 20, 1),
    -- Lucas: un viaje hace meses y varios en los últimos tres (en aumento).
    (N'38765432', 'AEP-COR', 'COR-AEP', 2, 1, 1, 250, 250, 999, 1),
    (N'38765432', 'AEP-COR', 'COR-AEP', 2, 1, 1,  80,  20, 20, 1),
    -- Carolina: San Pablo en Primera cada dos meses (Diamante).
    (N'26543210', 'EZE-GRU', 'GRU-EZE', 6, 3, 3, 360,  30, 60, 1),
    -- Federico: un solo viaje a Ushuaia (Bronce).
    (N'40112233', 'AEP-USH', 'USH-AEP', 7, 1, 1, 150, 150, 999, 1),
    -- Camila: un viaje a Montevideo hace un mes (cliente nueva).
    (N'42334455', 'AEP-MVD', 'MVD-AEP', 4, 1, 2,  35,  35, 999, 1),
    -- Familia Romero: vacaciones en Bariloche (viajan los tres).
    (N'28333444', 'AEP-BRC', 'BRC-AEP', 7, 1, 2, 300, 300, 999, 1),
    (N'28333444', 'AEP-BRC', 'BRC-AEP', 7, 1, 2,  60,  60, 999, 1),
    -- Juan: Salta dos veces.
    (N'32222333', 'AEP-SLA', 'SLA-AEP', 3, 1, 2, 200,  90, 110, 1),
    -- Rodrigo: Santiago en Ejecutiva y Montevideo.
    (N'34555666', 'EZE-SCL', 'SCL-EZE', 4, 2, 2, 100, 100, 999, 1),
    (N'34555666', 'AEP-MVD', 'MVD-AEP', 3, 1, 2,  50,  50, 999, 1),
    -- Natalia: un viaje a Ushuaia en Ejecutiva hace veinte días.
    (N'37888999', 'AEP-USH', 'USH-AEP', 6, 2, 3,  20,  20, 999, 1);

DECLARE @Viajes TABLE (
    Nro INT IDENTITY(1,1) PRIMARY KEY,
    Dni NVARCHAR(20), RutaIda CHAR(7), FechaIda DATE, RutaVuelta CHAR(7) NULL, FechaVuelta DATE NULL,
    IdClase INT, IdTarifa INT, Canal INT,
    Estado INT,                  -- 1 pendiente de pago, 2 confirmada, 3 cancelada
    ModoCheckIn CHAR(1),         -- R = realizado en el mostrador, O = realizado online, P = sin check-in
    Cancelacion CHAR(1) NULL,    -- C = cancelada después de pagar, V = vencida sin pago
    HorasCancelacion INT NULL,   -- horas antes de la salida en que se canceló (C)
    PorcPenalidad DECIMAL(5,2) NULL,
    RealizadaHaceDias INT NULL); -- para las reservas futuras: hace cuántos días se hizo

;WITH Numeros AS (
    SELECT TOP (800) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) - 1 AS K
    FROM sys.all_objects A CROSS JOIN sys.all_objects B
)
INSERT INTO @Viajes (Dni, RutaIda, FechaIda, RutaVuelta, FechaVuelta, IdClase, IdTarifa, Canal, Estado, ModoCheckIn)
SELECT P.Dni, P.RutaIda, DATEADD(DAY, -(P.Desde - N.K * P.Cada), @Hoy),
       P.RutaVuelta,
       CASE WHEN P.RutaVuelta IS NULL THEN NULL ELSE DATEADD(DAY, -(P.Desde - N.K * P.Cada) + P.Estadia, @Hoy) END,
       P.IdClase, P.IdTarifa, P.Canal, 2, CASE WHEN P.Canal = 2 THEN 'O' ELSE 'R' END
FROM @Patrones P
INNER JOIN Numeros N ON P.Desde - N.K * P.Cada >= P.Hasta
ORDER BY DATEADD(DAY, -(P.Desde - N.K * P.Cada), @Hoy), P.Dni;

-- Casos especiales (pasados y futuros).
INSERT INTO @Viajes (Dni, RutaIda, FechaIda, RutaVuelta, FechaVuelta, IdClase, IdTarifa, Canal, Estado, ModoCheckIn,
                     Cancelacion, HorasCancelacion, PorcPenalidad, RealizadaHaceDias)
VALUES
    -- Lucas no se presentó a su vuelo a Salta (el reporte de check-in lo muestra Ausente).
    (N'38765432', 'AEP-SLA', DATEADD(DAY, -12, @Hoy), NULL, NULL, 1, 1, 1, 2, 'P', NULL, NULL, NULL, NULL),
    -- Diego canceló un viaje dos días antes de salir: penalidad del 30 % y reembolso ya procesado.
    (N'29876543', 'AEP-BRC', DATEADD(DAY, -205, @Hoy), 'BRC-AEP', DATEADD(DAY, -201, @Hoy), 1, 2, 1, 3, 'P', 'C', 48, 0.30, NULL),
    -- Mañana: Juan (check-in pendiente, ya se puede hacer) y Laura (ya hizo el check-in online).
    (N'32222333', 'AEP-COR', DATEADD(DAY, 1, @Hoy), NULL, NULL, 1, 2, 1, 2, 'P', NULL, NULL, NULL, 6),
    (N'27654321', 'AEP-COR', DATEADD(DAY, 1, @Hoy), 'COR-AEP', DATEADD(DAY, 2, @Hoy), 2, 3, 2, 2, 'O', NULL, NULL, NULL, 10),
    -- Martín: Mendoza pasado mañana (check-in pendiente, ya abierto) y Santiago en 20 días.
    (N'31456789', 'AEP-MDZ', DATEADD(DAY, 2, @Hoy), 'MDZ-AEP', DATEADD(DAY, 5, @Hoy), 1, 2, 1, 2, 'P', NULL, NULL, NULL, 8),
    (N'31456789', 'EZE-SCL', DATEADD(DAY, 20, @Hoy), 'SCL-EZE', DATEADD(DAY, 25, @Hoy), 1, 2, 1, 2, 'P', NULL, NULL, NULL, 4),
    -- Gabriela: reserva de hoy, todavía sin pagar (vence en 24 horas).
    (N'36666777', 'AEP-MDZ', DATEADD(DAY, 5, @Hoy), NULL, NULL, 1, 1, 1, 1, 'P', NULL, NULL, NULL, 0),
    -- Sofía: canceló ayer un viaje de la semana que viene (sin penalidad, reembolso pendiente).
    (N'33987654', 'AEP-IGR', DATEADD(DAY, 8, @Hoy), 'IGR-AEP', DATEADD(DAY, 11, @Hoy), 1, 2, 1, 3, 'P', 'C', NULL, 0.00, 5),
    -- Federico: reservó hace tres días y no pagó (el sistema la canceló por vencimiento).
    (N'40112233', 'AEP-MDZ', DATEADD(DAY, 6, @Hoy), NULL, NULL, 1, 1, 1, 3, 'P', 'V', NULL, NULL, 3),
    -- Familia Romero: vuelven a Bariloche en 12 días.
    (N'28333444', 'AEP-BRC', DATEADD(DAY, 12, @Hoy), 'BRC-AEP', DATEADD(DAY, 19, @Hoy), 1, 2, 1, 2, 'P', NULL, NULL, NULL, 15),
    -- Carolina: San Pablo en Primera dentro de 25 días.
    (N'26543210', 'EZE-GRU', DATEADD(DAY, 25, @Hoy), 'GRU-EZE', DATEADD(DAY, 31, @Hoy), 3, 3, 1, 2, 'P', NULL, NULL, NULL, 7),
    -- Natalia: Bariloche en 9 días, tarifa Light (la butaca se asigna en el check-in).
    (N'37888999', 'AEP-BRC', DATEADD(DAY, 9, @Hoy), NULL, NULL, 1, 1, 1, 2, 'P', NULL, NULL, NULL, 2);

/* =====================================================================================
   7) Vuelos de la demostración: uno por ruta y fecha que use algún viaje
   ===================================================================================== */
DECLARE @VuelosNecesarios TABLE (Ruta CHAR(7), Fecha DATE, PRIMARY KEY (Ruta, Fecha));
INSERT INTO @VuelosNecesarios (Ruta, Fecha)
SELECT RutaIda, FechaIda FROM @Viajes
UNION
SELECT RutaVuelta, FechaVuelta FROM @Viajes WHERE RutaVuelta IS NOT NULL;

DECLARE @VuelosDemo TABLE (Ruta CHAR(7), Fecha DATE, Codigo NVARCHAR(10), IdVuelo INT NULL, PRIMARY KEY (Ruta, Fecha));
INSERT INTO @VuelosDemo (Ruta, Fecha, Codigo)
SELECT N.Ruta, N.Fecha,
       CAST(R.Prefijo + RIGHT('0' + CAST(R.Slot AS VARCHAR(2)), 2) + CONVERT(CHAR(6), N.Fecha, 12) AS NVARCHAR(10))
FROM @VuelosNecesarios N
INNER JOIN @Rutas R ON R.Ruta = N.Ruta;

INSERT INTO dbo.Vuelo (CodigoVuelo, IdAerolinea, IdOrigen, IdDestino, FechaHoraSalida, FechaHoraLlegada,
                       PuertaEmbarque, CostoKiloExceso, BorradoLogico)
SELECT D.Codigo, AL.Id, O.Id, DS.Id,
       DATEADD(MINUTE, DATEDIFF(MINUTE, CAST('00:00' AS TIME(0)), R.Hora), CAST(D.Fecha AS DATETIME2(0))),
       DATEADD(MINUTE, R.DuracionMin + DATEDIFF(MINUTE, CAST('00:00' AS TIME(0)), R.Hora), CAST(D.Fecha AS DATETIME2(0))),
       R.Puerta, R.CostoKilo, 0
FROM @VuelosDemo D
INNER JOIN @Rutas R ON R.Ruta = D.Ruta
INNER JOIN dbo.Aerolinea AL ON AL.Nombre = R.Aerolinea
INNER JOIN dbo.Aeropuerto O ON O.CodigoIata = R.Origen
INNER JOIN dbo.Aeropuerto DS ON DS.CodigoIata = R.Destino
WHERE NOT EXISTS (SELECT 1 FROM dbo.Vuelo V WHERE V.CodigoVuelo = D.Codigo);

UPDATE D SET IdVuelo = V.Id
FROM @VuelosDemo D INNER JOIN dbo.Vuelo V ON V.CodigoVuelo = D.Codigo;

-- Clases: el precio varía un poco según el día (determinístico), igual que en el generador.
INSERT INTO dbo.VueloClase (IdVuelo, IdClase, PrecioBase, CapacidadAsientos, AsientosReservados, FranquiciaEquipajeKg)
SELECT D.IdVuelo, C.IdClase,
       CAST(ROUND(R.Precio * (0.90 + ABS(CHECKSUM(D.Ruta, D.Fecha) % 21) / 100.0) * C.Factor, -2) AS DECIMAL(12,2)),
       C.Capacidad, 0, C.Franquicia
FROM @VuelosDemo D
INNER JOIN @Rutas R ON R.Ruta = D.Ruta
CROSS JOIN (VALUES (1, 1.0, 84, 15.00), (2, 2.2, 24, 23.00), (3, 3.5, 12, 32.00)) AS C (IdClase, Factor, Capacidad, Franquicia)
WHERE NOT EXISTS (SELECT 1 FROM dbo.VueloClase X WHERE X.IdVuelo = D.IdVuelo AND X.IdClase = C.IdClase);

-- Asientos: el mismo mapa que usa el sistema (filas 1-2 Primera, 3-6 Ejecutiva, 7-20 Económica).
INSERT INTO dbo.Asiento (IdVuelo, Fila, Letra, IdClase, Ubicacion)
SELECT D.IdVuelo, F.Fila, L.Letra,
       CASE WHEN F.Fila <= 2 THEN 3 WHEN F.Fila <= 6 THEN 2 ELSE 1 END,
       L.Ubicacion
FROM @VuelosDemo D
CROSS JOIN (SELECT TOP (20) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS Fila FROM sys.all_objects) AS F
CROSS JOIN (VALUES ('A', N'Ventana'), ('B', N'Central'), ('C', N'Pasillo'),
                   ('D', N'Pasillo'), ('E', N'Central'), ('F', N'Ventana')) AS L (Letra, Ubicacion)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Asiento X WHERE X.IdVuelo = D.IdVuelo AND X.Fila = F.Fila AND X.Letra = L.Letra);

/* =====================================================================================
   8) Reservas con sus pasajeros, adicionales, pagos, boletos y check-in
   ===================================================================================== */
DECLARE @IdEquipajeExtra INT = (SELECT Id FROM dbo.TipoAdicional WHERE Codigo = N'EQUIPAJE_EXTRA');
DECLARE @PrecioEquipaje DECIMAL(12,2) = ISNULL((SELECT PrecioUnitario FROM dbo.TipoAdicional WHERE Id = @IdEquipajeExtra), 25000);

DECLARE @Nro INT, @Dni NVARCHAR(20), @RutaIda CHAR(7), @FechaIda DATE, @RutaVuelta CHAR(7), @FechaVuelta DATE,
        @IdClase INT, @IdTarifa INT, @Canal INT, @Estado INT, @Modo CHAR(1), @Cancelacion CHAR(1),
        @HorasCancelacion INT, @PorcPenalidad DECIMAL(5,2), @RealizadaHaceDias INT;
DECLARE @IdVuelo INT, @IdVueloVuelta INT, @SalidaIda DATETIME2(0), @SalidaVuelta DATETIME2(0),
        @Recargo DECIMAL(5,2), @AsientoIncluido TINYINT, @Factores DECIMAL(6,2), @CantPasajeros INT, @CantAsientos INT,
        @ImporteBase DECIMAL(12,2), @Subtotal DECIMAL(12,2), @Impuestos DECIMAL(12,2), @Total DECIMAL(12,2),
        @Realizada DATETIME2(0), @Vence DATETIME2(0), @Vendedor NVARCHAR(50), @IdReserva INT, @IdPago INT,
        @MedioPago INT, @FechaPago DATETIME2(0), @FechaCancel DATETIME2(0), @Penalidad DECIMAL(12,2);

DECLARE @Pax TABLE (Dni NVARCHAR(20), IdTipoPasajero TINYINT, Orden INT);
DECLARE @Adic TABLE (IdTipoAdicional INT, Cantidad INT, Costo DECIMAL(12,2));

DECLARE cViajes CURSOR LOCAL FAST_FORWARD FOR
    SELECT Nro, Dni, RutaIda, FechaIda, RutaVuelta, FechaVuelta, IdClase, IdTarifa, Canal, Estado, ModoCheckIn,
           Cancelacion, HorasCancelacion, PorcPenalidad, RealizadaHaceDias
    FROM @Viajes ORDER BY FechaIda, Nro;
OPEN cViajes;
FETCH NEXT FROM cViajes INTO @Nro, @Dni, @RutaIda, @FechaIda, @RutaVuelta, @FechaVuelta, @IdClase, @IdTarifa, @Canal,
                             @Estado, @Modo, @Cancelacion, @HorasCancelacion, @PorcPenalidad, @RealizadaHaceDias;
WHILE @@FETCH_STATUS = 0
BEGIN
    SELECT @IdVuelo = IdVuelo FROM @VuelosDemo WHERE Ruta = @RutaIda AND Fecha = @FechaIda;
    SET @IdVueloVuelta = NULL;
    IF @RutaVuelta IS NOT NULL
        SELECT @IdVueloVuelta = IdVuelo FROM @VuelosDemo WHERE Ruta = @RutaVuelta AND Fecha = @FechaVuelta;
    SELECT @SalidaIda = FechaHoraSalida FROM dbo.Vuelo WHERE Id = @IdVuelo;
    SET @SalidaVuelta = (SELECT FechaHoraSalida FROM dbo.Vuelo WHERE Id = @IdVueloVuelta);

    SELECT @Recargo = PorcentajeRecargo, @AsientoIncluido = AsientoIncluido FROM dbo.TarifaFamilia WHERE Id = @IdTarifa;

    -- Pasajeros: el titular y sus acompañantes.
    DELETE FROM @Pax;
    INSERT INTO @Pax VALUES (@Dni, 1, 0);
    INSERT INTO @Pax SELECT A.Dni, A.IdTipoPasajero, ROW_NUMBER() OVER (ORDER BY A.Dni) FROM @Acompanantes A WHERE A.DniTitular = @Dni;
    SELECT @CantPasajeros = COUNT(*), @CantAsientos = SUM(CASE WHEN IdTipoPasajero = 3 THEN 0 ELSE 1 END),
           @Factores = SUM(CASE IdTipoPasajero WHEN 2 THEN 0.75 WHEN 3 THEN 0.10 ELSE 1.00 END)
    FROM @Pax;

    -- Tarifa de cada tramo: precio de la clase con el recargo de la familia tarifaria, por pasajero.
    SET @ImporteBase =
        ROUND(ROUND((SELECT PrecioBase FROM dbo.VueloClase WHERE IdVuelo = @IdVuelo AND IdClase = @IdClase) * (1 + @Recargo / 100), 2) * @Factores, 2)
      + ISNULL(ROUND(ROUND((SELECT PrecioBase FROM dbo.VueloClase WHERE IdVuelo = @IdVueloVuelta AND IdClase = @IdClase) * (1 + @Recargo / 100), 2) * @Factores, 2), 0);

    -- Servicios adicionales (en la ida), variados según el viaje para que el reporte encuentre combinaciones.
    DELETE FROM @Adic;
    INSERT INTO @Adic (IdTipoAdicional, Cantidad, Costo)
    SELECT T.Id, 1, T.PrecioUnitario
    FROM dbo.TipoAdicional T
    WHERE T.Nombre IN (SELECT X.Servicio FROM (VALUES
              (0, N'Comida especial'), (0, N'Asistencia prioritaria'),
              (2, N'Seguro de viaje'),
              (4, N'Embarque prioritario'), (4, N'Comida especial')) AS X (Grupo, Servicio)
          WHERE X.Grupo = @Nro % 5);
    IF @Nro % 5 IN (1, 2) AND @IdEquipajeExtra IS NOT NULL
        INSERT INTO @Adic VALUES (@IdEquipajeExtra, 1, @PrecioEquipaje);

    SELECT @Subtotal = ISNULL(SUM(Cantidad * Costo), 0) FROM @Adic;
    SET @Impuestos = ROUND((@ImporteBase + @Subtotal) * 0.21, 2);
    SET @Total = @ImporteBase + @Subtotal + @Impuestos;

    -- Fechas de la operación.
    IF @RealizadaHaceDias IS NOT NULL
        SET @Realizada = DATEADD(MINUTE, -(90 + @Nro % 120), DATEADD(DAY, -@RealizadaHaceDias, @Ahora));
    ELSE
        SET @Realizada = DATEADD(MINUTE, 600 + (@Nro * 37) % 420, CAST(DATEADD(DAY, -(7 + @Nro % 25), CAST(@SalidaIda AS DATE)) AS DATETIME2(0)));
    SET @Vence = CASE WHEN @Canal = 2 THEN DATEADD(MINUTE, 30, @Realizada) ELSE DATEADD(HOUR, 24, @Realizada) END;
    IF @Vence > @SalidaIda SET @Vence = @SalidaIda;

    SET @Vendedor = CASE WHEN @Canal = 2 THEN N'lfernandez' WHEN @Nro % 2 = 0 THEN N'vendedor1' ELSE N'vendedor2' END;
    SET @MedioPago = 1 + @Nro % 4;
    SET @FechaPago = DATEADD(MINUTE, 20, @Realizada);

    SET @FechaCancel = NULL; SET @Penalidad = NULL;
    IF @Cancelacion = 'C'
    BEGIN
        SET @FechaCancel = CASE WHEN @HorasCancelacion IS NULL THEN DATEADD(DAY, -1, @Ahora)
                                ELSE DATEADD(HOUR, -@HorasCancelacion, @SalidaIda) END;
        SET @Penalidad = ROUND(@Total * @PorcPenalidad, 2);
    END
    ELSE IF @Cancelacion = 'V'
    BEGIN
        SET @FechaCancel = @Vence;
        SET @Penalidad = 0;
    END

    INSERT INTO dbo.Reserva (DniCliente, IdVuelo, IdClase, IdTipoViaje, FechaRegreso, CantidadPasajeros,
                             ImporteBase, SubtotalAdicionales, Impuestos, ImporteTotal, IdEstadoReserva,
                             FechaRealizacion, LoginVendedor, IdCanalVenta, FechaCancelacion, MontoPenalidadCancelacion,
                             IdVueloVuelta, IdClaseVuelta, FechaVencimiento, VencidaSinPago, CantidadAsientos, IdTarifa)
    VALUES (@Dni, @IdVuelo, @IdClase, CASE WHEN @IdVueloVuelta IS NULL THEN 1 ELSE 2 END, CAST(@SalidaVuelta AS DATE),
            @CantPasajeros, @ImporteBase, @Subtotal, @Impuestos, @Total, @Estado,
            @Realizada, @Vendedor, @Canal, @FechaCancel, @Penalidad,
            @IdVueloVuelta, CASE WHEN @IdVueloVuelta IS NULL THEN NULL ELSE @IdClase END,
            @Vence, CASE WHEN @Cancelacion = 'V' THEN 1 ELSE 0 END, @CantAsientos, @IdTarifa);
    SET @IdReserva = SCOPE_IDENTITY();

    -- Pasajeros por tramo. La butaca se elige al reservar si la tarifa la incluye (Plus y Top);
    -- con Light se asigna gratis en el check-in. Las reservas canceladas no ocupan butacas.
    INSERT INTO dbo.ReservaPasajero (IdReserva, DniPasajero, IdAsiento, EquipajeExtra, Tramo, IdTipoPasajero, IdAsistencia)
    SELECT @IdReserva, P.Dni, NULL,
           CASE WHEN P.Orden = 0 AND EXISTS (SELECT 1 FROM @Adic WHERE IdTipoAdicional = @IdEquipajeExtra) AND T.Tramo = 1 THEN 1 ELSE 0 END,
           T.Tramo, P.IdTipoPasajero, 0
    FROM @Pax P
    CROSS JOIN (SELECT 1 AS Tramo UNION ALL SELECT 2 WHERE @IdVueloVuelta IS NOT NULL) AS T;

    IF @Estado IN (1, 2) AND @AsientoIncluido >= 1
    BEGIN
        DECLARE @TramoA TINYINT = 1, @DniA NVARCHAR(20), @VueloA INT;
        WHILE @TramoA <= 2
        BEGIN
            SET @VueloA = CASE WHEN @TramoA = 1 THEN @IdVuelo ELSE @IdVueloVuelta END;
            IF @VueloA IS NOT NULL
            BEGIN
                DECLARE cAsientos CURSOR LOCAL FAST_FORWARD FOR
                    SELECT Dni FROM @Pax WHERE IdTipoPasajero <> 3 ORDER BY Orden;
                OPEN cAsientos;
                FETCH NEXT FROM cAsientos INTO @DniA;
                WHILE @@FETCH_STATUS = 0
                BEGIN
                    UPDATE dbo.ReservaPasajero
                       SET IdAsiento = (SELECT TOP (1) A.Id FROM dbo.Asiento A
                                        WHERE A.IdVuelo = @VueloA AND A.IdClase = @IdClase AND A.EsPreferencial = 0
                                          AND NOT EXISTS (SELECT 1 FROM dbo.ReservaPasajero X WHERE X.IdAsiento = A.Id)
                                          AND NOT EXISTS (SELECT 1 FROM dbo.CheckIn X WHERE X.IdAsiento = A.Id)
                                        ORDER BY A.Fila DESC, A.Letra)
                     WHERE IdReserva = @IdReserva AND DniPasajero = @DniA AND Tramo = @TramoA;
                    FETCH NEXT FROM cAsientos INTO @DniA;
                END
                CLOSE cAsientos; DEALLOCATE cAsientos;
            END
            SET @TramoA += 1;
        END
    END

    INSERT INTO dbo.ReservaAdicional (IdReserva, IdTipoAdicional, Cantidad, CostoUnitario, Tramo)
    SELECT @IdReserva, IdTipoAdicional, Cantidad, Costo, 1 FROM @Adic;

    -- Pago, boletos y check-in (como en el sistema: al pagar se emiten los boletos y nace el check-in).
    IF @Estado = 2 OR @Cancelacion = 'C'
    BEGIN
        INSERT INTO dbo.Pago (IdReserva, ImporteTotalAbonado, IdMedioPago, NumeroTransaccion, FechaHoraPago, LoginVendedor)
        VALUES (@IdReserva, @Total, @MedioPago,
                CASE @MedioPago WHEN 4 THEN N'EFE-' + FORMAT(@FechaPago, 'yyyyMMddHHmmss') + N'-' + CAST(@IdReserva AS NVARCHAR(10))
                                ELSE N'OP-' + RIGHT('00000000' + CAST(70000000 + @IdReserva * 137 AS VARCHAR(8)), 8) END,
                @FechaPago, @Vendedor);
        SET @IdPago = SCOPE_IDENTITY();

        INSERT INTO dbo.Boleto (IdReserva, DniPasajero, FechaEmision, Tramo)
        SELECT IdReserva, DniPasajero, @FechaPago, Tramo FROM dbo.ReservaPasajero WHERE IdReserva = @IdReserva;

        INSERT INTO dbo.CheckIn (IdReserva, DniPasajero, IdEstadoCheckIn, FechaHoraCheckIn, IdAsiento, LoginEncargado, IdCanal, Tramo)
        SELECT IdReserva, DniPasajero, 1, NULL, IdAsiento, NULL, NULL, Tramo
        FROM dbo.ReservaPasajero WHERE IdReserva = @IdReserva AND IdTipoPasajero <> 3;
    END

    -- Check-in realizado (en el mostrador o online), con su tarjeta de embarque.
    IF @Estado = 2 AND @Modo IN ('R', 'O')
    BEGIN
        DECLARE @IdCheckIn INT, @TramoC TINYINT, @SalidaC DATETIME2(0), @VueloC INT, @HoraCheckIn DATETIME2(0);
        DECLARE cCheck CURSOR LOCAL FAST_FORWARD FOR
            SELECT CI.Id, CI.Tramo FROM dbo.CheckIn CI WHERE CI.IdReserva = @IdReserva ORDER BY CI.Tramo, CI.DniPasajero;
        OPEN cCheck;
        FETCH NEXT FROM cCheck INTO @IdCheckIn, @TramoC;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            SET @VueloC = CASE WHEN @TramoC = 1 THEN @IdVuelo ELSE @IdVueloVuelta END;
            SET @SalidaC = CASE WHEN @TramoC = 1 THEN @SalidaIda ELSE @SalidaVuelta END;
            -- Solo se hace el check-in de los vuelos que ya salieron o cuyo check-in ya abrió (48 h antes).
            IF @SalidaC <= DATEADD(HOUR, 48, @Ahora)
            BEGIN
                SET @HoraCheckIn = DATEADD(MINUTE, -(120 + (@IdCheckIn * 53) % 1300), @SalidaC);
                IF @HoraCheckIn > @Ahora SET @HoraCheckIn = DATEADD(MINUTE, -30, @Ahora);

                UPDATE dbo.CheckIn
                   SET IdEstadoCheckIn = 2, FechaHoraCheckIn = @HoraCheckIn,
                       LoginEncargado = CASE WHEN @Modo = 'O' THEN N'lfernandez' ELSE N'checkin1' END,
                       IdCanal = CASE WHEN @Modo = 'O' THEN 2 ELSE 1 END,
                       IdAsiento = ISNULL(IdAsiento,
                                   (SELECT TOP (1) A.Id FROM dbo.Asiento A
                                    WHERE A.IdVuelo = @VueloC AND A.IdClase = @IdClase AND A.EsPreferencial = 0
                                      AND NOT EXISTS (SELECT 1 FROM dbo.ReservaPasajero X WHERE X.IdAsiento = A.Id)
                                      AND NOT EXISTS (SELECT 1 FROM dbo.CheckIn X WHERE X.IdAsiento = A.Id)
                                    ORDER BY A.Fila, A.Letra))
                 WHERE Id = @IdCheckIn;

                INSERT INTO dbo.TarjetaEmbarque (IdCheckIn, PuertaEmbarque, HoraLimiteEmbarque, FechaHoraEmision)
                SELECT @IdCheckIn, V.PuertaEmbarque, DATEADD(MINUTE, -30, V.FechaHoraSalida), @HoraCheckIn
                FROM dbo.Vuelo V WHERE V.Id = @VueloC;
            END
            FETCH NEXT FROM cCheck INTO @IdCheckIn, @TramoC;
        END
        CLOSE cCheck; DEALLOCATE cCheck;
    END

    -- Cancelada después de pagar: se liberan las butacas y se registra el reembolso.
    IF @Cancelacion = 'C' AND @Total - @Penalidad > 0
        INSERT INTO dbo.Reembolso (IdReserva, Importe, IdMedioPago, IdEstado, FechaSolicitud, FechaProceso, LoginProceso)
        VALUES (@IdReserva, @Total - @Penalidad, @MedioPago,
                CASE WHEN @FechaCancel < DATEADD(DAY, -3, @Ahora) THEN 2 ELSE 1 END,
                @FechaCancel,
                CASE WHEN @FechaCancel < DATEADD(DAY, -3, @Ahora) THEN DATEADD(DAY, 2, @FechaCancel) END,
                CASE WHEN @FechaCancel < DATEADD(DAY, -3, @Ahora) THEN N'vendedor1' END);

    FETCH NEXT FROM cViajes INTO @Nro, @Dni, @RutaIda, @FechaIda, @RutaVuelta, @FechaVuelta, @IdClase, @IdTarifa, @Canal,
                                 @Estado, @Modo, @Cancelacion, @HorasCancelacion, @PorcPenalidad, @RealizadaHaceDias;
END
CLOSE cViajes; DEALLOCATE cViajes;

/* ---------- Cupo ocupado de cada vuelo y clase (reservas pendientes y confirmadas) ---------- */
;WITH Ocupados AS (
    SELECT CASE WHEN RP.Tramo = 2 THEN R.IdVueloVuelta ELSE R.IdVuelo END AS IdVuelo, R.IdClase, COUNT(*) AS Cant
    FROM dbo.ReservaPasajero RP
    INNER JOIN dbo.Reserva R ON R.Id = RP.IdReserva
    WHERE R.IdEstadoReserva IN (1, 2) AND RP.IdTipoPasajero <> 3
    GROUP BY CASE WHEN RP.Tramo = 2 THEN R.IdVueloVuelta ELSE R.IdVuelo END, R.IdClase
)
UPDATE VC SET AsientosReservados = O.Cant
FROM dbo.VueloClase VC
INNER JOIN Ocupados O ON O.IdVuelo = VC.IdVuelo AND O.IdClase = VC.IdClase;

/* =====================================================================================
   9) Bitácora de cambios de vuelos: dos vuelos futuros cambian de puerta
   (el trigger del sistema guarda la versión anterior en Vuelo_C)
   ===================================================================================== */
UPDATE V SET PuertaEmbarque = N'B9'
FROM dbo.Vuelo V
INNER JOIN @VuelosDemo D ON D.IdVuelo = V.Id
WHERE D.Ruta = 'AEP-BRC' AND D.Fecha = DATEADD(DAY, 12, @Hoy);

UPDATE V SET PuertaEmbarque = N'A15'
FROM dbo.Vuelo V
INNER JOIN @VuelosDemo D ON D.IdVuelo = V.Id
WHERE D.Ruta = 'EZE-SCL' AND D.Fecha = DATEADD(DAY, 20, @Hoy);

/* =====================================================================================
   10) Tareas que el sistema hace al iniciar: cifrar los emails y recalcular los dígitos
   verificadores de las tablas protegidas que se cargaron.
   ===================================================================================== */
INSERT INTO dbo.TareaPendiente_GV42 (Nombre)
SELECT X.T FROM (VALUES (N'CifrarEmailsPlanos'), (N'RecalcularDV:Usuario'),
                        (N'RecalcularDV:Reserva'), (N'RecalcularDV:Pago')) AS X (T)
WHERE NOT EXISTS (SELECT 1 FROM dbo.TareaPendiente_GV42 P WHERE P.Nombre = X.T);

COMMIT TRANSACTION;

/* ---------- Resumen ---------- */
SELECT
    (SELECT COUNT(*) FROM dbo.Pasajero)                              AS Clientes,
    (SELECT COUNT(*) FROM dbo.Vuelo)                                 AS Vuelos,
    (SELECT COUNT(*) FROM dbo.Reserva)                               AS Reservas,
    (SELECT COUNT(*) FROM dbo.Reserva WHERE IdEstadoReserva = 2)     AS Confirmadas,
    (SELECT COUNT(*) FROM dbo.Reserva WHERE IdEstadoReserva = 1)     AS PendientesDePago,
    (SELECT COUNT(*) FROM dbo.Reserva WHERE IdEstadoReserva = 3)     AS Canceladas,
    (SELECT COUNT(*) FROM dbo.CheckIn WHERE IdEstadoCheckIn = 2)     AS CheckInRealizados,
    (SELECT COUNT(*) FROM dbo.Usuario)                               AS Usuarios;

PRINT N'Datos de demostración cargados. Abrí el sistema: al iniciar cifra los emails y recalcula la integridad.';
GO
