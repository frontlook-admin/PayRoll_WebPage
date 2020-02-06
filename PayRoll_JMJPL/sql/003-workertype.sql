create table if not exists workertype
(
	WorkerTypeId int auto_increment
		primary key,
	WorkerTypeName varchar(30) not null,
	WorkerTypeCode varchar(30) not null,
	ArrangeOrder varchar(11) null,
	constraint AK_WorkerType_WorkerTypeCode
		unique (WorkerTypeCode),
	constraint AK_WorkerType_WorkerTypeCode_WorkerTypeId_WorkerTypeName
		unique (WorkerTypeCode, WorkerTypeId, WorkerTypeName),
	constraint AK_WorkerType_WorkerTypeName
		unique (WorkerTypeName)
);

