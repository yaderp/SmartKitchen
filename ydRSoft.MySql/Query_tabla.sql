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

ALTER TABLE usuario MODIFY clave VARCHAR(50) COLLATE utf8mb4_bin;

CREATE TABLE receta (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(750),
    ndif INT NOT NULL,
    tiempo INT NOT NULL,   
    fechareg DATE,
    estado INT NOT NULL
);

CREATE TABLE ingredientes (
    id INT PRIMARY KEY AUTO_INCREMENT,    
    idreceta INT NOT NULL,
    detalle VARCHAR(2500)
);
ALTER TABLE ingredientes
ADD FOREIGN KEY (idreceta) REFERENCES receta(id) ON DELETE CASCADE ON UPDATE CASCADE;

CREATE TABLE preparacion (
    id INT PRIMARY KEY AUTO_INCREMENT,    
    idreceta INT NOT NULL,
    detalle VARCHAR(7500)
);
ALTER TABLE preparacion
ADD FOREIGN KEY (idreceta) REFERENCES receta(id) ON DELETE CASCADE ON UPDATE CASCADE;

CREATE TABLE favoritos (
    id INT PRIMARY KEY AUTO_INCREMENT, 
	iduser INT NOT NULL,
    idreceta INT NOT NULL,
	fechareg DATE,
    estado INT NOT NULL
);
ALTER TABLE favoritos
ADD FOREIGN KEY (idreceta) REFERENCES receta(id) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE favoritos
ADD FOREIGN KEY (iduser) REFERENCES usuario(id) ON DELETE CASCADE ON UPDATE CASCADE;

select *from usuario;
select *from receta;

select *from ingredientes;
select *from preparacion;
