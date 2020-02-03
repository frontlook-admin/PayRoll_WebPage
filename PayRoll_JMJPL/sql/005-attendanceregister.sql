create table if not exists attendanceregister
(
	Id int auto_increment
		primary key,
	EmployeeId int not null,
	Attendance bit not null,
	AttendanceTime datetime(6) default CURRENT_TIMESTAMP(6) null on update CURRENT_TIMESTAMP(6),
	constraint FK_AttendanceRegister_Employee_EmployeeId
		foreign key (EmployeeId) references employee (Id)
			on delete cascade
);

create index IX_AttendanceRegister_EmployeeId
	on attendanceregister (EmployeeId);

