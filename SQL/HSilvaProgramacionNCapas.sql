USE [master]
GO
/****** Object:  Database [HSilvaProgramacionNCapas]    Script Date: 12/6/2022 1:42:47 PM ******/
CREATE DATABASE [HSilvaProgramacionNCapas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HSilvaProgramacionNCapas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\HSilvaProgramacionNCapas.mdf' , SIZE = 5184KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'HSilvaProgramacionNCapas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\HSilvaProgramacionNCapas_log.ldf' , SIZE = 1856KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HSilvaProgramacionNCapas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET ARITHABORT OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET RECOVERY FULL 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET  MULTI_USER 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'HSilvaProgramacionNCapas', N'ON'
GO
USE [HSilvaProgramacionNCapas]
GO
/****** Object:  StoredProcedure [dbo].[AreaGetAll]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AreaGetAll]
AS
SELECT [IdArea]
      ,[Nombre]
  FROM [Area]

GO
/****** Object:  StoredProcedure [dbo].[ColoniaGetByIdMunicipio]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[ColoniaGetByIdMunicipio] 
  @IdMunicipio INT
  AS
  SELECT [IdColonia]
        ,[Nombre]
		,[CodigoPostal]
        ,[IdMunicipio]
  FROM [Colonia]
  WHERE IdMunicipio = @IdMunicipio
GO
/****** Object:  StoredProcedure [dbo].[DepartamentoAdd]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DepartamentoAdd]
@Nombre VARCHAR(100),
@IdArea INT
AS
INSERT INTO [Departamento]
           ([Nombre],
		   [IdArea])

     VALUES
           (@Nombre,
		   @IdArea)
		  
GO
/****** Object:  StoredProcedure [dbo].[DepartamentoDelete]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DepartamentoDelete]
@IdDepartamento INT
AS
DELETE FROM [Departamento] 
WHERE IdDepartamento=@IdDepartamento
GO
/****** Object:  StoredProcedure [dbo].[DepartamentoGetAll]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DepartamentoGetAll]
AS
SELECT Departamento.[IdDepartamento],
      Departamento.[Nombre],
      Departamento.[IdArea],
	  Area.[Nombre] AS NombreArea
	  
  FROM [Departamento]
  INNER JOIN Area ON Departamento.IdArea = Area.IdArea
GO
/****** Object:  StoredProcedure [dbo].[DepartamentoGetById]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DepartamentoGetById]
@IdDepartamento INT
AS 
SELECT [IdDepartamento]
      ,Departamento.[Nombre]
	  ,Departamento.[IdArea]
	  ,Area.[Nombre] AS NombreArea

  FROM [Departamento]
  INNER JOIN Area ON Departamento.IdArea = Area.IdArea

  WHERE IdDepartamento= @IdDepartamento
GO
/****** Object:  StoredProcedure [dbo].[DepartamentoUpdate]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DepartamentoUpdate]
@IdDepartamento INT,
@Nombre VARCHAR(100),
@IdArea INT
AS
UPDATE [Departamento]
   SET [Nombre] = @Nombre,
       [IdArea] = @IdArea
 WHERE   [IdDepartamento] = @IdDepartamento
GO
/****** Object:  StoredProcedure [dbo].[EstadoGetByIdPais]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EstadoGetByIdPais] 
@IdPais INT
AS
SELECT [IdEstado]
      ,[Nombre]
      ,[IdPais]
  FROM [Estado]
  WHERE IdPais = @IdPais
GO
/****** Object:  StoredProcedure [dbo].[MunicipioGetByIdEstado]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MunicipioGetByIdEstado] 
@IdEstado INT
AS
SELECT [IdMunicipio]
      ,[Nombre]
      ,[IdEstado]
  FROM [Municipio]
  WHERE IdEstado = @IdEstado
GO
/****** Object:  StoredProcedure [dbo].[PaisGetAll]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PaisGetAll]
AS
SELECT [IdPais]
      ,[Nombre]
  FROM [Pais]
GO
/****** Object:  StoredProcedure [dbo].[ProductoAdd]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductoAdd]
@Nombre VARCHAR(200),
@PrecioUnitario DECIMAL(18,2),
@Stock INT,
@Descripcion VARCHAR(500),
@Imagen VARCHAR(MAX)
AS
INSERT INTO [Producto]
           ([Nombre]
           ,[PrecioUnitario]
           ,[Stock]
           ,[Descripcion]
		   ,[Imagen])
     VALUES
           (@Nombre
           ,@PrecioUnitario
           ,@Stock
           ,@Descripcion
		   ,@Imagen)

GO
/****** Object:  StoredProcedure [dbo].[ProductoGetAll]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductoGetAll]
@Nombre VARCHAR(50),
@IdDepartamento INT
AS
IF(@IdDepartamento > 0)
   BEGIN
SELECT Producto.[IdProducto]
      ,Producto.[Nombre]
      ,Producto.[PrecioUnitario]
      ,Producto.[Stock]
      ,Producto.[IdProveedor]
	  ,Proveedor.Nombre AS NombreProveedor
	  ,Proveedor.Telefono
      ,Producto.[IdDepartamento]
	  ,Departamento.Nombre AS NombreDepartamento
	  ,Departamento.IdArea
	  ,Area.Nombre AS NombreArea
      ,Producto.[Descripcion]
	  ,Producto.[Imagen]
  FROM Producto
        INNER JOIN Proveedor ON Producto.IdProveedor = Proveedor.IdProveedor
		INNER JOIN Departamento ON Producto.IdDepartamento = Departamento.IdDepartamento
		INNER JOIN Area ON Departamento.IdArea = Area.IdArea
		WHERE Producto.Nombre LIKE '%' + @Nombre + '%' AND Departamento.IdDepartamento = @IdDepartamento 
	END
	ELSE
	BEGIN
	SELECT Producto.[IdProducto]
      ,Producto.[Nombre]
      ,Producto.[PrecioUnitario]
      ,Producto.[Stock]
      ,Producto.[IdProveedor]
	  ,Proveedor.Nombre AS NombreProveedor
	  ,Proveedor.Telefono
      ,Producto.[IdDepartamento]
	  ,Departamento.Nombre AS NombreDepartamento
	  ,Departamento.IdArea
	  ,Area.Nombre AS NombreArea
      ,Producto.[Descripcion]
	  ,Producto.[Imagen]
  FROM Producto
        INNER JOIN Proveedor ON Producto.IdProveedor = Proveedor.IdProveedor
		INNER JOIN Departamento ON Producto.IdDepartamento = Departamento.IdDepartamento
		INNER JOIN Area ON Departamento.IdArea = Area.IdArea
		WHERE Producto.Nombre LIKE '%' + @Nombre + '%' OR Departamento.IdDepartamento = @IdDepartamento 
		END
GO
/****** Object:  StoredProcedure [dbo].[ProductoGetById]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductoGetById]
@IdProducto INT
AS
SELECT Producto.[IdProducto]
      ,Producto.[Nombre]
      ,Producto.[PrecioUnitario]
      ,Producto.[Stock]
      ,Producto.[IdProveedor]
	  ,Proveedor.Nombre AS NombreProveedor
	  ,Proveedor.Telefono
      ,Producto.[IdDepartamento]
	  ,Departamento.Nombre AS NombreDepartamento
	  ,Departamento.IdArea
	  ,Area.Nombre AS NombreArea
      ,Producto.[Descripcion]
	  ,Producto.[Imagen]
  FROM Producto
        INNER JOIN Proveedor ON Producto.IdProveedor = Proveedor.IdProveedor
		INNER JOIN Departamento ON Producto.IdDepartamento = Departamento.IdDepartamento
		INNER JOIN Area ON Departamento.IdArea = Area.IdArea
		WHERE IdProducto = @IdProducto
GO
/****** Object:  StoredProcedure [dbo].[ProductoUpdate]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductoUpdate]
@IdProducto INT,
@Nombre VARCHAR(200),
@PrecioUnitario DECIMAL(18,2),
@Stock INT,
@Descripcion VARCHAR(500),
@Imagen VARCHAR(MAX)
AS
UPDATE Producto SET  
            [Nombre] = @Nombre
           ,[PrecioUnitario] = @PrecioUnitario
           ,[Stock] = @Stock
           ,[Descripcion] = @Descripcion
		   ,[Imagen] = @Imagen

		   WHERE Producto.IdProducto = @IdProducto
GO
/****** Object:  StoredProcedure [dbo].[ProveedorGetAll]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProveedorGetAll]
AS
SELECT [IdProveedor]
      ,[Nombre]
      ,[Telefono]
  FROM [Proveedor]
GO
/****** Object:  StoredProcedure [dbo].[RolGetAll]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RolGetAll]
AS
SELECT [IdRol]
      ,[Nombre]
  FROM [Rol]
GO
/****** Object:  StoredProcedure [dbo].[UsuarioAdd]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[UsuarioAdd]
           @Nombre VARCHAR(50),
           @ApellidoPaterno VARCHAR(50),
           @ApellidoMaterno VARCHAR(50), 
           @FechaNacimiento VARCHAR(20),
           @Genero CHAR(1),
		   @UserName VARCHAR(50),
		   @Email VARCHAR(254),
		   @Password VARCHAR(50),
		   @Telefono VARCHAR(20),
		   @Celular VARCHAR(20),
		   @CURP VARCHAR(50),
		   @IdRol INT, 
		   @Imagen VARCHAR(MAX),
		   @Calle VARCHAR(50),
		   @NumeroExterior VARCHAR(20),
		   @NumeroIxterior VARCHAR(20),
		   @IdColonia INT  		    
AS
INSERT INTO [Usuario]
           ([Nombre]
           ,[ApellidoPaterno]
           ,[ApellidoMaterno]
           ,[FechaNacimiento]
           ,[Genero]
		   ,[UserName]
		   ,[Email]
		   ,[Password]
		   ,[Telefono]
		   ,[Celular]
		   ,[CURP]
		   ,[IdRol]
		   ,[Imagen]
		   ,[Status])
     VALUES
           (@Nombre, 
           @ApellidoPaterno,
           @ApellidoMaterno, 
           CONVERT (DATE, @FechaNacimiento, 105),
           @Genero,
		   @UserName,
		   @Email,
		   @Password,
		   @Telefono,
		   @Celular,
		   @CURP,
		   @IdRol,
		   @Imagen,
		   1)

	INSERT INTO[Direccion]
           ([Calle]
           ,[NumeroExterior]
           ,[NumeroIxterior]
           ,[IdColonia]
           ,[IdUsuario])
     VALUES
           (@Calle
           ,@NumeroExterior
           ,@NumeroIxterior
           ,@IdColonia
           ,@@IDENTITY)
GO
/****** Object:  StoredProcedure [dbo].[UsuarioChangeStatus]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioChangeStatus]
@IdUsuario INT,
@Status BIT
AS
UPDATE Usuario SET Status = @Status
WHERE IdUsuario = @IdUsuario
GO
/****** Object:  StoredProcedure [dbo].[UsuarioDelete]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioDelete]
@IdUsuario INT
AS
DELETE FROM Direccion
WHERE IdUsuario = @IdUsuario

DELETE FROM [Usuario] 
WHERE IdUsuario=@IdUsuario
GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetAll]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[UsuarioGetAll]
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50)
  AS SELECT Usuario.[IdUsuario]
      ,Usuario.[Nombre]
      ,Usuario.[ApellidoPaterno]
      ,Usuario.[ApellidoMaterno]
      ,Usuario.[FechaNacimiento]
      ,Usuario.[Genero]
	  ,Usuario.[UserName]
	  ,Usuario.[Email]
	  ,Usuario.[Password]
	  ,Usuario.[Telefono]
	  ,Usuario.[Celular]
	  ,Usuario.[CURP]
	  ,Usuario.[IdRol]
	  ,Usuario.[Imagen]
	  ,Usuario.[Status]
	  ,Rol.[Nombre] AS NombreRol
	  ,Direccion.IdDireccion
	  ,Direccion.Calle
	  ,Direccion.NumeroExterior
	  ,Direccion.NumeroIxterior
	  ,Direccion.IdColonia
	  ,Colonia.Nombre AS NombreColonia
	  ,Colonia.CodigoPostal
	  ,Colonia.IdMunicipio
	  ,Municipio.Nombre AS NombreMunicipio
	  ,Municipio.IdEstado
	  ,Estado.Nombre AS NumbreEstado
	  ,Estado.IdPais
	  ,Pais.Nombre AS PaisNombre
  FROM Usuario
  INNER JOIN Rol ON Usuario.IdRol = Rol.IdRol
  INNER JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario
  INNER JOIN Colonia ON Colonia.IdColonia = Direccion.IdColonia
  INNER JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio 
  INNER JOIN Estado ON Municipio.IdEstado = Estado.IdEstado
  INNER JOIN Pais ON Estado.IdPais = Pais.IdPais

  WHERE Usuario.Nombre LIKE '%' + @Nombre + '%' AND ApellidoPaterno LIKE '%' + @ApellidoPaterno + '%'
GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetById]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioGetById]
 @IdUsuario INT
 AS
SELECT Usuario.[IdUsuario]
      ,Usuario.[Nombre]
      ,Usuario.[ApellidoPaterno]
      ,Usuario.[ApellidoMaterno]
      ,Usuario.[FechaNacimiento]
      ,Usuario.[Genero]
	  ,Usuario.[UserName]
	  ,Usuario.[Email]
	  ,Usuario.[Password]
	  ,Usuario.[Telefono]
	  ,Usuario.[Celular]
	  ,Usuario.[CURP]
	  ,Usuario.[IdRol]
	  ,Usuario.[Imagen]
	  ,Usuario.[Status]
	  ,Rol.[Nombre] AS NombreRol
	  ,Direccion.IdDireccion
	  ,Direccion.Calle
	  ,Direccion.NumeroExterior
	  ,Direccion.NumeroIxterior
	  ,Direccion.IdColonia
	  ,Colonia.Nombre AS NombreColonia
	  ,Colonia.CodigoPostal
	  ,Colonia.IdMunicipio
	  ,Municipio.Nombre AS NombreMunicipio
	  ,Municipio.IdEstado
	  ,Estado.Nombre AS NumbreEstado
	  ,Estado.IdPais
	  ,Pais.Nombre AS PaisNombre

  FROM Usuario
  INNER JOIN Rol ON Usuario.IdRol = Rol.IdRol
  INNER JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario
  INNER JOIN Colonia ON Colonia.IdColonia = Direccion.IdColonia
  INNER JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio 
  INNER JOIN Estado ON Municipio.IdEstado = Estado.IdEstado
  INNER JOIN Pais ON Estado.IdPais = Pais.IdPais
   
   WHERE Usuario.[IdUsuario]= @IdUsuario
GO
/****** Object:  StoredProcedure [dbo].[UsuarioUpdate]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioUpdate]
@IdUsuario INT,
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@FechaNacimiento VARCHAR(20),
@Genero CHAR(1),
@UserName VARCHAR(50),
@Email VARCHAR(254),
@Password VARCHAR(50),
@Telefono VARCHAR(20),
@Celular VARCHAR(20),
@CURP VARCHAR(50),
@IdRol INT,
@Imagen VARCHAR(MAX),
@Calle VARCHAR(50),
@NumeroExterior VARCHAR(20),
@NumeroIxterior VARCHAR(20),
@IdColonia INT
 
AS
UPDATE [Usuario]
   SET [Nombre] = @Nombre
      ,[ApellidoPaterno] = @ApellidoPaterno
	  ,[ApellidoMaterno] = @ApellidoMaterno
      ,[FechaNacimiento] = CONVERT (DATE, @FechaNacimiento, 105)
      ,[Genero] = @Genero
	  ,[UserName] = @UserName
	  ,[Email] = @Email
	  ,[Password] = @Password
	  ,[Telefono] = @Telefono
	  ,[Celular] = @Celular
	  ,[CURP] = @CURP
	  ,[IdRol] = @IdRol 
	  ,[Imagen] = @Imagen
	  WHERE Usuario.[IdUsuario] = @IdUsuario

UPDATE [Direccion] SET
           [Calle] = @Calle
           ,[NumeroExterior] = @NumeroExterior
           ,[NumeroIxterior] = @NumeroIxterior
           ,[IdColonia] = @IdColonia  

	  WHERE Direccion.[IdUsuario] = @IdUsuario 
GO
/****** Object:  Table [dbo].[Area]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Area](
	[IdArea] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdArea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Colonia]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Colonia](
	[IdColonia] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[CodigoPostal] [varchar](50) NOT NULL,
	[IdMunicipio] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdColonia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Departamento]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Departamento](
	[IdDepartamento] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[IdArea] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDepartamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Direccion]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Direccion](
	[IdDireccion] [int] IDENTITY(1,1) NOT NULL,
	[Calle] [varchar](50) NOT NULL,
	[NumeroExterior] [varchar](20) NULL,
	[NumeroIxterior] [varchar](20) NOT NULL,
	[IdColonia] [int] NULL,
	[IdUsuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDireccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Estado]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Estado](
	[IdEstado] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[IdPais] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Municipio]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Municipio](
	[IdMunicipio] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[IdEstado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMunicipio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pais]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pais](
	[IdPais] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPais] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Producto](
	[IdProducto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[PrecioUnitario] [decimal](18, 2) NOT NULL,
	[Stock] [int] NOT NULL,
	[IdProveedor] [int] NULL,
	[IdDepartamento] [int] NULL,
	[Descripcion] [varchar](500) NULL,
	[Imagen] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Proveedor](
	[IdProveedor] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Telefono] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 12/6/2022 1:42:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[ApellidoPaterno] [varchar](50) NULL,
	[ApellidoMaterno] [varchar](50) NULL,
	[FechaNacimiento] [date] NOT NULL,
	[Genero] [char](1) NULL,
	[UserName] [varchar](50) NOT NULL,
	[Email] [varchar](254) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Telefono] [varchar](20) NOT NULL,
	[Celular] [varchar](20) NULL,
	[CURP] [varchar](50) NULL,
	[IdRol] [int] NULL,
	[Imagen] [varchar](max) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Usuario_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Usuario_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Area] ADD  DEFAULT ('') FOR [Nombre]
GO
ALTER TABLE [dbo].[Departamento] ADD  DEFAULT ('') FOR [Nombre]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [Usuario__UserName]  DEFAULT ('UsuarioN') FOR [UserName]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [Usuario__Email]  DEFAULT ('UsuarioN@gmail.com') FOR [Email]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [Usuario__Passwor]  DEFAULT ('*****') FOR [Password]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [Usuario__Telefono]  DEFAULT ('55555555') FOR [Telefono]
GO
ALTER TABLE [dbo].[Colonia]  WITH CHECK ADD  CONSTRAINT [Fk_ColoniaMunicipio] FOREIGN KEY([IdMunicipio])
REFERENCES [dbo].[Municipio] ([IdMunicipio])
GO
ALTER TABLE [dbo].[Colonia] CHECK CONSTRAINT [Fk_ColoniaMunicipio]
GO
ALTER TABLE [dbo].[Departamento]  WITH CHECK ADD  CONSTRAINT [Fk_Area] FOREIGN KEY([IdArea])
REFERENCES [dbo].[Area] ([IdArea])
GO
ALTER TABLE [dbo].[Departamento] CHECK CONSTRAINT [Fk_Area]
GO
ALTER TABLE [dbo].[Direccion]  WITH CHECK ADD  CONSTRAINT [Fk_DireccionColonia] FOREIGN KEY([IdColonia])
REFERENCES [dbo].[Colonia] ([IdColonia])
GO
ALTER TABLE [dbo].[Direccion] CHECK CONSTRAINT [Fk_DireccionColonia]
GO
ALTER TABLE [dbo].[Direccion]  WITH CHECK ADD  CONSTRAINT [Fk_DireccionUsuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Direccion] CHECK CONSTRAINT [Fk_DireccionUsuario]
GO
ALTER TABLE [dbo].[Estado]  WITH CHECK ADD  CONSTRAINT [Fk_EstadoPais] FOREIGN KEY([IdPais])
REFERENCES [dbo].[Pais] ([IdPais])
GO
ALTER TABLE [dbo].[Estado] CHECK CONSTRAINT [Fk_EstadoPais]
GO
ALTER TABLE [dbo].[Municipio]  WITH CHECK ADD  CONSTRAINT [Fk_MunicipioEstado] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Municipio] CHECK CONSTRAINT [Fk_MunicipioEstado]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD FOREIGN KEY([IdDepartamento])
REFERENCES [dbo].[Departamento] ([IdDepartamento])
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[Proveedor] ([IdProveedor])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [Fk_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [Fk_Rol]
GO
USE [master]
GO
ALTER DATABASE [HSilvaProgramacionNCapas] SET  READ_WRITE 
GO
