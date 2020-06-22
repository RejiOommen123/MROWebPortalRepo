USE [master]
GO
/****** Object:  Database [MRO_RequestWizard]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[lnkFacilityFieldMaps]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[lnkFacilityPrimaryReasons]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[lnkFacilityRecordTypes]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[lnkFacilitySensitiveInfo]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[lnkFacilityShipmentTypes]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[lstFields]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[lstPrimaryReasons]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[lstRecordTypes]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[lstSensitiveInfo]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[lstShipmentTypes]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[lstValidateAuthorizationDoc]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[lstWizardHelper]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[lstWizards]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[tblAdminModuleLogger]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[tblAdminUsers]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[tblFacilities]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[tblFacilityConnections]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[tblFacilityLocations]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[tblPatientModuleLogger]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[tblRequesters]    Script Date: 22-06-2020 15:54:22 ******/
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
/****** Object:  Table [dbo].[tblTempRequesters]    Script Date: 22-06-2020 15:54:22 ******/
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
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (1, 1, 1, 2, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (2, 1, 2, 3, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (3, 1, 3, 4, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (4, 1, 4, 4, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (5, 1, 5, 5, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (6, 1, 6, 5, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (7, 1, 7, 5, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (8, 1, 8, 5, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (9, 1, 9, 6, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (10, 1, 10, 7, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (11, 1, 11, 8, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (12, 1, 12, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (13, 1, 13, 10, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (14, 1, 14, 11, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (15, 1, 15, 12, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (16, 1, 16, 13, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (17, 1, 17, 14, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstFields')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (18, 1, 1, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (19, 1, 2, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (20, 1, 3, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (21, 1, 4, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (22, 1, 5, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (23, 1, 6, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstPrimaryReasons')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (24, 1, 1, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (25, 1, 2, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (26, 1, 3, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (27, 1, 4, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (28, 1, 5, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (29, 1, 6, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (30, 1, 7, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (31, 1, 8, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (32, 1, 9, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (33, 1, 10, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (34, 1, 11, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (35, 1, 12, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (36, 1, 13, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (37, 1, 14, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (38, 1, 15, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (39, 1, 16, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (40, 1, 17, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (41, 1, 18, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (42, 1, 19, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (43, 1, 20, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (44, 1, 21, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (45, 1, 22, 9, 1, 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.943' AS DateTime), N'lstRecordTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (46, 1, 1, 12, 1, 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (47, 1, 2, 12, 1, 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (48, 1, 3, 12, 1, 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (49, 1, 4, 12, 1, 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), N'lstSensitiveInfo')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (50, 1, 1, 14, 1, 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (51, 1, 2, 14, 1, 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (52, 1, 3, 14, 1, 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (53, 1, 4, 14, 1, 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), N'lstShipmentTypes')
GO
INSERT [dbo].[lnkFacilityFieldMaps] ([nFacilityFieldMapID], [nFacilityID], [nFieldID], [nWizardID], [bShow], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [sTableName]) VALUES (54, 1, 6, 14, 1, 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.947' AS DateTime), N'lstShipmentTypes')
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
INSERT [dbo].[tblFacilities] ([nFacilityID], [nROIFacilityID], [sFacilityName], [sDescription], [sSMTPUsername], [sSMTPPassword], [sSMTPUrl], [sFTPUsername], [sFTPPassword], [sFTPUrl], [sOutboundEmail], [bActiveStatus], [sConfigShowFields], [sConfigShowWizards], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate], [nFacLocCount], [bFacilityLogging]) VALUES (1, 0, N'Cleveland Clinic', N'Info about Cleveland', N'test1', N'pass@12', N'smtp.cleveland.com', N'cleveland101', N'pass@101', N'ftp.test.com', N'noreply@cleveland.com', 1, N'T Data', N'T Data', 1, CAST(N'2020-06-22T15:49:15.933' AS DateTime), 1, CAST(N'2020-06-22T15:49:15.933' AS DateTime), 1, 1)
GO
SET IDENTITY_INSERT [dbo].[tblFacilities] OFF
GO
SET IDENTITY_INSERT [dbo].[tblFacilityConnections] ON 
GO
INSERT [dbo].[tblFacilityConnections] ([nFacilityConnectionID], [nFacilityID], [sGUID], [sConnectionString], [nCreatingAdminUserID], [dtCreated], [nUpdateAdminUserID], [dtLastUpdate]) VALUES (1, 1, N'44BE3F7C-ACFD-4B09-BFF2-6671597817B5', N'', 1, CAST(N'2020-06-22T15:52:06.9870000' AS DateTime2), 1, CAST(N'2020-06-22T15:52:06.9870000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[tblFacilityConnections] OFF
GO
SET IDENTITY_INSERT [dbo].[tblFacilityLocations] ON 
GO
INSERT [dbo].[tblFacilityLocations] ([nFacilityLocationID], [nFacilityID], [nROILocationID], [sLocationCode], [sLocationName], [sLocationAddress], [sPhoneNo], [sFaxNo], [sConfigLogoName], [sConfigLogoData], [sConfigBackgroundName], [sConfigBackgroundData], [sAuthTemplate], [sAuthTemplateName], [bLocationActiveStatus], [sNormalizedLocationName], [nAuthExpirationMonths], [nCreatedAdminUserID], [dtCreated], [nUpdatedAdminUserID], [dtLastUpdate]) VALUES (1, 1, 1, N'LOC1', N'Cleveland Location', N'Cleveland, Ohio, US', N'9876543210', N'0123456789', N'cleveland_logo.jpg', N'data:image/jpeg;base64,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', N'cleveland_background.jpg', N'data:image/jpeg;base64,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', N'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cmVhbQ1lbmRvYmoNMjAwIDAgb2JqDTw8L0JCb3hbMC4wIDAuMCAxNTcuNTYgMTAuNjhdL0Zvcm1UeXBlIDEvTGVuZ3RoIDEzL01hdHJpeFsxLjAgMC4wIDAuMCAxLjAgMC4wIDAuMF0vUmVzb3VyY2VzPDwvUHJvY1NldFsvUERGXT4+L1N1YnR5cGUvRm9ybS9UeXBlL1hPYmplY3Q+PnN0cmVhbQ0KL1R4IEJNQyAKRU1DCg0KZW5kc3RyZWFtDWVuZG9iag0yMDEgMCBvYmoNPDwvQkJveFswIDAgODEuMjQgMTAuNjhdL0Zvcm1UeXBlIDEvTGVuZ3RoIDEyL01hdHJpeFsxIDAgMCAxIDAgMF0vUmVzb3VyY2VzPDwvUHJvY1NldFsvUERGXT4+L1N1YnR5cGUvRm9ybS9UeXBlL1hPYmplY3Q+PnN0cmVhbQ0KL1R4IEJNQwpFTUMKDQplbmRzdHJlYW0NZW5kb2JqDTIwMiAwIG9iag08PC9CQm94WzAgMCA0OS40NCAxMC42OF0vRm9ybVR5cGUgMS9MZW5ndGggMTIvTWF0cml4WzEgMCAwIDEgMCAwXS9SZXNvdXJjZXM8PC9Qcm9jU2V0Wy9QREZdPj4vU3VidHlwZS9Gb3JtL1R5cGUvWE9iamVjdD4+c3RyZWFtDQovVHggQk1DCkVNQwoNCmVuZHN0cmVhbQ1lbmRvYmoNMjAzIDAgb2JqDTw8L0JCb3hbMCAwIDM2IDEwLjY4XS9Gb3JtVHlwZSAxL0xlbmd0aCAxMi9NYXRyaXhbMSAwIDAgMSAwIDBdL1Jlc291cmNlczw8L1Byb2NTZXRbL1BERl0+Pi9TdWJ0eXBlL0Zvcm0vVHlwZS9YT2JqZWN0Pj5zdHJlYW0NCi9UeCBCTUMKRU1DCg0KZW5kc3RyZWFtDWVuZG9iag0yMDQgMCBvYmoNPDwvQkJveFswLjAgMC4wIDExNy4wIDEwLjY4XS9Gb3JtVHlwZSAxL0xlbmd0aCAxMy9NYXRyaXhbMS4wIDAuMCAwLjAgMS4wIDAuMCAwLjBdL1Jlc291cmNlczw8L1Byb2NTZXRbL1BERl0+Pi9TdWJ0eXBlL0Zvcm0vVHlwZS9YT2JqZWN0Pj5zdHJlYW0NCi9UeCBCTUMgCkVNQwoNCmVuZHN0cmVhbQ1lbmRvYmoNMjA1IDAgb2JqDTw8L0JCb3hbMC4wIDAuMCAxNTMuMTIgMTAuNjhdL0Zvcm1UeXBlIDEvTGVuZ3RoIDEzL01hdHJpeFsxLjAgMC4wIDAuMCAxLjAgMC4wIDAuMF0vUmVzb3VyY2VzPDwvUHJvY1NldFsvUERGXT4+L1N1YnR5cGUvRm9ybS9UeXBlL1hPYmplY3Q+PnN0cmVhbQ0KL1R4IEJNQyAKRU1DCg0KZW5kc3RyZWFtDWVuZG9iag0yMDYgMCBvYmoNPDwvQkJveFswIDAgMzEwLjY4IDEwLjY4XS9Gb3JtVHlwZSAxL0xlbmd0aCAxMi9NYXRyaXhbMSAwIDAgMSAwIDBdL1Jlc291cmNlczw8L1Byb2NTZXRbL1BERl0+Pi9TdWJ0eXBlL0Zvcm0vVHlwZS9YT2JqZWN0Pj5zdHJlYW0NCi9UeCBCTUMKRU1DCg0KZW5kc3RyZWFtDWVuZG9iag0yMDcgMCBvYmoNPDwvQkJveFswIDAgOTQuNDQgMTAuNjhdL0Zvcm1UeXBlIDEvTGVuZ3RoIDEyL01hdHJpeFsxIDAgMCAxIDAgMF0vUmVzb3VyY2VzPDwvUHJvY1NldFsvUERGXT4+L1N1YnR5cGUvRm9ybS9UeXBlL1hPYmplY3Q+PnN0cmVhbQ0KL1R4IEJNQwpFTUMKDQplbmRzdHJlYW0NZW5kb2JqDTIwOCAwIG9iag08PC9CQm94WzAgMCA1LjM5OTk5IDYuOTYwMDFdL0Zvcm1UeXBlIDEvTGVuZ3RoIDAvTWF0cml4WzEgMCAwIDEgMCAwXS9SZXNvdXJjZXM8PC9Qcm9jU2V0Wy9QREZdPj4vU3VidHlwZS9Gb3JtL1R5cGUvWE9iamVjdD4+c3RyZWFtDQoNCmVuZHN0cmVhbQ1lbmRvYmoNMjA5IDAgb2JqDTw8L0JCb3hbMCAwIDUuMzk5OTkgNi45NjAwMV0vRm9ybVR5cGUgMS9MZW5ndGggMzQvTWF0cml4WzEgMCAwIDEgMCAwXS9SZXNvdXJjZXM8PC9Qcm9jU2V0Wy9QREZdPj4vU3VidHlwZS9Gb3JtL1R5cGUvWE9iamVjdD4+c3RyZWFtDQowIDAgMCByZwoxLjA4IDEuMzIgMy4yNCA0LjMyIHJlCmYKDQplbmRzdHJlYW0NZW5kb2JqDTIxMCAwIG9iag08PC9CQm94WzAgMCA1LjM5OTk5IDYuOTYwMDFdL0Zvcm1UeXBlIDEvTGVuZ3RoIDM3L01hdHJpeFsxIDAgMCAxIDAgMF0vUmVzb3VyY2VzPDwvUHJvY1NldFsvUERGXT4+L1N1YnR5cGUvRm9ybS9UeXBlL1hPYmplY3Q+PnN0cmVhbQ0KMC41IDAuNSAwLjUgcmcKMC42IDAuNiA0LjIgNS43NiByZQpmCg0KZW5kc3RyZWFtDWVuZG9iag0yMTEgMCBvYmoNPDwvQkJveFswIDAgNS4zOTk5OSA2Ljk2MDAxXS9GaWx0ZXIvRmxhdGVEZWNvZGUvRm9ybVR5cGUgMS9MZW5ndGggNjQvTWF0cml4WzEgMCAwIDEgMCAwXS9SZXNvdXJjZXM8PC9Qcm9jU2V0Wy9QREZdPj4vU3VidHlwZS9Gb3JtL1R5cGUvWE9iamVjdD4+c3RyZWFtDQpIiTLQM1UwgOKidC4DPTMFEDbRM1Iw1TM3UyhK5UrjMlAAQaC0oZ6BhYKhnrGRgrGekQlQFZAFVgEQYAD0kA4XDQplbmRzdHJlYW0NZW5kb2JqDTIxMiAwIG9iag08PC9CQm94WzAgMCA1LjM5OTk5IDYuOTYwMDFdL0Zvcm1UeXBlIDEvTGVuZ3RoIDAvTWF0cml4WzEgMCAwIDEgMCAwXS9SZXNvdXJjZXM8PC9Qcm9jU2V0Wy9QREZdPj4vU3VidHlwZS9Gb3JtL1R5cGUvWE9iamVjdD4+c3RyZWFtDQoNCmVuZHN0cmVhbQ1lbmRvYmoNMjEzIDAgb2JqDTw8L0JCb3hbMCAwIDUuMzk5OTkgNi45NjAwMV0vRm9ybVR5cGUgMS9MZW5ndGggMzQvTWF0cml4WzEgMCAwIDEgMCAwXS9SZXNvdXJjZXM8PC9Qcm9jU2V0Wy9QREZdPj4vU3VidHlwZS9Gb3JtL1R5cGUvWE9iamVjdD4+c3RyZWFtDQowIDAgMCByZwoxLjA4IDEuMzIgMy4yNCA0LjMyIHJlCmYKDQplbmRzdHJlYW0NZW5kb2JqDTIxNCAwIG9iag08PC9CQm94WzAgMCA1LjM5OTk5IDYuOTYwMDFdL0Zvcm1UeXBlIDEvTGVuZ3RoIDM3L01hdHJpeFsxIDAgMCAxIDAgMF0vUmVzb3VyY2VzPDwvUHJvY1NldFsvUERGXT4+L1N1YnR5cGUvRm9ybS9UeXBlL1hPYmplY3Q+PnN0cmVhbQ0KMC41IDAuNSAwLjUgcmcKMC42IDAuNiA0LjIgNS43NiByZQpmCg0KZW5kc3RyZWFtDWVuZG9iag0yMTUgMCBvYmoNPDwvQkJveFswIDAgNS4zOTk5OSA2Ljk2MDAxXS9GaWx0ZXIvRmxhdGVEZWNvZGUvRm9ybVR5cGUgMS9MZW5ndGggNjQvTWF0cml4WzEgMCAwIDEgMCAwXS9SZXNvdXJjZXM8PC9Qcm9jU2V0Wy9QREZdPj4vU3VidHlwZS9Gb3JtL1R5cGUvWE9iamVjdD4+c3RyZWFtDQpIiTLQM1UwgOKidC4DPTMFEDbRM1Iw1TM3UyhK5UrjMlAAQaC0oZ6BhYKhnrGRgrGekQlQFZAFVgEQYAD0kA4XDQplbmRzdHJlYW0NZW5kb2JqDTIxNiAwIG9iag08PC9CQm94WzAgMCA1LjM5OTk5IDYuOTYwMDFdL0Zvcm1UeXBlIDEvTGVuZ3RoIDAvTWF0cml4WzEgMCAwIDEgMCAwXS9SZXNvdXJjZXM8PC9Qcm9jU2V0Wy9QREZdPj4vU3VidHlwZS9Gb3JtL1R5cGUvWE9iamVjdD4+c3RyZWFtDQoNCmVuZHN0cmVhbQ1lbmRvYmoNMjE3IDAgb2JqDTw8L0JCb3hbMCAwIDUuMzk5OTkgNi45NjAwMV0vRm9ybVR5cGUgMS9MZW5ndGggMzQvTWF0cml4WzEgMCAwIDEgMCAwXS9SZXNvdXJjZXM8PC9Qcm9jU2V0Wy9QREZdPj4vU3VidHlwZS9Gb3JtL1R5cGUvWE9iamVjdD4+c3RyZWFtDQowIDAgMCByZwoxLjA4IDEuMzIgMy4yNCA0LjMyIHJlCmYKDQplbmRzdHJlYW0NZW5kb2JqDTIxOCAwIG9iag08PC9CQm94WzAgMCA1LjM5OTk5IDYuOTYwMDFdL0Zvcm1UeXBlIDEvTGVuZ3RoIDM3L01hdHJpeFsxIDAgMCAxIDAgMF0vUmVzb3VyY2VzPDwvUHJvY1NldFsvUERGXT4+L1N1YnR5cGUvRm9ybS9UeXBlL1hPYmplY3Q+PnN0cmVhbQ0KMC41IDAuNSAwLjUgcmcKMC42IDAuNiA0LjIgNS43NiByZQpmCg0KZW5kc3RyZWFtDWVuZG9iag0yMTkgMCBvYmoNPDwvQkJveFswIDAgNS4zOTk5OSA2Ljk2MDAxXS9GaWx0ZXIvRmxhdGVEZWNvZGUvRm9ybVR5cGUgMS9MZW5ndGggNjQvTWF0cml4WzEgMCAwIDEgMCAwXS9SZXNvdXJjZXM8PC9Qcm9jU2V0Wy9QREZdPj4vU3VidHlwZS9Gb3JtL1R5cGUvWE9iamVjdD4+c3RyZWFtDQpIiTLQM1UwgOKidC4DPTMFEDbRM1Iw1TM3UyhK5UrjMlAAQaC0oZ6BhYKhnrGRgrGekQlQFZAFVgEQYAD0kA4XDQplbmRzdHJlYW0NZW5kb2JqDTIyMCAwIG9iag08PC9CQm94WzAgMCA3Ni40NCA5LjYwMDAxXS9Gb3JtVHlwZSAxL0xlbmd0aCAxMi9NYXRyaXhbMSAwIDAgMSAwIDBdL1Jlc291cmNlczw8L1Byb2NTZXRbL1BERl0+Pi9TdWJ0eXBlL0Zvcm0vVHlwZS9YT2JqZWN0Pj5zdHJlYW0NCi9UeCBCTUMKRU1DCg0KZW5kc3RyZWFtDWVuZG9iag0yMjEgMCBvYmoNPDwvQkJveFswIDAgNDM2LjY4IDEwLjY4XS9Gb3JtVHlwZSAxL0xlbmd0aCAxMi9NYXRyaXhbMSAwIDAgMSAwIDBdL1Jlc291cmNlczw8L1Byb2NTZXRbL1BERl0+Pi9TdWJ0eXBlL0Zvcm0vVHlwZS9YT2JqZWN0Pj5zdHJlYW0NCi9UeCBCTUMKRU1DCg0KZW5kc3RyZWFtDWVuZG9iag0yMjIgMCBvYmoNPDwvQkJveFswIDAgNDM2LjY4IDEwLjY4XS9Gb3JtVHlwZSAxL0xlbmd0aCAxMi9NYXRyaXhbMSAwIDAgMSAwIDBdL1Jlc291cmNlczw8L1Byb2NTZXRbL1BERl0+Pi9TdWJ0eXBlL0Zvcm0vVHlwZS9YT2JqZWN0Pj5zdHJlYW0NCi9UeCBCTUMKRU1DCg0KZW5kc3RyZWFtDWVuZG9iag0yMjMgMCBvYmoNPDwvQkJveFswIDAgNDM2LjY4IDEwLjMyXS9Gb3JtVHlwZSAxL0xlbmd0aCAxMi9NYXRyaXhbMSAwIDAgMSAwIDBdL1Jlc291cmNlczw8L1Byb2NTZXRbL1BERl0+Pi9TdWJ0eXBlL0Zvcm0vVHlwZS9YT2JqZWN0Pj5zdHJlYW0NCi9UeCBCTUMKRU1DCg0KZW5kc3RyZWFtDWVuZG9iag0yMjQgMCBvYmoNPDwvQkJveFswIDAgNDM3LjQgMTAuMl0vRm9ybVR5cGUgMS9MZW5ndGggMTIvTWF0cml4WzEgMCAwIDEgMCAwXS9SZXNvdXJjZXM8PC9Qcm9jU2V0Wy9QREZdPj4vU3VidHlwZS9Gb3JtL1R5cGUvWE9iamVjdD4+c3RyZWFtDQovVHggQk1DCkVNQwoNCmVuZHN0cmVhbQ1lbmRvYmoNMjI1IDAgb2JqDTw8L0JCb3hbMCAwIDE5My4zMiAxMC42OF0vRm9ybVR5cGUgMS9MZW5ndGggMTIvTWF0cml4WzEgMCAwIDEgMCAwXS9SZXNvdXJjZXM8PC9Qcm9jU2V0Wy9QREZdPj4vU3VidHlwZS9Gb3JtL1R5cGUvWE9iamVjdD4+c3RyZWFtDQovVHggQk1DCkVNQwoNCmVuZHN0cmVhbQ1lbmRvYmoNMjI2IDAgb2JqDTw8L0JCb3hbMCAwIDI5Ny4xMiA5LjYwMDAxXS9Gb3JtVHlwZSAxL0xlbmd0aCAxMi9NYXRyaXhbMSAwIDAgMSAwIDBdL1Jlc291cmNlczw8L1Byb2NTZXRbL1BERl0+Pi9TdWJ0eXBlL0Zvcm0vVHlwZS9YT2JqZWN0Pj5zdHJlYW0NCi9UeCBCTUMKRU1DCg0KZW5kc3RyZWFtDWVuZG9iag0yMjcgMCBvYmoNPDwvQkJveFswIDAgNTguNDQgMTAuNjhdL0Zvcm1UeXBlIDEvTGVuZ3RoIDEyL01hdHJpeFsxIDAgMCAxIDAgMF0vUmVzb3VyY2VzPDwvUHJvY1NldFsvUERGXT4+L1N1YnR5cGUvRm9ybS9UeXBlL1hPYmplY3Q+PnN0cmVhbQ0KL1R4IEJNQwpFTUMKDQplbmRzdHJlYW0NZW5kb2JqDTIyOCAwIG9iag08PC9CQm94WzAgMCAxMzAuNDQgMTAuNjhdL0Zvcm1UeXBlIDEvTGVuZ3RoIDEyL01hdHJpeFsxIDAgMCAxIDAgMF0vUmVzb3VyY2VzPDwvUHJvY1NldFsvUERGXT4+L1N1YnR5cGUvRm9ybS9UeXBlL1hPYmplY3Q+PnN0cmVhbQ0KL1R4IEJNQwpFTUMKDQplbmRzdHJlYW0NZW5kb2JqDTIyOSAwIG9iag08PC9GaWx0ZXIvRmxhdGVEZWNvZGUvRmly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ZS9JbWFnZS9UeXBlL1hPYmplY3QvV2lkdGggMTkwPj5zdHJlYW0NCv/Y/+EAGEV4aWYAAElJKgAIAAAAAAAAAAAAAAD/7AARRHVja3kAAQAEAAAAZAAA/+EEAWh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8APD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS4zLWMwMTEgNjYuMTQ1NjYxLCAyMDEyLzAyLzA2LTE0OjU2OjI3ICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6ZGM9Imh0dHA6Ly9wdXJsLm9yZy9kYy9lbGVtZW50cy8xLjEvIiB4bXBNTTpPcmlnaW5hbERvY3VtZW50SUQ9InV1aWQ6NUQyMDg5MjQ5M0JGREIxMTkxNEE4NTkwRDMxNTA4QzgiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6MkEwMTUzNkQ5M0QzMTFFMjgwQkVFOERERjJENDMwNzMiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6MkEwMTUzNkM5M0QzMTFFMjgwQkVFOERERjJENDMwNzMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgSWxsdXN0cmF0b3IgQ1M2IChNYWNpbnRvc2gpIj4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InV1aWQ6ZDAyMDJjNjItZDZmMy1iYjQ2LTllYWUtNDAyN2JkZTFiYmM3IiBzdFJlZjpkb2N1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Y1dvcmtmbG93LzEuMC8iPgogICAgICAgICA8eG1wOk1vZGlmeURhdGU+MjAyMC0wNi0xNVQyMTo0ODoyNyswNTozMDwveG1wOk1vZGlmeURhdGU+CiAgICAgICAgIDx4bXA6Q3JlYXRlRGF0ZT4yMDE2LTEyLTE2VDEzOjQ4OjA3LTA1OjAwPC94bXA6Q3JlYXRlRGF0ZT4KICAgICAgICAgPHhtcDpNZXRhZGF0YURhdGU+MjAyMC0wNi0xNVQyMTo0ODoyNyswNTozMDwveG1wOk1ldGFkYXRhRGF0ZT4KICAgICAgICAgPHhtcDpDcmVhdG9yVG9vbD5BY3JvYmF0IFBERk1ha2VyIDE1IGZvciBXb3JkPC94bXA6Q3JlYXRvclRvb2w+CiAgICAgICAgIDx4bXBNTTpEb2N1bWVudElEPnV1aWQ6M2I5ZGE4NWItNDY3Yy00NTA2LTk2YzUtZjE1NTQxMzE5Mjc1PC94bXBNTTpEb2N1bWVudElEPgogICAgICAgICA8eG1wTU06SW5zdGFuY2VJRD51dWlkOmQ2ZTQ5ZGE1LThhMTEtNGFlYy05Y2Q5LTQ2ZGEzMzAzMDgwZDwveG1wTU06SW5zdGFuY2VJRD4KICAgICAgICAgPHhtcE1NOnN1YmplY3Q+CiAgICAgICAgICAgIDxyZGY6U2VxPgogICAgICAgICAgICAgICA8cmRmOmxpPjQ8L3JkZjpsaT4KICAgICAgICAgICAgPC9yZGY6U2VxPgogICAgICAgICA8L3htcE1NOnN1YmplY3Q+CiAgICAgICAgIDxkYzpmb3JtYXQ+YXBwbGljYXRpb24vcGRmPC9kYzpmb3JtYXQ+CiAgICAgICAgIDxkYzp0aXRsZT4KICAgICAgICAgICAgPHJkZjpBbHQ+CiAgICAgICAgICAgICAgIDxyZGY6bGkgeG1sOmxhbmc9IngtZGVmYXVsdCI+SEVBTFRIIElORk9STUFUSU9OIE1BTkFHRU1FTlQ8L3JkZjpsaT4KICAgICAgICAgICAgPC9yZGY6QWx0PgogICAgICAgICA8L2RjOnRpdGxlPgogICAgICAgICA8ZGM6ZGVzY3JpcHRpb24+CiAgICAgICAgICAgIDxyZGY6QWx0PgogICAgICAgICAgICAgICA8cmRmOmxpIHhtbDpsYW5nPSJ4LWRlZmF1bHQiLz4KICAgICAgICAgICAgPC9yZGY6QWx0PgogICAgICAgICA8L2RjOmRlc2NyaXB0aW9uPgogICAgICAgICA8ZGM6Y3JlYXRvcj4KICAgICAgICAgICAgPHJkZjpTZXE+CiAgICAgICAgICAgICAgIDxyZGY6bGk+UmVnaW5hIEJlY2tlcnQ8L3JkZjpsaT4KICAgICAgICAgICAgPC9yZGY6U2VxPgogICAgICAgICA8L2RjOmNyZWF0b3I+CiAgICAgICAgIDxwZGY6UHJvZHVjZXI+QWRvYmUgUERGIExpYnJhcnkgMTUuMDwvcGRmOlByb2R1Y2VyPgogICAgICAgICA8cGRmOktleXdvcmRzLz4KICAgICAgICAgPHBkZng6U291cmNlTW9kaWZpZWQ+RDoyMDE2MTIwNzIyMTYwMTwvcGRmeDpTb3VyY2VNb2RpZmllZD4KICAgICAgICAgPHBkZng6Q29tcGFueT5UcmluaXR5IEhlYWx0aDwvcGRmeDpDb21wYW55PgogICAgICAgICA8YWRob2N3Zjp2ZXJzaW9uPjEuMTwvYWRob2N3Zjp2ZXJzaW9uPgogICAgICAgICA8YWRob2N3ZjpzdGF0ZT4xPC9hZGhvY3dmOnN0YXRlPgogICAgICA8L3JkZjpEZXNjcmlwdGlvbj4KICAgPC9yZGY6UkRGPgo8L3g6eG1wbWV0YT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgIAo8P3hwYWNrZXQgZW5kPSJ3Ij8+DQplbmRzdHJlYW0NZW5kb2JqDTYgMCBvYmoNPDwvRmlsdGVyL0ZsYXRlRGVjb2RlL0ZpcnN0IDYvTGVuZ3RoIDUyL04gMS9UeXBlL09ialN0bT4+c3RyZWFtDQpo3jK0NFEwULCx0XfOL80rUTDU985MKY42tLQAigbF6odUFqTqBySmpxbb2QEEGAD7wwxSDQplbmRzdHJlYW0NZW5kb2JqDTcgMCBvYmoNPDwvRmlsdGVyL0ZsYXRlRGVjb2RlL0ZpcnN0IDYvTGVuZ3RoIDIzOS9OIDEvVHlwZS9PYmpTdG0+PnN0cmVhbQ0KaN48zmFLwzAQBuC/ct+WItpLtq5TxiDazg7XbsyCn9P25qKzkZgi/ffeQPbh4DhennvlfQIIy2Wsh3ByXhzo3fYGHqn9JB+i+Ml9fZt+FLW3vQ0jFGTO4cR3TyZY12cmkMgeFMq5VDzT2QLTW0wmiJP/FKu69a4xAfbZujQMg0zg6Dy8Od9F8QuNv7z8iCguXXcVFeJcJkrOFiq9YXF6EffedUNLTHauoQsIW9t440c27zCKX93gW2LHHi1112qYKq6HkgND80Ft4Ge1DWcSRa63dQGbar07lLre7CoodaWf8zKv6mi1+hNgAF9KVnANCmVuZHN0cmVhbQ1lbmRvYmoNOCAwIG9iag08PC9EZWNvZGVQYXJtczw8L0NvbHVtbnMgNS9QcmVkaWN0b3IgMTI+Pi9GaWx0ZXIvRmxhdGVEZWNvZGUvSURbPEZDQzAzNTk1NDY1REZENERCRTE1MjI2MzNDRDlDQjg4PjxFNTEwQkY1MzM1M0JENzQyQjZGMUJCMjBDMzY4MDJCMD5dL0luZm8gMTk1IDAgUi9MZW5ndGggODMvUm9vdCAxOTcgMCBSL1NpemUgMTk2L1R5cGUvWFJlZi9XWzEgMyAxXT4+c3RyZWFtDQpo3mJiAAEmRkaxZAYmBgZGTxDJfAtEsgWDSJbHIFIwDUQyTACr8QGq/3+KGSzCwIidZPyDIfIPn/pRcpQkj2ScOxoOw4dk2gKOUwaAAAMAUp0Lkg0KZW5kc3RyZWFtDWVuZG9iag1zdGFydHhyZWYNCjExNg0KJSVFT0YNCg==', N'Trinity Health - Holy Cross Hospital Silver Spring (MD).pdf', 1, NULL, NULL, 1, CAST(N'2020-06-22T15:50:20.760' AS DateTime), 1, CAST(N'2020-06-22T15:50:20.760' AS DateTime))
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
/****** Object:  StoredProcedure [dbo].[spDeleteFacilityBynFacilityId]    Script Date: 22-06-2020 15:54:23 ******/
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
/****** Object:  StoredProcedure [dbo].[spGetLogoAndBackgroundImageByLocationId]    Script Date: 22-06-2020 15:54:23 ******/
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
/****** Object:  StoredProcedure [dbo].[spGetLogoAndBackgroundImageforFacility]    Script Date: 22-06-2020 15:54:23 ******/
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
/****** Object:  StoredProcedure [dbo].[spGetLogoAndBackgroundImageforFacilityGUID]    Script Date: 22-06-2020 15:54:23 ******/
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
/****** Object:  StoredProcedure [dbo].[spGetPatientFormBynFacilityID]    Script Date: 22-06-2020 15:54:23 ******/
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
/****** Object:  StoredProcedure [dbo].[spGetWizardConfigBynFacilityId]    Script Date: 22-06-2020 15:54:23 ******/
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
