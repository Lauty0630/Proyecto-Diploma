/* =====================================================================================
   ActualizacionNegocio.sql  -  agrega el NEGOCIO a una base de gestión de usuarios
   ---------------------------------------------------------------------------------
   La ejecuta el instalador (opción "Actualizar") sobre una base creada con EsquemaBase.sql.
   Crea las tablas, restricciones, índices, triggers y datos iniciales del negocio (vuelos,
   reservas, pagos, check-in, clientes) y suma los módulos, patentes y tipos de evento de
   bitácora del negocio. No toca ninguna fila de usuarios, roles, permisos ni bitácora.
   Se ejecuta completo dentro de una transacción: si algo falla no queda nada a medias.
   Después el instalador ejecuta ActualizacionBD.sql (versiones 1 a 14), que completa el
   negocio y asigna los permisos de los roles del sistema sin borrar nada.

   Generado a partir de EsquemaCompleto.sql (mismas tablas, tipos y restricciones).
   ===================================================================================== */
USE [Gestion Usuario]
GO
/****** Objeto: Table [dbo].[Aerolinea] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aerolinea](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](80) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Aeropuerto] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aeropuerto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CodigoIata] [char](3) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Ciudad] [nvarchar](60) NOT NULL,
	[Pais] [nvarchar](60) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Asiento] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asiento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdVuelo] [int] NOT NULL,
	[Fila] [int] NOT NULL,
	[Letra] [char](1) NOT NULL,
	[NumeroAsiento]  AS (CONVERT([nvarchar](3),[Fila])+CONVERT([nvarchar](1),[Letra])) PERSISTED,
	[IdClase] [int] NOT NULL,
	[Ubicacion] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Boleto] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Boleto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumeroBoleto]  AS (N'BOL-'+right(N'000000'+CONVERT([nvarchar](10),[Id]),(6))) PERSISTED,
	[IdReserva] [int] NOT NULL,
	[DniPasajero] [nvarchar](20) NOT NULL,
	[FechaEmision] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[CanalVenta] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CanalVenta](
	[Id] [int] NOT NULL,
	[Nombre] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[CargoExcesoEquipaje] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CargoExcesoEquipaje](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdEquipaje] [int] NOT NULL,
	[KilosExceso] [decimal](7, 2) NOT NULL,
	[CostoPorKilo] [decimal](10, 2) NOT NULL,
	[ImporteCargo] [decimal](12, 2) NOT NULL,
	[IdMedioPago] [int] NOT NULL,
	[NumeroTransaccion] [nvarchar](40) NULL,
	[FechaHoraCobro] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[CheckIn] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckIn](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdReserva] [int] NOT NULL,
	[DniPasajero] [nvarchar](20) NOT NULL,
	[IdEstadoCheckIn] [int] NOT NULL,
	[FechaHoraCheckIn] [datetime2](0) NULL,
	[IdAsiento] [int] NULL,
	[LoginEncargado] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[ClaseVuelo] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClaseVuelo](
	[Id] [int] NOT NULL,
	[Nombre] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Equipaje] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipaje](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCheckIn] [int] NOT NULL,
	[CantidadBultos] [int] NOT NULL,
	[PesoTotalKg] [decimal](7, 2) NOT NULL,
	[FranquiciaKg] [decimal](7, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[EstadoCheckIn] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoCheckIn](
	[Id] [int] NOT NULL,
	[Nombre] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[EstadoReserva] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoReserva](
	[Id] [int] NOT NULL,
	[Nombre] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[EtiquetaEquipaje] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EtiquetaEquipaje](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdEquipaje] [int] NOT NULL,
	[CodigoEquipaje] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[MedioPago] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedioPago](
	[Id] [int] NOT NULL,
	[Nombre] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Pago] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pago](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdReserva] [int] NOT NULL,
	[ImporteTotalAbonado] [decimal](12, 2) NOT NULL,
	[IdMedioPago] [int] NOT NULL,
	[NumeroTransaccion] [nvarchar](40) NOT NULL,
	[FechaHoraPago] [datetime2](0) NOT NULL,
	[LoginVendedor] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Pasajero] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pasajero](
	[DNI] [nvarchar](20) NOT NULL,
	[Nombre] [nvarchar](60) NOT NULL,
	[Apellido] [nvarchar](60) NOT NULL,
	[Email] [nvarchar](500) NOT NULL,
	[Telefono] [nvarchar](30) NOT NULL,
	[FechaAlta] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DNI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Reserva] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reserva](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumeroReserva]  AS (N'RES-'+right(N'000000'+CONVERT([nvarchar](10),[Id]),(6))) PERSISTED,
	[DniCliente] [nvarchar](20) NOT NULL,
	[IdVuelo] [int] NOT NULL,
	[IdClase] [int] NOT NULL,
	[IdTipoViaje] [int] NOT NULL,
	[FechaRegreso] [date] NULL,
	[CantidadPasajeros] [int] NOT NULL,
	[ImporteBase] [decimal](12, 2) NOT NULL,
	[SubtotalAdicionales] [decimal](12, 2) NOT NULL,
	[Impuestos] [decimal](12, 2) NOT NULL,
	[ImporteTotal] [decimal](12, 2) NOT NULL,
	[IdEstadoReserva] [int] NOT NULL,
	[FechaRealizacion] [datetime2](0) NOT NULL,
	[LoginVendedor] [nvarchar](50) NOT NULL,
	[IdCanalVenta] [int] NOT NULL,
	[FechaCancelacion] [datetime2](0) NULL,
	[MontoPenalidadCancelacion] [decimal](12, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[ReservaAdicional] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservaAdicional](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdReserva] [int] NOT NULL,
	[IdTipoAdicional] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[CostoUnitario] [decimal](12, 2) NOT NULL,
	[Subtotal]  AS ([Cantidad]*[CostoUnitario]) PERSISTED,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[ReservaPasajero] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservaPasajero](
	[IdReserva] [int] NOT NULL,
	[DniPasajero] [nvarchar](20) NOT NULL,
	[IdAsiento] [int] NULL,
 CONSTRAINT [PK_ReservaPasajero] PRIMARY KEY CLUSTERED 
(
	[IdReserva] ASC,
	[DniPasajero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[TarjetaEmbarque] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TarjetaEmbarque](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumeroTarjeta]  AS (N'TE-'+right(N'000000'+CONVERT([nvarchar](10),[Id]),(6))) PERSISTED,
	[IdCheckIn] [int] NOT NULL,
	[PuertaEmbarque] [nvarchar](10) NOT NULL,
	[HoraLimiteEmbarque] [datetime2](0) NOT NULL,
	[FechaHoraEmision] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[TipoAdicional] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoAdicional](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](60) NOT NULL,
	[Activo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[TipoViaje] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoViaje](
	[Id] [int] NOT NULL,
	[Nombre] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Vuelo] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vuelo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CodigoVuelo] [nvarchar](10) NOT NULL,
	[IdAerolinea] [int] NOT NULL,
	[IdOrigen] [int] NOT NULL,
	[IdDestino] [int] NOT NULL,
	[FechaHoraSalida] [datetime2](0) NOT NULL,
	[FechaHoraLlegada] [datetime2](0) NOT NULL,
	[PuertaEmbarque] [nvarchar](10) NOT NULL,
	[CostoKiloExceso] [decimal](10, 2) NOT NULL,
	[BorradoLogico] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[VueloClase] Fecha de script: 29/09/2026 08:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VueloClase](
	[IdVuelo] [int] NOT NULL,
	[IdClase] [int] NOT NULL,
	[PrecioBase] [decimal](12, 2) NOT NULL,
	[CapacidadAsientos] [int] NOT NULL,
	[AsientosReservados] [int] NOT NULL,
	[FranquiciaEquipajeKg] [decimal](6, 2) NOT NULL,
 CONSTRAINT [PK_VueloClase] PRIMARY KEY CLUSTERED 
(
	[IdVuelo] ASC,
	[IdClase] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Vuelo_C] Bitácora de cambios de Vuelo ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vuelo_C](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdVuelo] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Hora] [time](0) NOT NULL,
	[CodigoVuelo] [nvarchar](10) NOT NULL,
	[IdAerolinea] [int] NOT NULL,
	[IdOrigen] [int] NOT NULL,
	[IdDestino] [int] NOT NULL,
	[FechaHoraSalida] [datetime2](0) NOT NULL,
	[FechaHoraLlegada] [datetime2](0) NOT NULL,
	[PuertaEmbarque] [nvarchar](10) NOT NULL,
	[CostoKiloExceso] [decimal](10, 2) NOT NULL,
	[BorradoLogico] [bit] NOT NULL,
	[Act] [bit] NOT NULL,
 CONSTRAINT [PK_Vuelo_C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Aerolinea] ON 

INSERT [dbo].[Aerolinea] ([Id], [Nombre]) VALUES (1, N'Aerolíneas Argentinas')
INSERT [dbo].[Aerolinea] ([Id], [Nombre]) VALUES (2, N'Flybondi')
INSERT [dbo].[Aerolinea] ([Id], [Nombre]) VALUES (3, N'JetSMART')
SET IDENTITY_INSERT [dbo].[Aerolinea] OFF
GO
SET IDENTITY_INSERT [dbo].[Aeropuerto] ON 

INSERT [dbo].[Aeropuerto] ([Id], [CodigoIata], [Nombre], [Ciudad], [Pais]) VALUES (1, N'AEP', N'Aeroparque Jorge Newbery', N'Buenos Aires', N'Argentina')
INSERT [dbo].[Aeropuerto] ([Id], [CodigoIata], [Nombre], [Ciudad], [Pais]) VALUES (2, N'EZE', N'Aeropuerto Internacional Ezeiza', N'Buenos Aires', N'Argentina')
INSERT [dbo].[Aeropuerto] ([Id], [CodigoIata], [Nombre], [Ciudad], [Pais]) VALUES (3, N'COR', N'Aeropuerto Internacional Pajas Blancas', N'Córdoba', N'Argentina')
INSERT [dbo].[Aeropuerto] ([Id], [CodigoIata], [Nombre], [Ciudad], [Pais]) VALUES (4, N'MDZ', N'Aeropuerto Internacional El Plumerillo', N'Mendoza', N'Argentina')
INSERT [dbo].[Aeropuerto] ([Id], [CodigoIata], [Nombre], [Ciudad], [Pais]) VALUES (5, N'BRC', N'Aeropuerto Internacional Teniente Candelaria', N'Bariloche', N'Argentina')
INSERT [dbo].[Aeropuerto] ([Id], [CodigoIata], [Nombre], [Ciudad], [Pais]) VALUES (6, N'IGR', N'Aeropuerto Internacional Cataratas del Iguazú', N'Puerto Iguazú', N'Argentina')
SET IDENTITY_INSERT [dbo].[Aeropuerto] OFF
GO
SET IDENTITY_INSERT [dbo].[Asiento] ON 

INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (1, 1, 1, N'A', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (2, 1, 2, N'A', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (3, 1, 3, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (4, 1, 4, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (5, 1, 5, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (6, 1, 6, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (7, 1, 7, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (8, 1, 8, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (9, 1, 9, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (10, 1, 10, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (11, 1, 11, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (12, 1, 12, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (13, 1, 13, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (14, 1, 14, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (15, 1, 15, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (16, 1, 16, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (17, 1, 17, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (18, 1, 18, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (19, 1, 19, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (20, 1, 20, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (21, 1, 1, N'B', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (22, 1, 2, N'B', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (23, 1, 3, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (24, 1, 4, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (25, 1, 5, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (26, 1, 6, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (27, 1, 7, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (28, 1, 8, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (29, 1, 9, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (30, 1, 10, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (31, 1, 11, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (32, 1, 12, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (33, 1, 13, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (34, 1, 14, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (35, 1, 15, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (36, 1, 16, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (37, 1, 17, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (38, 1, 18, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (39, 1, 19, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (40, 1, 20, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (41, 1, 1, N'C', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (42, 1, 2, N'C', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (43, 1, 3, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (44, 1, 4, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (45, 1, 5, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (46, 1, 6, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (47, 1, 7, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (48, 1, 8, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (49, 1, 9, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (50, 1, 10, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (51, 1, 11, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (52, 1, 12, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (53, 1, 13, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (54, 1, 14, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (55, 1, 15, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (56, 1, 16, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (57, 1, 17, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (58, 1, 18, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (59, 1, 19, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (60, 1, 20, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (61, 1, 1, N'D', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (62, 1, 2, N'D', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (63, 1, 3, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (64, 1, 4, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (65, 1, 5, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (66, 1, 6, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (67, 1, 7, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (68, 1, 8, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (69, 1, 9, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (70, 1, 10, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (71, 1, 11, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (72, 1, 12, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (73, 1, 13, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (74, 1, 14, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (75, 1, 15, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (76, 1, 16, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (77, 1, 17, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (78, 1, 18, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (79, 1, 19, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (80, 1, 20, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (81, 1, 1, N'E', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (82, 1, 2, N'E', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (83, 1, 3, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (84, 1, 4, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (85, 1, 5, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (86, 1, 6, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (87, 1, 7, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (88, 1, 8, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (89, 1, 9, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (90, 1, 10, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (91, 1, 11, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (92, 1, 12, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (93, 1, 13, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (94, 1, 14, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (95, 1, 15, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (96, 1, 16, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (97, 1, 17, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (98, 1, 18, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (99, 1, 19, N'E', 1, N'Central')
GO
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (100, 1, 20, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (101, 1, 1, N'F', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (102, 1, 2, N'F', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (103, 1, 3, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (104, 1, 4, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (105, 1, 5, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (106, 1, 6, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (107, 1, 7, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (108, 1, 8, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (109, 1, 9, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (110, 1, 10, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (111, 1, 11, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (112, 1, 12, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (113, 1, 13, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (114, 1, 14, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (115, 1, 15, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (116, 1, 16, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (117, 1, 17, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (118, 1, 18, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (119, 1, 19, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (120, 1, 20, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (121, 2, 1, N'A', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (122, 2, 2, N'A', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (123, 2, 3, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (124, 2, 4, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (125, 2, 5, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (126, 2, 6, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (127, 2, 7, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (128, 2, 8, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (129, 2, 9, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (130, 2, 10, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (131, 2, 11, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (132, 2, 12, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (133, 2, 13, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (134, 2, 14, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (135, 2, 15, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (136, 2, 16, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (137, 2, 17, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (138, 2, 18, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (139, 2, 19, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (140, 2, 20, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (141, 2, 1, N'B', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (142, 2, 2, N'B', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (143, 2, 3, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (144, 2, 4, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (145, 2, 5, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (146, 2, 6, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (147, 2, 7, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (148, 2, 8, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (149, 2, 9, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (150, 2, 10, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (151, 2, 11, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (152, 2, 12, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (153, 2, 13, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (154, 2, 14, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (155, 2, 15, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (156, 2, 16, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (157, 2, 17, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (158, 2, 18, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (159, 2, 19, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (160, 2, 20, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (161, 2, 1, N'C', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (162, 2, 2, N'C', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (163, 2, 3, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (164, 2, 4, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (165, 2, 5, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (166, 2, 6, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (167, 2, 7, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (168, 2, 8, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (169, 2, 9, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (170, 2, 10, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (171, 2, 11, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (172, 2, 12, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (173, 2, 13, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (174, 2, 14, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (175, 2, 15, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (176, 2, 16, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (177, 2, 17, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (178, 2, 18, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (179, 2, 19, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (180, 2, 20, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (181, 2, 1, N'D', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (182, 2, 2, N'D', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (183, 2, 3, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (184, 2, 4, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (185, 2, 5, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (186, 2, 6, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (187, 2, 7, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (188, 2, 8, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (189, 2, 9, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (190, 2, 10, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (191, 2, 11, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (192, 2, 12, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (193, 2, 13, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (194, 2, 14, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (195, 2, 15, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (196, 2, 16, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (197, 2, 17, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (198, 2, 18, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (199, 2, 19, N'D', 1, N'Pasillo')
GO
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (200, 2, 20, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (201, 2, 1, N'E', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (202, 2, 2, N'E', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (203, 2, 3, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (204, 2, 4, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (205, 2, 5, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (206, 2, 6, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (207, 2, 7, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (208, 2, 8, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (209, 2, 9, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (210, 2, 10, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (211, 2, 11, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (212, 2, 12, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (213, 2, 13, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (214, 2, 14, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (215, 2, 15, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (216, 2, 16, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (217, 2, 17, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (218, 2, 18, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (219, 2, 19, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (220, 2, 20, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (221, 2, 1, N'F', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (222, 2, 2, N'F', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (223, 2, 3, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (224, 2, 4, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (225, 2, 5, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (226, 2, 6, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (227, 2, 7, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (228, 2, 8, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (229, 2, 9, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (230, 2, 10, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (231, 2, 11, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (232, 2, 12, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (233, 2, 13, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (234, 2, 14, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (235, 2, 15, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (236, 2, 16, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (237, 2, 17, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (238, 2, 18, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (239, 2, 19, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (240, 2, 20, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (241, 3, 1, N'A', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (242, 3, 2, N'A', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (243, 3, 3, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (244, 3, 4, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (245, 3, 5, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (246, 3, 6, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (247, 3, 7, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (248, 3, 8, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (249, 3, 9, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (250, 3, 10, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (251, 3, 11, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (252, 3, 12, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (253, 3, 13, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (254, 3, 14, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (255, 3, 15, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (256, 3, 16, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (257, 3, 17, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (258, 3, 18, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (259, 3, 19, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (260, 3, 20, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (261, 3, 1, N'B', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (262, 3, 2, N'B', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (263, 3, 3, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (264, 3, 4, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (265, 3, 5, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (266, 3, 6, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (267, 3, 7, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (268, 3, 8, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (269, 3, 9, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (270, 3, 10, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (271, 3, 11, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (272, 3, 12, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (273, 3, 13, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (274, 3, 14, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (275, 3, 15, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (276, 3, 16, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (277, 3, 17, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (278, 3, 18, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (279, 3, 19, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (280, 3, 20, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (281, 3, 1, N'C', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (282, 3, 2, N'C', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (283, 3, 3, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (284, 3, 4, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (285, 3, 5, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (286, 3, 6, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (287, 3, 7, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (288, 3, 8, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (289, 3, 9, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (290, 3, 10, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (291, 3, 11, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (292, 3, 12, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (293, 3, 13, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (294, 3, 14, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (295, 3, 15, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (296, 3, 16, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (297, 3, 17, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (298, 3, 18, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (299, 3, 19, N'C', 1, N'Pasillo')
GO
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (300, 3, 20, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (301, 3, 1, N'D', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (302, 3, 2, N'D', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (303, 3, 3, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (304, 3, 4, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (305, 3, 5, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (306, 3, 6, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (307, 3, 7, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (308, 3, 8, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (309, 3, 9, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (310, 3, 10, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (311, 3, 11, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (312, 3, 12, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (313, 3, 13, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (314, 3, 14, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (315, 3, 15, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (316, 3, 16, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (317, 3, 17, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (318, 3, 18, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (319, 3, 19, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (320, 3, 20, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (321, 3, 1, N'E', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (322, 3, 2, N'E', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (323, 3, 3, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (324, 3, 4, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (325, 3, 5, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (326, 3, 6, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (327, 3, 7, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (328, 3, 8, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (329, 3, 9, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (330, 3, 10, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (331, 3, 11, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (332, 3, 12, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (333, 3, 13, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (334, 3, 14, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (335, 3, 15, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (336, 3, 16, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (337, 3, 17, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (338, 3, 18, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (339, 3, 19, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (340, 3, 20, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (341, 3, 1, N'F', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (342, 3, 2, N'F', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (343, 3, 3, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (344, 3, 4, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (345, 3, 5, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (346, 3, 6, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (347, 3, 7, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (348, 3, 8, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (349, 3, 9, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (350, 3, 10, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (351, 3, 11, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (352, 3, 12, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (353, 3, 13, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (354, 3, 14, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (355, 3, 15, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (356, 3, 16, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (357, 3, 17, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (358, 3, 18, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (359, 3, 19, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (360, 3, 20, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (361, 4, 1, N'A', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (362, 4, 2, N'A', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (363, 4, 3, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (364, 4, 4, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (365, 4, 5, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (366, 4, 6, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (367, 4, 7, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (368, 4, 8, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (369, 4, 9, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (370, 4, 10, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (371, 4, 11, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (372, 4, 12, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (373, 4, 13, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (374, 4, 14, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (375, 4, 15, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (376, 4, 16, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (377, 4, 17, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (378, 4, 18, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (379, 4, 19, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (380, 4, 20, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (381, 4, 1, N'B', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (382, 4, 2, N'B', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (383, 4, 3, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (384, 4, 4, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (385, 4, 5, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (386, 4, 6, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (387, 4, 7, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (388, 4, 8, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (389, 4, 9, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (390, 4, 10, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (391, 4, 11, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (392, 4, 12, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (393, 4, 13, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (394, 4, 14, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (395, 4, 15, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (396, 4, 16, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (397, 4, 17, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (398, 4, 18, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (399, 4, 19, N'B', 1, N'Central')
GO
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (400, 4, 20, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (401, 4, 1, N'C', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (402, 4, 2, N'C', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (403, 4, 3, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (404, 4, 4, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (405, 4, 5, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (406, 4, 6, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (407, 4, 7, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (408, 4, 8, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (409, 4, 9, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (410, 4, 10, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (411, 4, 11, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (412, 4, 12, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (413, 4, 13, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (414, 4, 14, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (415, 4, 15, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (416, 4, 16, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (417, 4, 17, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (418, 4, 18, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (419, 4, 19, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (420, 4, 20, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (421, 4, 1, N'D', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (422, 4, 2, N'D', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (423, 4, 3, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (424, 4, 4, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (425, 4, 5, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (426, 4, 6, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (427, 4, 7, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (428, 4, 8, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (429, 4, 9, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (430, 4, 10, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (431, 4, 11, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (432, 4, 12, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (433, 4, 13, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (434, 4, 14, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (435, 4, 15, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (436, 4, 16, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (437, 4, 17, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (438, 4, 18, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (439, 4, 19, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (440, 4, 20, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (441, 4, 1, N'E', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (442, 4, 2, N'E', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (443, 4, 3, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (444, 4, 4, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (445, 4, 5, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (446, 4, 6, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (447, 4, 7, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (448, 4, 8, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (449, 4, 9, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (450, 4, 10, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (451, 4, 11, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (452, 4, 12, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (453, 4, 13, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (454, 4, 14, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (455, 4, 15, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (456, 4, 16, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (457, 4, 17, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (458, 4, 18, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (459, 4, 19, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (460, 4, 20, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (461, 4, 1, N'F', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (462, 4, 2, N'F', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (463, 4, 3, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (464, 4, 4, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (465, 4, 5, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (466, 4, 6, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (467, 4, 7, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (468, 4, 8, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (469, 4, 9, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (470, 4, 10, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (471, 4, 11, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (472, 4, 12, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (473, 4, 13, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (474, 4, 14, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (475, 4, 15, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (476, 4, 16, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (477, 4, 17, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (478, 4, 18, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (479, 4, 19, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (480, 4, 20, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (481, 5, 1, N'A', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (482, 5, 2, N'A', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (483, 5, 3, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (484, 5, 4, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (485, 5, 5, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (486, 5, 6, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (487, 5, 7, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (488, 5, 8, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (489, 5, 9, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (490, 5, 10, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (491, 5, 11, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (492, 5, 12, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (493, 5, 13, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (494, 5, 14, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (495, 5, 15, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (496, 5, 16, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (497, 5, 17, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (498, 5, 18, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (499, 5, 19, N'A', 1, N'Ventana')
GO
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (500, 5, 20, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (501, 5, 1, N'B', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (502, 5, 2, N'B', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (503, 5, 3, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (504, 5, 4, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (505, 5, 5, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (506, 5, 6, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (507, 5, 7, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (508, 5, 8, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (509, 5, 9, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (510, 5, 10, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (511, 5, 11, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (512, 5, 12, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (513, 5, 13, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (514, 5, 14, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (515, 5, 15, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (516, 5, 16, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (517, 5, 17, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (518, 5, 18, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (519, 5, 19, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (520, 5, 20, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (521, 5, 1, N'C', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (522, 5, 2, N'C', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (523, 5, 3, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (524, 5, 4, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (525, 5, 5, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (526, 5, 6, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (527, 5, 7, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (528, 5, 8, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (529, 5, 9, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (530, 5, 10, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (531, 5, 11, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (532, 5, 12, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (533, 5, 13, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (534, 5, 14, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (535, 5, 15, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (536, 5, 16, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (537, 5, 17, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (538, 5, 18, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (539, 5, 19, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (540, 5, 20, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (541, 5, 1, N'D', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (542, 5, 2, N'D', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (543, 5, 3, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (544, 5, 4, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (545, 5, 5, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (546, 5, 6, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (547, 5, 7, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (548, 5, 8, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (549, 5, 9, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (550, 5, 10, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (551, 5, 11, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (552, 5, 12, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (553, 5, 13, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (554, 5, 14, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (555, 5, 15, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (556, 5, 16, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (557, 5, 17, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (558, 5, 18, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (559, 5, 19, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (560, 5, 20, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (561, 5, 1, N'E', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (562, 5, 2, N'E', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (563, 5, 3, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (564, 5, 4, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (565, 5, 5, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (566, 5, 6, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (567, 5, 7, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (568, 5, 8, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (569, 5, 9, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (570, 5, 10, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (571, 5, 11, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (572, 5, 12, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (573, 5, 13, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (574, 5, 14, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (575, 5, 15, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (576, 5, 16, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (577, 5, 17, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (578, 5, 18, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (579, 5, 19, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (580, 5, 20, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (581, 5, 1, N'F', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (582, 5, 2, N'F', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (583, 5, 3, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (584, 5, 4, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (585, 5, 5, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (586, 5, 6, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (587, 5, 7, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (588, 5, 8, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (589, 5, 9, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (590, 5, 10, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (591, 5, 11, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (592, 5, 12, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (593, 5, 13, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (594, 5, 14, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (595, 5, 15, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (596, 5, 16, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (597, 5, 17, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (598, 5, 18, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (599, 5, 19, N'F', 1, N'Ventana')
GO
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (600, 5, 20, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (601, 6, 1, N'A', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (602, 6, 2, N'A', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (603, 6, 3, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (604, 6, 4, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (605, 6, 5, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (606, 6, 6, N'A', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (607, 6, 7, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (608, 6, 8, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (609, 6, 9, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (610, 6, 10, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (611, 6, 11, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (612, 6, 12, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (613, 6, 13, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (614, 6, 14, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (615, 6, 15, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (616, 6, 16, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (617, 6, 17, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (618, 6, 18, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (619, 6, 19, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (620, 6, 20, N'A', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (621, 6, 1, N'B', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (622, 6, 2, N'B', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (623, 6, 3, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (624, 6, 4, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (625, 6, 5, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (626, 6, 6, N'B', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (627, 6, 7, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (628, 6, 8, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (629, 6, 9, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (630, 6, 10, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (631, 6, 11, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (632, 6, 12, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (633, 6, 13, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (634, 6, 14, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (635, 6, 15, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (636, 6, 16, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (637, 6, 17, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (638, 6, 18, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (639, 6, 19, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (640, 6, 20, N'B', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (641, 6, 1, N'C', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (642, 6, 2, N'C', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (643, 6, 3, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (644, 6, 4, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (645, 6, 5, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (646, 6, 6, N'C', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (647, 6, 7, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (648, 6, 8, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (649, 6, 9, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (650, 6, 10, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (651, 6, 11, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (652, 6, 12, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (653, 6, 13, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (654, 6, 14, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (655, 6, 15, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (656, 6, 16, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (657, 6, 17, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (658, 6, 18, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (659, 6, 19, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (660, 6, 20, N'C', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (661, 6, 1, N'D', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (662, 6, 2, N'D', 3, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (663, 6, 3, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (664, 6, 4, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (665, 6, 5, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (666, 6, 6, N'D', 2, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (667, 6, 7, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (668, 6, 8, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (669, 6, 9, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (670, 6, 10, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (671, 6, 11, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (672, 6, 12, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (673, 6, 13, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (674, 6, 14, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (675, 6, 15, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (676, 6, 16, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (677, 6, 17, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (678, 6, 18, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (679, 6, 19, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (680, 6, 20, N'D', 1, N'Pasillo')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (681, 6, 1, N'E', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (682, 6, 2, N'E', 3, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (683, 6, 3, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (684, 6, 4, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (685, 6, 5, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (686, 6, 6, N'E', 2, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (687, 6, 7, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (688, 6, 8, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (689, 6, 9, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (690, 6, 10, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (691, 6, 11, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (692, 6, 12, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (693, 6, 13, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (694, 6, 14, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (695, 6, 15, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (696, 6, 16, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (697, 6, 17, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (698, 6, 18, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (699, 6, 19, N'E', 1, N'Central')
GO
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (700, 6, 20, N'E', 1, N'Central')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (701, 6, 1, N'F', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (702, 6, 2, N'F', 3, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (703, 6, 3, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (704, 6, 4, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (705, 6, 5, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (706, 6, 6, N'F', 2, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (707, 6, 7, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (708, 6, 8, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (709, 6, 9, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (710, 6, 10, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (711, 6, 11, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (712, 6, 12, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (713, 6, 13, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (714, 6, 14, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (715, 6, 15, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (716, 6, 16, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (717, 6, 17, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (718, 6, 18, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (719, 6, 19, N'F', 1, N'Ventana')
INSERT [dbo].[Asiento] ([Id], [IdVuelo], [Fila], [Letra], [IdClase], [Ubicacion]) VALUES (720, 6, 20, N'F', 1, N'Ventana')
SET IDENTITY_INSERT [dbo].[Asiento] OFF
GO
SET IDENTITY_INSERT [dbo].[Boleto] ON 

INSERT [dbo].[Boleto] ([Id], [IdReserva], [DniPasajero], [FechaEmision]) VALUES (1, 1, N'47333212', CAST(N'2026-09-29T06:50:20.0000000' AS DateTime2))
INSERT [dbo].[Boleto] ([Id], [IdReserva], [DniPasajero], [FechaEmision]) VALUES (2, 2, N'47333212', CAST(N'2026-09-29T06:58:30.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Boleto] OFF
GO
INSERT [dbo].[CanalVenta] ([Id], [Nombre]) VALUES (2, N'Autogestión')
INSERT [dbo].[CanalVenta] ([Id], [Nombre]) VALUES (1, N'Presencial')
GO
SET IDENTITY_INSERT [dbo].[CheckIn] ON 

INSERT [dbo].[CheckIn] ([Id], [IdReserva], [DniPasajero], [IdEstadoCheckIn], [FechaHoraCheckIn], [IdAsiento], [LoginEncargado]) VALUES (1, 1, N'47333212', 1, NULL, NULL, NULL)
INSERT [dbo].[CheckIn] ([Id], [IdReserva], [DniPasajero], [IdEstadoCheckIn], [FechaHoraCheckIn], [IdAsiento], [LoginEncargado]) VALUES (2, 2, N'47333212', 1, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[CheckIn] OFF
GO
INSERT [dbo].[ClaseVuelo] ([Id], [Nombre]) VALUES (1, N'Económica')
INSERT [dbo].[ClaseVuelo] ([Id], [Nombre]) VALUES (2, N'Ejecutiva')
INSERT [dbo].[ClaseVuelo] ([Id], [Nombre]) VALUES (3, N'Primera clase')
GO
INSERT [dbo].[EstadoCheckIn] ([Id], [Nombre]) VALUES (1, N'Pendiente')
INSERT [dbo].[EstadoCheckIn] ([Id], [Nombre]) VALUES (2, N'Realizado')
GO
INSERT [dbo].[EstadoReserva] ([Id], [Nombre]) VALUES (3, N'Cancelada')
INSERT [dbo].[EstadoReserva] ([Id], [Nombre]) VALUES (2, N'Confirmada')
INSERT [dbo].[EstadoReserva] ([Id], [Nombre]) VALUES (1, N'Pendiente de Pago')
GO
INSERT [dbo].[MedioPago] ([Id], [Nombre]) VALUES (4, N'Efectivo')
INSERT [dbo].[MedioPago] ([Id], [Nombre]) VALUES (2, N'Tarjeta de crédito')
INSERT [dbo].[MedioPago] ([Id], [Nombre]) VALUES (1, N'Tarjeta de débito')
INSERT [dbo].[MedioPago] ([Id], [Nombre]) VALUES (3, N'Transferencia')
GO
SET IDENTITY_INSERT [dbo].[Pago] ON 

INSERT [dbo].[Pago] ([Id], [IdReserva], [ImporteTotalAbonado], [IdMedioPago], [NumeroTransaccion], [FechaHoraPago], [LoginVendedor]) VALUES (1, 1, CAST(253132.00 AS Decimal(12, 2)), 3, N'0001', CAST(N'2026-09-29T06:50:20.0000000' AS DateTime2), N'lautaro212')
INSERT [dbo].[Pago] ([Id], [IdReserva], [ImporteTotalAbonado], [IdMedioPago], [NumeroTransaccion], [FechaHoraPago], [LoginVendedor]) VALUES (2, 2, CAST(402446.00 AS Decimal(12, 2)), 1, N'0002', CAST(N'2026-09-29T06:58:30.0000000' AS DateTime2), N'lautaro212')
SET IDENTITY_INSERT [dbo].[Pago] OFF
GO
INSERT [dbo].[Pasajero] ([DNI], [Nombre], [Apellido], [Email], [Telefono], [FechaAlta]) VALUES (N'47333212', N'lautaro', N'vergara', N'B1Kfkv6j+9/yWXidUj5So4zGuizGyQfjLKSwOhJqBhU=', N'1153898209', CAST(N'2026-09-29T06:48:19.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Reserva] ON 

INSERT [dbo].[Reserva] ([Id], [DniCliente], [IdVuelo], [IdClase], [IdTipoViaje], [FechaRegreso], [CantidadPasajeros], [ImporteBase], [SubtotalAdicionales], [Impuestos], [ImporteTotal], [IdEstadoReserva], [FechaRealizacion], [LoginVendedor], [IdCanalVenta], [FechaCancelacion], [MontoPenalidadCancelacion]) VALUES (1, N'47333212', 1, 2, 1, NULL, 1, CAST(209000.00 AS Decimal(12, 2)), CAST(200.00 AS Decimal(12, 2)), CAST(43932.00 AS Decimal(12, 2)), CAST(253132.00 AS Decimal(12, 2)), 2, CAST(N'2026-09-29T06:49:51.0000000' AS DateTime2), N'lautaro212', 1, NULL, NULL)
INSERT [dbo].[Reserva] ([Id], [DniCliente], [IdVuelo], [IdClase], [IdTipoViaje], [FechaRegreso], [CantidadPasajeros], [ImporteBase], [SubtotalAdicionales], [Impuestos], [ImporteTotal], [IdEstadoReserva], [FechaRealizacion], [LoginVendedor], [IdCanalVenta], [FechaCancelacion], [MontoPenalidadCancelacion]) VALUES (2, N'47333212', 1, 3, 1, NULL, 1, CAST(332500.00 AS Decimal(12, 2)), CAST(100.00 AS Decimal(12, 2)), CAST(69846.00 AS Decimal(12, 2)), CAST(402446.00 AS Decimal(12, 2)), 2, CAST(N'2026-09-29T06:58:12.0000000' AS DateTime2), N'lautaro212', 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Reserva] OFF
GO
SET IDENTITY_INSERT [dbo].[ReservaAdicional] ON 

INSERT [dbo].[ReservaAdicional] ([Id], [IdReserva], [IdTipoAdicional], [Cantidad], [CostoUnitario]) VALUES (1, 1, 2, 1, CAST(100.00 AS Decimal(12, 2)))
INSERT [dbo].[ReservaAdicional] ([Id], [IdReserva], [IdTipoAdicional], [Cantidad], [CostoUnitario]) VALUES (2, 1, 1, 1, CAST(100.00 AS Decimal(12, 2)))
INSERT [dbo].[ReservaAdicional] ([Id], [IdReserva], [IdTipoAdicional], [Cantidad], [CostoUnitario]) VALUES (3, 2, 4, 1, CAST(100.00 AS Decimal(12, 2)))
SET IDENTITY_INSERT [dbo].[ReservaAdicional] OFF
GO
INSERT [dbo].[ReservaPasajero] ([IdReserva], [DniPasajero], [IdAsiento]) VALUES (1, N'47333212', 103)
INSERT [dbo].[ReservaPasajero] ([IdReserva], [DniPasajero], [IdAsiento]) VALUES (2, N'47333212', 101)
GO
SET IDENTITY_INSERT [dbo].[TipoAdicional] ON 

INSERT [dbo].[TipoAdicional] ([Id], [Nombre], [Activo]) VALUES (1, N'Equipaje extra', 1)
INSERT [dbo].[TipoAdicional] ([Id], [Nombre], [Activo]) VALUES (2, N'Asiento preferencial', 1)
INSERT [dbo].[TipoAdicional] ([Id], [Nombre], [Activo]) VALUES (3, N'Comida especial', 1)
INSERT [dbo].[TipoAdicional] ([Id], [Nombre], [Activo]) VALUES (4, N'Asistencia prioritaria', 1)
INSERT [dbo].[TipoAdicional] ([Id], [Nombre], [Activo]) VALUES (5, N'Otro', 1)
SET IDENTITY_INSERT [dbo].[TipoAdicional] OFF
GO
INSERT [dbo].[TipoViaje] ([Id], [Nombre]) VALUES (1, N'Ida')
INSERT [dbo].[TipoViaje] ([Id], [Nombre]) VALUES (2, N'Ida y Vuelta')
GO
SET IDENTITY_INSERT [dbo].[Vuelo] ON 

INSERT [dbo].[Vuelo] ([Id], [CodigoVuelo], [IdAerolinea], [IdOrigen], [IdDestino], [FechaHoraSalida], [FechaHoraLlegada], [PuertaEmbarque], [CostoKiloExceso], [BorradoLogico]) VALUES (1, N'AR1500', 1, 1, 3, CAST(N'2026-09-29T10:30:00.0000000' AS DateTime2), CAST(N'2026-09-29T11:50:00.0000000' AS DateTime2), N'A4', CAST(3500.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[Vuelo] ([Id], [CodigoVuelo], [IdAerolinea], [IdOrigen], [IdDestino], [FechaHoraSalida], [FechaHoraLlegada], [PuertaEmbarque], [CostoKiloExceso], [BorradoLogico]) VALUES (2, N'AR1502', 1, 1, 3, CAST(N'2026-09-29T18:00:00.0000000' AS DateTime2), CAST(N'2026-09-29T19:20:00.0000000' AS DateTime2), N'A6', CAST(3500.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[Vuelo] ([Id], [CodigoVuelo], [IdAerolinea], [IdOrigen], [IdDestino], [FechaHoraSalida], [FechaHoraLlegada], [PuertaEmbarque], [CostoKiloExceso], [BorradoLogico]) VALUES (3, N'FB3020', 2, 1, 4, CAST(N'2026-10-01T07:15:00.0000000' AS DateTime2), CAST(N'2026-10-01T09:10:00.0000000' AS DateTime2), N'B2', CAST(3000.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[Vuelo] ([Id], [CodigoVuelo], [IdAerolinea], [IdOrigen], [IdDestino], [FechaHoraSalida], [FechaHoraLlegada], [PuertaEmbarque], [CostoKiloExceso], [BorradoLogico]) VALUES (4, N'JA8110', 3, 1, 5, CAST(N'2026-10-03T09:00:00.0000000' AS DateTime2), CAST(N'2026-10-03T11:20:00.0000000' AS DateTime2), N'C1', CAST(3200.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[Vuelo] ([Id], [CodigoVuelo], [IdAerolinea], [IdOrigen], [IdDestino], [FechaHoraSalida], [FechaHoraLlegada], [PuertaEmbarque], [CostoKiloExceso], [BorradoLogico]) VALUES (5, N'AR1880', 1, 2, 6, CAST(N'2026-10-04T13:45:00.0000000' AS DateTime2), CAST(N'2026-10-04T15:45:00.0000000' AS DateTime2), N'D3', CAST(3800.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[Vuelo] ([Id], [CodigoVuelo], [IdAerolinea], [IdOrigen], [IdDestino], [FechaHoraSalida], [FechaHoraLlegada], [PuertaEmbarque], [CostoKiloExceso], [BorradoLogico]) VALUES (6, N'AR1501', 1, 3, 1, CAST(N'2026-10-05T16:00:00.0000000' AS DateTime2), CAST(N'2026-10-05T17:20:00.0000000' AS DateTime2), N'B5', CAST(3500.00 AS Decimal(10, 2)), 0)
SET IDENTITY_INSERT [dbo].[Vuelo] OFF
GO
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (1, 1, CAST(95000.00 AS Decimal(12, 2)), 84, 0, CAST(15.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (1, 2, CAST(209000.00 AS Decimal(12, 2)), 24, 1, CAST(23.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (1, 3, CAST(332500.00 AS Decimal(12, 2)), 12, 1, CAST(32.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (2, 1, CAST(98000.00 AS Decimal(12, 2)), 84, 0, CAST(15.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (2, 2, CAST(215600.00 AS Decimal(12, 2)), 24, 0, CAST(23.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (2, 3, CAST(343000.00 AS Decimal(12, 2)), 12, 0, CAST(32.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (3, 1, CAST(72000.00 AS Decimal(12, 2)), 84, 0, CAST(15.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (3, 2, CAST(158400.00 AS Decimal(12, 2)), 24, 0, CAST(23.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (3, 3, CAST(252000.00 AS Decimal(12, 2)), 12, 0, CAST(32.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (4, 1, CAST(89000.00 AS Decimal(12, 2)), 84, 0, CAST(15.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (4, 2, CAST(195800.00 AS Decimal(12, 2)), 24, 0, CAST(23.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (4, 3, CAST(311500.00 AS Decimal(12, 2)), 12, 0, CAST(32.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (5, 1, CAST(130000.00 AS Decimal(12, 2)), 84, 0, CAST(15.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (5, 2, CAST(286000.00 AS Decimal(12, 2)), 24, 0, CAST(23.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (5, 3, CAST(455000.00 AS Decimal(12, 2)), 12, 0, CAST(32.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (6, 1, CAST(95000.00 AS Decimal(12, 2)), 84, 0, CAST(15.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (6, 2, CAST(209000.00 AS Decimal(12, 2)), 24, 0, CAST(23.00 AS Decimal(6, 2)))
INSERT [dbo].[VueloClase] ([IdVuelo], [IdClase], [PrecioBase], [CapacidadAsientos], [AsientosReservados], [FranquiciaEquipajeKg]) VALUES (6, 3, CAST(332500.00 AS Decimal(12, 2)), 12, 0, CAST(32.00 AS Decimal(6, 2)))
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ__Aeroline__75E3EFCF0F07AD45] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[Aerolinea] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ__Aeropuer__D00F17F654C742E5] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[Aeropuerto] ADD UNIQUE NONCLUSTERED 
(
	[CodigoIata] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_Asiento_Vuelo_Numero] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[Asiento] ADD  CONSTRAINT [UQ_Asiento_Vuelo_Numero] UNIQUE NONCLUSTERED 
(
	[IdVuelo] ASC,
	[Fila] ASC,
	[Letra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_Boleto_Numero] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[Boleto] ADD  CONSTRAINT [UQ_Boleto_Numero] UNIQUE NONCLUSTERED 
(
	[NumeroBoleto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_Boleto_Pasajero] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[Boleto] ADD  CONSTRAINT [UQ_Boleto_Pasajero] UNIQUE NONCLUSTERED 
(
	[IdReserva] ASC,
	[DniPasajero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ__CanalVen__75E3EFCF08259E1D] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[CanalVenta] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Objeto: Index [UQ_Cargo_Equipaje] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[CargoExcesoEquipaje] ADD  CONSTRAINT [UQ_Cargo_Equipaje] UNIQUE NONCLUSTERED 
(
	[IdEquipaje] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_CheckIn_Pasajero] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[CheckIn] ADD  CONSTRAINT [UQ_CheckIn_Pasajero] UNIQUE NONCLUSTERED 
(
	[IdReserva] ASC,
	[DniPasajero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Objeto: Index [UX_CheckIn_Asiento] Fecha de script: 29/09/2026 08:02:22 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UX_CheckIn_Asiento] ON [dbo].[CheckIn]
(
	[IdAsiento] ASC
)
WHERE ([IdAsiento] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ__ClaseVue__75E3EFCF8E97FE37] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[ClaseVuelo] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Objeto: Index [UQ_Equipaje_CheckIn] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[Equipaje] ADD  CONSTRAINT [UQ_Equipaje_CheckIn] UNIQUE NONCLUSTERED 
(
	[IdCheckIn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ__EstadoCh__75E3EFCF51EA6BCD] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[EstadoCheckIn] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ__EstadoRe__75E3EFCFE0DB42F3] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[EstadoReserva] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_Etiqueta_Codigo] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[EtiquetaEquipaje] ADD  CONSTRAINT [UQ_Etiqueta_Codigo] UNIQUE NONCLUSTERED 
(
	[CodigoEquipaje] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ__MedioPag__75E3EFCF2A84DEC7] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[MedioPago] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Objeto: Index [UQ_Pago_Reserva] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[Pago] ADD  CONSTRAINT [UQ_Pago_Reserva] UNIQUE NONCLUSTERED 
(
	[IdReserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_Pago_Transaccion] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[Pago] ADD  CONSTRAINT [UQ_Pago_Transaccion] UNIQUE NONCLUSTERED 
(
	[NumeroTransaccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_Reserva_Numero] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[Reserva] ADD  CONSTRAINT [UQ_Reserva_Numero] UNIQUE NONCLUSTERED 
(
	[NumeroReserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Objeto: Index [UX_ReservaPasajero_Asiento] Fecha de script: 29/09/2026 08:02:22 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UX_ReservaPasajero_Asiento] ON [dbo].[ReservaPasajero]
(
	[IdAsiento] ASC
)
WHERE ([IdAsiento] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Objeto: Index [UQ_Tarjeta_CheckIn] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[TarjetaEmbarque] ADD  CONSTRAINT [UQ_Tarjeta_CheckIn] UNIQUE NONCLUSTERED 
(
	[IdCheckIn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_Tarjeta_Numero] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[TarjetaEmbarque] ADD  CONSTRAINT [UQ_Tarjeta_Numero] UNIQUE NONCLUSTERED 
(
	[NumeroTarjeta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ__TipoAdic__75E3EFCF096B8E8E] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[TipoAdicional] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ__TipoViaj__75E3EFCFB1B50D67] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[TipoViaje] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ__Vuelo__76E0A7D336A40436] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[Vuelo] ADD UNIQUE NONCLUSTERED 
(
	[CodigoVuelo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Objeto: Index [IX_Vuelo_Busqueda] Fecha de script: 29/09/2026 08:02:22 ******/
CREATE NONCLUSTERED INDEX [IX_Vuelo_Busqueda] ON [dbo].[Vuelo]
(
	[IdOrigen] ASC,
	[IdDestino] ASC,
	[FechaHoraSalida] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Boleto] ADD  CONSTRAINT [DF_Boleto_Fecha]  DEFAULT (getdate()) FOR [FechaEmision]
GO
ALTER TABLE [dbo].[CargoExcesoEquipaje] ADD  CONSTRAINT [DF_Cargo_Fecha]  DEFAULT (getdate()) FOR [FechaHoraCobro]
GO
ALTER TABLE [dbo].[CheckIn] ADD  CONSTRAINT [DF_CheckIn_Estado]  DEFAULT ((1)) FOR [IdEstadoCheckIn]
GO
ALTER TABLE [dbo].[Pasajero] ADD  CONSTRAINT [DF_Pasajero_FechaAlta]  DEFAULT (getdate()) FOR [FechaAlta]
GO
ALTER TABLE [dbo].[Pago] ADD  CONSTRAINT [DF_Pago_Fecha]  DEFAULT (getdate()) FOR [FechaHoraPago]
GO
ALTER TABLE [dbo].[Reserva] ADD  CONSTRAINT [DF_Reserva_Subtotal]  DEFAULT ((0)) FOR [SubtotalAdicionales]
GO
ALTER TABLE [dbo].[Reserva] ADD  CONSTRAINT [DF_Reserva_Estado]  DEFAULT ((1)) FOR [IdEstadoReserva]
GO
ALTER TABLE [dbo].[Reserva] ADD  CONSTRAINT [DF_Reserva_Fecha]  DEFAULT (getdate()) FOR [FechaRealizacion]
GO
ALTER TABLE [dbo].[Reserva] ADD  CONSTRAINT [DF_Reserva_Canal]  DEFAULT ((1)) FOR [IdCanalVenta]
GO
ALTER TABLE [dbo].[TarjetaEmbarque] ADD  CONSTRAINT [DF_Tarjeta_Fecha]  DEFAULT (getdate()) FOR [FechaHoraEmision]
GO
ALTER TABLE [dbo].[TipoAdicional] ADD  CONSTRAINT [DF_TipoAdicional_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[VueloClase] ADD  CONSTRAINT [DF_VueloClase_Reservados]  DEFAULT ((0)) FOR [AsientosReservados]
GO
ALTER TABLE [dbo].[Asiento]  WITH CHECK ADD  CONSTRAINT [FK_Asiento_Clase] FOREIGN KEY([IdClase])
REFERENCES [dbo].[ClaseVuelo] ([Id])
GO
ALTER TABLE [dbo].[Asiento] CHECK CONSTRAINT [FK_Asiento_Clase]
GO
ALTER TABLE [dbo].[Asiento]  WITH CHECK ADD  CONSTRAINT [FK_Asiento_Vuelo] FOREIGN KEY([IdVuelo])
REFERENCES [dbo].[Vuelo] ([Id])
GO
ALTER TABLE [dbo].[Asiento] CHECK CONSTRAINT [FK_Asiento_Vuelo]
GO
ALTER TABLE [dbo].[Boleto]  WITH CHECK ADD  CONSTRAINT [FK_Boleto_ReservaPasajero] FOREIGN KEY([IdReserva], [DniPasajero])
REFERENCES [dbo].[ReservaPasajero] ([IdReserva], [DniPasajero])
GO
ALTER TABLE [dbo].[Boleto] CHECK CONSTRAINT [FK_Boleto_ReservaPasajero]
GO
ALTER TABLE [dbo].[CargoExcesoEquipaje]  WITH CHECK ADD  CONSTRAINT [FK_Cargo_Equipaje] FOREIGN KEY([IdEquipaje])
REFERENCES [dbo].[Equipaje] ([Id])
GO
ALTER TABLE [dbo].[CargoExcesoEquipaje] CHECK CONSTRAINT [FK_Cargo_Equipaje]
GO
ALTER TABLE [dbo].[CargoExcesoEquipaje]  WITH CHECK ADD  CONSTRAINT [FK_Cargo_Medio] FOREIGN KEY([IdMedioPago])
REFERENCES [dbo].[MedioPago] ([Id])
GO
ALTER TABLE [dbo].[CargoExcesoEquipaje] CHECK CONSTRAINT [FK_Cargo_Medio]
GO
ALTER TABLE [dbo].[CheckIn]  WITH CHECK ADD  CONSTRAINT [FK_CheckIn_Asiento] FOREIGN KEY([IdAsiento])
REFERENCES [dbo].[Asiento] ([Id])
GO
ALTER TABLE [dbo].[CheckIn] CHECK CONSTRAINT [FK_CheckIn_Asiento]
GO
ALTER TABLE [dbo].[CheckIn]  WITH CHECK ADD  CONSTRAINT [FK_CheckIn_Estado] FOREIGN KEY([IdEstadoCheckIn])
REFERENCES [dbo].[EstadoCheckIn] ([Id])
GO
ALTER TABLE [dbo].[CheckIn] CHECK CONSTRAINT [FK_CheckIn_Estado]
GO
ALTER TABLE [dbo].[CheckIn]  WITH CHECK ADD  CONSTRAINT [FK_CheckIn_ReservaPasajero] FOREIGN KEY([IdReserva], [DniPasajero])
REFERENCES [dbo].[ReservaPasajero] ([IdReserva], [DniPasajero])
GO
ALTER TABLE [dbo].[CheckIn] CHECK CONSTRAINT [FK_CheckIn_ReservaPasajero]
GO
ALTER TABLE [dbo].[Equipaje]  WITH CHECK ADD  CONSTRAINT [FK_Equipaje_CheckIn] FOREIGN KEY([IdCheckIn])
REFERENCES [dbo].[CheckIn] ([Id])
GO
ALTER TABLE [dbo].[Equipaje] CHECK CONSTRAINT [FK_Equipaje_CheckIn]
GO
ALTER TABLE [dbo].[EtiquetaEquipaje]  WITH CHECK ADD  CONSTRAINT [FK_Etiqueta_Equipaje] FOREIGN KEY([IdEquipaje])
REFERENCES [dbo].[Equipaje] ([Id])
GO
ALTER TABLE [dbo].[EtiquetaEquipaje] CHECK CONSTRAINT [FK_Etiqueta_Equipaje]
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD  CONSTRAINT [FK_Pago_Medio] FOREIGN KEY([IdMedioPago])
REFERENCES [dbo].[MedioPago] ([Id])
GO
ALTER TABLE [dbo].[Pago] CHECK CONSTRAINT [FK_Pago_Medio]
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD  CONSTRAINT [FK_Pago_Reserva] FOREIGN KEY([IdReserva])
REFERENCES [dbo].[Reserva] ([Id])
GO
ALTER TABLE [dbo].[Pago] CHECK CONSTRAINT [FK_Pago_Reserva]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_CanalVenta] FOREIGN KEY([IdCanalVenta])
REFERENCES [dbo].[CanalVenta] ([Id])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_CanalVenta]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Pasajero] FOREIGN KEY([DniCliente])
REFERENCES [dbo].[Pasajero] ([DNI])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Pasajero]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Estado] FOREIGN KEY([IdEstadoReserva])
REFERENCES [dbo].[EstadoReserva] ([Id])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Estado]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_TipoViaje] FOREIGN KEY([IdTipoViaje])
REFERENCES [dbo].[TipoViaje] ([Id])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_TipoViaje]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_VueloClase] FOREIGN KEY([IdVuelo], [IdClase])
REFERENCES [dbo].[VueloClase] ([IdVuelo], [IdClase])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_VueloClase]
GO
ALTER TABLE [dbo].[ReservaAdicional]  WITH CHECK ADD  CONSTRAINT [FK_ReservaAdicional_Reserva] FOREIGN KEY([IdReserva])
REFERENCES [dbo].[Reserva] ([Id])
GO
ALTER TABLE [dbo].[ReservaAdicional] CHECK CONSTRAINT [FK_ReservaAdicional_Reserva]
GO
ALTER TABLE [dbo].[ReservaAdicional]  WITH CHECK ADD  CONSTRAINT [FK_ReservaAdicional_Tipo] FOREIGN KEY([IdTipoAdicional])
REFERENCES [dbo].[TipoAdicional] ([Id])
GO
ALTER TABLE [dbo].[ReservaAdicional] CHECK CONSTRAINT [FK_ReservaAdicional_Tipo]
GO
ALTER TABLE [dbo].[ReservaPasajero]  WITH CHECK ADD  CONSTRAINT [FK_ReservaPasajero_Asiento] FOREIGN KEY([IdAsiento])
REFERENCES [dbo].[Asiento] ([Id])
GO
ALTER TABLE [dbo].[ReservaPasajero] CHECK CONSTRAINT [FK_ReservaPasajero_Asiento]
GO
ALTER TABLE [dbo].[ReservaPasajero]  WITH CHECK ADD  CONSTRAINT [FK_ReservaPasajero_Pasajero] FOREIGN KEY([DniPasajero])
REFERENCES [dbo].[Pasajero] ([DNI])
GO
ALTER TABLE [dbo].[ReservaPasajero] CHECK CONSTRAINT [FK_ReservaPasajero_Pasajero]
GO
ALTER TABLE [dbo].[ReservaPasajero]  WITH CHECK ADD  CONSTRAINT [FK_ReservaPasajero_Reserva] FOREIGN KEY([IdReserva])
REFERENCES [dbo].[Reserva] ([Id])
GO
ALTER TABLE [dbo].[ReservaPasajero] CHECK CONSTRAINT [FK_ReservaPasajero_Reserva]
GO
ALTER TABLE [dbo].[TarjetaEmbarque]  WITH CHECK ADD  CONSTRAINT [FK_Tarjeta_CheckIn] FOREIGN KEY([IdCheckIn])
REFERENCES [dbo].[CheckIn] ([Id])
GO
ALTER TABLE [dbo].[TarjetaEmbarque] CHECK CONSTRAINT [FK_Tarjeta_CheckIn]
GO
ALTER TABLE [dbo].[Vuelo]  WITH CHECK ADD  CONSTRAINT [FK_Vuelo_Aerolinea] FOREIGN KEY([IdAerolinea])
REFERENCES [dbo].[Aerolinea] ([Id])
GO
ALTER TABLE [dbo].[Vuelo] CHECK CONSTRAINT [FK_Vuelo_Aerolinea]
GO
ALTER TABLE [dbo].[Vuelo]  WITH CHECK ADD  CONSTRAINT [FK_Vuelo_Destino] FOREIGN KEY([IdDestino])
REFERENCES [dbo].[Aeropuerto] ([Id])
GO
ALTER TABLE [dbo].[Vuelo] CHECK CONSTRAINT [FK_Vuelo_Destino]
GO
ALTER TABLE [dbo].[Vuelo]  WITH CHECK ADD  CONSTRAINT [FK_Vuelo_Origen] FOREIGN KEY([IdOrigen])
REFERENCES [dbo].[Aeropuerto] ([Id])
GO
ALTER TABLE [dbo].[Vuelo] CHECK CONSTRAINT [FK_Vuelo_Origen]
GO
ALTER TABLE [dbo].[VueloClase]  WITH CHECK ADD  CONSTRAINT [FK_VueloClase_Clase] FOREIGN KEY([IdClase])
REFERENCES [dbo].[ClaseVuelo] ([Id])
GO
ALTER TABLE [dbo].[VueloClase] CHECK CONSTRAINT [FK_VueloClase_Clase]
GO
ALTER TABLE [dbo].[VueloClase]  WITH CHECK ADD  CONSTRAINT [FK_VueloClase_Vuelo] FOREIGN KEY([IdVuelo])
REFERENCES [dbo].[Vuelo] ([Id])
GO
ALTER TABLE [dbo].[VueloClase] CHECK CONSTRAINT [FK_VueloClase_Vuelo]
GO
ALTER TABLE [dbo].[Asiento]  WITH CHECK ADD  CONSTRAINT [CK_Asiento_Ubicacion] CHECK  (([Ubicacion]=N'Pasillo' OR [Ubicacion]=N'Central' OR [Ubicacion]=N'Ventana'))
GO
ALTER TABLE [dbo].[Asiento] CHECK CONSTRAINT [CK_Asiento_Ubicacion]
GO
ALTER TABLE [dbo].[CargoExcesoEquipaje]  WITH CHECK ADD  CONSTRAINT [CK_Cargo_Kilos] CHECK  (([KilosExceso]>(0)))
GO
ALTER TABLE [dbo].[CargoExcesoEquipaje] CHECK CONSTRAINT [CK_Cargo_Kilos]
GO
ALTER TABLE [dbo].[Equipaje]  WITH CHECK ADD  CONSTRAINT [CK_Equipaje_Bultos] CHECK  (([CantidadBultos]>(0)))
GO
ALTER TABLE [dbo].[Equipaje] CHECK CONSTRAINT [CK_Equipaje_Bultos]
GO
ALTER TABLE [dbo].[Equipaje]  WITH CHECK ADD  CONSTRAINT [CK_Equipaje_Peso] CHECK  (([PesoTotalKg]>(0)))
GO
ALTER TABLE [dbo].[Equipaje] CHECK CONSTRAINT [CK_Equipaje_Peso]
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD  CONSTRAINT [CK_Pago_Importe] CHECK  (([ImporteTotalAbonado]>(0)))
GO
ALTER TABLE [dbo].[Pago] CHECK CONSTRAINT [CK_Pago_Importe]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [CK_Reserva_Cantidad] CHECK  (([CantidadPasajeros]>(0)))
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [CK_Reserva_Cantidad]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [CK_Reserva_FechaRegreso] CHECK  (([IdTipoViaje]=(1) AND [FechaRegreso] IS NULL OR [IdTipoViaje]=(2) AND [FechaRegreso] IS NOT NULL))
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [CK_Reserva_FechaRegreso]
GO
ALTER TABLE [dbo].[ReservaAdicional]  WITH CHECK ADD  CONSTRAINT [CK_ReservaAdicional_Cantidad] CHECK  (([Cantidad]>(0)))
GO
ALTER TABLE [dbo].[ReservaAdicional] CHECK CONSTRAINT [CK_ReservaAdicional_Cantidad]
GO
ALTER TABLE [dbo].[ReservaAdicional]  WITH CHECK ADD  CONSTRAINT [CK_ReservaAdicional_Costo] CHECK  (([CostoUnitario]>=(0)))
GO
ALTER TABLE [dbo].[ReservaAdicional] CHECK CONSTRAINT [CK_ReservaAdicional_Costo]
GO
ALTER TABLE [dbo].[Vuelo]  WITH CHECK ADD  CONSTRAINT [CK_Vuelo_CostoKilo] CHECK  (([CostoKiloExceso]>=(0)))
GO
ALTER TABLE [dbo].[Vuelo] CHECK CONSTRAINT [CK_Vuelo_CostoKilo]
GO
ALTER TABLE [dbo].[Vuelo]  WITH CHECK ADD  CONSTRAINT [CK_Vuelo_Horario] CHECK  (([FechaHoraLlegada]>[FechaHoraSalida]))
GO
ALTER TABLE [dbo].[Vuelo] CHECK CONSTRAINT [CK_Vuelo_Horario]
GO
ALTER TABLE [dbo].[Vuelo]  WITH CHECK ADD  CONSTRAINT [CK_Vuelo_Ruta] CHECK  (([IdOrigen]<>[IdDestino]))
GO
ALTER TABLE [dbo].[Vuelo] CHECK CONSTRAINT [CK_Vuelo_Ruta]
GO
ALTER TABLE [dbo].[VueloClase]  WITH CHECK ADD  CONSTRAINT [CK_VueloClase_Capacidad] CHECK  (([CapacidadAsientos]>=(0)))
GO
ALTER TABLE [dbo].[VueloClase] CHECK CONSTRAINT [CK_VueloClase_Capacidad]
GO
ALTER TABLE [dbo].[VueloClase]  WITH CHECK ADD  CONSTRAINT [CK_VueloClase_Cupo] CHECK  (([AsientosReservados]>=(0) AND [AsientosReservados]<=[CapacidadAsientos]))
GO
ALTER TABLE [dbo].[VueloClase] CHECK CONSTRAINT [CK_VueloClase_Cupo]
GO
ALTER TABLE [dbo].[VueloClase]  WITH CHECK ADD  CONSTRAINT [CK_VueloClase_Franquicia] CHECK  (([FranquiciaEquipajeKg]>=(0)))
GO
ALTER TABLE [dbo].[VueloClase] CHECK CONSTRAINT [CK_VueloClase_Franquicia]
GO
ALTER TABLE [dbo].[VueloClase]  WITH CHECK ADD  CONSTRAINT [CK_VueloClase_Precio] CHECK  (([PrecioBase]>=(0)))
GO
ALTER TABLE [dbo].[VueloClase] CHECK CONSTRAINT [CK_VueloClase_Precio]
GO
ALTER TABLE [dbo].[Vuelo] ADD  CONSTRAINT [DF_Vuelo_BorradoLogico]  DEFAULT ((0)) FOR [BorradoLogico]
GO
ALTER TABLE [dbo].[Vuelo_C]  WITH CHECK ADD  CONSTRAINT [FK_Vuelo_C_Vuelo] FOREIGN KEY([IdVuelo])
REFERENCES [dbo].[Vuelo] ([Id])
GO
ALTER TABLE [dbo].[Vuelo_C] CHECK CONSTRAINT [FK_Vuelo_C_Vuelo]
GO
ALTER TABLE [dbo].[Vuelo_C]  WITH CHECK ADD  CONSTRAINT [FK_Vuelo_C_Aerolinea] FOREIGN KEY([IdAerolinea])
REFERENCES [dbo].[Aerolinea] ([Id])
GO
ALTER TABLE [dbo].[Vuelo_C] CHECK CONSTRAINT [FK_Vuelo_C_Aerolinea]
GO
ALTER TABLE [dbo].[Vuelo_C]  WITH CHECK ADD  CONSTRAINT [FK_Vuelo_C_Origen] FOREIGN KEY([IdOrigen])
REFERENCES [dbo].[Aeropuerto] ([Id])
GO
ALTER TABLE [dbo].[Vuelo_C] CHECK CONSTRAINT [FK_Vuelo_C_Origen]
GO
ALTER TABLE [dbo].[Vuelo_C]  WITH CHECK ADD  CONSTRAINT [FK_Vuelo_C_Destino] FOREIGN KEY([IdDestino])
REFERENCES [dbo].[Aeropuerto] ([Id])
GO
ALTER TABLE [dbo].[Vuelo_C] CHECK CONSTRAINT [FK_Vuelo_C_Destino]
GO
/****** Un solo registro activo (Act = 1) por vuelo ******/
CREATE UNIQUE NONCLUSTERED INDEX [UX_Vuelo_C_Activo] ON [dbo].[Vuelo_C]
(
	[IdVuelo] ASC
)
WHERE ([Act]=(1))
GO
CREATE NONCLUSTERED INDEX [IX_Vuelo_C_Codigo_Fecha] ON [dbo].[Vuelo_C]
(
	[CodigoVuelo] ASC,
	[Fecha] ASC
)
GO
/****** Carga inicial: la versión actual de cada vuelo queda como registro activo ******/
INSERT INTO [dbo].[Vuelo_C] ([IdVuelo], [Fecha], [Hora], [CodigoVuelo], [IdAerolinea], [IdOrigen], [IdDestino],
	[FechaHoraSalida], [FechaHoraLlegada], [PuertaEmbarque], [CostoKiloExceso], [BorradoLogico], [Act])
SELECT [Id], CAST(GETDATE() AS date), CAST(GETDATE() AS time(0)), [CodigoVuelo], [IdAerolinea], [IdOrigen], [IdDestino],
	[FechaHoraSalida], [FechaHoraLlegada], [PuertaEmbarque], [CostoKiloExceso], [BorradoLogico], 1
FROM [dbo].[Vuelo]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Vuelo: cada alta o cambio de datos genera un registro nuevo en Vuelo_C con Act = 1 ******/
CREATE TRIGGER [dbo].[TR_Vuelo_Historial] ON [dbo].[Vuelo]
AFTER INSERT, UPDATE
AS
BEGIN
	SET NOCOUNT ON;

	-- El registro activo anterior deja de serlo solo si los datos realmente cambiaron.
	-- (Al activar una versión vieja, Vuelo se actualiza con datos idénticos a los del registro
	-- que ya quedó activo: no corresponde crear una versión nueva.)
	UPDATE h SET h.[Act] = 0
	FROM [dbo].[Vuelo_C] h
	INNER JOIN inserted i ON i.[Id] = h.[IdVuelo]
	WHERE h.[Act] = 1
	  AND (h.[CodigoVuelo]     <> i.[CodigoVuelo]
	    OR h.[IdAerolinea]     <> i.[IdAerolinea]
	    OR h.[IdOrigen]        <> i.[IdOrigen]
	    OR h.[IdDestino]       <> i.[IdDestino]
	    OR h.[FechaHoraSalida] <> i.[FechaHoraSalida]
	    OR h.[FechaHoraLlegada]<> i.[FechaHoraLlegada]
	    OR h.[PuertaEmbarque]  <> i.[PuertaEmbarque]
	    OR h.[CostoKiloExceso] <> i.[CostoKiloExceso]
	    OR h.[BorradoLogico]   <> i.[BorradoLogico]);

	-- Vuelos sin registro activo (alta nueva o cambio recién detectado): nueva versión activa.
	INSERT INTO [dbo].[Vuelo_C] ([IdVuelo], [Fecha], [Hora], [CodigoVuelo], [IdAerolinea], [IdOrigen], [IdDestino],
		[FechaHoraSalida], [FechaHoraLlegada], [PuertaEmbarque], [CostoKiloExceso], [BorradoLogico], [Act])
	SELECT i.[Id], CAST(GETDATE() AS date), CAST(GETDATE() AS time(0)), i.[CodigoVuelo], i.[IdAerolinea], i.[IdOrigen], i.[IdDestino],
		i.[FechaHoraSalida], i.[FechaHoraLlegada], i.[PuertaEmbarque], i.[CostoKiloExceso], i.[BorradoLogico], 1
	FROM inserted i
	WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Vuelo_C] a WHERE a.[IdVuelo] = i.[Id] AND a.[Act] = 1);
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Vuelo: el borrado es lógico. Un DELETE físico se convierte en baja lógica ******/
CREATE TRIGGER [dbo].[TR_Vuelo_BorradoLogico] ON [dbo].[Vuelo]
INSTEAD OF DELETE
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE v SET v.[BorradoLogico] = 1
	FROM [dbo].[Vuelo] v
	INNER JOIN deleted d ON d.[Id] = v.[Id];
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Vuelo_C: no se elimina ningún registro ******/
CREATE TRIGGER [dbo].[TR_Vuelo_C_SinBorrado] ON [dbo].[Vuelo_C]
INSTEAD OF DELETE
AS
BEGIN
	SET NOCOUNT ON;
	THROW 50001, N'Vuelo_C es una bitácora: no se puede eliminar ningún registro.', 1;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Vuelo_C: de un registro solo se puede modificar la columna Act ******/
CREATE TRIGGER [dbo].[TR_Vuelo_C_SoloAct] ON [dbo].[Vuelo_C]
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	IF UPDATE([IdVuelo]) OR UPDATE([Fecha]) OR UPDATE([Hora]) OR UPDATE([CodigoVuelo]) OR UPDATE([IdAerolinea])
	   OR UPDATE([IdOrigen]) OR UPDATE([IdDestino]) OR UPDATE([FechaHoraSalida]) OR UPDATE([FechaHoraLlegada])
	   OR UPDATE([PuertaEmbarque]) OR UPDATE([CostoKiloExceso]) OR UPDATE([BorradoLogico])
	BEGIN
		ROLLBACK TRANSACTION;
		THROW 50002, N'Vuelo_C es una bitácora: de un registro solo se puede modificar la columna Act.', 1;
	END
END
GO
/* ---------- Módulos de bitácora del negocio ---------- */
DECLARE @Modulos TABLE (Id INT, Nombre NVARCHAR(50));
INSERT INTO @Modulos (Id, Nombre) VALUES
    (7, N'CheckIn'),
    (6, N'Reservas'),
    (8, N'Vuelos');
SET IDENTITY_INSERT dbo.Modulo ON;
INSERT INTO dbo.Modulo (Id, Nombre)
SELECT M.Id, M.Nombre FROM @Modulos M
WHERE NOT EXISTS (SELECT 1 FROM dbo.Modulo X WHERE X.Nombre = M.Nombre)
  AND NOT EXISTS (SELECT 1 FROM dbo.Modulo X WHERE X.Id = M.Id);
SET IDENTITY_INSERT dbo.Modulo OFF;
-- Si el Id ya estaba ocupado, se inserta con el siguiente Id libre.
INSERT INTO dbo.Modulo (Nombre)
SELECT M.Nombre FROM @Modulos M WHERE NOT EXISTS (SELECT 1 FROM dbo.Modulo X WHERE X.Nombre = M.Nombre);
GO

/* ---------- Patentes (permisos) del negocio ---------- */
DECLARE @Patentes TABLE (Id INT, Nombre NVARCHAR(100), DataKey NVARCHAR(100));
INSERT INTO @Patentes (Id, Nombre, DataKey) VALUES
    (19, N'Reservas - Generar', N'Reservas.Generar'),
    (20, N'Reservas - Consultar', N'Reservas.Consultar'),
    (21, N'Pagos - Registrar', N'Pagos.Registrar'),
    (22, N'Check-in - Realizar', N'CheckIn.Realizar'),
    (23, N'Reservas - Generar (propia)', N'Reservas.GenerarPropia'),
    (24, N'Reservas - Consultar (propia)', N'Reservas.ConsultarPropia'),
    (25, N'Pagos - Registrar (propio)', N'Pagos.RegistrarPropio'),
    (26, N'Reservas - Cancelar', N'Reservas.Cancelar'),
    (27, N'Reservas - Cancelar (propia)', N'Reservas.CancelarPropia'),
    (28, N'Vuelos - Gestionar', N'Vuelos.Gestionar'),
    (29, N'Vuelos - Bitácora de cambios', N'Vuelos.Bitacora'),
    (30, N'Vuelos - Activar versión', N'Vuelos.Activar');
SET IDENTITY_INSERT dbo.Patente ON;
INSERT INTO dbo.Patente (Id, Nombre, DataKey)
SELECT P.Id, P.Nombre, P.DataKey FROM @Patentes P
WHERE NOT EXISTS (SELECT 1 FROM dbo.Patente X WHERE X.DataKey = P.DataKey)
  AND NOT EXISTS (SELECT 1 FROM dbo.Patente X WHERE X.Id = P.Id);
SET IDENTITY_INSERT dbo.Patente OFF;
INSERT INTO dbo.Patente (Nombre, DataKey)
SELECT P.Nombre, P.DataKey FROM @Patentes P WHERE NOT EXISTS (SELECT 1 FROM dbo.Patente X WHERE X.DataKey = P.DataKey);
GO

/* ---------- Tipos de evento de bitácora del negocio ---------- */
DECLARE @Eventos TABLE (Id INT, Nombre NVARCHAR(100));
INSERT INTO @Eventos (Id, Nombre) VALUES
    (30, N'Asiento asignado'),
    (34, N'Asiento seleccionado'),
    (32, N'Check-in realizado'),
    (33, N'Cliente autogestionado registrado'),
    (26, N'Cliente registrado'),
    (29, N'Equipaje despachado'),
    (28, N'Pago registrado'),
    (35, N'Reserva cancelada'),
    (27, N'Reserva generada'),
    (31, N'Tarjeta de embarque emitida'),
    (36, N'Vuelo modificado'),
    (37, N'Vuelo dado de baja'),
    (38, N'Vuelo reactivado'),
    (39, N'Version de vuelo activada'),
    (40, N'Usuarios serializados a XML'),
    (41, N'Usuarios deserializados desde XML');
SET IDENTITY_INSERT dbo.TipoEvento ON;
INSERT INTO dbo.TipoEvento (Id, Nombre)
SELECT E.Id, E.Nombre FROM @Eventos E
WHERE NOT EXISTS (SELECT 1 FROM dbo.TipoEvento X WHERE X.Nombre = E.Nombre)
  AND NOT EXISTS (SELECT 1 FROM dbo.TipoEvento X WHERE X.Id = E.Id);
SET IDENTITY_INSERT dbo.TipoEvento OFF;
INSERT INTO dbo.TipoEvento (Nombre)
SELECT E.Nombre FROM @Eventos E WHERE NOT EXISTS (SELECT 1 FROM dbo.TipoEvento X WHERE X.Nombre = E.Nombre);
GO

/* ---------- Dígitos verificadores: los recalcula el sistema al terminar ---------- */
INSERT INTO dbo.TareaPendiente_GV42 (Nombre)
SELECT X.T FROM (VALUES (N'RecalcularDV:Modulo'), (N'RecalcularDV:Patente'), (N'RecalcularDV:TipoEvento'), (N'RecalcularDV:Reserva'), (N'RecalcularDV:Pago')) AS X(T)
WHERE NOT EXISTS (SELECT 1 FROM dbo.TareaPendiente_GV42 P WHERE P.Nombre = X.T);
GO
