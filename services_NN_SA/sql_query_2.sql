USE master;

GO

CREATE DATABASE NN_Soluciones;

GO

USE NN_Soluciones;

GO

-- Tabla Solicitante

CREATE TABLE Solicitante (
    IdSolicitante INT PRIMARY KEY,
    NombreSolicitante VARCHAR(255) NOT NULL
);

-- Tabla EstadoSolicitud

CREATE TABLE EstadoSolicitud (
    NroEstadoSolicitud INT PRIMARY KEY,
    NombreEstadoSolicitud VARCHAR(255) NOT NULL
);

-- Tabla Servicio

CREATE TABLE Servicio (
    NroServicio INT PRIMARY KEY,
    NombreServicio VARCHAR(255) NOT NULL
);

-- Tabla Solicitud

CREATE TABLE Solicitud (
    NroSolicitud INT PRIMARY KEY,
    FechaSolicitud DATE NOT NULL,
    NroEstadoSolicitud INT NOT NULL,
    IdSolicitante INT NOT NULL,
    FOREIGN KEY (NroEstadoSolicitud) REFERENCES EstadoSolicitud (NroEstadoSolicitud),
    FOREIGN KEY (IdSolicitante) REFERENCES Solicitante (IdSolicitante)
);

-- Tabla SolicitudServicio (Tabla intermedia para la relación N:N)

CREATE TABLE SolicitudServicio (
    NroSolicitud INT NOT NULL,
    NroServicio INT NOT NULL,
    FOREIGN KEY (NroSolicitud) REFERENCES Solicitud (NroSolicitud),
    FOREIGN KEY (NroServicio) REFERENCES Servicio (NroServicio)
);


-- Insertar datos en la tabla EstadoSolicitud

INSERT INTO EstadoSolicitud (NroEstadoSolicitud, NombreEstadoSolicitud)
VALUES
    (1, 'Pendiente'),
    (2, 'En curso'),
    (3, 'Finalizada');


-- Insertar datos en la tabla Solicitante

INSERT INTO Solicitante (IdSolicitante, NombreSolicitante)
VALUES
    (1, 'Andrés Martínez'),
    (2, 'María Rodríguez'),
    (3, 'Sebastían García');

-- Insertar datos en la tabla Servicio

INSERT INTO Servicio (NroServicio, NombreServicio)
VALUES
    (1, 'Desarrollo web'),
    (2, 'Diseño gráfico'),
    (3, 'Mantenimiento de software');

-- Insertar datos en la tabla Solicitud

INSERT INTO Solicitud (NroSolicitud, FechaSolicitud, NroEstadoSolicitud, IdSolicitante)
VALUES
    (1, '2024-03-04', 1, 1),
    (2, '2024-03-02', 2, 2),
    (3, '2024-03-01', 3, 3);

-- Insertar datos en la tabla SolicitudServicio

INSERT INTO SolicitudServicio (NroSolicitud, NroServicio)
VALUES
    (1, 1),
    (1, 2),
    (2, 3),
    (3, 1);


SELECT
    s.NroSolicitud,
    sol.NombreSolicitante,
    s.NroEstadoSolicitud,
    es.NombreEstadoSolicitud,
    ss.NroServicio,
    ser.NombreServicio
FROM Solicitud s
INNER JOIN Solicitante sol ON s.IdSolicitante = sol.IdSolicitante
INNER JOIN EstadoSolicitud es ON s.NroEstadoSolicitud = es.NroEstadoSolicitud
INNER JOIN SolicitudServicio ss ON s.NroSolicitud = ss.NroSolicitud
INNER JOIN Servicio ser ON ss.NroServicio = ser.NroServicio;