USE [master]
GO
/****** Object:  Database [prueba]    Script Date: 21/2/2025 12:10:52 ******/
CREATE DATABASE [prueba]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'prueba', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\prueba.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'prueba_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\prueba_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [prueba] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [prueba].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [prueba] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [prueba] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [prueba] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [prueba] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [prueba] SET ARITHABORT OFF 
GO
ALTER DATABASE [prueba] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [prueba] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [prueba] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [prueba] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [prueba] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [prueba] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [prueba] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [prueba] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [prueba] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [prueba] SET  DISABLE_BROKER 
GO
ALTER DATABASE [prueba] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [prueba] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [prueba] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [prueba] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [prueba] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [prueba] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [prueba] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [prueba] SET RECOVERY FULL 
GO
ALTER DATABASE [prueba] SET  MULTI_USER 
GO
ALTER DATABASE [prueba] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [prueba] SET DB_CHAINING OFF 
GO
ALTER DATABASE [prueba] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [prueba] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [prueba] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [prueba] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'prueba', N'ON'
GO
ALTER DATABASE [prueba] SET QUERY_STORE = ON
GO
ALTER DATABASE [prueba] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [prueba]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 21/2/2025 12:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cedula] [varchar](10) NULL,
	[nombre] [varchar](100) NULL,
	[apellido] [varchar](100) NULL,
	[direccion] [varchar](150) NULL,
	[num_celular] [varchar](10) NULL,
	[fecha_nacimiento] [date] NULL,
	[estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 21/2/2025 12:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[detalle] [varchar](100) NULL,
	[total] [decimal](16, 2) NULL,
	[totalHoras] [int] NULL,
	[fechaEmision] [datetime] NULL,
	[fechaRetorno] [datetime] NULL,
	[id_vehiculo] [int] NULL,
	[id_cliente] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehiculo]    Script Date: 21/2/2025 12:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehiculo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[modelo] [varchar](100) NULL,
	[color] [varchar](50) NULL,
	[tarifaBase] [decimal](12, 6) NULL,
	[placa] [varchar](6) NULL,
	[estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cliente] ON 

INSERT [dbo].[Cliente] ([id], [cedula], [nombre], [apellido], [direccion], [num_celular], [fecha_nacimiento], [estado]) VALUES (1, N'0944131929', N'Jean', N'Intriago', N'Azuay y Guerrero Valenzuela', N'0944131929', CAST(N'2000-01-31' AS Date), 1)
INSERT [dbo].[Cliente] ([id], [cedula], [nombre], [apellido], [direccion], [num_celular], [fecha_nacimiento], [estado]) VALUES (2, N'0969363475', N'Jean', N'Franco', N'Samanes 2', N'0944131929', CAST(N'2000-02-01' AS Date), 0)
INSERT [dbo].[Cliente] ([id], [cedula], [nombre], [apellido], [direccion], [num_celular], [fecha_nacimiento], [estado]) VALUES (3, N'0994689256', N'Jean', N'Santana', N'Samanes 3', N'0944131929', CAST(N'1997-06-07' AS Date), 1)
INSERT [dbo].[Cliente] ([id], [cedula], [nombre], [apellido], [direccion], [num_celular], [fecha_nacimiento], [estado]) VALUES (4, N'0999999999', N'Franco', N'Santana', N'12 y Argentina', N'0944131929', CAST(N'1999-10-20' AS Date), 0)
INSERT [dbo].[Cliente] ([id], [cedula], [nombre], [apellido], [direccion], [num_celular], [fecha_nacimiento], [estado]) VALUES (6, N'0994876255', N'JAZMIN DAYANA', N'HERNANDEZ CASTRO', N'LA 12 Y ARGENTINA', N'0963254111', CAST(N'2001-02-04' AS Date), 1)
SET IDENTITY_INSERT [dbo].[Cliente] OFF
GO
SET IDENTITY_INSERT [dbo].[Factura] ON 

INSERT [dbo].[Factura] ([id], [detalle], [total], [totalHoras], [fechaEmision], [fechaRetorno], [id_vehiculo], [id_cliente]) VALUES (1, N'Alquiler de coche por 4 horas', CAST(75.90 AS Decimal(16, 2)), 3, CAST(N'2025-02-04T14:30:00.000' AS DateTime), CAST(N'2025-02-04T17:30:00.000' AS DateTime), 1, 1)
INSERT [dbo].[Factura] ([id], [detalle], [total], [totalHoras], [fechaEmision], [fechaRetorno], [id_vehiculo], [id_cliente]) VALUES (2, N'Carro para paseo', CAST(55.55 AS Decimal(16, 2)), 1, CAST(N'2025-02-04T14:30:00.000' AS DateTime), CAST(N'2025-02-04T17:30:00.000' AS DateTime), 3, 2)
INSERT [dbo].[Factura] ([id], [detalle], [total], [totalHoras], [fechaEmision], [fechaRetorno], [id_vehiculo], [id_cliente]) VALUES (3, N'Trabajo', CAST(213.00 AS Decimal(16, 2)), 6, CAST(N'2025-02-04T14:30:00.000' AS DateTime), CAST(N'2025-02-04T17:30:00.000' AS DateTime), 2, 2)
INSERT [dbo].[Factura] ([id], [detalle], [total], [totalHoras], [fechaEmision], [fechaRetorno], [id_vehiculo], [id_cliente]) VALUES (4, N'Trabajo', CAST(213.00 AS Decimal(16, 2)), 6, CAST(N'2025-02-04T14:30:00.000' AS DateTime), CAST(N'2025-02-04T17:30:00.000' AS DateTime), 2, 1)
INSERT [dbo].[Factura] ([id], [detalle], [total], [totalHoras], [fechaEmision], [fechaRetorno], [id_vehiculo], [id_cliente]) VALUES (6, NULL, CAST(25.30 AS Decimal(16, 2)), NULL, CAST(N'2025-02-10T10:24:55.360' AS DateTime), NULL, 1, 1)
INSERT [dbo].[Factura] ([id], [detalle], [total], [totalHoras], [fechaEmision], [fechaRetorno], [id_vehiculo], [id_cliente]) VALUES (7, NULL, CAST(35.50 AS Decimal(16, 2)), NULL, CAST(N'2025-02-10T10:27:52.973' AS DateTime), NULL, 2, 2)
INSERT [dbo].[Factura] ([id], [detalle], [total], [totalHoras], [fechaEmision], [fechaRetorno], [id_vehiculo], [id_cliente]) VALUES (8, NULL, CAST(35.50 AS Decimal(16, 2)), NULL, CAST(N'2025-02-10T10:29:00.557' AS DateTime), NULL, 2, 2)
INSERT [dbo].[Factura] ([id], [detalle], [total], [totalHoras], [fechaEmision], [fechaRetorno], [id_vehiculo], [id_cliente]) VALUES (9, NULL, CAST(35.50 AS Decimal(16, 2)), NULL, CAST(N'2025-02-10T10:35:14.447' AS DateTime), NULL, 2, 2)
INSERT [dbo].[Factura] ([id], [detalle], [total], [totalHoras], [fechaEmision], [fechaRetorno], [id_vehiculo], [id_cliente]) VALUES (10, NULL, CAST(55.55 AS Decimal(16, 2)), NULL, CAST(N'2025-02-10T10:48:14.010' AS DateTime), NULL, 3, 3)
INSERT [dbo].[Factura] ([id], [detalle], [total], [totalHoras], [fechaEmision], [fechaRetorno], [id_vehiculo], [id_cliente]) VALUES (11, NULL, CAST(55.55 AS Decimal(16, 2)), NULL, CAST(N'2025-02-10T10:56:21.147' AS DateTime), NULL, 3, 3)
INSERT [dbo].[Factura] ([id], [detalle], [total], [totalHoras], [fechaEmision], [fechaRetorno], [id_vehiculo], [id_cliente]) VALUES (12, NULL, CAST(35.50 AS Decimal(16, 2)), NULL, CAST(N'2025-02-10T11:02:05.267' AS DateTime), NULL, 2, 2)
INSERT [dbo].[Factura] ([id], [detalle], [total], [totalHoras], [fechaEmision], [fechaRetorno], [id_vehiculo], [id_cliente]) VALUES (13, NULL, CAST(35.50 AS Decimal(16, 2)), NULL, CAST(N'2025-02-10T11:08:48.337' AS DateTime), NULL, 2, 2)
INSERT [dbo].[Factura] ([id], [detalle], [total], [totalHoras], [fechaEmision], [fechaRetorno], [id_vehiculo], [id_cliente]) VALUES (14, NULL, CAST(35.50 AS Decimal(16, 2)), NULL, CAST(N'2025-02-10T11:50:42.763' AS DateTime), NULL, 2, 2)
SET IDENTITY_INSERT [dbo].[Factura] OFF
GO
SET IDENTITY_INSERT [dbo].[Vehiculo] ON 

INSERT [dbo].[Vehiculo] ([id], [modelo], [color], [tarifaBase], [placa], [estado]) VALUES (1, N'Audi A3 Sedan', N'rojo', CAST(25.300000 AS Decimal(12, 6)), N'GYE256', 1)
INSERT [dbo].[Vehiculo] ([id], [modelo], [color], [tarifaBase], [placa], [estado]) VALUES (2, N'Audi A8', N'verde', CAST(35.500000 AS Decimal(12, 6)), N'GYE277', 1)
INSERT [dbo].[Vehiculo] ([id], [modelo], [color], [tarifaBase], [placa], [estado]) VALUES (3, N'Audi e-tron', N'negro', CAST(55.550000 AS Decimal(12, 6)), N'FOE346', 0)
INSERT [dbo].[Vehiculo] ([id], [modelo], [color], [tarifaBase], [placa], [estado]) VALUES (4, N'audi A9', N'Blanco', CAST(35.000000 AS Decimal(12, 6)), N'CYS564', 0)
SET IDENTITY_INSERT [dbo].[Vehiculo] OFF
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([id_cliente])
REFERENCES [dbo].[Cliente] ([id])
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([id_vehiculo])
REFERENCES [dbo].[Vehiculo] ([id])
GO
/****** Object:  StoredProcedure [dbo].[Add_Vehiculo]    Script Date: 21/2/2025 12:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Add_Vehiculo] 
@modelo Varchar(100),
@color Varchar(50),
@tarifaBase decimal(12,6),
@placa Varchar(6),
@estado bit

AS
BEGIN

	Insert into Vehiculo(modelo, color, tarifaBase, placa, estado) 
	Values (@modelo, @color, @tarifaBase, @placa, @estado)
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteVehiculo]    Script Date: 21/2/2025 12:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteVehiculo]
@id int

AS
BEGIN
	UPDATE Vehiculo SET estado = 0
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[GetClienteCedula]    Script Date: 21/2/2025 12:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetClienteCedula]
	@cedula varchar(10)

AS
BEGIN
	Select cedula, nombre, apellido From Cliente where cedula = @cedula
END
GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceByIdentification]    Script Date: 21/2/2025 12:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		NN
-- Create date: 04/02/2025  15:20 PM
-- Description:	Obtener facturas asociadas a 
--              cliente por numero de identificacion
-- =============================================
CREATE PROCEDURE [dbo].[GetInvoiceByIdentification] -- EXEC GetInvoiceByIdentification @identificacion = '0944131929'
	@identificacion varchar(10)
AS
BEGIN

	SELECT
		c.cedula,
		CONCAT(c.nombre,' ',c.apellido) AS NombreCompleto,
		c.direccion,
		c.num_celular,
		(YEAR(GETDATE()) - YEAR(REPLACE(c.fecha_nacimiento,'-',''))) AS Edad_Actual,
		v.modelo,
		v.color,
		f.fechaEmision,
		f.fechaRetorno,
		TRY_CONVERT(DECIMAL(12,2),(v.tarifaBase * f.totalHoras)) as totalAPagar,
		f.total,
		CASE
			WHEN GETDATE() < f.fechaRetorno THEN
				'Ocupado'
			ELSE 'Culminado'
		END as EstadoAlquiler
	FROM Factura f
	INNER JOIN Cliente c ON c.id = f.id_cliente
	INNER JOIN Vehiculo v ON v.id = f.id_vehiculo
	WHERE c.cedula = @identificacion
END
GO
/****** Object:  StoredProcedure [dbo].[GetVehiculobyPlaca]    Script Date: 21/2/2025 12:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetVehiculobyPlaca]
@placa Varchar(6)

AS
BEGIN
 
	SELECT id ,modelo, color, tarifaBase, placa, estado From Vehiculo Where placa = @placa

END
GO
/****** Object:  StoredProcedure [dbo].[ingresarUsuario]    Script Date: 21/2/2025 12:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ingresarUsuario] 
	@cedula varchar(10),
	@nombre varchar(100),
	@apellido varchar(100),
	@direccion varchar(150),
	@num_celular varchar(10),
	@fecha_nacimiento varchar(8)
	

AS
BEGIN Try 
	
	INSERT INTO Cliente (cedula, nombre, apellido, direccion, num_celular, fecha_nacimiento, estado) 
	values (@cedula, @nombre, @apellido, @direccion, @num_celular, convert(date, @fecha_nacimiento), 1);
	commit;
END Try

Begin catch
	rollback
end catch
GO
/****** Object:  StoredProcedure [dbo].[InsertarFactura]    Script Date: 21/2/2025 12:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertarFactura]
    @idCliente INT,
    @idVehiculo INT,
    @FechaEmision DATETIME,
    @Total DECIMAL(16, 2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Factura (id_Cliente, id_Vehiculo, FechaEmision, Total)
    VALUES (@idCliente, @idVehiculo, @FechaEmision, @Total);
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_clientes]    Script Date: 21/2/2025 12:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Obtener_clientes]
	@id int
AS
BEGIN
	Select id, cedula, nombre, apellido, direccion, num_celular, fecha_nacimiento, estado  From Cliente Where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Todos_clientes]    Script Date: 21/2/2025 12:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Obtener_Todos_clientes]

AS
BEGIN
	Select id, cedula, nombre, apellido, direccion, num_celular, fecha_nacimiento, estado  From Cliente;
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Vehiculo]    Script Date: 21/2/2025 12:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Obtener_Vehiculo]
	@id int
AS
BEGIN
	Select id, modelo, color, tarifaBase, placa, estado  From Vehiculo Where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateVehiculo]    Script Date: 21/2/2025 12:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateVehiculo]
@id int,
@modelo VARCHAR(100),
@color VARCHAR(50),
@tarifaBase DECIMAL(12,6),
@placa VARCHAR(6),
@estado BIT
AS
BEGIN
	UPDATE Vehiculo SET modelo = @modelo, color = @color, tarifaBase = @tarifaBase, placa=@placa, estado = @estado
	WHERE id = @id
END
GO
USE [master]
GO
ALTER DATABASE [prueba] SET  READ_WRITE 
GO
