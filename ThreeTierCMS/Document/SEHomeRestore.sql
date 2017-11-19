/*This script only works for SQL Server on Windows, not for linux(Oracle SQL Developer)*/
USE [master]

CREATE DATABASE [SEHome];
GO

USE [SEHome]

/****** Object:  Table [dbo].[seh_websitecatery]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[seh_websitecatery](
	[WebsiteCateryId] [int] IDENTITY(1,1) NOT NULL,
	[WebsiteCateryName] [nvarchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_CMS_SiteCatery] PRIMARY KEY CLUSTERED
(
	[WebsiteCateryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Website Catery Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_websitecatery', @level2type=N'COLUMN',@level2name=N'WebsiteCateryId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Website Catery Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_websitecatery', @level2type=N'COLUMN',@level2name=N'WebsiteCateryName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_websitecatery', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[seh_websitecatery] ON
INSERT [dbo].[seh_websitecatery] ([WebsiteCateryId], [WebsiteCateryName], [Sequence]) VALUES (1, N'SAP', 1)
INSERT [dbo].[seh_websitecatery] ([WebsiteCateryId], [WebsiteCateryName], [Sequence]) VALUES (2, N'编程开发', 2)
INSERT [dbo].[seh_websitecatery] ([WebsiteCateryId], [WebsiteCateryName], [Sequence]) VALUES (3, N'生活游戏', 3)
INSERT [dbo].[seh_websitecatery] ([WebsiteCateryId], [WebsiteCateryName], [Sequence]) VALUES (4, N'工作学习', 4)
INSERT [dbo].[seh_websitecatery] ([WebsiteCateryId], [WebsiteCateryName], [Sequence]) VALUES (5, N'关于-开源控件', 5)
SET IDENTITY_INSERT [dbo].[seh_websitecatery] OFF
/****** Object:  Table [dbo].[seh_website]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[seh_website](
	[WebsiteId] [int] IDENTITY(1,1) NOT NULL,
	[WebsiteName] [nvarchar](50) NOT NULL,
	[WebsiteCateryId] [int] NOT NULL,
	[Description] [nvarchar](100) NULL,
	[URL] [varchar](200) NOT NULL,
	[Hits] [int] NOT NULL,
	[IsDisplay] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedByName] [varchar](50) NOT NULL,
	[UpdatedTime] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[UpdatedByName] [varchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_CMS_Site] PRIMARY KEY CLUSTERED
(
	[WebsiteId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_PADDING OFF

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Website Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'WebsiteId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Website Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'WebsiteName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Website Catery' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'WebsiteCateryId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'Description'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'URL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'URL'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hits' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'Hits'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Display' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'IsDisplay'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'CreatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'CreatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'CreatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'UpdatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'UpdatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'UpdatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[seh_website] ON
INSERT [dbo].[seh_website] ([WebsiteId], [WebsiteName], [WebsiteCateryId], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (1, N'个人公积金', 3, N'查询个人公积金交费情况', N'https://persons.shgjj.com/', 0, 0, CAST(0x0000A03300A8EEC1 AS DateTime), 1, N'admin', CAST(0x0000A04100AB97CE AS DateTime), 1, N'admin', 1)
INSERT [dbo].[seh_website] ([WebsiteId], [WebsiteName], [WebsiteCateryId], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (2, N'个人社保', 3, N'查询个人社保缴费情况，包括养老金等', N'http://www.12333sh.v.cn/200912333/2009wsbs/grbs/shbx/01/200909/t20090917_1085043.shtml', 0, 1, CAST(0x0000A04100ABC414 AS DateTime), 1, N'admin', CAST(0x0000A04100ABC414 AS DateTime), 1, N'admin', 2)
INSERT [dbo].[seh_website] ([WebsiteId], [WebsiteName], [WebsiteCateryId], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (3, N'IMO', 3, N'网页版即时聊天工具，可登录MSN，GTalk等', N'https://imo.im/', 0, 1, CAST(0x0000A04100AC4AA9 AS DateTime), 1, N'admin', CAST(0x0000A04100AC4AA9 AS DateTime), 1, N'admin', 3)
INSERT [dbo].[seh_website] ([WebsiteId], [WebsiteName], [WebsiteCateryId], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (4, N'得益网Netyi.Net', 4, N'得益网NetYi.Net - 电子书', N'http://www.netyi.net/', 0, 1, CAST(0x0000A04100ACA22C AS DateTime), 1, N'admin', CAST(0x0000A04100ACA22C AS DateTime), 1, N'admin', 4)
INSERT [dbo].[seh_website] ([WebsiteId], [WebsiteName], [WebsiteCateryId], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (5, N'皮皮书屋', 4, N'皮皮书屋 - 分享电子书', N'http://www.ppurl.com/', 0, 1, CAST(0x0000A04100AD3AB0 AS DateTime), 1, N'admin', CAST(0x0000A04100AD3AB1 AS DateTime), 1, N'admin', 5)
INSERT [dbo].[seh_website] ([WebsiteId], [WebsiteName], [WebsiteCateryId], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (6, N'regexlib', 2, N'正则表达式', N'http://regexlib.com/', 0, 1, CAST(0x0000A04100AD70D4 AS DateTime), 1, N'admin', CAST(0x0000A04100AD70D4 AS DateTime), 1, N'admin', 6)
INSERT [dbo].[seh_website] ([WebsiteId], [WebsiteName], [WebsiteCateryId], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (7, N'VB To C#', 2, N'代码转换', N'http://www.developerfusion.com/tools/convert/vb-to-csharp/', 0, 1, CAST(0x0000A04100ADCA18 AS DateTime), 1, N'admin', CAST(0x0000A04100ADCA18 AS DateTime), 1, N'admin', 7)
INSERT [dbo].[seh_website] ([WebsiteId], [WebsiteName], [WebsiteCateryId], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (8, N'SAP Corporate Home', 1, N'SAP 内部网首页', N'https://portal.wdf.sap.corp/home', 0, 1, CAST(0x0000A04100B0C104 AS DateTime), 1, N'admin', CAST(0x0000A04100B0C104 AS DateTime), 1, N'admin', 8)
INSERT [dbo].[seh_website] ([WebsiteId], [WebsiteName], [WebsiteCateryId], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (9, N'SAP VPN', 1, N'远程登录', N'http://connectsha.sap.com/', 0, 1, CAST(0x0000A04100B0E929 AS DateTime), 1, N'admin', CAST(0x0000A04100B0E929 AS DateTime), 1, N'admin', 9)
INSERT [dbo].[seh_website] ([WebsiteId], [WebsiteName], [WebsiteCateryId], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (10, N'SAP WTS（内部）', 1, N'SAP远程桌面，适合内部访问', N'https://sgweb01.sap.com/', 0, 1, CAST(0x0000A04100B11037 AS DateTime), 1, N'admin', CAST(0x0000A04100B11037 AS DateTime), 1, N'admin', 10)
INSERT [dbo].[seh_website] ([WebsiteId], [WebsiteName], [WebsiteCateryId], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (11, N'SAP Training Content', 1, N'SAP培训资料库', N'https://sapneth1.wdf.sap.corp/trainingcontent', 0, 1, CAST(0x0000A04100B12A7D AS DateTime), 1, N'admin', CAST(0x0000A04100B12A7D AS DateTime), 1, N'admin', 11)
INSERT [dbo].[seh_website] ([WebsiteId], [WebsiteName], [WebsiteCateryId], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (12, N'Hotel Booker', 1, N'为SAP AG员工提供的酒店预订', N'http://www.hotelbooker.org/', 0, 1, CAST(0x0000A04100B246D2 AS DateTime), 1, N'admin', CAST(0x0000A04100B246D2 AS DateTime), 1, N'admin', 12)
INSERT [dbo].[seh_website] ([WebsiteId], [WebsiteName], [WebsiteCateryId], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (13, N'24x7', 1, N'电子书', N'http://www.books24x7.com/', 0, 1, CAST(0x0000A04100B2839A AS DateTime), 1, N'admin', CAST(0x0000A04100B2839A AS DateTime), 1, N'admin', 13)
INSERT [dbo].[seh_website] ([WebsiteId], [WebsiteName], [WebsiteCateryId], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (14, N'W3CSchool ', 1, N'学习网页编程', N'http://www.w3school.com.cn/', 0, 1, CAST(0x0000A04100B2A26A AS DateTime), 1, N'admin', CAST(0x0000A04100B2A26A AS DateTime), 1, N'admin', 14)
SET IDENTITY_INSERT [dbo].[seh_website] OFF
/****** Object:  Table [dbo].[seh_software]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[seh_software](
	[SoftwareId] [int] IDENTITY(1,1) NOT NULL,
	[SoftwareName] [varchar](200) NOT NULL,
	[ShortDescription] [varchar](500) NULL,
	[Description] [text] NULL,
	[Image] [varchar](200) NULL,
	[Feature1] [varchar](100) NULL,
	[Feature2] [varchar](100) NULL,
	[Feature3] [varchar](100) NULL,
	[Feature4] [varchar](100) NULL,
	[DownloadUrl] [varchar](500) NULL,
	[DocumentTitle] [varchar](100) NULL,
	[DocumentDescription] [varchar](500) NULL,
	[DocumentUrl] [varchar](500) NULL,
	[Hits] [int] NOT NULL,
	[Downloads] [int] NOT NULL,
	[IsDisplay] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedByName] [varchar](50) NOT NULL,
	[UpdatedTime] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[UpdatedByName] [varchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED
(
	[SoftwareId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

SET ANSI_PADDING OFF

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Software Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'SoftwareId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Software Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'SoftwareName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Short Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'ShortDescription'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Description'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Image' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Image'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Feature1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Feature1'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Feature2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Feature2'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Feature3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Feature3'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Feature4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Feature4'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Download Url' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'DownloadUrl'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Document Title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'DocumentTitle'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Document Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'DocumentDescription'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Document Url' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'DocumentUrl'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hits' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Hits'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Downloads' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Downloads'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Display' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'IsDisplay'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'CreatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'CreatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'CreatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'UpdatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'UpdatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'UpdatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[seh_software] ON
INSERT [dbo].[seh_software] ([SoftwareId], [SoftwareName], [ShortDescription], [Description], [Image], [Feature1], [Feature2], [Feature3], [Feature4], [DownloadUrl], [DocumentTitle], [DocumentDescription], [DocumentUrl], [Hits], [Downloads], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (1, N'.NET开发助手', N'基于.NET Framework2.0的一款以代码生成功能为主，附带一些常用工具的工具。', N'基于.NET&nbsp;Framework2.0的一款以代码生成功能为主，附带一些常用工具的工具。基于.NET&nbsp;Framework2.0的一款以代码生成功能为主，附带一些常用工具的工具。基于.NET&nbsp;Framework2.0的一款以代码生成功能为主，附带一些常用工具的工具。基于.NET&nbsp;Framework2.0的一款以代码生成功能为主，附带一些常用工具的工具。基于.NET&nbsp;Framework2.0的一款以代码生成功能为主，附带一些常用工具的工具。基于.NET&nbsp;Framework2.0的一款以代码生成功能为主，附带一些常用工具的工具。基于.NET&nbsp;Framework2.0的一款以代码生成功能为主，附带一些常用工具的工具。基于.NET&nbsp;Framework2.0的一款以代码生成功能为主，附带一些常用工具的工具。基于.NET&nbsp;Framework2.0的一款以代码生成功能为主，附带一些常用工具的工具。', N'images/gxt-thumb.png', N' 代码生成，自定义代码模板', N' 功能完善，操作简单', N' 可扩展', N' 自动升级', N' www.sdfsadfsdf.com/sdfasdfsdf/sdfdsf.zip', N' .NET开发助手用户手册', N' 详细描述了本工具的功能及使用方法。（Word格式）', N' ', 0, 0, 1, CAST(0x0000A0350166371C AS DateTime), 1, N'admin', CAST(0x0000A11A0137E3DE AS DateTime), 1, N'admin', 4)
INSERT [dbo].[seh_software] ([SoftwareId], [SoftwareName], [ShortDescription], [Description], [Image], [Feature1], [Feature2], [Feature3], [Feature4], [DownloadUrl], [DocumentTitle], [DocumentDescription], [DocumentUrl], [Hits], [Downloads], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (2, N'开心助手V2.0（多账户版）', N'专为开心网(www.kaixin001.com)而开发的外挂工具。', N'&lt;p&gt;专为开心网(&lt;a&nbsp;href="http://www.kaixin001.com"&gt;www.kaixin001.com&lt;/a&gt;)而开发的外挂工具。主要功能包括争车位，咬人，买卖奴隶，买房子，花园，牧场，超级大亨。&lt;/p&gt;
<br>&lt;p&gt;并附带各种实用工具。&lt;/p&gt;', N' images/gxt-thumb.png', N' 多账户，多任务，容易上手', N' 功能强大，包括了各种主流游戏', N' 附带多种工具，方便使用', N' 自动升级', N' www.ogle.com/download.aspx', N'开心助手用户手册', N' 详细描述了本外挂的功能及使用方法。（Word格式）', N' ', 0, 0, 1, CAST(0x0000A03501665758 AS DateTime), 1, N'admin', CAST(0x0000A11A0138095E AS DateTime), 1, N'admin', 3)
INSERT [dbo].[seh_software] ([SoftwareId], [SoftwareName], [ShortDescription], [Description], [Image], [Feature1], [Feature2], [Feature3], [Feature4], [DownloadUrl], [DocumentTitle], [DocumentDescription], [DocumentUrl], [Hits], [Downloads], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (3, N'CMS内容管理系统V1.0', N'基于.NET三层架构的后台内容管理系统。主要包括用户管理，权限管理，文章管理等功能。本网站的后台即为此内容管理系统。', N'基于.NET三层架构的后台内容管理系统。主要包括用户管理，权限管理，文章管理等功能。本网站的后台即为此内容管理系统。基于.NET三层架构的后台内容管理系统。主要包括用户管理，权限管理，文章管理等功能。本网站的后台即为此内容管理系统。基于.NET三层架构的后台内容管理系统。主要包括用户管理，权限管理，文章管理等功能。本网站的后台即为此内容管理系统。基于.NET三层架构的后台内容管理系统。主要包括用户管理，权限管理，文章管理等功能。本网站的后台即为此内容管理系统。基于.NET三层架构的后台内容管理系统。主要包括用户管理，权限管理，文章管理等功能。本网站的后台即为此内容管理系统。基于.NET三层架构的后台内容管理系统。主要包括用户管理，权限管理，文章管理等功能。本网站的后台即为此内容管理系统。基于.NET三层架构的后台内容管理系统。主要包括用户管理，权限管理，文章管理等功能。本网站的后台即为此内容管理系统。基于.NET三层架构的后台内容管理系统。主要包括用户管理，权限管理，文章管理等功能。本网站的后台即为此内容管理系统。基于.NET三层架构的后台内容管理系统。主要包括用户管理，权限管理，文章管理等功能。本网站的后台即为此内容管理系统。基于.NET三层架构的后台内容管理系统。主要包括用户管理，权限管理，文章管理等功能。本网站的后台即为此内容管理系统。基于.NET三层架构的后台内容管理系统。主要包括用户管理，权限管理，文章管理等功能。本网站的后台即为此内容管理系统。基于.NET三层架构的后台内容管理系统。主要包括用户管理，权限管理，文章管理等功能。本网站的后台即为此内容管理系统。基于.NET三层架构的后台内容管理系统。主要包括用户管理，权限管理，文章管理等功能。本网站的后台即为此内容管理系统。基于.NET三层架构的后台内容管理系统。主要包括用户管理，权限管理，文章管理等功能。本网站的后台即为此内容管理系统。', N'images/gxt-thumb.png', N' ', N' ', N' ', N' ', N'www.baidu.com/a.jpg', N' CMS内容管理系统', N'  CMS内容管理系统', N' ', 0, 0, 1, CAST(0x0000A03501666D5C AS DateTime), 1, N'admin', CAST(0x0000A11300EF8C34 AS DateTime), 1, N'admin', 1)
INSERT [dbo].[seh_software] ([SoftwareId], [SoftwareName], [ShortDescription], [Description], [Image], [Feature1], [Feature2], [Feature3], [Feature4], [DownloadUrl], [DocumentTitle], [DocumentDescription], [DocumentUrl], [Hits], [Downloads], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (4, N'申报要素收集器', N'抓取申报要素到本地', N'抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地抓取申报要素到本地', N'抓取申报要素到本地', N' ', N' ', N' ', N' ', N'抓取申报要素到本地', N'抓取申报要素到本地', N'抓取申报要素到本地', N' ', 0, 0, 1, CAST(0x0000A11300E87DC7 AS DateTime), 1, N'admin', CAST(0x0000A11300E87DC7 AS DateTime), 1, N'admin', 2)
INSERT [dbo].[seh_software] ([SoftwareId], [SoftwareName], [ShortDescription], [Description], [Image], [Feature1], [Feature2], [Feature3], [Feature4], [DownloadUrl], [DocumentTitle], [DocumentDescription], [DocumentUrl], [Hits], [Downloads], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (5, N'tet', N'ee', N'eesd', N'', N'', N'', N'', N'', N'', N'', N'', N'', 0, 0, 1, CAST(0x0000A4D300C38C9E AS DateTime), 0, N'', CAST(0x0000A4D300C38C9E AS DateTime), 0, N'', 5)
SET IDENTITY_INSERT [dbo].[seh_software] OFF
/****** Object:  Table [dbo].[seh_release]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[seh_release](
	[ReleaseId] [int] IDENTITY(1,1) NOT NULL,
	[SoftwareId] [int] NOT NULL,
	[ReleaseName] [varchar](100) NOT NULL,
	[ReleaseDate] [datetime] NOT NULL,
	[Description] [text] NULL,
	[Hits] [int] NOT NULL,
	[Downloads] [int] NOT NULL,
	[IsDisplay] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedByName] [varchar](50) NOT NULL,
	[UpdatedTime] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[UpdatedByName] [varchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_Software_Release] PRIMARY KEY CLUSTERED
(
	[ReleaseId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

SET ANSI_PADDING OFF

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Release Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'ReleaseId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Software' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'SoftwareId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Release Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'ReleaseName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Release Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'ReleaseDate'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'Description'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hits' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'Hits'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Downloads' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'Downloads'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Display' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'IsDisplay'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'CreatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'CreatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'CreatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'UpdatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'UpdatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'UpdatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[seh_release] ON
INSERT [dbo].[seh_release] ([ReleaseId], [SoftwareId], [ReleaseName], [ReleaseDate], [Description], [Hits], [Downloads], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (1, 1, N'.NET开发助手V1.0.0.123', CAST(0x0000A03100000000 AS DateTime), N'&lt;p&gt;主要更新：&lt;br&nbsp;/&gt;
<br>1.&nbsp;增加了超级大亨游戏。参考（6.8）&lt;br&nbsp;/&gt;
<br>2.&nbsp;更新数据：增加了超级大亨数据。&lt;br&nbsp;/&gt;
<br>3.&nbsp;任务：任务启动后，对其进行的更改在下次执行时会读取，无需重启动任务。（之前必需停止再启动才会读取。）&lt;br&nbsp;/&gt;
<br>4.&nbsp;增加了任务种类：单账号循环，专为玩超级大亨而开发。&lt;br&nbsp;/&gt;
<br>5.&nbsp;钓鱼：增加了出售所有鱼的功能。&lt;br&nbsp;/&gt;
<br>6.&nbsp;修正了一些bug。&lt;br&nbsp;/&gt;
<br>&lt;/p&gt;', 0, 0, 1, CAST(0x0000A03501674FC5 AS DateTime), 1, N'admin', CAST(0x0000A03501676C0F AS DateTime), 1, N'admin', 1)
INSERT [dbo].[seh_release] ([ReleaseId], [SoftwareId], [ReleaseName], [ReleaseDate], [Description], [Hits], [Downloads], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (2, 1, N'.NET开发助手V1.1.0.567', CAST(0x0000A11300000000 AS DateTime), N'&lt;p&gt;主要更新：&lt;br&nbsp;/&gt;
<br>1.&nbsp;增加了超级大亨游戏。参考（6.8）&lt;br&nbsp;/&gt;
<br>2.&nbsp;更新数据：增加了超级大亨数据。&lt;br&nbsp;/&gt;
<br>3.&nbsp;任务：任务启动后，对其进行的更改在下次执行时会读取，无需重启动任务。（之前必需停止再启动才会读取。）&lt;br&nbsp;/&gt;
<br>4.&nbsp;增加了任务种类：单账号循环，专为玩超级大亨而开发。&lt;br&nbsp;/&gt;
<br>5.&nbsp;钓鱼：增加了出售所有鱼的功能。&lt;br&nbsp;/&gt;
<br>6.&nbsp;修正了一些bug。&lt;br&nbsp;/&gt;
<br>7&nbsp;增加了超级大亨游戏。参考（6.8）&lt;br&nbsp;/&gt;
<br>8.&nbsp;更新数据：增加了超级大亨数据。&lt;br&nbsp;/&gt;
<br>9.&nbsp;任务：任务启动后，对其进行的更改在下次执行时会读取，无需重启动任务。（之前必需停止再启动才会读取。）&lt;br&nbsp;/&gt;
<br>10.&nbsp;增加了任务种类：单账号循环，专为玩超级大亨而开发。&lt;/p&gt;', 0, 0, 1, CAST(0x0000A11301008E61 AS DateTime), 1, N'admin', CAST(0x0000A11301017097 AS DateTime), 1, N'admin', 2)
SET IDENTITY_INSERT [dbo].[seh_release] OFF
/****** Object:  Table [dbo].[seh_opensource]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[seh_opensource](
	[OpenSourceId] [int] IDENTITY(1,1) NOT NULL,
	[OpenSourceName] [nvarchar](50) NOT NULL,
	[ShortDescription] [nvarchar](200) NULL,
	[Description] [text] NOT NULL,
	[URL] [varchar](200) NULL,
	[Hits] [int] NOT NULL,
	[IsDisplay] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedByName] [varchar](50) NOT NULL,
	[UpdatedTime] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[UpdatedByName] [varchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_seh_opensource] PRIMARY KEY CLUSTERED
(
	[OpenSourceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

SET ANSI_PADDING OFF

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'OpenSource Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'OpenSourceId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'OpenSource Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'OpenSourceName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Short Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'ShortDescription'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'Description'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'URL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'URL'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hits' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'Hits'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Display' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'IsDisplay'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'CreatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'CreatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'CreatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'UpdatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'UpdatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'UpdatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[seh_opensource] ON
INSERT [dbo].[seh_opensource] ([OpenSourceId], [OpenSourceName], [ShortDescription], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (1, N'log4.net', N'日志', N'日志日志日志日志', N'http://logging.apache.org/log4net', 0, 1, CAST(0x0000A11A0135E2D4 AS DateTime), 1, N'admin', CAST(0x0000A11A0138E724 AS DateTime), 1, N'admin', 1)
INSERT [dbo].[seh_opensource] ([OpenSourceId], [OpenSourceName], [ShortDescription], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (2, N'FCK Editor', N'网页内容编辑器', N'<p>sdfsdfs;ldfk;skf;skdf</p>', N'http://www.fckeditor.net/', 0, 1, CAST(0x0000A11A01361D0D AS DateTime), 1, N'admin', CAST(0x0000A11A01387397 AS DateTime), 1, N'admin', 2)
INSERT [dbo].[seh_opensource] ([OpenSourceId], [OpenSourceName], [ShortDescription], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (3, N'DockPanel', N'仿Visual Studio Dock控件', N'仿Visual&nbsp;Studio&nbsp;Dock控件仿Visual&nbsp;Studio&nbsp;Dock控件', N'http://localhost:1923/DockPanel', 0, 1, CAST(0x0000A11A01393BB3 AS DateTime), 1, N'admin', CAST(0x0000A11A01393BB3 AS DateTime), 1, N'admin', 3)
INSERT [dbo].[seh_opensource] ([OpenSourceId], [OpenSourceName], [ShortDescription], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (4, N'ExtendedWebBrowser2', N'浏览器控件', N'浏览器控件浏览器控件', N'http://localhost:1923/ExtendedWebBrowser2', 0, 1, CAST(0x0000A11A01396A75 AS DateTime), 1, N'admin', CAST(0x0000A11A01396A75 AS DateTime), 1, N'admin', 4)
INSERT [dbo].[seh_opensource] ([OpenSourceId], [OpenSourceName], [ShortDescription], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (5, N'RichTextEditor', N'文本编辑器', N'文本编辑器文本编辑器', N'http://localhost:1923/RichTextEditor', 0, 1, CAST(0x0000A11A01399BEB AS DateTime), 1, N'admin', CAST(0x0000A11A01399BEB AS DateTime), 1, N'admin', 5)
INSERT [dbo].[seh_opensource] ([OpenSourceId], [OpenSourceName], [ShortDescription], [Description], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (6, N'System.Net.Json', N'.Net Json控件', N'&lt;a&nbsp;title=".NET版Json类"&nbsp;style="font:&nbsp;12px/18px&nbsp;verdana,&nbsp;geneva,&nbsp;lucida,&nbsp;&quot;lucida&nbsp;grande&quot;,&nbsp;arial,&nbsp;helvetica,&nbsp;sans-serif;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&nbsp;href="http://localhost:1923/System.Net.Json"&nbsp;target="_blank"&gt;&lt;font&nbsp;color="#0464bb"&gt;System.Net.Json&lt;/font&gt;&lt;/a&gt;&lt;a&nbsp;title=".NET版Json类"&nbsp;style="font:&nbsp;12px/18px&nbsp;verdana,&nbsp;geneva,&nbsp;lucida,&nbsp;&quot;lucida&nbsp;grande&quot;,&nbsp;arial,&nbsp;helvetica,&nbsp;sans-serif;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&nbsp;href="http://localhost:1923/System.Net.Json"&nbsp;target="_blank"&gt;&lt;font&nbsp;color="#0464bb"&gt;System.Net.Json&lt;/font&gt;&lt;/a&gt;', N'http://localhost:1923/System.Net.Json', 0, 1, CAST(0x0000A11A0139CD66 AS DateTime), 1, N'admin', CAST(0x0000A11A0139CD66 AS DateTime), 1, N'admin', 6)
SET IDENTITY_INSERT [dbo].[seh_opensource] OFF
/****** Object:  Table [dbo].[seh_channel]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[seh_channel](
	[ChannelId] [int] IDENTITY(1,1) NOT NULL,
	[ChannelName] [nvarchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_C_NewsCatery] PRIMARY KEY CLUSTERED
(
	[ChannelId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Channel Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_channel', @level2type=N'COLUMN',@level2name=N'ChannelId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Channel Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_channel', @level2type=N'COLUMN',@level2name=N'ChannelName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_channel', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[seh_channel] ON
INSERT [dbo].[seh_channel] ([ChannelId], [ChannelName], [Sequence]) VALUES (1, N'热点转贴1', 0)
INSERT [dbo].[seh_channel] ([ChannelId], [ChannelName], [Sequence]) VALUES (2, N'.NET2', 1)
INSERT [dbo].[seh_channel] ([ChannelId], [ChannelName], [Sequence]) VALUES (3, N'SAP34', 2)
INSERT [dbo].[seh_channel] ([ChannelId], [ChannelName], [Sequence]) VALUES (4, N'Flex5', 3)
INSERT [dbo].[seh_channel] ([ChannelId], [ChannelName], [Sequence]) VALUES (5, N'关于-最佳实践', 4)
INSERT [dbo].[seh_channel] ([ChannelId], [ChannelName], [Sequence]) VALUES (6, N'软件项目7', 5)
INSERT [dbo].[seh_channel] ([ChannelId], [ChannelName], [Sequence]) VALUES (7, N'留言板8', 6)
INSERT [dbo].[seh_channel] ([ChannelId], [ChannelName], [Sequence]) VALUES (8, N'关于站长9', 7)
INSERT [dbo].[seh_channel] ([ChannelId], [ChannelName], [Sequence]) VALUES (9, N'互联网搜索', 8)
SET IDENTITY_INSERT [dbo].[seh_channel] OFF
/****** Object:  Table [dbo].[seh_bulletin]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[seh_bulletin](
	[BulletinId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Content] [text] NOT NULL,
	[URL] [varchar](200) NULL,
	[Hits] [int] NOT NULL,
	[IsDisplay] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedByName] [varchar](50) NOT NULL,
	[UpdatedTime] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[UpdatedByName] [varchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_C_Bulletin] PRIMARY KEY CLUSTERED
(
	[BulletinId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

SET ANSI_PADDING OFF

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bulletin Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'BulletinId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'Title'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Content' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'Content'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'URL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'URL'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hits' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'Hits'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IsDisplay' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'IsDisplay'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'CreatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'CreatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'CreatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'UpdatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'UpdatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'UpdatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[seh_bulletin] ON
INSERT [dbo].[seh_bulletin] ([BulletinId], [Title], [Content], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (1, N'Johnny的个人网站发布啦！', N'sdafasd', N'', 0, 0, CAST(0x00009CF300F1D04C AS DateTime), 2, N'admin', CAST(0x0000A0340160766B AS DateTime), 1, N'admin', 1)
INSERT [dbo].[seh_bulletin] ([BulletinId], [Title], [Content], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (2, N'开心助手最新版本V2.5.6.1228', N'开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228开心助手最新版本V2.5.6.1228', N'', 0, 0, CAST(0x00009CF300F1D880 AS DateTime), 2, N'admin', CAST(0x00009E1700836328 AS DateTime), 2, N'admin', 2)
INSERT [dbo].[seh_bulletin] ([BulletinId], [Title], [Content], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (3, N'请关注网盘中的资源，不断更新中', N'请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中请关注网盘中的资源，不断更新中', N'', 0, 0, CAST(0x00009CF300F1DE5C AS DateTime), 2, N'admin', CAST(0x00009E17008374BC AS DateTime), 2, N'admin', 3)
INSERT [dbo].[seh_bulletin] ([BulletinId], [Title], [Content], [URL], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (4, N'是大幅度萨芬', N'但是杀跌方式', N'', 0, 1, CAST(0x0000A03401606626 AS DateTime), 1, N'admin', CAST(0x0000A03401606626 AS DateTime), 1, N'admin', 4)
SET IDENTITY_INSERT [dbo].[seh_bulletin] OFF
/****** Object:  Table [dbo].[seh_blogcatery]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[seh_blogcatery](
	[BlogCateryId] [int] IDENTITY(1,1) NOT NULL,
	[BlogCateryName] [nvarchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_C_BlogCatery] PRIMARY KEY CLUSTERED
(
	[BlogCateryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Blog Catery Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blogcatery', @level2type=N'COLUMN',@level2name=N'BlogCateryId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Blog Catery Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blogcatery', @level2type=N'COLUMN',@level2name=N'BlogCateryName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blogcatery', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[seh_blogcatery] ON
INSERT [dbo].[seh_blogcatery] ([BlogCateryId], [BlogCateryName], [Sequence]) VALUES (1, N'我的生活', 1)
INSERT [dbo].[seh_blogcatery] ([BlogCateryId], [BlogCateryName], [Sequence]) VALUES (2, N'SAP', 2)
INSERT [dbo].[seh_blogcatery] ([BlogCateryId], [BlogCateryName], [Sequence]) VALUES (3, N'dotNet', 3)
INSERT [dbo].[seh_blogcatery] ([BlogCateryId], [BlogCateryName], [Sequence]) VALUES (4, N'宝宝', 4)
SET IDENTITY_INSERT [dbo].[seh_blogcatery] OFF
/****** Object:  Table [dbo].[seh_blog]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[seh_blog](
	[BlogId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[BlogCateryId] [int] NOT NULL,
	[Tag] [varchar](100) NULL,
	[Content] [text] NOT NULL,
	[Hits] [int] NOT NULL,
	[IsDisplay] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedByName] [varchar](50) NOT NULL,
	[UpdatedTime] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[UpdatedByName] [varchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_C_Blog] PRIMARY KEY CLUSTERED
(
	[BlogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

SET ANSI_PADDING OFF

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Blog Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'BlogId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'Title'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Blog Catery' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'BlogCateryId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'Tag'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Content' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'Content'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hits' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'Hits'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Display' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'IsDisplay'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'CreatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'CreatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'CreatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'UpdatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'UpdatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'UpdatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[seh_blog] ON
INSERT [dbo].[seh_blog] ([BlogId], [Title], [BlogCateryId], [Tag], [Content], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (1, N'经过几个月的努力，自己的第一个个人站点终于搭建完了', 1, N'', N'<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 经过几个月的努力，自己的第一个个人站点终于搭建完了。虽然现在已经不做.NET了，但对于技术的热爱还是那么执着。其实很早就有拥有属于自己网站的想法。但由于各种原因，始终没有进一步的实践行动。一方面是由于生活、工作等压力，没有时间去做。另一方面，我对技术有一种趋于完美的追求，总想着我的网站应该有这样、那样的功能。虽然构思了很多想法，但却最多是搜集一些材料，却未付诸行动。其实很多事情并不都是完美的。先建立一个原型，然后再慢慢完善是现在一个可操作的方法。</p>
<p>我把这个网站看作是自己在网络上的一个家，没事来看看，更新一些内容，记录一些生活，工作中的事情。当自己退休或者老去的时候，来回顾一下自己的人生。这或许是我对于.NET最后的实践。毕竟做了近5年的.NET的开发，总归要留下点什么。</p>
<p> 还有一个创建这个站点的原因是，我经常使用各类不同的网站来获得信息或者存储资料。但纷繁凌乱，而且会碰到或多或少的限制。如果有属于自己的站点，凭借自己的努力，开发出符合自己实际需求的功能，那不失为一件很有成就感的事。</p>', 0, 1, CAST(0x0000A0340112AEEF AS DateTime), 1, N'admin', CAST(0x0000A1130143E8FC AS DateTime), 1, N'admin', 1)
INSERT [dbo].[seh_blog] ([BlogId], [Title], [BlogCateryId], [Tag], [Content], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (2, N'ABAP Performance', 2, N'', N'ABAP&nbsp;PerformanceABAP&nbsp;PerformanceABAP&nbsp;PerformanceABAP&nbsp;PerformanceABAP&nbsp;PerformanceABAP&nbsp;PerformanceABAP&nbsp;PerformanceABAP&nbsp;Performance', 0, 1, CAST(0x0000A113013AE307 AS DateTime), 1, N'admin', CAST(0x0000A113013AE307 AS DateTime), 1, N'admin', 2)
INSERT [dbo].[seh_blog] ([BlogId], [Title], [BlogCateryId], [Tag], [Content], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (3, N'C#性能', 3, N'', N'C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能C#性能', 0, 1, CAST(0x0000A113013B0F3D AS DateTime), 1, N'admin', CAST(0x0000A113013B0F3D AS DateTime), 1, N'admin', 3)
INSERT [dbo].[seh_blog] ([BlogId], [Title], [BlogCateryId], [Tag], [Content], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (4, N'宝宝出生啦', 4, N'', N'宝宝出生啦宝宝出生啦宝宝出生啦宝宝出生啦宝宝出生啦宝宝出生啦', 0, 1, CAST(0x0000A113013B6890 AS DateTime), 1, N'admin', CAST(0x0000A113013B6890 AS DateTime), 1, N'admin', 4)
INSERT [dbo].[seh_blog] ([BlogId], [Title], [BlogCateryId], [Tag], [Content], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (5, N'宝宝理发了', 4, N'', N'宝宝出生啦宝宝出生啦宝宝出生啦宝宝出生啦', 0, 1, CAST(0x0000A113013B7A0F AS DateTime), 1, N'admin', CAST(0x0000A113013B7A0F AS DateTime), 1, N'admin', 5)
SET IDENTITY_INSERT [dbo].[seh_blog] OFF
/****** Object:  Table [dbo].[seh_bestpractice]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[seh_bestpractice](
	[BestPracticeId] [int] IDENTITY(1,1) NOT NULL,
	[BestPracticeName] [nvarchar](50) NOT NULL,
	[ShortDescription] [nvarchar](200) NULL,
	[Description] [text] NOT NULL,
	[Hits] [int] NOT NULL,
	[IsDisplay] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedByName] [varchar](50) NOT NULL,
	[UpdatedTime] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[UpdatedByName] [varchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_seh_bestpractice] PRIMARY KEY CLUSTERED
(
	[BestPracticeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

SET ANSI_PADDING OFF

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Best Practice Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'BestPracticeId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Best Practice Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'BestPracticeName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Short Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'ShortDescription'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'Description'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hits' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'Hits'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Display' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'IsDisplay'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CreatedTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'CreatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'CreatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'CreatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'UpdatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'UpdatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'UpdatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[seh_bestpractice] ON
INSERT [dbo].[seh_bestpractice] ([BestPracticeId], [BestPracticeName], [ShortDescription], [Description], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (1, N'网站统计-ogle Analysics', N'网站统计-ogle Analysics', N'&lt;a&nbsp;class="list_link"&nbsp;id="ctl00_cphPage_myManageGridView_ctl08_lblTitle"&nbsp;style="font:&nbsp;12px/normal&nbsp;arial;&nbsp;height:&nbsp;25px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(57,&nbsp;85,&nbsp;117);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;10px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;font&nbsp;color="#395575"&gt;网站统计-ogle&nbsp;Analysics&lt;/font&gt;&lt;/a&gt;', 0, 1, CAST(0x0000A15C01594883 AS DateTime), 1, N'admin', CAST(0x0000A15C01594883 AS DateTime), 1, N'admin', 1)
INSERT [dbo].[seh_bestpractice] ([BestPracticeId], [BestPracticeName], [ShortDescription], [Description], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (2, N'图片防盗链', N'图片防盗链', N'&lt;a&nbsp;class="list_link"&nbsp;id="ctl00_cphPage_myManageGridView_ctl07_lblTitle"&nbsp;style="font:&nbsp;12px/normal&nbsp;arial;&nbsp;height:&nbsp;25px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(57,&nbsp;85,&nbsp;117);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;10px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(235,&nbsp;243,&nbsp;253);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;font&nbsp;color="#395575"&gt;图片防盗链&lt;/font&gt;&lt;/a&gt;', 0, 1, CAST(0x0000A15C01596035 AS DateTime), 1, N'admin', CAST(0x0000A15C01596035 AS DateTime), 1, N'admin', 2)
INSERT [dbo].[seh_bestpractice] ([BestPracticeId], [BestPracticeName], [ShortDescription], [Description], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (3, N'URL重定向(URL Redirecte)', N'URL重定向(URL Redirecte)', N'&lt;a&nbsp;class="list_link"&nbsp;id="ctl00_cphPage_myManageGridView_ctl06_lblTitle"&nbsp;style="font:&nbsp;12px/20px&nbsp;arial;&nbsp;height:&nbsp;25px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(255,&nbsp;0,&nbsp;0);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;10px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;text-decoration:&nbsp;underline;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;249,&nbsp;232);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;font&nbsp;color="#ff0000"&gt;URL重定向(URL&nbsp;Redirecte)&lt;/font&gt;&lt;/a&gt;', 0, 1, CAST(0x0000A15C01596DBB AS DateTime), 1, N'admin', CAST(0x0000A15C01596DBB AS DateTime), 1, N'admin', 3)
INSERT [dbo].[seh_bestpractice] ([BestPracticeId], [BestPracticeName], [ShortDescription], [Description], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (4, N'静态页面生成', N'静态页面生成', N'&lt;a&nbsp;class="list_link"&nbsp;id="ctl00_cphPage_myManageGridView_ctl05_lblTitle"&nbsp;style="font:&nbsp;12px/normal&nbsp;arial;&nbsp;height:&nbsp;25px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(57,&nbsp;85,&nbsp;117);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;10px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(235,&nbsp;243,&nbsp;253);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;font&nbsp;color="#395575"&gt;静态页面生成&lt;/font&gt;&lt;/a&gt;', 0, 1, CAST(0x0000A15C015978AD AS DateTime), 1, N'admin', CAST(0x0000A15C015978AD AS DateTime), 1, N'admin', 4)
INSERT [dbo].[seh_bestpractice] ([BestPracticeId], [BestPracticeName], [ShortDescription], [Description], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (5, N'js框架-Extjs', N'js框架-Extjs', N'&lt;a&nbsp;class="list_link"&nbsp;id="ctl00_cphPage_myManageGridView_ctl04_lblTitle"&nbsp;style="font:&nbsp;12px/normal&nbsp;arial;&nbsp;height:&nbsp;25px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(57,&nbsp;85,&nbsp;117);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;10px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;249,&nbsp;232);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;font&nbsp;color="#395575"&gt;js框架-Extjs&lt;/font&gt;&lt;/a&gt;', 0, 1, CAST(0x0000A15C0159825F AS DateTime), 1, N'admin', CAST(0x0000A15C0159825F AS DateTime), 1, N'admin', 5)
INSERT [dbo].[seh_bestpractice] ([BestPracticeId], [BestPracticeName], [ShortDescription], [Description], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (6, N'统一异常错误处理', N'统一异常错误处理', N'&lt;a&nbsp;class="list_link"&nbsp;id="ctl00_cphPage_myManageGridView_ctl03_lblTitle"&nbsp;style="font:&nbsp;12px/20px&nbsp;arial;&nbsp;height:&nbsp;25px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(255,&nbsp;0,&nbsp;0);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;10px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;text-decoration:&nbsp;underline;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;249,&nbsp;232);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;font&nbsp;color="#ff0000"&gt;统一异常错误处理&lt;/font&gt;&lt;/a&gt;', 0, 1, CAST(0x0000A15C01598C99 AS DateTime), 1, N'admin', CAST(0x0000A15C01598C99 AS DateTime), 1, N'admin', 6)
INSERT [dbo].[seh_bestpractice] ([BestPracticeId], [BestPracticeName], [ShortDescription], [Description], [Hits], [IsDisplay], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (7, N'网页抓取(Web Scrapping)', N'网页抓取(Web Scrapping)', N'&lt;a&nbsp;class="list_link"&nbsp;id="ctl00_cphPage_myManageGridView_ctl02_lblTitle"&nbsp;style="font:&nbsp;12px/normal&nbsp;arial;&nbsp;height:&nbsp;25px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(57,&nbsp;85,&nbsp;117);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;10px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;249,&nbsp;232);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;font&nbsp;color="#395575"&gt;网页抓取(Web&nbsp;Scrapping)&lt;/font&gt;&lt;/a&gt;', 0, 1, CAST(0x0000A15C0159955B AS DateTime), 1, N'admin', CAST(0x0000A15C0159955B AS DateTime), 1, N'admin', 7)
SET IDENTITY_INSERT [dbo].[seh_bestpractice] OFF
/****** Object:  Table [dbo].[seh_article]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[seh_article](
	[ArticleId] [bigint] IDENTITY(1,1) NOT NULL,
	[ChannelId] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[SubTitle] [nvarchar](200) NULL,
	[KeyWord] [nvarchar](100) NULL,
	[SubContent] [varchar](500) NULL,
	[Content] [text] NOT NULL,
	[FirstImage] [varchar](500) NULL,
	[CopyFrom] [varchar](500) NULL,
	[Hits] [int] NOT NULL,
	[IsTop] [bit] NOT NULL,
	[IsElite] [bit] NOT NULL,
	[IsPic] [bit] NOT NULL,
	[IsPageType] [bit] NOT NULL,
	[IsVerified] [bit] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedByName] [varchar](50) NOT NULL,
	[UpdatedTime] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[UpdatedByName] [varchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED
(
	[ArticleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

SET ANSI_PADDING OFF

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Article Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'ArticleId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Channel Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'ChannelId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'Title'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sub Title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'SubTitle'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'KeyWord' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'KeyWord'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sub Content' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'SubContent'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Content' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'Content'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'First Image' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'FirstImage'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Copy From' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'CopyFrom'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hits' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'Hits'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Top' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'IsTop'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Elite' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'IsElite'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Picture' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'IsPic'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Page Type' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'IsPageType'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Verified' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'IsVerified'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'CreatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'CreatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'CreatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'UpdatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'UpdatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'UpdatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[seh_article] ON
INSERT [dbo].[seh_article] ([ArticleId], [ChannelId], [Title], [SubTitle], [KeyWord], [SubContent], [Content], [FirstImage], [CopyFrom], [Hits], [IsTop], [IsElite], [IsPic], [IsPageType], [IsVerified], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (1, 1, N'johnny的cms发布啦', N'', N'', N'', N'事大法师地方', N'aaa', N'', 0, 0, 1, 1, 0, 1, CAST(0x00009EEE0167E3BB AS DateTime), 1, N'admin', CAST(0x0000A03401125284 AS DateTime), 1, N'admin', 1)
INSERT [dbo].[seh_article] ([ArticleId], [ChannelId], [Title], [SubTitle], [KeyWord], [SubContent], [Content], [FirstImage], [CopyFrom], [Hits], [IsTop], [IsElite], [IsPic], [IsPageType], [IsVerified], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (2, 2, N'js出现中文乱码及VS打开js文件乱码的解决方法', N'', N'', N'', N'&lt;p&nbsp;style="font:&nbsp;13px/23px&nbsp;verdana,&nbsp;&quot;ms&nbsp;song&quot;,&nbsp;宋体,&nbsp;Arial,&nbsp;微软雅黑,&nbsp;Helvetica,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;auto;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(0,&nbsp;0,&nbsp;0);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;使用js经常出现中文汉字无法正常显示的问题，原因一般是编码方式不正确。&lt;br&nbsp;style="margin:&nbsp;0px;&nbsp;padding:&nbsp;0px;"&nbsp;/&gt;
<br>&lt;/p&gt;
<br>&lt;p&nbsp;style="font:&nbsp;13px/23px&nbsp;verdana,&nbsp;&quot;ms&nbsp;song&quot;,&nbsp;宋体,&nbsp;Arial,&nbsp;微软雅黑,&nbsp;Helvetica,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;auto;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(0,&nbsp;0,&nbsp;0);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;首页解决js中文乱码的问题：使用notepad打开js文件，另存为选择编码&ldquo;utf-8&quot;，覆盖之前的文件。这样预览不会出现js中alert、document.write乱码问题；&lt;/p&gt;
<br>&lt;p&nbsp;style="font:&nbsp;13px/23px&nbsp;verdana,&nbsp;&quot;ms&nbsp;song&quot;,&nbsp;宋体,&nbsp;Arial,&nbsp;微软雅黑,&nbsp;Helvetica,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;auto;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(0,&nbsp;0,&nbsp;0);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;img&nbsp;width="633"&nbsp;height="542"&nbsp;style="border-width:&nbsp;0px;&nbsp;margin:&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;border-image:&nbsp;initial;"&nbsp;alt=""&nbsp;src="http://www.51obj.cn/wp-content/uploads/2009-08-15_180604.png"&nbsp;/&gt;&lt;/p&gt;
<br>&lt;p&nbsp;style="font:&nbsp;13px/23px&nbsp;verdana,&nbsp;&quot;ms&nbsp;song&quot;,&nbsp;宋体,&nbsp;Arial,&nbsp;微软雅黑,&nbsp;Helvetica,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;auto;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(0,&nbsp;0,&nbsp;0);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;如果js是在html文件中书写的，要在文件头添加meta的charset元素为utf-8&lt;/p&gt;
<br>&lt;p&nbsp;style="font:&nbsp;13px/23px&nbsp;verdana,&nbsp;&quot;ms&nbsp;song&quot;,&nbsp;宋体,&nbsp;Arial,&nbsp;微软雅黑,&nbsp;Helvetica,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;auto;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(0,&nbsp;0,&nbsp;0);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;meta&nbsp;http-equiv=&quot;Content-Type&quot;&nbsp;content=&quot;text/html;&nbsp;charset=utf-8&quot;&nbsp;/&gt;&nbsp;&lt;span&nbsp;class="Apple-converted-space"&gt;&nbsp;&lt;/span&gt;&lt;br&nbsp;style="margin:&nbsp;0px;&nbsp;padding:&nbsp;0px;"&nbsp;/&gt;
<br>&lt;br&nbsp;style="margin:&nbsp;0px;&nbsp;padding:&nbsp;0px;"&nbsp;/&gt;
<br>其次在上面的基础上，如果使用vs2005打开此js文件会出现汉字又全部乱码显示的问题。解决方法就是在VS&nbsp;2005&nbsp;的设置里面选择自动检测utf-8&lt;/p&gt;
<br>&lt;p&nbsp;style="font:&nbsp;13px/23px&nbsp;verdana,&nbsp;&quot;ms&nbsp;song&quot;,&nbsp;宋体,&nbsp;Arial,&nbsp;微软雅黑,&nbsp;Helvetica,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;auto;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(0,&nbsp;0,&nbsp;0);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;img&nbsp;width="644"&nbsp;height="349"&nbsp;style="border-width:&nbsp;0px;&nbsp;margin:&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;border-image:&nbsp;initial;"&nbsp;alt=""&nbsp;src="http://www.51obj.cn/wp-content/uploads/2009-08-15_180401.png"&nbsp;/&gt;&lt;/p&gt;', N'aaa', N'', 13, 0, 0, 0, 0, 1, CAST(0x0000A03C011ECAB0 AS DateTime), 1, N'admin', CAST(0x0000A03C011ECAB0 AS DateTime), 1, N'admin', 2)
INSERT [dbo].[seh_article] ([ArticleId], [ChannelId], [Title], [SubTitle], [KeyWord], [SubContent], [Content], [FirstImage], [CopyFrom], [Hits], [IsTop], [IsElite], [IsPic], [IsPageType], [IsVerified], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (3, 5, N'网站统计-ogle Analysics', N'网站统计-ogle Analysics', N'', N'', N'&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;网站统计-ogle&nbsp;Analysics&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;
<br>&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;网站统计-ogle&nbsp;Analysics&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;
<br>&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;网站统计-ogle&nbsp;Analysics&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;', N'aaa', N'', 0, 0, 0, 0, 0, 1, CAST(0x0000A11400B6C930 AS DateTime), 1, N'admin', CAST(0x0000A11400B6C930 AS DateTime), 1, N'admin', 3)
INSERT [dbo].[seh_article] ([ArticleId], [ChannelId], [Title], [SubTitle], [KeyWord], [SubContent], [Content], [FirstImage], [CopyFrom], [Hits], [IsTop], [IsElite], [IsPic], [IsPageType], [IsVerified], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (4, 5, N'图片防盗链', N'图片防盗链', N'', N'', N'&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;图片防盗链&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;
<br>&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;图片防盗链&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;', N'aaa', N'', 0, 0, 0, 0, 0, 1, CAST(0x0000A11400B74576 AS DateTime), 1, N'admin', CAST(0x0000A11400B74576 AS DateTime), 1, N'admin', 4)
INSERT [dbo].[seh_article] ([ArticleId], [ChannelId], [Title], [SubTitle], [KeyWord], [SubContent], [Content], [FirstImage], [CopyFrom], [Hits], [IsTop], [IsElite], [IsPic], [IsPageType], [IsVerified], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (5, 5, N'URL重定向(URL Redirecte)', N'', N'', N'', N'&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;URL重定向(URL&nbsp;Redirecte)&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;
<br>&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;URL重定向(URL&nbsp;Redirecte)&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;
<br>&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;URL重定向(URL&nbsp;Redirecte)&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;', N'aaa', N'', 0, 0, 0, 0, 0, 1, CAST(0x0000A11400B759DF AS DateTime), 1, N'admin', CAST(0x0000A11400B759DF AS DateTime), 1, N'admin', 5)
INSERT [dbo].[seh_article] ([ArticleId], [ChannelId], [Title], [SubTitle], [KeyWord], [SubContent], [Content], [FirstImage], [CopyFrom], [Hits], [IsTop], [IsElite], [IsPic], [IsPageType], [IsVerified], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (6, 5, N'静态页面生成', N'', N'', N'', N'&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;静态页面生成&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;
<br>&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;静态页面生成&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;
<br>&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;静态页面生成&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;
<br>&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;静态页面生成&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;', N'aaa', N'', 0, 0, 0, 0, 0, 1, CAST(0x0000A11400B766D9 AS DateTime), 1, N'admin', CAST(0x0000A11400B766D9 AS DateTime), 1, N'admin', 6)
INSERT [dbo].[seh_article] ([ArticleId], [ChannelId], [Title], [SubTitle], [KeyWord], [SubContent], [Content], [FirstImage], [CopyFrom], [Hits], [IsTop], [IsElite], [IsPic], [IsPageType], [IsVerified], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (7, 5, N'js框架-Extjs', N'', N'', N'', N'&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;js框架-Extjs&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;
<br>&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;js框架-Extjs&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;', N'aaa', N'', 0, 0, 0, 0, 0, 1, CAST(0x0000A11400B775B1 AS DateTime), 1, N'admin', CAST(0x0000A11400B775B1 AS DateTime), 1, N'admin', 7)
INSERT [dbo].[seh_article] ([ArticleId], [ChannelId], [Title], [SubTitle], [KeyWord], [SubContent], [Content], [FirstImage], [CopyFrom], [Hits], [IsTop], [IsElite], [IsPic], [IsPageType], [IsVerified], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (8, 5, N'统一异常错误处理', N'统一异常错误处理', N'', N'', N'&lt;h4&nbsp;style="margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;统一异常错误处理&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;
<br>&lt;h4&nbsp;style="margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;统一异常错误处理&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;
<br>&lt;h4&nbsp;style="margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;统一异常错误处理&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;', N'aaa', N'', 0, 0, 0, 0, 0, 1, CAST(0x0000A11400B7810D AS DateTime), 1, N'admin', CAST(0x0000A11400B794BC AS DateTime), 1, N'admin', 8)
INSERT [dbo].[seh_article] ([ArticleId], [ChannelId], [Title], [SubTitle], [KeyWord], [SubContent], [Content], [FirstImage], [CopyFrom], [Hits], [IsTop], [IsElite], [IsPic], [IsPageType], [IsVerified], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (9, 5, N'网页抓取(Web Scrapping)', N'', N'', N'', N'&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;网页抓取(Web&nbsp;Scrapping)&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;
<br>&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;网页抓取(Web&nbsp;Scrapping)&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;
<br>&lt;h4&nbsp;style="font:&nbsp;bold&nbsp;14px/18px&nbsp;????,&nbsp;Tahoma,&nbsp;arial,&nbsp;sans-serif;&nbsp;margin:&nbsp;5px&nbsp;0px;&nbsp;padding:&nbsp;0px;&nbsp;text-align:&nbsp;left;&nbsp;color:&nbsp;rgb(85,&nbsp;85,&nbsp;85);&nbsp;text-transform:&nbsp;none;&nbsp;text-indent:&nbsp;0px;&nbsp;letter-spacing:&nbsp;normal;&nbsp;word-spacing:&nbsp;0px;&nbsp;white-space:&nbsp;normal;&nbsp;orphans:&nbsp;2;&nbsp;widows:&nbsp;2;&nbsp;background-color:&nbsp;rgb(255,&nbsp;255,&nbsp;255);&nbsp;-webkit-text-size-adjust:&nbsp;auto;&nbsp;-webkit-text-stroke-width:&nbsp;0px;"&gt;&lt;a&nbsp;style="color:&nbsp;rgb(4,&nbsp;100,&nbsp;187);"&nbsp;href="http://localhost:1923/saparticledetail.html"&gt;&lt;font&nbsp;color="#0464bb"&gt;网页抓取(Web&nbsp;Scrapping)&lt;/font&gt;&lt;/a&gt;&lt;/h4&gt;', N'aaa', N'', 0, 0, 0, 0, 0, 1, CAST(0x0000A11400B78A0B AS DateTime), 1, N'admin', CAST(0x0000A11400B78A0B AS DateTime), 1, N'admin', 9)
SET IDENTITY_INSERT [dbo].[seh_article] OFF
/****** Object:  Table [dbo].[cms_websettings]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[cms_websettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WebsiteName] [nvarchar](40) NOT NULL,
	[WebsiteTitle] [nvarchar](100) NOT NULL,
	[ShortDescription] [varchar](500) NULL,
	[Tel] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[WebsiteAddress] [varchar](200) NOT NULL,
	[WebsitePath] [varchar](50) NOT NULL,
	[FileSize] [int] NOT NULL,
	[LoPath] [varchar](100) NOT NULL,
	[BannerPath] [varchar](100) NOT NULL,
	[Copyright] [nvarchar](500) NULL,
	[MetaKeywords] [nvarchar](100) NULL,
	[MetaDescription] [nvarchar](400) NULL,
	[IsClosed] [bit] NOT NULL,
	[ClosedInfo] [nvarchar](1000) NULL,
	[UserAgreement] [text] NULL,
	[LoginType] [bit] NOT NULL,
 CONSTRAINT [PK_cms_websettings] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

SET ANSI_PADDING OFF

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号（自动加1）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'Id'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网站名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'WebsiteName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网站标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'WebsiteTitle'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网站简介' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'ShortDescription'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'Tel'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'传真' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'Fax'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子邮件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'Email'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'WebsiteAddress'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安装路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'WebsitePath'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传文件大小' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'FileSize'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LO地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'LoPath'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Banner地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'BannerPath'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版权信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'Copyright'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网站关键词' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'MetaKeywords'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网站描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'MetaDescription'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否关闭网站' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'IsClosed'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关闭网站描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'ClosedInfo'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户协议' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'UserAgreement'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台登陆方式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'LoginType'

SET IDENTITY_INSERT [dbo].[cms_websettings] ON
INSERT [dbo].[cms_websettings] ([Id], [WebsiteName], [WebsiteTitle], [ShortDescription], [Tel], [Fax], [Email], [WebsiteAddress], [WebsitePath], [FileSize], [LoPath], [BannerPath], [Copyright], [MetaKeywords], [MetaDescription], [IsClosed], [ClosedInfo], [UserAgreement], [LoginType]) VALUES (1, N'Johnny之家', N'专业猎头网23232', N'这个网站是一个Web及.NET技术展示的平台。网站采用三层架构，既MODEL,DAL,BLL实现。除了一些基本的内容管理及呈现之外，包括了一些常用的Web技术及其最佳实践。', N'13764971002', N'5879745', N'ajohn_2000zr@hotmail.com', N'www.hrhunter.com', N'/hrhunter', 2048, N'/image/lo.jpg', N'/image/banner.jpg', N'jojostudio copyright', N'猎头|求职|高级人才', N'找工作', 0, N'网站正在维护中', N'你们好2222', 0)
SET IDENTITY_INSERT [dbo].[cms_websettings] OFF
/****** Object:  Table [dbo].[cms_topmenubinding]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[cms_topmenubinding](
	[TopMenuId] [int] NOT NULL,
	[MenuCateryId] [int] NOT NULL
) ON [PRIMARY]

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Top Menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_topmenubinding', @level2type=N'COLUMN',@level2name=N'TopMenuId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Catery' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_topmenubinding', @level2type=N'COLUMN',@level2name=N'MenuCateryId'

INSERT [dbo].[cms_topmenubinding] ([TopMenuId], [MenuCateryId]) VALUES (5, 7)
INSERT [dbo].[cms_topmenubinding] ([TopMenuId], [MenuCateryId]) VALUES (5, 8)
INSERT [dbo].[cms_topmenubinding] ([TopMenuId], [MenuCateryId]) VALUES (5, 9)
INSERT [dbo].[cms_topmenubinding] ([TopMenuId], [MenuCateryId]) VALUES (3, 3)
INSERT [dbo].[cms_topmenubinding] ([TopMenuId], [MenuCateryId]) VALUES (6, 10)
INSERT [dbo].[cms_topmenubinding] ([TopMenuId], [MenuCateryId]) VALUES (1, 4)
INSERT [dbo].[cms_topmenubinding] ([TopMenuId], [MenuCateryId]) VALUES (2, 2)
INSERT [dbo].[cms_topmenubinding] ([TopMenuId], [MenuCateryId]) VALUES (4, 11)
INSERT [dbo].[cms_topmenubinding] ([TopMenuId], [MenuCateryId]) VALUES (3, 4)
INSERT [dbo].[cms_topmenubinding] ([TopMenuId], [MenuCateryId]) VALUES (1, 3)
INSERT [dbo].[cms_topmenubinding] ([TopMenuId], [MenuCateryId]) VALUES (4, 10)
/****** Object:  Table [dbo].[cms_topmenu]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[cms_topmenu](
	[TopMenuId] [int] IDENTITY(1,1) NOT NULL,
	[TopMenuName] [nvarchar](50) NOT NULL,
	[ToolTip] [nvarchar](50) NOT NULL,
	[PageLink] [varchar](100) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_cms_topmenu2] PRIMARY KEY CLUSTERED
(
	[TopMenuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_PADDING OFF

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Top Menu Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_topmenu', @level2type=N'COLUMN',@level2name=N'TopMenuId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Top Menu Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_topmenu', @level2type=N'COLUMN',@level2name=N'TopMenuName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ToolTip' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_topmenu', @level2type=N'COLUMN',@level2name=N'ToolTip'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default Page' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_topmenu', @level2type=N'COLUMN',@level2name=N'PageLink'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_topmenu', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[cms_topmenu] ON
INSERT [dbo].[cms_topmenu] ([TopMenuId], [TopMenuName], [ToolTip], [PageLink], [Sequence]) VALUES (1, N'Shortcut', N'Shortcut', N'seh/articleadd.aspx', 1)
INSERT [dbo].[cms_topmenu] ([TopMenuId], [TopMenuName], [ToolTip], [PageLink], [Sequence]) VALUES (2, N'Member', N'Member', N'access/profile.aspx', 2)
INSERT [dbo].[cms_topmenu] ([TopMenuId], [TopMenuName], [ToolTip], [PageLink], [Sequence]) VALUES (3, N'&nbsp;&nbsp;CMS', N'Content Management', N'seh/articlelist.aspx', 3)
INSERT [dbo].[cms_topmenu] ([TopMenuId], [TopMenuName], [ToolTip], [PageLink], [Sequence]) VALUES (4, N'SEHome', N'SEHome', N'seh/blogcaterylist.aspx', 4)
INSERT [dbo].[cms_topmenu] ([TopMenuId], [TopMenuName], [ToolTip], [PageLink], [Sequence]) VALUES (5, N'System', N'System Panel', N'access/administratorlist.aspx', 6)
SET IDENTITY_INSERT [dbo].[cms_topmenu] OFF
/****** Object:  Table [dbo].[cms_rolepermission]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[cms_rolepermission](
	[RoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL
) ON [PRIMARY]

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Role' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_rolepermission', @level2type=N'COLUMN',@level2name=N'RoleId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_rolepermission', @level2type=N'COLUMN',@level2name=N'PermissionId'

INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 11)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 18)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 12)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 13)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 17)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 1)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 2)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 3)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 4)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 5)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (4, 2)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (4, 3)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 6)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 7)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 8)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 9)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 10)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 16)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 14)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (1, 15)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (2, 11)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (2, 18)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (2, 12)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (2, 13)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (2, 17)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (2, 1)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (2, 2)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (3, 11)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (3, 18)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (3, 12)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (3, 13)
INSERT [dbo].[cms_rolepermission] ([RoleId], [PermissionId]) VALUES (3, 17)
/****** Object:  Table [dbo].[cms_role]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[cms_role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_cms_role] PRIMARY KEY CLUSTERED
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Role Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_role', @level2type=N'COLUMN',@level2name=N'RoleId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Role Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_role', @level2type=N'COLUMN',@level2name=N'RoleName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_role', @level2type=N'COLUMN',@level2name=N'Description'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_role', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[cms_role] ON
INSERT [dbo].[cms_role] ([RoleId], [RoleName], [Description], [Sequence]) VALUES (1, N'System Admin', N'System Administrator', 1)
INSERT [dbo].[cms_role] ([RoleId], [RoleName], [Description], [Sequence]) VALUES (2, N'Website Admin', N'Website Administrator', 2)
INSERT [dbo].[cms_role] ([RoleId], [RoleName], [Description], [Sequence]) VALUES (3, N'News Editor', N'News Editor', 3)
INSERT [dbo].[cms_role] ([RoleId], [RoleName], [Description], [Sequence]) VALUES (4, N'User', N'User', 4)
INSERT [dbo].[cms_role] ([RoleId], [RoleName], [Description], [Sequence]) VALUES (5, N'sdfsdf', N'dsfsdf', 5)
SET IDENTITY_INSERT [dbo].[cms_role] OFF
/****** Object:  Table [dbo].[cms_permissioncatery]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[cms_permissioncatery](
	[PermissionCateryId] [int] IDENTITY(1,1) NOT NULL,
	[PermissionCateryName] [nvarchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_cms_permissioncatery] PRIMARY KEY CLUSTERED
(
	[PermissionCateryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Catery Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_permissioncatery', @level2type=N'COLUMN',@level2name=N'PermissionCateryId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Catery Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_permissioncatery', @level2type=N'COLUMN',@level2name=N'PermissionCateryName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_permissioncatery', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[cms_permissioncatery] ON
INSERT [dbo].[cms_permissioncatery] ([PermissionCateryId], [PermissionCateryName], [Sequence]) VALUES (1, N'Shortcut', 1)
INSERT [dbo].[cms_permissioncatery] ([PermissionCateryId], [PermissionCateryName], [Sequence]) VALUES (2, N'MySpace', 2)
INSERT [dbo].[cms_permissioncatery] ([PermissionCateryId], [PermissionCateryName], [Sequence]) VALUES (3, N'News', 3)
INSERT [dbo].[cms_permissioncatery] ([PermissionCateryId], [PermissionCateryName], [Sequence]) VALUES (4, N'Bulletin', 4)
INSERT [dbo].[cms_permissioncatery] ([PermissionCateryId], [PermissionCateryName], [Sequence]) VALUES (5, N'FriendLinks', 5)
INSERT [dbo].[cms_permissioncatery] ([PermissionCateryId], [PermissionCateryName], [Sequence]) VALUES (6, N'Members', 6)
INSERT [dbo].[cms_permissioncatery] ([PermissionCateryId], [PermissionCateryName], [Sequence]) VALUES (7, N'Accounts', 7)
INSERT [dbo].[cms_permissioncatery] ([PermissionCateryId], [PermissionCateryName], [Sequence]) VALUES (8, N'Menus', 8)
INSERT [dbo].[cms_permissioncatery] ([PermissionCateryId], [PermissionCateryName], [Sequence]) VALUES (9, N'WebsiteSettings', 9)
INSERT [dbo].[cms_permissioncatery] ([PermissionCateryId], [PermissionCateryName], [Sequence]) VALUES (10, N'MasterData', 10)
INSERT [dbo].[cms_permissioncatery] ([PermissionCateryId], [PermissionCateryName], [Sequence]) VALUES (11, N'SEHome', 11)
SET IDENTITY_INSERT [dbo].[cms_permissioncatery] OFF
/****** Object:  Table [dbo].[cms_permission]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[cms_permission](
	[PermissionId] [int] IDENTITY(1,1) NOT NULL,
	[PermissionName] [nvarchar](50) NOT NULL,
	[PermissionCateryId] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_cms_permission] PRIMARY KEY CLUSTERED
(
	[PermissionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_permission', @level2type=N'COLUMN',@level2name=N'PermissionId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_permission', @level2type=N'COLUMN',@level2name=N'PermissionName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Catery' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_permission', @level2type=N'COLUMN',@level2name=N'PermissionCateryId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_permission', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[cms_permission] ON
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (1, N'Administrator', 7, 1)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (2, N'AdminRole', 7, 2)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (3, N'Role', 7, 3)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (4, N'RolePermission', 7, 4)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (5, N'Permission', 7, 5)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (6, N'PermissionCatery', 7, 6)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (7, N'Menu', 8, 7)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (8, N'MenuCatery', 8, 8)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (9, N'TopMenu', 8, 9)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (10, N'MenuBinding', 8, 10)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (11, N'ResetPassword', 2, 11)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (12, N'Channel2', 3, 12)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (13, N'Article', 3, 13)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (14, N'MasterData', 10, 14)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (15, N'SEHome', 11, 15)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (16, N'WebsiteSettings', 9, 16)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (17, N'Bulletin', 4, 17)
INSERT [dbo].[cms_permission] ([PermissionId], [PermissionName], [PermissionCateryId], [Sequence]) VALUES (18, N'Profile', 2, 18)
SET IDENTITY_INSERT [dbo].[cms_permission] OFF
/****** Object:  Table [dbo].[cms_pagebinding]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[cms_pagebinding](
	[PageBindingId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[MenuCateryId] [int] NOT NULL,
	[ListMenuId] [int] NOT NULL,
	[AddMenuId] [int] NOT NULL,
 CONSTRAINT [PK_cms_pagebinding] PRIMARY KEY CLUSTERED
(
	[PageBindingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Page Binding Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_pagebinding', @level2type=N'COLUMN',@level2name=N'PageBindingId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_pagebinding', @level2type=N'COLUMN',@level2name=N'Title'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Catery' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_pagebinding', @level2type=N'COLUMN',@level2name=N'MenuCateryId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'List Page' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_pagebinding', @level2type=N'COLUMN',@level2name=N'ListMenuId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Add Page' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_pagebinding', @level2type=N'COLUMN',@level2name=N'AddMenuId'

SET IDENTITY_INSERT [dbo].[cms_pagebinding] ON
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (1, N'Administrator', 7, 1, 2)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (2, N'Menu', 8, 12, 13)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (3, N'Menu Catery', 8, 14, 15)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (4, N'Top Menu', 8, 16, 17)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (5, N'Top Menu Binding', 8, 18, 18)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (6, N'Permission', 7, 8, 9)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (7, N'Permission Catery', 7, 10, 11)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (8, N'Role', 7, 5, 6)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (9, N'Role Permission', 7, 7, 7)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (10, N'Admin Role', 7, 3, 4)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (11, N'Reset Password', 2, 20, 20)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (12, N'Profile', 2, 50, 50)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (13, N'Article', 3, 23, 24)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (14, N'News', 3, 21, 22)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (15, N'Bulletin', 4, 48, 49)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (16, N'Website Catery', 11, 36, 37)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (17, N'Website', 11, 38, 39)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (18, N'Blog Catery', 11, 40, 41)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (19, N'Blog', 11, 46, 47)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (20, N'Software', 10, 19, 25)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (21, N'Release', 10, 26, 27)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (22, N'Open Source', 11, 51, 52)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (23, N'Best Practice', 11, 53, 54)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (24, N'Page Binding', 8, 55, 56)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (25, N'Website Settings', 9, 42, 42)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (26, N'Email Settings', 9, 43, 43)
INSERT [dbo].[cms_pagebinding] ([PageBindingId], [Title], [MenuCateryId], [ListMenuId], [AddMenuId]) VALUES (27, N'Breviary Settings', 9, 44, 44)
SET IDENTITY_INSERT [dbo].[cms_pagebinding] OFF
/****** Object:  Table [dbo].[cms_navigator]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[cms_navigator](
	[NavigatorId] [int] NOT NULL,
	[NavigatorName] [nvarchar](50) NOT NULL,
	[Url] [varchar](200) NOT NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_cms_navigator] PRIMARY KEY CLUSTERED
(
	[NavigatorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_PADDING OFF

INSERT [dbo].[cms_navigator] ([NavigatorId], [NavigatorName], [Url], [ParentId]) VALUES (1, N'首页', N'index.aspx', 0)
INSERT [dbo].[cms_navigator] ([NavigatorId], [NavigatorName], [Url], [ParentId]) VALUES (2, N'SAP', N'sap.aspx', 1)
INSERT [dbo].[cms_navigator] ([NavigatorId], [NavigatorName], [Url], [ParentId]) VALUES (3, N'.NET', N'dotnet.aspx', 1)
INSERT [dbo].[cms_navigator] ([NavigatorId], [NavigatorName], [Url], [ParentId]) VALUES (4, N'IT行业', N'it.aspx', 1)
INSERT [dbo].[cms_navigator] ([NavigatorId], [NavigatorName], [Url], [ParentId]) VALUES (5, N'软件项目', N'software.aspx', 1)
INSERT [dbo].[cms_navigator] ([NavigatorId], [NavigatorName], [Url], [ParentId]) VALUES (6, N'收藏', N'favorite.aspx', 1)
INSERT [dbo].[cms_navigator] ([NavigatorId], [NavigatorName], [Url], [ParentId]) VALUES (7, N'Blog', N'blog.aspx', 1)
INSERT [dbo].[cms_navigator] ([NavigatorId], [NavigatorName], [Url], [ParentId]) VALUES (8, N'关于', N'about.aspx', 1)
INSERT [dbo].[cms_navigator] ([NavigatorId], [NavigatorName], [Url], [ParentId]) VALUES (9, N'软件介绍', N'softwaredetail.aspx', 5)
INSERT [dbo].[cms_navigator] ([NavigatorId], [NavigatorName], [Url], [ParentId]) VALUES (10, N'下载', N'download.aspx', 5)
INSERT [dbo].[cms_navigator] ([NavigatorId], [NavigatorName], [Url], [ParentId]) VALUES (11, N'历史版本', N'releasehistory.aspx', 5)
INSERT [dbo].[cms_navigator] ([NavigatorId], [NavigatorName], [Url], [ParentId]) VALUES (12, N'实用网址', N'websites.aspx', 6)
INSERT [dbo].[cms_navigator] ([NavigatorId], [NavigatorName], [Url], [ParentId]) VALUES (13, N'博客', N'blogdetail.aspx', 7)
INSERT [dbo].[cms_navigator] ([NavigatorId], [NavigatorName], [Url], [ParentId]) VALUES (14, N'文章', N'articledetail.aspx', 2)
/****** Object:  Table [dbo].[cms_menucatery]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[cms_menucatery](
	[MenuCateryId] [int] IDENTITY(1,1) NOT NULL,
	[MenuCateryName] [nvarchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_cms_menucatery2] PRIMARY KEY CLUSTERED
(
	[MenuCateryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Catery Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menucatery', @level2type=N'COLUMN',@level2name=N'MenuCateryId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Catery Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menucatery', @level2type=N'COLUMN',@level2name=N'MenuCateryName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menucatery', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[cms_menucatery] ON
INSERT [dbo].[cms_menucatery] ([MenuCateryId], [MenuCateryName], [Sequence]) VALUES (1, N'ShortCut', 1)
INSERT [dbo].[cms_menucatery] ([MenuCateryId], [MenuCateryName], [Sequence]) VALUES (2, N'MySpace', 2)
INSERT [dbo].[cms_menucatery] ([MenuCateryId], [MenuCateryName], [Sequence]) VALUES (3, N'News', 3)
INSERT [dbo].[cms_menucatery] ([MenuCateryId], [MenuCateryName], [Sequence]) VALUES (4, N'Bulletin', 5)
INSERT [dbo].[cms_menucatery] ([MenuCateryId], [MenuCateryName], [Sequence]) VALUES (5, N'FriendLinks', 4)
INSERT [dbo].[cms_menucatery] ([MenuCateryId], [MenuCateryName], [Sequence]) VALUES (6, N'Members', 6)
INSERT [dbo].[cms_menucatery] ([MenuCateryId], [MenuCateryName], [Sequence]) VALUES (7, N'Accounts', 7)
INSERT [dbo].[cms_menucatery] ([MenuCateryId], [MenuCateryName], [Sequence]) VALUES (8, N'Menu', 8)
INSERT [dbo].[cms_menucatery] ([MenuCateryId], [MenuCateryName], [Sequence]) VALUES (9, N'WebsiteConfig', 9)
INSERT [dbo].[cms_menucatery] ([MenuCateryId], [MenuCateryName], [Sequence]) VALUES (10, N'SoftwareProjects', 10)
INSERT [dbo].[cms_menucatery] ([MenuCateryId], [MenuCateryName], [Sequence]) VALUES (11, N'SEHome', 11)
INSERT [dbo].[cms_menucatery] ([MenuCateryId], [MenuCateryName], [Sequence]) VALUES (12, N'Management', 12)
SET IDENTITY_INSERT [dbo].[cms_menucatery] OFF
/****** Object:  Table [dbo].[cms_menu]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[cms_menu](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](50) NOT NULL,
	[MenuCateryId] [int] NOT NULL,
	[PageLink] [varchar](100) NOT NULL,
	[ToolTip] [nvarchar](100) NULL,
	[Image] [varchar](200) NULL,
	[PermissionId] [int] NOT NULL,
	[IsDisplay] [bit] NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_cms_menu2] PRIMARY KEY CLUSTERED
(
	[MenuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_PADDING OFF

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'MenuId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'MenuName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Catery' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'MenuCateryId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Page Link' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'PageLink'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ToolTip' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'ToolTip'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Image' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'Image'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'PermissionId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Display in the Menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'IsDisplay'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[cms_menu] ON
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (1, N'Administrator', 7, N'access/administratorlist.aspx', N'', N'', 1, 1, 1)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (2, N'Add Administrator', 7, N'access/administratoradd.aspx', N'', N'', 1, 0, 2)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (3, N'AdminRole', 7, N'access/adminrolelist.aspx', N'', N'', 2, 1, 3)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (4, N'Add Admin Role', 7, N'access/adminroleadd.aspx', N'', N'', 2, 0, 4)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (5, N'Role', 7, N'access/rolelist.aspx', N'', N'', 3, 1, 5)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (6, N'Add Role', 7, N'access/roleadd.aspx', N'', N'', 3, 0, 6)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (7, N'Role Permission', 7, N'access/rolepermission.aspx', N'', N'', 4, 1, 7)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (8, N'Permission', 7, N'access/permissionlist.aspx', N'', N'', 5, 1, 8)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (9, N'Add Permission', 7, N'access/permissionadd.aspx', N'', N'', 5, 0, 9)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (10, N'Permission Catery', 7, N'access/permissioncaterylist.aspx', N'', N'', 6, 1, 10)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (11, N'Add Permission Catery', 7, N'access/permissioncateryadd.aspx', N'', N'', 6, 0, 11)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (12, N'Menu', 8, N'systeminfo/menulist.aspx', N'', N'', 7, 1, 12)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (13, N'Add Menu', 8, N'systeminfo/menuadd.aspx', N'', N'', 7, 0, 13)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (14, N'Menu Catery', 8, N'systeminfo/menucaterylist.aspx', N'', N'', 8, 1, 14)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (15, N'Add Menu Catery', 8, N'systeminfo/menucateryadd.aspx', N'', N'', 8, 0, 15)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (16, N'Top Menu', 8, N'systeminfo/topmenulist.aspx', N'', N'', 9, 1, 16)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (17, N'Add Top Menu', 8, N'systeminfo/topmenuadd.aspx', N'', N'', 9, 0, 17)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (18, N'Top Menu Binding', 8, N'systeminfo/topmenubinding.aspx', N'', N'', 10, 1, 18)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (19, N'Software', 10, N'seh/softwarelist.aspx', N'industry', N'', 15, 1, 19)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (20, N'Reset Password', 2, N'access/passwordreset.aspx', N'', N'', 11, 1, 20)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (21, N'Channel', 3, N'seh/channellist.aspx', N'', N'', 12, 1, 21)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (22, N'Add Channel', 3, N'seh/channeladd.aspx', N'', N'', 12, 0, 22)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (23, N'Article', 3, N'seh/articlelist.aspx', N'', N'', 13, 1, 23)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (24, N'Add Article', 3, N'seh/articleadd.aspx', N'', N'', 13, 1, 24)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (25, N'Add Software', 10, N'seh/softwareadd.aspx', N'', N'', 15, 0, 25)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (26, N'Release', 10, N'seh/releaselist.aspx', N'', N'', 15, 1, 26)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (27, N'Create New Release', 10, N'seh/releaseadd.aspx', N'', N'', 15, 0, 27)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (36, N'Website Catery', 11, N'seh/websitecaterylist.aspx', N'', N'', 15, 1, 36)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (37, N'Add Website Catery', 11, N'seh/websitecateryadd.aspx', N'', N'', 15, 0, 37)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (38, N'Website', 11, N'seh/websitelist.aspx', N'', N'', 15, 1, 38)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (39, N'Add Website', 11, N'seh/websiteadd.aspx', N'', N'', 15, 0, 39)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (40, N'Blog Catery', 11, N'seh/blogcaterylist.aspx', N'', N'', 15, 1, 40)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (41, N'Add Blog Catery', 11, N'seh/blogcateryadd.aspx', N'', N'', 15, 0, 41)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (42, N'Website Settings', 9, N'systeminfo/websettings.aspx', N'', N'', 16, 1, 42)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (43, N'Email Settings', 9, N'systeminfo/mailsettings.aspx', N'', N'', 16, 1, 43)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (44, N'Breviary Settings', 9, N'systeminfo/breviarysettings.aspx', N'', N'', 16, 1, 44)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (45, N'Server Info', 9, N'display.aspx', N'', N'', 16, 1, 45)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (46, N'Blog', 11, N'seh/bloglist.aspx', N'博客列表', N'', 15, 1, 46)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (47, N'Add Blog', 11, N'seh/blogadd.aspx', N'', N'', 15, 0, 47)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (48, N'Bulletin', 4, N'seh/bulletinlist.aspx', N'', N'', 17, 1, 48)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (49, N'Add Bulletin', 4, N'seh/bulletinadd.aspx', N'', N'', 17, 1, 49)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (50, N'Profile', 2, N'access/profile.aspx', N'', N'', 18, 1, 50)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (51, N'Open Source', 10, N'seh/opensourcelist.aspx', N'', N'', 15, 1, 51)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (52, N'Add Open Source', 10, N'seh/opensourceadd.aspx', N'', N'', 15, 0, 52)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (53, N'Best Practice', 11, N'seh/bestpracticelist.aspx', N'最佳实践', N'', 15, 1, 53)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (54, N'Add Best Practice', 11, N'seh/bestpracticeadd.aspx', N'添加最佳实践', N'', 15, 0, 54)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (55, N'Page Binding', 8, N'systeminfo/pagebindinglist.aspx', N'', N'', 7, 1, 55)
INSERT [dbo].[cms_menu] ([MenuId], [MenuName], [MenuCateryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (56, N'Add PageBinding', 8, N'systeminfo/pagebindingadd.aspx', N'', N'', 7, 0, 56)
SET IDENTITY_INSERT [dbo].[cms_menu] OFF
/****** Object:  Table [dbo].[cms_mailsettings]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[cms_mailsettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SmtpServerIP] [varchar](50) NOT NULL,
	[SmtpServerPort] [int] NOT NULL,
	[MailId] [varchar](100) NOT NULL,
	[MailPassword] [varchar](50) NOT NULL,
 CONSTRAINT [PK_cms_mailsettings] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_PADDING OFF

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号（自动加1）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_mailsettings', @level2type=N'COLUMN',@level2name=N'Id'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_mailsettings', @level2type=N'COLUMN',@level2name=N'SmtpServerIP'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Port' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_mailsettings', @level2type=N'COLUMN',@level2name=N'SmtpServerPort'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮件地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_mailsettings', @level2type=N'COLUMN',@level2name=N'MailId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_mailsettings', @level2type=N'COLUMN',@level2name=N'MailPassword'

SET IDENTITY_INSERT [dbo].[cms_mailsettings] ON
INSERT [dbo].[cms_mailsettings] ([Id], [SmtpServerIP], [SmtpServerPort], [MailId], [MailPassword]) VALUES (1, N'smtp.163.com', 8080, N'ajohn@163.com', N'12334')
SET IDENTITY_INSERT [dbo].[cms_mailsettings] OFF
/****** Object:  Table [dbo].[CMS_Log]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[CMS_Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Thread] [varchar](255) NOT NULL,
	[Level] [varchar](50) NOT NULL,
	[Logger] [varchar](255) NOT NULL,
	[Message] [varchar](4000) NOT NULL,
	[Exception] [varchar](2000) NULL
) ON [PRIMARY]

SET ANSI_PADDING OFF

SET IDENTITY_INSERT [dbo].[CMS_Log] ON
INSERT [dbo].[CMS_Log] ([Id], [Date], [Thread], [Level], [Logger], [Message], [Exception]) VALUES (1, CAST(0x00009A340182F20B AS DateTime), N'4516', N'ERROR', N'logger.database', N'test.logger', NULL)
INSERT [dbo].[CMS_Log] ([Id], [Date], [Thread], [Level], [Logger], [Message], [Exception]) VALUES (2, CAST(0x00009A340182F228 AS DateTime), N'4516', N'ERROR', N'logger.database', N'Exception: System.DivideByZeroException
Message: 试图除以零。
Source: TestWeb
   在 TestWeb.WebForm3.Button1_Click(Object sender, EventArgs e) 位置 E:\WorkSpace\Study\TestWeb\TestWeb\WebForm3.aspx.cs:行号 54
', NULL)
INSERT [dbo].[CMS_Log] ([Id], [Date], [Thread], [Level], [Logger], [Message], [Exception]) VALUES (3, CAST(0x00009B10015BB1B6 AS DateTime), N'5536', N'ERROR', N'logger.database', N'test.logger', NULL)
INSERT [dbo].[CMS_Log] ([Id], [Date], [Thread], [Level], [Logger], [Message], [Exception]) VALUES (4, CAST(0x00009B10015BBE41 AS DateTime), N'5536', N'ERROR', N'logger.database', N'Exception: System.DivideByZeroException
Message: 试图除以零。
Source: TestWeb
   在 TestWeb.WebForm3.Button1_Click(Object sender, EventArgs e) 位置 E:\WorkSpace\Study\TestWeb\TestWeb\WebForm3.aspx.cs:行号 54
', NULL)
SET IDENTITY_INSERT [dbo].[CMS_Log] OFF
/****** Object:  Table [dbo].[cms_breviarysettings]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[cms_breviarysettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[PlusWatermark] [bit] NOT NULL,
	[WatermarkType] [bit] NULL,
	[WatermarkImage] [varchar](800) NULL,
	[ImageTransparent] [int] NULL,
	[WatermarkText] [nvarchar](50) NULL,
	[TextTransparent] [int] NULL,
	[WatermarkPosition] [int] NULL,
 CONSTRAINT [PK_cms_breviarysettings] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_PADDING OFF

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号（自动加1）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'Id'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缩略图宽度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'Width'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缩略图高度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'Height'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'添加水印' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'PlusWatermark'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'水印类型：图片水印,文字水印 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'WatermarkType'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'水印图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'WatermarkImage'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片水印透明度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'ImageTransparent'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'水印文字' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'WatermarkText'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文字水印透明度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'TextTransparent'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'水印位置：左上，左中，左下，中上，正中，中下，右上，右中，右下' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'WatermarkPosition'

SET IDENTITY_INSERT [dbo].[cms_breviarysettings] ON
INSERT [dbo].[cms_breviarysettings] ([Id], [Width], [Height], [PlusWatermark], [WatermarkType], [WatermarkImage], [ImageTransparent], [WatermarkText], [TextTransparent], [WatermarkPosition]) VALUES (1, 200, 100, 1, 1, N'', 20, N'by johnny ', 20, 5)
SET IDENTITY_INSERT [dbo].[cms_breviarysettings] OFF
/****** Object:  Table [dbo].[cms_adminrole]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[cms_adminrole](
	[AdminRoleId] [int] IDENTITY(1,1) NOT NULL,
	[AdminId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_cms_adminrole] PRIMARY KEY CLUSTERED
(
	[AdminRoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AdminRoleId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminrole', @level2type=N'COLUMN',@level2name=N'AdminRoleId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Admin Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminrole', @level2type=N'COLUMN',@level2name=N'AdminId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Role Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminrole', @level2type=N'COLUMN',@level2name=N'RoleId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminrole', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[cms_adminrole] ON
INSERT [dbo].[cms_adminrole] ([AdminRoleId], [AdminId], [RoleId], [Sequence]) VALUES (1, 1, 1, 1)
INSERT [dbo].[cms_adminrole] ([AdminRoleId], [AdminId], [RoleId], [Sequence]) VALUES (2, 2, 2, 2)
INSERT [dbo].[cms_adminrole] ([AdminRoleId], [AdminId], [RoleId], [Sequence]) VALUES (3, 3, 4, 3)
SET IDENTITY_INSERT [dbo].[cms_adminrole] OFF
/****** Object:  Table [dbo].[cms_adminloginlog]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[cms_adminloginlog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Password] [varchar](32) NOT NULL,
	[LoginTime] [datetime] NOT NULL,
	[LoutTime] [datetime] NOT NULL,
	[LoginIP] [varchar](50) NOT NULL,
	[HosterName] [varchar](100) NULL,
	[LoginStatus] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_cms_adminloginlog] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_PADDING OFF

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号（自动加1）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminloginlog', @level2type=N'COLUMN',@level2name=N'Id'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'管理员名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminloginlog', @level2type=N'COLUMN',@level2name=N'Name'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminloginlog', @level2type=N'COLUMN',@level2name=N'Password'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminloginlog', @level2type=N'COLUMN',@level2name=N'LoginTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登出时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminloginlog', @level2type=N'COLUMN',@level2name=N'LoutTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录时IP地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminloginlog', @level2type=N'COLUMN',@level2name=N'LoginIP'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录时机器名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminloginlog', @level2type=N'COLUMN',@level2name=N'HosterName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录状况' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminloginlog', @level2type=N'COLUMN',@level2name=N'LoginStatus'

/****** Object:  Table [dbo].[cms_administrator]    Script Date: 11/18/2017 21:07:49 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [dbo].[cms_administrator](
	[AdminId] [int] IDENTITY(1,1) NOT NULL,
	[AdminName] [varchar](50) NOT NULL,
	[Password] [varchar](32) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Gender] [bit] NULL,
	[Tel] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[ValidFrom] [datetime] NOT NULL,
	[ValidTo] [datetime] NOT NULL,
	[IsActivated] [bit] NOT NULL,
	[LoginTimes] [int] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedByName] [varchar](50) NOT NULL,
	[UpdatedTime] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[UpdatedByName] [varchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_cms_administrator] PRIMARY KEY CLUSTERED
(
	[AdminId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_PADDING OFF

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AdminId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'AdminId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AdminName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'AdminName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Password' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'Password'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Full Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'FullName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Gender' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'Gender'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tel' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'Tel'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Email' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'Email'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valid From' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'ValidFrom'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valid To' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'ValidTo'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Activated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'IsActivated'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Login Times' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'LoginTimes'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'CreatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'CreatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'CreatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'UpdatedTime'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'UpdatedById'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'UpdatedByName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'Sequence'

SET IDENTITY_INSERT [dbo].[cms_administrator] ON
INSERT [dbo].[cms_administrator] ([AdminId], [AdminName], [Password], [FullName], [Gender], [Tel], [Email], [ValidFrom], [ValidTo], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (1, N'admin', N'21232F297A57A5A743894A0E4A801FC3', N'johnny', 1, N'13764971002', N'20656154@qq.com', CAST(0x0000A4D000000000 AS DateTime), CAST(0x002D23E500000000 AS DateTime), 1, 617, CAST(0x00009EBF015D81D4 AS DateTime), 1, N'johnny', CAST(0x0000A4D00165F2CB AS DateTime), 1, N'admin', 1)
INSERT [dbo].[cms_administrator] ([AdminId], [AdminName], [Password], [FullName], [Gender], [Tel], [Email], [ValidFrom], [ValidTo], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (2, N'bossli', N'E10ADC3949BA59ABBE56E057F20F883E', N'tian li', 1, N'sdfpoo', N'jojo@am.com', CAST(0x00009EDD00000000 AS DateTime), CAST(0x00009F0600000000 AS DateTime), 1, 3, CAST(0x00009EE50161DDF3 AS DateTime), 1, N'admin', CAST(0x0000A59600A9EAFE AS DateTime), 1, N'admin', 2)
INSERT [dbo].[cms_administrator] ([AdminId], [AdminName], [Password], [FullName], [Gender], [Tel], [Email], [ValidFrom], [ValidTo], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (3, N'editor', N'E10ADC3949BA59ABBE56E057F20F883E', N'news editor', 1, N'', N'jojozhuang@gmail.com', CAST(0x00009EED00000000 AS DateTime), CAST(0x00009F4500000000 AS DateTime), 1, 1, CAST(0x00009EED01694703 AS DateTime), 1, N'admin', CAST(0x0000A59600AA1A22 AS DateTime), 1, N'admin', 3)
INSERT [dbo].[cms_administrator] ([AdminId], [AdminName], [Password], [FullName], [Gender], [Tel], [Email], [ValidFrom], [ValidTo], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (6, N'test', N'123456', N'test', 1, N'wef', N'efe@sdf.com', CAST(0x0000A4D100000000 AS DateTime), CAST(0x0000A4E000000000 AS DateTime), 1, 0, CAST(0x0000A4D201672268 AS DateTime), 1, N'admin', CAST(0x0000A59600AA2669 AS DateTime), 1, N'admin', 4)
SET IDENTITY_INSERT [dbo].[cms_administrator] OFF
