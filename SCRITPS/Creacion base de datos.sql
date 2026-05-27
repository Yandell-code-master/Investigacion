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

-- 3. Insertar unos datos de prueba iniciales para que la API tenga qué mostrar
INSERT INTO Productos (CodigoInterno, CodigoBarra, Descripcion, PrecioVenta, Existencia)
VALUES 
('PROD01', '74410011', 'Hamburguesa BigFOOD', 3500.00, 50),
('PROD02', '74410022', 'Papas Fritas Medianas', 1500.00, 100),
('PROD03', '74410033', 'Refresco Gaseoso 500ml', 1200.00, 75);
GO