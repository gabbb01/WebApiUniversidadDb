-- CREANDO LA BASE DE DATOS
CREATE DATABASE UniversidadDB;
GO

USE UniversidadDB;
GO

-- CREANDO TABLA DE PROFESORES
CREATE TABLE Profesores (
    ProfesorId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Especialidad NVARCHAR(100),
    Activo BIT DEFAULT 1 NOT NULL, 
    FechaCreacion DATETIME DEFAULT GETDATE() NOT NULL 
);
GO

-- CREANDO TABLA DE AULAS
CREATE TABLE Aulas (
    AulaId INT IDENTITY(1,1) PRIMARY KEY,
    CodigoAula NVARCHAR(20) NOT NULL,
    Capacidad INT NOT NULL,
    Activo BIT DEFAULT 1 NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE() NOT NULL
);
GO

-- CREANDO TABLA DE ASIGNATURAS CON LLAVES FORANEAS
CREATE TABLE Asignaturas (
    AsignaturaId INT IDENTITY(1,1) PRIMARY KEY, 
    Nombre NVARCHAR(100) NOT NULL,
    Creditos INT NOT NULL,
    ProfesorId INT NOT NULL,
    AulaId INT NOT NULL,
    Activo BIT DEFAULT 1 NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE() NOT NULL,
    
    CONSTRAINT FK_Asignaturas_Profesores FOREIGN KEY (ProfesorId) REFERENCES Profesores(ProfesorId),
    CONSTRAINT FK_Asignaturas_Aulas FOREIGN KEY (AulaId) REFERENCES Aulas(AulaId)
);
GO

-- CREANDO TABLA DE ESTUDIANTES
CREATE TABLE Estudiantes (
    EstudianteId INT IDENTITY(1,1) PRIMARY KEY,
    NumeroCuenta NVARCHAR(50) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(100),
    Activo BIT DEFAULT 1 NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE() NOT NULL
);
GO

SELECT * FROM Profesores
GO
SELECT * FROM Aulas
GO 
SELECT * FROM Asignaturas
GO
SELECT * FROM Estudiantes
GO

INSERT INTO Profesores
(	
	Nombre,
	Apellido,
	Especialidad
)
VALUES
('Gabriel','Ordoñez','Matematicas')
GO

INSERT INTO Aulas
(	
	CodigoAula,
	Capacidad
)
VALUES
('101',35)
GO

INSERT INTO Asignaturas
(	
	Nombre,
	Creditos,
	ProfesorId,
    AulaId
)
VALUES
('Calculo I',5,1,1)
GO

INSERT INTO Estudiantes
(	
	NumeroCuenta,
	Nombre,
	Apellido,
    Correo
)
VALUES
('20230012689','Jose','Guillen','jguillen@gmail.com')


