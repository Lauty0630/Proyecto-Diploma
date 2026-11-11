/* =====================================================================================
   EsquemaBase.sql  -  instalación inicial: GESTIÓN DE USUARIOS (sin el negocio)
   ---------------------------------------------------------------------------------
   Crea la base "Gestion Usuario" con lo que tenía el sistema antes del negocio:
     usuarios, roles, patentes y familias de permisos, bitácora, integridad (dígitos
     verificadores), backup/restore e idioma.
   NO crea nada del negocio (vuelos, reservas, pagos, check-in, clientes, reportes).
   El negocio se agrega después con la opción "Actualizar" del instalador, que ejecuta
   ActualizacionNegocio.sql y ActualizacionBD.sql SIN borrar ni modificar lo ya cargado.

   Generado a partir de EsquemaCompleto.sql (mismas tablas, tipos y restricciones). Los datos
   iniciales son: el rol Admin con todos sus permisos y los usuarios administradores.
   Marca de esta base: dbo.VersionBD_GV42 con la versión 0.
   ===================================================================================== */
USE [master]
GO
/****** Objeto: Database [Gestion Usuario] Fecha de script: 29/09/2026 08:02:21 ******/
-- Portable: sin rutas de archivo fijas, SQL Server usa su carpeta de datos por defecto
-- (antes apuntaba a C:\Users\Usuario\..., que solo existe en la computadora original).
IF CAST(PARSENAME(CONVERT(VARCHAR(20), SERVERPROPERTY('ProductVersion')), 4) AS INT) < 14
    RAISERROR(N'Se requiere SQL Server 2017 o superior (por ejemplo LocalDB 2019, que se instala con Visual Studio).', 16, 1);
ELSE
    CREATE DATABASE [Gestion Usuario];
GO
-- Nivel de compatibilidad según la versión instalada (máximo 150 = SQL Server 2019).
DECLARE @Nivel INT = CAST(PARSENAME(CONVERT(VARCHAR(20), SERVERPROPERTY('ProductVersion')), 4) AS INT) * 10;
IF @Nivel > 150 SET @Nivel = 150;
DECLARE @Sql NVARCHAR(200) = N'ALTER DATABASE [Gestion Usuario] SET COMPATIBILITY_LEVEL = ' + CAST(@Nivel AS NVARCHAR(3));
EXEC (@Sql);
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
ALTER DATABASE [Gestion Usuario] SET QUERY_STORE = OFF
GO
USE [Gestion Usuario]
GO
/****** Objeto: Table [dbo].[EVENTOS] Fecha de script: 29/09/2026 08:02:22 ******/
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
/****** Objeto: Table [dbo].[Familia] Fecha de script: 29/09/2026 08:02:22 ******/
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
/****** Objeto: Table [dbo].[FamiliaIntegrada] Fecha de script: 29/09/2026 08:02:22 ******/
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
/****** Objeto: Table [dbo].[FamiliaPatente] Fecha de script: 29/09/2026 08:02:22 ******/
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
/****** Objeto: Table [dbo].[IntegridadDVH] Fecha de script: 29/09/2026 08:02:22 ******/
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
/****** Objeto: Table [dbo].[IntegridadDVV] Fecha de script: 29/09/2026 08:02:22 ******/
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
/****** Objeto: Table [dbo].[Modulo] Fecha de script: 29/09/2026 08:02:22 ******/
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
/****** Objeto: Table [dbo].[Patente] Fecha de script: 29/09/2026 08:02:22 ******/
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
/****** Objeto: Table [dbo].[Roles] Fecha de script: 29/09/2026 08:02:22 ******/
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
/****** Objeto: Table [dbo].[RolFamilia] Fecha de script: 29/09/2026 08:02:22 ******/
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
/****** Objeto: Table [dbo].[RolPatente] Fecha de script: 29/09/2026 08:02:22 ******/
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
/****** Objeto: Table [dbo].[TipoEvento] Fecha de script: 29/09/2026 08:02:22 ******/
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
/****** Objeto: Table [dbo].[Usuario] Fecha de script: 29/09/2026 08:02:22 ******/
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
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ__Familia__75E3EFCFCB4C7743] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[Familia] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ__Modulo__75E3EFCFD90CD761] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[Modulo] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ__Patente__6A1C4B2FB3D7E365] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[Patente] ADD UNIQUE NONCLUSTERED 
(
	[DataKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ__Patente__75E3EFCF1C160AB9] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[Patente] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ__Roles__75E3EFCF6E44373F] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[Roles] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ__TipoEven__75E3EFCFC80AFEF3] Fecha de script: 29/09/2026 08:02:22 ******/
ALTER TABLE [dbo].[TipoEvento] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_UserName] Fecha de script: 29/09/2026 08:02:22 ******/
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
/* ---------- Versión de la base y tareas pendientes (mismas tablas que crea ActualizacionBD.sql) ---------- */
CREATE TABLE dbo.VersionBD_GV42 (
    Version         INT          NOT NULL PRIMARY KEY,
    FechaAplicacion DATETIME2(0) NOT NULL CONSTRAINT DF_VersionBD_Fecha DEFAULT (SYSDATETIME()),
    Descripcion     NVARCHAR(200) NULL
);
GO
CREATE TABLE dbo.TareaPendiente_GV42 (Nombre NVARCHAR(100) NOT NULL PRIMARY KEY);
GO
INSERT INTO dbo.VersionBD_GV42 (Version, Descripcion)
VALUES (0, N'Base de gestión de usuarios (sin negocio)');
GO
/* ---------- Datos iniciales ---------- */
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
SET IDENTITY_INSERT [dbo].[Roles] OFF
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
INSERT [dbo].[TipoEvento] ([Id], [Nombre]) VALUES (44, N'Backup restaurado')
SET IDENTITY_INSERT [dbo].[TipoEvento] OFF
GO
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'11111116', N'Vergara', N'Alejo', N'alejo116', N'1d67ce4964b0bab4066ed684541c91238aa92197e280718038405b7899064db4', N'P+bKpZYuSEhtmCSOfqj/p9WfACx4SyVzqo5+OdKF6EI=', 0, 1, 1, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'12321222', N'rodriguez', N'Lautaro', N'lautaro222', N'0a364dd9329173e02f473152fb8de99d03626013fb069c0f17930fee449e173d', N'B1Kfkv6j+9/yWXidUj5So4zGuizGyQfjLKSwOhJqBhU=', 0, 1, 1, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'12345675', N'Almeida', N'Juan', N'juan675', N'62014fad19089687f5f8b48240572d133f5beffcf60f76c58f5fee901c42876b', N'Juan1@gmail.com', 0, 1, 1, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'23642737', N'rodriguez', N'roxana', N'roxana737', N'2f30da4b70ef4dd1ff0c3004ea2fe2ae696a905d5a09dc42beb875e422011753', N'roxana@gmail.com', 0, 1, 1, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'46947540', N'Rico', N'Julian', N'julian540', N'bded47e467ed1d20b249e6b8080bd37492872fe0b5c729fae6fd52f8ef769ee7', N'xBWBO+r7Cx2nlsV1L2YE53gr5Ji/JGWLFb94CpjHWRg=', 0, 1, 1, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'46947542', N'Gomez', N'Dylan', N'dylan542', N'e34b9d2c74e67d52c92d05e86fe33f399e7838d93aba62862b1a889c5f76fe50', N'GIHVR3AySRiFkfZ/uHSVa5sjQsMIIP1clqNp1rsZX3w=', 0, 1, 1, 0, NULL, 0, N'es')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'46947544', N'Gomez', N'Jeremias', N'jeremias544', N'bded47e467ed1d20b249e6b8080bd37492872fe0b5c729fae6fd52f8ef769ee7', N'facundojeremias@gmail.com', 0, 1, 1, 0, NULL, 0, N'en')
INSERT [dbo].[Usuario] ([DNI], [Apellido], [Nombre], [UserName], [Contrasena], [Email], [Bloqueo], [Activo], [IdRol], [IntentosFallidos], [UltimoIntentoFallido], [DebeCambiarContrasena], [Idioma]) VALUES (N'47333212', N'Vergara', N'Lautaro111', N'lautaro212', N'914dd5e733e4e6922adb7f4ddd2fcc6612e357cc23454b0b3106145e139436af', N'lautaro@email.com', 0, 1, 1, 0, NULL, 0, N'es')
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
GO
/* ---------- Dígitos verificadores: los calcula el sistema al terminar la instalación ---------- */
INSERT INTO dbo.TareaPendiente_GV42 (Nombre)
SELECT X.T FROM (VALUES (N'RecalcularDV:Usuario'), (N'RecalcularDV:Roles'), (N'RecalcularDV:Familia'), (N'RecalcularDV:Patente'), (N'RecalcularDV:FamiliaPatente'), (N'RecalcularDV:FamiliaIntegrada'), (N'RecalcularDV:RolPatente'), (N'RecalcularDV:RolFamilia'), (N'RecalcularDV:Modulo'), (N'RecalcularDV:TipoEvento')) AS X(T)
WHERE NOT EXISTS (SELECT 1 FROM dbo.TareaPendiente_GV42 P WHERE P.Nombre = X.T);
GO
USE [master]
GO
ALTER DATABASE [Gestion Usuario] SET  READ_WRITE 
GO
