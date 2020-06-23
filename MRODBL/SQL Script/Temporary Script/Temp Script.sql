USE [master]
GO
/****** Object:  Database [MRO_RequestWizard]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[lnkFacilityFieldMaps]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[lnkFacilityPrimaryReasons]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[lnkFacilityRecordTypes]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[lnkFacilitySensitiveInfo]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[lnkFacilityShipmentTypes]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[lstFields]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[lstPrimaryReasons]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[lstRecordTypes]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[lstSensitiveInfo]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[lstShipmentTypes]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[lstValidateAuthorizationDoc]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[lstWizardHelper]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[lstWizards]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[tblAdminModuleLogger]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[tblAdminUsers]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[tblFacilities]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[tblFacilityConnections]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[tblFacilityLocations]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[tblPatientModuleLogger]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[tblRequesters]    Script Date: 22-06-2020 20:54:41 ******/
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
/****** Object:  Table [dbo].[tblTempRequesters]    Script Date: 22-06-2020 20:54:41 ******/
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
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (1, 1, 1, 2, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (2, 1, 2, 3, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (3, 1, 3, 4, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (4, 1, 4, 4, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (5, 1, 5, 5, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (6, 1, 6, 5, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (7, 1, 7, 5, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (8, 1, 8, 5, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (9, 1, 9, 6, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (10, 1, 10, 7, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (11, 1, 11, 8, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (12, 1, 12, 9, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (13, 1, 13, 10, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (14, 1, 14, 11, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (15, 1, 15, 12, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (16, 1, 16, 13, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (17, 1, 17, 14, 1, 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.427' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (18, 1, 1, 9, 1, 1, CAST(N'2020-06-22T20:48:32.433' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.433' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (19, 1, 2, 9, 1, 1, CAST(N'2020-06-22T20:48:32.433' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.433' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (20, 1, 3, 9, 1, 1, CAST(N'2020-06-22T20:48:32.433' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.433' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (21, 1, 4, 9, 1, 1, CAST(N'2020-06-22T20:48:32.433' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.433' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (22, 1, 5, 9, 1, 1, CAST(N'2020-06-22T20:48:32.433' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.433' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (23, 1, 6, 9, 1, 1, CAST(N'2020-06-22T20:48:32.433' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.433' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (24, 1, 1, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (25, 1, 2, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (26, 1, 3, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (27, 1, 4, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (28, 1, 5, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (29, 1, 6, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (30, 1, 7, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (31, 1, 8, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (32, 1, 9, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (33, 1, 10, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (34, 1, 11, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (35, 1, 12, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (36, 1, 13, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (37, 1, 14, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (38, 1, 15, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (39, 1, 16, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (40, 1, 17, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (41, 1, 18, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (42, 1, 19, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (43, 1, 20, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (44, 1, 21, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (45, 1, 22, 9, 1, 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.437' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (46, 1, 1, 12, 1, 1, CAST(N'2020-06-22T20:48:32.440' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.440' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (47, 1, 2, 12, 1, 1, CAST(N'2020-06-22T20:48:32.440' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.440' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (48, 1, 3, 12, 1, 1, CAST(N'2020-06-22T20:48:32.440' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.440' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (49, 1, 4, 12, 1, 1, CAST(N'2020-06-22T20:48:32.440' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.440' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (50, 1, 1, 14, 1, 1, CAST(N'2020-06-22T20:48:32.443' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.443' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (51, 1, 2, 14, 1, 1, CAST(N'2020-06-22T20:48:32.443' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.443' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (52, 1, 3, 14, 1, 1, CAST(N'2020-06-22T20:48:32.443' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.443' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (53, 1, 4, 14, 1, 1, CAST(N'2020-06-22T20:48:32.443' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.443' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (54, 1, 6, 14, 1, 1, CAST(N'2020-06-22T20:48:32.443' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.443' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (55, 2, 1, 2, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (56, 2, 2, 3, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (57, 2, 3, 4, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (58, 2, 4, 4, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (59, 2, 5, 5, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (60, 2, 6, 5, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (61, 2, 7, 5, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (62, 2, 8, 5, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (63, 2, 9, 6, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (64, 2, 10, 7, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (65, 2, 11, 8, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (66, 2, 12, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (67, 2, 13, 10, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (68, 2, 14, 11, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (69, 2, 15, 12, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (70, 2, 16, 13, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (71, 2, 17, 14, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (72, 2, 1, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (73, 2, 2, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (74, 2, 3, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (75, 2, 4, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (76, 2, 5, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (77, 2, 6, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (78, 2, 1, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (79, 2, 2, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (80, 2, 3, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (81, 2, 4, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (82, 2, 5, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (83, 2, 6, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (84, 2, 7, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (85, 2, 8, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (86, 2, 9, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (87, 2, 10, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (88, 2, 11, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (89, 2, 12, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (90, 2, 13, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (91, 2, 14, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (92, 2, 15, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (93, 2, 16, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (94, 2, 17, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (95, 2, 18, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (96, 2, 19, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (97, 2, 20, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (98, 2, 21, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (99, 2, 22, 9, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (100, 2, 1, 12, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (101, 2, 2, 12, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (102, 2, 3, 12, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (103, 2, 4, 12, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (104, 2, 1, 14, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (105, 2, 2, 14, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (106, 2, 3, 14, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (107, 2, 4, 14, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (108, 2, 6, 14, 1, 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.120' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (109, 3, 1, 2, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (110, 3, 2, 3, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (111, 3, 3, 4, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (112, 3, 4, 4, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (113, 3, 5, 5, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (114, 3, 6, 5, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (115, 3, 7, 5, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (116, 3, 8, 5, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (117, 3, 9, 6, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (118, 3, 10, 7, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (119, 3, 11, 8, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (120, 3, 12, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (121, 3, 13, 10, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (122, 3, 14, 11, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (123, 3, 15, 12, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (124, 3, 16, 13, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (125, 3, 17, 14, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (126, 3, 1, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (127, 3, 2, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (128, 3, 3, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (129, 3, 4, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (130, 3, 5, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (131, 3, 6, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (132, 3, 1, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (133, 3, 2, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (134, 3, 3, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (135, 3, 4, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (136, 3, 5, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (137, 3, 6, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (138, 3, 7, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (139, 3, 8, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (140, 3, 9, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (141, 3, 10, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (142, 3, 11, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (143, 3, 12, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (144, 3, 13, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (145, 3, 14, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (146, 3, 15, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (147, 3, 16, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (148, 3, 17, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (149, 3, 18, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (150, 3, 19, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (151, 3, 20, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (152, 3, 21, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (153, 3, 22, 9, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (154, 3, 1, 12, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (155, 3, 2, 12, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (156, 3, 3, 12, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (157, 3, 4, 12, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (158, 3, 1, 14, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (159, 3, 2, 14, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (160, 3, 3, 14, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (161, 3, 4, 14, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (162, 3, 6, 14, 1, 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.893' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (163, 4, 1, 2, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (164, 4, 2, 3, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (165, 4, 3, 4, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (166, 4, 4, 4, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (167, 4, 5, 5, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (168, 4, 6, 5, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (169, 4, 7, 5, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (170, 4, 8, 5, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (171, 4, 9, 6, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (172, 4, 10, 7, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (173, 4, 11, 8, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (174, 4, 12, 9, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (175, 4, 13, 10, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (176, 4, 14, 11, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (177, 4, 15, 12, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (178, 4, 16, 13, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (179, 4, 17, 14, 1, 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.643' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (180, 4, 1, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (181, 4, 2, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (182, 4, 3, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (183, 4, 4, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (184, 4, 5, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (185, 4, 6, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (186, 4, 1, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (187, 4, 2, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (188, 4, 3, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (189, 4, 4, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (190, 4, 5, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (191, 4, 6, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (192, 4, 7, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (193, 4, 8, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (194, 4, 9, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (195, 4, 10, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (196, 4, 11, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (197, 4, 12, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (198, 4, 13, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (199, 4, 14, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (200, 4, 15, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (201, 4, 16, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (202, 4, 17, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (203, 4, 18, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (204, 4, 19, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (205, 4, 20, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (206, 4, 21, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (207, 4, 22, 9, 1, 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.647' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (208, 4, 1, 12, 1, 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (209, 4, 2, 12, 1, 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (210, 4, 3, 12, 1, 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (211, 4, 4, 12, 1, 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (212, 4, 1, 14, 1, 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (213, 4, 2, 14, 1, 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (214, 4, 3, 14, 1, 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (215, 4, 4, 14, 1, 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (216, 4, 6, 14, 1, 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.650' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (217, 5, 1, 2, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (218, 5, 2, 3, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (219, 5, 3, 4, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (220, 5, 4, 4, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (221, 5, 5, 5, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (222, 5, 6, 5, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (223, 5, 7, 5, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (224, 5, 8, 5, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (225, 5, 9, 6, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (226, 5, 10, 7, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (227, 5, 11, 8, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (228, 5, 12, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (229, 5, 13, 10, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (230, 5, 14, 11, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (231, 5, 15, 12, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (232, 5, 16, 13, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (233, 5, 17, 14, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (234, 5, 1, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (235, 5, 2, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (236, 5, 3, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (237, 5, 4, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (238, 5, 5, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (239, 5, 6, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (240, 5, 1, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (241, 5, 2, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (242, 5, 3, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (243, 5, 4, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (244, 5, 5, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (245, 5, 6, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (246, 5, 7, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (247, 5, 8, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (248, 5, 9, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (249, 5, 10, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (250, 5, 11, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (251, 5, 12, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (252, 5, 13, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (253, 5, 14, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (254, 5, 15, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (255, 5, 16, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (256, 5, 17, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (257, 5, 18, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (258, 5, 19, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (259, 5, 20, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (260, 5, 21, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (261, 5, 22, 9, 1, 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.207' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (262, 5, 1, 12, 1, 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (263, 5, 2, 12, 1, 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (264, 5, 3, 12, 1, 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (265, 5, 4, 12, 1, 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (266, 5, 1, 14, 1, 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (267, 5, 2, 14, 1, 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (268, 5, 3, 14, 1, 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (269, 5, 4, 14, 1, 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (270, 5, 6, 14, 1, 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.210' AS DateTime), N'lstShipmentTypes')
GO
SET IDENTITY_INSERT [dbo].[lnkFacilityFieldMaps] OFF
GO
SET IDENTITY_INSERT [dbo].[lstFields] ON 
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (1, 2, N'Facility Location Selections', N'MROSelectedLocation', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (2, 3, N'Are you the patient?', N'MROAreYouPatient', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (3, 4, N'What is your\Patient''s email?', N'MROPEmailId', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (4, 4, N'Confirm to send Email Report checkbox', N'MROConfirmReport', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (5, 5, N'Patient First Name', N'MROPatientFirstName', CAST(N'2020-06-08T21:57:10.037' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (6, 5, N'Patient Middle Initial', N'MROPatientMiddleInitial', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (7, 5, N'Patient Last Name', N'MROPatientLastName', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (8, 5, N'Is Patient Deceased', N'MROIsPatientDeceased', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (9, 6, N'What is the patient''s ZIP code?', N'MROZipCode', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (10, 7, N'What is the patient''s street address?', N'MROStreetArea', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (11, 8, N'When was the patient born?', N'MROPBirthDate', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (12, 9, N'What''s the primary reason
for requesting records?', N'MROPrimaryReason', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (13, 10, N'Specify an approximate*
date range (optional)', N'MROTFDateRange', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (14, 11, N'Which types of
records would you like?', N'MRORecordType', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (15, 12, N'Is there any sensitive info you
would also like included?', N'MROSensitiveInfo', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (16, 13, N'When should this
authorization expire?', N'MROAuthExpireDate', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (17, 14, N'How should we send records ?', N'MROShipmentType', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[lstFields] OFF
GO
SET IDENTITY_INSERT [dbo].[lstPrimaryReasons] ON 
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [nWizardID], [sNormalizedPrimaryReasonName], [sFieldToolTip], [dtLastUpdate]) VALUES (1, N'Continued Care', 9, N'MROContinuedCare', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [nWizardID], [sNormalizedPrimaryReasonName], [sFieldToolTip], [dtLastUpdate]) VALUES (2, N'Patient Request', 9, N'MROPatientRequest', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [nWizardID], [sNormalizedPrimaryReasonName], [sFieldToolTip], [dtLastUpdate]) VALUES (3, N'Insurance', 9, N'MROInsurance', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [nWizardID], [sNormalizedPrimaryReasonName], [sFieldToolTip], [dtLastUpdate]) VALUES (4, N'Attorney', 9, N'MROAttorney', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [nWizardID], [sNormalizedPrimaryReasonName], [sFieldToolTip], [dtLastUpdate]) VALUES (5, N'Social Security Benefit', 9, N'MROSocialSecurityBenefit', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [nWizardID], [sNormalizedPrimaryReasonName], [sFieldToolTip], [dtLastUpdate]) VALUES (6, N'Other Reason', 9, N'MROOtherPrimaryReason', NULL, CAST(N'2020-06-22T12:48:07.853' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstPrimaryReasons] OFF
GO
SET IDENTITY_INSERT [dbo].[lstRecordTypes] ON 
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (1, N'Abstract', 9, N'MROAbstract', N'State abstract per facility definition/template design.', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (2, N'DRS - (Designated Record Set) ', 9, N'MRODRS DesignatedRecordSet)', N'any item, collection, or grouping of protected health information maintained, collected, used, or disseminated by a covered entity” that are used to make decisions about individuals health care or payment of claim.', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (3, N'History & Physical ', 9, N'MROHistoryPhysical ', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (4, N'Progress Notes ', 9, N'MROProgressNotes', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (5, N'S.O.A.P Notes (Nurse’s assessment notes)', 9, N'MROSOAPNotesNursesassessmentnotes', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (6, N'Consultation Reports ', 9, N'MROConsultationReports', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (7, N'Lab Results ', 9, N'MROLabResults', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (8, N'Radiology Reports ', 9, N'MRORadiologyReports', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:39:56.217' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (9, N'Radiology Images ', 9, N'MRORadiologyImages', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:40:10.597' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (10, N'Wound Care Photos ', 9, N'MROWoundCarePhotos', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:40:24.900' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (11, N'Lab Results ', 9, N'MROLabResults', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:40:38.990' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (12, N'Test Results ', 9, N'MROTestResults', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:40:49.813' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (13, N'Physician Orders ', 9, N'MROPhysicianOrders', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:41:00.270' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (14, N'Psychotherapy Notes (see item 2) ', 9, N'MROPsychotherapyNotesseeitem2)', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:41:17.543' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (15, N'Discharge Summary/Instructions', 9, N'MRODischargeSummaryInstructions', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:41:33.550' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (16, N'EKG Reports ', 9, N'MROEKGReports', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:41:49.140' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (17, N'ECG Reports ', 9, N'MROECGReports', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:42:08.137' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (18, N'Cardiology Reports', 9, N'MROCardiologyReports', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:42:20.223' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (19, N'Catheterization Imaging ', 9, N'MROCatheterizationImaging', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:42:30.950' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (20, N'Rhythm Strips ', 9, N'MRORhythmStrips', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:42:41.513' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (21, N'Pathology Reports ', 9, N'MROPathologyReports', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:42:52.557' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (22, N'Pathology Slides', 9, N'MROPathologySlides', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:43:03.360' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstRecordTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[lstSensitiveInfo] ON 
GO
INSERT [dbo].[lstSensitiveInfo] ([nSensitiveInfoID], [sSensitiveInfoName], [nWizardID], [sNormalizedSensitiveInfoName], [sFieldToolTip], [dtLastUpdate]) VALUES (1, N'HIV Test Results', 12, N'MROHIVTestResults', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstSensitiveInfo] ([nSensitiveInfoID], [sSensitiveInfoName], [nWizardID], [sNormalizedSensitiveInfoName], [sFieldToolTip], [dtLastUpdate]) VALUES (2, N'Behavioural/Mental Health Records', 12, N'MROBehavioural/MentalHealthRecords', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstSensitiveInfo] ([nSensitiveInfoID], [sSensitiveInfoName], [nWizardID], [sNormalizedSensitiveInfoName], [sFieldToolTip], [dtLastUpdate]) VALUES (3, N'Substance Abuse Information', 12, N'MROSubstanceAbuseInformation', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstSensitiveInfo] ([nSensitiveInfoID], [sSensitiveInfoName], [nWizardID], [sNormalizedSensitiveInfoName], [sFieldToolTip], [dtLastUpdate]) VALUES (4, N'Sexually Transmitted Dieases', 12, N'MROSexuallyTransmittedDieases', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstSensitiveInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[lstShipmentTypes] ON 
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [nWizardID], [sNormalizedShipmentTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (1, N'Patient Portal', 14, N'MROPatientPortal', N'Please contact your healthcare provider to setup a patient portal if you do not have one already setup for guidance on how to do so.', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [nWizardID], [sNormalizedShipmentTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (2, N'Secure Email', 14, N'MROSecureEmail', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [nWizardID], [sNormalizedShipmentTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (3, N'Email', 14, N'MROEmailShipment', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [nWizardID], [sNormalizedShipmentTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (4, N'In-Person', 14, N'MROIn-Person', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [nWizardID], [sNormalizedShipmentTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (6, N'Fax', 14, N'MROFax', N'Over certain number of pages will be sent by mail – paper or CD or specify only fax to providers, etc.', CAST(N'2020-06-21T13:14:38.497' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstShipmentTypes] OFF
GO
INSERT [dbo].[lstValidateAuthorizationDoc] ([sFieldname], [sKeyword], [nFieldID]) VALUES (N'PatientName', N'MROPatientFullName', 1)
GO
INSERT [dbo].[lstValidateAuthorizationDoc] ([sFieldname], [sKeyword], [nFieldID]) VALUES (N'PatientDateOfBirth', N'MROPatientDOB', 2)
GO
INSERT [dbo].[lstValidateAuthorizationDoc] ([sFieldname], [sKeyword], [nFieldID]) VALUES (N'PatientTelephoneNo', N'MROPatientTelephoneNo', 3)
GO
INSERT [dbo].[lstValidateAuthorizationDoc] ([sFieldname], [sKeyword], [nFieldID]) VALUES (N'PatientAddress', N'MROPatientAddress', 4)
GO
SET IDENTITY_INSERT [dbo].[lstWizardHelper] ON 
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (1, N'disclaimer01', N'Want to print, fax or mail in your request? Click here for our PDF form.', 1, CAST(N'2020-06-22T13:07:57.680' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (2, N'disclaimer02', N'Note: Please use a recent version of Chrome, Firefox, or Safari.', 1, CAST(N'2020-06-22T13:07:57.683' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (3, N'disclaimer03', N'It only takes a few minutes to request your records. <br />Please have a copy of your Government or Photo ID ready.', 1, CAST(N'2020-06-22T13:07:57.687' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (4, N'disclaimer04', N'Request your health records.', 1, CAST(N'2020-06-22T13:07:57.687' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (5, N'disclaimer05', N'Potential Charge may be incurred for the request of records', 1, CAST(N'2020-06-22T13:07:57.690' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (6, N'phoneFooter', N'1234567890', 1, CAST(N'2020-06-22T13:07:57.697' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (7, N'disclaimer01', N'Great! Which location are you requesting records from?', 2, CAST(N'2020-06-22T13:07:57.697' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (8, N'disclaimer01', N'Are you requesting records for yourself?', 3, CAST(N'2020-06-22T13:07:57.700' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (9, N'disclaimer02', N'*If you''re not the patient, you''ll have the opportunity to <br />supply additional documentation to verify that you''re <br />authorized to make this request.', 3, CAST(N'2020-06-22T13:07:57.703' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (10, N'disclaimer03', N'*legal* relationship required with examples such as Parent, Medical POA, Next of Kin etc.', 3, CAST(N'2020-06-22T13:07:57.707' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (11, N'disclaimer04', N'Yes, I want my medical records.', 3, CAST(N'2020-06-22T13:07:57.707' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (12, N'disclaimer05', N'No, I am requesting records for someone else (Child, Dependent, Decedent).', 3, CAST(N'2020-06-22T13:07:57.710' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (13, N'disclaimer05', N'If you are not the patient, you will have the opportunity to supply additional documentation to verify that you are authorized to make this request on their behalf.', 3, CAST(N'2020-06-22T13:07:57.713' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (14, N'disclaimer01', N'This should be the name given (i.e. maiden name, nickname, other) to the provider at the time of your visit.', 4, CAST(N'2020-06-22T13:07:57.717' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (15, N'disclaimer01', N'We will email you a confirmation of your request when you are finished', 7, CAST(N'2020-06-22T13:07:57.720' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (16, N'disclaimer02', N'must be US address', 7, CAST(N'2020-06-22T13:07:57.720' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (17, N'disclaimer01', N'Date range does not have to be exact.', 8, CAST(N'2020-06-22T13:07:57.723' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (18, N'disclaimer01', N'(This is optional but may help us better fulfill your request.)', 10, CAST(N'2020-06-22T13:07:57.723' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (19, N'disclaimer01', N'We will respond to your request within 30 days. If you need your records sooner, or if you have an upcoming appointment, please let us know.', 15, CAST(N'2020-06-22T13:07:57.727' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (20, N'disclaimer01', N'We will do our best to meet your time requirement. Enter date at least one day in the future.', 16, CAST(N'2020-06-22T13:07:57.727' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (21, N'disclaimer01', N'Please enter a valid mobile/cell phone number. We will use this phone number to verify your request with a text code.', 17, CAST(N'2020-06-22T13:07:57.730' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (22, N'disclaimer02', N'Standard messaging rates apply.', 17, CAST(N'2020-06-22T13:07:57.730' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (23, N'disclaimer01', N'Please enter a valid mobile/cell phone number. We will use this phone number to verify your request with a text code.', 18, CAST(N'2020-06-22T13:07:57.733' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (24, N'disclaimer02', N'Note: You will need to take a photo of your selected ID. Please click "Allow" when the browser asks to use your camera on the next screen.', 18, CAST(N'2020-06-22T13:07:57.733' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (25, N'disclaimer01', N'Click below to review your request form and sign your request. Please review all sections for accuracy, as any incorrect information may cause a delay in your records request delivery.', 20, CAST(N'2020-06-22T13:07:57.733' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (26, N'disclaimer01', N'We will respond to your request within 30 days. If you entered a deadline by which you need your records, or if you have an upcoming appointment, your request will be prioritized accordingly.', 22, CAST(N'2020-06-22T13:07:57.737' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (27, N'disclaimer01', N'Your request is now complete. You may now close this window', 24, CAST(N'2020-06-22T13:07:57.737' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstWizardHelper] OFF
GO
SET IDENTITY_INSERT [dbo].[lstWizards] ON 
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (1, N'Wizard-01', N'Introduction', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (2, N'Wizard-02', N'Locations', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (3, N'Wizard-03', N'Are You Patient', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (4, N'Wizard-04', N'Patient Full Name', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (5, N'Wizard-05', N'Patient DOB', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (6, N'Wizard-06', N'Patient Email', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (7, N'Wizard-07', N'Patient Address', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (8, N'Wizard-08', N'Date Range for requesting records', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (9, N'Wizard-09', N'Record Type', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (10, N'Wizard-10', N'Primary Reason', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (11, N'Wizard-11', N'To Whom should we release', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (12, N'Wizard-12', N'Shipment Type', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (13, N'Wizard-13', N'Authorization expire', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (14, N'Wizard-14', N'Sensitive Info', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (15, N'Wizard-15', N'Is there a deadline for this request', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (16, N'Wizard-16', N'Deadline Value', CAST(N'2020-06-21T19:06:25.137' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (17, N'Wizard-17', N'Additional details', CAST(N'2020-06-21T19:08:10.287' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (18, N'Wizard-18', N'Phone Verification', CAST(N'2020-06-21T19:08:23.620' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (19, N'Wizard-19', N'Photo Identification Document (Driving License)', CAST(N'2020-06-21T19:08:34.470' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (20, N'Wizard-20', N'Camera Option or File Upload', CAST(N'2020-06-21T19:09:00.637' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (21, N'Wizard-21', N'PDF Generation from Authorization Document and Digital Signature', CAST(N'2020-06-21T19:09:27.847' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (23, N'Wizard-22', N'Request Received Success Message', CAST(N'2020-06-21T19:10:24.823' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (24, N'Wizard-23', N'Rating and Feedback', CAST(N'2020-06-21T19:11:30.923' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (25, N'Wizard-24', N'Thank you for feedback ', CAST(N'2020-06-21T19:11:32.997' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstWizards] OFF
GO
SET IDENTITY_INSERT [dbo].[tblFacilities] ON 
GO
INSERT [dbo].[tblFacilities] ([nFacilityID], [nROIFacilityID], [sFacilityName], [sDescription], [sSMTPUsername], [sSMTPPassword], [sSMTPUrl], [sFTPUsername], [sFTPPassword], [sFTPUrl], [sOutboundEmail], [bActiveStatus], [sConfigShowFields], [sConfigShowWizards], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [nFacLocCount], [bFacilityLogging]) VALUES (1, 0, N'Ortho Nebraska', N'Get comprehensive treatments and services', N'admin@ortho.com', N'Admin@123', N'smtp.ortho.com', N'admin@ortho.com', N'Admin@123', N'ftp.ortho.com', N'noreply@ortho.com', 0, N'T Data', N'T Data', 1, CAST(N'2020-06-22T20:48:32.200' AS DateTime), 1, CAST(N'2020-06-22T20:48:32.200' AS DateTime), 0, 1)
GO
INSERT [dbo].[tblFacilities] ([nFacilityID], [nROIFacilityID], [sFacilityName], [sDescription], [sSMTPUsername], [sSMTPPassword], [sSMTPUrl], [sFTPUsername], [sFTPPassword], [sFTPUrl], [sOutboundEmail], [bActiveStatus], [sConfigShowFields], [sConfigShowWizards], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [nFacLocCount], [bFacilityLogging]) VALUES (2, 0, N'Cleveland Clinic', N'American academic medical center based in Cleveland', N'admin@cleveland.com', N'Admin@123', N'smtp.cleveland.com', N'admin@cleveland.com', N'Admin@123', N'ftp.cleveland.com', N'noreply@cleveland.com', 0, N'T Data', N'T Data', 1, CAST(N'2020-06-22T20:50:09.113' AS DateTime), 1, CAST(N'2020-06-22T20:50:09.113' AS DateTime), 0, 1)
GO
INSERT [dbo].[tblFacilities] ([nFacilityID], [nROIFacilityID], [sFacilityName], [sDescription], [sSMTPUsername], [sSMTPPassword], [sSMTPUrl], [sFTPUsername], [sFTPPassword], [sFTPUrl], [sOutboundEmail], [bActiveStatus], [sConfigShowFields], [sConfigShowWizards], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [nFacLocCount], [bFacilityLogging]) VALUES (3, 0, N'Trinity Health', N'American not-for-profit Catholic health system', N'admin@trinity.com', N'Admin@123', N'smtp.trinity.com', N'admin@trinity.com', N'Admin@123', N'ftp.trinity.com', N'noreply@trinity.com', 0, N'T Data', N'T Data', 1, CAST(N'2020-06-22T20:51:17.877' AS DateTime), 1, CAST(N'2020-06-22T20:51:17.877' AS DateTime), 0, 1)
GO
INSERT [dbo].[tblFacilities] ([nFacilityID], [nROIFacilityID], [sFacilityName], [sDescription], [sSMTPUsername], [sSMTPPassword], [sSMTPUrl], [sFTPUsername], [sFTPPassword], [sFTPUrl], [sOutboundEmail], [bActiveStatus], [sConfigShowFields], [sConfigShowWizards], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [nFacLocCount], [bFacilityLogging]) VALUES (4, 0, N'Ascension Health', N'Listening to you, caring for you', N'admin@ascension-health.com', N'Admin@123', N'smtp.ascension-health.com', N'admin@ascension-health.com', N'Admin@123', N'ftp.ascension-health.com', N'noreply@ascension-health.com', 0, N'T Data', N'T Data', 1, CAST(N'2020-06-22T20:52:14.630' AS DateTime), 1, CAST(N'2020-06-22T20:52:14.630' AS DateTime), 0, 1)
GO
INSERT [dbo].[tblFacilities] ([nFacilityID], [nROIFacilityID], [sFacilityName], [sDescription], [sSMTPUsername], [sSMTPPassword], [sSMTPUrl], [sFTPUsername], [sFTPPassword], [sFTPUrl], [sOutboundEmail], [bActiveStatus], [sConfigShowFields], [sConfigShowWizards], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [nFacLocCount], [bFacilityLogging]) VALUES (5, 0, N'Hospital Corporation of America', N'American for-profit operator of healthcare facilities that was founded in 1968', N'admin@hca.com', N'Admin@123', N'smtp.hca.com', N'admin@hca.com', N'Admin@123', N'ftp.hca.com', N'noreply@hca.com', 0, N'T Data', N'T Data', 1, CAST(N'2020-06-22T20:53:25.187' AS DateTime), 1, CAST(N'2020-06-22T20:53:25.187' AS DateTime), 0, 1)
GO
SET IDENTITY_INSERT [dbo].[tblFacilities] OFF
GO
SET IDENTITY_INSERT [dbo].[tblFacilityConnections] ON 
GO
INSERT [dbo].[tblFacilityConnections] ([nFacilityConnectionID], [nFacilityID], [sGUID], [sConnectionString], [nCreatingAdminUserID], [dtCreated], [nUpdateAdminUserID], [dtLastUpdate]) VALUES (1, 1, N'62fd2634-104b-4644-a577-57fe5e47db87', N'server=MUM-AT-W4-DE001\SQLEXPRESS; database=MRO_RequestWizard;Trusted_Connection=True;', 1, CAST(N'2020-06-22T20:48:37.3400000' AS DateTime2), 1, CAST(N'2020-06-22T20:48:37.3400000' AS DateTime2))
GO
INSERT [dbo].[tblFacilityConnections] ([nFacilityConnectionID], [nFacilityID], [sGUID], [sConnectionString], [nCreatingAdminUserID], [dtCreated], [nUpdateAdminUserID], [dtLastUpdate]) VALUES (2, 2, N'b39216ee-2579-4024-a6b3-b3364faed0ea', N'server=MUM-AT-W4-DE001\SQLEXPRESS; database=MRO_RequestWizard;Trusted_Connection=True;', 1, CAST(N'2020-06-22T20:50:09.1200000' AS DateTime2), 1, CAST(N'2020-06-22T20:50:09.1200000' AS DateTime2))
GO
INSERT [dbo].[tblFacilityConnections] ([nFacilityConnectionID], [nFacilityID], [sGUID], [sConnectionString], [nCreatingAdminUserID], [dtCreated], [nUpdateAdminUserID], [dtLastUpdate]) VALUES (3, 3, N'fa82b8ef-5cd9-4ed3-8d4d-0c11b65c9c46', N'server=MUM-AT-W4-DE001\SQLEXPRESS; database=MRO_RequestWizard;Trusted_Connection=True;', 1, CAST(N'2020-06-22T20:51:17.8930000' AS DateTime2), 1, CAST(N'2020-06-22T20:51:17.8930000' AS DateTime2))
GO
INSERT [dbo].[tblFacilityConnections] ([nFacilityConnectionID], [nFacilityID], [sGUID], [sConnectionString], [nCreatingAdminUserID], [dtCreated], [nUpdateAdminUserID], [dtLastUpdate]) VALUES (4, 4, N'0bd74310-922a-45b4-bb3e-9905c9c3c06d', N'server=MUM-AT-W4-DE001\SQLEXPRESS; database=MRO_RequestWizard;Trusted_Connection=True;', 1, CAST(N'2020-06-22T20:52:14.6500000' AS DateTime2), 1, CAST(N'2020-06-22T20:52:14.6500000' AS DateTime2))
GO
INSERT [dbo].[tblFacilityConnections] ([nFacilityConnectionID], [nFacilityID], [sGUID], [sConnectionString], [nCreatingAdminUserID], [dtCreated], [nUpdateAdminUserID], [dtLastUpdate]) VALUES (5, 5, N'e06f4f67-66f2-414b-bd57-f428b8da7edc', N'server=MUM-AT-W4-DE001\SQLEXPRESS; database=MRO_RequestWizard;Trusted_Connection=True;', 1, CAST(N'2020-06-22T20:53:25.2100000' AS DateTime2), 1, CAST(N'2020-06-22T20:53:25.2100000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[tblFacilityConnections] OFF
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
/****** Object:  StoredProcedure [dbo].[spDeleteFacilityBynFacilityId]    Script Date: 22-06-2020 20:54:42 ******/
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
/****** Object:  StoredProcedure [dbo].[spGetLogoAndBackgroundImageByLocationId]    Script Date: 22-06-2020 20:54:42 ******/
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
/****** Object:  StoredProcedure [dbo].[spGetLogoAndBackgroundImageforFacility]    Script Date: 22-06-2020 20:54:42 ******/
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
/****** Object:  StoredProcedure [dbo].[spGetLogoAndBackgroundImageforFacilityGUID]    Script Date: 22-06-2020 20:54:42 ******/
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
/****** Object:  StoredProcedure [dbo].[spGetPatientFormBynFacilityID]    Script Date: 22-06-2020 20:54:42 ******/
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
/****** Object:  StoredProcedure [dbo].[spGetWizardConfigBynFacilityId]    Script Date: 22-06-2020 20:54:42 ******/
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
		[lstFields].[sNormalizedFieldName], 
		tblFacilityFieldMaps.[bShow]
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

	--SELECT
	--	sLocationName
	--	,sNormalizedLocationName
	--	,nAuthExpirationMonths
	--FROM
	--	tblFacilityLocations
	--WHERE 
	--	nFacilityID = @nFacilityId 
	--	AND 
	--	bLocationActiveStatus = 1
    
END
GO
USE [master]
GO
ALTER DATABASE [MRO_RequestWizard] SET  READ_WRITE 
GO
