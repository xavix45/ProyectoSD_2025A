-- 1. Crear base de datos
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'EleccionesSD')
BEGIN
    ALTER DATABASE EleccionesSD SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE EleccionesSD;
END
GO

CREATE DATABASE EleccionesSD;
GO

USE EleccionesSD;
GO

-- 2. Crear tablas

-- Tabla Localidad
CREATE TABLE Localidad (
    IdLocalidad INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL UNIQUE
);
GO
select * from Localidad
-- Tabla Candidato
CREATE TABLE Candidato (
    IdCandidato INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL UNIQUE
);
GO

-- Tabla Mesa
CREATE TABLE Mesa (
    IdMesa INT IDENTITY(1,1) PRIMARY KEY,
    FechaAsignada DATE NOT NULL,
    IdLocalidad INT NOT NULL FOREIGN KEY REFERENCES Localidad(IdLocalidad),
    NumeroMesa INT NOT NULL,
    Votantes INT NOT NULL DEFAULT 100,
    Cerrada BIT NOT NULL DEFAULT 0,
    CONSTRAINT UQ_Mesa_Localidad_Fecha_Numero UNIQUE (IdLocalidad, FechaAsignada, NumeroMesa)
);
GO

-- Tabla MesaCandidato
CREATE TABLE MesaCandidato (
    IdMesa INT NOT NULL FOREIGN KEY REFERENCES Mesa(IdMesa),
    IdCandidato INT NOT NULL FOREIGN KEY REFERENCES Candidato(IdCandidato),
    Votos INT NOT NULL DEFAULT 0,
    PRIMARY KEY (IdMesa, IdCandidato)
);
GO

-- Tabla VotosExtras
CREATE TABLE VotosExtras (
    IdMesa INT PRIMARY KEY FOREIGN KEY REFERENCES Mesa(IdMesa),
    Blancos INT NOT NULL DEFAULT 0,
    Nulos INT NOT NULL DEFAULT 0,
    Ausentes INT NOT NULL DEFAULT 0
);
GO

-- 3. Procedimientos almacenados

-- a) AsignarMesa
CREATE PROCEDURE AsignarMesa
    @Fecha DATE,
    @Localidad NVARCHAR(100),
    @IdMesa INT OUTPUT,
    @NumeroMesa INT OUTPUT,
    @Votantes INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdLocalidad INT;
    DECLARE @MaxNumeroMesa INT;

    SELECT @IdLocalidad = IdLocalidad FROM Localidad WHERE Nombre = @Localidad;

    IF @IdLocalidad IS NULL
    BEGIN
        RAISERROR('Localidad no encontrada.', 16, 1);
        RETURN;
    END

    SELECT @MaxNumeroMesa = ISNULL(MAX(NumeroMesa), 0)
    FROM Mesa
    WHERE IdLocalidad = @IdLocalidad AND FechaAsignada = @Fecha;

    IF @MaxNumeroMesa >= 100
    BEGIN
        RAISERROR('ERROR: No hay mesas disponibles', 16, 1);
        RETURN;
    END

    SET @NumeroMesa = @MaxNumeroMesa + 1;
    SET @Votantes = 100;

    INSERT INTO Mesa (FechaAsignada, IdLocalidad, NumeroMesa, Votantes, Cerrada)
    VALUES (@Fecha, @IdLocalidad, @NumeroMesa, @Votantes, 0);

    SET @IdMesa = SCOPE_IDENTITY();

    INSERT INTO MesaCandidato (IdMesa, IdCandidato, Votos)
    SELECT @IdMesa, IdCandidato, 0 FROM Candidato;
END
GO

-- b) RegistrarDatos
CREATE PROCEDURE RegistrarDatos
    @IdMesa INT,
    @Votos VARCHAR(MAX),
    @Blancos INT,
    @Nulos INT,
    @Respuesta NVARCHAR(50) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Votantes INT;
    DECLARE @Cerrada BIT;

    SELECT @Votantes = Votantes, @Cerrada = Cerrada FROM Mesa WHERE IdMesa = @IdMesa;

    IF @Cerrada = 1
    BEGIN
        SET @Respuesta = 'CERRADA';
        RETURN;
    END

    DECLARE @CantCandidatos INT;
    SELECT @CantCandidatos = COUNT(*) FROM Candidato;

    DECLARE @CantidadVotos INT;
    SET @CantidadVotos = LEN(@Votos) - LEN(REPLACE(@Votos, ',', '')) + 1;

    IF @CantidadVotos <> @CantCandidatos
    BEGIN
        SET @Respuesta = 'ERROR: Número de votos no coincide con candidatos';
        RETURN;
    END

    DECLARE @VotosTabla TABLE (Id INT IDENTITY(1,1), Valor INT);
    INSERT INTO @VotosTabla (Valor)
    SELECT TRY_CAST(value AS INT) FROM STRING_SPLIT(@Votos, ',');

    IF EXISTS (SELECT 1 FROM @VotosTabla WHERE Valor IS NULL OR Valor < 0)
    BEGIN
        SET @Respuesta = 'ERROR: Valores de votos inválidos';
        RETURN;
    END

    DECLARE @SumaTotal INT;
    SELECT @SumaTotal = SUM(Valor) FROM @VotosTabla;

    SET @SumaTotal = ISNULL(@SumaTotal,0) + ISNULL(@Blancos,0) + ISNULL(@Nulos,0);

    IF @SumaTotal > @Votantes
    BEGIN
        SET @Respuesta = 'ERROR: Supera el número de votantes asignados';
        RETURN;
    END

    UPDATE mc
    SET mc.Votos = mc.Votos + vt.Valor
    FROM MesaCandidato mc
    INNER JOIN @VotosTabla vt ON mc.IdCandidato = vt.Id
    WHERE mc.IdMesa = @IdMesa;

    MERGE VotosExtras AS target
    USING (SELECT @IdMesa AS IdMesa) AS source
    ON (target.IdMesa = source.IdMesa)
    WHEN MATCHED THEN
        UPDATE SET 
            Blancos = Blancos + @Blancos,
            Nulos = Nulos + @Nulos,
            Ausentes = Ausentes + (@Votantes - @SumaTotal)
    WHEN NOT MATCHED THEN
        INSERT (IdMesa, Blancos, Nulos, Ausentes)
        VALUES (@IdMesa, @Blancos, @Nulos, (@Votantes - @SumaTotal));

    SET @Respuesta = 'OK';
END
GO

-- c) CerrarMesa
CREATE PROCEDURE CerrarMesa
    @IdMesa INT,
    @Respuesta NVARCHAR(10) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Cerrada BIT;
    SELECT @Cerrada = Cerrada FROM Mesa WHERE IdMesa = @IdMesa;

    IF @Cerrada = 1
    BEGIN
        SET @Respuesta = 'CERRADA';
        RETURN;
    END

    UPDATE Mesa SET Cerrada = 1 WHERE IdMesa = @IdMesa;
    SET @Respuesta = 'OK';
END
GO

-- 4. Consulta de estadísticas por mesa

CREATE VIEW EstadisticasMesa AS
SELECT 
    m.IdMesa,
    m.NumeroMesa,
    l.Nombre AS Localidad,
    m.Votantes,
    ISNULL(ve.Blancos, 0) AS Blancos,
    ISNULL(ve.Nulos, 0) AS Nulos,
    ISNULL(ve.Ausentes, 0) AS Ausentes,
    ISNULL(SUM(mc.Votos), 0) AS TotalVotos,
    (m.Votantes - ISNULL(SUM(mc.Votos), 0) - ISNULL(ve.Blancos, 0) - ISNULL(ve.Nulos, 0)) AS Ausentismo
FROM Mesa m
INNER JOIN Localidad l ON m.IdLocalidad = l.IdLocalidad
LEFT JOIN VotosExtras ve ON m.IdMesa = ve.IdMesa
LEFT JOIN MesaCandidato mc ON m.IdMesa = mc.IdMesa
GROUP BY m.IdMesa, m.NumeroMesa, l.Nombre, m.Votantes, ve.Blancos, ve.Nulos, ve.Ausentes;
GO

--------------------------------------

USE EleccionesSD;
GO

-- 1. Insertar localidades
INSERT INTO Localidad (Nombre) VALUES

('Cuenca'),('Quito'), ('Guayaquil');
GO

-- 2. Insertar candidatos
INSERT INTO Candidato (Nombre) VALUES
('Alice'),
('Bob'),
('Charlie'),
('Diana'),
('Eduardo');
GO

-- 3. Insertar mesas

-- Primero obtén los IdLocalidad para no errar
DECLARE @IdQuito INT = (SELECT IdLocalidad FROM Localidad WHERE Nombre = 'Quito');
DECLARE @IdGuayaquil INT = (SELECT IdLocalidad FROM Localidad WHERE Nombre = 'Guayaquil');
DECLARE @IdCuenca INT = (SELECT IdLocalidad FROM Localidad WHERE Nombre = 'Cuenca');

INSERT INTO Mesa (FechaAsignada, IdLocalidad, NumeroMesa, Votantes, Cerrada) VALUES
('2025-05-29', @IdQuito, 1, 100, 0),
('2025-05-29', @IdQuito, 2, 100, 0),
('2025-05-29', @IdGuayaquil, 1, 100, 0),
('2025-05-29', @IdCuenca, 1, 100, 0);
GO

-- 4. Insertar votos por candidato para cada mesa

-- Obtener IdMesa para cada fila recién insertada
DECLARE @IdMesaQ1 INT = (SELECT TOP 1 IdMesa FROM Mesa WHERE IdLocalidad = 1 AND NumeroMesa = 1 AND FechaAsignada = '2025-05-29');
DECLARE @IdMesaQ2 INT = (SELECT TOP 1 IdMesa FROM Mesa WHERE IdLocalidad = 1 AND NumeroMesa = 2 AND FechaAsignada = '2025-05-29');
DECLARE @IdMesaG1 INT = (SELECT TOP 1 IdMesa FROM Mesa WHERE IdLocalidad = 2 AND NumeroMesa = 1 AND FechaAsignada = '2025-05-29');
DECLARE @IdMesaC1 INT = (SELECT TOP 1 IdMesa FROM Mesa WHERE IdLocalidad = 3 AND NumeroMesa = 1 AND FechaAsignada = '2025-05-29');

-- Obtener IdCandidato de cada candidato
DECLARE @IdAlice INT = (SELECT IdCandidato FROM Candidato WHERE Nombre = 'Alice');
DECLARE @IdBob INT = (SELECT IdCandidato FROM Candidato WHERE Nombre = 'Bob');
DECLARE @IdCharlie INT = (SELECT IdCandidato FROM Candidato WHERE Nombre = 'Charlie');
DECLARE @IdDiana INT = (SELECT IdCandidato FROM Candidato WHERE Nombre = 'Diana');
DECLARE @IdEduardo INT = (SELECT IdCandidato FROM Candidato WHERE Nombre = 'Eduardo');

-- Insertar votos iniciales 0 en MesaCandidato para cada mesa y candidato
INSERT INTO MesaCandidato (IdMesa, IdCandidato, Votos) VALUES
(5, 1, 0), (5, 2, 0), (5, 3, 0), (5, 4, 0), (5, 5, 0),

(2, 1, 0), (2, 2, 0), (2, 3, 0), (2, 4, 0), (2, 5, 0),

(3, 1, 0), (3, 2, 0), (3,3, 0), (3, 4, 0), (3, 5, 0),

(4, 1, 0), (4, 2, 0), (4, 3, 0), (4, 4, 0), (4, 5, 0);
GO

-- 5. Insertar votos extras iniciales (blancos, nulos, ausentes = 0)
INSERT INTO VotosExtras (IdMesa, Blancos, Nulos, Ausentes) VALUES
(3, 0, 0, 0),
(2, 0, 0, 0),
(4, 0, 0, 0),
(5, 0, 0, 0);
GO

SELECT IdMesa, NumeroMesa, FechaAsignada FROM Mesa;

DELETE FROM MesaCandidato WHERE IdMesa = 1;


------------------------------------------
-- Mostrar todas las localidades
SELECT * FROM Localidad;
GO

-- Mostrar todos los candidatos
SELECT * FROM Candidato;
GO

-- Mostrar todas las mesas
SELECT * FROM Mesa;
GO

-- Mostrar votos por candidato en cada mesa
SELECT * FROM MesaCandidato;
GO

-- Mostrar votos extras (blancos, nulos, ausentes) por mesa
SELECT * FROM VotosExtras;
GO