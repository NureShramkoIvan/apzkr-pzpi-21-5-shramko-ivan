use dronetrainer;

create table training_groups
(
	id int primary key identity,
	name nvarchar(10) not null,
	organization_id int foreign key references organizations(id)
);

create table devices
(
	id int primary key identity,
	type int not null,
	organization_id int not null foreign key references organizations(id)
);

create table training_programs
(
	id int primary key identity,
	organization_id int not null foreign key references organizations(id)
);

create table training_program_steps
(
	id int primary key identity,
	device_id int null foreign key references devices(id)
		on delete set null,
	position int not null,
	program_id int not null foreign key references training_programs(id)
		on delete cascade
);

create table training_sessions
(
	id int primary key identity,
	scheduled_at datetime not null,
	program_id int not null foreign key references training_programs(id),
	group_id int not null foreign key references training_groups(id),
	instructor_id int not null foreign key references users(id),
	started_at datetime null,
	finished_at datetime null
);

create table group_users
(
	user_id int not null foreign key references users(id)
		on delete cascade,
	group_id int not null foreign key references training_groups(id)
		on delete cascade
);