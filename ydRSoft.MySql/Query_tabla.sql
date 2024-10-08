create database dbsk;

use dbsk;

CREATE TABLE usuario (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombres VARCHAR(100),
    dni VARCHAR(8),
    correo VARCHAR(120),
    clave VARCHAR(20),
    idsexo INT NOT NULL,
    fechareg DATE,
    estado INT NOT NULL
);

