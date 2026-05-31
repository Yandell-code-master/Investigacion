CREATE DATABASE InvestigacionNetAngular;
GO

USE InvestigacionNetAngular;
GO

-- 2. Crear una única tabla de Productos independiente
CREATE TABLE Productos (
    CodigoInterno VARCHAR(20) NOT NULL,
    CodigoBarra   VARCHAR(50) NULL,
    Descripcion   VARCHAR(100) NOT NULL,
    PrecioVenta   DECIMAL(18,2) NOT NULL,
    Existencia    INT NOT NULL,
    PRIMARY KEY (CodigoInterno)
);
GO

CREATE TABLE Clientes (
    id INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(150) NOT NULL,
    
    -- Definición de la Clave Primaria
    CONSTRAINT PK_Clientes PRIMARY KEY (id)
);
GO

-- 2. Crear la tabla de Facturas (Entidad relación/intermediaria)
CREATE TABLE Facturas (
    id INT IDENTITY(1,1) NOT NULL, -- ID propio para facilitar búsquedas rápidos
    ClienteId INT NOT NULL,        -- Relación con la tabla Clientes
    FechaEmision DATETIME NOT NULL DEFAULT GETDATE(),
    Total DECIMAL(18,2) NOT NULL DEFAULT 0.00,
    
    -- Definición de la Clave Primaria
    CONSTRAINT PK_Facturas PRIMARY KEY (id),
    
    -- Definición de la Clave Foránea (Relación con Cliente)
    CONSTRAINT FK_Facturas_Clientes FOREIGN KEY (ClienteId) 
        REFERENCES Clientes(id) 
        ON DELETE CASCADE -- Si se borra un cliente, se borran sus facturas (opcional)
);
GO

CREATE TABLE DetalleFacturas (
    id INT IDENTITY(1,1) NOT NULL,
    FacturaId INT NOT NULL,              -- Enlace a la cabecera (Tabla Facturas)
    CodigoInterno VARCHAR(20) NOT NULL,  -- Enlace al producto comprado (Tabla Productos)
    Cantidad INT NOT NULL CONSTRAINT DF_DetalleFacturas_Cantidad DEFAULT 1,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    SubTotal AS (Cantidad * PrecioUnitario), -- Columna calculada automática (¡punto extra para la demo!)

    -- Clave Primaria de la tabla detalle
    CONSTRAINT PK_DetalleFacturas PRIMARY KEY (id),

    -- Relación con la tabla Facturas (Si se borra la factura, se borran sus detalles)
    CONSTRAINT FK_DetalleFacturas_Facturas FOREIGN KEY (FacturaId) 
        REFERENCES Facturas(id) 
        ON DELETE CASCADE,

    -- Relación con la tabla Productos (Asumiendo que CodigoInterno es la llave primaria en Productos)
    CONSTRAINT FK_DetalleFacturas_Productos FOREIGN KEY (CodigoInterno) 
        REFERENCES Productos(CodigoInterno)
);
GO

-- 3. Insertar unos datos de prueba iniciales para que la API tenga qué mostrar
INSERT INTO Productos (CodigoInterno, CodigoBarra, Descripcion, PrecioVenta, Existencia)
VALUES 
('PROD01', '74410011', 'Hamburguesa BigFOOD', 3500.00, 50),
('PROD02', '74410022', 'Papas Fritas Medianas', 1500.00, 100),
('PROD03', '74410033', 'Refresco Gaseoso 500ml', 1200.00, 75);
GO

INSERT INTO Clientes (Nombre)
VALUES 
('Carlos Mendoza'),
('Ana María López'),
('Sofía Rodríguez');
GO

-- 2. Insertar datos de prueba en la tabla Facturas
-- Nota: Como usamos IDENTITY(1,1), asumimos que los IDs de los clientes creados arriba son 1, 2 y 3.
INSERT INTO Facturas (ClienteId, FechaEmision, Total)
VALUES 
(1, GETDATE(), 5000.00), -- Carlos compró una Hamburguesa (3500) y Papas (1500)
(2, GETDATE(), 1200.00), -- Ana compró solo un Refresco (1200)
(3, GETDATE(), 9700.00), -- Sofía compró dos Hamburguesas (7000) y un Refresco (1200) y Papas (1500)
(1, GETDATE(), 1500.00); -- Carlos regresó otro día a comprar Papas Fritas (1500)
GO

