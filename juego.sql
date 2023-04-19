DROP DATABASE IF EXISTS juego;
CREATE DATABASE juego;

USE juego;

CREATE TABLE jugadores(
	idj INT PRIMARY KEY NOT NULL ,
	usuario VARCHAR(60) NOT NULL,
	contrasenya VARCHAR(10) NOT NULL,
	puntuacion INT
)ENGINE = InnoDB;

CREATE TABLE partidas(
	idp INT PRIMARY KEY NOT NULL ,
	numjugadores INT NOT NULL,
	ganador VARCHAR(60) NOT NULL,
	equipo VARCHAR(60) NOT NULL,
	duracion VARCHAR(60) NOT NULL,
	fecha	VARCHAR(60) NOT NULL
)ENGINE = InnoDB;

CREATE TABLE participacion( 
	idPart INT NOT NULL,
	idJugadores INT NOT NULL,
	FOREIGN KEY (idPart) REFERENCES partidas (idp),
	FOREIGN KEY (idJugadores) REFERENCES jugadores (idj)
)ENGINE = InnoDB;

INSERT INTO jugadores VALUES (1, 'Carla', '1234',5);
INSERT INTO jugadores VALUES (2, 'Adria', '5678',6);

INSERT INTO partidas VALUES (1, 2, 'Carla', 'Equipazo', '35', '13/02/2023');
INSERT INTO participacion VALUES (1, 2);





