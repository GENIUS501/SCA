create database SCA
go

use SCA
go

create table Departamento
(
	IdDepartamento int IDENTITY(1,1) PRIMARY KEY,
	Nombre varchar(50) NOT NULL
)

create table Licencia
(
	IdLicencia int IDENTITY(1,1) PRIMARY KEY,
	TipoLicencia VARCHAR(2) NOT NULL,
	VenceLicencia date NOT NULL
)

create table Personal
(
	IdPersonal int IDENTITY(1,1) PRIMARY KEY,
	Cedula varchar(12) UNIQUE NOT NULL,
	Nombre varchar(50) NOT NULL,
	Apellido1 varchar(50) NOT NULL,
	Apellido2 varchar(50) NOT NULL,
	Telefono varchar(8) NOT NULL,
	Correo varchar(50) NOT NULL,
	IdLicencia int,
	CarnetMS int,
	VenceCarnetMS date,
	IdDepartamento int,
	MotivoDeshabilitar int,
	CONSTRAINT FK_Personal_Licencia FOREIGN KEY (IdLicencia) REFERENCES Licencia (IdLicencia),
	CONSTRAINT FK_Personal_Departamento FOREIGN KEY (IdDepartamento) REFERENCES Departamento (IdDepartamento)
)

create table Inventario
(
IdInventario int IDENTITY(1,1) PRIMARY KEY,
CodigoEmpresa varchar(50) UNIQUE NOT NULL,
Nombre varchar(50) NOT NULL,
Modelo varchar(50) NOT NULL,
Serie varchar(50) NOT NULL,
Fabricante varchar(50) NOT NULL,
FechaCompra date NOT NULL,
CostoEquipo int NOT NULL,
Garantia int NOT NULL,
VenceGarantia date NOT NULL,
IdDepartamento int,
MotivoDeshabilitar int

CONSTRAINT FK_Inventario_Departamento FOREIGN KEY (IdDepartamento) REFERENCES Departamento (IdDepartamento)
)

create table Flotilla
(
IdFlotilla int IDENTITY(1,1) PRIMARY KEY,
Placa int UNIQUE NOT NULL,
Marca varchar(25) NOT NULL,
Modelo varchar(25) NOT NULL,
Traccion int NOT NULL,
Ano int NOT NULL,
Combustible int NOT NULL,
FechaCompra date NOT NULL,
CostoVehiculo int NOT NULL,
IdDepartamento int,
MotivoDeshabilitar int

CONSTRAINT FK_Flotilla_Departamento FOREIGN KEY (IdDepartamento) REFERENCES Departamento (IdDepartamento)
)

create table ControlInventario
(
IdControlInventario int IDENTITY(1,1)PRIMARY KEY,
IdInventario int,
IdPersonal int,
EstadoActivo int NOT NULL,
FechaSalida date NOT NULL,
FechaIngresa date NOT NULL,
Anomalias varchar(max) NOT NULL

CONSTRAINT FK_ControlInventario_Inventario FOREIGN KEY (IdInventario) REFERENCES Inventario (IdInventario),
CONSTRAINT FK_ControlInventario_Personal FOREIGN KEY (IdPersonal) REFERENCES Personal (IdPersonal)
)

create table ControlVehiculo
(
IdControlVehiculo int IDENTITY(1,1) PRIMARY KEY,
IdFlotilla int,
IdPersonal int,
EstadoVehiculo int NOT NULL,
FechaSalida date NOT NULL,
KilometrajeSalida int NOT NULL,
FechaIngresa date NOT NULL,
KilometrajeIngresa int NOT NULL,
Anomalias varchar(max) NOT NULL

CONSTRAINT FK_ControlVehiculo_Flotilla FOREIGN KEY (IdFlotilla) REFERENCES Flotilla (IdFlotilla),
CONSTRAINT FK_ControlVehiculo_Personal FOREIGN KEY (IdPersonal) REFERENCES Personal (IdPersonal)
)

create table MantenimientoInventario
(
IdMantenimientoInventario int IDENTITY(1,1) PRIMARY KEY,
IdInventario int,
TipoMantenimiento int NOT NULL,
CostoMantenimiento int NOT NULL,
FechaMantenimiento date,
FechaProximoMantenimiento date,
DescripcionServicio varchar(max) NOT NULL,

CONSTRAINT FK_MantenimientoInventario_Inventario FOREIGN KEY (IdInventario) REFERENCES Inventario (IdInventario)
)

create table MantenimientoVehiculo
(
IdMantenimientoVehiculo int IDENTITY(1,1) PRIMARY KEY,
IdFlotilla int,
TipoMantenimiento int NOT NULL,
KilometrajeActual int NOT NULL,
ProximoKilometraje int NOT NULL,
CostoMantenimiento int NOT NULL,
FechaMantenimiento date,
DescripcionServicio varchar(max) NOT NULL

CONSTRAINT FK_MantenimientoVehiculo_Flotilla FOREIGN KEY (IdFlotilla) REFERENCES Flotilla (IdFlotilla)
)

CREATE TABLE Perfiles_Acceso(
	[Id_Perfil] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[NombrePerfil] [varchar](50) NOT NULL,
	[Descripcion] [varchar](200) NOT NULL,

);

CREATE TABLE [dbo].[Perfiles_Permisos](
	[Id_Permiso] [int] IDENTITY(1,1) NOT NULL,
	[Id_Perfil] [int] NULL,
	[Modulo] VARCHAR(25) NULL,
	[Agregar] [varchar](2) NULL,
	[Modificar] [varchar](2) NULL,
	[Eliminar] [varchar](2) NULL,
	CONSTRAINT FK_Permisos_Perfil FOREIGN KEY (Id_Perfil) REFERENCES Perfiles_Acceso(Id_Perfil)
);

create table Usuario
(
	IdUsuario int IDENTITY(1,1) PRIMARY KEY,
	IdPersonal int,
	IdPerfiles int,
	Usuario varchar(50) NOT NULL,
	Contraseña varchar(max) NOT NULL,
	CONSTRAINT FK_Usuario_Personal FOREIGN KEY (IdPersonal) REFERENCES Personal (IdPersonal),
	CONSTRAINT FK_Usuario_Perfil FOREIGN KEY (IdPerfiles) REFERENCES Perfiles_Acceso (Id_Perfil)
);

create table BitacoraIngresoSalida
(
	Id int IDENTITY(1,1) PRIMARY KEY,
	IdUsuario int NOT NULL,
	FechaIngreso datetime NOT NULL,
	FechaSalida datetime NULL,
	CONSTRAINT FK_BitacoraIngresoSalida_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario (IdUsuario)
)

create table BitacoraMovimiento
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdUsuario int NOT NULL,
	FechaMovimiento date NOT NULL,
	TipoMovimiento varchar(50) NOT NULL,
	ModuloAfectado varchar (50) NOT NULL,
	ValorAntiguo varchar (50) NULL,
	ValorNuevo varchar (50) NULL,
	CONSTRAINT FK_BitacoraMovimiento_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario (IdUsuario)
)