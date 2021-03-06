USE [master]
GO

/****** Object:  Database [DB_SingleFurance]    Script Date: 01/06/2020 11:28:25 ******/
CREATE DATABASE [DB_SingleFurance] ON  PRIMARY 
( NAME = N'DB_SingleFurance', FILENAME = N'D:\MIBDB\DB_SingleFurance.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DB_SingleFurance_log', FILENAME = N'D:\MIBDB\DB_SingleFurance_log.ldf' , SIZE = 11200KB , MAXSIZE = 20GB , FILEGROWTH = 10%)
GO
USE [DB_SingleFurance]
GO
/****** Object:  Table [dbo].[tb_RoleInfo]    Script Date: 01/09/2020 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_RoleInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tb_RoleInfo] ON
INSERT [dbo].[tb_RoleInfo] ([Id], [RoleName]) VALUES (1, N'超级管理员')
INSERT [dbo].[tb_RoleInfo] ([Id], [RoleName]) VALUES (2, N'管理员')
INSERT [dbo].[tb_RoleInfo] ([Id], [RoleName]) VALUES (3, N'厂家')
INSERT [dbo].[tb_RoleInfo] ([Id], [RoleName]) VALUES (4, N'ME')
INSERT [dbo].[tb_RoleInfo] ([Id], [RoleName]) VALUES (5, N'生产')
SET IDENTITY_INSERT [dbo].[tb_RoleInfo] OFF
/****** Object:  Table [dbo].[tb_OperateRecord]    Script Date: 01/09/2020 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_OperateRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordTime] [datetime] NULL,
	[EmployeeName] [varchar](50) NULL,
	[OperateContent] [varchar](256) NULL,
 CONSTRAINT [PK__tb_Opera__3214EC07117F9D94] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tb_OperateRecord] ON
INSERT [dbo].[tb_OperateRecord] ([Id], [RecordTime], [EmployeeName], [OperateContent]) VALUES (84, CAST(0x0000AB3A00B86410 AS DateTime), N'厂家测试员', N'清空数据库数据')
INSERT [dbo].[tb_OperateRecord] ([Id], [RecordTime], [EmployeeName], [OperateContent]) VALUES (85, CAST(0x0000AB3A00EC8AFC AS DateTime), N'厂家测试员', N'刷卡开机登录成功')
INSERT [dbo].[tb_OperateRecord] ([Id], [RecordTime], [EmployeeName], [OperateContent]) VALUES (86, CAST(0x0000AB3A013A4163 AS DateTime), N'厂家测试员', N'刷卡开机登录成功')
INSERT [dbo].[tb_OperateRecord] ([Id], [RecordTime], [EmployeeName], [OperateContent]) VALUES (87, CAST(0x0000AB3A013C4172 AS DateTime), N'厂家测试员', N'刷卡开机登录成功')
INSERT [dbo].[tb_OperateRecord] ([Id], [RecordTime], [EmployeeName], [OperateContent]) VALUES (88, CAST(0x0000AB3B00B98596 AS DateTime), N'厂家测试员', N'刷卡开机登录成功')
INSERT [dbo].[tb_OperateRecord] ([Id], [RecordTime], [EmployeeName], [OperateContent]) VALUES (89, CAST(0x0000AB3C0135736F AS DateTime), N'厂家测试员', N'刷卡开机登录成功')
INSERT [dbo].[tb_OperateRecord] ([Id], [RecordTime], [EmployeeName], [OperateContent]) VALUES (90, CAST(0x0000AB3D00BC50A1 AS DateTime), N'厂家测试员', N'刷卡开机登录成功')
SET IDENTITY_INSERT [dbo].[tb_OperateRecord] OFF
/****** Object:  Table [dbo].[tb_MenuInfo]    Script Date: 01/09/2020 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_MenuInfo](
	[Id] [int] NOT NULL,
	[MenuName] [varchar](50) NOT NULL,
	[ParentId] [int] NOT NULL,
	[IsActived] [bit] NOT NULL,
 CONSTRAINT [PK_tb_MenuInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (100, N'设备概况', 0, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (101, N'设备运行概况', 100, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (102, N'电气控制', 100, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (103, N'一键换型', 100, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (200, N'报警管理', 0, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (201, N'报警历史', 200, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (202, N'报警规则', 200, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (300, N'数据分析', 0, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (301, N'电池分析', 300, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (302, N'温度分析', 300, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (303, N'电池温度分析', 300, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (304, N'小车状态', 300, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (305, N'实时温度', 300, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (400, N'统计报表', 0, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (401, N'产量统计', 400, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (500, N'手动防呆', 0, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (501, N'内部数据防呆', 500, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (502, N'MES防呆', 500, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (503, N'强交互防呆', 500, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (1000, N'用户设置', 0, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (1001, N'用户管理', 1000, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (1002, N'角色管理', 1000, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (1003, N'权限管理', 1000, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (1100, N'系统设置', 0, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (1101, N'常用参数设置', 1100, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (1102, N'MES参数设置', 1100, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (1103, N'电气参数设置', 1100, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (1104, N'操作日志', 1100, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (1200, N'关于', 0, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (1201, N'版本信息', 1200, 1)
INSERT [dbo].[tb_MenuInfo] ([Id], [MenuName], [ParentId], [IsActived]) VALUES (1202, N'帮助文档', 1200, 1)
/****** Object:  Table [dbo].[tb_MachineStatusTime]    Script Date: 01/09/2020 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_MachineStatusTime](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WaitComeTime] [int] NULL,
	[WaitOutTime] [int] NULL,
	[AutoTime] [int] NULL,
	[HandleTime] [int] NULL,
	[WarnTime] [int] NULL,
	[SaveTime] [datetime] NULL,
 CONSTRAINT [PK_tb_MachineStatusTime] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_CellInfo]    Script Date: 01/09/2020 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_CellInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ScanTime] [datetime] NOT NULL,
	[CellCode] [varchar](50) NOT NULL,
	[RankCode] [int] NOT NULL,
	[LayerNumber] [int] NOT NULL,
	[RowNum] [int] NULL,
	[ColumnNum] [int] NULL,
	[CarrierId] [int] NOT NULL,
 CONSTRAINT [PK__tb_CellI__3214EC0707F6335A] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_CarrierInfo]    Script Date: 01/09/2020 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_CarrierInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProduceId] [int] NOT NULL,
	[CarrierNum] [int] NOT NULL,
	[BeginTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[VacuumValue] [decimal](8, 2) NULL,
 CONSTRAINT [PK__tb_Furan__3214EC077F60ED59] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_AlarmRecord]    Script Date: 01/09/2020 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_AlarmRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[AlarmRank] [int] NULL,
	[AlarmContent] [varchar](500) NULL,
 CONSTRAINT [PK__tb_Alarm__3214EC0715502E78] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlarmRule]    Script Date: 01/09/2020 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AlarmRule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlcIndex] [int] NOT NULL,
	[AlarmContent] [varchar](500) NOT NULL,
 CONSTRAINT [PK_AlarmRule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[AlarmRule] ON
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (1, 1, N'入料机器人急停被按下')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (2, 2, N'上料急停被按下')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (3, 3, N'腔体1急停被按下')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (4, 4, N'腔体6急停被按下')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (5, 5, N'下料急停被按下')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (6, 6, N'出料机器人急停被按下')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (7, 7, N'上位机生命帧异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (8, 8, N'与上位机参数比较异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (9, 9, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (10, 10, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (11, 11, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (12, 12, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (13, 13, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (14, 14, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (15, 15, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (16, 16, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (17, 17, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (18, 18, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (19, 19, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (20, 20, N'上料烟雾报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (21, 21, N'下料烟雾报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (22, 22, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (23, 23, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (24, 24, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (25, 25, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (26, 26, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (27, 27, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (28, 28, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (29, 29, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (30, 30, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (31, 31, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (32, 32, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (33, 33, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (34, 34, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (35, 35, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (36, 36, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (37, 37, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (38, 38, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (39, 39, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (40, 40, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (41, 41, N'NG盒计数清零报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (42, 42, N'入料拉带电机手动时报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (43, 43, N'入料排序拉带电机手动时报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (44, 44, N' 入料拉带溢出 ')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (45, 45, N'入料排序溢出')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (46, 46, N'NG盒满报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (47, 47, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (48, 48, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (49, 49, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (50, 50, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (51, 51, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (52, 52, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (53, 53, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (54, 54, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (55, 55, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (56, 56, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (57, 57, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (58, 58, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (59, 59, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (60, 60, N'入料机器人报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (61, 61, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (62, 62, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (63, 63, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (64, 64, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (65, 65, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (66, 66, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (67, 67, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (68, 68, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (69, 69, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (70, 70, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (71, 71, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (72, 72, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (73, 73, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (74, 74, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (75, 75, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (76, 76, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (77, 77, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (78, 78, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (79, 79, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (80, 80, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (81, 81, N'上料旋转气缸缩回报警 ')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (82, 82, N' 上料旋转气缸伸出报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (83, 83, N'上料钩子1缩回报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (84, 84, N'上料钩子1伸出报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (85, 85, N'上料钩子2缩回报警 ')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (86, 86, N'上料钩子2伸出报警 ')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (87, 87, N'上料托举主轴报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (88, 88, N'上料托举主轴报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (89, 89, N' 上料吸盘横向轴报警 ')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (90, 90, N' 上料吸盘纵向主轴报警 ')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (91, 91, N' 上料吸盘纵向从轴报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (92, 92, N'上料吸盘纵向未耦合')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (93, 93, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (94, 94, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (95, 95, N'上料初始化托举电机未找到回原方式 ')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (96, 96, N'上料天车收到钩子交接板信号，但交接处并没层板')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (97, 97, N'上料完成扫层异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (98, 98, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (99, 99, N'上料托板电机解耦失败')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (100, 100, N'上料托板电机耦合失败')
GO
print 'Processed 100 total records'
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (101, 101, N'上料小车ID号异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (102, 102, N'上料托板电机放板位置异常，有压板危险')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (103, 103, N'上料天车钩错层板')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (104, 104, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (105, 105, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (106, 106, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (107, 107, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (108, 108, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (109, 109, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (110, 110, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (111, 111, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (112, 112, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (113, 113, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (114, 114, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (115, 115, N'上料吸盘吸电池失败')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (116, 116, N'上料吸盘处理电池标志位数组时小车ID号或层板数异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (117, 117, N'上料吸盘破真空失败')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (118, 118, N'上料吸盘横向电机运动时吸盘纵向没有在原点')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (119, 119, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (120, 120, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (121, 121, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (122, 122, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (123, 123, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (124, 124, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (125, 125, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (126, 126, N'小车进门门板电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (127, 127, N'上料位定位气缸异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (128, 128, N'上料位流转气缸异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (129, 129, N'上料到腔体1后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (130, 130, N'上料到腔体1过程中超限或腔体电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (131, 131, N'腔体1小车防压传感器状态异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (132, 132, N'小车进门门板锁紧气缸异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (133, 133, N'腔体1小车到腔体2后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (134, 134, N'腔体1小车到腔体2过程中超限或腔体电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (135, 135, N'腔体2小车到腔体3后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (136, 136, N'腔体2小车到腔体3过程中超限或腔体电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (137, 137, N'腔体3小车到腔体4后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (138, 138, N'腔体3小车到腔体4过程中超限或腔体电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (139, 139, N'腔体4小车到腔体5后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (140, 140, N'腔体4小车到腔体5过程中超限或腔体电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (141, 141, N'腔体5小车到腔体6后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (142, 142, N'腔体5小车到腔体6过程中超限或腔体电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (143, 143, N'小车出门门板电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (144, 144, N'腔体6小车到下料位后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (145, 145, N'腔体6小车到下料位过程中超限或腔体电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (146, 146, N'腔体6小车防压传感器状态异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (147, 147, N'小车出门门板锁紧气缸异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (148, 148, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (149, 149, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (150, 150, N'手动上料位开往腔体1传感器信号异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (151, 151, N'手动腔体1开往腔体2传感器信号异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (152, 152, N'手动腔体2开往腔体3传感器信号异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (153, 153, N'手动腔体3开往腔体4传感器信号异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (154, 154, N'手动腔体5开往腔体6传感器信号异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (155, 155, N'手动腔体6开往下料位传感器信号异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (156, 156, N'腔体出门升到位后位置异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (157, 157, N'腔体出门降到位后位置异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (158, 158, N'腔体入门升到位后位置异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (159, 159, N'腔体入门降到位后位置异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (160, 160, N'腔体1流转电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (161, 161, N'腔体2流转电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (162, 162, N'腔体3流转电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (163, 163, N'腔体4流转电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (164, 164, N'腔体5流转电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (165, 165, N'腔体6流转电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (166, 166, N'下料流转轴报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (167, 167, N'腔体进门电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (168, 168, N'腔体出门报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (169, 169, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (170, 170, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (171, 171, N'上料天车横向电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (172, 172, N'下料天车横向电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (173, 173, N'上料天车横向到腔体6后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (174, 174, N'上料天车钩子气缸异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (175, 175, N'上料天车横向到上料位后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (176, 176, N'上料天车纵向电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (177, 177, N'上料天车防粘板缩回异常 ')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (178, 178, N'上料天车防粘板伸出异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (179, 179, N'上料天车横向运动时纵向未在等待位')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (180, 180, N'上料天车取板时传感器信号异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (181, 181, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (182, 182, N'上料天车取层板时腔体缓存区无层板')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (183, 183, N'上料天车运行时下料天车未在安全位')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (184, 184, N'上料天车到腔体1缓存位后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (185, 185, N'上料天车到腔体2缓存位后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (186, 186, N'上料天车到腔体3缓存位后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (187, 187, N'上料天车到腔体4缓存位后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (188, 188, N'上料天车到腔体5缓存位后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (189, 189, N'上料天车到上料位位后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (190, 190, N'上料天车横向电机报警或超限')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (191, 191, N'上料天车纵向电机报警或超限')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (192, 192, N'下料天车钩子气缸异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (193, 193, N'下料天车横向到腔体6后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (194, 194, N'下料天车横向到下料位后2个传感器无感应')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (195, 195, N'下料天车放小车架子横向电机超限或报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (196, 196, N'下料天车放小车架子纵向电机超限或报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (197, 197, N'下料天车纵向报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (198, 198, N'下料天车横向运动时纵向未在等待位')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (199, 199, N'下料天车取板时传感器信号异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (200, 200, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (201, 201, N'下料天车钩子气缸异常')
GO
print 'Processed 200 total records'
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (202, 202, N'下料天车放小车层板横向电机超限或报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (203, 203, N'下料天车放小车层板纵向电机超限或报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (204, 204, N'腔体缓存位层板数量记忆与传感器比较异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (205, 205, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (206, 206, N'横向天车运动条件未满足，轴未回原或门未下降到位。')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (207, 207, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (208, 208, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (209, 209, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (210, 210, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (211, 211, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (212, 212, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (213, 213, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (214, 214, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (215, 215, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (216, 216, N'腔体1温差过大')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (217, 217, N'腔体2温差过大')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (218, 218, N'腔体3温差过大')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (219, 219, N'腔体4温差过大')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (220, 220, N'腔体5温差过大')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (221, 221, N'腔体6温差过大')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (222, 222, N'腔体1温度超65℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (223, 223, N'腔体1温度超63℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (224, 224, N'腔体1温度低于57℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (225, 225, N'腔体1温度低于55℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (226, 226, N'腔体2温度超65℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (227, 227, N'腔体2温度超63℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (228, 228, N'腔体2温度低于57℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (229, 229, N'腔体2温度低于55℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (230, 230, N'腔体3温度超65℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (231, 231, N'腔体3温度超63℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (232, 232, N'腔体3温度低于57℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (233, 233, N'腔体3温度低于55℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (234, 234, N'腔体4温度超65℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (235, 235, N'腔体4温度超63℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (236, 236, N'腔体4温度低于57℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (237, 237, N'腔体4温度低于55℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (238, 238, N'腔体5温度超65℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (239, 239, N'腔体5温度超63℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (240, 240, N'腔体5温度低于57℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (241, 241, N'腔体5温度低于55℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (242, 242, N'腔体6温度超65℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (243, 243, N'腔体6温度超63℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (244, 244, N'腔体6温度低于57℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (245, 245, N'腔体6温度低于55℃')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (246, 246, N'腔体1风机超时报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (247, 247, N'腔体2风机超时报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (248, 248, N'腔体3风机超时报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (249, 249, N'腔体4风机超时报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (250, 250, N'腔体5风机超时报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (251, 251, N'腔体6风机超时报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (252, 252, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (253, 253, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (254, 254, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (255, 255, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (256, 256, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (257, 257, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (258, 258, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (259, 259, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (260, 260, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (261, 261, N'下料吸盘旋转气缸报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (262, 262, N'下料吸盘纵向电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (263, 263, N'下料吸盘横向电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (264, 264, N'下料缩回左边托板电机钩子时钩子上有层板')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (265, 265, N'下料左边托板电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (266, 266, N'下料右边托板电机钩子气缸异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (267, 267, N'下料右边托板电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (268, 268, N'下料缩回右边托板电机钩子时钩子上有层板')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (269, 269, N'下料托板电机解耦失败')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (270, 270, N'下料托板电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (271, 271, N'下料扫层层数异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (272, 272, N'下料扫层层板位置异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (273, 273, N'下料钩子气缸异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (274, 274, N'下料托板电机耦合异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (275, 275, N'下料吸盘旋转气缸异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (276, 276, N'下料纵向电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (277, 277, N'下料横向电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (278, 278, N'下料横向电机运行时纵向电机未在原点')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (279, 279, N'下料吸盘吸电池失败')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (280, 280, N'下料吸盘破真空失败')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (281, 281, N'空运转时下料吸盘完成时层板内有电池提醒')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (282, 282, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (283, 283, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (284, 284, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (285, 285, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (286, 286, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (287, 287, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (288, 288, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (289, 289, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (290, 290, N'下料吸盘纵向未耦合')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (291, 291, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (292, 292, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (293, 293, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (294, 294, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (295, 295, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (296, 296, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (297, 297, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (298, 298, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (299, 299, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (300, 300, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (301, 301, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (302, 302, N'备用')
GO
print 'Processed 300 total records'
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (303, 303, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (304, 304, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (305, 305, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (306, 306, N'NG1盒电池计数清零失败')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (307, 307, N'NG2盒电池计数清零失败')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (308, 308, N'下料排序拉带电池溢出')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (309, 309, N'下料排序拉带电池溢出')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (310, 310, N'自动走定长时出料排序拉带电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (311, 311, N'出料拉带1电池溢出')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (312, 312, N'下料排序拉带电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (313, 313, N'出料拉带1电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (314, 314, N'出料拉带2电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (315, 315, N'出料拉带2电池溢出')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (316, 316, N'出料模组1电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (317, 317, N'出料模组2电机报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (318, 318, N'出料模组1气缸收回异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (319, 319, N'出料模组1气缸伸出超时')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (320, 320, N'转移模组真空异常')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (321, 321, N'出料模组2气缸伸出异常 ')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (322, 322, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (323, 323, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (324, 324, N'备用')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (325, 325, N'出料机器人报警')
INSERT [dbo].[AlarmRule] ([Id], [PlcIndex], [AlarmContent]) VALUES (326, 365, N'备用')
SET IDENTITY_INSERT [dbo].[AlarmRule] OFF
/****** Object:  Table [dbo].[tb_TemperatureInfo]    Script Date: 01/09/2020 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_TemperatureInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordTime] [datetime] NULL,
	[ControlValue] [decimal](8, 2) NULL,
	[PatrolValue] [decimal](8, 2) NULL,
	[LayerNum] [int] NULL,
	[CarrierId] [int] NOT NULL,
	[StationNum] [int] NULL,
 CONSTRAINT [PK__tb_Tempe__3214EC070CBAE877] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_UserInfo]    Script Date: 01/09/2020 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_UserInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[UserPassword] [varchar](50) NOT NULL,
	[CreateTime] [datetime] NULL,
	[FK_RoleInfo_Id] [int] NOT NULL,
	[IsActived] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tb_UserInfo] ON
INSERT [dbo].[tb_UserInfo] ([Id], [UserName], [UserPassword], [CreateTime], [FK_RoleInfo_Id], [IsActived]) VALUES (1, N'SuperAdmin', N'e10adc3949ba59abbe56e057f20f883e', CAST(0x0000AA9A00A4CB80 AS DateTime), 1, 1)
INSERT [dbo].[tb_UserInfo] ([Id], [UserName], [UserPassword], [CreateTime], [FK_RoleInfo_Id], [IsActived]) VALUES (2, N'Admin', N'fb326880ec770c22086196b8013995af', CAST(0x0000AA9B00F1E14F AS DateTime), 2, 1)
INSERT [dbo].[tb_UserInfo] ([Id], [UserName], [UserPassword], [CreateTime], [FK_RoleInfo_Id], [IsActived]) VALUES (3, N'SPF', N'fb326880ec770c22086196b8013995af', CAST(0x0000AA9B00F239C4 AS DateTime), 3, 1)
INSERT [dbo].[tb_UserInfo] ([Id], [UserName], [UserPassword], [CreateTime], [FK_RoleInfo_Id], [IsActived]) VALUES (4, N'YJB', N'fb326880ec770c22086196b8013995af', CAST(0x0000AAA00142A316 AS DateTime), 4, 1)
INSERT [dbo].[tb_UserInfo] ([Id], [UserName], [UserPassword], [CreateTime], [FK_RoleInfo_Id], [IsActived]) VALUES (5, N'SCYG', N'fb326880ec770c22086196b8013995af', CAST(0x0000AAA00142B717 AS DateTime), 5, 1)
SET IDENTITY_INSERT [dbo].[tb_UserInfo] OFF
/****** Object:  Table [dbo].[tb_RoleMenuBinding]    Script Date: 01/09/2020 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_RoleMenuBinding](
	[FK_RoleInfo_Id] [int] NOT NULL,
	[FK_MenuInfo_Id] [int] NOT NULL
) ON [PRIMARY]
GO
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 101)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 102)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 201)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 202)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 301)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 302)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 303)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 304)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 401)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 501)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 502)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 503)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 1001)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 1002)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 1003)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 1101)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 1102)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 1103)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 1104)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 1201)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 1202)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 101)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 102)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 201)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 202)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 301)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 302)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 303)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 304)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 401)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 501)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 502)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 503)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 1001)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 1003)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 1101)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 1102)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 1103)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 1104)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 1201)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 1202)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 101)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 102)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 201)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 202)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 301)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 302)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 303)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 304)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 401)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 501)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 502)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 503)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 1101)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 1102)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 1103)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 1104)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 1201)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 1202)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (4, 101)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (4, 102)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (4, 201)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (4, 202)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (4, 301)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (4, 302)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (4, 303)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (4, 304)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (4, 401)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (4, 501)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (4, 502)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (4, 503)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (4, 1104)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (4, 1201)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (4, 1202)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (5, 101)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (5, 201)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (5, 301)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (5, 302)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (5, 303)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (5, 304)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (5, 401)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (5, 501)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (5, 502)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (5, 503)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (5, 1201)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (5, 1202)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 1002)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (2, 103)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (3, 103)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 103)
INSERT [dbo].[tb_RoleMenuBinding] ([FK_RoleInfo_Id], [FK_MenuInfo_Id]) VALUES (1, 305)
/****** Object:  Default [DF_tb_MenuInfo_IsActived]    Script Date: 01/09/2020 11:44:04 ******/
ALTER TABLE [dbo].[tb_MenuInfo] ADD  CONSTRAINT [DF_tb_MenuInfo_IsActived]  DEFAULT ((1)) FOR [IsActived]
GO
/****** Object:  ForeignKey [FK__tb_RoleMe__FK_Ro__24927208]    Script Date: 01/09/2020 11:44:04 ******/
ALTER TABLE [dbo].[tb_RoleMenuBinding]  WITH CHECK ADD FOREIGN KEY([FK_RoleInfo_Id])
REFERENCES [dbo].[tb_RoleInfo] ([Id])
GO
/****** Object:  ForeignKey [fk_role_menu]    Script Date: 01/09/2020 11:44:04 ******/
ALTER TABLE [dbo].[tb_RoleMenuBinding]  WITH CHECK ADD  CONSTRAINT [fk_role_menu] FOREIGN KEY([FK_MenuInfo_Id])
REFERENCES [dbo].[tb_MenuInfo] ([Id])
GO
ALTER TABLE [dbo].[tb_RoleMenuBinding] CHECK CONSTRAINT [fk_role_menu]
GO
/****** Object:  ForeignKey [FK__tb_UserIn__FK_Ro__1ED998B2]    Script Date: 01/09/2020 11:44:04 ******/
ALTER TABLE [dbo].[tb_UserInfo]  WITH CHECK ADD FOREIGN KEY([FK_RoleInfo_Id])
REFERENCES [dbo].[tb_RoleInfo] ([Id])
GO
