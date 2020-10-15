CREATE DATABASE [Andreani]
GO
USE [Andreani]
GO
CREATE TABLE [dbo].[Address](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Calle] [nvarchar](max) NOT NULL,
	[Numero] [int] NOT NULL,
	[Ciudad] [nvarchar](max) NOT NULL,
	[Codigo_Postal] [nvarchar](max) NOT NULL,
	[Provincia] [nvarchar](max) NOT NULL,
	[Pais] [nvarchar](max) NOT NULL,
	PRIMARY KEY (ID)
)

GO

CREATE TABLE [dbo].[GeocodedAddress](
	[Id] [int] NOT NULL,
	[Latitude] [nvarchar](max) NOT NULL,
	[Longitude] [nvarchar](max) NOT NULL,,
	PRIMARY KEY (ID)
)

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Mariano Brayotta>
-- Create date: <Create 9,10,2020>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertAddress] 
	 @calle VARCHAR(MAX)
	,@numero INT
    ,@ciudad VARCHAR(MAX)
	,@codigo_postal VARCHAR(MAX)
	,@provincia VARCHAR(MAX)
	,@pais VARCHAR(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO [Address] (Calle, Numero, Ciudad, Codigo_Postal, Provincia, Pais)
	VALUES (@calle, @numero, @ciudad, @codigo_postal, @provincia, @pais)

	SELECT @@IDENTITY AS ID
END
GO

CREATE PROCEDURE [dbo].[InsertGeocodedAddress] 
	 @id INT
    ,@latitude VARCHAR(MAX)
	,@longitude VARCHAR(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO [GeocodedAddress] (Id, Latitude, Longitude)
	VALUES (@id, @latitude, @longitude)
END
GO

CREATE PROCEDURE [dbo].[GetAddressById] 
	 @id INT
AS
BEGIN
SET NOCOUNT ON;

	SELECT * FROM [GeocodedAddress]
	WHERE Id = @id
END
GO
