Create TABLE Persona
(
	Id int IDENTITY(1,1) Primary Key,
	Nombres varchar(100) NULL,
	Apellido varchar(100) NULL,
	Fecha_Nacimiento date NULL,
	Balance numeric(18,2) NULL,
	Id_Persona_Tipo int NULL,
	Id_Estatus tinyint NULL
)

Create TABLE Persona_Tipo
(
	Id int IDENTITY(1,1) Primary Key,
	Descripcion varchar(50) NULL
	)

CREATE TABLE [Estatus]
( 
	[Id] [tinyint] IDENTITY(1,1) Primary Key,
	[Descripcion] [nchar](10) NOT NULL
	)

-- VISTAS PARA CONSULTA DE LOS DATOS--
--OBJETO EN LA BASE DE DATOS DEL TIPO VISTA--
-- with SchemaBinding enlaza la vista con cada unas de las tablas.

CREATE View dbo.vConsultaPersona
with SchemaBinding
As
Select Persona.Id, Persona.Nombres,Persona.Apellido,Persona.Fecha_Nacimiento,
		Persona.Balance,Persona.Id_Persona_Tipo, Tipo.Descripcion as Tipo_Descripcion,
		Persona.Id_Estatus, Estatus.Descripcion as Estatus_Descripcion
		FROM dbo.Persona as Persona Inner Join
		dbo.Estatus as Estatus on Estatus.Id = Persona.Id_Estatus JOIN
		dbo.Persona_Tipo as Tipo on Tipo.id = Persona.Id_Persona_Tipo
with Check Option

-- *******************************************************************

--Restricciones 

ALTER TABLE [dbo].[Persona]
with check add constraint [FK_Persona_Estatus]
FOREIGN KEY ([Id_Estatus])
references [dbo].[Estatus] ([Id])
go

ALTER TABLE [dbo].[Persona] check constraint [FK_Persona_Estatus]
go

alter table [dbo].[Persona]
with check add constraint [FK_Persona_Persona_Tipo]
foreign key ([Id_Persona_Tipo])
references [dbo].[Persona_Tipo] ([Id])
go

alter table [dbo].[Persona]
check constraint [FK_Persona_Persona_Tipo]
go


-- ***************************************************************************+

-- crear funciones

create Function dbo.fncConsultaPersonalPorId(@Id as int) Returns table
as
 return 
	select Id,Nombres,Apellido,Fecha_Nacimiento,Balance,Id_Persona_Tipo,
			Tipo_Descripcion,Id_Estatus,Estatus_Descripcion
			from vConsultaPersona
			where Id = @Id

create Function dbo.fncConsultaPersonaPorNombres(@Nombres as varchar) Returns table
as
 return 
	select Id,Nombres,Apellido,Fecha_Nacimiento,Balance,Id_Persona_Tipo,
			Tipo_Descripcion,Id_Estatus,Estatus_Descripcion
			from vConsultaPersona
			where Nombres Like @Nombres


create Function dbo.fncConsultaPersonaPorNombresYApellido(@Nombres as varchar,@Apellido as varchar) Returns table
as
 return 
	select Id,Nombres,Apellido,Fecha_Nacimiento,Balance,Id_Persona_Tipo,
			Tipo_Descripcion,Id_Estatus,Estatus_Descripcion
			from vConsultaPersona
			where Nombres Like @Nombres and Apellido Like @Apellido