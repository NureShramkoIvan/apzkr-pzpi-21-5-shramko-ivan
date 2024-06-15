use dronetrainer;

create table organizations
(
	id int primary key identity,
	name nvarchar(50) unique
);

create table users
(
	id int primary key identity,
	username nvarchar(12) unique not null,
	normalized_username nvarchar(12) unique not null,
	password nvarchar(100) not null,
	organization_id int foreign key references organizations(id) not null,
	role int not null,
	time_zone int not null
);

create table backups
(
	id int primary key identity,
	file_name varchar(30) unique not null
);