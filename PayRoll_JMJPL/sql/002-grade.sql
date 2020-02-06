create table if not exists grade
(
	GradeId int auto_increment
		primary key,
	GradeName varchar(30) not null,
	GradeCode varchar(30) not null,
	ArrangeOrder varchar(11) null,
	constraint AK_Grade_GradeCode
		unique (GradeCode),
	constraint AK_Grade_GradeCode_GradeId_GradeName
		unique (GradeCode, GradeId, GradeName),
	constraint AK_Grade_GradeName
		unique (GradeName)
);

