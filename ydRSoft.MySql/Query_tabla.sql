create database dbsk;

drop table ingredientes;
drop table preparacion;
drop table favoritos;
drop table receta;
drop table usuario;


use dbsk;
-- 0 eliminado 1 privado 2 publico
CREATE TABLE usuario (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombres VARCHAR(100) NOT NULL,
    dni VARCHAR(8),
    correo VARCHAR(120),
    clave VARCHAR(50) NOT NULL,
    idsexo INT NOT NULL,
    idcargo INT NOT NULL,
    fechareg DATETIME,
    estado INT NOT NULL,
    region VARCHAR(200),
    sugonff INT NOT NULL
);

ALTER TABLE usuario
ADD COLUMN sugonff INT NOT NULL default 1;

ALTER TABLE usuario MODIFY clave VARCHAR(50) NOT NULL COLLATE utf8mb4_bin;

INSERT INTO usuario(nombres, dni, correo, clave, idsexo, idcargo, fechareg, estado, region) VALUES('INVITADO','12345678','correo@correo','123',1,0,now(),1,'PERU');
INSERT INTO usuario(nombres, dni, correo, clave, idsexo, idcargo, fechareg, estado, region) VALUES('YADER','43114343','yaderch@gmail.com','palomita',1,1,now(),1,'PERU');
INSERT INTO usuario(nombres, dni, correo, clave, idsexo, idcargo, fechareg, estado, region) VALUES('CARLOS','12345678','correo@gmail.com','1234',1,1,now(),1,'PERU');
INSERT INTO usuario(nombres, dni, correo, clave, idsexo, idcargo, fechareg, estado, region) VALUES('USUARIO1','12345678','correo@gmail.com','1234',1,1,now(),2,'PERU');
INSERT INTO usuario(nombres, dni, correo, clave, idsexo, idcargo, fechareg, estado, region) VALUES('USUARIO2','12345678','correo@gmail.com','1234',1,1,now(),2,'PERU');
INSERT INTO usuario(nombres, dni, correo, clave, idsexo, idcargo, fechareg, estado, region) VALUES('USUARIO3','12345678','correo@gmail.com','1234',1,1,now(),2,'PERU');

CREATE TABLE receta (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(750),
    ndif INT NOT NULL,
    tiempo INT NOT NULL, 
    categoria VARCHAR(100),
    ingredientes TEXT,
    preparacion TEXT,
    fechareg DATETIME,
    estado INT NOT NULL,
    calificacion INT NOT NULL,
    iduser INT NOT NULL
);

ALTER TABLE receta
ADD COLUMN iduser INT NOT NULL default 1;


CREATE TABLE favoritos (
    id INT PRIMARY KEY AUTO_INCREMENT, 
	iduser INT NOT NULL,
    idreceta INT NOT NULL,
	fechareg DATETIME,
    estado INT NOT NULL
);

ALTER TABLE favoritos
ADD FOREIGN KEY (idreceta) REFERENCES receta(id);

ALTER TABLE favoritos
ADD FOREIGN KEY (iduser) REFERENCES usuario(id);


CREATE TABLE producto (
    id INT PRIMARY KEY AUTO_INCREMENT, 
	nombre VARCHAR(100),
    calorias VARCHAR(100),
    proteinas VARCHAR(100),
    colesterol VARCHAR(100),
    fibra VARCHAR(100),
    carbohidratos VARCHAR(100),
    azucares VARCHAR(100),    
    sodio VARCHAR(100),
    calcio VARCHAR(100),
    grasa VARCHAR(100),
	fechareg DATETIME,
    estado INT NOT NULL
);


-- Estado 0: Eliminado 1: Preferido 2: Alergia
CREATE TABLE preferencias (
    id INT PRIMARY KEY AUTO_INCREMENT, 
	iduser INT NOT NULL,
    idprod INT NOT NULL,
	fechareg DATETIME,
    estado INT NOT NULL
);

ALTER TABLE preferencias
ADD FOREIGN KEY (iduser) REFERENCES usuario(id);

ALTER TABLE preferencias
ADD FOREIGN KEY (idprod) REFERENCES producto(id);

CREATE TABLE sugerencias (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(300),
	idreceta INT NOT NULL,
    iduser INT NOT NULL,
	fechareg DATE
);

ALTER TABLE sugerencias
ADD COLUMN estado INT NOT NULL;

ALTER TABLE sugerencias
ADD FOREIGN KEY (idreceta) REFERENCES receta(id);

select *from usuario;
select *from receta;
select *from favoritos;

select *from producto
where estado =0;
select *from preferencias;
select *from sugerencias;

update sugerencias
set estado = 1
where id>0
