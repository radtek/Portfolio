USE [master]
GO
/****** Object:  Database [SEHome]    Script Date: 11/18/2017 21:03:38 ******/
CREATE DATABASE [SEHome] ON  PRIMARY 
( NAME = N'SEHome_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SEHome.mdf' , SIZE = 3328KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'SEHome_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SEHome.ldf' , SIZE = 7616KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
GO
ALTER DATABASE [SEHome] SET COMPATIBILITY_LEVEL = 80
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SEHome].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [SEHome] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [SEHome] SET ANSI_NULLS OFF
GO
ALTER DATABASE [SEHome] SET ANSI_PADDING OFF
GO
ALTER DATABASE [SEHome] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [SEHome] SET ARITHABORT OFF
GO
ALTER DATABASE [SEHome] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [SEHome] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [SEHome] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [SEHome] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [SEHome] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [SEHome] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [SEHome] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [SEHome] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [SEHome] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [SEHome] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [SEHome] SET  DISABLE_BROKER
GO
ALTER DATABASE [SEHome] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [SEHome] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [SEHome] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [SEHome] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [SEHome] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [SEHome] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [SEHome] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [SEHome] SET  READ_WRITE
GO
ALTER DATABASE [SEHome] SET RECOVERY FULL
GO
ALTER DATABASE [SEHome] SET  MULTI_USER
GO
ALTER DATABASE [SEHome] SET PAGE_VERIFY TORN_PAGE_DETECTION
GO
ALTER DATABASE [SEHome] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'SEHome', N'ON'
GO
USE [SEHome]
GO
/****** Object:  User [zrlogin]    Script Date: 11/18/2017 21:03:38 ******/
CREATE USER [zrlogin] FOR LOGIN [zrlogin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [sehomeuser]    Script Date: 11/18/2017 21:03:38 ******/
CREATE USER [sehomeuser] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [psadmin]    Script Date: 11/18/2017 21:03:38 ******/
CREATE USER [psadmin] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[psadmin]
GO
/****** Object:  User [hrhunter]    Script Date: 11/18/2017 21:03:38 ******/
CREATE USER [hrhunter] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[hrhunter]
GO
/****** Object:  Schema [psadmin]    Script Date: 11/18/2017 21:03:38 ******/
CREATE SCHEMA [psadmin] AUTHORIZATION [psadmin]
GO
/****** Object:  Schema [hrhunter]    Script Date: 11/18/2017 21:03:38 ******/
CREATE SCHEMA [hrhunter] AUTHORIZATION [hrhunter]
GO
/****** Object:  StoredProcedure [dbo].[TempGetVersion]    Script Date: 11/18/2017 21:03:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*****************************************************************************/

CREATE PROCEDURE [dbo].[TempGetVersion]
    @ver      char(10) OUTPUT
AS
    SELECT @ver = "2"
    RETURN 0
GO
/****** Object:  UserDefinedDataType [dbo].[tAppName]    Script Date: 11/18/2017 21:03:39 ******/
CREATE TYPE [dbo].[tAppName] FROM [varchar](280) NOT NULL
GO
/****** Object:  StoredProcedure [dbo].[SM_Users_UpdateLoginTimes]    Script Date: 11/18/2017 21:03:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SM_Users_UpdateLoginTimes]
	@username varchar(20)
AS
	UPDATE [SmartMoney_Users] SET [LoginTimes] = [LoginTimes] + 1, [LastLogin] = GETDATE()
        WHERE [Username]=@username
GO
/****** Object:  StoredProcedure [dbo].[SM_Users_GetList]    Script Date: 11/18/2017 21:03:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SM_Users_GetList]
AS
	SELECT [UserId], [UserName], [Password], [RoleId], [LoginTimes], [LastLogin], [UpdateBy], [UpdateTime], [AddBy], [AddTime] FROM [SmartMoney_Users]
GO
/****** Object:  StoredProcedure [dbo].[SM_Users_GetById]    Script Date: 11/18/2017 21:03:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SM_Users_GetById]
	@UserId int
AS
	SELECT [UserId], [UserName], [Password], [RoleId], [LoginTimes], [LastLogin], [UpdateBy], [UpdateTime], [AddBy], [AddTime] FROM [SmartMoney_Users] WHERE [UserId] = @UserId
GO
/****** Object:  StoredProcedure [dbo].[SM_Users_CheckLogin]    Script Date: 11/18/2017 21:03:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[SM_Users_CheckLogin] 
	@username varchar(20),
        @password varchar(32)
AS
	SELECT [UserId] FROM [SmartMoney_Users] 
        WHERE [UserName]=@username AND [Password]=@password
GO
/****** Object:  StoredProcedure [dbo].[SM_Users_CheckExist]    Script Date: 11/18/2017 21:03:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SM_Users_CheckExist]
	@username varchar(20)
AS
	SELECT [UserId] FROM [SmartMoney_Users] 
        WHERE [UserName]=@username
GO
/****** Object:  Table [dbo].[seh_websitecategory]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[seh_websitecategory](
	[WebsiteCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[WebsiteCategoryName] [nvarchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_CMS_SiteCategory] PRIMARY KEY CLUSTERED 
(
	[WebsiteCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Website Category Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_websitecategory', @level2type=N'COLUMN',@level2name=N'WebsiteCategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Website Category Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_websitecategory', @level2type=N'COLUMN',@level2name=N'WebsiteCategoryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_websitecategory', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[seh_website]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[seh_website](
	[WebsiteId] [int] IDENTITY(1,1) NOT NULL,
	[WebsiteName] [nvarchar](50) NOT NULL,
	[WebsiteCategoryId] [int] NOT NULL,
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
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Website Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'WebsiteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Website Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'WebsiteName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Website Category' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'WebsiteCategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'URL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hits' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'Hits'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Display' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'CreatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'CreatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'UpdatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'UpdatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'UpdatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_website', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[seh_software]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Software Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'SoftwareId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Software Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'SoftwareName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Short Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'ShortDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Image' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Image'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Feature1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Feature1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Feature2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Feature2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Feature3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Feature3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Feature4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Feature4'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Download Url' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'DownloadUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Document Title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'DocumentTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Document Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'DocumentDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Document Url' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'DocumentUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hits' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Hits'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Downloads' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Downloads'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Display' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'CreatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'CreatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'UpdatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'UpdatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'UpdatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_software', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[seh_release]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Release Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'ReleaseId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Software' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'SoftwareId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Release Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'ReleaseName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Release Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'ReleaseDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hits' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'Hits'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Downloads' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'Downloads'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Display' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'CreatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'CreatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'UpdatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'UpdatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'UpdatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_release', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[seh_opensource]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'OpenSource Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'OpenSourceId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'OpenSource Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'OpenSourceName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Short Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'ShortDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'URL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hits' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'Hits'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Display' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'CreatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'CreatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'UpdatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'UpdatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'UpdatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_opensource', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[seh_channel]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[seh_channel](
	[ChannelId] [int] IDENTITY(1,1) NOT NULL,
	[ChannelName] [nvarchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_C_NewsCategory] PRIMARY KEY CLUSTERED 
(
	[ChannelId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Channel Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_channel', @level2type=N'COLUMN',@level2name=N'ChannelId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Channel Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_channel', @level2type=N'COLUMN',@level2name=N'ChannelName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_channel', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[seh_bulletin]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bulletin Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'BulletinId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Content' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'URL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hits' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'Hits'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IsDisplay' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'CreatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'CreatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'UpdatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'UpdatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'UpdatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bulletin', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[seh_blogcategory]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[seh_blogcategory](
	[BlogCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[BlogCategoryName] [nvarchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_C_BlogCategory] PRIMARY KEY CLUSTERED 
(
	[BlogCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Blog Category Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blogcategory', @level2type=N'COLUMN',@level2name=N'BlogCategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Blog Category Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blogcategory', @level2type=N'COLUMN',@level2name=N'BlogCategoryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blogcategory', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[seh_blog]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[seh_blog](
	[BlogId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[BlogCategoryId] [int] NOT NULL,
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
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Blog Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'BlogId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Blog Category' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'BlogCategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'Tag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Content' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hits' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'Hits'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Display' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'CreatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'CreatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'UpdatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'UpdatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'UpdatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_blog', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[seh_bestpractice]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Best Practice Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'BestPracticeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Best Practice Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'BestPracticeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Short Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'ShortDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hits' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'Hits'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Display' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CreatedTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'CreatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'CreatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'UpdatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'UpdatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'UpdatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_bestpractice', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[seh_article]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Article Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'ArticleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Channel Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'ChannelId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sub Title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'SubTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'KeyWord' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'KeyWord'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sub Content' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'SubContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Content' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'First Image' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'FirstImage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Copy From' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'CopyFrom'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hits' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'Hits'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Top' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'IsTop'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Elite' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'IsElite'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Picture' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'IsPic'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Page Type' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'IsPageType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Verified' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'IsVerified'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'CreatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'CreatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'UpdatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'UpdatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'UpdatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'seh_article', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  StoredProcedure [dbo].[GetMajorVersion]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*****************************************************************************/

CREATE PROCEDURE [dbo].[GetMajorVersion]
    @@ver int OUTPUT
AS
BEGIN
	DECLARE @version        nchar(100)
	DECLARE @dot            int
	DECLARE @hyphen         int
	DECLARE @SqlToExec      nchar(4000)

	SELECT @@ver = 7
	SELECT @version = @@Version
	SELECT @hyphen  = CHARINDEX(N' - ', @version)
	IF (NOT(@hyphen IS NULL) AND @hyphen > 0)
	BEGIN
		SELECT @hyphen = @hyphen + 3
		SELECT @dot    = CHARINDEX(N'.', @version, @hyphen)
		IF (NOT(@dot IS NULL) AND @dot > @hyphen)
		BEGIN
			SELECT @version = SUBSTRING(@version, @hyphen, @dot - @hyphen)
			SELECT @@ver     = CONVERT(int, @version)
		END
	END
END
GO
/****** Object:  StoredProcedure [dbo].[CreateTempTables]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[CreateTempTables]
        AS
            CREATE TABLE [SEHome].dbo.ASPStateTempSessions (
                SessionId           nvarchar(88)    NOT NULL PRIMARY KEY,
                Created             datetime        NOT NULL DEFAULT GETUTCDATE(),
                Expires             datetime        NOT NULL,
                LockDate            datetime        NOT NULL,
                LockDateLocal       datetime        NOT NULL,
                LockCookie          int             NOT NULL,
                Timeout             int             NOT NULL,
                Locked              bit             NOT NULL,
                SessionItemShort    VARBINARY(7000) NULL,
                SessionItemLong     image           NULL,
                Flags               int             NOT NULL DEFAULT 0,
            ) 

            CREATE NONCLUSTERED INDEX Index_Expires ON [SEHome].dbo.ASPStateTempSessions(Expires)

            CREATE TABLE [SEHome].dbo.ASPStateTempApplications (
                AppId               int             NOT NULL PRIMARY KEY,
                AppName             char(280)       NOT NULL,
            ) 

            CREATE NONCLUSTERED INDEX Index_AppName ON [SEHome].dbo.ASPStateTempApplications(AppName)

            RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[CreateEngine]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateEngine]
AS
	if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Engine]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Engine]
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Engine]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Engine]
CREATE TABLE [dbo].[Engine] (
	[A7] [int] NOT NULL ,
	[B7] [bit] NOT NULL ,
	[C7] [bit] NOT NULL ,
	[D7] [bit] NOT NULL ,
	[E7] [bit] NOT NULL ,
	[F7] [float] NULL ,
	[G7] [float] NULL ,
	[H7] [float] NULL ,
	[I7] [float] NULL ,
	[J7] [float] NULL ,
	[K7] [float] NULL ,
	[L7] [float] NULL ,
	[M7] [float] NULL ,
	[N7] [float] NULL ,
	[O7] [float] NULL ,
	[P7] [float] NULL ,
	[Q7] [float] NULL ,
	[R7] [float] NULL ,
	[S7] [float] NULL ,
	[T7] [float] NULL ,
	[U7] [float] NULL ,
	[V7] [float] NULL ,
	[W7] [float] NULL ,
	[X7] [float] NULL ,
	[Y7] [float] NULL ,
	[Z7] [float] NULL ,
	[AA] [float] NULL 
) ON [PRIMARY]
ALTER TABLE [dbo].[Engine] WITH NOCHECK ADD 
	CONSTRAINT [PK_Engine] PRIMARY KEY  CLUSTERED 
	(
		[A7]
	)  ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cms_websettings]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
	[LogoPath] [varchar](100) NOT NULL,
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
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号（自动加1）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网站名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'WebsiteName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网站标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'WebsiteTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网站简介' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'ShortDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'Tel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'传真' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'Fax'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子邮件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'WebsiteAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安装路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'WebsitePath'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传文件大小' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'FileSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LOGO地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'LogoPath'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Banner地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'BannerPath'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版权信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'Copyright'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网站关键词' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'MetaKeywords'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网站描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'MetaDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否关闭网站' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'IsClosed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关闭网站描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'ClosedInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户协议' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'UserAgreement'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台登陆方式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_websettings', @level2type=N'COLUMN',@level2name=N'LoginType'
GO
/****** Object:  Table [dbo].[cms_topmenubinding]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_topmenubinding](
	[TopMenuId] [int] NOT NULL,
	[MenuCategoryId] [int] NOT NULL
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Top Menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_topmenubinding', @level2type=N'COLUMN',@level2name=N'TopMenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Category' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_topmenubinding', @level2type=N'COLUMN',@level2name=N'MenuCategoryId'
GO
/****** Object:  Table [dbo].[cms_topmenu]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Top Menu Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_topmenu', @level2type=N'COLUMN',@level2name=N'TopMenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Top Menu Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_topmenu', @level2type=N'COLUMN',@level2name=N'TopMenuName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ToolTip' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_topmenu', @level2type=N'COLUMN',@level2name=N'ToolTip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default Page' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_topmenu', @level2type=N'COLUMN',@level2name=N'PageLink'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_topmenu', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[cms_rolepermission]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_rolepermission](
	[RoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Role' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_rolepermission', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_rolepermission', @level2type=N'COLUMN',@level2name=N'PermissionId'
GO
/****** Object:  Table [dbo].[cms_role]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Role Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_role', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Role Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_role', @level2type=N'COLUMN',@level2name=N'RoleName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_role', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_role', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[cms_permissioncategory]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_permissioncategory](
	[PermissionCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[PermissionCategoryName] [nvarchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_cms_permissioncategory] PRIMARY KEY CLUSTERED 
(
	[PermissionCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Category Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_permissioncategory', @level2type=N'COLUMN',@level2name=N'PermissionCategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Category Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_permissioncategory', @level2type=N'COLUMN',@level2name=N'PermissionCategoryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_permissioncategory', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[cms_permission]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_permission](
	[PermissionId] [int] IDENTITY(1,1) NOT NULL,
	[PermissionName] [nvarchar](50) NOT NULL,
	[PermissionCategoryId] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_cms_permission] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_permission', @level2type=N'COLUMN',@level2name=N'PermissionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_permission', @level2type=N'COLUMN',@level2name=N'PermissionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Category' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_permission', @level2type=N'COLUMN',@level2name=N'PermissionCategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_permission', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[cms_pagebinding]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_pagebinding](
	[PageBindingId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[MenuCategoryId] [int] NOT NULL,
	[ListMenuId] [int] NOT NULL,
	[AddMenuId] [int] NOT NULL,
 CONSTRAINT [PK_cms_pagebinding] PRIMARY KEY CLUSTERED 
(
	[PageBindingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Page Binding Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_pagebinding', @level2type=N'COLUMN',@level2name=N'PageBindingId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_pagebinding', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Category' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_pagebinding', @level2type=N'COLUMN',@level2name=N'MenuCategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'List Page' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_pagebinding', @level2type=N'COLUMN',@level2name=N'ListMenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Add Page' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_pagebinding', @level2type=N'COLUMN',@level2name=N'AddMenuId'
GO
/****** Object:  Table [dbo].[cms_navigator]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_menucategory]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_menucategory](
	[MenuCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[MenuCategoryName] [nvarchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_cms_menucategory2] PRIMARY KEY CLUSTERED 
(
	[MenuCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Category Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menucategory', @level2type=N'COLUMN',@level2name=N'MenuCategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Category Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menucategory', @level2type=N'COLUMN',@level2name=N'MenuCategoryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menucategory', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[cms_menu]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_menu](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](50) NOT NULL,
	[MenuCategoryId] [int] NOT NULL,
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
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'MenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'MenuName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Category' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'MenuCategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Page Link' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'PageLink'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ToolTip' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'ToolTip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Image' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'Image'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'PermissionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Display in the Menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_menu', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[cms_mailsettings]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号（自动加1）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_mailsettings', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_mailsettings', @level2type=N'COLUMN',@level2name=N'SmtpServerIP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Port' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_mailsettings', @level2type=N'COLUMN',@level2name=N'SmtpServerPort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮件地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_mailsettings', @level2type=N'COLUMN',@level2name=N'MailId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_mailsettings', @level2type=N'COLUMN',@level2name=N'MailPassword'
GO
/****** Object:  Table [dbo].[CMS_Log]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CMS_Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Thread] [varchar](255) NOT NULL,
	[Level] [varchar](50) NOT NULL,
	[Logger] [varchar](255) NOT NULL,
	[Message] [varchar](4000) NOT NULL,
	[Exception] [varchar](2000) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_breviarysettings]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号（自动加1）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缩略图宽度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'Width'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缩略图高度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'Height'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'添加水印' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'PlusWatermark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'水印类型：图片水印,文字水印 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'WatermarkType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'水印图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'WatermarkImage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片水印透明度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'ImageTransparent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'水印文字' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'WatermarkText'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文字水印透明度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'TextTransparent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'水印位置：左上，左中，左下，中上，正中，中下，右上，右中，右下' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breviarysettings', @level2type=N'COLUMN',@level2name=N'WatermarkPosition'
GO
/****** Object:  Table [dbo].[cms_adminrole]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AdminRoleId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminrole', @level2type=N'COLUMN',@level2name=N'AdminRoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Admin Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminrole', @level2type=N'COLUMN',@level2name=N'AdminId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Role Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminrole', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminrole', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[cms_adminloginlog]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_adminloginlog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Password] [varchar](32) NOT NULL,
	[LoginTime] [datetime] NOT NULL,
	[LogoutTime] [datetime] NOT NULL,
	[LoginIP] [varchar](50) NOT NULL,
	[HosterName] [varchar](100) NULL,
	[LoginStatus] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_cms_adminloginlog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号（自动加1）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminloginlog', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'管理员名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminloginlog', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminloginlog', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminloginlog', @level2type=N'COLUMN',@level2name=N'LoginTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登出时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminloginlog', @level2type=N'COLUMN',@level2name=N'LogoutTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录时IP地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminloginlog', @level2type=N'COLUMN',@level2name=N'LoginIP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录时机器名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminloginlog', @level2type=N'COLUMN',@level2name=N'HosterName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录状况' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_adminloginlog', @level2type=N'COLUMN',@level2name=N'LoginStatus'
GO
/****** Object:  Table [dbo].[cms_administrator]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AdminId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'AdminId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AdminName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'AdminName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Password' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Full Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'FullName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Gender' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'Gender'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tel' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'Tel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Email' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valid From' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'ValidFrom'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valid To' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'ValidTo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Activated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'IsActivated'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Login Times' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'LoginTimes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'CreatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'CreatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'UpdatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'UpdatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'UpdatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_administrator', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  StoredProcedure [dbo].[C_SequenceMove]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  Procedure [dbo].[C_SequenceMove]
(
	@tbl Nvarchar(50),
	@primarykey Nvarchar(50),
	@id int,
	@sequence int,
	@direction Nvarchar(10)
	 
)
AS
  Begin
	Declare @sql Nvarchar(2000)
    Set  @sql='Declare @a int'
			 +' If '''+@direction+'''=''Up'''
			 +' Begin'
                                         +' select @a = case when max(Sequence) is null then '+str(@sequence)+' else max(Sequence) end from '+@tbl+' where Sequence<'+str(@sequence)	                                         		 
			 +' update '+@tbl+' Set Sequence='+str(@sequence)+' where Sequence=@a'
			 +' update '+@tbl+' Set Sequence=@a where '+@primarykey+' ='+str(@id)			 
			 +' End'
			 +' else'
			 +' Begin'
                                         +' select @a =case when min(Sequence) is null then '+str(@sequence)+' else min(Sequence) end from '+@tbl+' where Sequence>'+str(@sequence)
			 +' update '+@tbl+' Set Sequence='+str(@sequence)+' where Sequence=@a'
			 +' update '+@tbl+' Set Sequence=@a where ' +@primarykey+'='+str(@id)
			 +' End'
			print @sql
			Execute(@sql)
  End
GO
/****** Object:  StoredProcedure [dbo].[DeleteExpiredSessions]    Script Date: 11/18/2017 21:03:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[DeleteExpiredSessions]
        AS
            DECLARE @now datetime
            SET @now = GETUTCDATE()

            DELETE [SEHome].dbo.ASPStateTempSessions
            WHERE Expires < @now

            RETURN 0
GO
/****** Object:  View [dbo].[VIEW1]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VIEW1]
AS
SELECT *
FROM dbo.S_Menu
GO
/****** Object:  UserDefinedDataType [dbo].[tTextPtr]    Script Date: 11/18/2017 21:03:42 ******/
CREATE TYPE [dbo].[tTextPtr] FROM [varbinary](16) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[tSessionItemShort]    Script Date: 11/18/2017 21:03:42 ******/
CREATE TYPE [dbo].[tSessionItemShort] FROM [varbinary](7000) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[tSessionItemLong]    Script Date: 11/18/2017 21:03:42 ******/
CREATE TYPE [dbo].[tSessionItemLong] FROM [image] NULL
GO
/****** Object:  UserDefinedDataType [dbo].[tSessionId]    Script Date: 11/18/2017 21:03:42 ******/
CREATE TYPE [dbo].[tSessionId] FROM [nvarchar](88) NOT NULL
GO
/****** Object:  StoredProcedure [dbo].[TempUpdateStateItemShortNullLong]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempUpdateStateItemShortNullLong]
            @id         tSessionId,
            @itemShort  tSessionItemShort,
            @timeout    int,
            @lockCookie int
        AS    
            UPDATE [SEHome].dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, @timeout, GETUTCDATE()), 
                SessionItemShort = @itemShort, 
                SessionItemLong = NULL, 
                Timeout = @timeout,
                Locked = 0
            WHERE SessionId = @id AND LockCookie = @lockCookie

            RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[TempUpdateStateItemShort]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempUpdateStateItemShort]
            @id         tSessionId,
            @itemShort  tSessionItemShort,
            @timeout    int,
            @lockCookie int
        AS    
            UPDATE [SEHome].dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, @timeout, GETUTCDATE()), 
                SessionItemShort = @itemShort, 
                Timeout = @timeout,
                Locked = 0
            WHERE SessionId = @id AND LockCookie = @lockCookie

            RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[TempUpdateStateItemLongNullShort]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempUpdateStateItemLongNullShort]
            @id         tSessionId,
            @itemLong   tSessionItemLong,
            @timeout    int,
            @lockCookie int
        AS    
            UPDATE [SEHome].dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, @timeout, GETUTCDATE()), 
                SessionItemLong = @itemLong, 
                SessionItemShort = NULL,
                Timeout = @timeout,
                Locked = 0
            WHERE SessionId = @id AND LockCookie = @lockCookie

            RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[TempUpdateStateItemLong]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempUpdateStateItemLong]
            @id         tSessionId,
            @itemLong   tSessionItemLong,
            @timeout    int,
            @lockCookie int
        AS    
            UPDATE [SEHome].dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, @timeout, GETUTCDATE()), 
                SessionItemLong = @itemLong,
                Timeout = @timeout,
                Locked = 0
            WHERE SessionId = @id AND LockCookie = @lockCookie

            RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[TempResetTimeout]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempResetTimeout]
            @id     tSessionId
        AS
            UPDATE [SEHome].dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, GETUTCDATE())
            WHERE SessionId = @id
            RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[TempRemoveStateItem]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempRemoveStateItem]
        @id     tSessionId,
        @lockCookie int
    AS
        DELETE [SEHome].dbo.ASPStateTempSessions
        WHERE SessionId = @id AND LockCookie = @lockCookie
        RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[TempReleaseStateItemExclusive]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempReleaseStateItemExclusive]
            @id         tSessionId,
            @lockCookie int
        AS
            UPDATE [SEHome].dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, GETUTCDATE()), 
                Locked = 0
            WHERE SessionId = @id AND LockCookie = @lockCookie

            RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[TempInsertUninitializedItem]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempInsertUninitializedItem]
            @id         tSessionId,
            @itemShort  tSessionItemShort,
            @timeout    int
        AS    

            DECLARE @now AS datetime
            DECLARE @nowLocal AS datetime
            
            SET @now = GETUTCDATE()
            SET @nowLocal = GETDATE()

            INSERT [SEHome].dbo.ASPStateTempSessions 
                (SessionId, 
                 SessionItemShort, 
                 Timeout, 
                 Expires, 
                 Locked, 
                 LockDate,
                 LockDateLocal,
                 LockCookie,
                 Flags) 
            VALUES 
                (@id, 
                 @itemShort, 
                 @timeout, 
                 DATEADD(n, @timeout, @now), 
                 0, 
                 @now,
                 @nowLocal,
                 1,
                 1)

            RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[TempInsertStateItemShort]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempInsertStateItemShort]
            @id         tSessionId,
            @itemShort  tSessionItemShort,
            @timeout    int
        AS    

            DECLARE @now AS datetime
            DECLARE @nowLocal AS datetime
            
            SET @now = GETUTCDATE()
            SET @nowLocal = GETDATE()

            INSERT [SEHome].dbo.ASPStateTempSessions 
                (SessionId, 
                 SessionItemShort, 
                 Timeout, 
                 Expires, 
                 Locked, 
                 LockDate,
                 LockDateLocal,
                 LockCookie) 
            VALUES 
                (@id, 
                 @itemShort, 
                 @timeout, 
                 DATEADD(n, @timeout, @now), 
                 0, 
                 @now,
                 @nowLocal,
                 1)

            RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[TempInsertStateItemLong]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempInsertStateItemLong]
            @id         tSessionId,
            @itemLong   tSessionItemLong,
            @timeout    int
        AS    
            DECLARE @now AS datetime
            DECLARE @nowLocal AS datetime
            
            SET @now = GETUTCDATE()
            SET @nowLocal = GETDATE()

            INSERT [SEHome].dbo.ASPStateTempSessions 
                (SessionId, 
                 SessionItemLong, 
                 Timeout, 
                 Expires, 
                 Locked, 
                 LockDate,
                 LockDateLocal,
                 LockCookie) 
            VALUES 
                (@id, 
                 @itemLong, 
                 @timeout, 
                 DATEADD(n, @timeout, @now), 
                 0, 
                 @now,
                 @nowLocal,
                 1)

            RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[GetHashCode]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*****************************************************************************/

CREATE PROCEDURE [dbo].[GetHashCode]
    @input tAppName,
    @hash int OUTPUT
AS
    /* 
       This sproc is based on this C# hash function:

        int GetHashCode(string s)
        {
            int     hash = 5381;
            int     len = s.Length;

            for (int i = 0; i < len; i++) {
                int     c = Convert.ToInt32(s[i]);
                hash = ((hash << 5) + hash) ^ c;
            }

            return hash;
        }

        However, SQL 7 doesn't provide a 32-bit integer
        type that allows rollover of bits, we have to
        divide our 32bit integer into the upper and lower
        16 bits to do our calculation.
    */
       
    DECLARE @hi_16bit   int
    DECLARE @lo_16bit   int
    DECLARE @hi_t       int
    DECLARE @lo_t       int
    DECLARE @len        int
    DECLARE @i          int
    DECLARE @c          int
    DECLARE @carry      int

    SET @hi_16bit = 0
    SET @lo_16bit = 5381
    
    SET @len = DATALENGTH(@input)
    SET @i = 1
    
    WHILE (@i <= @len)
    BEGIN
        SET @c = ASCII(SUBSTRING(@input, @i, 1))

        /* Formula:                        
           hash = ((hash << 5) + hash) ^ c */

        /* hash << 5 */
        SET @hi_t = @hi_16bit * 32 /* high 16bits << 5 */
        SET @hi_t = @hi_t & 0xFFFF /* zero out overflow */
        
        SET @lo_t = @lo_16bit * 32 /* low 16bits << 5 */
        
        SET @carry = @lo_16bit & 0x1F0000 /* move low 16bits carryover to hi 16bits */
        SET @carry = @carry / 0x10000 /* >> 16 */
        SET @hi_t = @hi_t + @carry
        SET @hi_t = @hi_t & 0xFFFF /* zero out overflow */

        /* + hash */
        SET @lo_16bit = @lo_16bit + @lo_t
        SET @hi_16bit = @hi_16bit + @hi_t + (@lo_16bit / 0x10000)
        /* delay clearing the overflow */

        /* ^c */
        SET @lo_16bit = @lo_16bit ^ @c

        /* Now clear the overflow bits */	
        SET @hi_16bit = @hi_16bit & 0xFFFF
        SET @lo_16bit = @lo_16bit & 0xFFFF

        SET @i = @i + 1
    END

    /* Do a sign extension of the hi-16bit if needed */
    IF (@hi_16bit & 0x8000 <> 0)
        SET @hi_16bit = 0xFFFF0000 | @hi_16bit

    /* Merge hi and lo 16bit back together */
    SET @hi_16bit = @hi_16bit * 0x10000 /* << 16 */
    SET @hash = @hi_16bit | @lo_16bit

    RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[TempGetStateItemExclusive3]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempGetStateItemExclusive3]
            @id         tSessionId,
            @itemShort  tSessionItemShort OUTPUT,
            @locked     bit OUTPUT,
            @lockAge    int OUTPUT,
            @lockCookie int OUTPUT,
            @actionFlags int OUTPUT
        AS
            DECLARE @textptr AS tTextPtr
            DECLARE @length AS int
            DECLARE @now AS datetime
            DECLARE @nowLocal AS datetime

            SET @now = GETUTCDATE()
            SET @nowLocal = GETDATE()
            
            UPDATE [SEHome].dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, @now), 
                LockDate = CASE Locked
                    WHEN 0 THEN @now
                    ELSE LockDate
                    END,
                LockDateLocal = CASE Locked
                    WHEN 0 THEN @nowLocal
                    ELSE LockDateLocal
                    END,
                @lockAge = CASE Locked
                    WHEN 0 THEN 0
                    ELSE DATEDIFF(second, LockDate, @now)
                    END,
                @lockCookie = LockCookie = CASE Locked
                    WHEN 0 THEN LockCookie + 1
                    ELSE LockCookie
                    END,
                @itemShort = CASE Locked
                    WHEN 0 THEN SessionItemShort
                    ELSE NULL
                    END,
                @textptr = CASE Locked
                    WHEN 0 THEN TEXTPTR(SessionItemLong)
                    ELSE NULL
                    END,
                @length = CASE Locked
                    WHEN 0 THEN DATALENGTH(SessionItemLong)
                    ELSE NULL
                    END,
                @locked = Locked,
                Locked = 1,

                /* If the Uninitialized flag (0x1) if it is set,
                   remove it and return InitializeItem (0x1) in actionFlags */
                Flags = CASE
                    WHEN (Flags & 1) <> 0 THEN (Flags & ~1)
                    ELSE Flags
                    END,
                @actionFlags = CASE
                    WHEN (Flags & 1) <> 0 THEN 1
                    ELSE 0
                    END
            WHERE SessionId = @id
            IF @length IS NOT NULL BEGIN
                READTEXT [SEHome].dbo.ASPStateTempSessions.SessionItemLong @textptr 0 @length
            END

            RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[TempGetStateItemExclusive2]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempGetStateItemExclusive2]
            @id         tSessionId,
            @itemShort  tSessionItemShort OUTPUT,
            @locked     bit OUTPUT,
            @lockAge    int OUTPUT,
            @lockCookie int OUTPUT
        AS
            DECLARE @textptr AS tTextPtr
            DECLARE @length AS int
            DECLARE @now AS datetime
            DECLARE @nowLocal AS datetime

            SET @now = GETUTCDATE()
            SET @nowLocal = GETDATE()
            
            UPDATE [SEHome].dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, @now), 
                LockDate = CASE Locked
                    WHEN 0 THEN @now
                    ELSE LockDate
                    END,
                LockDateLocal = CASE Locked
                    WHEN 0 THEN @nowLocal
                    ELSE LockDateLocal
                    END,
                @lockAge = CASE Locked
                    WHEN 0 THEN 0
                    ELSE DATEDIFF(second, LockDate, @now)
                    END,
                @lockCookie = LockCookie = CASE Locked
                    WHEN 0 THEN LockCookie + 1
                    ELSE LockCookie
                    END,
                @itemShort = CASE Locked
                    WHEN 0 THEN SessionItemShort
                    ELSE NULL
                    END,
                @textptr = CASE Locked
                    WHEN 0 THEN TEXTPTR(SessionItemLong)
                    ELSE NULL
                    END,
                @length = CASE Locked
                    WHEN 0 THEN DATALENGTH(SessionItemLong)
                    ELSE NULL
                    END,
                @locked = Locked,
                Locked = 1
            WHERE SessionId = @id
            IF @length IS NOT NULL BEGIN
                READTEXT [SEHome].dbo.ASPStateTempSessions.SessionItemLong @textptr 0 @length
            END

            RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[TempGetStateItemExclusive]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempGetStateItemExclusive]
            @id         tSessionId,
            @itemShort  tSessionItemShort OUTPUT,
            @locked     bit OUTPUT,
            @lockDate   datetime OUTPUT,
            @lockCookie int OUTPUT
        AS
            DECLARE @textptr AS tTextPtr
            DECLARE @length AS int
            DECLARE @now AS datetime
            DECLARE @nowLocal AS datetime

            SET @now = GETUTCDATE()
            SET @nowLocal = GETDATE()
            
            UPDATE [SEHome].dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, @now), 
                LockDate = CASE Locked
                    WHEN 0 THEN @now
                    ELSE LockDate
                    END,
                @lockDate = LockDateLocal = CASE Locked
                    WHEN 0 THEN @nowLocal
                    ELSE LockDateLocal
                    END,
                @lockCookie = LockCookie = CASE Locked
                    WHEN 0 THEN LockCookie + 1
                    ELSE LockCookie
                    END,
                @itemShort = CASE Locked
                    WHEN 0 THEN SessionItemShort
                    ELSE NULL
                    END,
                @textptr = CASE Locked
                    WHEN 0 THEN TEXTPTR(SessionItemLong)
                    ELSE NULL
                    END,
                @length = CASE Locked
                    WHEN 0 THEN DATALENGTH(SessionItemLong)
                    ELSE NULL
                    END,
                @locked = Locked,
                Locked = 1
            WHERE SessionId = @id
            IF @length IS NOT NULL BEGIN
                READTEXT [SEHome].dbo.ASPStateTempSessions.SessionItemLong @textptr 0 @length
            END

            RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[TempGetStateItem3]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempGetStateItem3]
            @id         tSessionId,
            @itemShort  tSessionItemShort OUTPUT,
            @locked     bit OUTPUT,
            @lockAge    int OUTPUT,
            @lockCookie int OUTPUT,
            @actionFlags int OUTPUT
        AS
            DECLARE @textptr AS tTextPtr
            DECLARE @length AS int
            DECLARE @now AS datetime
            SET @now = GETUTCDATE()

            UPDATE [SEHome].dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, @now), 
                @locked = Locked,
                @lockAge = DATEDIFF(second, LockDate, @now),
                @lockCookie = LockCookie,
                @itemShort = CASE @locked
                    WHEN 0 THEN SessionItemShort
                    ELSE NULL
                    END,
                @textptr = CASE @locked
                    WHEN 0 THEN TEXTPTR(SessionItemLong)
                    ELSE NULL
                    END,
                @length = CASE @locked
                    WHEN 0 THEN DATALENGTH(SessionItemLong)
                    ELSE NULL
                    END,

                /* If the Uninitialized flag (0x1) if it is set,
                   remove it and return InitializeItem (0x1) in actionFlags */
                Flags = CASE
                    WHEN (Flags & 1) <> 0 THEN (Flags & ~1)
                    ELSE Flags
                    END,
                @actionFlags = CASE
                    WHEN (Flags & 1) <> 0 THEN 1
                    ELSE 0
                    END
            WHERE SessionId = @id
            IF @length IS NOT NULL BEGIN
                READTEXT [SEHome].dbo.ASPStateTempSessions.SessionItemLong @textptr 0 @length
            END

            RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[TempGetStateItem2]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempGetStateItem2]
            @id         tSessionId,
            @itemShort  tSessionItemShort OUTPUT,
            @locked     bit OUTPUT,
            @lockAge    int OUTPUT,
            @lockCookie int OUTPUT
        AS
            DECLARE @textptr AS tTextPtr
            DECLARE @length AS int
            DECLARE @now AS datetime
            SET @now = GETUTCDATE()

            UPDATE [SEHome].dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, @now), 
                @locked = Locked,
                @lockAge = DATEDIFF(second, LockDate, @now),
                @lockCookie = LockCookie,
                @itemShort = CASE @locked
                    WHEN 0 THEN SessionItemShort
                    ELSE NULL
                    END,
                @textptr = CASE @locked
                    WHEN 0 THEN TEXTPTR(SessionItemLong)
                    ELSE NULL
                    END,
                @length = CASE @locked
                    WHEN 0 THEN DATALENGTH(SessionItemLong)
                    ELSE NULL
                    END
            WHERE SessionId = @id
            IF @length IS NOT NULL BEGIN
                READTEXT [SEHome].dbo.ASPStateTempSessions.SessionItemLong @textptr 0 @length
            END

            RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[TempGetStateItem]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempGetStateItem]
            @id         tSessionId,
            @itemShort  tSessionItemShort OUTPUT,
            @locked     bit OUTPUT,
            @lockDate   datetime OUTPUT,
            @lockCookie int OUTPUT
        AS
            DECLARE @textptr AS tTextPtr
            DECLARE @length AS int
            DECLARE @now AS datetime
            SET @now = GETUTCDATE()

            UPDATE [SEHome].dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, @now), 
                @locked = Locked,
                @lockDate = LockDateLocal,
                @lockCookie = LockCookie,
                @itemShort = CASE @locked
                    WHEN 0 THEN SessionItemShort
                    ELSE NULL
                    END,
                @textptr = CASE @locked
                    WHEN 0 THEN TEXTPTR(SessionItemLong)
                    ELSE NULL
                    END,
                @length = CASE @locked
                    WHEN 0 THEN DATALENGTH(SessionItemLong)
                    ELSE NULL
                    END
            WHERE SessionId = @id
            IF @length IS NOT NULL BEGIN
                READTEXT [SEHome].dbo.ASPStateTempSessions.SessionItemLong @textptr 0 @length
            END

            RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[TempGetAppID]    Script Date: 11/18/2017 21:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[TempGetAppID]
    @appName    tAppName,
    @appId      int OUTPUT
    AS
    SET @appName = LOWER(@appName)
    SET @appId = NULL

    SELECT @appId = AppId
    FROM [SEHome].dbo.ASPStateTempApplications
    WHERE AppName = @appName

    IF @appId IS NULL BEGIN
        BEGIN TRAN        

        SELECT @appId = AppId
        FROM [SEHome].dbo.ASPStateTempApplications WITH (TABLOCKX)
        WHERE AppName = @appName
        
        IF @appId IS NULL
        BEGIN
            EXEC GetHashCode @appName, @appId OUTPUT
            
            INSERT [SEHome].dbo.ASPStateTempApplications
            VALUES
            (@appId, @appName)
            
            IF @@ERROR = 2627 
            BEGIN
                DECLARE @dupApp tAppName
            
                SELECT @dupApp = RTRIM(AppName)
                FROM [SEHome].dbo.ASPStateTempApplications 
                WHERE AppId = @appId
                
                RAISERROR('SQL session state fatal error: hash-code collision between applications ''%s'' and ''%s''. Please rename the 1st application to resolve the problem.', 
                            18, 1, @appName, @dupApp)
            END
        END

        COMMIT
    END

    RETURN 0
GO
