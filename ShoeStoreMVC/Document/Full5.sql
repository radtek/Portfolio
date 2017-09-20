USE [master]
GO
/****** Object:  Database [ShoeStore5]    Script Date: 11/17/2015 11:21:16 AM ******/
CREATE DATABASE [ShoeStore5]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ShoeStore5', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ShoeStore5.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ShoeStore5_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ShoeStore5_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ShoeStore5] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShoeStore5].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ShoeStore5] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ShoeStore5] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ShoeStore5] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ShoeStore5] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ShoeStore5] SET ARITHABORT OFF 
GO
ALTER DATABASE [ShoeStore5] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ShoeStore5] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ShoeStore5] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ShoeStore5] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ShoeStore5] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ShoeStore5] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ShoeStore5] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ShoeStore5] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ShoeStore5] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ShoeStore5] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ShoeStore5] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ShoeStore5] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ShoeStore5] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ShoeStore5] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ShoeStore5] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ShoeStore5] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ShoeStore5] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ShoeStore5] SET RECOVERY FULL 
GO
ALTER DATABASE [ShoeStore5] SET  MULTI_USER 
GO
ALTER DATABASE [ShoeStore5] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ShoeStore5] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ShoeStore5] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ShoeStore5] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ShoeStore5] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ShoeStore5', N'ON'
GO
USE [ShoeStore5]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NULL,
	[ContextKey] [nvarchar](300) NULL,
	[Model] [varbinary](max) NULL,
	[ProductVersion] [nvarchar](32) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Administrators]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Administrators](
	[AdminId] [int] NULL,
	[AdminName] [varchar](50) NULL,
	[Password] [varchar](32) NULL,
	[FullName] [nvarchar](50) NULL,
	[Gender] [bit] NULL,
	[Tel] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[ValidFrom] [datetime] NULL,
	[ValidTo] [datetime] NULL,
	[IsActivated] [bit] NULL,
	[LoginTimes] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedById] [int] NULL,
	[CreatedByName] [varchar](50) NULL,
	[UpdatedTime] [datetime] NULL,
	[UpdatedById] [int] NULL,
	[UpdatedByName] [varchar](50) NULL,
	[Sequence] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AdminRoles]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminRoles](
	[AdminId] [int] NULL,
	[RoleId] [nvarchar](128) NULL,
	[Sequence] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NULL,
	[Name] [nvarchar](256) NULL,
	[Discriminator] [nvarchar](128) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] NULL,
	[UserId] [nvarchar](128) NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NULL,
	[ProviderKey] [nvarchar](128) NULL,
	[UserId] [nvarchar](128) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NULL,
	[RoleId] [nvarchar](128) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NULL,
	[TwoFactorEnabled] [bit] NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NULL,
	[AccessFailedCount] [int] NULL,
	[UserName] [nvarchar](256) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customers]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] NULL,
	[CustomerName] [nvarchar](50) NULL,
	[Password] [varchar](32) NULL,
	[FullName] [nvarchar](50) NULL,
	[Gender] [bit] NULL,
	[Tel] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[IsActivated] [bit] NULL,
	[LoginTimes] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedById] [int] NULL,
	[CreatedByName] [varchar](50) NULL,
	[UpdatedTime] [datetime] NULL,
	[UpdatedById] [int] NULL,
	[UpdatedByName] [varchar](50) NULL,
	[Sequence] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MailSettings]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MailSettings](
	[MailSettingId] [int] NULL,
	[SmtpServer] [varchar](50) NULL,
	[SmtpPort] [int] NULL,
	[EmailAddress] [varchar](100) NULL,
	[EmailPassword] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MenuCategories]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuCategories](
	[MenuCategoryId] [int] NULL,
	[MenuCategoryName] [nvarchar](50) NULL,
	[Sequence] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menus]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Menus](
	[MenuId] [int] NULL,
	[MenuName] [nvarchar](50) NULL,
	[MenuCategoryId] [int] NULL,
	[Controller] [varchar](100) NULL,
	[Action] [varchar](100) NULL,
	[ToolTip] [nvarchar](100) NULL,
	[Image] [varchar](200) NULL,
	[PermissionId] [int] NULL,
	[IsDisplay] [bit] NULL,
	[Sequence] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PageBindings]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageBindings](
	[PageBindingId] [int] NULL,
	[PageTitle] [nvarchar](50) NULL,
	[MenuCategoryId] [int] NULL,
	[ListMenuId] [int] NULL,
	[AddMenuId] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PermissionCategories]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionCategories](
	[PermissionCategoryId] [int] NULL,
	[PermissionCategoryName] [nvarchar](50) NULL,
	[Sequence] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[PermissionId] [int] NULL,
	[PermissionName] [nvarchar](50) NULL,
	[PermissionCategoryId] [int] NULL,
	[Sequence] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductCategories]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategories](
	[ProductCategoryId] [int] NULL,
	[ProductCategoryName] [nvarchar](50) NULL,
	[Sequence] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] NULL,
	[ProductCategoryId] [int] NULL,
	[ProductName] [nvarchar](100) NULL,
	[Description] [nvarchar](500) NULL,
	[Price] [decimal](10, 5) NULL,
	[ImageData] [varbinary](max) NULL,
	[ImageMimeType] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermissions](
	[RoleId] [nvarchar](128) NULL,
	[PermissionId] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [nvarchar](128) NULL,
	[RoleName] [nvarchar](50) NULL,
	[Description] [nvarchar](200) NULL,
	[Sequence] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesOrderItems]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrderItems](
	[SalesOrderId] [int] NULL,
	[ProductId] [int] NULL,
	[ProductName] [nvarchar](100) NULL,
	[Price] [decimal](10, 5) NULL,
	[Quantity] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesOrders]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesOrders](
	[SalesOrderId] [int] NULL,
	[CustomerId] [int] NULL,
	[SalesOrderStatusId] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedById] [int] NULL,
	[CreatedByName] [varchar](50) NULL,
	[UpdatedTime] [datetime] NULL,
	[UpdatedById] [int] NULL,
	[UpdatedByName] [varchar](50) NULL,
	[Sequence] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SalesOrderStatus]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrderStatus](
	[SalesOrderStatusId] [int] NULL,
	[SalesOrderStatusName] [nvarchar](50) NULL,
	[IsActivated] [bit] NULL,
	[Sequence] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sysdiagrams]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysdiagrams](
	[name] [nvarchar](256) NULL,
	[principal_id] [int] NULL,
	[diagram_id] [int] NULL,
	[version] [int] NULL,
	[definition] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TopMenuBindings]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopMenuBindings](
	[TopMenuId] [int] NULL,
	[MenuCategoryId] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TopMenus]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TopMenus](
	[TopMenuId] [int] NULL,
	[TopMenuName] [nvarchar](50) NULL,
	[PageLink] [varchar](100) NULL,
	[Image] [varchar](200) NULL,
	[ToolTip] [nvarchar](50) NULL,
	[Sequence] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[View_AdminRole]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[View_AdminRole](
	[AdminId] [int] NULL,
	[AdminName] [varchar](50) NULL,
	[RoleId] [nvarchar](128) NULL,
	[RoleName] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[View_Menu]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[View_Menu](
	[MenuId] [int] NULL,
	[MenuName] [nvarchar](50) NULL,
	[MenuCategoryId] [int] NULL,
	[MenuCategoryName] [nvarchar](50) NULL,
	[Controller] [varchar](100) NULL,
	[Action] [varchar](100) NULL,
	[ToolTip] [nvarchar](100) NULL,
	[Image] [varchar](200) NULL,
	[PermissionId] [int] NULL,
	[IsDisplay] [bit] NULL,
	[Sequence] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[View_PageBinding]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[View_PageBinding](
	[PageBindingId] [int] NULL,
	[PageTitle] [nvarchar](50) NULL,
	[MenuCategoryId] [int] NULL,
	[MenuCategoryName] [nvarchar](50) NULL,
	[ListMenuId] [int] NULL,
	[ListMenuName] [nvarchar](50) NULL,
	[AddMenuId] [int] NULL,
	[AddMenuName] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[View_Permission]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[View_Permission](
	[PermissionId] [int] NULL,
	[PermissionName] [nvarchar](50) NULL,
	[PermissionCategoryName] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[View_Product]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[View_Product](
	[ProductId] [int] NULL,
	[ProductName] [nvarchar](100) NULL,
	[ProductCategoryId] [int] NULL,
	[ProductCategoryName] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[Price] [decimal](10, 5) NULL,
	[ImageData] [varbinary](max) NULL,
	[ImageMimeType] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[View_SalesOrder]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[View_SalesOrder](
	[SalesOrderId] [int] NULL,
	[CustomerId] [int] NULL,
	[CustomerName] [nvarchar](50) NULL,
	[SalesOrderStatusId] [int] NULL,
	[SalesOrderStatusName] [nvarchar](50) NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedById] [int] NULL,
	[CreatedByName] [varchar](50) NULL,
	[UpdatedTime] [datetime] NULL,
	[UpdatedById] [int] NULL,
	[UpdatedByName] [varchar](50) NULL,
	[Sequence] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[View_TopMenu]    Script Date: 11/17/2015 11:21:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[View_TopMenu](
	[TopMenuId] [int] NULL,
	[TopMenuName] [nvarchar](50) NULL,
	[MenuCategoryId] [int] NULL,
	[MenuCategoryName] [nvarchar](50) NULL,
	[PageLink] [varchar](100) NULL,
	[Image] [varchar](200) NULL,
	[ToolTip] [nvarchar](50) NULL,
	[Sequence] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201509052009480_InitialCreate', N'Johnny.ShoeStore.Domain.Concrete.AppIdentityDbContext', 0x53797374656D2E427974655B5D, N'6.1.3-40302')
INSERT [dbo].[Administrators] ([AdminId], [AdminName], [Password], [FullName], [Gender], [Tel], [Email], [ValidFrom], [ValidTo], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (1, N'Admin', N'E10ADC3949BA59ABBE56E057F20F883E', N'Johnny', 1, N'3124786579', N'jojozhuang@gmail.com', CAST(N'2008-12-03 00:00:00.000' AS DateTime), CAST(N'2034-12-03 00:00:00.000' AS DateTime), 1, 0, CAST(N'2015-08-29 16:48:18.347' AS DateTime), 0, N'', CAST(N'2015-08-31 22:23:53.967' AS DateTime), 0, N'', 0)
INSERT [dbo].[Administrators] ([AdminId], [AdminName], [Password], [FullName], [Gender], [Tel], [Email], [ValidFrom], [ValidTo], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (4, N'Johnny', N'E10ADC3949BA59ABBE56E057F20F883E', N'12222222222222', 0, N'aaaaaaaaaaaaaaaaaaaaaaaa', N'aeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee', CAST(N'2016-12-12 00:00:00.000' AS DateTime), CAST(N'2017-12-12 00:00:00.000' AS DateTime), 0, 0, CAST(N'2015-08-29 20:53:00.950' AS DateTime), 0, N'', CAST(N'2015-08-29 21:00:42.727' AS DateTime), 0, N'', 0)
INSERT [dbo].[Administrators] ([AdminId], [AdminName], [Password], [FullName], [Gender], [Tel], [Email], [ValidFrom], [ValidTo], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (5, N'Jinjing', N'E10ADC3949BA59ABBE56E057F20F883E', N'Jinjing', 0, N'sdf', N'wefw', CAST(N'2012-12-12 00:00:00.000' AS DateTime), CAST(N'2012-12-12 00:00:00.000' AS DateTime), 1, 0, CAST(N'2015-08-29 21:38:36.067' AS DateTime), 0, N'', CAST(N'2015-08-31 22:24:09.153' AS DateTime), 0, N'', 0)
INSERT [dbo].[Administrators] ([AdminId], [AdminName], [Password], [FullName], [Gender], [Tel], [Email], [ValidFrom], [ValidTo], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (6, N'Yaoyao', N'E10ADC3949BA59ABBE56E057F20F883E', N'Yaoyao', 0, NULL, NULL, CAST(N'2012-12-12 00:00:00.000' AS DateTime), CAST(N'2012-12-12 00:00:00.000' AS DateTime), 0, 0, CAST(N'2015-08-29 21:38:52.713' AS DateTime), 0, N'', CAST(N'2015-08-29 21:38:52.713' AS DateTime), 0, N'', 0)
INSERT [dbo].[AdminRoles] ([AdminId], [RoleId], [Sequence]) VALUES (1, N'1', 0)
INSERT [dbo].[AdminRoles] ([AdminId], [RoleId], [Sequence]) VALUES (4, N'2', 0)
INSERT [dbo].[AdminRoles] ([AdminId], [RoleId], [Sequence]) VALUES (5, N'1', 0)
INSERT [dbo].[AdminRoles] ([AdminId], [RoleId], [Sequence]) VALUES (6, N'3', 0)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', N'System Admin', N'AppRole')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'0b253193-9293-43bd-b5a7-84b25fa42f91', N'Customer', N'AppRole')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'2583fbf9-3399-4abb-9504-e3e5afa7051c', N'Shoe Store Admin', N'AppRole')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'47ecf6a4-ed85-4f84-8100-77c92523051c', N'123123', N'AppRole')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'5817f924-d08b-42d3-96d7-dbc90e668bb3', N'Website Admin', N'AppRole')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'c9b6a097-2339-43db-a1ff-4534fb65c234', N'BasicAdmin', N'AppRole')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'096739d1-5fa2-4f91-ad16-e5915ee68e8a', N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'096739d1-5fa2-4f91-ad16-e5915ee68e8a', N'0b253193-9293-43bd-b5a7-84b25fa42f91')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'096739d1-5fa2-4f91-ad16-e5915ee68e8a', N'c9b6a097-2339-43db-a1ff-4534fb65c234')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'65bfb417-22b7-4d1e-9af9-b170ecb0c1a4', N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'65bfb417-22b7-4d1e-9af9-b170ecb0c1a4', N'0b253193-9293-43bd-b5a7-84b25fa42f91')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'65bfb417-22b7-4d1e-9af9-b170ecb0c1a4', N'2583fbf9-3399-4abb-9504-e3e5afa7051c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'65bfb417-22b7-4d1e-9af9-b170ecb0c1a4', N'5817f924-d08b-42d3-96d7-dbc90e668bb3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'65bfb417-22b7-4d1e-9af9-b170ecb0c1a4', N'c9b6a097-2339-43db-a1ff-4534fb65c234')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6dcfead5-9d3e-4621-9e01-7f49ee07bdf4', N'0b253193-9293-43bd-b5a7-84b25fa42f91')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'eee9bc2f-efb4-4023-a78e-60cd9365872d', N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'eee9bc2f-efb4-4023-a78e-60cd9365872d', N'0b253193-9293-43bd-b5a7-84b25fa42f91')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'096739d1-5fa2-4f91-ad16-e5915ee68e8a', N'johnny2333@example.com', 0, N'AA7tjhOQDth22nyE4wn9qUdNZR6SRKNNwQcEKW4XavmJMRG+mWqGUMYHOW1RHUT/gQ==', N'531610a8-e578-4633-9a58-e28bb2260429', NULL, 0, 0, NULL, 0, 0, N'johnny')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'65bfb417-22b7-4d1e-9af9-b170ecb0c1a4', N'admin@example.com', 0, N'AAa4Vlj7RNuef8YJ7i21MYhjkuKZBB5C+NPOeI9Q0q1iImYnbL+UYu7NcM2g2jOvFQ==', N'aadc5008-c7c1-4946-8459-f43eadc0461f', NULL, 0, 0, NULL, 0, 0, N'Admin')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6dcfead5-9d3e-4621-9e01-7f49ee07bdf4', N'amei@example.com', 0, N'AHu9RpvFl4BWjGivaSm7Oo9ivPXIZpydvKe35bn7FIkZdQWQisMiK9c4daJX1GDgrw==', N'c8139ee5-8fb1-4da6-bfc3-892bf7470565', NULL, 0, 0, NULL, 0, 0, N'amei')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'eee9bc2f-efb4-4023-a78e-60cd9365872d', N'webmaster@example.com', 0, N'AF+rXJL5s0eIJCOKb11HhSeN3OtLMKPUv3R58pqDXpR3ZrIB8YEnPT6L0aLx88o8Fg==', N'7414a9f6-c61e-48ba-8692-e9cef851be05', NULL, 0, 0, NULL, 0, 0, N'webmaster')
INSERT [dbo].[Customers] ([CustomerId], [CustomerName], [Password], [FullName], [Gender], [Tel], [Email], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (1, N'Customer1', N'E10ADC3949BA59ABBE56E057F20F883E', N'Johnny Walker', 1, N'3124786579', N'jojozhuang@gmail.com', 1, 0, CAST(N'2015-08-31 10:48:46.407' AS DateTime), 0, N'', CAST(N'2015-08-31 10:48:46.410' AS DateTime), 0, N'', 0)
INSERT [dbo].[Customers] ([CustomerId], [CustomerName], [Password], [FullName], [Gender], [Tel], [Email], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (2, N'Customer2', N'E10ADC3949BA59ABBE56E057F20F883E', N'Johnny Walker2', 0, N'31247865791113', N'jojozhuang@gmail.com222', 1, 0, CAST(N'2015-08-31 10:50:24.917' AS DateTime), 0, N'', CAST(N'2015-08-31 10:50:44.370' AS DateTime), 0, N'', 0)
INSERT [dbo].[Customers] ([CustomerId], [CustomerName], [Password], [FullName], [Gender], [Tel], [Email], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (3, N'RongZhuang', N'E10ADC3949BA59ABBE56E057F20F883E', N'Rong Zhuang', 0, NULL, N'RZHUANG@cdm.depaul.edu', 0, 0, CAST(N'2015-08-31 10:51:04.360' AS DateTime), 0, N'', CAST(N'2015-08-31 10:51:04.360' AS DateTime), 0, N'', 0)
INSERT [dbo].[MailSettings] ([MailSettingId], [SmtpServer], [SmtpPort], [EmailAddress], [EmailPassword]) VALUES (1, N'smtp.163.com2', 123123, N'ajohn@163.com2', N'123343')
INSERT [dbo].[MenuCategories] ([MenuCategoryId], [MenuCategoryName], [Sequence]) VALUES (1, N'Accounts', 0)
INSERT [dbo].[MenuCategories] ([MenuCategoryId], [MenuCategoryName], [Sequence]) VALUES (2, N'Menu', 0)
INSERT [dbo].[MenuCategories] ([MenuCategoryId], [MenuCategoryName], [Sequence]) VALUES (3, N'WebsiteConfig', 0)
INSERT [dbo].[MenuCategories] ([MenuCategoryId], [MenuCategoryName], [Sequence]) VALUES (4, N'Shoe Store Operations', 0)
INSERT [dbo].[MenuCategories] ([MenuCategoryId], [MenuCategoryName], [Sequence]) VALUES (5, N'Shoe Store Settings', 0)
INSERT [dbo].[MenuCategories] ([MenuCategoryId], [MenuCategoryName], [Sequence]) VALUES (6, N'My Space', 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (1, N'User', 1, N'User', N'List', N'Administrator Management', NULL, 1, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (2, N'Add User', 1, N'User', N'Create', N'Create Administrator', NULL, 1, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (3, N'User Role', 1, N'UserRole', N'List', NULL, NULL, 2, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (4, N'Add User Role', 1, N'UserRole', N'Create', NULL, NULL, 2, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (5, N'Role', 1, N'Role', N'List', NULL, NULL, 3, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (6, N'Add Role', 1, N'Role', N'Create', NULL, NULL, 3, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (7, N'Role Permission', 1, N'RolePermission', N'List', NULL, NULL, 4, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (8, N'Permission', 1, N'Permission', N'List', NULL, NULL, 5, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (9, N'Create Permission', 1, N'Permission', N'Create', NULL, NULL, 5, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (10, N'Permission Category', 1, N'PermissionCategory', N'List', NULL, NULL, 6, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (11, N'Create Permission Category', 1, N'PermissionCategory', N'Create', NULL, NULL, 6, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (12, N'Menu', 2, N'Menu', N'List', NULL, NULL, 7, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (13, N'Create Menu', 2, N'Menu', N'Create', NULL, NULL, 7, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (14, N'Menu Category', 2, N'MenuCategory', N'List', NULL, NULL, 8, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (15, N'Create Menu Category', 2, N'MenuCategory', N'Create', NULL, NULL, 8, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (16, N'Top Menu', 2, N'TopMenu', N'List', NULL, NULL, 9, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (17, N'Create Top Menu', 2, N'TopMenu', N'Create', NULL, NULL, 9, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (18, N'Top Menu Binding', 2, N'TopMenuBinding', N'List', NULL, NULL, 10, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (19, N'Page Binding', 2, N'PageBinding', N'List', NULL, NULL, 11, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (20, N'Mail Setting', 3, N'WebsiteConfig', N'EditMailSetting', NULL, NULL, 12, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (21, N'Customer', 4, N'Customer', N'List', NULL, NULL, 16, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (22, N'Create Customer', 4, N'Customer', N'Create', NULL, NULL, 16, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (23, N'Sales Order', 4, N'SalesOrder', N'List', NULL, NULL, 17, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (24, N'Create Sales Order', 4, N'SalesOrder', N'Create', NULL, NULL, 17, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (25, N'Product', 4, N'Product', N'List', NULL, NULL, 18, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (26, N'Create Product', 4, N'Product', N'Create', NULL, NULL, 18, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (27, N'Product Category', 4, N'ProductCategory', N'List', NULL, NULL, 19, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (28, N'Create Product Category', 4, N'ProductCategory', N'Create', NULL, NULL, 19, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (29, N'Sales Order Status', 5, N'SalesOrderStatus', N'List', NULL, NULL, 20, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (31, N'Website Settings', 3, N'Website', N'Settings', NULL, NULL, 12, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (32, N'Breviary Settings', 3, N'Breviary', N'Settings', NULL, NULL, 12, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (33, N'Server Info', 3, N'Server', N'Info', NULL, NULL, 12, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (34, N'Create Page Binding', 2, N'PageBinding', N'Create', NULL, NULL, 11, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (35, N'Binding', 2, N'TopMenu', N'Binding', NULL, NULL, 10, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (36, N'Create Sales Order Status', 5, N'SalesOrderStatus', N'Create', NULL, NULL, 20, 0, 0)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (1, N'User', 1, 1, 2)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (2, N'User Role', 1, 3, 4)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (3, N'Role', 1, 5, 6)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (4, N'Role Permission', 1, 7, 7)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (5, N'Permission', 1, 8, 9)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (6, N'Permission Category', 1, 10, 11)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (7, N'Menu', 2, 12, 13)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (8, N'Menu Category', 1, 14, 15)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (9, N'Top Menu', 2, 16, 17)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (10, N'Top Menu Binding', 1, 18, 18)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (11, N'Page Binding', 2, 19, 34)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (12, N'Mail Settings', 3, 20, 20)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (13, N'Menu Category', 2, 14, 15)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (14, N'Customer', 4, 21, 22)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (15, N'SalesOrderStatus', 5, 29, 36)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (16, N'Sales Order', 4, 23, 24)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (17, N'Product', 4, 25, 26)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (18, N'Product Category', 4, 27, 28)
INSERT [dbo].[PermissionCategories] ([PermissionCategoryId], [PermissionCategoryName], [Sequence]) VALUES (1, N'Accounts', 0)
INSERT [dbo].[PermissionCategories] ([PermissionCategoryId], [PermissionCategoryName], [Sequence]) VALUES (2, N'Menu', 0)
INSERT [dbo].[PermissionCategories] ([PermissionCategoryId], [PermissionCategoryName], [Sequence]) VALUES (3, N'Website Config', 0)
INSERT [dbo].[PermissionCategories] ([PermissionCategoryId], [PermissionCategoryName], [Sequence]) VALUES (4, N'Shortcut', 0)
INSERT [dbo].[PermissionCategories] ([PermissionCategoryId], [PermissionCategoryName], [Sequence]) VALUES (5, N'Shoe Store Settings', 0)
INSERT [dbo].[PermissionCategories] ([PermissionCategoryId], [PermissionCategoryName], [Sequence]) VALUES (6, N'Shoe Store Operations', 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (1, N'User', 1, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (2, N'UserRole', 1, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (3, N'Role', 1, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (4, N'RolePermission', 1, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (5, N'Permission', 1, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (6, N'PermissionCategory', 1, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (7, N'Menu', 2, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (8, N'MenuCategory', 2, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (9, N'TopMenu', 2, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (10, N'Top Menu Binding', 2, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (11, N'Page Binding', 2, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (12, N'WebsiteSettings', 3, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (13, N'Email Settings', 3, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (14, N'Breviary Settings', 3, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (15, N'Server Info', 3, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (16, N'Customer', 6, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (17, N'Orders', 6, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (18, N'Product', 6, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (19, N'Product Category', 6, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (20, N'Sales Order Status', 5, 0)
INSERT [dbo].[ProductCategories] ([ProductCategoryId], [ProductCategoryName], [Sequence]) VALUES (1, N'Chess', 0)
INSERT [dbo].[ProductCategories] ([ProductCategoryId], [ProductCategoryName], [Sequence]) VALUES (2, N'Soccer', 0)
INSERT [dbo].[ProductCategories] ([ProductCategoryId], [ProductCategoryName], [Sequence]) VALUES (3, N'Water Sports', 0)
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (1, 1, N'Kayak', N'A boat for one person', CAST(275.00000 AS Decimal(10, 5)), NULL, N'image/jpeg')
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (2, 1, N'Lifejacket', N'Protective and fashionable', CAST(48.95000 AS Decimal(10, 5)), NULL, N'image/jpeg')
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (3, 1, N'Soccer Ball', N'FIFA-approved size and weight', CAST(19.50000 AS Decimal(10, 5)), NULL, N'image/jpeg')
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (4, 2, N'Corner Flags', N'Give your playing field a professional touch', CAST(34.95000 AS Decimal(10, 5)), NULL, N'image/jpeg')
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (5, 2, N'Stadium', N'Flat-packed, 35,000-seat stadium', CAST(79500.00000 AS Decimal(10, 5)), NULL, NULL)
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (6, 2, N'Thinking Cap', N'Improve your brain efficiency by 75%', CAST(16.00000 AS Decimal(10, 5)), NULL, NULL)
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (7, 3, N'Unsteady Chair', N'Secretly give your opponent a disadvantage', CAST(29.95000 AS Decimal(10, 5)), NULL, NULL)
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (8, 3, N'Human Chess Board', N'A fun game for the family', CAST(75.00000 AS Decimal(10, 5)), NULL, NULL)
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (9, 3, N'Bling-Bling King', N'Gold-plated, diamond-studded King', CAST(1200.00000 AS Decimal(10, 5)), NULL, NULL)
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (10, 3, N'Johnny', N'Teacher', CAST(123.00000 AS Decimal(10, 5)), NULL, NULL)
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (16, 3, N'tese', N'sese', CAST(1212.00000 AS Decimal(10, 5)), NULL, NULL)
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (17, 3, N'newooow', N'wew', CAST(123.00000 AS Decimal(10, 5)), NULL, NULL)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 1)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 2)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 3)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 4)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 5)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 6)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 7)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 8)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 9)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 10)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 11)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 12)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 13)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 14)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 15)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 16)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 17)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 18)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 19)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', 20)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'2583fbf9-3399-4abb-9504-e3e5afa7051c', 16)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'2583fbf9-3399-4abb-9504-e3e5afa7051c', 17)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'2583fbf9-3399-4abb-9504-e3e5afa7051c', 18)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'2583fbf9-3399-4abb-9504-e3e5afa7051c', 19)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'47ecf6a4-ed85-4f84-8100-77c92523051c', 1)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'47ecf6a4-ed85-4f84-8100-77c92523051c', 2)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'47ecf6a4-ed85-4f84-8100-77c92523051c', 3)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'47ecf6a4-ed85-4f84-8100-77c92523051c', 4)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'47ecf6a4-ed85-4f84-8100-77c92523051c', 5)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'47ecf6a4-ed85-4f84-8100-77c92523051c', 10)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'47ecf6a4-ed85-4f84-8100-77c92523051c', 16)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'47ecf6a4-ed85-4f84-8100-77c92523051c', 17)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'47ecf6a4-ed85-4f84-8100-77c92523051c', 18)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'47ecf6a4-ed85-4f84-8100-77c92523051c', 19)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'47ecf6a4-ed85-4f84-8100-77c92523051c', 20)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'5817f924-d08b-42d3-96d7-dbc90e668bb3', 12)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'5817f924-d08b-42d3-96d7-dbc90e668bb3', 13)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (N'5817f924-d08b-42d3-96d7-dbc90e668bb3', 14)
INSERT [dbo].[Roles] ([RoleId], [RoleName], [Description], [Sequence]) VALUES (N'1', N'System Admin', N'System Administrator', 0)
INSERT [dbo].[Roles] ([RoleId], [RoleName], [Description], [Sequence]) VALUES (N'2', N'Website Admin', N'Website Administrator', 0)
INSERT [dbo].[Roles] ([RoleId], [RoleName], [Description], [Sequence]) VALUES (N'3', N'Product Editor', N'Product Editor', 0)
INSERT [dbo].[SalesOrders] ([SalesOrderId], [CustomerId], [SalesOrderStatusId], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (1, 1, 1, CAST(N'2015-08-31 13:00:00.897' AS DateTime), 0, N'', CAST(N'2015-08-31 13:00:00.897' AS DateTime), 0, N'', 0)
INSERT [dbo].[SalesOrders] ([SalesOrderId], [CustomerId], [SalesOrderStatusId], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (2, 2, 3, CAST(N'2015-08-31 14:24:35.447' AS DateTime), 0, N'', CAST(N'2015-08-31 14:24:35.447' AS DateTime), 0, N'', 0)
INSERT [dbo].[SalesOrders] ([SalesOrderId], [CustomerId], [SalesOrderStatusId], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (3, 1, 1, CAST(N'2015-08-31 14:40:22.167' AS DateTime), 0, N'', CAST(N'2015-08-31 14:40:22.167' AS DateTime), 0, N'', 0)
INSERT [dbo].[SalesOrderStatus] ([SalesOrderStatusId], [SalesOrderStatusName], [IsActivated], [Sequence]) VALUES (1, N'Order Placed', 1, 0)
INSERT [dbo].[SalesOrderStatus] ([SalesOrderStatusId], [SalesOrderStatusName], [IsActivated], [Sequence]) VALUES (2, N'Shipped', 1, 0)
INSERT [dbo].[SalesOrderStatus] ([SalesOrderStatusId], [SalesOrderStatusName], [IsActivated], [Sequence]) VALUES (3, N'Delivered', 1, 0)
INSERT [dbo].[SalesOrderStatus] ([SalesOrderStatusId], [SalesOrderStatusName], [IsActivated], [Sequence]) VALUES (4, N'Cancelled', 1, 0)
INSERT [dbo].[sysdiagrams] ([name], [principal_id], [diagram_id], [version], [definition]) VALUES (N'Diagram_0', 1, 1, 1, N'System.Byte[]')
INSERT [dbo].[TopMenuBindings] ([TopMenuId], [MenuCategoryId]) VALUES (1, 6)
INSERT [dbo].[TopMenuBindings] ([TopMenuId], [MenuCategoryId]) VALUES (2, 6)
INSERT [dbo].[TopMenuBindings] ([TopMenuId], [MenuCategoryId]) VALUES (3, 4)
INSERT [dbo].[TopMenuBindings] ([TopMenuId], [MenuCategoryId]) VALUES (3, 5)
INSERT [dbo].[TopMenuBindings] ([TopMenuId], [MenuCategoryId]) VALUES (4, 1)
INSERT [dbo].[TopMenuBindings] ([TopMenuId], [MenuCategoryId]) VALUES (4, 2)
INSERT [dbo].[TopMenuBindings] ([TopMenuId], [MenuCategoryId]) VALUES (4, 3)
INSERT [dbo].[TopMenus] ([TopMenuId], [TopMenuName], [PageLink], [Image], [ToolTip], [Sequence]) VALUES (1, N'Shortcut', N'Menu/ShortCut', N'fa-outdent', N'Shortcut', 0)
INSERT [dbo].[TopMenus] ([TopMenuId], [TopMenuName], [PageLink], [Image], [ToolTip], [Sequence]) VALUES (2, N'My Account', N'Menu/Member', N'fa-users', N'ShoeStore', 0)
INSERT [dbo].[TopMenus] ([TopMenuId], [TopMenuName], [PageLink], [Image], [ToolTip], [Sequence]) VALUES (3, N'Shoe Store', N'System', N'fa-shopping-cart', N'System', 0)
INSERT [dbo].[TopMenus] ([TopMenuId], [TopMenuName], [PageLink], [Image], [ToolTip], [Sequence]) VALUES (4, N'System', N'System', N'fa-cog', N'System', 0)
INSERT [dbo].[View_AdminRole] ([AdminId], [AdminName], [RoleId], [RoleName]) VALUES (1, N'Admin', N'1', N'System Admin')
INSERT [dbo].[View_AdminRole] ([AdminId], [AdminName], [RoleId], [RoleName]) VALUES (4, N'Johnny', N'2', N'Website Admin')
INSERT [dbo].[View_AdminRole] ([AdminId], [AdminName], [RoleId], [RoleName]) VALUES (5, N'Jinjing', N'1', N'System Admin')
INSERT [dbo].[View_AdminRole] ([AdminId], [AdminName], [RoleId], [RoleName]) VALUES (6, N'Yaoyao', N'3', N'Product Editor')
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (1, N'User', 1, N'Accounts', N'User', N'List', N'Administrator Management', NULL, 1, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (2, N'Add User', 1, N'Accounts', N'User', N'Create', N'Create Administrator', NULL, 1, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (3, N'User Role', 1, N'Accounts', N'UserRole', N'List', NULL, NULL, 2, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (4, N'Add User Role', 1, N'Accounts', N'UserRole', N'Create', NULL, NULL, 2, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (5, N'Role', 1, N'Accounts', N'Role', N'List', NULL, NULL, 3, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (6, N'Add Role', 1, N'Accounts', N'Role', N'Create', NULL, NULL, 3, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (7, N'Role Permission', 1, N'Accounts', N'RolePermission', N'List', NULL, NULL, 4, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (8, N'Permission', 1, N'Accounts', N'Permission', N'List', NULL, NULL, 5, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (9, N'Create Permission', 1, N'Accounts', N'Permission', N'Create', NULL, NULL, 5, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (10, N'Permission Category', 1, N'Accounts', N'PermissionCategory', N'List', NULL, NULL, 6, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (11, N'Create Permission Category', 1, N'Accounts', N'PermissionCategory', N'Create', NULL, NULL, 6, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (12, N'Menu', 2, N'Menu', N'Menu', N'List', NULL, NULL, 7, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (13, N'Create Menu', 2, N'Menu', N'Menu', N'Create', NULL, NULL, 7, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (14, N'Menu Category', 2, N'Menu', N'MenuCategory', N'List', NULL, NULL, 8, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (15, N'Create Menu Category', 2, N'Menu', N'MenuCategory', N'Create', NULL, NULL, 8, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (16, N'Top Menu', 2, N'Menu', N'TopMenu', N'List', NULL, NULL, 9, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (17, N'Create Top Menu', 2, N'Menu', N'TopMenu', N'Create', NULL, NULL, 9, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (18, N'Top Menu Binding', 2, N'Menu', N'TopMenuBinding', N'List', NULL, NULL, 10, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (19, N'Page Binding', 2, N'Menu', N'PageBinding', N'List', NULL, NULL, 11, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (20, N'Mail Setting', 3, N'WebsiteConfig', N'WebsiteConfig', N'EditMailSetting', NULL, NULL, 12, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (21, N'Customer', 4, N'Shoe Store Operations', N'Customer', N'List', NULL, NULL, 16, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (22, N'Create Customer', 4, N'Shoe Store Operations', N'Customer', N'Create', NULL, NULL, 16, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (23, N'Sales Order', 4, N'Shoe Store Operations', N'SalesOrder', N'List', NULL, NULL, 17, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (24, N'Create Sales Order', 4, N'Shoe Store Operations', N'SalesOrder', N'Create', NULL, NULL, 17, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (25, N'Product', 4, N'Shoe Store Operations', N'Product', N'List', NULL, NULL, 18, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (26, N'Create Product', 4, N'Shoe Store Operations', N'Product', N'Create', NULL, NULL, 18, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (27, N'Product Category', 4, N'Shoe Store Operations', N'ProductCategory', N'List', NULL, NULL, 19, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (28, N'Create Product Category', 4, N'Shoe Store Operations', N'ProductCategory', N'Create', NULL, NULL, 19, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (29, N'Sales Order Status', 5, N'Shoe Store Settings', N'SalesOrderStatus', N'List', NULL, NULL, 20, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (31, N'Website Settings', 3, N'WebsiteConfig', N'Website', N'Settings', NULL, NULL, 12, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (32, N'Breviary Settings', 3, N'WebsiteConfig', N'Breviary', N'Settings', NULL, NULL, 12, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (33, N'Server Info', 3, N'WebsiteConfig', N'Server', N'Info', NULL, NULL, 12, 1, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (34, N'Create Page Binding', 2, N'Menu', N'PageBinding', N'Create', NULL, NULL, 11, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (35, N'Binding', 2, N'Menu', N'TopMenu', N'Binding', NULL, NULL, 10, 0, 0)
INSERT [dbo].[View_Menu] ([MenuId], [MenuName], [MenuCategoryId], [MenuCategoryName], [Controller], [Action], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (36, N'Create Sales Order Status', 5, N'Shoe Store Settings', N'SalesOrderStatus', N'Create', NULL, NULL, 20, 0, 0)
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (1, N'User', 1, N'Accounts', 1, N'User', 2, N'Add User')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (2, N'User Role', 1, N'Accounts', 3, N'User Role', 4, N'Add User Role')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (3, N'Role', 1, N'Accounts', 5, N'Role', 6, N'Add Role')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (4, N'Role Permission', 1, N'Accounts', 7, N'Role Permission', 7, N'Role Permission')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (5, N'Permission', 1, N'Accounts', 8, N'Permission', 9, N'Create Permission')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (6, N'Permission Category', 1, N'Accounts', 10, N'Permission Category', 11, N'Create Permission Category')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (7, N'Menu', 2, N'Menu', 12, N'Menu', 13, N'Create Menu')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (8, N'Menu Category', 1, N'Accounts', 14, N'Menu Category', 15, N'Create Menu Category')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (9, N'Top Menu', 2, N'Menu', 16, N'Top Menu', 17, N'Create Top Menu')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (10, N'Top Menu Binding', 1, N'Accounts', 18, N'Top Menu Binding', 18, N'Top Menu Binding')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (11, N'Page Binding', 2, N'Menu', 19, N'Page Binding', 34, N'Create Page Binding')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (12, N'Mail Settings', 3, N'WebsiteConfig', 20, N'Mail Setting', 20, N'Mail Setting')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (13, N'Menu Category', 2, N'Menu', 14, N'Menu Category', 15, N'Create Menu Category')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (14, N'Customer', 4, N'Shoe Store Operations', 21, N'Customer', 22, N'Create Customer')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (15, N'SalesOrderStatus', 5, N'Shoe Store Settings', 29, N'Sales Order Status', 36, N'Create Sales Order Status')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (16, N'Sales Order', 4, N'Shoe Store Operations', 23, N'Sales Order', 24, N'Create Sales Order')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (17, N'Product', 4, N'Shoe Store Operations', 25, N'Product', 26, N'Create Product')
INSERT [dbo].[View_PageBinding] ([PageBindingId], [PageTitle], [MenuCategoryId], [MenuCategoryName], [ListMenuId], [ListMenuName], [AddMenuId], [AddMenuName]) VALUES (18, N'Product Category', 4, N'Shoe Store Operations', 27, N'Product Category', 28, N'Create Product Category')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (1, N'User', N'Accounts')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (2, N'UserRole', N'Accounts')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (3, N'Role', N'Accounts')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (4, N'RolePermission', N'Accounts')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (5, N'Permission', N'Accounts')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (6, N'PermissionCategory', N'Accounts')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (7, N'Menu', N'Menu')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (8, N'MenuCategory', N'Menu')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (9, N'TopMenu', N'Menu')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (10, N'Top Menu Binding', N'Menu')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (11, N'Page Binding', N'Menu')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (12, N'WebsiteSettings', N'Website Config')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (13, N'Email Settings', N'Website Config')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (14, N'Breviary Settings', N'Website Config')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (15, N'Server Info', N'Website Config')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (16, N'Customer', N'Shoe Store Operations')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (17, N'Orders', N'Shoe Store Operations')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (18, N'Product', N'Shoe Store Operations')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (19, N'Product Category', N'Shoe Store Operations')
INSERT [dbo].[View_Permission] ([PermissionId], [PermissionName], [PermissionCategoryName]) VALUES (20, N'Sales Order Status', N'Shoe Store Settings')
INSERT [dbo].[View_Product] ([ProductId], [ProductName], [ProductCategoryId], [ProductCategoryName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (1, N'Kayak', 1, N'Chess', N'A boat for one person', CAST(275.00000 AS Decimal(10, 5)), NULL, N'image/jpeg')
INSERT [dbo].[View_Product] ([ProductId], [ProductName], [ProductCategoryId], [ProductCategoryName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (2, N'Lifejacket', 1, N'Chess', N'Protective and fashionable', CAST(48.95000 AS Decimal(10, 5)), NULL, N'image/jpeg')
INSERT [dbo].[View_Product] ([ProductId], [ProductName], [ProductCategoryId], [ProductCategoryName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (3, N'Soccer Ball', 1, N'Chess', N'FIFA-approved size and weight', CAST(19.50000 AS Decimal(10, 5)), NULL, N'image/jpeg')
INSERT [dbo].[View_Product] ([ProductId], [ProductName], [ProductCategoryId], [ProductCategoryName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (4, N'Corner Flags', 2, N'Soccer', N'Give your playing field a professional touch', CAST(34.95000 AS Decimal(10, 5)), NULL, N'image/jpeg')
INSERT [dbo].[View_Product] ([ProductId], [ProductName], [ProductCategoryId], [ProductCategoryName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (5, N'Stadium', 2, N'Soccer', N'Flat-packed, 35,000-seat stadium', CAST(79500.00000 AS Decimal(10, 5)), NULL, NULL)
INSERT [dbo].[View_Product] ([ProductId], [ProductName], [ProductCategoryId], [ProductCategoryName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (6, N'Thinking Cap', 2, N'Soccer', N'Improve your brain efficiency by 75%', CAST(16.00000 AS Decimal(10, 5)), NULL, NULL)
INSERT [dbo].[View_Product] ([ProductId], [ProductName], [ProductCategoryId], [ProductCategoryName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (7, N'Unsteady Chair', 3, N'Water Sports', N'Secretly give your opponent a disadvantage', CAST(29.95000 AS Decimal(10, 5)), NULL, NULL)
INSERT [dbo].[View_Product] ([ProductId], [ProductName], [ProductCategoryId], [ProductCategoryName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (8, N'Human Chess Board', 3, N'Water Sports', N'A fun game for the family', CAST(75.00000 AS Decimal(10, 5)), NULL, NULL)
INSERT [dbo].[View_Product] ([ProductId], [ProductName], [ProductCategoryId], [ProductCategoryName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (9, N'Bling-Bling King', 3, N'Water Sports', N'Gold-plated, diamond-studded King', CAST(1200.00000 AS Decimal(10, 5)), NULL, NULL)
INSERT [dbo].[View_Product] ([ProductId], [ProductName], [ProductCategoryId], [ProductCategoryName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (10, N'Johnny', 3, N'Water Sports', N'Teacher', CAST(123.00000 AS Decimal(10, 5)), NULL, NULL)
INSERT [dbo].[View_Product] ([ProductId], [ProductName], [ProductCategoryId], [ProductCategoryName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (16, N'tese', 3, N'Water Sports', N'sese', CAST(1212.00000 AS Decimal(10, 5)), NULL, NULL)
INSERT [dbo].[View_Product] ([ProductId], [ProductName], [ProductCategoryId], [ProductCategoryName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (17, N'newooow', 3, N'Water Sports', N'wew', CAST(123.00000 AS Decimal(10, 5)), NULL, NULL)
INSERT [dbo].[View_SalesOrder] ([SalesOrderId], [CustomerId], [CustomerName], [SalesOrderStatusId], [SalesOrderStatusName], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (1, 1, N'Customer1', 1, N'Order Placed', CAST(N'2015-08-31 13:00:00.897' AS DateTime), 0, N'', CAST(N'2015-08-31 13:00:00.897' AS DateTime), 0, N'', 0)
INSERT [dbo].[View_SalesOrder] ([SalesOrderId], [CustomerId], [CustomerName], [SalesOrderStatusId], [SalesOrderStatusName], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (2, 2, N'Customer2', 3, N'Delivered', CAST(N'2015-08-31 14:24:35.447' AS DateTime), 0, N'', CAST(N'2015-08-31 14:24:35.447' AS DateTime), 0, N'', 0)
INSERT [dbo].[View_SalesOrder] ([SalesOrderId], [CustomerId], [CustomerName], [SalesOrderStatusId], [SalesOrderStatusName], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (3, 1, N'Customer1', 1, N'Order Placed', CAST(N'2015-08-31 14:40:22.167' AS DateTime), 0, N'', CAST(N'2015-08-31 14:40:22.167' AS DateTime), 0, N'', 0)
INSERT [dbo].[View_TopMenu] ([TopMenuId], [TopMenuName], [MenuCategoryId], [MenuCategoryName], [PageLink], [Image], [ToolTip], [Sequence]) VALUES (1, N'Shortcut', 6, N'My Space', N'Menu/ShortCut', N'fa-outdent', N'Shortcut', 0)
INSERT [dbo].[View_TopMenu] ([TopMenuId], [TopMenuName], [MenuCategoryId], [MenuCategoryName], [PageLink], [Image], [ToolTip], [Sequence]) VALUES (2, N'My Account', 6, N'My Space', N'Menu/Member', N'fa-users', N'ShoeStore', 0)
INSERT [dbo].[View_TopMenu] ([TopMenuId], [TopMenuName], [MenuCategoryId], [MenuCategoryName], [PageLink], [Image], [ToolTip], [Sequence]) VALUES (3, N'Shoe Store', 4, N'Shoe Store Operations', N'System', N'fa-shopping-cart', N'System', 0)
INSERT [dbo].[View_TopMenu] ([TopMenuId], [TopMenuName], [MenuCategoryId], [MenuCategoryName], [PageLink], [Image], [ToolTip], [Sequence]) VALUES (3, N'Shoe Store', 5, N'Shoe Store Settings', N'System', N'fa-shopping-cart', N'System', 0)
INSERT [dbo].[View_TopMenu] ([TopMenuId], [TopMenuName], [MenuCategoryId], [MenuCategoryName], [PageLink], [Image], [ToolTip], [Sequence]) VALUES (4, N'System', 1, N'Accounts', N'System', N'fa-cog', N'System', 0)
INSERT [dbo].[View_TopMenu] ([TopMenuId], [TopMenuName], [MenuCategoryId], [MenuCategoryName], [PageLink], [Image], [ToolTip], [Sequence]) VALUES (4, N'System', 2, N'Menu', N'System', N'fa-cog', N'System', 0)
INSERT [dbo].[View_TopMenu] ([TopMenuId], [TopMenuName], [MenuCategoryId], [MenuCategoryName], [PageLink], [Image], [ToolTip], [Sequence]) VALUES (4, N'System', 3, N'WebsiteConfig', N'System', N'fa-cog', N'System', 0)
USE [master]
GO
ALTER DATABASE [ShoeStore5] SET  READ_WRITE 
GO
