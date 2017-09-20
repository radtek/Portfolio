USE [master]
GO
/****** Object:  Database [ShoeStore4]    Script Date: 09/05/2015 23:10:44 ******/
CREATE DATABASE [ShoeStore4] ON  PRIMARY 
( NAME = N'ShoeStore4', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\ShoeStore4.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ShoeStore4_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\ShoeStore4_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ShoeStore4] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShoeStore4].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ShoeStore4] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [ShoeStore4] SET ANSI_NULLS OFF
GO
ALTER DATABASE [ShoeStore4] SET ANSI_PADDING OFF
GO
ALTER DATABASE [ShoeStore4] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [ShoeStore4] SET ARITHABORT OFF
GO
ALTER DATABASE [ShoeStore4] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [ShoeStore4] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [ShoeStore4] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [ShoeStore4] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [ShoeStore4] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [ShoeStore4] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [ShoeStore4] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [ShoeStore4] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [ShoeStore4] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [ShoeStore4] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [ShoeStore4] SET  DISABLE_BROKER
GO
ALTER DATABASE [ShoeStore4] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [ShoeStore4] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [ShoeStore4] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [ShoeStore4] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [ShoeStore4] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [ShoeStore4] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [ShoeStore4] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [ShoeStore4] SET  READ_WRITE
GO
ALTER DATABASE [ShoeStore4] SET RECOVERY FULL
GO
ALTER DATABASE [ShoeStore4] SET  MULTI_USER
GO
ALTER DATABASE [ShoeStore4] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [ShoeStore4] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'ShoeStore4', N'ON'
GO
USE [ShoeStore4]
GO
/****** Object:  User [zrlogin]    Script Date: 09/05/2015 23:10:44 ******/
CREATE USER [zrlogin] FOR LOGIN [zrlogin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[PermissionCategories]    Script Date: 09/05/2015 23:10:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionCategories](
	[PermissionCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[PermissionCategoryName] [nvarchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_PermissionCategories] PRIMARY KEY CLUSTERED 
(
	[PermissionCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Category Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PermissionCategories', @level2type=N'COLUMN',@level2name=N'PermissionCategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Category Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PermissionCategories', @level2type=N'COLUMN',@level2name=N'PermissionCategoryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PermissionCategories', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
SET IDENTITY_INSERT [dbo].[PermissionCategories] ON
INSERT [dbo].[PermissionCategories] ([PermissionCategoryId], [PermissionCategoryName], [Sequence]) VALUES (1, N'Accounts', 0)
INSERT [dbo].[PermissionCategories] ([PermissionCategoryId], [PermissionCategoryName], [Sequence]) VALUES (2, N'Menu', 0)
INSERT [dbo].[PermissionCategories] ([PermissionCategoryId], [PermissionCategoryName], [Sequence]) VALUES (3, N'Website Config', 0)
INSERT [dbo].[PermissionCategories] ([PermissionCategoryId], [PermissionCategoryName], [Sequence]) VALUES (4, N'Shortcut', 0)
INSERT [dbo].[PermissionCategories] ([PermissionCategoryId], [PermissionCategoryName], [Sequence]) VALUES (5, N'Shoe Store Settings', 0)
INSERT [dbo].[PermissionCategories] ([PermissionCategoryId], [PermissionCategoryName], [Sequence]) VALUES (6, N'Shoe Store Operations', 0)
SET IDENTITY_INSERT [dbo].[PermissionCategories] OFF
/****** Object:  Table [dbo].[Administrators]    Script Date: 09/05/2015 23:10:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Administrators](
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
 CONSTRAINT [PK_Administrators] PRIMARY KEY CLUSTERED 
(
	[AdminId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AdminId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'AdminId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AdminName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'AdminName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Password' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Full Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'FullName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Gender' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'Gender'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tel' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'Tel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Email' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valid From' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'ValidFrom'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valid To' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'ValidTo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Activated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'IsActivated'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Login Times' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'LoginTimes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'CreatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'CreatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'UpdatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'UpdatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'UpdatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Administrators', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
SET IDENTITY_INSERT [dbo].[Administrators] ON
INSERT [dbo].[Administrators] ([AdminId], [AdminName], [Password], [FullName], [Gender], [Tel], [Email], [ValidFrom], [ValidTo], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (1, N'Admin', N'E10ADC3949BA59ABBE56E057F20F883E', N'Johnny', 1, N'3124786579', N'jojozhuang@gmail.com', CAST(0x00009B6700000000 AS DateTime), CAST(0x0000C07F00000000 AS DateTime), 1, 0, CAST(0x0000A5030114F080 AS DateTime), 0, N'', CAST(0x0000A50501711CEE AS DateTime), 0, N'', 0)
INSERT [dbo].[Administrators] ([AdminId], [AdminName], [Password], [FullName], [Gender], [Tel], [Email], [ValidFrom], [ValidTo], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (4, N'Johnny', N'E10ADC3949BA59ABBE56E057F20F883E', N'12222222222222', 0, N'aaaaaaaaaaaaaaaaaaaaaaaa', N'aeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee', CAST(0x0000A6DA00000000 AS DateTime), CAST(0x0000A84700000000 AS DateTime), 0, 0, CAST(0x0000A503015826AD AS DateTime), 0, N'', CAST(0x0000A503015A43D2 AS DateTime), 0, N'', 0)
INSERT [dbo].[Administrators] ([AdminId], [AdminName], [Password], [FullName], [Gender], [Tel], [Email], [ValidFrom], [ValidTo], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (5, N'Jinjing', N'E10ADC3949BA59ABBE56E057F20F883E', N'Jinjing', 0, N'sdf', N'wefw', CAST(0x0000A12500000000 AS DateTime), CAST(0x0000A12500000000 AS DateTime), 1, 0, CAST(0x0000A5030164ABE4 AS DateTime), 0, N'', CAST(0x0000A50501712EBA AS DateTime), 0, N'', 0)
INSERT [dbo].[Administrators] ([AdminId], [AdminName], [Password], [FullName], [Gender], [Tel], [Email], [ValidFrom], [ValidTo], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (6, N'Yaoyao', N'E10ADC3949BA59ABBE56E057F20F883E', N'Yaoyao', 0, NULL, NULL, CAST(0x0000A12500000000 AS DateTime), CAST(0x0000A12500000000 AS DateTime), 0, 0, CAST(0x0000A5030164BF66 AS DateTime), 0, N'', CAST(0x0000A5030164BF66 AS DateTime), 0, N'', 0)
SET IDENTITY_INSERT [dbo].[Administrators] OFF
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 09/05/2015 23:10:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201509052009480_InitialCreate', N'Johnny.ShoeStore.Domain.Concrete.AppIdentityDbContext', 0x1F8B0800000000000400DD5CDB6EE436127D5F60FF41D053B270BA7DD9194C8CEE049EB69DF5667CC1B427C8DB802DB1DBC4489422528E8D45BE2C0FF9A4FD852D4AD48D2275E9565F6631C0A02592A78AC522592C1DFABF7FFE35F9F1C5F7AC671C3112D0A97D323AB62D4C9DC0257435B563BEFCEE9DFDE30F7FFFDBE4CAF55FAC5FB27A67A21EB4A46C6A3F711E9E8FC7CC79C23E62239F3851C082251F39813F466E303E3D3EFE7E7C7232C600610396654D3EC694131F270FF0380BA883431E23EF3670B1C7E47B289927A8D61DF2310B9183A7F6BF83274A5F47F3A700CF7910E1D165E02342470223C21CDBD6854710E835C7DED2B610A501471CB43EFFC4A04514D0D53C8417C87B7C0D31D45B228F61D99BF3A27AD78E1D9F8A8E8D8B86199413330EAAF5033C3993961AABCDD7B2B79D5B126C790536E7AFA2D7893DA7F68D8B93571F030F0CA00A3C9F7991A83CB56F7311172CBCC37C94351CA590D711C0FD1E445F4665C423AB73BBA3DCB34E47C7E2DF91358B3D1E47784A71CC23E41D590FF1C223CECFF8F531F882E9F4EC64B13C7BF7E62D72CFDEFE139FBD29F714FA0AF52A2FE0D54314843802DDF032EFBF6D8DABEDC66AC3BC59A94D6A15F0259824B6758B5E3E60BAE24F307D4EDFD9D63579C16EF6463AD7274A604E41231EC5F078177B1E5A78382F1F37CA14FF37483D7DF37610A977E899AC92A157E4C3C489986D7DC45E52CA9E48984EAFCA787F96D5AEA3C017CF55FF4A4B3FCF83387244670263954714AD30AF6A371917CEDBC9A505D4F06E9DA11EBE6B0B4DEBEEADAD2A3AB4CE4CC844EC7A3664FA6E576E678FBB08C33647336D590916C16C24318E2C43CDC2894EBA3A1185CED9D67BC4B0B4933A5D95EEEAFA254678D37E098CC1FBF57FBCD65F815DBC0116FB0E5220625A92C8C7792FDF07E01788F6D6F90131066B9DFB2FC49E1A54879F03A83EC74E1C81ABCE39F2C3AD4B7B780A28BE8BFD859809BB9335D8D03CFE1E5C2307A6DC1515AD36C6FB10385F82985F51F71271FC893B19A0787C247E778041D4B9701CCCD83538337667011C2A32C01BCACF4E7BC389C56ADFC1D6CC43C4D7475B7239FD9C552922AD6A492DCA528A751156934A1F8215A1CD2A6555EA2AA525469564715F950448B346B2465DA1A4C0A84F5A3A580C9A587CF82034813DFC2874B34DD734874B664C82899F30C5112C3FEE03E21C47B418812EF37D1F9B7C327C42E8D6F79444D22FC88B8716B5D66C4826FBF0B321813DFCD990A809AF9F892BA2890E47B3AC32C077AAAF3FF5B5CF3945B35D4F874A37772D7C376B8069BA5C3016382499059AA49C4CA954F587D8CB6ACFAF980E7DB7E0E82404D78637D0375B75AA7B7A893DCCB175E1A449CB19620E72EB66840EB93D14CB76548D6245AEA6AADC3F6A32C1D371241A21717861305309E5F56941A84342E4B55A4969D9710B137DCF65A8259738C454086CB54417E1FAD48C502097A30C4A9B8526E392C7353BA2126D9AC6DA147A16E39C671276E27B8658D7E077323EDB8AE3E92DB303A7D39BA08B6063DA70970E27CF126D03AC1E2CF6ED70CA49C6E0703204DAAAC3552DB34387AB9AE0AB71B8F4A8D836BECAB971DFEE563DA8EE7E5BD59A6587BE56E9FF81B95A1AEB411B0E2D7054B85B3636970B51885FB8E630047ACAF31093A1A5EA0A02618E79351552C497ADC97E0D88EA344D808563B580CA0F8235A07CC2F4502ACB79356A2577F31EB059DEAA1156AED90A6C69ECEBD8E50FA2A58AE6CFA6AA53768AF2F39EE55E5073EE4E41790947E308EAE254ED7807A3A879CBBA419A62CD2ED166A903D2E80D863044880623644A0F6685CCE5CC56D005405D42A0B5ACA0842D062B644A0F6605E95B66236836E50EDBF25A26A86EA5034D86ECC49FEF0279D9649CB29AE48BC9D8407F9ADCA230247455A243C937D63CE542CDBE9BF7A705F929C6D8611A7650AE6D2E8907115A61A5144483A6D72462FC1271B44022DF3173FD5A35ED9E67589E3391E56DAD3E78D93A9DD516BFE5DACBC4EFFBE5376D14B1CAFAFBAD267890D8D7D0735F441E098AC62F925DB0D6DC12B435E4A14893D99E055EEC537320646E9D7E972AB74FDFD411266345FF5AC053B3621F33B75A3763441C9C5D41439724F17F92112F7137CACD2F097322E2138A40BB43B46FB3950ECEC8D53874A796CECA8BC5A5D3D2535FF6B7327E791CBDFE189A214CE3989D82CAE3603A199951B2C46419C594ACDCDB309A02EBCD96B6E4E4D27FC48C2DB7B35B481A511940BEEA895162A2D4C04A65DD51AB64A13266B5A43BA2C2082A432A453DB42CF37E2A4A960BD6C23358545FA3BB843AD3A78C5E2FED8EACE1FC94A135C56B606B7456CBBAA36A684165604D7177EC8223A42EA1DB8AC706D8C58CE7EA81B6B134F1B2D93E66C0D8CEFA38CC3658E27194814AAF7B6249A6460D4CBE3F48DF32662B06F2AD34FBB6996F1930CCAB52850F515D941A491C66CC0AC9A1B2F037913CCC78FD3C78AB7E524B75A85572E979CA43496D4C649AA1FDFA572DEF9056B1ADCC8CB0E9BF328EFD91A8309AFFE6CD3C82C5129F55B845942C31E329B1C73E3D3E7EA7DC193B9CFB5B63C65C4F93A6315DE2AA8ED90E387AF41945CE138AEA8C990DEE3815A0B58F2337D4C52F53FB3F49ABF3248F277E25AF8FAC1BF68992DF6228788C626CFD5167EEF6D44A39A16ED4E7F5EF30AD31AA3BBFFFD37DCC6E7EFD9C363DB2EE23988FE7D6B13252EBF84FF556502F6DD2A61B68D3E7AE50FD10F8554DD3CAF5142DAA32CDD6BF8DB2207C909B289996DFF8E8E5DBBEAA696F9B6C84A8B9513214DE202634DD185907CB785BC485479EDC16E9D759FDED91755433DE1C21B43F987A6FA4FBF293B51C6603DBE08AC2CE97A4C4CEADFCFD8DC8BCFBDE936A34FF8D267A9DCADF036E03BAFE1A9EF19531DD07DB1D3544F6C1B0F7E9DA5B67AF1F0A61BDA036ED97A7BE4B6A7AC367A8AF9A91BE478EA68693B63FFEF9AE7DC9940B3E50F26F37B6F98138936422EE8F5BBE6B6732257F0FD4993A31C90FC497F6B5CFEDC9933A6F717BE789D7A975866F32BA0C701B0F3C4D97C3097C11C0E0A7115F7A5D564F7034092B9CC428B0A862166A6656AA82F38952939797348BE9D737B93137764ED669166BE00F37C996EB78A36C59A759B681B5BB0FE6BA9657ABBB45D0B25E3591A00E91A95ED1D870F1A12D766CFC607E88C4F48D3A5DF17EC397DCC3E3A16FD4E5215DBB07EFBCFED115F6B2D25FE684FD9491550121B89D143B955D2CAF73439741B6992A1A6555948CC62DE6C8852DEE22E264891C0EC522279BDCBF97DCD12B7F81DD1B7A1FF330E6D065EC2FBC4A82486CCA4DF213727D55E7C97D98FCC99821BA006A1291CBBEA7EF63E2B9B9DED79A1C8A0142ECF632032AC6928B4CE8EA3547BA0B68472069BE3C4879C47EE80118BBA773F48CD7D10DDCEF035E21E7B5C8989940DA07A26AF6C92541AB08F94C6214EDE1117CD8F55F7EF81F7BC70DECA0560000, N'6.1.3-40302')
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 09/05/2015 23:10:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Discriminator] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles] 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100', N'Administrators', N'AppRole')
/****** Object:  Table [dbo].[MenuCategories]    Script Date: 09/05/2015 23:10:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuCategories](
	[MenuCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[MenuCategoryName] [nvarchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_MenuCategories] PRIMARY KEY CLUSTERED 
(
	[MenuCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Category Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MenuCategories', @level2type=N'COLUMN',@level2name=N'MenuCategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Category Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MenuCategories', @level2type=N'COLUMN',@level2name=N'MenuCategoryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MenuCategories', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
SET IDENTITY_INSERT [dbo].[MenuCategories] ON
INSERT [dbo].[MenuCategories] ([MenuCategoryId], [MenuCategoryName], [Sequence]) VALUES (1, N'Accounts', 0)
INSERT [dbo].[MenuCategories] ([MenuCategoryId], [MenuCategoryName], [Sequence]) VALUES (2, N'Menu', 0)
INSERT [dbo].[MenuCategories] ([MenuCategoryId], [MenuCategoryName], [Sequence]) VALUES (3, N'WebsiteConfig', 0)
INSERT [dbo].[MenuCategories] ([MenuCategoryId], [MenuCategoryName], [Sequence]) VALUES (4, N'Shoe Store Operations', 0)
INSERT [dbo].[MenuCategories] ([MenuCategoryId], [MenuCategoryName], [Sequence]) VALUES (5, N'Shoe Store Settings', 0)
INSERT [dbo].[MenuCategories] ([MenuCategoryId], [MenuCategoryName], [Sequence]) VALUES (6, N'My Space', 0)
SET IDENTITY_INSERT [dbo].[MenuCategories] OFF
/****** Object:  Table [dbo].[MailSettings]    Script Date: 09/05/2015 23:10:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MailSettings](
	[MailSettingId] [int] IDENTITY(1,1) NOT NULL,
	[SmtpServer] [varchar](50) NOT NULL,
	[SmtpPort] [int] NOT NULL,
	[EmailAddress] [varchar](100) NOT NULL,
	[EmailPassword] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MailSettings] PRIMARY KEY CLUSTERED 
(
	[MailSettingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mail Setting Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MailSettings', @level2type=N'COLUMN',@level2name=N'MailSettingId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SMTP Server' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MailSettings', @level2type=N'COLUMN',@level2name=N'SmtpServer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SMTP Port' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MailSettings', @level2type=N'COLUMN',@level2name=N'SmtpPort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Email Address' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MailSettings', @level2type=N'COLUMN',@level2name=N'EmailAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Email Password' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MailSettings', @level2type=N'COLUMN',@level2name=N'EmailPassword'
GO
SET IDENTITY_INSERT [dbo].[MailSettings] ON
INSERT [dbo].[MailSettings] ([MailSettingId], [SmtpServer], [SmtpPort], [EmailAddress], [EmailPassword]) VALUES (1, N'smtp.163.com2', 123123, N'ajohn@163.com2', N'123343')
SET IDENTITY_INSERT [dbo].[MailSettings] OFF
/****** Object:  Table [dbo].[Customers]    Script Date: 09/05/2015 23:10:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](50) NOT NULL,
	[Password] [varchar](32) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Gender] [bit] NULL,
	[Tel] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[IsActivated] [bit] NOT NULL,
	[LoginTimes] [int] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedByName] [varchar](50) NOT NULL,
	[UpdatedTime] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[UpdatedByName] [varchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'CustomerId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'CustomerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Password' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Full Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'FullName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Gender' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'Gender'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tel' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'Tel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Email' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Activated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'IsActivated'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Login Times' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'LoginTimes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'CreatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Created By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'CreatedByName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated Time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'UpdatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'UpdatedById'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Updated By Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customers', @level2type=N'COLUMN',@level2name=N'UpdatedByName'
GO
SET IDENTITY_INSERT [dbo].[Customers] ON
INSERT [dbo].[Customers] ([CustomerId], [CustomerName], [Password], [FullName], [Gender], [Tel], [Email], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (1, N'Customer1', N'E10ADC3949BA59ABBE56E057F20F883E', N'Johnny Walker', 1, N'3124786579', N'jojozhuang@gmail.com', 1, 0, CAST(0x0000A50500B230E2 AS DateTime), 0, N'', CAST(0x0000A50500B230E3 AS DateTime), 0, N'', 0)
INSERT [dbo].[Customers] ([CustomerId], [CustomerName], [Password], [FullName], [Gender], [Tel], [Email], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (2, N'Customer2', N'E10ADC3949BA59ABBE56E057F20F883E', N'Johnny Walker2', 0, N'31247865791113', N'jojozhuang@gmail.com222', 1, 0, CAST(0x0000A50500B2A453 AS DateTime), 0, N'', CAST(0x0000A50500B2BB1F AS DateTime), 0, N'', 0)
INSERT [dbo].[Customers] ([CustomerId], [CustomerName], [Password], [FullName], [Gender], [Tel], [Email], [IsActivated], [LoginTimes], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (3, N'RongZhuang', N'E10ADC3949BA59ABBE56E057F20F883E', N'Rong Zhuang', 0, NULL, N'RZHUANG@cdm.depaul.edu', 0, 0, CAST(0x0000A50500B2D28C AS DateTime), 0, N'', CAST(0x0000A50500B2D28C AS DateTime), 0, N'', 0)
SET IDENTITY_INSERT [dbo].[Customers] OFF
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 09/05/2015 23:10:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers] 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'096739d1-5fa2-4f91-ad16-e5915ee68e8a', N'johnny2333@example.com', 0, N'AC7jnPWR4A0J8KOt8mNiChgYLkKh72jfC6Z84ldmi7f90QlY4bNaTBqdWl1Lzj6nxA==', N'531610a8-e578-4633-9a58-e28bb2260429', NULL, 0, 0, NULL, 0, 0, N'johnny222')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'65bfb417-22b7-4d1e-9af9-b170ecb0c1a4', N'admin@example.com', 0, N'AAa4Vlj7RNuef8YJ7i21MYhjkuKZBB5C+NPOeI9Q0q1iImYnbL+UYu7NcM2g2jOvFQ==', N'aadc5008-c7c1-4946-8459-f43eadc0461f', NULL, 0, 0, NULL, 0, 0, N'Admin')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6dcfead5-9d3e-4621-9e01-7f49ee07bdf4', N'amei@example.com', 0, N'AHu9RpvFl4BWjGivaSm7Oo9ivPXIZpydvKe35bn7FIkZdQWQisMiK9c4daJX1GDgrw==', N'c8139ee5-8fb1-4da6-bfc3-892bf7470565', NULL, 0, 0, NULL, 0, 0, N'amei')
/****** Object:  StoredProcedure [dbo].[sp_SequenceMove]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  Procedure [dbo].[sp_SequenceMove]
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
/****** Object:  Table [dbo].[SalesOrderStatus]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrderStatus](
	[SalesOrderStatusId] [int] IDENTITY(1,1) NOT NULL,
	[SalesOrderStatusName] [nvarchar](50) NOT NULL,
	[IsActivated] [bit] NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_SalesOrderStatus] PRIMARY KEY CLUSTERED 
(
	[SalesOrderStatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales Order Status Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SalesOrderStatus', @level2type=N'COLUMN',@level2name=N'SalesOrderStatusId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales Order Status Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SalesOrderStatus', @level2type=N'COLUMN',@level2name=N'SalesOrderStatusName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Is Activated' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SalesOrderStatus', @level2type=N'COLUMN',@level2name=N'IsActivated'
GO
SET IDENTITY_INSERT [dbo].[SalesOrderStatus] ON
INSERT [dbo].[SalesOrderStatus] ([SalesOrderStatusId], [SalesOrderStatusName], [IsActivated], [Sequence]) VALUES (1, N'Order Placed', 1, 0)
INSERT [dbo].[SalesOrderStatus] ([SalesOrderStatusId], [SalesOrderStatusName], [IsActivated], [Sequence]) VALUES (2, N'Shipped', 1, 0)
INSERT [dbo].[SalesOrderStatus] ([SalesOrderStatusId], [SalesOrderStatusName], [IsActivated], [Sequence]) VALUES (3, N'Delivered', 1, 0)
INSERT [dbo].[SalesOrderStatus] ([SalesOrderStatusId], [SalesOrderStatusName], [IsActivated], [Sequence]) VALUES (4, N'Cancelled2', 1, 0)
SET IDENTITY_INSERT [dbo].[SalesOrderStatus] OFF
/****** Object:  Table [dbo].[Roles]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Role Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Role Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'RoleName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
SET IDENTITY_INSERT [dbo].[Roles] ON
INSERT [dbo].[Roles] ([RoleId], [RoleName], [Description], [Sequence]) VALUES (1, N'System Admin', N'System Administrator', 0)
INSERT [dbo].[Roles] ([RoleId], [RoleName], [Description], [Sequence]) VALUES (2, N'Website Admin', N'Website Administrator', 0)
INSERT [dbo].[Roles] ([RoleId], [RoleName], [Description], [Sequence]) VALUES (3, N'Product Editor', N'Product Editor', 0)
SET IDENTITY_INSERT [dbo].[Roles] OFF
/****** Object:  Table [dbo].[ProductCategories]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategories](
	[ProductCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryName] [nvarchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_ProductCategories] PRIMARY KEY CLUSTERED 
(
	[ProductCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product Category Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductCategories', @level2type=N'COLUMN',@level2name=N'ProductCategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product Category Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductCategories', @level2type=N'COLUMN',@level2name=N'ProductCategoryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductCategories', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
SET IDENTITY_INSERT [dbo].[ProductCategories] ON
INSERT [dbo].[ProductCategories] ([ProductCategoryId], [ProductCategoryName], [Sequence]) VALUES (1, N'Chess', 0)
INSERT [dbo].[ProductCategories] ([ProductCategoryId], [ProductCategoryName], [Sequence]) VALUES (2, N'Soccer', 0)
INSERT [dbo].[ProductCategories] ([ProductCategoryId], [ProductCategoryName], [Sequence]) VALUES (3, N'Water Sports', 0)
SET IDENTITY_INSERT [dbo].[ProductCategories] OFF
/****** Object:  Table [dbo].[TopMenus]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TopMenus](
	[TopMenuId] [int] IDENTITY(1,1) NOT NULL,
	[TopMenuName] [nvarchar](50) NOT NULL,
	[PageLink] [varchar](100) NOT NULL,
	[Image] [varchar](200) NULL,
	[ToolTip] [nvarchar](50) NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_TopMenus] PRIMARY KEY CLUSTERED 
(
	[TopMenuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Top Menu Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TopMenus', @level2type=N'COLUMN',@level2name=N'TopMenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Top Menu Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TopMenus', @level2type=N'COLUMN',@level2name=N'TopMenuName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Default Page' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TopMenus', @level2type=N'COLUMN',@level2name=N'PageLink'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ToolTip' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TopMenus', @level2type=N'COLUMN',@level2name=N'ToolTip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TopMenus', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
SET IDENTITY_INSERT [dbo].[TopMenus] ON
INSERT [dbo].[TopMenus] ([TopMenuId], [TopMenuName], [PageLink], [Image], [ToolTip], [Sequence]) VALUES (1, N'Shortcut', N'Menu/ShortCut', N'fa-outdent', N'Shortcut', 0)
INSERT [dbo].[TopMenus] ([TopMenuId], [TopMenuName], [PageLink], [Image], [ToolTip], [Sequence]) VALUES (2, N'My Account', N'Menu/Member', N'fa-users', N'ShoeStore', 0)
INSERT [dbo].[TopMenus] ([TopMenuId], [TopMenuName], [PageLink], [Image], [ToolTip], [Sequence]) VALUES (3, N'Shoe Store', N'System', N'fa-shopping-cart', N'System', 0)
INSERT [dbo].[TopMenus] ([TopMenuId], [TopMenuName], [PageLink], [Image], [ToolTip], [Sequence]) VALUES (4, N'System', N'System', N'fa-cog', N'System', 0)
SET IDENTITY_INSERT [dbo].[TopMenus] OFF
/****** Object:  Table [dbo].[TopMenuBindings]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopMenuBindings](
	[TopMenuId] [int] NOT NULL,
	[MenuCategoryId] [int] NOT NULL,
 CONSTRAINT [PK_TopMenuBindings] PRIMARY KEY CLUSTERED 
(
	[TopMenuId] ASC,
	[MenuCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Top Menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TopMenuBindings', @level2type=N'COLUMN',@level2name=N'TopMenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Category' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TopMenuBindings', @level2type=N'COLUMN',@level2name=N'MenuCategoryId'
GO
INSERT [dbo].[TopMenuBindings] ([TopMenuId], [MenuCategoryId]) VALUES (1, 4)
INSERT [dbo].[TopMenuBindings] ([TopMenuId], [MenuCategoryId]) VALUES (2, 6)
INSERT [dbo].[TopMenuBindings] ([TopMenuId], [MenuCategoryId]) VALUES (3, 4)
INSERT [dbo].[TopMenuBindings] ([TopMenuId], [MenuCategoryId]) VALUES (3, 5)
INSERT [dbo].[TopMenuBindings] ([TopMenuId], [MenuCategoryId]) VALUES (4, 1)
INSERT [dbo].[TopMenuBindings] ([TopMenuId], [MenuCategoryId]) VALUES (4, 2)
INSERT [dbo].[TopMenuBindings] ([TopMenuId], [MenuCategoryId]) VALUES (4, 3)
/****** Object:  Table [dbo].[Products]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryId] [int] NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Price] [decimal](16, 2) NOT NULL,
	[ImageData] [varbinary](max) NULL,
	[ImageMimeType] [varchar](50) NULL,
 CONSTRAINT [PK__Products__B40CC6ED7F60ED59] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (1, 1, N'Kayak', N'A boat for one person', CAST(275.00 AS Decimal(16, 2)), NULL, N'image/jpeg')
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (2, 1, N'Lifejacket', N'Protective and fashionable', CAST(48.95 AS Decimal(16, 2)), NULL, N'image/jpeg')
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (3, 1, N'Soccer Ball', N'FIFA-approved size and weight', CAST(19.50 AS Decimal(16, 2)), NULL, N'image/jpeg')
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (4, 2, N'Corner Flags', N'Give your playing field a professional touch', CAST(34.95 AS Decimal(16, 2)), NULL, N'image/jpeg')
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (5, 2, N'Stadium', N'Flat-packed, 35,000-seat stadium', CAST(79500.00 AS Decimal(16, 2)), NULL, NULL)
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (6, 2, N'Thinking Cap', N'Improve your brain efficiency by 75%', CAST(16.00 AS Decimal(16, 2)), NULL, NULL)
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (7, 3, N'Unsteady Chair', N'Secretly give your opponent a disadvantage', CAST(29.95 AS Decimal(16, 2)), NULL, NULL)
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (8, 3, N'Human Chess Board', N'A fun game for the family', CAST(75.00 AS Decimal(16, 2)), NULL, NULL)
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (9, 3, N'Bling-Bling King', N'Gold-plated, diamond-studded King', CAST(1200.00 AS Decimal(16, 2)), NULL, NULL)
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (10, 3, N'Johnny', N'Teacher', CAST(123.00 AS Decimal(16, 2)), NULL, NULL)
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (16, 3, N'tese', N'sese', CAST(1212.00 AS Decimal(16, 2)), NULL, NULL)
INSERT [dbo].[Products] ([ProductId], [ProductCategoryId], [ProductName], [Description], [Price], [ImageData], [ImageMimeType]) VALUES (17, 3, N'newooow', N'wew', CAST(123.00 AS Decimal(16, 2)), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Products] OFF
/****** Object:  Table [dbo].[Permissions]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[PermissionId] [int] IDENTITY(1,1) NOT NULL,
	[PermissionName] [nvarchar](50) NOT NULL,
	[PermissionCategoryId] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'PermissionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'PermissionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Category' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'PermissionCategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
SET IDENTITY_INSERT [dbo].[Permissions] ON
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (1, N'Administrator', 1, 0)
INSERT [dbo].[Permissions] ([PermissionId], [PermissionName], [PermissionCategoryId], [Sequence]) VALUES (2, N'AdminRole', 1, 0)
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
SET IDENTITY_INSERT [dbo].[Permissions] OFF
/****** Object:  Table [dbo].[SalesOrders]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesOrders](
	[SalesOrderId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[SalesOrderStatusId] [int] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedByName] [varchar](50) NULL,
	[UpdatedTime] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[UpdatedByName] [varchar](50) NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_SalesOrder] PRIMARY KEY CLUSTERED 
(
	[SalesOrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[SalesOrders] ON
INSERT [dbo].[SalesOrders] ([SalesOrderId], [CustomerId], [SalesOrderStatusId], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (1, 1, 1, CAST(0x0000A50500D63CCD AS DateTime), 0, N'', CAST(0x0000A50500D63CCD AS DateTime), 0, N'', 0)
INSERT [dbo].[SalesOrders] ([SalesOrderId], [CustomerId], [SalesOrderStatusId], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (2, 2, 3, CAST(0x0000A50500ED778A AS DateTime), 0, N'', CAST(0x0000A50500ED778A AS DateTime), 0, N'', 0)
INSERT [dbo].[SalesOrders] ([SalesOrderId], [CustomerId], [SalesOrderStatusId], [CreatedTime], [CreatedById], [CreatedByName], [UpdatedTime], [UpdatedById], [UpdatedByName], [Sequence]) VALUES (3, 1, 1, CAST(0x0000A50500F1CCFA AS DateTime), 0, N'', CAST(0x0000A50500F1CCFA AS DateTime), 0, N'', 0)
SET IDENTITY_INSERT [dbo].[SalesOrders] OFF
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles] 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'65bfb417-22b7-4d1e-9af9-b170ecb0c1a4', N'00cc6a9a-315c-4ac6-87dc-1ec7c63b4100')
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdminRoles]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminRoles](
	[AdminId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_AdminRoles] PRIMARY KEY CLUSTERED 
(
	[AdminId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Admin Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdminRoles', @level2type=N'COLUMN',@level2name=N'AdminId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Role Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdminRoles', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdminRoles', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
INSERT [dbo].[AdminRoles] ([AdminId], [RoleId], [Sequence]) VALUES (1, 1, 0)
INSERT [dbo].[AdminRoles] ([AdminId], [RoleId], [Sequence]) VALUES (4, 2, 0)
INSERT [dbo].[AdminRoles] ([AdminId], [RoleId], [Sequence]) VALUES (5, 1, 0)
INSERT [dbo].[AdminRoles] ([AdminId], [RoleId], [Sequence]) VALUES (6, 3, 0)
/****** Object:  Table [dbo].[Menus]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Menus](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](50) NOT NULL,
	[MenuCategoryId] [int] NOT NULL,
	[PageLink] [varchar](100) NOT NULL,
	[ToolTip] [nvarchar](100) NULL,
	[Image] [varchar](200) NULL,
	[PermissionId] [int] NOT NULL,
	[IsDisplay] [bit] NOT NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menus', @level2type=N'COLUMN',@level2name=N'MenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menus', @level2type=N'COLUMN',@level2name=N'MenuName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Category' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menus', @level2type=N'COLUMN',@level2name=N'MenuCategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Page Link' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menus', @level2type=N'COLUMN',@level2name=N'PageLink'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ToolTip' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menus', @level2type=N'COLUMN',@level2name=N'ToolTip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Image' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menus', @level2type=N'COLUMN',@level2name=N'Image'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menus', @level2type=N'COLUMN',@level2name=N'PermissionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Display in the Menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menus', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menus', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
SET IDENTITY_INSERT [dbo].[Menus] ON
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (1, N'User', 1, N'User/List', N'Administrator Management', NULL, 1, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (2, N'Add User', 1, N'User/Create', N'Create Administrator', NULL, 1, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (3, N'AdminRole', 1, N'Account/AdminRole', NULL, NULL, 2, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (4, N'Add AdminRole', 1, N'Account/CreateAdminRole', NULL, NULL, 2, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (5, N'Role', 1, N'Account/Role', NULL, NULL, 3, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (6, N'Add Role', 1, N'Account/CreateRole', NULL, NULL, 3, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (7, N'RolePermission', 1, N'Account/RolePermission', NULL, NULL, 4, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (8, N'Permission', 1, N'Account/Permission', NULL, NULL, 5, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (9, N'CreatePermission', 1, N'Account/CreatePermission', NULL, NULL, 5, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (10, N'PermissionCategory', 1, N'Account/PermissionCategory', NULL, NULL, 6, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (11, N'CreatePermissionCategory', 1, N'Account/CreatePermissionCategory', NULL, NULL, 6, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (12, N'Menu', 2, N'Menu/Menu', NULL, NULL, 7, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (13, N'CreateMenu', 2, N'Menu/CreateMenu', NULL, NULL, 7, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (14, N'MenuCategory', 2, N'Menu/MenuCategory', NULL, NULL, 8, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (15, N'CreateMenuCategory', 2, N'Menu/CreateMenuCategory', NULL, NULL, 8, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (16, N'TopMenu', 2, N'Menu/TopMenu', NULL, NULL, 9, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (17, N'CreateTopMenu', 2, N'Menu/CreateTopMenu', NULL, NULL, 9, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (18, N'TopMenuBinding', 2, N'Menu/TopMenuBinding', NULL, NULL, 10, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (19, N'PageBinding', 2, N'Menu/PageBinding', NULL, NULL, 11, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (20, N'Mail Setting', 3, N'WebsiteConfig/EditMailSetting', NULL, NULL, 12, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (21, N'Customer', 4, N'ShoeStore/Customer', NULL, NULL, 16, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (22, N'Create Customer', 4, N'ShoeStore/CreateCustomer', NULL, NULL, 16, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (23, N'Sales Order', 4, N'ShoeStore/SalesOrder', NULL, NULL, 17, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (24, N'Create Sales Order', 4, N'ShoeStore/CreateSalesOrder', NULL, NULL, 17, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (25, N'Product', 4, N'ShoeStore/Product', NULL, NULL, 18, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (26, N'Create Product', 4, N'ShoeStore/CreateProduct', NULL, NULL, 18, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (27, N'ProductCategory', 4, N'ShoeStore/ProductCategory', NULL, NULL, 19, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (28, N'Create Product Category', 4, N'ShoeStore/CreateProductCategory', NULL, NULL, 19, 0, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (29, N'Sales Order Status', 5, N'ShoeStore/SalesOrderStatus', NULL, NULL, 20, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (31, N'Website Settings', 3, N'Website/Settings', NULL, NULL, 12, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (32, N'Breviary Settings', 3, N'Breviary/Settings', NULL, NULL, 12, 1, 0)
INSERT [dbo].[Menus] ([MenuId], [MenuName], [MenuCategoryId], [PageLink], [ToolTip], [Image], [PermissionId], [IsDisplay], [Sequence]) VALUES (33, N'Server Info', 3, N'Server/Info', NULL, NULL, 12, 1, 0)
SET IDENTITY_INSERT [dbo].[Menus] OFF
/****** Object:  Table [dbo].[PageBindings]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageBindings](
	[PageBindingId] [int] IDENTITY(1,1) NOT NULL,
	[PageTitle] [nvarchar](50) NOT NULL,
	[MenuCategoryId] [int] NOT NULL,
	[ListMenuId] [int] NOT NULL,
	[AddMenuId] [int] NOT NULL,
 CONSTRAINT [PK_PageBindings] PRIMARY KEY CLUSTERED 
(
	[PageBindingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Page Binding Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageBindings', @level2type=N'COLUMN',@level2name=N'PageBindingId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Page Title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageBindings', @level2type=N'COLUMN',@level2name=N'PageTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Menu Category' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageBindings', @level2type=N'COLUMN',@level2name=N'MenuCategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'List Page' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageBindings', @level2type=N'COLUMN',@level2name=N'ListMenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Add Page' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PageBindings', @level2type=N'COLUMN',@level2name=N'AddMenuId'
GO
SET IDENTITY_INSERT [dbo].[PageBindings] ON
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (1, N'Administrator', 1, 1, 2)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (2, N'Admin Role', 1, 3, 4)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (3, N'Role', 1, 5, 6)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (4, N'Role Permission', 1, 7, 7)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (5, N'Permission', 1, 8, 9)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (6, N'Permission Category', 1, 10, 11)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (7, N'Menu', 2, 12, 13)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (8, N'Menu Category', 1, 14, 15)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (9, N'Top Menu', 2, 16, 13)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (10, N'Top Menu Binding', 1, 18, 18)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (11, N'Page Binding', 2, 19, 19)
INSERT [dbo].[PageBindings] ([PageBindingId], [PageTitle], [MenuCategoryId], [ListMenuId], [AddMenuId]) VALUES (12, N'Mail Settings', 3, 20, 20)
SET IDENTITY_INSERT [dbo].[PageBindings] OFF
/****** Object:  Table [dbo].[SalesOrderItems]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrderItems](
	[SalesOrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[Price] [decimal](16, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_SalesOrderItem] PRIMARY KEY CLUSTERED 
(
	[SalesOrderId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 09/05/2015 23:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermissions](
	[RoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
 CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[PermissionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Role' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePermissions', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePermissions', @level2type=N'COLUMN',@level2name=N'PermissionId'
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 1)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 2)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 3)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 4)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 5)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 6)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 7)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 8)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 9)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 10)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 11)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 12)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 13)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (2, 12)
INSERT [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (3, 11)
/****** Object:  View [dbo].[View_Menu]    Script Date: 09/05/2015 23:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Menu]
AS
SELECT        dbo.Menus.MenuId, dbo.Menus.MenuName, dbo.Menus.MenuCategoryId, dbo.MenuCategories.MenuCategoryName, dbo.Menus.PageLink, dbo.Menus.ToolTip, dbo.Menus.Image, dbo.Menus.PermissionId, 
                         dbo.Menus.IsDisplay, dbo.Menus.Sequence
FROM            dbo.MenuCategories INNER JOIN
                         dbo.Menus ON dbo.MenuCategories.MenuCategoryId = dbo.Menus.MenuCategoryId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "MenuCategories"
            Begin Extent = 
               Top = 22
               Left = 473
               Bottom = 135
               Right = 673
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Menus"
            Begin Extent = 
               Top = 1
               Left = 129
               Bottom = 131
               Right = 307
            End
            DisplayFlags = 280
            TopColumn = 5
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Menu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Menu'
GO
/****** Object:  View [dbo].[View_AdminRole]    Script Date: 09/05/2015 23:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_AdminRole]
AS
SELECT     dbo.Administrators.AdminId, dbo.Administrators.AdminName, dbo.Roles.RoleId, dbo.Roles.RoleName
FROM         dbo.Administrators INNER JOIN
                      dbo.AdminRoles ON dbo.Administrators.AdminId = dbo.AdminRoles.AdminId INNER JOIN
                      dbo.Roles ON dbo.AdminRoles.RoleId = dbo.Roles.RoleId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Administrators"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 207
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AdminRoles"
            Begin Extent = 
               Top = 6
               Left = 245
               Bottom = 110
               Right = 405
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Roles"
            Begin Extent = 
               Top = 6
               Left = 443
               Bottom = 125
               Right = 603
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_AdminRole'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_AdminRole'
GO
/****** Object:  View [dbo].[View_SalesOrder]    Script Date: 09/05/2015 23:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_SalesOrder]
AS
SELECT     dbo.SalesOrders.SalesOrderId, dbo.SalesOrders.CustomerId, dbo.Customers.CustomerName, dbo.SalesOrders.SalesOrderStatusId, 
                      dbo.SalesOrderStatus.SalesOrderStatusName, dbo.SalesOrders.CreatedTime, dbo.SalesOrders.CreatedById, dbo.SalesOrders.CreatedByName, 
                      dbo.SalesOrders.UpdatedTime, dbo.SalesOrders.UpdatedById, dbo.SalesOrders.UpdatedByName, dbo.SalesOrders.Sequence
FROM         dbo.SalesOrders INNER JOIN
                      dbo.SalesOrderStatus ON dbo.SalesOrders.SalesOrderStatusId = dbo.SalesOrderStatus.SalesOrderStatusId INNER JOIN
                      dbo.Customers ON dbo.SalesOrders.CustomerId = dbo.Customers.CustomerId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[53] 4[22] 2[7] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "SalesOrders"
            Begin Extent = 
               Top = 79
               Left = 34
               Bottom = 198
               Right = 217
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "SalesOrderStatus"
            Begin Extent = 
               Top = 124
               Left = 385
               Bottom = 295
               Right = 600
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Customers"
            Begin Extent = 
               Top = 10
               Left = 319
               Bottom = 129
               Right = 488
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_SalesOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_SalesOrder'
GO
/****** Object:  View [dbo].[View_Product]    Script Date: 09/05/2015 23:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Product]
AS
SELECT     dbo.Products.ProductId, dbo.Products.ProductName, dbo.Products.ProductCategoryId, dbo.ProductCategories.ProductCategoryName, dbo.Products.Description, 
                      dbo.Products.Price, dbo.Products.ImageData, dbo.Products.ImageMimeType
FROM         dbo.ProductCategories INNER JOIN
                      dbo.Products ON dbo.ProductCategories.ProductCategoryId = dbo.Products.ProductCategoryId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ProductCategories"
            Begin Extent = 
               Top = 30
               Left = 309
               Bottom = 134
               Right = 507
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Products"
            Begin Extent = 
               Top = 11
               Left = 14
               Bottom = 195
               Right = 186
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Product'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Product'
GO
/****** Object:  View [dbo].[View_Permission]    Script Date: 09/05/2015 23:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Permission]
AS
SELECT        dbo.Permissions.PermissionId, dbo.Permissions.PermissionName, dbo.PermissionCategories.PermissionCategoryName
FROM            dbo.PermissionCategories INNER JOIN
                         dbo.Permissions ON dbo.PermissionCategories.PermissionCategoryId = dbo.Permissions.PermissionCategoryId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PermissionCategories"
            Begin Extent = 
               Top = 165
               Left = 405
               Bottom = 278
               Right = 632
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Permissions"
            Begin Extent = 
               Top = 136
               Left = 65
               Bottom = 266
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3240
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Permission'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Permission'
GO
/****** Object:  View [dbo].[View_PageBinding]    Script Date: 09/05/2015 23:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_PageBinding]
AS
SELECT        dbo.PageBindings.PageBindingId, dbo.PageBindings.PageTitle, dbo.PageBindings.MenuCategoryId, dbo.MenuCategories.MenuCategoryName, dbo.PageBindings.ListMenuId, 
                         ListMenu.MenuName AS ListMenuName, dbo.PageBindings.AddMenuId, AddMenu.MenuName AS AddMenuName
FROM            dbo.PageBindings INNER JOIN
                         dbo.MenuCategories ON dbo.PageBindings.MenuCategoryId = dbo.MenuCategories.MenuCategoryId INNER JOIN
                         dbo.Menus AS ListMenu ON dbo.PageBindings.ListMenuId = ListMenu.MenuId AND dbo.MenuCategories.MenuCategoryId = ListMenu.MenuCategoryId INNER JOIN
                         dbo.Menus AS AddMenu ON dbo.PageBindings.AddMenuId = AddMenu.MenuId AND dbo.MenuCategories.MenuCategoryId = AddMenu.MenuCategoryId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[25] 2[15] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PageBindings"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 216
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "MenuCategories"
            Begin Extent = 
               Top = 6
               Left = 254
               Bottom = 119
               Right = 454
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ListMenu"
            Begin Extent = 
               Top = 168
               Left = 503
               Bottom = 298
               Right = 681
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "AddMenu"
            Begin Extent = 
               Top = 141
               Left = 855
               Bottom = 271
               Right = 1033
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 2625
         Output = 1575
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 135' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_PageBinding'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'0
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_PageBinding'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_PageBinding'
GO
/****** Object:  ForeignKey [FK_TopMenuBindings_MenuCategory]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[TopMenuBindings]  WITH CHECK ADD  CONSTRAINT [FK_TopMenuBindings_MenuCategory] FOREIGN KEY([MenuCategoryId])
REFERENCES [dbo].[MenuCategories] ([MenuCategoryId])
GO
ALTER TABLE [dbo].[TopMenuBindings] CHECK CONSTRAINT [FK_TopMenuBindings_MenuCategory]
GO
/****** Object:  ForeignKey [FK_TopMenuBindings_TopMenu]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[TopMenuBindings]  WITH CHECK ADD  CONSTRAINT [FK_TopMenuBindings_TopMenu] FOREIGN KEY([TopMenuId])
REFERENCES [dbo].[TopMenus] ([TopMenuId])
GO
ALTER TABLE [dbo].[TopMenuBindings] CHECK CONSTRAINT [FK_TopMenuBindings_TopMenu]
GO
/****** Object:  ForeignKey [FK_Products_Category]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Category] FOREIGN KEY([ProductCategoryId])
REFERENCES [dbo].[ProductCategories] ([ProductCategoryId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Category]
GO
/****** Object:  ForeignKey [FK_Permissions_Category]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Category] FOREIGN KEY([PermissionCategoryId])
REFERENCES [dbo].[PermissionCategories] ([PermissionCategoryId])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_Category]
GO
/****** Object:  ForeignKey [FK_SalesOrders_Customer]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[SalesOrders]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrders_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[SalesOrders] CHECK CONSTRAINT [FK_SalesOrders_Customer]
GO
/****** Object:  ForeignKey [FK_SalesOrders_OrderStatus]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[SalesOrders]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrders_OrderStatus] FOREIGN KEY([SalesOrderStatusId])
REFERENCES [dbo].[SalesOrderStatus] ([SalesOrderStatusId])
GO
ALTER TABLE [dbo].[SalesOrders] CHECK CONSTRAINT [FK_SalesOrders_OrderStatus]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_AdminRoles_Admin]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[AdminRoles]  WITH CHECK ADD  CONSTRAINT [FK_AdminRoles_Admin] FOREIGN KEY([AdminId])
REFERENCES [dbo].[Administrators] ([AdminId])
GO
ALTER TABLE [dbo].[AdminRoles] CHECK CONSTRAINT [FK_AdminRoles_Admin]
GO
/****** Object:  ForeignKey [FK_AdminRoles_Role]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[AdminRoles]  WITH CHECK ADD  CONSTRAINT [FK_AdminRoles_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[AdminRoles] CHECK CONSTRAINT [FK_AdminRoles_Role]
GO
/****** Object:  ForeignKey [FK_Menus_Category]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[Menus]  WITH CHECK ADD  CONSTRAINT [FK_Menus_Category] FOREIGN KEY([MenuCategoryId])
REFERENCES [dbo].[MenuCategories] ([MenuCategoryId])
GO
ALTER TABLE [dbo].[Menus] CHECK CONSTRAINT [FK_Menus_Category]
GO
/****** Object:  ForeignKey [FK_PageBindings_AddMenu]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[PageBindings]  WITH CHECK ADD  CONSTRAINT [FK_PageBindings_AddMenu] FOREIGN KEY([AddMenuId])
REFERENCES [dbo].[Menus] ([MenuId])
GO
ALTER TABLE [dbo].[PageBindings] CHECK CONSTRAINT [FK_PageBindings_AddMenu]
GO
/****** Object:  ForeignKey [FK_PageBindings_ListMenu]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[PageBindings]  WITH CHECK ADD  CONSTRAINT [FK_PageBindings_ListMenu] FOREIGN KEY([ListMenuId])
REFERENCES [dbo].[Menus] ([MenuId])
GO
ALTER TABLE [dbo].[PageBindings] CHECK CONSTRAINT [FK_PageBindings_ListMenu]
GO
/****** Object:  ForeignKey [FK_PageBindings_MenuCategory]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[PageBindings]  WITH CHECK ADD  CONSTRAINT [FK_PageBindings_MenuCategory] FOREIGN KEY([MenuCategoryId])
REFERENCES [dbo].[MenuCategories] ([MenuCategoryId])
GO
ALTER TABLE [dbo].[PageBindings] CHECK CONSTRAINT [FK_PageBindings_MenuCategory]
GO
/****** Object:  ForeignKey [FK_SalesOrderItems_Product]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[SalesOrderItems]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderItems_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[SalesOrderItems] CHECK CONSTRAINT [FK_SalesOrderItems_Product]
GO
/****** Object:  ForeignKey [FK_SalesOrderItems_SalesOrder]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[SalesOrderItems]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderItems_SalesOrder] FOREIGN KEY([SalesOrderId])
REFERENCES [dbo].[SalesOrders] ([SalesOrderId])
GO
ALTER TABLE [dbo].[SalesOrderItems] CHECK CONSTRAINT [FK_SalesOrderItems_SalesOrder]
GO
/****** Object:  ForeignKey [FK_RolePermissions_Permission]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Permission] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permissions] ([PermissionId])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Permission]
GO
/****** Object:  ForeignKey [FK_RolePermissions_Role]    Script Date: 09/05/2015 23:10:47 ******/
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Role]
GO
