USE [master]
GO
/****** Object:  Database [MRO_RequestWizard]    Script Date: 10-06-2020 18:07:22 ******/
CREATE DATABASE [MRO_RequestWizard]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MRO_RequestWizard', FILENAME = N'D:\MRO\Data\MRO_RequestWizard.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MRO_RequestWizard_log', FILENAME = N'D:\MRO\Data\MRO_RequestWizard_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MRO_RequestWizard] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MRO_RequestWizard].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MRO_RequestWizard] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET ARITHABORT OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MRO_RequestWizard] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MRO_RequestWizard] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MRO_RequestWizard] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MRO_RequestWizard] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET RECOVERY FULL 
GO
ALTER DATABASE [MRO_RequestWizard] SET  MULTI_USER 
GO
ALTER DATABASE [MRO_RequestWizard] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MRO_RequestWizard] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MRO_RequestWizard] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MRO_RequestWizard] SET DELAYED_DURABILITY = DISABLED 
GO
USE [MRO_RequestWizard]
GO
/****** Object:  Table [dbo].[lnkFacilityFieldMaps]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lnkFacilityFieldMaps](
	[nFacilityFieldMapID] [int] IDENTITY(1,1) NOT NULL,
	[nFacilityID] [int] NOT NULL,
	[nFieldID] [int] NOT NULL,
	[nWizardID] [int] NOT NULL,
	[bShow] [bit] NOT NULL,
	[nCreatedAdminUserID] [int] NOT NULL,
	[dtCreated] [datetime] NOT NULL,
	[nUpdatedAdminUserID] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_lnkFacilityFieldMaps] PRIMARY KEY CLUSTERED 
(
	[nFacilityID] ASC,
	[nFieldID] ASC,
	[nWizardID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lnkFacilityPrimaryReasons]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lnkFacilityPrimaryReasons](
	[nFacilityPrimaryReasonID] [int] IDENTITY(1,1) NOT NULL,
	[nFacilityID] [int] NOT NULL,
	[nPrimaryReasonID] [int] NOT NULL,
	[sPrimaryReasonName] [varchar](50) NULL,
	[dtLastUpdate] [datetime] NULL,
 CONSTRAINT [PK_lnkFacilityPrimaryReasons_1] PRIMARY KEY CLUSTERED 
(
	[nFacilityID] ASC,
	[nPrimaryReasonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lnkFacilityRecordTypes]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lnkFacilityRecordTypes](
	[nFacilityRecordTypeID] [int] IDENTITY(1,1) NOT NULL,
	[nFacilityID] [int] NOT NULL,
	[nRecordTypeID] [int] NOT NULL,
	[sRecordTypeName] [varchar](50) NULL,
	[dtLastUpdate] [datetime] NULL,
 CONSTRAINT [PK_lnkFacilityRecordTypes] PRIMARY KEY CLUSTERED 
(
	[nFacilityID] ASC,
	[nRecordTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lnkFacilitySensitiveInfo]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lnkFacilitySensitiveInfo](
	[nFacilitySensitiveInfoID] [int] IDENTITY(1,1) NOT NULL,
	[nSensitiveInfoID] [int] NOT NULL,
	[nFacilityID] [int] NOT NULL,
	[sSensitiveInfoName] [varchar](50) NULL,
	[dtLastUpdate] [datetime] NULL,
 CONSTRAINT [PK_lnkROIFacilitySensitiveInfo] PRIMARY KEY CLUSTERED 
(
	[nFacilityID] ASC,
	[nSensitiveInfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lnkFacilityShipmentTypes]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lnkFacilityShipmentTypes](
	[nFacilityShipmentTypeID] [int] IDENTITY(1,1) NOT NULL,
	[nShipmentTypeID] [int] NOT NULL,
	[nFacilityID] [int] NOT NULL,
	[sShipmentTypeName] [varchar](50) NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_lnkFacilityShipmentTypes] PRIMARY KEY CLUSTERED 
(
	[nShipmentTypeID] ASC,
	[nFacilityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lstFields]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lstFields](
	[nFieldID] [int] IDENTITY(1,1) NOT NULL,
	[nWizardID] [int] NOT NULL,
	[sFieldName] [varchar](max) NULL,
	[sNormalizedFieldName] [varchar](max) NULL,
	[dtLastUpdate] [datetime] NULL,
 CONSTRAINT [PK_Field] PRIMARY KEY CLUSTERED 
(
	[nFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lstPrimaryReasons]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lstPrimaryReasons](
	[nPrimaryReasonID] [int] IDENTITY(1,1) NOT NULL,
	[sPrimaryReasonName] [varchar](50) NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_lst_PrimaryReason] PRIMARY KEY CLUSTERED 
(
	[nPrimaryReasonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lstRecordTypes]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lstRecordTypes](
	[nRecordTypeID] [int] IDENTITY(1,1) NOT NULL,
	[sRecordTypeName] [varchar](50) NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_lst_RecordType] PRIMARY KEY CLUSTERED 
(
	[nRecordTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lstSensitiveInfo]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lstSensitiveInfo](
	[nSensitiveInfoID] [int] IDENTITY(1,1) NOT NULL,
	[sSensitiveInfoName] [varchar](50) NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_lst_SensitiveInfor] PRIMARY KEY CLUSTERED 
(
	[nSensitiveInfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lstShipmentTypes]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lstShipmentTypes](
	[nShipmentTypeID] [int] IDENTITY(1,1) NOT NULL,
	[sShipmentTypeName] [varchar](50) NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_lstShipmentTypes] PRIMARY KEY CLUSTERED 
(
	[nShipmentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lstWizards]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lstWizards](
	[nWizardID] [int] IDENTITY(1,1) NOT NULL,
	[sWizardName] [varchar](max) NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_Wizard] PRIMARY KEY CLUSTERED 
(
	[nWizardID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblAdminUsers]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAdminUsers](
	[nAdminUserID] [int] IDENTITY(1,1) NOT NULL,
	[sUPN] [varchar](200) NULL,
	[sName] [varchar](50) NULL,
	[sEmail] [varchar](max) NULL,
	[dtCreated] [datetime] NOT NULL,
	[nUpdatedAdminUserID] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_AdminUser] PRIMARY KEY CLUSTERED 
(
	[nAdminUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblFacilities]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFacilities](
	[nFacilityID] [int] IDENTITY(1,1) NOT NULL,
	[nROIFacilityID] [int] NOT NULL,
	[sFacilityName] [varchar](max) NULL,
	[sDescription] [varchar](max) NULL,
	[sSMTPUsername] [varchar](max) NULL,
	[sSMTPPassword] [varchar](max) NULL,
	[sSMTPUrl] [varchar](max) NULL,
	[sFTPUsername] [varchar](max) NULL,
	[sFTPPassword] [varchar](max) NULL,
	[sFTPUrl] [varchar](max) NULL,
	[sOutboundEmail] [varchar](max) NULL,
	[bActiveStatus] [bit] NOT NULL,
	[sConfigShowFields] [nvarchar](max) NULL,
	[sConfigShowWizards] [nvarchar](max) NULL,
	[nCreatedAdminUserID] [int] NOT NULL,
	[dtCreated] [datetime] NOT NULL,
	[nUpdatedAdminUserID] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[nFacilityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblFacilityConnections]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFacilityConnections](
	[nFacilityConnectionID] [int] IDENTITY(1,1) NOT NULL,
	[nFacilityID] [int] NOT NULL,
	[sGUID] [varchar](max) NOT NULL,
	[sConnectionString] [varchar](200) NOT NULL,
	[nCreatingAdminUserID] [int] NOT NULL,
	[dtCreated] [datetime2](7) NOT NULL,
	[nUpdateAdminUserID] [int] NOT NULL,
	[dtLastUpdate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_tblFacilityConnections] PRIMARY KEY CLUSTERED 
(
	[nFacilityConnectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblFacilityLocations]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFacilityLocations](
	[nFacilityLocationID] [int] IDENTITY(1,1) NOT NULL,
	[nFacilityID] [int] NOT NULL,
	[nROILocationID] [int] NOT NULL,
	[sLocationCode] [varchar](50) NULL,
	[sLocationName] [varchar](30) NULL,
	[sLocationAddress] [varchar](200) NULL,
	[sPhoneNo] [nvarchar](10) NULL,
	[sFaxNo] [nvarchar](10) NULL,
	[sConfigLogoName] [nvarchar](50) NULL,
	[sConfigLogoData] [varchar](max) NULL,
	[sConfigBackgroundName] [nvarchar](50) NULL,
	[sConfigBackgroundData] [varchar](max) NULL,
	[sAuthTemplate] [varchar](max) NULL,
	[nCreatedAdminUserID] [int] NOT NULL,
	[dtCreated] [datetime] NOT NULL,
	[nUpdatedAdminUserID] [int] NOT NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[nFacilityID] ASC,
	[nROILocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblRequesters]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblRequesters](
	[nRequesterID] [int] IDENTITY(1,1) NOT NULL,
	[nFacilityID] [int] NULL,
	[nLocationID] [int] NULL,
	[bAreYouPatient] [bit] NULL,
	[sRelativeName] [varchar](30) NULL,
	[sRelationship] [varchar](20) NULL,
	[sEmailId] [varchar](30) NULL,
	[sPatientFirstName] [varchar](20) NULL,
	[sPatientLastName] [varchar](50) NULL,
	[sPatientMiddleInitial] [varchar](50) NULL,
	[bIsPatientDeceased] [bit] NULL,
	[sPatientZip] [char](10) NULL,
	[sPatientStreetAddress] [varchar](20) NULL,
	[dtPatientDOB] [date] NULL,
	[dtRecordTimeFrameStart] [date] NULL,
	[dtRecordTimeFrameEnd] [date] NULL,
	[sRecordType] [varchar](max) NULL,
	[sRecordTypeOther] [varchar](max) NULL,
	[sSensitiveData] [varchar](max) NULL,
	[dtAuthExpireDate] [date] NULL,
	[bRequestDeadline] [bit] NULL,
	[dtRequestDeadlineDate] [date] NULL,
	[sWhomReleaseRecord] [varchar](30) NULL,
	[sShipmentType] [varchar](30) NULL,
	[sWhomReleaseRecordName] [varchar](30) NULL,
	[sWhomReleaseRecordZip] [char](5) NULL,
	[sWhomReleaseRecordStreetAdd] [varchar](30) NULL,
	[sAdditionalData] [varchar](max) NULL,
	[sPhoneNo] [nvarchar](10) NULL,
	[sPhotoIdentity] [varchar](max) NULL,
	[sSignature] [varchar](max) NULL,
	[sPDF] [varchar](max) NULL,
	[bToolEasyToUse] [bit] NULL,
	[sToolTextFeedback] [varchar](max) NULL,
	[dtLastUpdate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_tblRequestors] PRIMARY KEY CLUSTERED 
(
	[nRequesterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTempRequesters]    Script Date: 10-06-2020 18:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTempRequesters](
	[nRequesterID] [int] IDENTITY(1,1) NOT NULL,
	[nFacilityID] [int] NULL,
	[bAreYouPatient] [bit] NULL,
	[sRelativeName] [varchar](30) NULL,
	[sRelationship] [varchar](20) NULL,
	[sEmailId] [varchar](30) NULL,
	[sPatientFirstName] [varchar](20) NULL,
	[sPatientLastName] [varchar](50) NULL,
	[sPatientMiddleInitial] [varchar](50) NULL,
	[bIsPatientDeceased] [bit] NULL,
	[sPatientZip] [char](10) NULL,
	[sPatientStreetAddress] [varchar](20) NULL,
	[dtPatientDOB] [date] NULL,
	[dtRecordTimeFrameStart] [date] NULL,
	[dtRecordTimeFrameEnd] [date] NULL,
	[sRecordType] [varchar](max) NULL,
	[sRecordTypeOther] [varchar](max) NULL,
	[sSensitiveData] [varchar](max) NULL,
	[dtAuthExpireDate] [date] NULL,
	[bRequestDeadline] [bit] NULL,
	[dtRequestDeadlineDate] [date] NULL,
	[sWhomReleaseRecord] [varchar](30) NULL,
	[sShipmentType] [varchar](30) NULL,
	[sWhomReleaseRecordName] [varchar](30) NULL,
	[sWhomReleaseRecordZip] [char](5) NULL,
	[sWhomReleaseRecordStreetAdd] [varchar](30) NULL,
	[sAdditionalData] [varchar](max) NULL,
	[sPhoneNo] [char](10) NULL,
	[sPhotoIdentity] [varchar](max) NULL,
	[sSignature] [varchar](max) NULL,
	[sPDF] [varchar](max) NULL,
	[bToolEasyToUse] [bit] NULL,
	[sToolTextFeedback] [varchar](max) NULL,
	[dtLastUpdate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_tblTempRequestors] PRIMARY KEY CLUSTERED 
(
	[nRequesterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[lnkFacilityFieldMaps] ON 
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (1, 1, 1, 2, 1, 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime), 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime))
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (2, 1, 2, 3, 1, 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime), 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime))
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (3, 1, 3, 4, 1, 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime), 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime))
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (4, 1, 4, 4, 1, 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime), 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime))
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (5, 1, 5, 5, 1, 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime), 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime))
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (6, 1, 6, 5, 1, 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime), 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime))
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (7, 1, 7, 5, 1, 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime), 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime))
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (8, 1, 8, 5, 1, 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime), 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime))
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (9, 1, 9, 6, 1, 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime), 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime))
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (10, 1, 10, 7, 1, 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime), 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime))
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (11, 1, 11, 8, 1, 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime), 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime))
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (12, 1, 12, 9, 1, 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime), 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime))
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (13, 1, 13, 10, 1, 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime), 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime))
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (14, 1, 14, 11, 1, 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime), 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime))
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (15, 1, 15, 12, 1, 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime), 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime))
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (16, 1, 16, 13, 1, 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime), 1, CAST(N'2020-06-10T17:57:05.007' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lnkFacilityFieldMaps] OFF
GO
SET IDENTITY_INSERT [dbo].[lnkFacilityPrimaryReasons] ON 
GO
INSERT [dbo].[lnkFacilityPrimaryReasons] ([nFacilityPrimaryReasonID], [nFacilityID], [nPrimaryReasonID], [sPrimaryReasonName], [dtLastUpdate]) VALUES (1, 1, 1, N'Continued Care', CAST(N'2020-06-10T17:57:05.087' AS DateTime))
GO
INSERT [dbo].[lnkFacilityPrimaryReasons] ([nFacilityPrimaryReasonID], [nFacilityID], [nPrimaryReasonID], [sPrimaryReasonName], [dtLastUpdate]) VALUES (2, 1, 2, N'Patient Request', CAST(N'2020-06-10T17:57:05.087' AS DateTime))
GO
INSERT [dbo].[lnkFacilityPrimaryReasons] ([nFacilityPrimaryReasonID], [nFacilityID], [nPrimaryReasonID], [sPrimaryReasonName], [dtLastUpdate]) VALUES (3, 1, 3, N'Insurance', CAST(N'2020-06-10T17:57:05.087' AS DateTime))
GO
INSERT [dbo].[lnkFacilityPrimaryReasons] ([nFacilityPrimaryReasonID], [nFacilityID], [nPrimaryReasonID], [sPrimaryReasonName], [dtLastUpdate]) VALUES (4, 1, 4, N'Social Security Benefit', CAST(N'2020-06-10T17:57:05.087' AS DateTime))
GO
INSERT [dbo].[lnkFacilityPrimaryReasons] ([nFacilityPrimaryReasonID], [nFacilityID], [nPrimaryReasonID], [sPrimaryReasonName], [dtLastUpdate]) VALUES (5, 1, 5, N'Other Reason', CAST(N'2020-06-10T17:57:05.087' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lnkFacilityPrimaryReasons] OFF
GO
SET IDENTITY_INSERT [dbo].[lnkFacilityRecordTypes] ON 
GO
INSERT [dbo].[lnkFacilityRecordTypes] ([nFacilityRecordTypeID], [nFacilityID], [nRecordTypeID], [sRecordTypeName], [dtLastUpdate]) VALUES (1, 1, 1, N'Abstract', CAST(N'2020-06-10T17:57:05.093' AS DateTime))
GO
INSERT [dbo].[lnkFacilityRecordTypes] ([nFacilityRecordTypeID], [nFacilityID], [nRecordTypeID], [sRecordTypeName], [dtLastUpdate]) VALUES (2, 1, 2, N'Discharge Summary', CAST(N'2020-06-10T17:57:05.093' AS DateTime))
GO
INSERT [dbo].[lnkFacilityRecordTypes] ([nFacilityRecordTypeID], [nFacilityID], [nRecordTypeID], [sRecordTypeName], [dtLastUpdate]) VALUES (3, 1, 3, N'Operative Reports', CAST(N'2020-06-10T17:57:05.093' AS DateTime))
GO
INSERT [dbo].[lnkFacilityRecordTypes] ([nFacilityRecordTypeID], [nFacilityID], [nRecordTypeID], [sRecordTypeName], [dtLastUpdate]) VALUES (4, 1, 4, N'History And Physical', CAST(N'2020-06-10T17:57:05.093' AS DateTime))
GO
INSERT [dbo].[lnkFacilityRecordTypes] ([nFacilityRecordTypeID], [nFacilityID], [nRecordTypeID], [sRecordTypeName], [dtLastUpdate]) VALUES (5, 1, 5, N'Laboratory Reports', CAST(N'2020-06-10T17:57:05.093' AS DateTime))
GO
INSERT [dbo].[lnkFacilityRecordTypes] ([nFacilityRecordTypeID], [nFacilityID], [nRecordTypeID], [sRecordTypeName], [dtLastUpdate]) VALUES (6, 1, 6, N'Radiology Report', CAST(N'2020-06-10T17:57:05.093' AS DateTime))
GO
INSERT [dbo].[lnkFacilityRecordTypes] ([nFacilityRecordTypeID], [nFacilityID], [nRecordTypeID], [sRecordTypeName], [dtLastUpdate]) VALUES (7, 1, 7, N'Other', CAST(N'2020-06-10T17:57:05.093' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lnkFacilityRecordTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[lnkFacilitySensitiveInfo] ON 
GO
INSERT [dbo].[lnkFacilitySensitiveInfo] ([nFacilitySensitiveInfoID], [nSensitiveInfoID], [nFacilityID], [sSensitiveInfoName], [dtLastUpdate]) VALUES (1, 1, 1, N'HIV Test Results', CAST(N'2020-06-10T17:57:05.100' AS DateTime))
GO
INSERT [dbo].[lnkFacilitySensitiveInfo] ([nFacilitySensitiveInfoID], [nSensitiveInfoID], [nFacilityID], [sSensitiveInfoName], [dtLastUpdate]) VALUES (2, 2, 1, N'Behavioural/Mental Health Records', CAST(N'2020-06-10T17:57:05.100' AS DateTime))
GO
INSERT [dbo].[lnkFacilitySensitiveInfo] ([nFacilitySensitiveInfoID], [nSensitiveInfoID], [nFacilityID], [sSensitiveInfoName], [dtLastUpdate]) VALUES (3, 3, 1, N'Substance Abuse Information', CAST(N'2020-06-10T17:57:05.100' AS DateTime))
GO
INSERT [dbo].[lnkFacilitySensitiveInfo] ([nFacilitySensitiveInfoID], [nSensitiveInfoID], [nFacilityID], [sSensitiveInfoName], [dtLastUpdate]) VALUES (4, 4, 1, N'Sexually Transmitted Dieases', CAST(N'2020-06-10T17:57:05.100' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lnkFacilitySensitiveInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[lnkFacilityShipmentTypes] ON 
GO
INSERT [dbo].[lnkFacilityShipmentTypes] ([nFacilityShipmentTypeID], [nShipmentTypeID], [nFacilityID], [sShipmentTypeName], [dtLastUpdate]) VALUES (1, 1, 1, N'Patient Portal', CAST(N'2020-06-10T17:57:05.107' AS DateTime))
GO
INSERT [dbo].[lnkFacilityShipmentTypes] ([nFacilityShipmentTypeID], [nShipmentTypeID], [nFacilityID], [sShipmentTypeName], [dtLastUpdate]) VALUES (2, 2, 1, N'Secure Email', CAST(N'2020-06-10T17:57:05.107' AS DateTime))
GO
INSERT [dbo].[lnkFacilityShipmentTypes] ([nFacilityShipmentTypeID], [nShipmentTypeID], [nFacilityID], [sShipmentTypeName], [dtLastUpdate]) VALUES (3, 3, 1, N'Email', CAST(N'2020-06-10T17:57:05.107' AS DateTime))
GO
INSERT [dbo].[lnkFacilityShipmentTypes] ([nFacilityShipmentTypeID], [nShipmentTypeID], [nFacilityID], [sShipmentTypeName], [dtLastUpdate]) VALUES (4, 4, 1, N'In-Person', CAST(N'2020-06-10T17:57:05.107' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lnkFacilityShipmentTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[lstFields] ON 
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (1, 2, N'Selected Location', N'SelectedLocation', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (2, 3, N'Are You Patient ?', N'AreYouPatient?', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (3, 4, N'Patient Email Id', N'PEmailId', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (4, 4, N'Confirm Report', N'ConfirmReport', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (5, 5, N'Patient First Name', N'PFName', CAST(N'2020-06-08T21:57:10.037' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (6, 5, N'Patient Middle Initial', N'PMInitial', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (7, 5, N'Patient Last Name', N'PLName', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (8, 5, N'Is Patient Deceased', N'IsPatientDeceased', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (9, 6, N'Zip Code', N'ZipCode', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (10, 7, N'Street Area', N'StreetArea', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (11, 8, N'Patient Birth Date', N'PBirthDate', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (12, 9, N'Primary Reason', N'PrimaryReason', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (13, 10, N'TimeFrame Date Range', N'TFDateRange', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (14, 11, N'Record type', N'RecordType', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (15, 12, N'Sensitive Info', N'SensitiveInfo', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (16, 13, N'Authorization Expire', N'Authexpire', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstFields] OFF
GO
SET IDENTITY_INSERT [dbo].[lstPrimaryReasons] ON 
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [dtLastUpdate]) VALUES (1, N'Continued Care', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [dtLastUpdate]) VALUES (2, N'Patient Request', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [dtLastUpdate]) VALUES (3, N'Insurance', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [dtLastUpdate]) VALUES (4, N'Social Security Benefit', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [dtLastUpdate]) VALUES (5, N'Other Reason', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstPrimaryReasons] OFF
GO
SET IDENTITY_INSERT [dbo].[lstRecordTypes] ON 
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [dtLastUpdate]) VALUES (1, N'Abstract', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [dtLastUpdate]) VALUES (2, N'Discharge Summary', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [dtLastUpdate]) VALUES (3, N'Operative Reports', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [dtLastUpdate]) VALUES (4, N'History And Physical', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [dtLastUpdate]) VALUES (5, N'Laboratory Reports', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [dtLastUpdate]) VALUES (6, N'Radiology Report', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [dtLastUpdate]) VALUES (7, N'Other', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstRecordTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[lstSensitiveInfo] ON 
GO
INSERT [dbo].[lstSensitiveInfo] ([nSensitiveInfoID], [sSensitiveInfoName], [dtLastUpdate]) VALUES (1, N'HIV Test Results', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstSensitiveInfo] ([nSensitiveInfoID], [sSensitiveInfoName], [dtLastUpdate]) VALUES (2, N'Behavioural/Mental Health Records', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstSensitiveInfo] ([nSensitiveInfoID], [sSensitiveInfoName], [dtLastUpdate]) VALUES (3, N'Substance Abuse Information', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstSensitiveInfo] ([nSensitiveInfoID], [sSensitiveInfoName], [dtLastUpdate]) VALUES (4, N'Sexually Transmitted Dieases', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstSensitiveInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[lstShipmentTypes] ON 
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [dtLastUpdate]) VALUES (1, N'Patient Portal', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [dtLastUpdate]) VALUES (2, N'Secure Email', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [dtLastUpdate]) VALUES (3, N'Email', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [dtLastUpdate]) VALUES (4, N'In-Person', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstShipmentTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[lstWizards] ON 
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (1, N'Wizrad-1', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (2, N'Wizard-2', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (3, N'Wizard-3', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (4, N'Wizard-4', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (5, N'Wizard-5', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (6, N'Wizard-6', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (7, N'Wizard-7', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (8, N'Wizard-8', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (9, N'Wizard-9', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (10, N'Wizard-10', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (11, N'Wizard-11', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (12, N'Wizard-12', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (13, N'Wizard-13', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (14, N'Wizard-14', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (15, N'Wizard-15', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstWizards] OFF
GO
SET IDENTITY_INSERT [dbo].[tblAdminUsers] ON 
GO
INSERT [dbo].[tblAdminUsers] ([nAdminUserID], [sUPN], [sName], [sEmail], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (1, N'Test UPN', N'Test Name', N'test@test.com', CAST(N'2020-10-06T00:00:00.000' AS DateTime), 1, CAST(N'2020-10-06T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tblAdminUsers] OFF
GO
SET IDENTITY_INSERT [dbo].[tblFacilities] ON 
GO
INSERT [dbo].[tblFacilities] ([nFacilityID], [nROIFacilityID], [sFacilityName], [sDescription], [sSMTPUsername], [sSMTPPassword], [sSMTPUrl], [sFTPUsername], [sFTPPassword], [sFTPUrl], [sOutboundEmail], [bActiveStatus], [sConfigShowFields], [sConfigShowWizards], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (1, 0, N'Cleveland Clinic', N'Info about Cleveland', N'user', N'user@123', N'smtp.cleveland.com', N'user', N'user@123', N'ftp.cleveland.com', N'noreply@cleveland.com', 1, N'T Data', N'T Data', 1, CAST(N'2020-06-10T17:56:59.713' AS DateTime), 1, CAST(N'2020-06-10T17:56:59.713' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tblFacilities] OFF
GO
SET IDENTITY_INSERT [dbo].[tblFacilityLocations] ON 
GO
INSERT [dbo].[tblFacilityLocations] ([nFacilityLocationID], [nFacilityID], [nROILocationID], [sLocationCode], [sLocationName], [sLocationAddress], [sPhoneNo], [sFaxNo], [sConfigLogoName], [sConfigLogoData], [sConfigBackgroundName], [sConfigBackgroundData], [sAuthTemplate], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (1, 1, 1, N'Location Code', N'Cleveland Location', N'Location Address', N'9876543210', N'0123456789', N'nature.jpg', N'data:image/png;base64,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', N'', N'data:image/jpeg;base64,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', NULL, 1, CAST(N'2020-06-10T18:06:03.563' AS DateTime), 1, CAST(N'2020-06-10T18:06:03.567' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tblFacilityLocations] OFF
GO
ALTER TABLE [dbo].[lnkFacilityFieldMaps] ADD  CONSTRAINT [DF_lnkROIFacilityFieldMaps_nWizardID]  DEFAULT ((0)) FOR [nWizardID]
GO
ALTER TABLE [dbo].[lnkFacilityFieldMaps] ADD  CONSTRAINT [DF_lnkROIFacilityFieldMaps_nCreatedAdminUserID]  DEFAULT ((0)) FOR [nCreatedAdminUserID]
GO
ALTER TABLE [dbo].[lnkFacilityFieldMaps] ADD  CONSTRAINT [DF_lnkROIFacilityFieldMaps_dtCreated]  DEFAULT (getdate()) FOR [dtCreated]
GO
ALTER TABLE [dbo].[lnkFacilityFieldMaps] ADD  CONSTRAINT [DF_lnkROIFacilityFieldMaps_nUpdatedAdminUserID]  DEFAULT ((0)) FOR [nUpdatedAdminUserID]
GO
ALTER TABLE [dbo].[lnkFacilityFieldMaps] ADD  CONSTRAINT [DF_lnkROIFacilityFieldMaps_dtLastUpdate]  DEFAULT (getdate()) FOR [dtLastUpdate]
GO
ALTER TABLE [dbo].[lnkFacilityPrimaryReasons] ADD  CONSTRAINT [DF_lnkROIFacilityPrimaryReasons_dtLastUpdate]  DEFAULT (getdate()) FOR [dtLastUpdate]
GO
ALTER TABLE [dbo].[lnkFacilityRecordTypes] ADD  CONSTRAINT [DF_lnkROIFacilityRecordTypes_dtLastUpdate]  DEFAULT (getdate()) FOR [dtLastUpdate]
GO
ALTER TABLE [dbo].[lnkFacilitySensitiveInfo] ADD  CONSTRAINT [DF_lnkROIFacilitySensitiveInfo_dtLastUpdate]  DEFAULT (getdate()) FOR [dtLastUpdate]
GO
ALTER TABLE [dbo].[lnkFacilityShipmentTypes] ADD  CONSTRAINT [DF_lnkFacilityShipmentTypes_dtLastUpdate]  DEFAULT (getdate()) FOR [dtLastUpdate]
GO
ALTER TABLE [dbo].[lstFields] ADD  CONSTRAINT [DF_lstFields_dtLastUpdate]  DEFAULT (getdate()) FOR [dtLastUpdate]
GO
ALTER TABLE [dbo].[lstPrimaryReasons] ADD  CONSTRAINT [DF_lstPrimaryReasons_dtLastUpdate]  DEFAULT (getdate()) FOR [dtLastUpdate]
GO
ALTER TABLE [dbo].[lstRecordTypes] ADD  CONSTRAINT [DF_lstRecordTypes_dtLastUpdate]  DEFAULT (getdate()) FOR [dtLastUpdate]
GO
ALTER TABLE [dbo].[lstSensitiveInfo] ADD  CONSTRAINT [DF_lstSensitiveInfor_dtLastUpdate]  DEFAULT (getdate()) FOR [dtLastUpdate]
GO
ALTER TABLE [dbo].[lstShipmentTypes] ADD  CONSTRAINT [DF_lstShipmentTypes_dtLastUpdate]  DEFAULT (getdate()) FOR [dtLastUpdate]
GO
ALTER TABLE [dbo].[lstWizards] ADD  CONSTRAINT [DF_lstWizards_dtLastUpdate]  DEFAULT (getdate()) FOR [dtLastUpdate]
GO
ALTER TABLE [dbo].[tblAdminUsers] ADD  CONSTRAINT [DF_tblAdminUsers_dtCreated]  DEFAULT (getdate()) FOR [dtCreated]
GO
ALTER TABLE [dbo].[tblAdminUsers] ADD  CONSTRAINT [DF_tblAdminUsers_nUpdatedAdminUserID]  DEFAULT ((0)) FOR [nUpdatedAdminUserID]
GO
ALTER TABLE [dbo].[tblAdminUsers] ADD  CONSTRAINT [DF_tblAdminUsers_dtLastUpdate]  DEFAULT (getdate()) FOR [dtLastUpdate]
GO
ALTER TABLE [dbo].[tblFacilities] ADD  CONSTRAINT [DF_tblROIFacilities_nCreatedAdminUserID]  DEFAULT ((0)) FOR [nCreatedAdminUserID]
GO
ALTER TABLE [dbo].[tblFacilities] ADD  CONSTRAINT [DF_tblROIFacilities_dtCreated]  DEFAULT (getdate()) FOR [dtCreated]
GO
ALTER TABLE [dbo].[tblFacilities] ADD  CONSTRAINT [DF_tblROIFacilities_nUpdatedAdminUserID]  DEFAULT ((0)) FOR [nUpdatedAdminUserID]
GO
ALTER TABLE [dbo].[tblFacilities] ADD  CONSTRAINT [DF_tblROIFacilities_dtLastUpdate]  DEFAULT (getdate()) FOR [dtLastUpdate]
GO
ALTER TABLE [dbo].[tblFacilityLocations] ADD  CONSTRAINT [DF_tblROILocations_nCreatedAdminUserID]  DEFAULT ((0)) FOR [nCreatedAdminUserID]
GO
ALTER TABLE [dbo].[tblFacilityLocations] ADD  CONSTRAINT [DF_tblROILocations_dtCreated]  DEFAULT (getdate()) FOR [dtCreated]
GO
ALTER TABLE [dbo].[tblFacilityLocations] ADD  CONSTRAINT [DF_tblROILocations_nUpdatedAdminUserID]  DEFAULT ((0)) FOR [nUpdatedAdminUserID]
GO
ALTER TABLE [dbo].[tblFacilityLocations] ADD  CONSTRAINT [DF_tblROILocations_dtLastUpdate]  DEFAULT (getdate()) FOR [dtLastUpdate]
GO
ALTER TABLE [dbo].[lnkFacilityFieldMaps]  WITH CHECK ADD  CONSTRAINT [FK_lnkFacilityFieldMaps_nFacilityID] FOREIGN KEY([nFacilityID])
REFERENCES [dbo].[tblFacilities] ([nFacilityID])
GO
ALTER TABLE [dbo].[lnkFacilityFieldMaps] CHECK CONSTRAINT [FK_lnkFacilityFieldMaps_nFacilityID]
GO
ALTER TABLE [dbo].[lnkFacilityFieldMaps]  WITH CHECK ADD  CONSTRAINT [FK_lnkFacilityFieldMaps_nFieldID] FOREIGN KEY([nFieldID])
REFERENCES [dbo].[lstFields] ([nFieldID])
GO
ALTER TABLE [dbo].[lnkFacilityFieldMaps] CHECK CONSTRAINT [FK_lnkFacilityFieldMaps_nFieldID]
GO
ALTER TABLE [dbo].[lnkFacilityPrimaryReasons]  WITH CHECK ADD  CONSTRAINT [FK_lnkFacilityPrimaryReasons_nFacilityID] FOREIGN KEY([nFacilityID])
REFERENCES [dbo].[tblFacilities] ([nFacilityID])
GO
ALTER TABLE [dbo].[lnkFacilityPrimaryReasons] CHECK CONSTRAINT [FK_lnkFacilityPrimaryReasons_nFacilityID]
GO
ALTER TABLE [dbo].[lnkFacilityPrimaryReasons]  WITH CHECK ADD  CONSTRAINT [FK_lnkFacilityPrimaryReasons_nPrimaryReasonID] FOREIGN KEY([nPrimaryReasonID])
REFERENCES [dbo].[lstPrimaryReasons] ([nPrimaryReasonID])
GO
ALTER TABLE [dbo].[lnkFacilityPrimaryReasons] CHECK CONSTRAINT [FK_lnkFacilityPrimaryReasons_nPrimaryReasonID]
GO
ALTER TABLE [dbo].[lnkFacilityRecordTypes]  WITH CHECK ADD  CONSTRAINT [FK_lnkFacilityRecordTypes_nFacilityID] FOREIGN KEY([nFacilityID])
REFERENCES [dbo].[tblFacilities] ([nFacilityID])
GO
ALTER TABLE [dbo].[lnkFacilityRecordTypes] CHECK CONSTRAINT [FK_lnkFacilityRecordTypes_nFacilityID]
GO
ALTER TABLE [dbo].[lnkFacilityRecordTypes]  WITH CHECK ADD  CONSTRAINT [FK_lnkFacilityRecordTypes_nRecordTypeID] FOREIGN KEY([nRecordTypeID])
REFERENCES [dbo].[lstRecordTypes] ([nRecordTypeID])
GO
ALTER TABLE [dbo].[lnkFacilityRecordTypes] CHECK CONSTRAINT [FK_lnkFacilityRecordTypes_nRecordTypeID]
GO
ALTER TABLE [dbo].[lnkFacilitySensitiveInfo]  WITH CHECK ADD  CONSTRAINT [FK_lnkFacilitySensitiveInfo_nFacilityID] FOREIGN KEY([nFacilityID])
REFERENCES [dbo].[tblFacilities] ([nFacilityID])
GO
ALTER TABLE [dbo].[lnkFacilitySensitiveInfo] CHECK CONSTRAINT [FK_lnkFacilitySensitiveInfo_nFacilityID]
GO
ALTER TABLE [dbo].[lnkFacilitySensitiveInfo]  WITH CHECK ADD  CONSTRAINT [FK_tbl_lnkFacilitySensitiveInfo_nSensitiveInfoID] FOREIGN KEY([nSensitiveInfoID])
REFERENCES [dbo].[lstSensitiveInfo] ([nSensitiveInfoID])
GO
ALTER TABLE [dbo].[lnkFacilitySensitiveInfo] CHECK CONSTRAINT [FK_tbl_lnkFacilitySensitiveInfo_nSensitiveInfoID]
GO
ALTER TABLE [dbo].[lnkFacilityShipmentTypes]  WITH CHECK ADD  CONSTRAINT [FK_lnkFacilityShipmentTypes_nFacilityID] FOREIGN KEY([nFacilityID])
REFERENCES [dbo].[tblFacilities] ([nFacilityID])
GO
ALTER TABLE [dbo].[lnkFacilityShipmentTypes] CHECK CONSTRAINT [FK_lnkFacilityShipmentTypes_nFacilityID]
GO
ALTER TABLE [dbo].[lnkFacilityShipmentTypes]  WITH CHECK ADD  CONSTRAINT [FK_lnkFacilityShipmentTypes_nShipmentTypeID] FOREIGN KEY([nShipmentTypeID])
REFERENCES [dbo].[lstShipmentTypes] ([nShipmentTypeID])
GO
ALTER TABLE [dbo].[lnkFacilityShipmentTypes] CHECK CONSTRAINT [FK_lnkFacilityShipmentTypes_nShipmentTypeID]
GO
ALTER TABLE [dbo].[lstFields]  WITH CHECK ADD  CONSTRAINT [FK_Field_Wizard] FOREIGN KEY([nWizardID])
REFERENCES [dbo].[lstWizards] ([nWizardID])
GO
ALTER TABLE [dbo].[lstFields] CHECK CONSTRAINT [FK_Field_Wizard]
GO
ALTER TABLE [dbo].[tblFacilityConnections]  WITH CHECK ADD  CONSTRAINT [FK_tblFacilityConnections_nFacilityID] FOREIGN KEY([nFacilityID])
REFERENCES [dbo].[tblFacilities] ([nFacilityID])
GO
ALTER TABLE [dbo].[tblFacilityConnections] CHECK CONSTRAINT [FK_tblFacilityConnections_nFacilityID]
GO
ALTER TABLE [dbo].[tblFacilityLocations]  WITH CHECK ADD  CONSTRAINT [FK_tblFacilityLocations_nFacilityID] FOREIGN KEY([nFacilityID])
REFERENCES [dbo].[tblFacilities] ([nFacilityID])
GO
ALTER TABLE [dbo].[tblFacilityLocations] CHECK CONSTRAINT [FK_tblFacilityLocations_nFacilityID]
GO
ALTER TABLE [dbo].[tblRequesters]  WITH CHECK ADD  CONSTRAINT [FK_tblRequesters_nFacilityID] FOREIGN KEY([nFacilityID])
REFERENCES [dbo].[tblFacilities] ([nFacilityID])
GO
ALTER TABLE [dbo].[tblRequesters] CHECK CONSTRAINT [FK_tblRequesters_nFacilityID]
GO
ALTER TABLE [dbo].[tblRequesters]  WITH CHECK ADD  CONSTRAINT [FK_tblRequesters_nFacilityLocationID] FOREIGN KEY([nFacilityID], [nLocationID])
REFERENCES [dbo].[tblFacilityLocations] ([nFacilityID], [nROILocationID])
GO
ALTER TABLE [dbo].[tblRequesters] CHECK CONSTRAINT [FK_tblRequesters_nFacilityLocationID]
GO
USE [master]
GO
ALTER DATABASE [MRO_RequestWizard] SET  READ_WRITE 
GO
