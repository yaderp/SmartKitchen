create database dbsk;

drop table ingredientes;
drop table preparacion;
drop table favoritos;
drop table receta;
drop table usuario;


use dbsk;

CREATE TABLE usuario (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombres VARCHAR(100) NOT NULL,
    dni VARCHAR(8),
    correo VARCHAR(120),
    clave VARCHAR(50) NOT NULL,
    idsexo INT NOT NULL,
    idcargo INT NOT NULL,
    fechareg DATETIME,
    estado INT NOT NULL
);

ALTER TABLE usuario MODIFY clave VARCHAR(50) NOT NULL COLLATE utf8mb4_bin;

INSERT INTO usuario(nombres, dni, correo, clave, idsexo, idcargo, fechareg, estado) VALUES('INVITADO','12345678','correo@correo','123',1,0,now(),1);
INSERT INTO usuario(nombres, dni, correo, clave, idsexo, idcargo, fechareg, estado) VALUES('YDRSOFT','43114343','yaderch@gmail.com','palomita',1,1,now(),1);

CREATE TABLE receta (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(750),
    ndif INT NOT NULL,
    tiempo INT NOT NULL, 
    categoria varchar(100),
    fechareg DATETIME,
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
	fechareg DATETIME,
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
