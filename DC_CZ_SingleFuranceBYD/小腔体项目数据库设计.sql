USE [DB_SingleFurance]
--表一：单体炉信息表
GO

CREATE TABLE [tb_FuranceInfo]				--单体炉信息表
(
	Id int primary key identity(1,1),		--炉生产ID（主键）
	FuranceId int not null,					--腔体编号
	InTime datetime null,					--入炉时间
	OutTime datetime null,					--出炉时间
	VacuumValue decimal(8,2) null			--真空度
)

--表二：层板信息表
GO

CREATE TABLE [tb_LayerInfo]					--层板信息表
(
	Id int primary key identity(1,1),		--层生产ID（主键）
	FK_FuranceInfo_Id int not null, 		--炉生产ID（外键）
	LayerId int not null,					--层板号
	HeatingTimes int null,					--加热次数
	foreign key(FK_FuranceInfo_Id) references [tb_FuranceInfo](Id)
)

--表三：电池信息表
GO

CREATE TABLE [tb_CellInfo]					--电池信息表
(
	Id int primary key identity(1,1),		--自增ID（主键）
	ScanTime datetime null,					--扫码时间
	CellCode varchar(50) not null,          --条码
	RankCode int not null,					--编码（未来可选）
	FK_LayerInfo_Id int not null, 		    --层生产ID（外键）	
	foreign key(FK_LayerInfo_Id) references [tb_LayerInfo](Id)
)

--表四：温度信息表
GO

CREATE TABLE [tb_TemperatureInfo]		    --温度信息表
(
	Id int primary key identity(1,1),		--自增ID（主键）
	RecordTime datetime null,				--记录时间
	ControlValue decimal(8,2) null,			--控温温度
	PatrolValue decimal(8,2) null,			--巡检温度
	FK_LayerInfo_Id int not null, 		    --层生产ID（外键）	
	foreign key(FK_LayerInfo_Id) references [tb_LayerInfo](Id)
)

--表五：操作记录表
GO

CREATE TABLE [tb_OperateRecord]			    --操作记录表
(
	Id int primary key identity(1,1),		--自增ID（主键）
	RecordTime datetime null,			    --记录时间
    UserId int not null,					--用户ID
	OperateContent varchar(256) null,		--内容
)

--表六：报警记录表
GO

CREATE TABLE [tb_AlarmRecord]			    --报警记录表
(
	Id int primary key identity(1,1),		--自增ID（主键）
	RecordTime datetime null,			    --记录时间
    AlarmRank int not null,					--报警级别
	AlarmContent varchar(256) null,		    --内容
)

--表七：角色信息表
GO

CREATE TABLE [tb_RoleInfo]			    	--角色信息表
(
	Id int primary key identity(1,1),		--自增ID（主键）
	RoleName varchar(50) not null			--角色名称
)

--表八：用户信息表
GO

CREATE TABLE [tb_UserInfo]			    	--用户信息表
(
	Id int primary key identity(1,1),		--自增ID（主键）
	UserName varchar(50) not null,   		--用户名
	UserPassword varchar(50) not null,		--用户密码
	CreateTime datetime null,			    --创建时间
	FK_RoleInfo_Id int not null,			--角色ID（外键）
	foreign key(FK_RoleInfo_Id) references [tb_RoleInfo](Id)	
)

--表九：菜单信息表
GO

CREATE TABLE [tb_MenuInfo]			    	--菜单信息表
(
	Id int primary key ,		            --自增ID（主键）
	MenuName varchar(50) not null,			--菜单名称
	ParentId int not null,                  --父节点
	IsActived bit null                      --是否启用
)

--表十：角色-菜单绑定表
GO

CREATE TABLE [tb_RoleMenuBinding]			--角色-菜单绑定表
(
	FK_RoleInfo_Id int not null,	   		--角色ID（外键）
	FK_MenuInfo_Id int not null,			--菜单ID（外键）
	foreign key(FK_RoleInfo_Id) references [tb_RoleInfo](Id),
	foreign key(FK_MenuInfo_Id) references [tb_MenuInfo](Id)
)