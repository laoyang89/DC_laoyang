USE [BYD_DB]
GO
/****** Object:  Table [dbo].[EquipmentStatus]    Script Date: 11/24/2020 13:35:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EquipmentStatus](
	[SystemAutoID] [bigint] IDENTITY(1,1) NOT NULL,
	[Eno] [varchar](20) NOT NULL,
	[Status] [varchar](20) NOT NULL,
	[Stime] [datetime] NOT NULL,
	[Remark] [varchar](50) NULL,
 CONSTRAINT [PK_EquipmentStatus] PRIMARY KEY CLUSTERED 
(
	[SystemAutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EquipmentParameters]    Script Date: 11/24/2020 13:35:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EquipmentParameters](
	[SystemAutoID] [bigint] IDENTITY(1,1) NOT NULL,
	[Eno] [varchar](20) NULL,
	[WorkStationNo] [varchar](50) NOT NULL,
	[LayerNum] [int] NULL,
	[TemperatureSV] [decimal](18, 1) NOT NULL,
	[TemperatureControl] [decimal](18, 1) NULL,
	[TemperaturePV1] [decimal](18, 1) NOT NULL,
	[TemperaturePV2] [decimal](18, 1) NOT NULL,
	[TemperaturePV3] [decimal](18, 1) NULL,
	[VacLimitsMax] [decimal](18, 1) NOT NULL,
	[VacLimitsMin] [decimal](18, 1) NOT NULL,
	[VacPV] [decimal](18, 1) NOT NULL,
	[SamplingTime] [datetime] NULL,
	[CarSystemID] [bigint] NOT NULL,
	[Remark] [varchar](50) NULL,
 CONSTRAINT [PK_EquipmentParameters] PRIMARY KEY CLUSTERED 
(
	[SystemAutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CarDistribution]    Script Date: 11/24/2020 13:35:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CarDistribution](
	[SystemAutoID] [bigint] IDENTITY(1,1) NOT NULL,
	[Eno] [varchar](20) NULL,
	[CarNo] [varchar](50) NOT NULL,
	[LoadingTime] [datetime] NULL,
	[EnterTime] [datetime] NULL,
	[OutTime] [datetime] NULL,
	[UnloadTime] [datetime] NULL,
	[SamplingTime] [datetime] NULL,
 CONSTRAINT [PK_CarDistribution] PRIMARY KEY CLUSTERED 
(
	[SystemAutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BatteryLoadBindings]    Script Date: 11/24/2020 13:35:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BatteryLoadBindings](
	[SystemAutoID] [bigint] IDENTITY(1,1) NOT NULL,
	[Eno] [varchar](20) NULL,
	[ScanTime] [datetime] NULL,
	[PLotNo] [varchar](50) NOT NULL,
	[RankCode] [int] NULL,
	[LayerNumber] [int] NULL,
	[RowNum] [int] NULL,
	[ColumnNum] [int] NULL,
	[CarSystemID] [bigint] NOT NULL,
	[Remark] [varchar](50) NULL,
 CONSTRAINT [PK_BatteryLoadBindings] PRIMARY KEY CLUSTERED 
(
	[SystemAutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlarmRecord]    Script Date: 11/24/2020 13:35:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AlarmRecord](
	[SystemAutoID] [bigint] IDENTITY(1,1) NOT NULL,
	[Eno] [varchar](20) NULL,
	[Status] [varchar](10) NOT NULL,
	[AlarmCode] [varchar](20) NOT NULL,
	[Stime] [datetime] NOT NULL,
	[Remark] [varchar](50) NULL,
 CONSTRAINT [PK_AlarmRecord] PRIMARY KEY CLUSTERED 
(
	[SystemAutoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
