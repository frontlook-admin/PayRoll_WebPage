create table if not exists employee
(
	Id int auto_increment
		primary key,
	EmployeePhoto longblob null,
	FirstName varchar(200) not null,
	MiddleName varchar(200) null,
	LastName varchar(200) null,
	FullName varchar(700) null,
	Gender varchar(20) null,
	PrimaryMobileNo varchar(10) not null,
	SecondaryMobileNo varchar(10) null,
	AreaStdCode varchar(6) null,
	PhoneNo varchar(8) null,
	EmailId varchar(400) null,
	Address1 varchar(400) not null,
	Address2 varchar(400) null,
	Address3 varchar(400) null,
	City varchar(100) not null,
	District varchar(100) null,
	Pin varchar(6) not null,
	PostOffice varchar(100) not null,
	PoliceStation varchar(100) not null,
	DepartmentId int not null,
	GradeId int not null,
	WorkerTypeId int not null,
	constraint AK_Employee_EmailId
		unique (EmailId),
	constraint AK_Employee_Id_PrimaryMobileNo
		unique (Id, PrimaryMobileNo),
	constraint AK_Employee_PrimaryMobileNo
		unique (PrimaryMobileNo),
	constraint AK_Employee_SecondaryMobileNo
		unique (SecondaryMobileNo),
	constraint FK_Employee_Department_DepartmentId
		foreign key (DepartmentId) references department (DepartmentId)
			on delete cascade,
	constraint FK_Employee_Grade_GradeId
		foreign key (GradeId) references grade (GradeId)
			on delete cascade,
	constraint FK_Employee_WorkerType_WorkerTypeId
		foreign key (WorkerTypeId) references workertype (WorkerTypeId)
			on delete cascade
);

create index IX_Employee_DepartmentId
	on employee (DepartmentId);

create index IX_Employee_GradeId
	on employee (GradeId);

create index IX_Employee_WorkerTypeId
	on employee (WorkerTypeId);

