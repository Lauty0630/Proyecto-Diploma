USE [master]
GO
/****** Object:  Database [Gestion Usuario]    Script Date: 7/7/2026 7:40:53 PM ******/
CREATE DATABASE [Gestion Usuario]
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Gestion Usuario].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Gestion Usuario] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Gestion Usuario] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Gestion Usuario] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Gestion Usuario] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Gestion Usuario] SET ARITHABORT OFF 
GO
ALTER DATABASE [Gestion Usuario] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Gestion Usuario] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Gestion Usuario] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Gestion Usuario] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Gestion Usuario] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Gestion Usuario] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Gestion Usuario] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Gestion Usuario] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Gestion Usuario] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Gestion Usuario] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Gestion Usuario] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Gestion Usuario] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Gestion Usuario] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Gestion Usuario] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Gestion Usuario] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Gestion Usuario] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Gestion Usuario] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Gestion Usuario] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Gestion Usuario] SET  MULTI_USER 
GO
ALTER DATABASE [Gestion Usuario] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Gestion Usuario] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Gestion Usuario] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Gestion Usuario] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Gestion Usuario] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Gestion Usuario]
GO
/****** Object:  Table [dbo].[EVENTOS]    Script Date: 7/7/2026 7:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EVENTOS](
	[Id_Evento] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](250) NOT NULL,
	[Criticidad] [nvarchar](50) NOT NULL,
	[FechaHora] [datetime] NULL,
	[IdModulo] [int] NULL,
	[IdTipoEvento] [int] NULL,
	[Detalle] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Evento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Familia]    Script Date: 7/7/2026 7:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Familia](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FamiliaIntegrada]    Script Date: 7/7/2026 7:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FamiliaIntegrada](
	[IdFamiliaPadre] [int] NOT NULL,
	[IdFamiliaHija] [int] NOT NULL,
 CONSTRAINT [PK_FamiliaIntegrada] PRIMARY KEY CLUSTERED 
(
	[IdFamiliaPadre] ASC,
	[IdFamiliaHija] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FamiliaPatente]    Script Date: 7/7/2026 7:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FamiliaPatente](
	[IdFamilia] [int] NOT NULL,
	[IdPatente] [int] NOT NULL,
 CONSTRAINT [PK_FamiliaPatente] PRIMARY KEY CLUSTERED 
(
	[IdFamilia] ASC,
	[IdPatente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IntegridadDVH]    Script Date: 7/7/2026 7:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IntegridadDVH](
	[NombreTabla] [nvarchar](50) NOT NULL,
	[IdRegistro] [nvarchar](50) NOT NULL,
	[DVH] [nvarchar](64) NOT NULL,
	[FechaCalculo] [datetime] NOT NULL,
 CONSTRAINT [PK_IntegridadDVH] PRIMARY KEY CLUSTERED 
(
	[NombreTabla] ASC,
	[IdRegistro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IntegridadDVV]    Script Date: 7/7/2026 7:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IntegridadDVV](
	[NombreTabla] [nvarchar](50) NOT NULL,
	[DVV] [nvarchar](64) NOT NULL,
	[FechaCalculo] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NombreTabla] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Modulo]    Script Date: 7/7/2026 7:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modulo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patente]    Script Date: 7/7/2026 7:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[DataKey] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 7/7/2026 7:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolFamilia]    Script Date: 7/7/2026 7:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolFamilia](
	[IdRol] [int] NOT NULL,
	[IdFamilia] [int] NOT NULL,
 CONSTRAINT [PK_RolFamilia] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC,
	[IdFamilia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolPatente]    Script Date: 7/7/2026 7:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolPatente](
	[IdRol] [int] NOT NULL,
	[IdPatente] [int] NOT NULL,
 CONSTRAINT [PK_RolPatente] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC,
	[IdPatente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoEvento]    Script Date: 7/7/2026 7:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoEvento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 7/7/2026 7:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[DNI] [nvarchar](250) NOT NULL,
	[Apellido] [nvarchar](250) NOT NULL,
	[Nombre] [nvarchar](250) NOT NULL,
	[UserName] [nvarchar](250) NOT NULL,
	[Contrasena] [nvarchar](250) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[Bloqueo] [bit] NOT NULL,
	[Activo] [bit] NOT NULL,
	[IdRol] [int] NOT NULL,
	[IntentosFallidos] [int] NOT NULL,
	[UltimoIntentoFallido] [datetime] NULL,
	[DebeCambiarContrasena] [bit] NOT NULL,
	[Idioma] [nvarchar](5) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DNI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[EVENTOS] ON 

INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2140, N'jeremias544', N'Baja', CAST(N'2026-05-19T10:08:24.127' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2141, N'jeremias544', N'Baja', CAST(N'2026-05-19T10:09:48.330' AS DateTime), 5, 13, N'Login: lautaro222')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2142, N'jeremias544', N'Media', CAST(N'2026-05-19T10:10:06.833' AS DateTime), 5, 10, N'DNI: 12121211')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2143, N'jeremias544', N'Media', CAST(N'2026-05-19T10:10:17.977' AS DateTime), 5, 9, N'DNI: 12121211')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2144, N'jeremias544', N'Media', CAST(N'2026-05-19T10:10:35.730' AS DateTime), 5, 11, N'DNI: 12121211')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2145, N'jeremias544', N'Media', CAST(N'2026-05-19T10:10:52.637' AS DateTime), 4, 1, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2146, N'jeremias544', N'Alta', CAST(N'2026-05-19T10:10:58.530' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2147, N'jeremias544', N'Baja', CAST(N'2026-05-19T10:13:46.477' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2148, N'jeremias544', N'Media', CAST(N'2026-05-19T15:39:15.953' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2149, N'jeremias544', N'Media', CAST(N'2026-05-19T15:39:18.353' AS DateTime), 4, 5, N'Intento 2/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2150, N'jeremias544', N'Media', CAST(N'2026-05-19T15:39:27.017' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2151, N'jeremias544', N'Baja', CAST(N'2026-05-19T15:40:15.900' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2152, N'jeremias544', N'Media', CAST(N'2026-05-19T15:53:36.583' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2153, N'jeremias544', N'Baja', CAST(N'2026-05-19T15:53:41.217' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2154, N'jeremias544', N'Alta', CAST(N'2026-05-19T15:55:01.433' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2155, N'jeremias544', N'Media', CAST(N'2026-05-19T15:55:47.160' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2156, N'jeremias544', N'Media', CAST(N'2026-05-19T15:56:32.077' AS DateTime), 4, 5, N'Intento 2/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2157, N'jeremias544', N'Baja', CAST(N'2026-05-19T16:01:30.647' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2158, N'jeremias544', N'Media', CAST(N'2026-05-19T16:02:06.233' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2159, N'jeremias544', N'Baja', CAST(N'2026-05-19T16:03:42.210' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2160, N'jeremias544', N'Baja', CAST(N'2026-05-19T16:08:53.240' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2161, N'jeremias544', N'Baja', CAST(N'2026-05-19T16:15:49.327' AS DateTime), 5, 13, N'Login: alejo116')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2162, N'jeremias544', N'Media', CAST(N'2026-05-19T16:16:14.270' AS DateTime), 5, 11, N'DNI: 11111116')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2163, N'jeremias544', N'Baja', CAST(N'2026-05-19T16:17:14.227' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2164, N'jeremias544', N'Baja', CAST(N'2026-05-19T16:17:49.283' AS DateTime), 4, 14, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2165, N'jeremias544', N'Alta', CAST(N'2026-05-19T16:17:54.760' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2166, N'jeremias544', N'Baja', CAST(N'2026-05-19T16:18:02.740' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2167, N'jeremias544', N'Baja', CAST(N'2026-05-19T16:19:21.513' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2168, N'jeremias544', N'Baja', CAST(N'2026-05-19T16:34:21.193' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2169, N'jeremias544', N'Baja', CAST(N'2026-05-19T16:36:17.777' AS DateTime), 5, 13, N'Login: dylan542')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2170, N'jeremias544', N'Baja', CAST(N'2026-05-19T16:37:28.473' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2171, N'jeremias544', N'Media', CAST(N'2026-05-19T16:37:48.987' AS DateTime), 4, 1, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2172, N'jeremias544', N'Alta', CAST(N'2026-05-19T16:37:53.440' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2173, N'jeremias544', N'Baja', CAST(N'2026-05-19T16:47:07.150' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2174, N'jeremias544', N'Media', CAST(N'2026-05-19T16:47:18.013' AS DateTime), 5, 11, N'DNI: 11111116')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2175, N'jeremias544', N'Baja', CAST(N'2026-05-19T17:05:15.533' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2176, N'jeremias544', N'Baja', CAST(N'2026-05-19T18:02:15.060' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2177, N'jeremias544', N'Baja', CAST(N'2026-05-19T18:03:02.353' AS DateTime), 5, 13, N'Login: rosalia736')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2178, N'jeremias544', N'Media', CAST(N'2026-05-19T18:03:16.733' AS DateTime), 5, 11, N'DNI: 23642736')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2179, N'jeremias544', N'Media', CAST(N'2026-05-19T18:03:31.573' AS DateTime), 5, 10, N'DNI: 23642736')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2180, N'jeremias544', N'Media', CAST(N'2026-05-19T18:03:58.393' AS DateTime), 5, 10, N'DNI: 11111116')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2181, N'jeremias544', N'Media', CAST(N'2026-05-19T18:04:17.043' AS DateTime), 5, 9, N'DNI: 23642736')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2182, N'jeremias544', N'Alta', CAST(N'2026-05-19T18:05:42.150' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2183, N'jeremias544', N'Media', CAST(N'2026-05-19T18:08:53.240' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2184, N'jeremias544', N'Media', CAST(N'2026-05-19T18:08:54.653' AS DateTime), 4, 5, N'Intento 2/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2185, N'jeremias544', N'Alta', CAST(N'2026-05-19T18:08:55.597' AS DateTime), 4, 6, N'3 intentos fallidos consecutivos dentro de 60 min')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2186, N'lautaro212', N'Baja', CAST(N'2026-05-19T18:09:13.207' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2187, N'jeremias544', N'Alta', CAST(N'2026-05-19T18:12:53.290' AS DateTime), 4, 3, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2188, N'lautaro212', N'Baja', CAST(N'2026-05-19T18:13:07.833' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2189, N'lautaro212', N'Baja', CAST(N'2026-05-19T18:14:03.283' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2190, N'lautaro212', N'Baja', CAST(N'2026-05-19T18:15:08.677' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2191, N'lautaro212', N'Media', CAST(N'2026-05-19T18:20:38.700' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2192, N'lautaro212', N'Baja', CAST(N'2026-05-19T18:20:44.803' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2193, N'lautaro212', N'Alta', CAST(N'2026-05-19T18:20:55.130' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2197, N'lautaro212', N'Baja', CAST(N'2026-05-19T18:52:21.450' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2198, N'lautaro212', N'Baja', CAST(N'2026-05-19T19:10:42.303' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2199, N'lautaro212', N'Media', CAST(N'2026-05-19T19:10:56.417' AS DateTime), 5, 8, N'Usuario jeremias544 desbloqueado y contraseña reseteada')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2200, N'lautaro212', N'Alta', CAST(N'2026-05-19T19:11:06.980' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2201, N'jeremias544', N'Baja', CAST(N'2026-05-19T19:11:12.520' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2202, N'jeremias544', N'Media', CAST(N'2026-05-19T19:14:40.663' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2203, N'jeremias544', N'Media', CAST(N'2026-05-19T19:14:41.593' AS DateTime), 4, 5, N'Intento 2/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2204, N'lautaro212', N'Baja', CAST(N'2026-05-19T19:17:39.900' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2205, N'lautaro212', N'Baja', CAST(N'2026-05-19T19:17:55.303' AS DateTime), 4, 14, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2206, N'lautaro212', N'Media', CAST(N'2026-05-19T19:20:36.543' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2207, N'lautaro212', N'Baja', CAST(N'2026-05-19T19:20:39.873' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2208, N'lautaro212', N'Baja', CAST(N'2026-05-19T19:20:56.907' AS DateTime), 4, 14, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2209, N'lautaro212', N'Alta', CAST(N'2026-05-19T19:20:57.780' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2210, N'lautaro212', N'Baja', CAST(N'2026-05-19T19:32:01.560' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2211, N'lautaro212', N'Baja', CAST(N'2026-05-19T19:33:32.337' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2212, N'lautaro212', N'Baja', CAST(N'2026-05-19T19:35:16.970' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2213, N'jeremias544', N'Baja', CAST(N'2026-05-19T20:15:34.180' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2214, N'jeremias544', N'Media', CAST(N'2026-05-19T20:16:18.050' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2215, N'jeremias544', N'Media', CAST(N'2026-05-19T20:16:19.740' AS DateTime), 4, 5, N'Intento 2/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2216, N'lautaro212', N'Baja', CAST(N'2026-05-19T20:20:12.853' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2217, N'lautaro212', N'Alta', CAST(N'2026-05-19T20:20:20.893' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2218, N'jeremias544', N'Media', CAST(N'2026-05-19T21:17:25.847' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2220, N'jeremias544', N'Media', CAST(N'2026-05-19T21:20:45.580' AS DateTime), 4, 5, N'Intento 2/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2221, N'jeremias544', N'Alta', CAST(N'2026-05-19T21:20:46.780' AS DateTime), 4, 6, N'3 intentos fallidos consecutivos dentro de 60 min')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2222, N'lautaro212', N'Baja', CAST(N'2026-05-19T21:20:56.187' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2223, N'lautaro212', N'Media', CAST(N'2026-05-19T21:21:03.730' AS DateTime), 5, 8, N'Usuario jeremias544 desbloqueado y contraseña reseteada')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2224, N'lautaro212', N'Alta', CAST(N'2026-05-19T21:21:07.380' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2225, N'jeremias544', N'Media', CAST(N'2026-05-19T21:21:17.697' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2226, N'jeremias544', N'Media', CAST(N'2026-05-19T21:21:19.390' AS DateTime), 4, 5, N'Intento 2/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2227, N'lautaro212', N'Media', CAST(N'2026-05-19T21:21:30.460' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2228, N'lautaro212', N'Media', CAST(N'2026-05-19T21:21:32.527' AS DateTime), 4, 5, N'Intento 2/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2229, N'jeremias544', N'Baja', CAST(N'2026-05-19T21:22:59.233' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2230, N'jeremias544', N'Baja', CAST(N'2026-05-19T21:23:09.973' AS DateTime), 4, 14, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2231, N'jeremias544', N'Alta', CAST(N'2026-05-19T21:23:10.923' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2232, N'jeremias544', N'Baja', CAST(N'2026-05-19T21:31:16.523' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2233, N'jeremias544', N'Baja', CAST(N'2026-05-19T21:31:44.917' AS DateTime), 4, 14, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2234, N'jeremias544', N'Alta', CAST(N'2026-05-19T21:31:49.580' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2235, N'jeremias544', N'Baja', CAST(N'2026-05-19T21:35:17.287' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2236, N'jeremias544', N'Baja', CAST(N'2026-05-19T21:35:30.713' AS DateTime), 4, 14, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2237, N'jeremias544', N'Alta', CAST(N'2026-05-19T21:35:32.227' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2238, N'jeremias544', N'Baja', CAST(N'2026-05-19T21:46:44.343' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2239, N'jeremias544', N'Baja', CAST(N'2026-05-19T21:47:48.900' AS DateTime), 5, 13, N'Login: julian540')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2240, N'jeremias544', N'Baja', CAST(N'2026-05-19T21:48:17.967' AS DateTime), 4, 14, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2241, N'jeremias544', N'Alta', CAST(N'2026-05-19T21:48:26.323' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2242, N'julian540', N'Baja', CAST(N'2026-05-19T21:48:36.843' AS DateTime), 4, 7, NULL)
GO
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2243, N'julian540', N'Baja', CAST(N'2026-05-19T21:49:19.823' AS DateTime), 4, 14, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2244, N'julian540', N'Media', CAST(N'2026-05-19T21:50:08.870' AS DateTime), 5, 10, N'DNI: 12121211')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2245, N'julian540', N'Media', CAST(N'2026-05-19T21:50:24.890' AS DateTime), 5, 9, N'DNI: 11111116')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2246, N'julian540', N'Baja', CAST(N'2026-05-19T21:51:11.947' AS DateTime), 5, 13, N'Login: thiago444')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2247, N'julian540', N'Alta', CAST(N'2026-05-19T21:52:56.297' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2248, N'thiago444', N'Baja', CAST(N'2026-05-19T21:53:09.317' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2249, N'thiago444', N'Baja', CAST(N'2026-05-19T21:53:22.447' AS DateTime), 4, 14, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2250, N'thiago444', N'Baja', CAST(N'2026-05-19T21:53:44.650' AS DateTime), 4, 14, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2251, N'thiago444', N'Alta', CAST(N'2026-05-19T21:53:47.683' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2252, N'jeremias544', N'Media', CAST(N'2026-05-19T21:55:34.427' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2253, N'jeremias544', N'Baja', CAST(N'2026-05-19T21:55:38.783' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2254, N'jeremias544', N'Baja', CAST(N'2026-05-19T21:56:03.587' AS DateTime), 5, 13, N'Login: lautaro555')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2255, N'jeremias544', N'Alta', CAST(N'2026-05-19T21:56:09.177' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2257, N'lautaro555', N'Baja', CAST(N'2026-05-19T21:56:23.857' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2258, N'lautaro555', N'Baja', CAST(N'2026-05-19T21:57:21.733' AS DateTime), 4, 14, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2259, N'lautaro555', N'Alta', CAST(N'2026-05-19T21:57:27.587' AS DateTime), 4, 15, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2260, N'jeremias544', N'Baja', CAST(N'2026-05-19T22:00:02.490' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2261, N'jeremias544', N'Baja', CAST(N'2026-05-19T22:28:08.520' AS DateTime), 4, 7, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2262, N'lautaro212', N'Media', CAST(N'2026-05-19T22:32:18.287' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2263, N'lautaro212', N'Baja', CAST(N'2026-05-19T22:32:25.997' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2264, N'jeremias544', N'Baja', CAST(N'2026-05-19T22:37:13.793' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2265, N'jeremias544', N'Baja', CAST(N'2026-05-20T04:55:00.350' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2266, N'jeremias544', N'Baja', CAST(N'2026-05-20T04:56:34.097' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2267, N'jeremias544', N'Baja', CAST(N'2026-05-20T04:58:12.317' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2268, N'jeremias544', N'Baja', CAST(N'2026-05-20T04:59:43.283' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2269, N'jeremias544', N'Baja', CAST(N'2026-05-20T05:50:46.167' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2270, N'jeremias544', N'Media', CAST(N'2026-05-20T05:50:56.090' AS DateTime), 4, 1, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2271, N'jeremias544', N'Media', CAST(N'2026-05-20T05:50:59.593' AS DateTime), 4, 1, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2273, N'jeremias544', N'Baja', CAST(N'2026-05-25T18:04:21.550' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2274, N'jeremias544', N'Alta', CAST(N'2026-05-25T18:05:22.893' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2275, N'jeremias544', N'Media', CAST(N'2026-05-31T20:54:41.383' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2276, N'jeremias544', N'Baja', CAST(N'2026-05-31T20:54:46.220' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2277, N'jeremias544', N'Alta', CAST(N'2026-05-31T20:55:35.703' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2278, N'jeremias544', N'Baja', CAST(N'2026-05-31T20:55:43.233' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2279, N'jeremias544', N'Alta', CAST(N'2026-05-31T20:55:48.530' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2280, N'jeremias544', N'Baja', CAST(N'2026-05-31T20:55:56.763' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2281, N'jeremias544', N'Baja', CAST(N'2026-05-31T21:01:51.237' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2282, N'jeremias544', N'Alta', CAST(N'2026-05-31T21:02:56.940' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2283, N'jeremias544', N'Baja', CAST(N'2026-05-31T21:03:05.040' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2284, N'jeremias544', N'Alta', CAST(N'2026-05-31T21:03:10.160' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2285, N'jeremias544', N'Baja', CAST(N'2026-06-01T11:24:25.567' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2286, N'jeremias544', N'Alta', CAST(N'2026-06-01T11:24:47.840' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2287, N'jeremias544', N'Baja', CAST(N'2026-06-01T11:24:55.717' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2288, N'jeremias544', N'Alta', CAST(N'2026-06-01T11:25:02.090' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2289, N'jeremias544', N'Baja', CAST(N'2026-06-01T11:25:12.417' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2290, N'jeremias544', N'Baja', CAST(N'2026-06-01T20:07:41.357' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2291, N'jeremias544', N'Baja', CAST(N'2026-06-01T20:44:15.560' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2292, N'jeremias544', N'Baja', CAST(N'2026-06-01T20:54:01.757' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2293, N'jeremias544', N'Baja', CAST(N'2026-06-01T20:58:05.250' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2294, N'jeremias544', N'Baja', CAST(N'2026-06-01T21:00:15.143' AS DateTime), 5, 13, N'Login: facundo888')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2295, N'jeremias544', N'Alta', CAST(N'2026-06-01T21:00:20.203' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2296, N'facundo888', N'Baja', CAST(N'2026-06-01T21:00:29.730' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2297, N'facundo888', N'Baja', CAST(N'2026-06-01T21:00:43.593' AS DateTime), 4, 14, N'Combio contrasenia')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2298, N'facundo888', N'Alta', CAST(N'2026-06-01T21:01:03.960' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2299, N'jeremias544', N'Baja', CAST(N'2026-06-01T21:01:11.403' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2300, N'facundo888', N'Baja', CAST(N'2026-06-01T21:09:28.533' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2301, N'facundo888', N'Alta', CAST(N'2026-06-01T21:09:43.223' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2302, N'jeremias544', N'Baja', CAST(N'2026-06-01T21:09:49.700' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2303, N'jeremias544', N'Baja', CAST(N'2026-06-01T21:10:04.497' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2304, N'jeremias544', N'Baja', CAST(N'2026-06-01T21:10:38.097' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2305, N'jeremias544', N'Baja', CAST(N'2026-06-01T21:12:26.727' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2306, N'jeremias544', N'Baja', CAST(N'2026-06-01T21:15:29.147' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2307, N'facundo888', N'Baja', CAST(N'2026-06-01T21:15:43.510' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2308, N'jeremias544', N'Baja', CAST(N'2026-06-01T21:15:54.637' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2309, N'jeremias544', N'Baja', CAST(N'2026-06-01T21:20:00.257' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2310, N'facundo888', N'Baja', CAST(N'2026-06-01T21:20:13.003' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2311, N'facundo888', N'Baja', CAST(N'2026-06-01T21:21:46.870' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2312, N'facundo888', N'Baja', CAST(N'2026-06-01T21:23:47.743' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2313, N'facundo888', N'Baja', CAST(N'2026-06-01T21:28:27.930' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2314, N'jeremias544', N'Baja', CAST(N'2026-06-01T21:28:41.543' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2315, N'facundo888', N'Baja', CAST(N'2026-06-01T21:30:47.677' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2316, N'facundo888', N'Baja', CAST(N'2026-06-01T21:32:02.140' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2317, N'facundo888', N'Baja', CAST(N'2026-06-01T21:32:40.153' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2318, N'facundo888', N'Baja', CAST(N'2026-06-01T21:35:16.683' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2319, N'facundo888', N'Alta', CAST(N'2026-06-01T21:35:26.243' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2320, N'jeremias544', N'Baja', CAST(N'2026-06-01T21:35:36.987' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2321, N'jeremias544', N'Baja', CAST(N'2026-06-01T21:37:23.113' AS DateTime), 5, 13, N'Login: nahuel222')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2322, N'jeremias544', N'Alta', CAST(N'2026-06-01T21:37:37.803' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2323, N'nahuel222', N'Baja', CAST(N'2026-06-01T21:37:45.257' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2324, N'nahuel222', N'Baja', CAST(N'2026-06-01T21:38:04.367' AS DateTime), 4, 14, N'Combio contrasenia')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2325, N'nahuel222', N'Baja', CAST(N'2026-06-01T21:46:07.967' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2326, N'jeremias544', N'Baja', CAST(N'2026-06-01T21:46:26.540' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2327, N'jeremias544', N'Baja', CAST(N'2026-06-01T21:47:24.567' AS DateTime), 5, 13, N'Login: lautaro000')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2328, N'jeremias544', N'Alta', CAST(N'2026-06-01T21:47:30.647' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2329, N'lautaro000', N'Baja', CAST(N'2026-06-01T21:47:37.763' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2330, N'lautaro000', N'Baja', CAST(N'2026-06-01T21:47:54.323' AS DateTime), 4, 14, N'Combio contrasenia')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2331, N'lautaro000', N'Baja', CAST(N'2026-06-01T21:51:08.983' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2332, N'lautaro000', N'Baja', CAST(N'2026-06-01T21:54:22.203' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2333, N'jeremias544', N'Baja', CAST(N'2026-06-01T21:54:36.110' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2334, N'jeremias544', N'Baja', CAST(N'2026-06-01T21:55:56.493' AS DateTime), 5, 13, N'Login: matias666')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2335, N'jeremias544', N'Alta', CAST(N'2026-06-01T21:56:11.400' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2336, N'matias666', N'Baja', CAST(N'2026-06-01T21:56:17.627' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2337, N'matias666', N'Baja', CAST(N'2026-06-01T21:56:29.547' AS DateTime), 4, 14, N'Combio contrasenia')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2338, N'matias666', N'Baja', CAST(N'2026-06-01T21:59:14.857' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2339, N'matias666', N'Baja', CAST(N'2026-06-01T22:05:15.110' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2340, N'jeremias544', N'Baja', CAST(N'2026-06-01T23:08:32.277' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2341, N'jeremias544', N'Media', CAST(N'2026-06-01T23:09:00.797' AS DateTime), 5, 12, N'DNI 66666666 -> rol PEPE')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2342, N'jeremias544', N'Alta', CAST(N'2026-06-01T23:09:07.830' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2343, N'matias666', N'Baja', CAST(N'2026-06-01T23:09:14.730' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2344, N'jeremias544', N'Baja', CAST(N'2026-06-01T23:10:29.100' AS DateTime), 4, 7, N'Login correcto')
GO
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2345, N'jeremias544', N'Media', CAST(N'2026-06-01T23:12:25.400' AS DateTime), 5, 12, N'DNI 66666666 -> rol Lauty')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2346, N'jeremias544', N'Alta', CAST(N'2026-06-01T23:12:28.127' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2347, N'matias666', N'Baja', CAST(N'2026-06-01T23:12:36.550' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2348, N'jeremias544', N'Baja', CAST(N'2026-06-01T23:13:16.143' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2349, N'jeremias544', N'Media', CAST(N'2026-06-01T23:13:43.933' AS DateTime), 5, 12, N'DNI 66666666 -> rol PEPE')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2350, N'jeremias544', N'Media', CAST(N'2026-06-01T23:14:14.757' AS DateTime), 5, 12, N'DNI 66666666 -> rol Lauty')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2351, N'jeremias544', N'Alta', CAST(N'2026-06-01T23:14:17.553' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2352, N'matias666', N'Baja', CAST(N'2026-06-01T23:14:31.053' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2353, N'matias666', N'Alta', CAST(N'2026-06-01T23:14:51.037' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2354, N'jeremias544', N'Baja', CAST(N'2026-06-02T13:13:41.127' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2355, N'jeremias544', N'Alta', CAST(N'2026-06-02T13:13:52.837' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2356, N'jeremias544', N'Baja', CAST(N'2026-06-02T13:14:02.417' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2357, N'jeremias544', N'Alta', CAST(N'2026-06-02T13:14:09.197' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2358, N'jeremias544', N'Baja', CAST(N'2026-06-02T13:27:09.223' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2359, N'jeremias544', N'Baja', CAST(N'2026-06-02T13:32:51.503' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2360, N'jeremias544', N'Baja', CAST(N'2026-06-02T13:37:28.333' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2361, N'jeremias544', N'Alta', CAST(N'2026-06-02T13:37:50.897' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2362, N'jeremias544', N'Baja', CAST(N'2026-06-02T18:30:18.110' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2363, N'jeremias544', N'Media', CAST(N'2026-06-02T18:34:47.253' AS DateTime), 5, 12, N'DNI 12345679 -> rol UsuarioBitacora')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2364, N'jeremias544', N'Alta', CAST(N'2026-06-02T18:35:00.957' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2365, N'alejo679', N'Media', CAST(N'2026-06-02T18:35:07.983' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2366, N'alejo679', N'Media', CAST(N'2026-06-02T18:35:12.040' AS DateTime), 4, 5, N'Intento 2/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2367, N'alejo679', N'Alta', CAST(N'2026-06-02T18:35:19.973' AS DateTime), 4, 6, N'3 intentos fallidos consecutivos dentro de 60 min')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2368, N'jeremias544', N'Baja', CAST(N'2026-06-02T18:35:28.660' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2369, N'jeremias544', N'Media', CAST(N'2026-06-02T18:35:37.070' AS DateTime), 5, 12, N'DNI 66666666 -> rol UsuarioBitacora')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2370, N'jeremias544', N'Alta', CAST(N'2026-06-02T18:35:39.867' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2371, N'matias666', N'Baja', CAST(N'2026-06-02T18:35:46.813' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2372, N'matias666', N'Alta', CAST(N'2026-06-02T18:36:04.323' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2373, N'jeremias544', N'Baja', CAST(N'2026-06-02T19:37:12.920' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2374, N'jeremias544', N'Baja', CAST(N'2026-06-02T19:44:30.347' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2375, N'jeremias544', N'Baja', CAST(N'2026-06-02T19:51:51.407' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2376, N'jeremias544', N'Baja', CAST(N'2026-06-02T19:52:13.433' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2377, N'jeremias544', N'Baja', CAST(N'2026-06-02T20:04:40.400' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2378, N'jeremias544', N'Baja', CAST(N'2026-06-02T20:08:53.607' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2379, N'jeremias544', N'Baja', CAST(N'2026-06-08T20:12:06.580' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2380, N'jeremias544', N'Baja', CAST(N'2026-06-08T20:12:24.337' AS DateTime), 4, 16, N'en -> es')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2381, N'jeremias544', N'Alta', CAST(N'2026-06-08T20:14:45.900' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2382, N'jeremias544', N'Baja', CAST(N'2026-06-15T12:51:00.657' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2383, N'jeremias544', N'Baja', CAST(N'2026-06-15T12:51:03.213' AS DateTime), 4, 16, N'es -> en')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2384, N'jeremias544', N'Baja', CAST(N'2026-06-15T12:51:05.087' AS DateTime), 4, 16, N'en -> es')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2385, N'jeremias544', N'Baja', CAST(N'2026-06-15T12:51:08.047' AS DateTime), 4, 16, N'es -> en')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2386, N'jeremias544', N'Baja', CAST(N'2026-06-15T12:51:10.403' AS DateTime), 4, 16, N'en -> es')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2387, N'jeremias544', N'Baja', CAST(N'2026-06-15T13:51:36.257' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2388, N'jeremias544', N'Baja', CAST(N'2026-06-15T14:13:15.330' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2389, N'jeremias544', N'Baja', CAST(N'2026-06-15T14:13:18.237' AS DateTime), 4, 16, N'es -> en')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2390, N'jeremias544', N'Baja', CAST(N'2026-06-15T14:16:32.290' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2391, N'jeremias544', N'Baja', CAST(N'2026-06-15T14:17:14.810' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2392, N'jeremias544', N'Baja', CAST(N'2026-06-15T14:19:30.767' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2393, N'jeremias544', N'Baja', CAST(N'2026-06-15T14:24:38.527' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2394, N'jeremias544', N'Baja', CAST(N'2026-06-15T14:25:18.183' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2395, N'jeremias544', N'Baja', CAST(N'2026-06-15T14:26:18.853' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2396, N'jeremias544', N'Baja', CAST(N'2026-06-15T14:34:11.297' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2397, N'jeremias544', N'Baja', CAST(N'2026-06-15T14:35:20.437' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2398, N'jeremias544', N'Baja', CAST(N'2026-06-15T14:37:26.060' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2399, N'jeremias544', N'Baja', CAST(N'2026-06-16T10:48:40.953' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2400, N'jeremias544', N'Alta', CAST(N'2026-06-16T10:48:46.683' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2401, N'jeremias544', N'Baja', CAST(N'2026-06-16T10:48:52.743' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2402, N'jeremias544', N'Alta', CAST(N'2026-06-16T10:48:57.623' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2403, N'jeremias544', N'Baja', CAST(N'2026-06-16T10:53:15.847' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2404, N'jeremias544', N'Baja', CAST(N'2026-06-16T10:54:30.857' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2405, N'jeremias544', N'Baja', CAST(N'2026-06-16T10:58:18.107' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2406, N'jeremias544', N'Alta', CAST(N'2026-06-16T10:58:23.310' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2407, N'jeremias544', N'Baja', CAST(N'2026-06-16T10:58:25.353' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2408, N'jeremias544', N'Baja', CAST(N'2026-06-16T11:01:45.763' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2409, N'jeremias544', N'Baja', CAST(N'2026-06-16T11:02:14.143' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2410, N'jeremias544', N'Alta', CAST(N'2026-06-16T11:02:14.160' AS DateTime), 5, 17, N'Usuario')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2411, N'jeremias544', N'Alta', CAST(N'2026-06-16T11:02:57.547' AS DateTime), 5, 18, N'Admin aceptó los cambios externos como válidos.')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2412, N'jeremias544', N'Baja', CAST(N'2026-06-16T11:03:20.863' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2413, N'jeremias544', N'Baja', CAST(N'2026-06-16T11:03:22.633' AS DateTime), 4, 16, N'en -> es')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2414, N'jeremias544', N'Baja', CAST(N'2026-06-16T11:03:51.143' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2415, N'jeremias544', N'Alta', CAST(N'2026-06-16T11:03:51.157' AS DateTime), 5, 17, N'Usuario')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2416, N'jeremias544', N'Alta', CAST(N'2026-06-16T11:04:33.427' AS DateTime), 5, 18, N'Admin aceptó los cambios externos como válidos.')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2417, N'jeremias544', N'Baja', CAST(N'2026-06-16T11:16:43.053' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2418, N'jeremias544', N'Alta', CAST(N'2026-06-16T11:17:54.103' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2419, N'jeremias544', N'Baja', CAST(N'2026-06-16T11:18:05.033' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2420, N'jeremias544', N'Alta', CAST(N'2026-06-16T11:21:17.093' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2421, N'jeremias544', N'Baja', CAST(N'2026-06-16T11:21:27.353' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2422, N'jeremias544', N'Alta', CAST(N'2026-06-16T19:04:39.897' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2423, N'jeremias544', N'Baja', CAST(N'2026-06-16T19:12:33.577' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2424, N'jeremias544', N'Alta', CAST(N'2026-06-16T19:15:11.360' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2425, N'jeremias544', N'Baja', CAST(N'2026-06-20T18:13:58.077' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2426, N'jeremias544', N'Baja', CAST(N'2026-06-20T21:26:30.340' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2427, N'jeremias544', N'Baja', CAST(N'2026-06-20T21:29:51.793' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2428, N'jeremias544', N'Baja', CAST(N'2026-06-20T21:36:29.340' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2429, N'jeremias544', N'Baja', CAST(N'2026-06-20T21:58:16.850' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2430, N'jeremias544', N'Baja', CAST(N'2026-06-20T22:09:37.470' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2431, N'jeremias544', N'Baja', CAST(N'2026-06-20T22:15:56.107' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2432, N'jeremias544', N'Baja', CAST(N'2026-06-20T22:22:04.897' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2433, N'jeremias544', N'Baja', CAST(N'2026-06-20T22:34:23.270' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2434, N'jeremias544', N'Baja', CAST(N'2026-06-20T22:51:31.263' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2435, N'jeremias544', N'Baja', CAST(N'2026-06-20T23:32:20.897' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2436, N'jeremias544', N'Baja', CAST(N'2026-06-20T23:41:39.667' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2437, N'jeremias544', N'Baja', CAST(N'2026-06-21T17:35:52.980' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2438, N'jeremias544', N'Baja', CAST(N'2026-06-22T20:25:16.170' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2439, N'jeremias544', N'Alta', CAST(N'2026-06-22T20:25:16.197' AS DateTime), 5, 17, N'RolPatente')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2440, N'jeremias544', N'Alta', CAST(N'2026-06-22T20:25:23.807' AS DateTime), 5, 18, N'Admin aceptó los cambios externos como válidos.')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2442, N'jeremias544', N'Media', CAST(N'2026-06-22T20:26:04.813' AS DateTime), 5, 12, N'DNI 55555555 -> rol AdminGestionUsuario')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2443, N'jeremias544', N'Media', CAST(N'2026-06-22T20:26:18.097' AS DateTime), 5, 12, N'DNI 23642736 -> rol Lauty')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2444, N'jeremias544', N'Media', CAST(N'2026-06-22T20:26:26.153' AS DateTime), 5, 12, N'DNI 12345678 -> rol maxi1')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2445, N'jeremias544', N'Media', CAST(N'2026-06-22T20:26:32.213' AS DateTime), 5, 12, N'DNI 12345677 -> rol Lauty')
GO
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2446, N'jeremias544', N'Baja', CAST(N'2026-06-22T20:27:29.137' AS DateTime), 4, 16, N'es -> en')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2447, N'jeremias544', N'Baja', CAST(N'2026-06-22T20:28:43.733' AS DateTime), 4, 16, N'en -> es')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2448, N'jeremias544', N'Media', CAST(N'2026-06-22T20:28:58.417' AS DateTime), 5, 12, N'DNI 44444444 -> rol AdminGestionUsuario')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2449, N'jeremias544', N'Media', CAST(N'2026-06-22T20:30:11.117' AS DateTime), 5, 12, N'DNI 12121211 -> rol AdminGestionUsuario')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2450, N'jeremias544', N'Media', CAST(N'2026-06-22T20:30:15.167' AS DateTime), 5, 9, N'DNI: 12121211')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2451, N'jeremias544', N'Alta', CAST(N'2026-06-22T20:30:28.250' AS DateTime), 5, 23, N'IdRol: 2')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2452, N'jeremias544', N'Baja', CAST(N'2026-06-22T20:33:11.377' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2453, N'jeremias544', N'Media', CAST(N'2026-06-22T20:33:18.713' AS DateTime), 5, 22, N'Nombre: ''Usuario'', Patentes: 0, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2454, N'jeremias544', N'Media', CAST(N'2026-06-22T20:33:26.117' AS DateTime), 5, 12, N'IdRol: 17, Nombre: ''Usuario'', Patentes: 1, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2455, N'jeremias544', N'Media', CAST(N'2026-06-22T20:33:46.530' AS DateTime), 5, 22, N'Nombre: ''Usuario1'', Patentes: 1, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2456, N'jeremias544', N'Media', CAST(N'2026-06-22T20:33:52.823' AS DateTime), 5, 12, N'IdRol: 18, Nombre: ''Usuario1'', Patentes: 0, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2457, N'jeremias544', N'Media', CAST(N'2026-06-22T20:34:07.763' AS DateTime), 5, 12, N'IdRol: 17, Nombre: ''Usuario'', Patentes: 0, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2458, N'jeremias544', N'Baja', CAST(N'2026-06-22T20:39:44.940' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2459, N'jeremias544', N'Alta', CAST(N'2026-06-22T21:28:42.230' AS DateTime), 5, 23, N'IdRol: 17')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2460, N'jeremias544', N'Alta', CAST(N'2026-06-22T21:28:48.743' AS DateTime), 5, 23, N'IdRol: 18')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2461, N'jeremias544', N'Media', CAST(N'2026-06-22T21:28:54.577' AS DateTime), 5, 22, N'Nombre: ''Usuario'', Patentes: 0, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2462, N'jeremias544', N'Media', CAST(N'2026-06-22T21:29:11.677' AS DateTime), 5, 22, N'Nombre: ''Usuario1'', Patentes: 0, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2463, N'jeremias544', N'Media', CAST(N'2026-06-22T21:29:44.627' AS DateTime), 5, 12, N'IdRol: 19, Nombre: ''Usuario'', Patentes: 1, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2464, N'jeremias544', N'Baja', CAST(N'2026-06-22T21:30:30.113' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2465, N'jeremias544', N'Media', CAST(N'2026-06-22T21:31:03.343' AS DateTime), 5, 20, N'IdFamilia: 3, Nombre: ''Gestion Usuario'', Patentes: 6, Subfamilias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2466, N'jeremias544', N'Media', CAST(N'2026-06-22T21:31:35.057' AS DateTime), 5, 20, N'IdFamilia: 3, Nombre: ''Gestion Usuario'', Patentes: 5, Subfamilias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2467, N'jeremias544', N'Baja', CAST(N'2026-06-23T14:10:18.447' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2469, N'jeremias544', N'Baja', CAST(N'2026-06-23T20:06:11.153' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2471, N'jeremias544', N'Media', CAST(N'2026-06-23T20:07:05.653' AS DateTime), 5, 12, N'IdRol: 19, Nombre: ''Usuario'', Patentes: 0, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2472, N'jeremias544', N'Baja', CAST(N'2026-06-23T20:07:41.327' AS DateTime), 5, 13, N'Login: olivia333')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2473, N'jeremias544', N'Baja', CAST(N'2026-06-23T20:07:48.627' AS DateTime), 4, 16, N'es -> en')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2474, N'jeremias544', N'Alta', CAST(N'2026-06-23T20:07:58.007' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2475, N'olivia333', N'Baja', CAST(N'2026-06-23T20:08:20.023' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2476, N'olivia333', N'Baja', CAST(N'2026-06-23T20:08:44.587' AS DateTime), 4, 14, N'Cambio de contraseña')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2477, N'olivia333', N'Baja', CAST(N'2026-06-23T20:09:21.177' AS DateTime), 4, 16, N'es -> en')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2478, N'olivia333', N'Alta', CAST(N'2026-06-23T20:09:24.750' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2479, N'jeremias544', N'Baja', CAST(N'2026-06-23T20:17:48.107' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2480, N'jeremias544', N'Alta', CAST(N'2026-06-23T20:17:48.147' AS DateTime), 5, 17, N'Usuario')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2481, N'jeremias544', N'Alta', CAST(N'2026-06-23T20:18:29.340' AS DateTime), 5, 18, N'Admin aceptó los cambios externos como válidos.')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2482, N'jeremias544', N'Baja', CAST(N'2026-06-23T20:18:32.727' AS DateTime), 4, 16, N'en -> es')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2483, N'jeremias544', N'Alta', CAST(N'2026-06-23T20:18:36.007' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2484, N'jeremias544', N'Baja', CAST(N'2026-06-23T20:18:42.423' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2485, N'jeremias544', N'Baja', CAST(N'2026-06-23T20:18:43.833' AS DateTime), 4, 16, N'es -> en')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2486, N'jeremias544', N'Alta', CAST(N'2026-06-23T20:18:46.127' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2487, N'jeremias544', N'Baja', CAST(N'2026-06-23T20:18:56.837' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2488, N'jeremias544', N'Baja', CAST(N'2026-06-23T20:26:28.390' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2489, N'jeremias544', N'Alta', CAST(N'2026-06-23T20:26:39.710' AS DateTime), 5, 23, N'IdRol: 20')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2490, N'jeremias544', N'Media', CAST(N'2026-06-23T20:27:10.160' AS DateTime), 5, 12, N'IdRol: 19, Nombre: ''Usuario'', Patentes: 1, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2491, N'jeremias544', N'Alta', CAST(N'2026-06-23T20:27:15.740' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2492, N'jeremias544', N'Baja', CAST(N'2026-06-23T20:27:21.730' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2493, N'jeremias544', N'Alta', CAST(N'2026-06-23T20:27:30.110' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2494, N'olivia333', N'Baja', CAST(N'2026-06-23T20:27:36.610' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2495, N'jeremias544', N'Baja', CAST(N'2026-06-23T21:34:02.560' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2496, N'jeremias544', N'Alta', CAST(N'2026-06-23T21:34:02.590' AS DateTime), 5, 17, N'Patente, RolPatente')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2497, N'jeremias544', N'Alta', CAST(N'2026-06-23T21:34:05.690' AS DateTime), 5, 18, N'Admin aceptó los cambios externos como válidos.')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2498, N'jeremias544', N'Baja', CAST(N'2026-06-23T21:34:45.227' AS DateTime), 4, 16, N'en -> es')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2499, N'jeremias544', N'Baja', CAST(N'2026-06-23T21:41:14.960' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2500, N'jeremias544', N'Media', CAST(N'2026-06-23T21:41:23.280' AS DateTime), 5, 12, N'IdRol: 1, Nombre: ''Admin'', Patentes: 9, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2501, N'jeremias544', N'Media', CAST(N'2026-06-23T21:41:32.937' AS DateTime), 5, 12, N'IdRol: 1, Nombre: ''Admin'', Patentes: 10, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2502, N'jeremias544', N'Media', CAST(N'2026-06-23T21:41:36.720' AS DateTime), 5, 12, N'IdRol: 1, Nombre: ''Admin'', Patentes: 11, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2503, N'jeremias544', N'Baja', CAST(N'2026-06-23T21:41:52.147' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2504, N'jeremias544', N'Baja', CAST(N'2026-06-23T21:44:50.647' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2505, N'jeremias544', N'Media', CAST(N'2026-06-23T21:45:22.610' AS DateTime), 5, 22, N'Nombre: ''Problema'', Patentes: 3, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2506, N'jeremias544', N'Media', CAST(N'2026-06-23T21:45:49.053' AS DateTime), 5, 12, N'DNI 33333333 -> rol Problema')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2507, N'olivia333', N'Baja', CAST(N'2026-06-23T21:46:02.720' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2508, N'jeremias544', N'Baja', CAST(N'2026-06-23T21:49:05.147' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2509, N'jeremias544', N'Baja', CAST(N'2026-06-23T21:51:40.903' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2510, N'jeremias544', N'Baja', CAST(N'2026-06-23T21:58:07.390' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2511, N'jeremias544', N'Media', CAST(N'2026-06-23T21:58:33.190' AS DateTime), 5, 12, N'IdRol: 19, Nombre: ''Usuario'', Patentes: 1, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2512, N'jeremias544', N'Media', CAST(N'2026-06-23T21:59:16.187' AS DateTime), 5, 12, N'IdRol: 19, Nombre: ''Usuario'', Patentes: 1, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2513, N'jeremias544', N'Alta', CAST(N'2026-06-23T21:59:31.783' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2514, N'olivia333', N'Baja', CAST(N'2026-06-23T21:59:38.047' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2515, N'olivia333', N'Alta', CAST(N'2026-06-23T21:59:49.487' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2516, N'olivia333', N'Baja', CAST(N'2026-06-23T22:06:26.447' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2517, N'olivia333', N'Alta', CAST(N'2026-06-23T22:07:06.173' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2518, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:07:13.850' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2519, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:08:47.097' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2520, N'jeremias544', N'Media', CAST(N'2026-06-23T22:09:26.827' AS DateTime), 5, 12, N'DNI 33333333 -> rol Usuario')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2521, N'jeremias544', N'Alta', CAST(N'2026-06-23T22:09:30.227' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2522, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:09:35.717' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2523, N'jeremias544', N'Alta', CAST(N'2026-06-23T22:09:39.533' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2524, N'olivia333', N'Baja', CAST(N'2026-06-23T22:09:44.610' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2525, N'olivia333', N'Alta', CAST(N'2026-06-23T22:09:48.680' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2526, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:09:57.063' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2527, N'jeremias544', N'Media', CAST(N'2026-06-23T22:10:16.483' AS DateTime), 5, 12, N'IdRol: 19, Nombre: ''Usuario'', Patentes: 2, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2528, N'jeremias544', N'Alta', CAST(N'2026-06-23T22:10:33.883' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2529, N'olivia333', N'Baja', CAST(N'2026-06-23T22:10:50.400' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2530, N'olivia333', N'Media', CAST(N'2026-06-23T22:11:02.977' AS DateTime), 4, 1, NULL)
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2531, N'olivia333', N'Baja', CAST(N'2026-06-23T22:11:06.570' AS DateTime), 4, 16, N'en -> es')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2532, N'olivia333', N'Alta', CAST(N'2026-06-23T22:11:11.890' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2533, N'olivia333', N'Baja', CAST(N'2026-06-23T22:11:22.800' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2534, N'olivia333', N'Alta', CAST(N'2026-06-23T22:11:38.897' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2535, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:17:34.767' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2536, N'jeremias544', N'Alta', CAST(N'2026-06-23T22:17:34.780' AS DateTime), 5, 17, N'Patente, FamiliaPatente, RolPatente')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2537, N'jeremias544', N'Alta', CAST(N'2026-06-23T22:17:37.660' AS DateTime), 5, 18, N'Admin aceptó los cambios externos como válidos.')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2538, N'jeremias544', N'Media', CAST(N'2026-06-23T22:18:04.310' AS DateTime), 5, 12, N'IdRol: 19, Nombre: ''Usuario'', Patentes: 4, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2539, N'jeremias544', N'Alta', CAST(N'2026-06-23T22:18:08.487' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2540, N'olivia333', N'Baja', CAST(N'2026-06-23T22:18:15.123' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2541, N'olivia333', N'Alta', CAST(N'2026-06-23T22:18:31.080' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2542, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:18:36.950' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2543, N'jeremias544', N'Media', CAST(N'2026-06-23T22:18:50.300' AS DateTime), 5, 12, N'IdRol: 19, Nombre: ''Usuario'', Patentes: 4, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2544, N'jeremias544', N'Alta', CAST(N'2026-06-23T22:18:53.823' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2545, N'olivia333', N'Baja', CAST(N'2026-06-23T22:19:02.953' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2546, N'olivia333', N'Alta', CAST(N'2026-06-23T22:19:12.397' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2547, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:20:27.623' AS DateTime), 4, 7, N'Login correcto')
GO
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2548, N'jeremias544', N'Media', CAST(N'2026-06-23T22:23:08.207' AS DateTime), 5, 22, N'Nombre: ''NoCambiarContrasenia'', Patentes: 0, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2549, N'jeremias544', N'Media', CAST(N'2026-06-23T22:24:29.320' AS DateTime), 5, 12, N'DNI 33333333 -> rol NoCambiarContrasenia')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2550, N'jeremias544', N'Alta', CAST(N'2026-06-23T22:24:33.087' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2551, N'olivia333', N'Baja', CAST(N'2026-06-23T22:24:49.510' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2552, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:25:13.893' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2553, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:25:53.290' AS DateTime), 5, 13, N'Login: maximo555')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2554, N'jeremias544', N'Alta', CAST(N'2026-06-23T22:25:57.053' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2555, N'maximo555', N'Baja', CAST(N'2026-06-23T22:26:02.900' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2556, N'maximo555', N'Baja', CAST(N'2026-06-23T22:26:13.500' AS DateTime), 4, 14, N'Cambio de contraseña')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2557, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:27:41.940' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2558, N'jeremias544', N'Alta', CAST(N'2026-06-23T22:27:41.953' AS DateTime), 5, 17, N'Usuario')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2559, N'jeremias544', N'Alta', CAST(N'2026-06-23T22:27:59.327' AS DateTime), 5, 18, N'Admin aceptó los cambios externos como válidos.')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2560, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:32:47.773' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2561, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:35:46.250' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2562, N'jeremias544', N'Media', CAST(N'2026-06-23T22:37:15.427' AS DateTime), 5, 12, N'IdRol: 1, Nombre: ''Admin'', Patentes: 7, Familias: 1')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2563, N'jeremias544', N'Media', CAST(N'2026-06-23T22:38:38.017' AS DateTime), 5, 20, N'IdFamilia: 17, Nombre: ''pepe'', Patentes: 1, Subfamilias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2564, N'jeremias544', N'Media', CAST(N'2026-06-23T22:39:10.407' AS DateTime), 5, 20, N'IdFamilia: 18, Nombre: ''pepe2'', Patentes: 1, Subfamilias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2565, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:45:48.643' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2566, N'jeremias544', N'Media', CAST(N'2026-06-23T22:46:03.530' AS DateTime), 5, 12, N'IdRol: 1, Nombre: ''Admin'', Patentes: 6, Familias: 1')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2567, N'jeremias544', N'Alta', CAST(N'2026-06-23T22:46:10.177' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2568, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:46:22.537' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2569, N'jeremias544', N'Media', CAST(N'2026-06-23T22:46:34.977' AS DateTime), 5, 12, N'IdRol: 1, Nombre: ''Admin'', Patentes: 7, Familias: 1')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2570, N'jeremias544', N'Alta', CAST(N'2026-06-23T22:46:35.997' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2571, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:48:57.947' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2572, N'jeremias544', N'Media', CAST(N'2026-06-23T22:49:07.017' AS DateTime), 5, 12, N'IdRol: 1, Nombre: ''Admin'', Patentes: 6, Familias: 1')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2573, N'jeremias544', N'Alta', CAST(N'2026-06-23T22:49:08.010' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2574, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:53:00.270' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2575, N'jeremias544', N'Media', CAST(N'2026-06-23T22:53:06.910' AS DateTime), 5, 12, N'IdRol: 1, Nombre: ''Admin'', Patentes: 7, Familias: 1')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2576, N'jeremias544', N'Alta', CAST(N'2026-06-23T22:53:08.497' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2577, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:53:14.130' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2578, N'jeremias544', N'Baja', CAST(N'2026-06-23T22:54:10.550' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2579, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:10:06.317' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2580, N'jeremias544', N'Alta', CAST(N'2026-06-23T23:10:06.323' AS DateTime), 5, 17, N'Patente, RolPatente')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2581, N'jeremias544', N'Alta', CAST(N'2026-06-23T23:10:10.553' AS DateTime), 5, 18, N'Admin aceptó los cambios externos como válidos.')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2583, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:10:23.080' AS DateTime), 4, 16, N'es -> en')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2584, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:13:13.480' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2585, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:14:31.517' AS DateTime), 4, 16, N'en -> es')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2586, N'jeremias544', N'Media', CAST(N'2026-06-23T23:15:04.560' AS DateTime), 5, 22, N'Nombre: ''jere'', Patentes: 1, Familias: 1')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2587, N'jeremias544', N'Media', CAST(N'2026-06-23T23:16:35.087' AS DateTime), 5, 12, N'IdRol: 23, Nombre: ''jere'', Patentes: 0, Familias: 1')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2588, N'jeremias544', N'Media', CAST(N'2026-06-23T23:17:12.577' AS DateTime), 5, 12, N'DNI 33333333 -> rol jere')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2589, N'jeremias544', N'Alta', CAST(N'2026-06-23T23:17:16.843' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2590, N'olivia333', N'Baja', CAST(N'2026-06-23T23:17:23.283' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2591, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:17:57.663' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2592, N'jeremias544', N'Media', CAST(N'2026-06-23T23:18:23.710' AS DateTime), 5, 20, N'IdFamilia: 3, Nombre: ''Gestion Usuario'', Patentes: 4, Subfamilias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2593, N'jeremias544', N'Media', CAST(N'2026-06-23T23:18:46.743' AS DateTime), 5, 12, N'IdRol: 1, Nombre: ''Admin'', Patentes: 10, Familias: 1')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2594, N'jeremias544', N'Alta', CAST(N'2026-06-23T23:18:48.507' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2595, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:18:53.613' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2596, N'jeremias544', N'Media', CAST(N'2026-06-23T23:19:12.377' AS DateTime), 5, 12, N'IdRol: 23, Nombre: ''jere'', Patentes: 1, Familias: 1')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2597, N'jeremias544', N'Alta', CAST(N'2026-06-23T23:19:15.223' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2598, N'olivia333', N'Baja', CAST(N'2026-06-23T23:19:26.287' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2599, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:19:39.067' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2600, N'jeremias544', N'Media', CAST(N'2026-06-23T23:21:25.403' AS DateTime), 5, 20, N'IdFamilia: 17, Nombre: ''pepe'', Patentes: 1, Subfamilias: 1')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2601, N'jeremias544', N'Media', CAST(N'2026-06-23T23:21:31.820' AS DateTime), 5, 20, N'IdFamilia: 17, Nombre: ''pepe'', Patentes: 2, Subfamilias: 1')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2602, N'jeremias544', N'Media', CAST(N'2026-06-23T23:22:21.630' AS DateTime), 5, 20, N'IdFamilia: 17, Nombre: ''pepe'', Patentes: 1, Subfamilias: 1')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2603, N'jeremias544', N'Media', CAST(N'2026-06-23T23:23:06.190' AS DateTime), 5, 12, N'IdRol: 1, Nombre: ''Admin'', Patentes: 8, Familias: 1')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2604, N'jeremias544', N'Alta', CAST(N'2026-06-23T23:23:06.800' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2605, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:23:12.600' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2606, N'jeremias544', N'Media', CAST(N'2026-06-23T23:23:30.183' AS DateTime), 5, 20, N'IdFamilia: 3, Nombre: ''Gestion Usuario'', Patentes: 6, Subfamilias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2607, N'jeremias544', N'Media', CAST(N'2026-06-23T23:24:31.657' AS DateTime), 5, 20, N'IdFamilia: 3, Nombre: ''Gestion Usuario'', Patentes: 4, Subfamilias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2608, N'jeremias544', N'Media', CAST(N'2026-06-23T23:24:40.787' AS DateTime), 5, 20, N'IdFamilia: 18, Nombre: ''pepe2'', Patentes: 1, Subfamilias: 1')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2609, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:26:18.017' AS DateTime), 4, 16, N'es -> en')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2610, N'jeremias544', N'Alta', CAST(N'2026-06-23T23:27:30.840' AS DateTime), 5, 21, N'IdFamilia: 18')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2611, N'jeremias544', N'Media', CAST(N'2026-06-23T23:28:16.387' AS DateTime), 5, 12, N'IdRol: 1, Nombre: ''Admin'', Patentes: 13, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2612, N'jeremias544', N'Alta', CAST(N'2026-06-23T23:28:17.797' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2613, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:28:23.563' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2614, N'jeremias544', N'Media', CAST(N'2026-06-23T23:28:36.133' AS DateTime), 5, 12, N'IdRol: 10, Nombre: ''AdminGestionUsuario'', Patentes: 3, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2615, N'jeremias544', N'Media', CAST(N'2026-06-23T23:28:47.287' AS DateTime), 5, 12, N'IdRol: 23, Nombre: ''jere'', Patentes: 1, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2616, N'jeremias544', N'Alta', CAST(N'2026-06-23T23:29:12.893' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2617, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:34:03.013' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2618, N'jeremias544', N'Media', CAST(N'2026-06-23T23:34:14.727' AS DateTime), 5, 22, N'Nombre: ''dsadasdsad'', Patentes: 1, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2619, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:34:46.467' AS DateTime), 4, 16, N'en -> es')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2620, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:34:49.830' AS DateTime), 4, 16, N'es -> en')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2621, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:48:09.987' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2622, N'jeremias544', N'Media', CAST(N'2026-06-23T23:48:19.913' AS DateTime), 5, 12, N'IdRol: 1, Nombre: ''Admin'', Patentes: 15, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2623, N'jeremias544', N'Alta', CAST(N'2026-06-23T23:48:20.803' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2624, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:48:27.097' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2625, N'jeremias544', N'Media', CAST(N'2026-06-23T23:48:59.450' AS DateTime), 5, 12, N'IdRol: 10, Nombre: ''AdminGestionUsuario'', Patentes: 15, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2626, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:52:57.560' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2627, N'jeremias544', N'Media', CAST(N'2026-06-23T23:53:21.800' AS DateTime), 5, 12, N'IdRol: 10, Nombre: ''AdminGestionUsuario'', Patentes: 12, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2628, N'jeremias544', N'Alta', CAST(N'2026-06-23T23:53:46.747' AS DateTime), 5, 23, N'IdRol: 24')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2629, N'jeremias544', N'Alta', CAST(N'2026-06-23T23:54:01.653' AS DateTime), 5, 23, N'IdRol: 12')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2630, N'jeremias544', N'Alta', CAST(N'2026-06-23T23:54:34.810' AS DateTime), 5, 21, N'IdFamilia: 17')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2631, N'jeremias544', N'Baja', CAST(N'2026-06-23T23:54:47.057' AS DateTime), 4, 16, N'en -> es')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2632, N'jeremias544', N'Baja', CAST(N'2026-06-24T00:13:58.263' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2633, N'jeremias544', N'Baja', CAST(N'2026-06-24T00:15:06.533' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2634, N'jeremias544', N'Alta', CAST(N'2026-06-24T00:15:06.553' AS DateTime), 5, 17, N'Patente, RolPatente')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2635, N'jeremias544', N'Alta', CAST(N'2026-06-24T00:15:10.523' AS DateTime), 5, 18, N'Admin aceptó los cambios externos como válidos.')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2636, N'jeremias544', N'Media', CAST(N'2026-06-24T00:15:38.330' AS DateTime), 5, 12, N'IdRol: 1, Nombre: ''Admin'', Patentes: 15, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2637, N'jeremias544', N'Alta', CAST(N'2026-06-24T00:15:39.630' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2638, N'jeremias544', N'Baja', CAST(N'2026-06-24T00:15:46.023' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2639, N'jeremias544', N'Media', CAST(N'2026-06-24T00:15:58.770' AS DateTime), 5, 12, N'IdRol: 1, Nombre: ''Admin'', Patentes: 14, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2640, N'jeremias544', N'Alta', CAST(N'2026-06-24T00:15:59.383' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2641, N'jeremias544', N'Baja', CAST(N'2026-06-24T00:16:05.397' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2642, N'jeremias544', N'Media', CAST(N'2026-06-24T00:16:17.593' AS DateTime), 5, 12, N'IdRol: 1, Nombre: ''Admin'', Patentes: 16, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2643, N'jeremias544', N'Alta', CAST(N'2026-06-24T00:16:18.187' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2644, N'jeremias544', N'Baja', CAST(N'2026-06-24T00:16:25.343' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2645, N'jeremias544', N'Baja', CAST(N'2026-06-24T00:25:54.510' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2646, N'jeremias544', N'Baja', CAST(N'2026-07-06T17:38:15.533' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2647, N'jeremias544', N'Alta', CAST(N'2026-07-06T18:02:59.590' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2648, N'jeremias544', N'Baja', CAST(N'2026-07-06T18:40:01.977' AS DateTime), 4, 7, N'Login correcto')
GO
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2649, N'jeremias544', N'Alta', CAST(N'2026-07-06T18:40:01.983' AS DateTime), 5, 17, N'Modulo, TipoEvento')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2650, N'jeremias544', N'Alta', CAST(N'2026-07-06T18:40:07.767' AS DateTime), 5, 18, N'Admin aceptó los cambios externos como válidos.')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2651, N'jeremias544', N'Baja', CAST(N'2026-07-06T18:54:59.307' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2652, N'jeremias544', N'Media', CAST(N'2026-07-06T22:59:25.310' AS DateTime), 4, 5, N'Intento 1/3')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2653, N'jeremias544', N'Baja', CAST(N'2026-07-06T22:59:33.250' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2654, N'jeremias544', N'Alta', CAST(N'2026-07-06T23:42:09.947' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2655, N'jeremias544', N'Baja', CAST(N'2026-07-06T23:52:51.137' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2656, N'jeremias544', N'Baja', CAST(N'2026-07-06T23:52:56.990' AS DateTime), 4, 16, N'es -> en')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2657, N'jeremias544', N'Baja', CAST(N'2026-07-06T23:53:35.727' AS DateTime), 4, 16, N'en -> es')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2658, N'jeremias544', N'Alta', CAST(N'2026-07-06T23:53:37.520' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2659, N'jeremias544', N'Baja', CAST(N'2026-07-07T19:20:09.690' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2660, N'jeremias544', N'Alta', CAST(N'2026-07-07T19:20:09.707' AS DateTime), 5, 17, N'[Insertado] Patente#18 | [Insertado] Patente#19 | [Insertado] RolPatente#1_18 | [Insertado] RolPatente#1_19 | [Insertado] RolPatente#10_18 | [Insertado] RolPatente#10_19 | [Insertado] RolPatente#19_18 | [Insertado] RolPatente#19_19')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2661, N'jeremias544', N'Alta', CAST(N'2026-07-07T19:20:17.710' AS DateTime), 5, 18, N'Admin aceptó los cambios externos como válidos.')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2663, N'jeremias544', N'Baja', CAST(N'2026-07-07T19:22:38.360' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2664, N'jeremias544', N'Alta', CAST(N'2026-07-07T19:22:38.363' AS DateTime), 5, 17, N'[Insertado] TipoEvento#25')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2665, N'jeremias544', N'Alta', CAST(N'2026-07-07T19:22:41.383' AS DateTime), 5, 18, N'Admin aceptó los cambios externos como válidos.')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2666, N'jeremias544', N'Baja', CAST(N'2026-07-07T19:27:01.163' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2667, N'jeremias544', N'Alta', CAST(N'2026-07-07T19:27:01.170' AS DateTime), 5, 17, N'[Eliminado] Patente#19 | [Eliminado] RolPatente#1_19 | [Eliminado] RolPatente#10_19 | [Eliminado] RolPatente#19_19')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2668, N'jeremias544', N'Alta', CAST(N'2026-07-07T19:27:04.087' AS DateTime), 5, 18, N'Admin aceptó los cambios externos como válidos.')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2669, N'jeremias544', N'Media', CAST(N'2026-07-07T19:28:23.367' AS DateTime), 5, 12, N'IdRol: 23, Nombre: ''jere'', Patentes: 3, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2670, N'jeremias544', N'Alta', CAST(N'2026-07-07T19:28:30.800' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2671, N'jeremias544', N'Baja', CAST(N'2026-07-07T19:28:42.627' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2672, N'jeremias544', N'Alta', CAST(N'2026-07-07T19:28:53.740' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2673, N'olivia333', N'Baja', CAST(N'2026-07-07T19:29:05.317' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2674, N'jeremias544', N'Baja', CAST(N'2026-07-07T19:29:31.717' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2675, N'jeremias544', N'Media', CAST(N'2026-07-07T19:29:51.507' AS DateTime), 5, 12, N'IdRol: 23, Nombre: ''jere'', Patentes: 3, Familias: 0')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2676, N'jeremias544', N'Alta', CAST(N'2026-07-07T19:29:55.797' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2677, N'olivia333', N'Baja', CAST(N'2026-07-07T19:30:03.917' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2678, N'jeremias544', N'Baja', CAST(N'2026-07-07T19:30:14.967' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2679, N'jeremias544', N'Alta', CAST(N'2026-07-07T19:35:31.057' AS DateTime), 4, 15, N'LogOut')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2680, N'jeremias544', N'Baja', CAST(N'2026-07-07T19:35:39.820' AS DateTime), 4, 7, N'Login correcto')
INSERT [dbo].[EVENTOS] ([Id_Evento], [UserName], [Criticidad], [FechaHora], [IdModulo], [IdTipoEvento], [Detalle]) VALUES (2681, N'jeremias544', N'Baja', CAST(N'2026-07-07T19:36:22.757' AS DateTime), 4, 16, N'es -> en')
SET IDENTITY_INSERT [dbo].[EVENTOS] OFF
GO
SET IDENTITY_INSERT [dbo].[Familia] ON 

INSERT [dbo].[Familia] ([Id], [Nombre]) VALUES (16, N'BitacoraGestion')
INSERT [dbo].[Familia] ([Id], [Nombre]) VALUES (3, N'Gestion Usuario')
SET IDENTITY_INSERT [dbo].[Familia] OFF
GO
INSERT [dbo].[FamiliaPatente] ([IdFamilia], [IdPatente]) VALUES (3, 2)
INSERT [dbo].[FamiliaPatente] ([IdFamilia], [IdPatente]) VALUES (3, 3)
INSERT [dbo].[FamiliaPatente] ([IdFamilia], [IdPatente]) VALUES (3, 4)
INSERT [dbo].[FamiliaPatente] ([IdFamilia], [IdPatente]) VALUES (3, 5)
INSERT [dbo].[FamiliaPatente] ([IdFamilia], [IdPatente]) VALUES (16, 6)
INSERT [dbo].[FamiliaPatente] ([IdFamilia], [IdPatente]) VALUES (16, 7)
GO
GO
GO
GO
SET IDENTITY_INSERT [dbo].[Modulo] ON 

INSERT [dbo].[Modulo] ([Id], [Nombre]) VALUES (5, N'Admin')
INSERT [dbo].[Modulo] ([Id], [Nombre]) VALUES (4, N'Usuario')
SET IDENTITY_INSERT [dbo].[Modulo] OFF
GO
SET IDENTITY_INSERT [dbo].[Patente] ON 

INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (1, N'Gestión Usuarios - Ver', N'Usuarios.Ver')
INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (2, N'Gestión Usuarios - Crear', N'Usuarios.Crear')
INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (3, N'Gestión Usuarios - Modificar', N'Usuarios.Modificar')
INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (4, N'Gestión Usuarios - Desbloquear', N'Usuarios.Desbloquear')
INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (5, N'Gestión Usuarios - Activar', N'Usuarios.Activar')
INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (6, N'Bitácora - Ver', N'Bitacora.Ver')
INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (7, N'Bitácora - Exportar PDF', N'Bitacora.ExportarPDF')
INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (9, N'Cambiar Contraseña', N'Sesion.CambiarClave')
INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (10, N'Re-Login', N'Sesion.ReLogin')
INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (11, N'Cerrar Sesión', N'Sesion.Logout')
INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (12, N'Permisos - Patentes', N'Permisos.Patentes')
INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (13, N'Permisos - Familias', N'Permisos.Familias')
INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (14, N'Permisos - Roles', N'Permisos.Roles')
INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (15, N'Integridad - Recalcular', N'Integridad.Recalcular')
INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (16, N'Integridad - Restore', N'Integridad.Restore')
INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (17, N'Cambiar Idioma', N'Sesion.CambiarIdioma')
INSERT [dbo].[Patente] ([Id], [Nombre], [DataKey]) VALUES (18, N'Backup - Crear', N'Backup.Crear')
SET IDENTITY_INSERT [dbo].[Patente] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (10, N'AdminGestionUsuario')
INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (23, N'jere')
INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (15, N'Lauty')
INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (16, N'maxi1')
INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (22, N'NoCambiarContrasenia')
INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (21, N'Problema')
INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (11, N'SoloCrear')
INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (19, N'Usuario')
INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (8, N'UsuarioBitacora')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
INSERT [dbo].[RolFamilia] ([IdRol], [IdFamilia]) VALUES (16, 16)
GO
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 1)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 2)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 3)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 4)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 5)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 6)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 7)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 9)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 10)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 11)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 12)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 13)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 14)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 15)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 16)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 17)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (1, 18)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (8, 9)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (8, 10)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (8, 11)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (8, 17)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (10, 1)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (10, 2)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (10, 3)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (10, 4)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (10, 5)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (10, 6)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (10, 7)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (10, 9)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (10, 11)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (10, 13)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (10, 15)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (10, 16)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (10, 17)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (10, 18)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (11, 2)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (11, 9)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (11, 10)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (11, 11)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (11, 17)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (15, 6)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (15, 9)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (15, 10)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (15, 11)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (15, 17)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (16, 5)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (16, 9)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (16, 10)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (16, 11)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (16, 17)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (19, 10)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (19, 11)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (19, 12)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (19, 14)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (19, 15)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (19, 16)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (19, 17)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (19, 18)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (21, 9)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (21, 10)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (21, 11)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (21, 17)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (22, 17)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (23, 10)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (23, 16)
INSERT [dbo].[RolPatente] ([IdRol], [IdPatente]) VALUES (23, 17)
GO
SET IDENTITY_INSERT [dbo].[TipoEvento] ON 

INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (24, N'Backup automatico generado')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (25, N'Backup manual generado')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (14, N'Contraseña cambiada exitosamente')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (5, N'Contraseña incorrecta')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (11, N'Email modificado')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (19, N'Familia creada')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (21, N'Familia eliminada')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (20, N'Familia modificada')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (16, N'Idioma cambiado')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (17, N'Integridad comprometida')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (18, N'Integridad recalculada')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (1, N'Intento de login con sesión ya activa')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (7, N'Login exitoso')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (15, N'Logout realizado')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (22, N'Rol creado')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (23, N'Rol eliminado')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (12, N'Rol modificado')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (9, N'Usuario activado')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (3, N'Usuario bloqueado')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (6, N'Usuario bloqueado por intentos fallidos')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (13, N'Usuario creado')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (10, N'Usuario desactivado')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (8, N'Usuario desbloqueado')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (4, N'Usuario inactivo')
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (2, N'Usuario inexistente')
SET IDENTITY_INSERT [dbo].[TipoEvento] OFF
GO
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'00000000', N'Lopez', N'Lautaro', N'lautaro000', N'bded47e467ed1d20b249e6b8080bd37492872fe0b5c729fae6fd52f8ef769ee7', N'w2/FSHutnIDUW/QcmhZKbQ==', 0, 1, 11, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'11111116', N'Vergara', N'Alejo', N'alejo116', N'1d67ce4964b0bab4066ed684541c91238aa92197e280718038405b7899064db4', N'P+bKpZYuSEhtmCSOfqj/p9WfACx4SyVzqo5+OdKF6EI=', 0, 1, 1, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'12121211', N'Gomez', N'pepe', N'pepe211', N'163d91856700395350a27f141986935e70df70015307eae44853558301db7a89', N'Yo5pmztsqhu13ineCpalcdVf/RSpoJzSyj0NSDJO8/g=', 0, 1, 10, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'12321222', N'rodriguez', N'Lautaro', N'lautaro222', N'0a364dd9329173e02f473152fb8de99d03626013fb069c0f17930fee449e173d', N'B1Kfkv6j+9/yWXidUj5So4zGuizGyQfjLKSwOhJqBhU=', 0, 1, 1, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'12345675', N'Almeida', N'Juan', N'juan675', N'62014fad19089687f5f8b48240572d133f5beffcf60f76c58f5fee901c42876b', N'Juan1@gmail.com', 0, 1, 1, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'12345677', N'gomez', N'facundo', N'facundo677', N'b2f5d51c5716a61833a33f49bb687d26009e8d307bacccf01381782dca76325e', N'facundo1@gmail.com', 0, 1, 15, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'12345678', N'Pignataro', N'Julian', N'julian678', N'd4be6bfaf2340b50d09884b13d0b880e076ef69a6ee94cfeb0bba2e2d08d73cd', N'Julian@gmail.com', 0, 1, 16, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'12345679', N'Rico', N'Alejo', N'alejo679', N'0c2dd9ddcea75ffa1e29b22a34a1bd8f2b928b07ca22c8b67ce06670342a490a', N'Alejo@gmail.com', 1, 1, 8, 3, CAST(N'2026-06-02T18:35:19.970' AS DateTime), 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'22222222', N'Vergara', N'Nahuel', N'nahuel222', N'bded47e467ed1d20b249e6b8080bd37492872fe0b5c729fae6fd52f8ef769ee7', N'RggRlsg81Sqc4AXCBzlCGlvEy1Kj022kQ6hzKnbWmcM=', 0, 1, 8, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'23642736', N'rodriguez', N'rosalia', N'rosalia736', N'861fece367bdd79a8d8436c0c0c03d3bdaadbf0f8b04cf9ed27d4c9800b2ab35', N'ZhRyJpB6yTEvM68su5AGc5/jKCNtUEjy5J314GwKF5E=', 0, 1, 15, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'23642737', N'rodriguez', N'roxana', N'roxana737', N'2f30da4b70ef4dd1ff0c3004ea2fe2ae696a905d5a09dc42beb875e422011753', N'roxana@gmail.com', 0, 1, 1, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'33333333', N'Gomez', N'Olivia', N'olivia333', N'bded47e467ed1d20b249e6b8080bd37492872fe0b5c729fae6fd52f8ef769ee7', N'IKXN9blAl5Zqe5QCofA32VpH9wQ2VwHG3dDW4SSXptI=', 0, 1, 23, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'44444444', N'Pignataro', N'1', N'thiago444', N'92966223827b5069660cd0e3ec0cd906f0a32223af26c22ad14e66eec5c80c75', N'f2Z3GIQxJ6cKjnzeW+ujjHz1v1J0eWkWiYhuuRUJmkg=', 0, 1, 10, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'46947540', N'Rico', N'Julian', N'julian540', N'bded47e467ed1d20b249e6b8080bd37492872fe0b5c729fae6fd52f8ef769ee7', N'xBWBO+r7Cx2nlsV1L2YE53gr5Ji/JGWLFb94CpjHWRg=', 0, 1, 1, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'46947542', N'Gomez', N'Dylan', N'dylan542', N'e34b9d2c74e67d52c92d05e86fe33f399e7838d93aba62862b1a889c5f76fe50', N'GIHVR3AySRiFkfZ/uHSVa5sjQsMIIP1clqNp1rsZX3w=', 0, 1, 1, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'46947544', N'Gomez', N'Jeremias', N'jeremias544', N'bded47e467ed1d20b249e6b8080bd37492872fe0b5c729fae6fd52f8ef769ee7', N'facundojeremias@gmail.com', 0, 1, 1, 0, NULL, 0, N'en')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'47333212', N'Vergara', N'Lautaro111', N'lautaro212', N'914dd5e733e4e6922adb7f4ddd2fcc6612e357cc23454b0b3106145e139436af', N'lautaro@email.com', 0, 1, 1, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'55555555', N'vergara', N'lautaro', N'lautaro555', N'bded47e467ed1d20b249e6b8080bd37492872fe0b5c729fae6fd52f8ef769ee7', N'B1Kfkv6j+9/yWXidUj5So4zGuizGyQfjLKSwOhJqBhU=', 0, 1, 10, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'66666666', N'rodriguez', N'matias', N'matias666', N'bded47e467ed1d20b249e6b8080bd37492872fe0b5c729fae6fd52f8ef769ee7', N'YU/gA00NnsH5fuiuqVIU99/ACvpZrJhHvdA8fA3YYVg=', 0, 1, 8, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'88888555', N'gomez', N'maximo', N'maximo555', N'bded47e467ed1d20b249e6b8080bd37492872fe0b5c729fae6fd52f8ef769ee7', N'zDUvxi3UZowRwf+preKtyJdWNZ4fKL74kAPMdhLbLpU=', 0, 1, 22, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'88888888', N'gomez', N'facundo', N'facundo888', N'bded47e467ed1d20b249e6b8080bd37492872fe0b5c729fae6fd52f8ef769ee7', N'cP3qn8sfM/G7xWboZmdJnkuKYDw2KeYLhPGptShoxTg=', 0, 1, 10, 0, NULL, 0, N'es')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Familia__75E3EFCFCCD3709D]    Script Date: 7/7/2026 7:40:53 PM ******/
ALTER TABLE [dbo].[Familia] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Modulo__75E3EFCFE089E561]    Script Date: 7/7/2026 7:40:53 PM ******/
ALTER TABLE [dbo].[Modulo] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Patente__6A1C4B2F111F5458]    Script Date: 7/7/2026 7:40:53 PM ******/
ALTER TABLE [dbo].[Patente] ADD UNIQUE NONCLUSTERED 
(
	[DataKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Patente__75E3EFCF8F29985D]    Script Date: 7/7/2026 7:40:53 PM ******/
ALTER TABLE [dbo].[Patente] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Roles__75E3EFCF226891CC]    Script Date: 7/7/2026 7:40:53 PM ******/
ALTER TABLE [dbo].[Roles] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__TipoEven__75E3EFCFFE0147AB]    Script Date: 7/7/2026 7:40:53 PM ******/
ALTER TABLE [dbo].[TipoEvento] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_UserName]    Script Date: 7/7/2026 7:40:53 PM ******/
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UQ_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EVENTOS] ADD  DEFAULT (getdate()) FOR [FechaHora]
GO
ALTER TABLE [dbo].[IntegridadDVH] ADD  DEFAULT (getdate()) FOR [FechaCalculo]
GO
ALTER TABLE [dbo].[IntegridadDVV] ADD  DEFAULT (getdate()) FOR [FechaCalculo]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ((0)) FOR [Bloqueo]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ((0)) FOR [IntentosFallidos]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ((0)) FOR [DebeCambiarContrasena]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ('es') FOR [Idioma]
GO
ALTER TABLE [dbo].[EVENTOS]  WITH CHECK ADD  CONSTRAINT [FK_Eventos_Modulo] FOREIGN KEY([IdModulo])
REFERENCES [dbo].[Modulo] ([Id])
GO
ALTER TABLE [dbo].[EVENTOS] CHECK CONSTRAINT [FK_Eventos_Modulo]
GO
ALTER TABLE [dbo].[EVENTOS]  WITH CHECK ADD  CONSTRAINT [FK_Eventos_TipoEvento] FOREIGN KEY([IdTipoEvento])
REFERENCES [dbo].[TipoEvento] ([Id])
GO
ALTER TABLE [dbo].[EVENTOS] CHECK CONSTRAINT [FK_Eventos_TipoEvento]
GO
ALTER TABLE [dbo].[EVENTOS]  WITH CHECK ADD  CONSTRAINT [FK_Eventos_Usuario] FOREIGN KEY([UserName])
REFERENCES [dbo].[Usuario] ([UserName])
GO
ALTER TABLE [dbo].[EVENTOS] CHECK CONSTRAINT [FK_Eventos_Usuario]
GO
ALTER TABLE [dbo].[FamiliaIntegrada]  WITH CHECK ADD  CONSTRAINT [FK_FamiliaIntegrada_Hija] FOREIGN KEY([IdFamiliaHija])
REFERENCES [dbo].[Familia] ([Id])
GO
ALTER TABLE [dbo].[FamiliaIntegrada] CHECK CONSTRAINT [FK_FamiliaIntegrada_Hija]
GO
ALTER TABLE [dbo].[FamiliaIntegrada]  WITH CHECK ADD  CONSTRAINT [FK_FamiliaIntegrada_Padre] FOREIGN KEY([IdFamiliaPadre])
REFERENCES [dbo].[Familia] ([Id])
GO
ALTER TABLE [dbo].[FamiliaIntegrada] CHECK CONSTRAINT [FK_FamiliaIntegrada_Padre]
GO
ALTER TABLE [dbo].[FamiliaPatente]  WITH CHECK ADD  CONSTRAINT [FK_FamiliaPatente_Familia] FOREIGN KEY([IdFamilia])
REFERENCES [dbo].[Familia] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FamiliaPatente] CHECK CONSTRAINT [FK_FamiliaPatente_Familia]
GO
ALTER TABLE [dbo].[FamiliaPatente]  WITH CHECK ADD  CONSTRAINT [FK_FamiliaPatente_Patente] FOREIGN KEY([IdPatente])
REFERENCES [dbo].[Patente] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FamiliaPatente] CHECK CONSTRAINT [FK_FamiliaPatente_Patente]
GO
ALTER TABLE [dbo].[RolFamilia]  WITH CHECK ADD  CONSTRAINT [FK_RolFamilia_Familia] FOREIGN KEY([IdFamilia])
REFERENCES [dbo].[Familia] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolFamilia] CHECK CONSTRAINT [FK_RolFamilia_Familia]
GO
ALTER TABLE [dbo].[RolFamilia]  WITH CHECK ADD  CONSTRAINT [FK_RolFamilia_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolFamilia] CHECK CONSTRAINT [FK_RolFamilia_Rol]
GO
ALTER TABLE [dbo].[RolPatente]  WITH CHECK ADD  CONSTRAINT [FK_RolPatente_Patente] FOREIGN KEY([IdPatente])
REFERENCES [dbo].[Patente] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolPatente] CHECK CONSTRAINT [FK_RolPatente_Patente]
GO
ALTER TABLE [dbo].[RolPatente]  WITH CHECK ADD  CONSTRAINT [FK_RolPatente_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolPatente] CHECK CONSTRAINT [FK_RolPatente_Rol]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Roles] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Roles]
GO
ALTER TABLE [dbo].[FamiliaIntegrada]  WITH CHECK ADD  CONSTRAINT [CK_FamiliaIntegrada_NoAutoref] CHECK  (([IdFamiliaPadre]<>[IdFamiliaHija]))
GO
ALTER TABLE [dbo].[FamiliaIntegrada] CHECK CONSTRAINT [CK_FamiliaIntegrada_NoAutoref]
GO
/* ==========================================================================
   NEGOCIO: RFN 1 (Reserva de vuelo) y RFN 2 (Check-in)
   Base: [Gestion Usuario]
   - Es idempotente: se puede ejecutar más de una vez sin romper nada.
   - NO toca las tablas protegidas por el módulo de integridad
     (Usuario, Roles, Patente, Modulo, TipoEvento, etc.).
   ========================================================================== */
USE [Gestion Usuario]
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

/* ------------------------------ CATÁLOGOS FIJOS ------------------------------
   Los Id coinciden con los enums de la capa BE (ClaseVuelo_GV42, etc.).       */

IF OBJECT_ID(N'dbo.ClaseVuelo', N'U') IS NULL
    CREATE TABLE dbo.ClaseVuelo (
        Id     INT          NOT NULL PRIMARY KEY,
        Nombre NVARCHAR(30) NOT NULL UNIQUE
    )
GO
IF OBJECT_ID(N'dbo.TipoViaje', N'U') IS NULL
    CREATE TABLE dbo.TipoViaje (
        Id     INT          NOT NULL PRIMARY KEY,
        Nombre NVARCHAR(30) NOT NULL UNIQUE
    )
GO
IF OBJECT_ID(N'dbo.EstadoReserva', N'U') IS NULL
    CREATE TABLE dbo.EstadoReserva (
        Id     INT          NOT NULL PRIMARY KEY,
        Nombre NVARCHAR(30) NOT NULL UNIQUE
    )
GO
IF OBJECT_ID(N'dbo.MedioPago', N'U') IS NULL
    CREATE TABLE dbo.MedioPago (
        Id     INT          NOT NULL PRIMARY KEY,
        Nombre NVARCHAR(30) NOT NULL UNIQUE
    )
GO
IF OBJECT_ID(N'dbo.EstadoCheckIn', N'U') IS NULL
    CREATE TABLE dbo.EstadoCheckIn (
        Id     INT          NOT NULL PRIMARY KEY,
        Nombre NVARCHAR(30) NOT NULL UNIQUE
    )
GO

INSERT INTO dbo.ClaseVuelo (Id, Nombre)
SELECT v.Id, v.Nombre FROM (VALUES (1, N'Económica'), (2, N'Ejecutiva'), (3, N'Primera clase')) v(Id, Nombre)
WHERE NOT EXISTS (SELECT 1 FROM dbo.ClaseVuelo c WHERE c.Id = v.Id)
GO
INSERT INTO dbo.TipoViaje (Id, Nombre)
SELECT v.Id, v.Nombre FROM (VALUES (1, N'Ida'), (2, N'Ida y Vuelta')) v(Id, Nombre)
WHERE NOT EXISTS (SELECT 1 FROM dbo.TipoViaje c WHERE c.Id = v.Id)
GO
INSERT INTO dbo.EstadoReserva (Id, Nombre)
SELECT v.Id, v.Nombre FROM (VALUES (1, N'Pendiente de Pago'), (2, N'Confirmada')) v(Id, Nombre)
WHERE NOT EXISTS (SELECT 1 FROM dbo.EstadoReserva c WHERE c.Id = v.Id)
GO
INSERT INTO dbo.MedioPago (Id, Nombre)
SELECT v.Id, v.Nombre FROM (VALUES (1, N'Tarjeta de débito'), (2, N'Tarjeta de crédito'), (3, N'Transferencia'), (4, N'Efectivo')) v(Id, Nombre)
WHERE NOT EXISTS (SELECT 1 FROM dbo.MedioPago c WHERE c.Id = v.Id)
GO
INSERT INTO dbo.EstadoCheckIn (Id, Nombre)
SELECT v.Id, v.Nombre FROM (VALUES (1, N'Pendiente'), (2, N'Realizado')) v(Id, Nombre)
WHERE NOT EXISTS (SELECT 1 FROM dbo.EstadoCheckIn c WHERE c.Id = v.Id)
GO

/* --------------------------- CATÁLOGOS AMPLIABLES --------------------------- */

IF OBJECT_ID(N'dbo.TipoAdicional', N'U') IS NULL
    CREATE TABLE dbo.TipoAdicional (
        Id     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Nombre NVARCHAR(60)      NOT NULL UNIQUE,
        Activo BIT               NOT NULL CONSTRAINT DF_TipoAdicional_Activo DEFAULT (1)
    )
GO
INSERT INTO dbo.TipoAdicional (Nombre)
SELECT v.Nombre FROM (VALUES (N'Equipaje extra'), (N'Asiento preferencial'), (N'Comida especial'),
                             (N'Asistencia prioritaria'), (N'Otro')) v(Nombre)
WHERE NOT EXISTS (SELECT 1 FROM dbo.TipoAdicional t WHERE t.Nombre = v.Nombre)
GO

IF OBJECT_ID(N'dbo.Aeropuerto', N'U') IS NULL
    CREATE TABLE dbo.Aeropuerto (
        Id         INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        CodigoIata CHAR(3)           NOT NULL UNIQUE,
        Nombre     NVARCHAR(100)     NOT NULL,
        Ciudad     NVARCHAR(60)      NOT NULL,
        Pais       NVARCHAR(60)      NOT NULL
    )
GO

IF OBJECT_ID(N'dbo.Aerolinea', N'U') IS NULL
    CREATE TABLE dbo.Aerolinea (
        Id     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Nombre NVARCHAR(80)      NOT NULL UNIQUE
    )
GO

/* --------------------------------- VUELOS ---------------------------------- */

IF OBJECT_ID(N'dbo.Vuelo', N'U') IS NULL
    CREATE TABLE dbo.Vuelo (
        Id                INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        CodigoVuelo       NVARCHAR(10)      NOT NULL UNIQUE,
        IdAerolinea       INT               NOT NULL CONSTRAINT FK_Vuelo_Aerolinea REFERENCES dbo.Aerolinea (Id),
        IdOrigen          INT               NOT NULL CONSTRAINT FK_Vuelo_Origen    REFERENCES dbo.Aeropuerto (Id),
        IdDestino         INT               NOT NULL CONSTRAINT FK_Vuelo_Destino   REFERENCES dbo.Aeropuerto (Id),
        FechaHoraSalida   DATETIME2(0)      NOT NULL,
        FechaHoraLlegada  DATETIME2(0)      NOT NULL,
        PuertaEmbarque    NVARCHAR(10)      NOT NULL,
        CostoKiloExceso   DECIMAL(10,2)     NOT NULL CONSTRAINT CK_Vuelo_CostoKilo CHECK (CostoKiloExceso >= 0),
        CONSTRAINT CK_Vuelo_Ruta    CHECK (IdOrigen <> IdDestino),
        CONSTRAINT CK_Vuelo_Horario CHECK (FechaHoraLlegada > FechaHoraSalida)
    )
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Vuelo_Busqueda' AND object_id = OBJECT_ID(N'dbo.Vuelo'))
    CREATE INDEX IX_Vuelo_Busqueda ON dbo.Vuelo (IdOrigen, IdDestino, FechaHoraSalida)
GO

-- Precio, cupo y franquicia de equipaje de cada clase de un vuelo.
-- AsientosReservados sube al generar una reserva (incluso pendiente de pago).
IF OBJECT_ID(N'dbo.VueloClase', N'U') IS NULL
    CREATE TABLE dbo.VueloClase (
        IdVuelo              INT           NOT NULL CONSTRAINT FK_VueloClase_Vuelo REFERENCES dbo.Vuelo (Id),
        IdClase              INT           NOT NULL CONSTRAINT FK_VueloClase_Clase REFERENCES dbo.ClaseVuelo (Id),
        PrecioBase           DECIMAL(12,2) NOT NULL CONSTRAINT CK_VueloClase_Precio CHECK (PrecioBase >= 0),
        CapacidadAsientos    INT           NOT NULL CONSTRAINT CK_VueloClase_Capacidad CHECK (CapacidadAsientos >= 0),
        AsientosReservados   INT           NOT NULL CONSTRAINT DF_VueloClase_Reservados DEFAULT (0),
        FranquiciaEquipajeKg DECIMAL(6,2)  NOT NULL CONSTRAINT CK_VueloClase_Franquicia CHECK (FranquiciaEquipajeKg >= 0),
        CONSTRAINT PK_VueloClase PRIMARY KEY (IdVuelo, IdClase),
        CONSTRAINT CK_VueloClase_Cupo CHECK (AsientosReservados >= 0 AND AsientosReservados <= CapacidadAsientos)
    )
GO

IF OBJECT_ID(N'dbo.Asiento', N'U') IS NULL
    CREATE TABLE dbo.Asiento (
        Id            INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        IdVuelo       INT               NOT NULL CONSTRAINT FK_Asiento_Vuelo REFERENCES dbo.Vuelo (Id),
        Fila          INT               NOT NULL,
        Letra         CHAR(1)           NOT NULL,
        NumeroAsiento AS (CAST(Fila AS NVARCHAR(3)) + CAST(Letra AS NVARCHAR(1))) PERSISTED,
        IdClase       INT               NOT NULL CONSTRAINT FK_Asiento_Clase REFERENCES dbo.ClaseVuelo (Id),
        Ubicacion     NVARCHAR(10)      NOT NULL CONSTRAINT CK_Asiento_Ubicacion CHECK (Ubicacion IN (N'Ventana', N'Central', N'Pasillo')),
        CONSTRAINT UQ_Asiento_Vuelo_Numero UNIQUE (IdVuelo, Fila, Letra)
    )
GO

/* ------------------------------ PERSONAS (RFN 1) ------------------------------
   El Email se guarda cifrado desde la capa DAL (igual que en Usuario).          */

IF OBJECT_ID(N'dbo.Cliente', N'U') IS NULL
    CREATE TABLE dbo.Cliente (
        DNI       NVARCHAR(20)  NOT NULL PRIMARY KEY,
        Nombre    NVARCHAR(60)  NOT NULL,
        Apellido  NVARCHAR(60)  NOT NULL,
        Email     NVARCHAR(500) NOT NULL,
        Telefono  NVARCHAR(30)  NOT NULL,
        FechaAlta DATETIME2(0)  NOT NULL CONSTRAINT DF_Cliente_FechaAlta DEFAULT (GETDATE())
    )
GO
IF OBJECT_ID(N'dbo.Pasajero', N'U') IS NULL
    CREATE TABLE dbo.Pasajero (
        DNI      NVARCHAR(20)  NOT NULL PRIMARY KEY,
        Nombre   NVARCHAR(60)  NOT NULL,
        Apellido NVARCHAR(60)  NOT NULL,
        Email    NVARCHAR(500) NOT NULL,
        Telefono NVARCHAR(30)  NOT NULL
    )
GO

/* --------------------------------- RESERVA ---------------------------------- */

IF OBJECT_ID(N'dbo.Reserva', N'U') IS NULL
    CREATE TABLE dbo.Reserva (
        Id                  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        NumeroReserva       AS (N'RES-' + RIGHT(N'000000' + CAST(Id AS NVARCHAR(10)), 6)) PERSISTED,
        DniCliente          NVARCHAR(20)      NOT NULL CONSTRAINT FK_Reserva_Cliente    REFERENCES dbo.Cliente (DNI),
        IdVuelo             INT               NOT NULL,
        IdClase             INT               NOT NULL,
        IdTipoViaje         INT               NOT NULL CONSTRAINT FK_Reserva_TipoViaje  REFERENCES dbo.TipoViaje (Id),
        FechaRegreso        DATE              NULL,
        CantidadPasajeros   INT               NOT NULL CONSTRAINT CK_Reserva_Cantidad CHECK (CantidadPasajeros > 0),
        ImporteBase         DECIMAL(12,2)     NOT NULL,
        SubtotalAdicionales DECIMAL(12,2)     NOT NULL CONSTRAINT DF_Reserva_Subtotal DEFAULT (0),
        Impuestos           DECIMAL(12,2)     NOT NULL,
        ImporteTotal        DECIMAL(12,2)     NOT NULL,
        IdEstadoReserva     INT               NOT NULL CONSTRAINT DF_Reserva_Estado DEFAULT (1)
                                              CONSTRAINT FK_Reserva_Estado REFERENCES dbo.EstadoReserva (Id),
        FechaRealizacion    DATETIME2(0)      NOT NULL CONSTRAINT DF_Reserva_Fecha DEFAULT (GETDATE()),
        LoginVendedor       NVARCHAR(50)      NOT NULL,
        CONSTRAINT FK_Reserva_VueloClase FOREIGN KEY (IdVuelo, IdClase) REFERENCES dbo.VueloClase (IdVuelo, IdClase),
        CONSTRAINT UQ_Reserva_Numero UNIQUE (NumeroReserva),
        CONSTRAINT CK_Reserva_FechaRegreso CHECK (
            (IdTipoViaje = 1 AND FechaRegreso IS NULL) OR (IdTipoViaje = 2 AND FechaRegreso IS NOT NULL))
    )
GO

IF OBJECT_ID(N'dbo.ReservaPasajero', N'U') IS NULL
    CREATE TABLE dbo.ReservaPasajero (
        IdReserva   INT          NOT NULL CONSTRAINT FK_ReservaPasajero_Reserva  REFERENCES dbo.Reserva (Id),
        DniPasajero NVARCHAR(20) NOT NULL CONSTRAINT FK_ReservaPasajero_Pasajero REFERENCES dbo.Pasajero (DNI),
        CONSTRAINT PK_ReservaPasajero PRIMARY KEY (IdReserva, DniPasajero)
    )
GO

IF OBJECT_ID(N'dbo.ReservaAdicional', N'U') IS NULL
    CREATE TABLE dbo.ReservaAdicional (
        Id              INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        IdReserva       INT               NOT NULL CONSTRAINT FK_ReservaAdicional_Reserva REFERENCES dbo.Reserva (Id),
        IdTipoAdicional INT               NOT NULL CONSTRAINT FK_ReservaAdicional_Tipo    REFERENCES dbo.TipoAdicional (Id),
        Cantidad        INT               NOT NULL CONSTRAINT CK_ReservaAdicional_Cantidad CHECK (Cantidad > 0),
        CostoUnitario   DECIMAL(12,2)     NOT NULL CONSTRAINT CK_ReservaAdicional_Costo    CHECK (CostoUnitario >= 0),
        Subtotal        AS (Cantidad * CostoUnitario) PERSISTED
    )
GO

IF OBJECT_ID(N'dbo.Pago', N'U') IS NULL
    CREATE TABLE dbo.Pago (
        Id                  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        IdReserva         INT               NOT NULL,
        ImporteTotalAbonado DECIMAL(12,2)     NOT NULL CONSTRAINT CK_Pago_Importe CHECK (ImporteTotalAbonado > 0),
        IdMedioPago         INT               NOT NULL CONSTRAINT FK_Pago_Medio REFERENCES dbo.MedioPago (Id),
        NumeroTransaccion   NVARCHAR(40)      NOT NULL CONSTRAINT UQ_Pago_Transaccion UNIQUE,
        FechaHoraPago       DATETIME2(0)      NOT NULL CONSTRAINT DF_Pago_Fecha DEFAULT (GETDATE()),
        LoginVendedor       NVARCHAR(50)      NOT NULL,
        CONSTRAINT UQ_Pago_Reserva UNIQUE (IdReserva),
        CONSTRAINT FK_Pago_Reserva FOREIGN KEY (IdReserva) REFERENCES dbo.Reserva (Id)
    )
GO

-- Un boleto por pasajero, emitido al confirmarse el pago.
IF OBJECT_ID(N'dbo.Boleto', N'U') IS NULL
    CREATE TABLE dbo.Boleto (
        Id            INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        NumeroBoleto  AS (N'BOL-' + RIGHT(N'000000' + CAST(Id AS NVARCHAR(10)), 6)) PERSISTED,
        IdReserva     INT               NOT NULL,
        DniPasajero   NVARCHAR(20)      NOT NULL,
        FechaEmision  DATETIME2(0)      NOT NULL CONSTRAINT DF_Boleto_Fecha DEFAULT (GETDATE()),
        CONSTRAINT UQ_Boleto_Numero UNIQUE (NumeroBoleto),
        CONSTRAINT UQ_Boleto_Pasajero UNIQUE (IdReserva, DniPasajero),
        CONSTRAINT FK_Boleto_ReservaPasajero FOREIGN KEY (IdReserva, DniPasajero) REFERENCES dbo.ReservaPasajero (IdReserva, DniPasajero)
    )
GO

/* --------------------------------- CHECK-IN --------------------------------- */

-- Se crea (estado Pendiente) para cada pasajero al confirmarse el pago de la reserva.
IF OBJECT_ID(N'dbo.CheckIn', N'U') IS NULL
    CREATE TABLE dbo.CheckIn (
        Id               INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        IdReserva        INT               NOT NULL,
        DniPasajero      NVARCHAR(20)      NOT NULL,
        IdEstadoCheckIn  INT               NOT NULL CONSTRAINT DF_CheckIn_Estado DEFAULT (1)
                                           CONSTRAINT FK_CheckIn_Estado REFERENCES dbo.EstadoCheckIn (Id),
        FechaHoraCheckIn DATETIME2(0)      NULL,
        IdAsiento        INT               NULL CONSTRAINT FK_CheckIn_Asiento REFERENCES dbo.Asiento (Id),
        LoginEncargado   NVARCHAR(50)      NULL,
        CONSTRAINT UQ_CheckIn_Pasajero UNIQUE (IdReserva, DniPasajero),
        CONSTRAINT FK_CheckIn_ReservaPasajero FOREIGN KEY (IdReserva, DniPasajero) REFERENCES dbo.ReservaPasajero (IdReserva, DniPasajero)
    )
GO
-- Un asiento no puede estar asignado a dos check-ins a la vez.
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UX_CheckIn_Asiento' AND object_id = OBJECT_ID(N'dbo.CheckIn'))
    CREATE UNIQUE INDEX UX_CheckIn_Asiento ON dbo.CheckIn (IdAsiento) WHERE IdAsiento IS NOT NULL
GO

IF OBJECT_ID(N'dbo.Equipaje', N'U') IS NULL
    CREATE TABLE dbo.Equipaje (
        Id             INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        IdCheckIn         INT               NOT NULL,
        CantidadBultos INT               NOT NULL CONSTRAINT CK_Equipaje_Bultos CHECK (CantidadBultos > 0),
        PesoTotalKg    DECIMAL(7,2)      NOT NULL CONSTRAINT CK_Equipaje_Peso   CHECK (PesoTotalKg > 0),
        FranquiciaKg   DECIMAL(7,2)      NOT NULL,
        CONSTRAINT UQ_Equipaje_CheckIn UNIQUE (IdCheckIn),
        CONSTRAINT FK_Equipaje_CheckIn FOREIGN KEY (IdCheckIn) REFERENCES dbo.CheckIn (Id)
    )
GO

IF OBJECT_ID(N'dbo.EtiquetaEquipaje', N'U') IS NULL
    CREATE TABLE dbo.EtiquetaEquipaje (
        Id             INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        IdEquipaje     INT               NOT NULL CONSTRAINT FK_Etiqueta_Equipaje REFERENCES dbo.Equipaje (Id),
        CodigoEquipaje NVARCHAR(20)      NOT NULL CONSTRAINT UQ_Etiqueta_Codigo UNIQUE
    )
GO

-- Solo existe si el peso superó la franquicia; incluye el cobro al pasajero.
IF OBJECT_ID(N'dbo.CargoExcesoEquipaje', N'U') IS NULL
    CREATE TABLE dbo.CargoExcesoEquipaje (
        Id                INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        IdEquipaje        INT               NOT NULL,
        KilosExceso       DECIMAL(7,2)      NOT NULL CONSTRAINT CK_Cargo_Kilos CHECK (KilosExceso > 0),
        CostoPorKilo      DECIMAL(10,2)     NOT NULL,
        ImporteCargo      DECIMAL(12,2)     NOT NULL,
        IdMedioPago       INT               NOT NULL CONSTRAINT FK_Cargo_Medio REFERENCES dbo.MedioPago (Id),
        NumeroTransaccion NVARCHAR(40)      NULL,
        FechaHoraCobro    DATETIME2(0)      NOT NULL CONSTRAINT DF_Cargo_Fecha DEFAULT (GETDATE()),
        CONSTRAINT UQ_Cargo_Equipaje UNIQUE (IdEquipaje),
        CONSTRAINT FK_Cargo_Equipaje FOREIGN KEY (IdEquipaje) REFERENCES dbo.Equipaje (Id)
    )
GO

IF OBJECT_ID(N'dbo.TarjetaEmbarque', N'U') IS NULL
    CREATE TABLE dbo.TarjetaEmbarque (
        Id                 INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        NumeroTarjeta      AS (N'TE-' + RIGHT(N'000000' + CAST(Id AS NVARCHAR(10)), 6)) PERSISTED,
        IdCheckIn         INT               NOT NULL,
        PuertaEmbarque     NVARCHAR(10)      NOT NULL,
        HoraLimiteEmbarque DATETIME2(0)      NOT NULL,
        FechaHoraEmision   DATETIME2(0)      NOT NULL CONSTRAINT DF_Tarjeta_Fecha DEFAULT (GETDATE()),
        CONSTRAINT UQ_Tarjeta_Numero UNIQUE (NumeroTarjeta),
        CONSTRAINT UQ_Tarjeta_CheckIn UNIQUE (IdCheckIn),
        CONSTRAINT FK_Tarjeta_CheckIn FOREIGN KEY (IdCheckIn) REFERENCES dbo.CheckIn (Id)
    )
GO

/* =====================================================================
   DATOS DE EJEMPLO (aeropuertos, aerolíneas, vuelos y asientos)
   Las fechas son relativas a HOY para que siempre haya vuelos futuros.
   Para probar el check-in usar el vuelo AR1500 (sale mañana, dentro de
   la ventana de 48 hs).
   ===================================================================== */

INSERT INTO dbo.Aeropuerto (CodigoIata, Nombre, Ciudad, Pais)
SELECT v.Iata, v.Nombre, v.Ciudad, v.Pais
FROM (VALUES
    ('AEP', N'Aeroparque Jorge Newbery',            N'Buenos Aires', N'Argentina'),
    ('EZE', N'Aeropuerto Internacional Ezeiza',     N'Buenos Aires', N'Argentina'),
    ('COR', N'Aeropuerto Internacional Pajas Blancas', N'Córdoba',   N'Argentina'),
    ('MDZ', N'Aeropuerto Internacional El Plumerillo', N'Mendoza',   N'Argentina'),
    ('BRC', N'Aeropuerto Internacional Teniente Candelaria', N'Bariloche', N'Argentina'),
    ('IGR', N'Aeropuerto Internacional Cataratas del Iguazú', N'Puerto Iguazú', N'Argentina')
) v(Iata, Nombre, Ciudad, Pais)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Aeropuerto a WHERE a.CodigoIata = v.Iata)
GO

INSERT INTO dbo.Aerolinea (Nombre)
SELECT v.Nombre FROM (VALUES (N'Aerolíneas Argentinas'), (N'Flybondi'), (N'JetSMART')) v(Nombre)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Aerolinea a WHERE a.Nombre = v.Nombre)
GO

-- Vuelos de ejemplo.
INSERT INTO dbo.Vuelo (CodigoVuelo, IdAerolinea, IdOrigen, IdDestino, FechaHoraSalida, FechaHoraLlegada, PuertaEmbarque, CostoKiloExceso)
SELECT v.Codigo, al.Id, o.Id, d.Id,
       DATEADD(MINUTE, v.MinSalida, CAST(CAST(GETDATE() AS DATE) AS DATETIME2(0))) ,
       DATEADD(MINUTE, v.MinSalida + v.DuracionMin, CAST(CAST(GETDATE() AS DATE) AS DATETIME2(0))),
       v.Puerta, v.CostoKilo
FROM (VALUES
    -- Código  Aerolínea                  Origen Destino  minutos desde hoy 00:00      Duración Puerta  $/kg
    (N'AR1500', N'Aerolíneas Argentinas', 'AEP', 'COR',   1 * 1440 + 10 * 60 + 30,      80,      N'A4',  3500.00),
    (N'AR1502', N'Aerolíneas Argentinas', 'AEP', 'COR',   1 * 1440 + 18 * 60,           80,      N'A6',  3500.00),
    (N'FB3020', N'Flybondi',              'AEP', 'MDZ',   3 * 1440 +  7 * 60 + 15,      115,     N'B2',  3000.00),
    (N'JA8110', N'JetSMART',              'AEP', 'BRC',   5 * 1440 +  9 * 60,           140,     N'C1',  3200.00),
    (N'AR1880', N'Aerolíneas Argentinas', 'EZE', 'IGR',   6 * 1440 + 13 * 60 + 45,      120,     N'D3',  3800.00),
    (N'AR1501', N'Aerolíneas Argentinas', 'COR', 'AEP',   7 * 1440 + 16 * 60,           80,      N'B5',  3500.00)
) v(Codigo, Aerolinea, Origen, Destino, MinSalida, DuracionMin, Puerta, CostoKilo)
INNER JOIN dbo.Aerolinea al ON al.Nombre = v.Aerolinea
INNER JOIN dbo.Aeropuerto o ON o.CodigoIata = v.Origen
INNER JOIN dbo.Aeropuerto d ON d.CodigoIata = v.Destino
WHERE NOT EXISTS (SELECT 1 FROM dbo.Vuelo x WHERE x.CodigoVuelo = v.Codigo)
GO

-- Asientos de cada vuelo de ejemplo:
--   Filas 1-2   Primera clase  (12 asientos)
--   Filas 3-6   Ejecutiva      (24 asientos)
--   Filas 7-20  Económica      (84 asientos)
-- Letras A y F = Ventana, B y E = Central, C y D = Pasillo.
;WITH Filas AS (
    SELECT TOP (20) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS Fila FROM sys.all_objects
),
Letras AS (
    SELECT l.Letra, l.Ubicacion FROM (VALUES
        ('A', N'Ventana'), ('B', N'Central'), ('C', N'Pasillo'),
        ('D', N'Pasillo'), ('E', N'Central'), ('F', N'Ventana')) l(Letra, Ubicacion)
)
INSERT INTO dbo.Asiento (IdVuelo, Fila, Letra, IdClase, Ubicacion)
SELECT v.Id, f.Fila, l.Letra,
       CASE WHEN f.Fila <= 2 THEN 3 WHEN f.Fila <= 6 THEN 2 ELSE 1 END,
       l.Ubicacion
FROM dbo.Vuelo v
CROSS JOIN Filas f
CROSS JOIN Letras l
WHERE NOT EXISTS (SELECT 1 FROM dbo.Asiento a WHERE a.IdVuelo = v.Id)
GO

-- Cupo, precio y franquicia por clase (el cupo coincide con la cantidad de asientos creados).
INSERT INTO dbo.VueloClase (IdVuelo, IdClase, PrecioBase, CapacidadAsientos, FranquiciaEquipajeKg)
SELECT v.Id, c.IdClase,
       CAST(ROUND(b.PrecioEco * c.Factor, 0) AS DECIMAL(12,2)),
       c.Capacidad, c.Franquicia
FROM dbo.Vuelo v
INNER JOIN (VALUES
    (N'AR1500', 95000.00), (N'AR1502', 98000.00), (N'FB3020', 72000.00),
    (N'JA8110', 89000.00), (N'AR1880', 130000.00), (N'AR1501', 95000.00)
) b(Codigo, PrecioEco) ON b.Codigo = v.CodigoVuelo
CROSS JOIN (VALUES
    -- IdClase Factor Capacidad Franquicia (kg)
    (1, 1.0, 84, 15.0),
    (2, 2.2, 24, 23.0),
    (3, 3.5, 12, 32.0)
) c(IdClase, Factor, Capacidad, Franquicia)
WHERE NOT EXISTS (SELECT 1 FROM dbo.VueloClase x WHERE x.IdVuelo = v.Id AND x.IdClase = c.IdClase)
GO
GO
USE [master]
GO
ALTER DATABASE [Gestion Usuario] SET  READ_WRITE 
GO
