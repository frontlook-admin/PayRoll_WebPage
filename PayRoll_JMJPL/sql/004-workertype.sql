create table if not exists workertype
(
	ID int auto_increment
		primary key,
	CategoryName varchar(30) not null,
	CategoryCode varchar(30) not null,
	ArrangeOrder varchar(11) null,
	constraint AK_WorkerType_CategoryCode
		unique (CategoryCode),
	constraint AK_WorkerType_CategoryCode_CategoryName_ID
		unique (CategoryCode, CategoryName, ID),
	constraint AK_WorkerType_CategoryName
		unique (CategoryName)
);

