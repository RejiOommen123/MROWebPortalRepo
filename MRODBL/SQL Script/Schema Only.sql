USE [master]
GO
/****** Object:  Database [MRO_RequestWizard]    Script Date: 22-06-2020 15:44:42 ******/
CREATE DATABASE [MRO_RequestWizard]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MRO_RequestWizard', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\MRO_RequestWizard.mdf' , SIZE = 6336KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MRO_RequestWizard_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\MRO_RequestWizard_log.ldf' , SIZE = 2368KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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
ALTER DATABASE [MRO_RequestWizard] SET AUTO_CLOSE ON 
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
ALTER DATABASE [MRO_RequestWizard] SET  ENABLE_BROKER 
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
ALTER DATABASE [MRO_RequestWizard] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MRO_RequestWizard] SET  MULTI_USER 
GO
ALTER DATABASE [MRO_RequestWizard] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MRO_RequestWizard] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MRO_RequestWizard] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MRO_RequestWizard] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [MRO_RequestWizard] SET DELAYED_DURABILITY = DISABLED 
GO
USE [MRO_RequestWizard]
GO
/****** Object:  Table [dbo].[lnkFacilityFieldMaps]    Script Date: 22-06-2020 15:44:42 ******/
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
	[sTableName] [nvarchar](50) NULL,
 CONSTRAINT [PK_lnkFacilityFieldMaps_1] PRIMARY KEY CLUSTERED 
(
	[nFacilityFieldMapID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lnkFacilityPrimaryReasons]    Script Date: 22-06-2020 15:44:42 ******/
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
/****** Object:  Table [dbo].[lnkFacilityRecordTypes]    Script Date: 22-06-2020 15:44:42 ******/
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
/****** Object:  Table [dbo].[lnkFacilitySensitiveInfo]    Script Date: 22-06-2020 15:44:42 ******/
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
/****** Object:  Table [dbo].[lnkFacilityShipmentTypes]    Script Date: 22-06-2020 15:44:42 ******/
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
/****** Object:  Table [dbo].[lstFields]    Script Date: 22-06-2020 15:44:42 ******/
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
	[sFieldToolTip] [nvarchar](500) NULL,
 CONSTRAINT [PK_Field] PRIMARY KEY CLUSTERED 
(
	[nFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lstPrimaryReasons]    Script Date: 22-06-2020 15:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lstPrimaryReasons](
	[nPrimaryReasonID] [int] IDENTITY(1,1) NOT NULL,
	[sPrimaryReasonName] [nvarchar](100) NULL,
	[nWizardID] [int] NULL,
	[sNormalizedPrimaryReasonName] [nvarchar](100) NULL,
	[sFieldToolTip] [nvarchar](500) NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_lst_PrimaryReason] PRIMARY KEY CLUSTERED 
(
	[nPrimaryReasonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lstRecordTypes]    Script Date: 22-06-2020 15:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lstRecordTypes](
	[nRecordTypeID] [int] IDENTITY(1,1) NOT NULL,
	[sRecordTypeName] [varchar](50) NULL,
	[nWizardID] [int] NULL,
	[sNormalizedRecordTypeName] [nvarchar](50) NULL,
	[sFieldToolTip] [nvarchar](500) NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_lst_RecordType] PRIMARY KEY CLUSTERED 
(
	[nRecordTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lstSensitiveInfo]    Script Date: 22-06-2020 15:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lstSensitiveInfo](
	[nSensitiveInfoID] [int] IDENTITY(1,1) NOT NULL,
	[sSensitiveInfoName] [varchar](50) NULL,
	[nWizardID] [int] NULL,
	[sNormalizedSensitiveInfoName] [nvarchar](50) NULL,
	[sFieldToolTip] [nvarchar](500) NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_lst_SensitiveInfor] PRIMARY KEY CLUSTERED 
(
	[nSensitiveInfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lstShipmentTypes]    Script Date: 22-06-2020 15:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lstShipmentTypes](
	[nShipmentTypeID] [int] IDENTITY(1,1) NOT NULL,
	[sShipmentTypeName] [varchar](50) NULL,
	[nWizardID] [int] NULL,
	[sNormalizedShipmentTypeName] [nvarchar](50) NULL,
	[sFieldToolTip] [nvarchar](500) NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_lstShipmentTypes] PRIMARY KEY CLUSTERED 
(
	[nShipmentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lstValidateAuthorizationDoc]    Script Date: 22-06-2020 15:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lstValidateAuthorizationDoc](
	[sFieldname] [nvarchar](max) NOT NULL,
	[sKeyword] [nvarchar](max) NOT NULL,
	[nFieldID] [int] NOT NULL,
 CONSTRAINT [PK_lstCompulsory Fields] PRIMARY KEY CLUSTERED 
(
	[nFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lstWizardHelper]    Script Date: 22-06-2020 15:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lstWizardHelper](
	[nWizardHelperID] [int] IDENTITY(1,1) NOT NULL,
	[sWizardHelperType] [varchar](50) NULL,
	[sWizardHelperValue] [varchar](max) NULL,
	[nWizardID] [int] NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_WizardHelper] PRIMARY KEY CLUSTERED 
(
	[nWizardHelperID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lstWizards]    Script Date: 22-06-2020 15:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lstWizards](
	[nWizardID] [int] IDENTITY(1,1) NOT NULL,
	[sWizardName] [varchar](50) NULL,
	[sWizardDescription] [varchar](500) NULL,
	[dtLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_Wizard] PRIMARY KEY CLUSTERED 
(
	[nWizardID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblAdminModuleLogger]    Script Date: 22-06-2020 15:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAdminModuleLogger](
	[nAMLID] [int] NOT NULL,
	[nAdminUserID] [int] NOT NULL,
	[sModuleName] [nvarchar](50) NOT NULL,
	[sEventName] [nvarchar](50) NOT NULL,
	[sDescription] [nvarchar](200) NOT NULL,
	[dtLogTime] [datetime] NOT NULL,
 CONSTRAINT [PK_tblAdminModuleLogger] PRIMARY KEY CLUSTERED 
(
	[nAMLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblAdminUsers]    Script Date: 22-06-2020 15:44:42 ******/
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
	[dtLastLoggedIn] [datetime] NOT NULL,
 CONSTRAINT [PK_AdminUser] PRIMARY KEY CLUSTERED 
(
	[nAdminUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblFacilities]    Script Date: 22-06-2020 15:44:42 ******/
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
	[nFacLocCount] [int] NULL,
	[bFacilityLogging] [bit] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[nFacilityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblFacilityConnections]    Script Date: 22-06-2020 15:44:42 ******/
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
/****** Object:  Table [dbo].[tblFacilityLocations]    Script Date: 22-06-2020 15:44:42 ******/
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
	[sAuthTemplateName] [nvarchar](200) NULL,
	[bLocationActiveStatus] [bit] NULL,
	[sNormalizedLocationName] [nvarchar](50) NULL,
	[nAuthExpirationMonths] [int] NULL,
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
/****** Object:  Table [dbo].[tblPatientModuleLogger]    Script Date: 22-06-2020 15:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPatientModuleLogger](
	[nPMLID] [int] NOT NULL,
	[sUserIPAddress] [nvarchar](50) NOT NULL,
	[sFacilityGUID] [varchar](max) NOT NULL,
	[sWizardName] [nvarchar](75) NOT NULL,
	[dtLogTime] [datetime] NOT NULL,
 CONSTRAINT [PK_tblPatientModuleLogger] PRIMARY KEY CLUSTERED 
(
	[nPMLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblRequesters]    Script Date: 22-06-2020 15:44:42 ******/
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
/****** Object:  Table [dbo].[tblTempRequesters]    Script Date: 22-06-2020 15:44:42 ******/
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
ALTER TABLE [dbo].[lstWizardHelper] ADD  CONSTRAINT [DF_lstWizardHelper_dtLastUpdate]  DEFAULT (getdate()) FOR [dtLastUpdate]
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
/****** Object:  StoredProcedure [dbo].[spDeleteFacilityBynFacilityId]    Script Date: 22-06-2020 15:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Malay Doshi
-- Create date: 06-22-2020
-- Description:	Deleting All Records for Facility
-- =============================================
CREATE PROCEDURE [dbo].[spDeleteFacilityBynFacilityId]
	-- Add the parameters for the stored procedure here
	@nFacilityId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM [dbo].[lnkFacilityFieldMaps] where nFacilityID = @nFacilityId
	DELETE FROM [dbo].[tblFacilityLocations] where nFacilityID = @nFacilityId
	DELETE FROM [dbo].[tblFacilityConnections] where nFacilityID = @nFacilityId
	DELETE FROM [tblFacilities] where nFacilityID = @nFacilityId
END
GO
/****** Object:  StoredProcedure [dbo].[spGetLogoAndBackgroundImageByLocationId]    Script Date: 22-06-2020 15:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ========================================================
-- Author:		Reji Oommen
-- Create date: 06-21-2020
-- Description:	Get Logo And Background Image By LocationId
-- ========================================================
CREATE PROCEDURE [dbo].[spGetLogoAndBackgroundImageByLocationId]
	-- Add the parameters for the stored procedure here
	@nLocationId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Select Top 1
		sConfigLogoData
		,sConfigBackgroundData
	FROM
		tblFacilityLocations
	WHERE
		tblFacilityLocations.nFacilityLocationID = 1
		AND
		tblFacilityLocations.bLocationActiveStatus = 1

END
GO
/****** Object:  StoredProcedure [dbo].[spGetLogoAndBackgroundImageforFacility]    Script Date: 22-06-2020 15:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Reji Oommen
-- Create date: 06-21-2020
-- Description:	to get the LOGO and Background
-- =============================================
CREATE PROCEDURE [dbo].[spGetLogoAndBackgroundImageforFacility] 
	-- Add the parameters for the stored procedure here
	@nFacilityId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select Top 1
		sConfigLogoData
		,sConfigBackgroundData
	FROM
		tblFacilityLocations
	INNER JOIN tblFacilities
		ON tblFacilities.nFacilityID = tblFacilityLocations.nFacilityID
	WHERE
		tblFacilities.nFacilityID = @nFacilityId
		AND
		tblFacilityLocations.bLocationActiveStatus = 1
	ORDER BY 
		tblFacilityLocations.nFacilityLocationID asc
END
GO
/****** Object:  StoredProcedure [dbo].[spGetLogoAndBackgroundImageforFacilityGUID]    Script Date: 22-06-2020 15:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        Reji Oommen
-- Create date: 06-21-2020
-- Description:    to get the LOGO and Background
-- =============================================
create PROCEDURE [dbo].[spGetLogoAndBackgroundImageforFacilityGUID] 
    -- Add the parameters for the stored procedure here
    --@nFacilityId int
    @sGuid nvarchar(max)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

 

    -- Insert statements for procedure here
    Select Top 1
        sConfigLogoData
        ,sConfigBackgroundData
    FROM
        tblFacilityLocations
    INNER JOIN tblFacilities
        ON tblFacilities.nFacilityID = tblFacilityLocations.nFacilityID
    INNER JOIN [dbo].[tblFacilityConnections]
        ON [tblFacilityConnections].nFacilityID = tblFacilityLocations.nFacilityID
    WHERE
        --tblFacilities.nFacilityID = @nFacilityId
        --AND
        [tblFacilityConnections].[sGUID] = @sGuid
        AND
        tblFacilityLocations.bLocationActiveStatus = 1
    ORDER BY 
        tblFacilityLocations.nFacilityLocationID asc
END
GO
/****** Object:  StoredProcedure [dbo].[spGetPatientFormBynFacilityID]    Script Date: 22-06-2020 15:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Reji Oommen>
-- Create date: <06-20-2020>
-- Description:	<Get Patient Form By FacilityID>
-- =============================================
CREATE PROCEDURE [dbo].[spGetPatientFormBynFacilityID] @ID int
AS
BEGIN
SELECT 
    nFieldID 
	,nWizardId 
	,sTableName 
	,sFieldName 
    ,bShow 
    ,nFacilityID 
    ,nFacilityFieldMapID 
FROM( 
SELECT 
                                
      lnkFacilityFieldMaps.nFacilityFieldMapID 
    , lnkFacilityFieldMaps.nFieldID 
    , lnkFacilityFieldMaps.nWizardID 
    , lnkFacilityFieldMaps.sTableName 
    , lnkFacilityFieldMaps.bShow 
    , lnkFacilityFieldMaps.nFacilityID 
    , lstFields.sFieldName 
FROM lnkFacilityFieldMaps 
INNER JOIN lstFields 

    ON lstFields.nFieldID = lnkFacilityFieldMaps.nFieldID 
WHERE 

    nFacilityId = @ID AND sTableName = 'lstFields' 

UNION 

SELECT 

      lnkFacilityFieldMaps.nFacilityFieldMapID 
    , lnkFacilityFieldMaps.nFieldID 
    , lnkFacilityFieldMaps.nWizardID 
    , lnkFacilityFieldMaps.sTableName 
    , lnkFacilityFieldMaps.bShow 
    , lnkFacilityFieldMaps.nFacilityID 
    , 'Primary Reason - ' + lstPrimaryReasons.sPrimaryReasonName as sFieldName 
FROM lnkFacilityFieldMaps 
INNER JOIN lstPrimaryReasons 

    ON lstPrimaryReasons.nPrimaryReasonID = lnkFacilityFieldMaps.nFieldID 
WHERE 

    nFacilityId = @ID AND sTableName = 'lstPrimaryReasons' 

UNION 

SELECT 

      lnkFacilityFieldMaps.nFacilityFieldMapID 
    , lnkFacilityFieldMaps.nFieldID 
    , lnkFacilityFieldMaps.nWizardID 
    , lnkFacilityFieldMaps.sTableName 
    , lnkFacilityFieldMaps.bShow 
    , lnkFacilityFieldMaps.nFacilityID 
    , 'Record Type - ' + lstRecordTypes.sRecordTypeName as sFieldName 
FROM lnkFacilityFieldMaps 
INNER JOIN lstRecordTypes 

    ON lstRecordTypes.nRecordTypeID = lnkFacilityFieldMaps.nFieldID 
WHERE 

    nFacilityId = @ID AND sTableName = 'lstRecordTypes' 

UNION 

SELECT 

      lnkFacilityFieldMaps.nFacilityFieldMapID 
    , lnkFacilityFieldMaps.nFieldID 
    , lnkFacilityFieldMaps.nWizardID 
    , lnkFacilityFieldMaps.sTableName 
    , lnkFacilityFieldMaps.bShow 
    , lnkFacilityFieldMaps.nFacilityID 
    , 'Sensitive Info - ' + lstSensitiveInfo.sSensitiveInfoName as sFieldName 
FROM lnkFacilityFieldMaps 
INNER JOIN lstSensitiveInfo 

    ON lstSensitiveInfo.nSensitiveInfoID = lnkFacilityFieldMaps.nFieldID 
WHERE 

    nFacilityId = @ID AND sTableName = 'lstSensitiveInfo' 

UNION 

SELECT 

      lnkFacilityFieldMaps.nFacilityFieldMapID 
    , lnkFacilityFieldMaps.nFieldID 
    , lnkFacilityFieldMaps.nWizardID 
    , lnkFacilityFieldMaps.sTableName 
    , lnkFacilityFieldMaps.bShow 
    , lnkFacilityFieldMaps.nFacilityID 
    , 'Shipment Type - ' + lstShipmentTypes.sShipmentTypeName as sFieldName 
FROM lnkFacilityFieldMaps 
INNER JOIN lstShipmentTypes 

    ON lstShipmentTypes.nShipmentTypeID = lnkFacilityFieldMaps.nFieldID 
WHERE 

    nFacilityId = @ID AND sTableName = 'lstShipmentTypes' 
    ) as FieldMap 

ORDER BY sTableName
END
GO
/****** Object:  StoredProcedure [dbo].[spGetWizardConfigBynFacilityId]    Script Date: 22-06-2020 15:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Reji Oommen
-- Create date: 06-20-2020
-- Description:	Get Wizard Config By nFacilityId
-- =============================================
CREATE PROCEDURE [dbo].[spGetWizardConfigBynFacilityId]
	-- Add the parameters for the stored procedure here
	 @nFacilityId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	-- Select Fields for generic fields
	SELECT 
		[lstFields].[sNormalizedFieldName] + ':' + CASE tblFacilityFieldMaps.[bShow] WHEN 1 THEN 'true' ELSE 'false' END AS 'FieldShowHide'
	FROM 
		[lnkFacilityFieldMaps] as tblFacilityFieldMaps
	INNER JOIN [lstFields]
		ON [lstFields].nFieldID = tblFacilityFieldMaps.nFieldID
	WHERE 
		tblFacilityFieldMaps.nFacilityID = @nFacilityId
		AND
		tblFacilityFieldMaps.sTableName = 'lstFields'


	-- Select Fields for Active Wizard
	SELECT 
			--[nWizardID]
			[sWizardName]
			--,[dtLastUpdate]
	FROM [MRO_RequestWizard].[dbo].[lstWizards]	
	WHERE nWizardID IN (
						SELECT [nWizardID] FROM (
						SELECT 
							  [nWizardID]
							  ,[lnkFacilityFieldMaps].bShow
						FROM [MRO_RequestWizard].[dbo].[lnkFacilityFieldMaps]
						WHERE nFacilityID = 1
						GROUP BY nWizardID,bShow ) AS A
						WHERE A.bShow = 1
						)

	-- Select Fields for Primary Reasons if bshow is true
	SELECT 
		[lstPrimaryReasons].[sNormalizedPrimaryReasonName]
		,[lstPrimaryReasons].[sPrimaryReasonName]
		,[lstPrimaryReasons].[sFieldToolTip]
	FROM 
		[lnkFacilityFieldMaps]
	INNER JOIN [lstPrimaryReasons]
		ON [lstPrimaryReasons].nWizardID = [lnkFacilityFieldMaps].nWizardID
		AND [lstPrimaryReasons].nPrimaryReasonID = [lnkFacilityFieldMaps].nFieldID
	WHERE 
		[lnkFacilityFieldMaps].nFacilityID = @nFacilityId
		AND
		[lnkFacilityFieldMaps].sTableName = 'lstPrimaryReasons'
		AND
		[lnkFacilityFieldMaps].bShow = 1


	-- Select Fields for Record Types if bshow is true
	SELECT 
		[lstRecordTypes].[sNormalizedRecordTypeName]
		,[lstRecordTypes].[sRecordTypeName]
		,[lstRecordTypes].[sFieldToolTip]
	FROM 
		[lnkFacilityFieldMaps]
	INNER JOIN [lstRecordTypes]
		ON [lstRecordTypes].nWizardID = [lnkFacilityFieldMaps].nWizardID
		AND [lstRecordTypes].nRecordTypeID = [lnkFacilityFieldMaps].nFieldID
	WHERE 
		[lnkFacilityFieldMaps].nFacilityID = @nFacilityId
		AND
		[lnkFacilityFieldMaps].sTableName = 'lstRecordTypes'
		AND
		[lnkFacilityFieldMaps].bShow = 1


	-- Select Fields for Sensitive Info if bshow is true
	SELECT 
		[lstSensitiveInfo].[sNormalizedSensitiveInfoName]
		,[lstSensitiveInfo].[sSensitiveInfoName]
		,[lstSensitiveInfo].[sFieldToolTip]
	FROM 
		[lnkFacilityFieldMaps]
	INNER JOIN [lstSensitiveInfo]
		ON [lstSensitiveInfo].nWizardID = [lnkFacilityFieldMaps].nWizardID
		AND [lstSensitiveInfo].nSensitiveInfoID = [lnkFacilityFieldMaps].nFieldID
	WHERE 
		[lnkFacilityFieldMaps].nFacilityID = @nFacilityId
		AND
		[lnkFacilityFieldMaps].sTableName = 'lstSensitiveInfo'
		AND
		[lnkFacilityFieldMaps].bShow = 1


	-- Select Fields for Shipment Types if bshow is true
	SELECT 
		[lstShipmentTypes].[sNormalizedShipmentTypeName]
		,[lstShipmentTypes].[sShipmentTypeName]
		,[lstShipmentTypes].[sFieldToolTip]
	FROM 
		[lnkFacilityFieldMaps]
	INNER JOIN [lstShipmentTypes]
		ON [lstShipmentTypes].nWizardID = [lnkFacilityFieldMaps].nWizardID
		AND [lstShipmentTypes].nShipmentTypeID = [lnkFacilityFieldMaps].nFieldID
	WHERE 
		[lnkFacilityFieldMaps].nFacilityID = @nFacilityId
		AND
		[lnkFacilityFieldMaps].sTableName = 'lstShipmentTypes'
		AND
		[lnkFacilityFieldMaps].bShow = 1

	-- Select [lstWizardHelper] for all the configured text from DB
	SELECT
		[sWizardHelperType]
		,[sWizardHelperValue]
	FROM 
		[lstWizardHelper]
	INNER JOIN [dbo].[lstWizards]
		ON [lstWizards].nWizardID = [lstWizardHelper].nWizardID

	--Select Active Locations for Current Facility Id

	SELECT
		sLocationName
		,sNormalizedLocationName
		,nAuthExpirationMonths
	FROM
		tblFacilityLocations
	WHERE 
		nFacilityID = @nFacilityId 
		AND 
		bLocationActiveStatus = 1
    
END
GO
USE [master]
GO
ALTER DATABASE [MRO_RequestWizard] SET  READ_WRITE 
GO
