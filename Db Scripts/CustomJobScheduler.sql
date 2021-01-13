----------------------------------------------- Job
USE [CustomJobScheduler]
GO

/****** Object:  Table [dbo].[Job]    Script Date: 1/12/2021 10:55:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Job](
	[Id] [uniqueidentifier] NOT NULL,
	[JobKey] [nvarchar](160) NOT NULL,
	[JobName] [nvarchar](100) NOT NULL,
	[OldJobName] [nvarchar](100) NULL,
	[Group] [nvarchar](50) NOT NULL,
	[OldGroup] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[Type] [nvarchar](500) NOT NULL,
	[IsCopy] [bit] NOT NULL,
	[IsNew] [bit] NOT NULL,
	[Recovery] [bit] NOT NULL,
	[JobData] [nvarchar](max) NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Job] ADD  CONSTRAINT [DF_Job_Group]  DEFAULT (N'DEFAULT') FOR [Group]
GO

ALTER TABLE [dbo].[Job] ADD  CONSTRAINT [DF_Job_IsCopy]  DEFAULT ((0)) FOR [IsCopy]
GO

ALTER TABLE [dbo].[Job] ADD  CONSTRAINT [DF_Job_IsNew]  DEFAULT ((1)) FOR [IsNew]
GO

ALTER TABLE [dbo].[Job] ADD  CONSTRAINT [DF_Job_Recovery]  DEFAULT ((1)) FOR [Recovery]
GO


----------------------------------------------- Trigger
USE [CustomJobScheduler]
GO

/****** Object:  Table [dbo].[Trigger]    Script Date: 1/12/2021 10:56:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Trigger](
	[Id] [uniqueidentifier] NOT NULL,
	[TriggerKey] [nvarchar](160) NOT NULL,
	[JobId] [uniqueidentifier] NOT NULL,
	[Type] [smallint] NOT NULL,
	[TriggerName] [nvarchar](100) NOT NULL,
	[OldTriggerName] [nvarchar](100) NULL,
	[TriggerGroup] [nvarchar](50) NOT NULL,
	[OldTriggerGroup] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[IsCopy] [bit] NOT NULL,
	[IsNew] [bit] NOT NULL,
	[Priority] [int] NULL,
	[PriorityOrDefault] [int] NULL,
	[StartTimeUtc] [nvarchar](50) NULL,
	[EndTimeUtc] [nvarchar](50) NULL,
	[DateFormat] [nvarchar](25) NULL,
	[TimeFormat] [nvarchar](25) NULL,
	[DateTimeFormat] [nvarchar](50) NULL,
	[MisFireInstruction] [int] NULL,
	[MisFireInstructionsJson] [nvarchar](max) NULL,
 CONSTRAINT [PK_Trigger] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Trigger] ADD  CONSTRAINT [DF_Trigger_TriggerGroup]  DEFAULT (N'DEFAULT') FOR [TriggerGroup]
GO

ALTER TABLE [dbo].[Trigger] ADD  CONSTRAINT [DF_Trigger_IsCopy]  DEFAULT ((0)) FOR [IsCopy]
GO

ALTER TABLE [dbo].[Trigger] ADD  CONSTRAINT [DF_Trigger_IsNew]  DEFAULT ((1)) FOR [IsNew]
GO

ALTER TABLE [dbo].[Trigger]  WITH CHECK ADD  CONSTRAINT [FK_Trigger_Job] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Trigger] CHECK CONSTRAINT [FK_Trigger_Job]
GO



----------------------------------------------- TriggerType
USE [CustomJobScheduler]
GO

/****** Object:  Table [dbo].[TriggerType]    Script Date: 1/12/2021 10:57:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TriggerType](
	[Id] [uniqueidentifier] NOT NULL,
	[Expression] [nvarchar](50) NULL,
	[TimeZone] [nvarchar](200) NULL,
	[RepeatCount] [int] NULL,
	[RepeatForever] [bit] NULL,
	[RepeatInterval] [int] NULL,
	[RepeatUnit] [smallint] NULL,
	[PreserveHourAcrossDst] [bit] NULL,
	[SkipDayIfHourDoesNotExist] [bit] NULL,
	[AreOnlyWeekdaysEnabled] [bit] NULL,
	[AreOnlyWeekendEnabled] [bit] NULL,
	[Friday] [bit] NULL,
	[Saturday] [bit] NULL,
	[Sunday] [bit] NULL,
	[Monday] [bit] NULL,
	[Tuesday] [bit] NULL,
	[Wednessday] [bit] NULL,
	[Thursday] [bit] NULL,
	[StartTime] [bigint] NULL,
	[EndTime] [bigint] NULL,
 CONSTRAINT [PK_TriggerType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TriggerType] ADD  CONSTRAINT [DF_TriggerType_RepeatForever]  DEFAULT ((1)) FOR [RepeatForever]
GO

ALTER TABLE [dbo].[TriggerType] ADD  CONSTRAINT [DF_TriggerType_PreserveHourAcrossDst]  DEFAULT ((0)) FOR [PreserveHourAcrossDst]
GO

ALTER TABLE [dbo].[TriggerType] ADD  CONSTRAINT [DF_TriggerType_SkipDayIfHourDoesNotExist]  DEFAULT ((0)) FOR [SkipDayIfHourDoesNotExist]
GO

ALTER TABLE [dbo].[TriggerType] ADD  CONSTRAINT [DF_TriggerType_AreOnlyWeekdaysEnabled]  DEFAULT ((0)) FOR [AreOnlyWeekdaysEnabled]
GO

ALTER TABLE [dbo].[TriggerType] ADD  CONSTRAINT [DF_TriggerType_AreOnlyWeekendEnabled]  DEFAULT ((0)) FOR [AreOnlyWeekendEnabled]
GO

ALTER TABLE [dbo].[TriggerType] ADD  CONSTRAINT [DF_TriggerType_Friday]  DEFAULT ((1)) FOR [Friday]
GO

ALTER TABLE [dbo].[TriggerType] ADD  CONSTRAINT [DF_TriggerType_Saturday]  DEFAULT ((1)) FOR [Saturday]
GO

ALTER TABLE [dbo].[TriggerType] ADD  CONSTRAINT [DF_TriggerType_Sunday]  DEFAULT ((1)) FOR [Sunday]
GO

ALTER TABLE [dbo].[TriggerType] ADD  CONSTRAINT [DF_TriggerType_Monday]  DEFAULT ((1)) FOR [Monday]
GO

ALTER TABLE [dbo].[TriggerType] ADD  CONSTRAINT [DF_TriggerType_Tuesday]  DEFAULT ((1)) FOR [Tuesday]
GO

ALTER TABLE [dbo].[TriggerType] ADD  CONSTRAINT [DF_TriggerType_Wednessday]  DEFAULT ((1)) FOR [Wednessday]
GO

ALTER TABLE [dbo].[TriggerType] ADD  CONSTRAINT [DF_TriggerType_Thursday]  DEFAULT ((1)) FOR [Thursday]
GO




