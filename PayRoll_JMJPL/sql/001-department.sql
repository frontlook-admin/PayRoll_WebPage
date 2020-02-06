create table if not exists department
(
	DepartmentId int auto_increment
		primary key,
	DepartmentName varchar(30) not null,
	DepartmentCode varchar(30) not null,
	ArrangeOrder varchar(11) null,
	constraint AK_Department_DepartmentCode
		unique (DepartmentCode),
	constraint AK_Department_DepartmentCode_DepartmentId_DepartmentName
		unique (DepartmentCode, DepartmentId, DepartmentName),
	constraint AK_Department_DepartmentName
		unique (DepartmentName)
);

