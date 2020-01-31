create table if not exists department
(
	ID int auto_increment
		primary key,
	`Department Name` varchar(30) null,
	`Department Code` varchar(30) null,
	`Arrange Order` int null,
	`Department Formula` longtext null,
	constraint `Department_Arrange Order_uindex`
		unique (`Arrange Order`),
	constraint `Department_Department Code_uindex`
		unique (`Department Code`),
	constraint `Department_Department Name_uindex`
		unique (`Department Name`)
)
comment 'Determines Department Of Employees';

