# SQL Stored Procedures Documentation

## Client Management
- `sp_InsertarCliente`: Creates new client with business name, type, creation date and RFC
- `sp_ActualizarCliente`: Updates existing client information
- `sp_EliminarCliente`: Removes client by ID
- `sp_ObtenerClientes`: Retrieves all clients

## Product Management
- `sp_InsertarProducto`: Creates new product with name, image, price and extension
- `sp_ActualizarProducto`: Updates existing product information
- `sp_EliminarProducto`: Removes product by ID
- `sp_ObtenerProductos`: Retrieves all products

## Client Type Management
- `sp_InsertarTipoCliente`: Creates new client type
- `sp_ActualizarTipoCliente`: Updates existing client type
- `sp_EliminarTipoCliente`: Removes client type by ID
- `sp_ObtenerTiposClientes`: Retrieves all client types

## Invoice Management
- `sp_CreateFactura`: Creates new invoice with:
  - Automatic calculation of subtotal, taxes (19%), and total
  - Product details storage
  - Returns invoice ID and confirmation message
- `sp_BuscarFactura`: Searches invoices by:
  - Client name
  - Invoice number
  
## Database Schema
The procedures interact with the following tables:
- `TblClientes`: Client information
- `CatProductos`: Product catalog
- `CatTipoCliente`: Client types
- `TblFacturas`: Invoices
- `TblDetallesFactura`: Invoice details

## Common Parameters
- `@Id`: Primary key for updates/deletes
- `@RazonSocial`: Business name
- `@RFC`: Tax ID
- `@NombreProducto`: Product name
- `@PrecioUnitario`: Unit price
- `@FechaCreacion`: Creation date

USE [LabDev]
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarCliente]    Script Date: 27/12/2024 11:28:32 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_ActualizarCliente]
    @Id INT,
    @RazonSocial VARCHAR(200),
    @IdTipoCliente INT,
    @FechaCreacion DATE,
    @RFC VARCHAR(50)
AS
BEGIN
    UPDATE dbo.TblClientes
    SET RazonSocial = @RazonSocial,
        IdTipoCliente = @IdTipoCliente,
        FechaCreacion = @FechaCreacion,
        RFC = @RFC
    WHERE Id = @Id
END

USE [LabDev]
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarProducto]    Script Date: 27/12/2024 11:28:51 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_ActualizarProducto]
    @Id INT,
    @NombreProducto VARCHAR(50),
    @ImagenProducto IMAGE = NULL,
    @PrecioUnitario DECIMAL(18, 2),
    @ext VARCHAR(5) = NULL
AS
BEGIN
    UPDATE dbo.CatProductos
    SET NombreProducto = @NombreProducto,
        ImagenProducto = @ImagenProducto,
        PrecioUnitario = @PrecioUnitario,
        ext = @ext
    WHERE Id = @Id
END

USE [LabDev]
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarTipoCliente]    Script Date: 27/12/2024 11:29:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_ActualizarTipoCliente]
    @Id INT,
    @TipoCliente VARCHAR(50)
AS
BEGIN
    UPDATE dbo.CatTipoCliente
    SET TipoCliente = @TipoCliente
    WHERE Id = @Id
END

USE [LabDev]
GO
/****** Object:  StoredProcedure [dbo].[sp_BuscarFactura]    Script Date: 27/12/2024 11:29:10 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_BuscarFactura]
    @TipoBusqueda NVARCHAR(20),  -- Puede ser 'Cliente' o 'NumeroFactura'
    @ValorBusqueda NVARCHAR(50)  -- El valor del cliente o número de factura
AS
BEGIN
    IF @TipoBusqueda = 'Cliente'
    BEGIN
        -- Buscar facturas por cliente
        SELECT f.Id, f.FechaEmisionFactura, f.NumeroFactura, f.SubTotalFacturas, f.TotalImpuestos, f.TotalFactura
        FROM [dbo].[TblFacturas] f
        INNER JOIN [dbo].[TblClientes] c ON f.IdCliente = c.Id
        WHERE c.RazonSocial LIKE '%' + @ValorBusqueda + '%'
    END
    ELSE IF @TipoBusqueda = 'NumeroFactura'
    BEGIN
        -- Buscar facturas por número de factura
        SELECT f.Id, f.FechaEmisionFactura, f.NumeroFactura, f.SubTotalFacturas, f.TotalImpuestos, f.TotalFactura
        FROM [dbo].[TblFacturas] f
        WHERE f.NumeroFactura = @ValorBusqueda
    END
    ELSE
    BEGIN
        SELECT 'Tipo de búsqueda no válido' AS Message
    END
END

USE [LabDev]
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateFactura]    Script Date: 27/12/2024 11:29:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_CreateFactura]
    @NumeroFactura INT,
    @IdCliente INT,
    @Productos NVARCHAR(MAX),
    @FechaEmision DATETIME
AS
BEGIN
    DECLARE @Subtotal DECIMAL(18,2) = 0
    DECLARE @TotalImpuestos DECIMAL(18,2) = 0
    DECLARE @TotalFactura DECIMAL(18,2) = 0
    DECLARE @IdFactura INT

 
    SET @Subtotal = (SELECT SUM(p.PrecioUnitario * p.Cantidad)
                     FROM OPENJSON(@Productos)
                     WITH (ProductoId INT, PrecioUnitario DECIMAL(18,2), Cantidad INT) p)
    
    SET @TotalImpuestos = @Subtotal * 0.19
    SET @TotalFactura = @Subtotal + @TotalImpuestos

    INSERT INTO [dbo].[TblFacturas]
        ([FechaEmisionFactura], [IdCliente], [NumeroFactura], [NumeroTotalArticulos], [SubTotalFacturas], [TotalImpuestos], [TotalFactura])
    VALUES
        (@FechaEmision, @IdCliente, @NumeroFactura, (SELECT COUNT(*) FROM OPENJSON(@Productos)), @Subtotal, @TotalImpuestos, @TotalFactura)

    SET @IdFactura = SCOPE_IDENTITY()

    INSERT INTO [dbo].[TblDetallesFactura]
        ([IdFactura], [IdProducto], [CantidadDeProducto], [PrecioUnitarioProducto], [SubtotalProducto])
    SELECT
        @IdFactura,
        p.ProductoId,
        p.Cantidad,
        p.PrecioUnitario,
        p.PrecioUnitario * p.Cantidad
    FROM OPENJSON(@Productos)
    WITH (ProductoId INT, PrecioUnitario DECIMAL(18,2), Cantidad INT) p

    SELECT 'Factura Creada Exitosamente' AS Message, @IdFactura AS FacturaId
END

USE [LabDev]
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarCliente]    Script Date: 27/12/2024 11:29:32 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_EliminarCliente]
    @Id INT
AS
BEGIN
    DELETE FROM dbo.TblClientes
    WHERE Id = @Id
END

USE [LabDev]
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarProducto]    Script Date: 27/12/2024 11:29:47 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_EliminarProducto]
    @Id INT
AS
BEGIN
    DELETE FROM dbo.CatProductos
    WHERE Id = @Id
END

USE [LabDev]
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarTipoCliente]    Script Date: 27/12/2024 11:30:04 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_EliminarTipoCliente]
    @Id INT
AS
BEGIN
    DELETE FROM dbo.CatTipoCliente
    WHERE Id = @Id
END

USE [LabDev]
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarCliente]    Script Date: 27/12/2024 11:30:12 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_InsertarCliente]
    @RazonSocial VARCHAR(200),
    @IdTipoCliente INT,
    @FechaCreacion DATE,
    @RFC VARCHAR(50)
AS
BEGIN
    INSERT INTO dbo.TblClientes (RazonSocial, IdTipoCliente, FechaCreacion, RFC)
    VALUES (@RazonSocial, @IdTipoCliente, @FechaCreacion, @RFC)
END

USE [LabDev]
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarProducto]    Script Date: 27/12/2024 11:30:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_InsertarProducto]
    @NombreProducto VARCHAR(50),
    @ImagenProducto IMAGE = NULL,
    @PrecioUnitario DECIMAL(18, 2),
    @ext VARCHAR(5) = NULL
AS
BEGIN
    INSERT INTO dbo.CatProductos (NombreProducto, ImagenProducto, PrecioUnitario, ext)
    VALUES (@NombreProducto, @ImagenProducto, @PrecioUnitario, @ext)
END

USE [LabDev]
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarTipoCliente]    Script Date: 27/12/2024 11:30:28 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_InsertarTipoCliente]
    @TipoCliente VARCHAR(50)
AS
BEGIN
    INSERT INTO dbo.CatTipoCliente (TipoCliente)
    VALUES (@TipoCliente)
END

USE [LabDev]
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerClientes]    Script Date: 27/12/2024 11:30:36 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_ObtenerClientes]
AS
BEGIN
    SELECT Id, RazonSocial, IdTipoCliente, FechaCreacion, RFC
    FROM dbo.TblClientes
END

USE [LabDev]
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerProductos]    Script Date: 27/12/2024 11:30:42 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_ObtenerProductos]
AS
BEGIN
    SELECT Id, NombreProducto, PrecioUnitario, ImagenProducto, ext
    FROM dbo.CatProductos
END

USE [LabDev]
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerTiposClientes]    Script Date: 27/12/2024 11:30:49 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_ObtenerTiposClientes]
AS
BEGIN
    SELECT Id, TipoCliente
    FROM dbo.CatTipoCliente
END