create database ProjectV;
go

use ProjectV;
go


create table Projects
(
	ProjectID	smallint identity,
	PName		varchar(50) not null,
	CreateDate	date not null,
	Username	varchar(50) not null
	PRIMARY KEY (ProjectID,Username)
);
go


create table Devices
(
	DeviceID	smallint identity,
	DName		varchar(50) not null,
	CreateDate	date not null,
	AssignedProj	smallint not null,
	Username	varchar(50) not null
	PRIMARY KEY (DeviceID,Username)
);
go

create table ValueRec
(
	ValueID		smallint identity,
	DeviceID	smallint not null,
	CreateDate	datetime not null DEFAULT(GETDATE()),
	RecordedVal	int not null,
	Username	varchar(50) not null
	PRIMARY KEY (ValueID,Username)
);
go


create table Users
(
	Username	varchar(50) primary key,
	Password	varchar(50) not null,
);
go


INSERT INTO Users VALUES ('parth', 'pass123');
INSERT INTO Users VALUES ('omkar', 'pass123');

select * from Users;


ALTER TABLE Devices
ADD FOREIGN KEY (AssignedProj, Username) REFERENCES Projects(ProjectID, Username);
go


ALTER TABLE ValueRec
ADD FOREIGN KEY (DeviceID, Username) REFERENCES Devices(DeviceID, Username) ON DELETE CASCADE;
go

----DUMMY DATA
INSERT INTO Projects VALUES ('ProjectA', GETDATE(), 'parth');
INSERT INTO Projects VALUES ('ProjectB', GETDATE(), 'parth');
INSERT INTO Projects VALUES ('ProjectC', GETDATE(), 'omkar');
INSERT INTO Projects VALUES ('ProjectD', GETDATE(), 'omkar');

SELECT * FROM Projects;


INSERT INTO Devices VALUES ('SensorA', GETDATE(), 1, 'parth');
INSERT INTO Devices VALUES ('SensorB', GETDATE(), 1, 'parth');
INSERT INTO Devices VALUES ('SensorC', GETDATE(), 2, 'parth');
INSERT INTO Devices VALUES ('SensorD', GETDATE(), 3, 'omkar');
INSERT INTO Devices VALUES ('SensorG', GETDATE(), 3, 'omkar');
INSERT INTO Devices VALUES ('SensorH', GETDATE(), 4, 'omkar');


SELECT * FROM Devices;


INSERT INTO ValueRec VALUES (1, GETDATE(), 0, 'parth');
INSERT INTO ValueRec VALUES (1, GETDATE(), 15, 'parth');
INSERT INTO ValueRec VALUES (2, GETDATE(), -33, 'parth');
INSERT INTO ValueRec VALUES (2, GETDATE(), 18, 'parth');
INSERT INTO ValueRec VALUES (3, GETDATE(), 45, 'parth');
INSERT INTO ValueRec VALUES (3, GETDATE(), -75, 'parth');
INSERT INTO ValueRec VALUES (4, GETDATE(), 75, 'omkar');
INSERT INTO ValueRec VALUES (4, GETDATE(), 36, 'omkar');
INSERT INTO ValueRec VALUES (5, GETDATE(), 13, 'omkar');
INSERT INTO ValueRec VALUES (5, GETDATE(), -6, 'omkar');
INSERT INTO ValueRec VALUES (6, GETDATE(), -12, 'omkar');
INSERT INTO ValueRec VALUES (6, GETDATE(), -23, 'omkar');


SELECT * FROM ValueRec;
