select * from FAVORITOS
select * from USERS


--Añadir relaciónes entre tablas:
--ALTER TABLE FAVORITOS ADD CONSTRAINT PK_FAVORITOS PRIMARY KEY (Id)

--ALTER TABLE USERS ADD CONSTRAINT PK_USERS PRIMARY KEY (Id)

--ALTER TABLE FAVORITOS ADD CONSTRAINT FK_Favoritos_User FOREIGN KEY (IdUser) REFERENCES USERS(Id)
--ALTER TABLE FAVORITOS ADD CONSTRAINT FK_Favoritos_Articulos FOREIGN KEY (IdArticulo) REFERENCES ARTICULOS(Id)

--ALTER TABLE ARTICULOS ADD CONSTRAINT FK_Articulos_Marcas FOREIGN KEY (IdMarca) REFERENCES MARCAS(Id)


--Visualizar constraint en la tabla:
--SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
--WHERE TABLE_NAME = 'FAVORITOS'


--Creación de procedimiento almacenado para validar usuario
--CREATE PROCEDURE spValidarUsuario
--@Email varchar(50),
--@Password varchar(100)
--as
--begin
--	SELECT id, email, pass, nombre, apellido, UrlImagenPerfil, admin
--	FROM USERS 
--	WHERE email = @Email AND pass = @Password
--END
--GO

