create table if not exists __efmigrationshistory
(
	MigrationId varchar(95) not null,
	ProductVersion varchar(32) not null,
	primary key (MigrationId)
);

