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
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- CREANDO TABLA DE MATRÍCULAS CON LLAVES FORÁNEAS
-- Relaciona Estudiantes con Asignaturas y almacena la nota obtenida
-- ─────────────────────────────────────────────────────────────────────────────
CREATE TABLE Matriculas (
    MatriculaId  INT            IDENTITY(1,1) PRIMARY KEY,
    EstudianteId INT            NOT NULL,
    AsignaturaId INT            NOT NULL,
    Nota         DECIMAL(5,2)   NOT NULL DEFAULT 0,
    Activo       BIT            NOT NULL DEFAULT 1,
    FechaCreacion DATETIME      NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Matriculas_Estudiantes FOREIGN KEY (EstudianteId)
        REFERENCES Estudiantes(EstudianteId),

    CONSTRAINT FK_Matriculas_Asignaturas FOREIGN KEY (AsignaturaId)
        REFERENCES Asignaturas(AsignaturaId)
);
GO

-- Ver todas las matrículas
SELECT * FROM Matriculas
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- CONSULTA DECLARATIVA: SELECT con INNER JOIN + WHERE + ORDER BY
-- Paradigma Declarativo — describe QUÉ datos quiero, no CÓMO obtenerlos
-- ─────────────────────────────────────────────────────────────────────────────
SELECT
    m.MatriculaId,
    e.NumeroCuenta,
    e.Nombre        AS NombreEstudiante,
    e.Apellido      AS ApellidoEstudiante,
    a.Nombre        AS NombreAsignatura,
    a.Creditos,
    m.Nota,
    m.FechaCreacion
FROM Matriculas m
INNER JOIN Estudiantes e ON m.EstudianteId = e.EstudianteId
INNER JOIN Asignaturas a ON m.AsignaturaId = a.AsignaturaId
WHERE m.Activo = 1
ORDER BY e.Apellido, e.Nombre, a.Nombre;
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- CONSULTA DECLARATIVA: GROUP BY + AVG + COUNT + ORDER BY
-- Promedio de notas por estudiante (paradigma declarativo avanzado)
-- ─────────────────────────────────────────────────────────────────────────────
SELECT
    e.EstudianteId,
    e.Nombre         AS NombreEstudiante,
    e.Apellido       AS ApellidoEstudiante,
    AVG(m.Nota)      AS PromedioNota,
    COUNT(m.MatriculaId) AS TotalAsignaturas
FROM Matriculas m
INNER JOIN Estudiantes e ON m.EstudianteId = e.EstudianteId
WHERE m.Activo = 1
GROUP BY e.EstudianteId, e.Nombre, e.Apellido
ORDER BY PromedioNota DESC;
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- DATOS DE EJEMPLO — Insertar matrícula de prueba
-- ─────────────────────────────────────────────────────────────────────────────
INSERT INTO Matriculas
(
    EstudianteId,
    AsignaturaId,
    Nota
)
VALUES
(1, 1, 85.50)
GO
