create table if not exists `worker type`
(
	ID int auto_increment
		primary key,
	`Catagory Name` varchar(20) not null,
	`Arrange Order` int null,
	Command longtext null,
	Remark longtext null,
	constraint `Worker Type_Arrange Order_uindex`
		unique (`Arrange Order`),
	constraint `Worker Type_Catagory Name_uindex`
		unique (`Catagory Name`)
)
comment 'Determines Employee Type';

