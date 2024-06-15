use "dronetrainer.training";

create table user_attempts
(
	id int primary key identity,
	session_id int not null,
	user_id int not null,
	started_at datetime null,
	finished_at datetime null
);

create table attempt_steps
(
	id int primary key identity,
	attempt_id int not null foreign key references user_attempts (id)
		on delete cascade,
	device_id int not null,
	passed_at datetime null
);