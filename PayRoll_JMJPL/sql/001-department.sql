create table if not exists department
(
	ID int auto_increment
		primary key,
	DepartmentName varchar(30) not null,
	DepartmentCode varchar(30) not null,
	ArrangeOrder int not null,
	constraint AK_Department_DepartmentCode
		unique (DepartmentCode),
	constraint AK_Department_DepartmentCode_DepartmentName_ID
		unique (DepartmentCode, DepartmentName, ID),
	constraint AK_Department_DepartmentName
		unique (DepartmentName)
);

