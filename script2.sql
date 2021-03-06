USE [master]
GO
/****** Object:  Database [hCMS]    Script Date: 06/07/2018 1:25:10 CH ******/
CREATE DATABASE [hCMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'hCMS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.HIEUHT\MSSQL\DATA\hCMS.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'hCMS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.HIEUHT\MSSQL\DATA\hCMS_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [hCMS] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [hCMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [hCMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [hCMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [hCMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [hCMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [hCMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [hCMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [hCMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [hCMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [hCMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [hCMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [hCMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [hCMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [hCMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [hCMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [hCMS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [hCMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [hCMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [hCMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [hCMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [hCMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [hCMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [hCMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [hCMS] SET RECOVERY FULL 
GO
ALTER DATABASE [hCMS] SET  MULTI_USER 
GO
ALTER DATABASE [hCMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [hCMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [hCMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [hCMS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [hCMS] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'hCMS', N'ON'
GO
USE [hCMS]
GO
/****** Object:  Table [dbo].[AdvertContentTypes]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvertContentTypes](
	[AdvertContentTypeId] [tinyint] NOT NULL,
	[AdvertContentTypeName] [nvarchar](50) NULL,
	[AdvertContentTypeDesc] [nvarchar](50) NULL,
 CONSTRAINT [PK_AdvertContentTypes] PRIMARY KEY CLUSTERED 
(
	[AdvertContentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AdvertDisplayTypes]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvertDisplayTypes](
	[AdvertDisplayTypeId] [tinyint] NOT NULL,
	[AdvertDisplayTypeName] [nvarchar](50) NULL,
	[AdvertDisplayTypeDesc] [nvarchar](50) NULL,
 CONSTRAINT [PK_AdvertDisplayTypes] PRIMARY KEY CLUSTERED 
(
	[AdvertDisplayTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AdvertPositionAdverts]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvertPositionAdverts](
	[AdvertPositionAdvertId] [int] IDENTITY(1,1) NOT NULL,
	[AdvertPositionId] [int] NULL,
	[SiteId] [smallint] NULL,
	[CategoryId] [smallint] NULL,
	[AdvertId] [int] NULL,
	[DisplayOrder] [int] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_AdvertPositionAdverts] PRIMARY KEY CLUSTERED 
(
	[AdvertPositionAdvertId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AdvertPositions]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvertPositions](
	[AdvertPositionId] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [smallint] NULL,
	[CategoryId] [smallint] NULL,
	[ApplicationTypeId] [tinyint] NULL,
	[PositionName] [nvarchar](255) NULL,
	[PositionDesc] [nvarchar](255) NULL,
	[Width] [nvarchar](50) NULL,
	[Height] [nvarchar](50) NULL,
	[OverflowWidth] [nvarchar](50) NULL,
	[OverflowHeight] [nvarchar](50) NULL,
	[AdvertDisplayTypeId] [tinyint] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_AdvertPositions] PRIMARY KEY CLUSTERED 
(
	[AdvertPositionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adverts]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adverts](
	[AdvertId] [int] IDENTITY(1,1) NOT NULL,
	[AdvertName] [nvarchar](255) NULL,
	[AdvertDesc] [nvarchar](255) NULL,
	[Url] [nvarchar](500) NULL,
	[ImagePath] [nvarchar](255) NULL,
	[ScriptContent] [nvarchar](max) NULL,
	[SiteId] [smallint] NULL,
	[AdvertContentTypeId] [tinyint] NULL,
	[Width] [nvarchar](50) NULL,
	[Height] [nvarchar](50) NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[PartnerId] [smallint] NULL,
	[AdvertStatusId] [tinyint] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_Adverts] PRIMARY KEY CLUSTERED 
(
	[AdvertId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AdvertStatus]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvertStatus](
	[AdvertStatusId] [tinyint] NOT NULL,
	[AdvertStatusName] [nvarchar](50) NULL,
	[AdvertStatusDesc] [nvarchar](50) NULL,
 CONSTRAINT [PK_AdvertStatus] PRIMARY KEY CLUSTERED 
(
	[AdvertStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AdvertViewLogs]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvertViewLogs](
	[AdvertViewLogId] [int] IDENTITY(1,1) NOT NULL,
	[AdvertId] [int] NULL,
	[AdvertPositionId] [int] NULL,
	[CategoryId] [smallint] NULL,
	[UserAgent] [nvarchar](255) NULL,
	[CustomerId] [int] NULL,
	[FromIP] [nvarchar](15) NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_AdvertViewLogs] PRIMARY KEY CLUSTERED 
(
	[AdvertViewLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArticleCategories]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleCategories](
	[ArticleCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NULL,
	[CategoryId] [smallint] NULL,
	[DisplayOrder] [int] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_ArticleCategories] PRIMARY KEY CLUSTERED 
(
	[ArticleCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArticleComments]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleComments](
	[ArticleCommentId] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [tinyint] NULL,
	[ApplicationTypeId] [tinyint] NULL,
	[SiteId] [smallint] NULL,
	[DataTypeId] [smallint] NULL,
	[ArticleId] [int] NULL,
	[ParentCommentId] [int] NULL,
	[CommentLevel] [tinyint] NULL,
	[FullName] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Comment] [nvarchar](2000) NULL,
	[RatingScore] [tinyint] NULL,
	[FromIP] [nvarchar](30) NULL,
	[UserAgent] [nvarchar](255) NULL,
	[ReviewStatusId] [tinyint] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_ArticleComments] PRIMARY KEY CLUSTERED 
(
	[ArticleCommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArticleDisplays]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleDisplays](
	[ArticleDisplayId] [int] IDENTITY(1,1) NOT NULL,
	[DisplayTypeId] [smallint] NULL,
	[LanguageId] [tinyint] NULL,
	[ApplicationTypeId] [tinyint] NULL,
	[ArticleId] [int] NULL,
	[DisplayOrder] [int] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_ArticleDisplays] PRIMARY KEY CLUSTERED 
(
	[ArticleDisplayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArticleEventStreams]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleEventStreams](
	[ArticleEventStreamId] [int] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NULL,
	[EventStreamId] [int] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_ArticleEventStreams] PRIMARY KEY CLUSTERED 
(
	[ArticleEventStreamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArticleFeatures]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleFeatures](
	[ArticleFeatureId] [int] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NULL,
	[FeatureId] [smallint] NULL,
	[FeatureValue] [nvarchar](255) NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_ArticleFeatures] PRIMARY KEY CLUSTERED 
(
	[ArticleFeatureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArticleLanguages]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleLanguages](
	[ArticleLanguageId] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [tinyint] NULL,
	[ApplicationTypeId] [tinyint] NULL,
	[ArticleId] [int] NULL,
	[Title] [nvarchar](500) NULL,
	[Summary] [nvarchar](2000) NULL,
	[ArticleContent] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](255) NULL,
	[SourceUrl] [nvarchar](255) NULL,
	[DataSourceId] [smallint] NULL,
	[DisplayOrder] [int] NULL,
	[ReviewStatusId] [tinyint] NULL,
	[ViewCount] [int] NULL,
	[CommentCount] [int] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_ArticleLanguages] PRIMARY KEY CLUSTERED 
(
	[ArticleLanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArticleLocations]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleLocations](
	[ArticleLocationId] [int] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NULL,
	[ProvinceId] [smallint] NULL,
	[DistrictId] [smallint] NULL,
	[WardId] [int] NULL,
	[Address] [nvarchar](100) NULL,
	[Longitude] [float] NULL,
	[Latitude] [float] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_ArticleLocations] PRIMARY KEY CLUSTERED 
(
	[ArticleLocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArticleMedias]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleMedias](
	[ArticleMediaId] [int] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NULL,
	[MediaId] [int] NULL,
	[MediaTypeId] [tinyint] NULL,
	[FilePath] [nvarchar](200) NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_ArticleMedias] PRIMARY KEY CLUSTERED 
(
	[ArticleMediaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArticlePublics]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticlePublics](
	[ArticlePublicId] [bigint] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NULL,
	[CategoryId] [smallint] NULL,
	[HotOrder] [int] NULL,
	[TimeAtOrder] [int] NULL,
	[PublishedDate] [datetime] NULL,
 CONSTRAINT [PK_ArticlePublics] PRIMARY KEY CLUSTERED 
(
	[ArticlePublicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArticleRateDetails]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleRateDetails](
	[ArticleRateDetailId] [int] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NULL,
	[RateTypeId] [tinyint] NULL,
	[FromIP] [nvarchar](30) NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_ArticleRateDetails] PRIMARY KEY CLUSTERED 
(
	[ArticleRateDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArticleRelates]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleRelates](
	[ArticleRelateId] [int] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NULL,
	[ArticleReferenceId] [int] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_ArticleRelates] PRIMARY KEY CLUSTERED 
(
	[ArticleRelateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Articles]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articles](
	[ArticleId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Summary] [nvarchar](2000) NULL,
	[ArticleContent] [nvarchar](max) NULL,
	[ArticleCode] [nvarchar](50) NULL,
	[ImagePath] [nvarchar](255) NULL,
	[ArticleUrl] [nvarchar](255) NULL,
	[SourceUrl] [nvarchar](255) NULL,
	[DataSourceId] [smallint] NULL,
	[CategoryId] [smallint] NULL,
	[SiteId] [smallint] NULL,
	[DataTypeId] [tinyint] NULL,
	[ArticleTypeId] [tinyint] NULL,
	[MetaTitle] [nvarchar](255) NULL,
	[MetaDesc] [nvarchar](500) NULL,
	[MetaKeywords] [nvarchar](500) NULL,
	[OriginalPrice] [float] NULL,
	[SalePrice] [float] NULL,
	[ContactPrice] [nvarchar](50) NULL,
	[CurrencyId] [tinyint] NULL,
	[InventoryStatusId] [tinyint] NULL,
	[IsVerify] [tinyint] NULL,
	[JsonData] [nvarchar](max) NULL,
	[PublishTime] [datetime] NULL,
	[DisplayStartTime] [datetime] NULL,
	[DisplayEndTime] [datetime] NULL,
	[DisplayOrder] [int] NULL,
	[ShowTop] [tinyint] NULL,
	[ShowBottom] [tinyint] NULL,
	[ShowWeb] [tinyint] NULL,
	[ShowWap] [tinyint] NULL,
	[ShowApp] [tinyint] NULL,
	[IconStatusId] [tinyint] NULL,
	[ReviewStatusId] [tinyint] NULL,
	[ViewCount] [int] NULL,
	[CommentCount] [int] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
	[UpdUserId] [int] NULL,
	[UpdDateTime] [datetime] NULL,
	[RevUserId] [int] NULL,
	[RevDateTime] [datetime] NULL,
 CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArticleTags]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleTags](
	[ArticleTagId] [int] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NULL,
	[TagId] [int] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_ArticleTags] PRIMARY KEY CLUSTERED 
(
	[ArticleTagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArticleTypes]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleTypes](
	[ArticleTypeId] [tinyint] IDENTITY(1,1) NOT NULL,
	[ArticleTypeName] [nvarchar](50) NULL,
	[ArticleTypeDesc] [nvarchar](50) NULL,
	[DataTypeId] [tinyint] NULL,
	[DisplayOrder] [tinyint] NULL,
	[DateTime] [datetime] NULL,
 CONSTRAINT [PK_ArticleTypes] PRIMARY KEY CLUSTERED 
(
	[ArticleTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArticleViewCount]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleViewCount](
	[ArticleViewCountId] [int] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NULL,
	[LanguageId] [tinyint] NULL,
	[ApplicationTypeId] [tinyint] NULL,
	[ViewCount] [int] NULL,
	[CrDateTime] [datetime] NULL,
	[LastViewTime] [datetime] NULL,
 CONSTRAINT [PK_ArticleViewCount] PRIMARY KEY CLUSTERED 
(
	[ArticleViewCountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArticleViewLogs]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleViewLogs](
	[ArticleViewLogId] [int] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NULL,
	[SiteId] [smallint] NULL,
	[DataTypeId] [tinyint] NULL,
	[CategoryId] [smallint] NULL,
	[LanguageId] [tinyint] NULL,
	[ApplicationTypeId] [tinyint] NULL,
	[RefererFrom] [nvarchar](255) NULL,
	[UserAgent] [nvarchar](255) NULL,
	[CustomerId] [int] NULL,
	[FromIP] [nvarchar](15) NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_ArticleViewLogs] PRIMARY KEY CLUSTERED 
(
	[ArticleViewLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [smallint] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](200) NULL,
	[CategoryDesc] [nvarchar](200) NULL,
	[CategoryUrl] [nvarchar](200) NULL,
	[DataTypeId] [smallint] NULL,
	[FeatureGroupId] [smallint] NULL,
	[SiteId] [smallint] NULL,
	[MetaTitle] [nvarchar](200) NULL,
	[MetaDesc] [nvarchar](500) NULL,
	[MetaKeywords] [nvarchar](500) NULL,
	[CanonicalTag] [nvarchar](255) NULL,
	[H1Tag] [nvarchar](255) NULL,
	[SeoFooter] [nvarchar](2000) NULL,
	[SeoInfo1] [nvarchar](255) NULL,
	[SeoInfo2] [nvarchar](255) NULL,
	[PageDesc] [nvarchar](1000) NULL,
	[ParentCategoryId] [int] NULL,
	[CategoryLevel] [tinyint] NULL,
	[ImagePath] [nvarchar](200) NULL,
	[DisplayOrder] [int] NULL,
	[TreeOrder] [int] NULL,
	[ShowTop] [tinyint] NULL,
	[ShowBottom] [tinyint] NULL,
	[ShowWeb] [tinyint] NULL,
	[ShowWap] [tinyint] NULL,
	[ShowApp] [tinyint] NULL,
	[UrlRewriteType] [nvarchar](30) NULL,
	[JsonData] [ntext] NULL,
	[ReviewStatusId] [tinyint] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CategoryDisplays]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryDisplays](
	[CategoryDisplayId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [smallint] NULL,
	[DisplayTypeId] [smallint] NULL,
	[DisplayOrder] [int] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_CategoryDisplays] PRIMARY KEY CLUSTERED 
(
	[CategoryDisplayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CategoryLanguages]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryLanguages](
	[CategoryLanguageId] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [tinyint] NULL,
	[ApplicationTypeId] [tinyint] NULL,
	[CategoryId] [smallint] NULL,
	[CategoryName] [nvarchar](200) NULL,
	[CategoryDesc] [nvarchar](200) NULL,
	[CategoryUrl] [nvarchar](200) NULL,
	[MetaTitle] [nvarchar](200) NULL,
	[MetaDesc] [nvarchar](500) NULL,
	[MetaKeywords] [nvarchar](500) NULL,
	[DisplayOrder] [int] NULL,
	[ReviewStatusId] [tinyint] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_CategoryLanguages] PRIMARY KEY CLUSTERED 
(
	[CategoryLanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DataDictionaries]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataDictionaries](
	[DataDictionaryId] [int] IDENTITY(1,1) NOT NULL,
	[DataDictionaryTypeId] [smallint] NULL,
	[DataDictionaryName] [nvarchar](255) NULL,
	[DataDictionaryDesc] [nvarchar](255) NULL,
	[MinValue] [int] NULL,
	[MaxValue] [int] NULL,
	[DisplayOrder] [int] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_DataDictionaries] PRIMARY KEY CLUSTERED 
(
	[DataDictionaryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DataDictionaryTypes]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataDictionaryTypes](
	[DataDictionaryTypeId] [smallint] IDENTITY(1,1) NOT NULL,
	[DataDictionaryTypeName] [nvarchar](200) NULL,
	[DataDictionaryTypeDesc] [nvarchar](200) NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_DataDictionaryTypes] PRIMARY KEY CLUSTERED 
(
	[DataDictionaryTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DataSources]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataSources](
	[DataSourceId] [smallint] IDENTITY(1,1) NOT NULL,
	[DataTypeId] [tinyint] NULL,
	[DataSourceName] [nvarchar](100) NULL,
	[DataSourceDesc] [nvarchar](100) NULL,
	[DisplayOrder] [smallint] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_DataSources] PRIMARY KEY CLUSTERED 
(
	[DataSourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DataTypes]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataTypes](
	[DataTypeId] [tinyint] IDENTITY(1,1) NOT NULL,
	[DataTypeName] [nvarchar](50) NULL,
	[DataTypeDesc] [nvarchar](50) NULL,
	[DisplayOrder] [tinyint] NULL,
	[FeatureGroupId] [smallint] NULL,
	[UrlType] [nvarchar](50) NULL,
 CONSTRAINT [PK_DataTypes] PRIMARY KEY CLUSTERED 
(
	[DataTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DisplayTypes]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DisplayTypes](
	[DisplayTypeId] [smallint] IDENTITY(1,1) NOT NULL,
	[DisplayTypeName] [nvarchar](100) NULL,
	[DisplayTypeDesc] [nvarchar](100) NULL,
	[DataTypeId] [tinyint] NULL,
	[PositionDisplayGroupId] [smallint] NULL,
	[RowDisplay] [tinyint] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_DisplayTypes] PRIMARY KEY CLUSTERED 
(
	[DisplayTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventStreamLanguages]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventStreamLanguages](
	[EventStreamLanguageId] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [tinyint] NULL,
	[ApplicationTypeId] [tinyint] NULL,
	[EventStreamId] [int] NULL,
	[EventStreamName] [nvarchar](200) NULL,
	[EventStreamDesc] [nvarchar](200) NULL,
	[ReviewStatusId] [tinyint] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_EventStreamLanguages] PRIMARY KEY CLUSTERED 
(
	[EventStreamLanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventStreams]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventStreams](
	[EventStreamId] [int] IDENTITY(1,1) NOT NULL,
	[EventStreamName] [nvarchar](200) NULL,
	[EventStreamDesc] [nvarchar](200) NULL,
	[SiteId] [smallint] NULL,
	[CategoryId] [smallint] NULL,
	[ReviewStatusId] [tinyint] NULL,
	[ImagePath] [nvarchar](255) NULL,
	[DisplayStartTime] [datetime] NULL,
	[DisplayEndTime] [datetime] NULL,
	[ShowTop] [tinyint] NULL,
	[ShowBottom] [tinyint] NULL,
	[ShowWeb] [tinyint] NULL,
	[ShowWap] [tinyint] NULL,
	[ShowApp] [tinyint] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_EventStreams] PRIMARY KEY CLUSTERED 
(
	[EventStreamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FeatureGroups]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeatureGroups](
	[FeatureGroupId] [smallint] IDENTITY(1,1) NOT NULL,
	[FeatureGroupName] [nvarchar](200) NULL,
	[FeatureGroupDesc] [nvarchar](200) NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_FeatureGroups] PRIMARY KEY CLUSTERED 
(
	[FeatureGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Features]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Features](
	[FeatureId] [smallint] IDENTITY(1,1) NOT NULL,
	[FeatureGroupId] [smallint] NULL,
	[FeatureName] [nvarchar](50) NULL,
	[FeatureDesc] [nvarchar](50) NULL,
	[ParentFeatureId] [smallint] NULL,
	[InputTypeId] [tinyint] NULL,
	[DataDictionaryTypeId] [smallint] NULL,
	[IconPath] [nvarchar](200) NULL,
	[DisplayOrder] [smallint] NULL,
	[IsData] [tinyint] NULL,
	[IsSearch] [tinyint] NULL,
	[IsDisplay] [tinyint] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_Features] PRIMARY KEY CLUSTERED 
(
	[FeatureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FeedBackGroups]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedBackGroups](
	[FeedBackGroupId] [smallint] IDENTITY(1,1) NOT NULL,
	[FeedBackGroupName] [nvarchar](200) NULL,
	[FeedBackGroupDesc] [nvarchar](200) NULL,
	[SiteId] [smallint] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_FeedBackGroups] PRIMARY KEY CLUSTERED 
(
	[FeedBackGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FeedBacks]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedBacks](
	[FeedBackId] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [smallint] NULL,
	[LanguageId] [tinyint] NULL,
	[ApplicationTypeId] [tinyint] NULL,
	[FeedBackGroupId] [smallint] NULL,
	[UserId] [int] NULL,
	[OrganName] [nvarchar](255) NULL,
	[FullName] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[Title] [nvarchar](255) NULL,
	[Comment] [nvarchar](2000) NULL,
	[RatingScore] [tinyint] NULL,
	[ReviewStatusId] [tinyint] NULL,
	[FromIP] [nvarchar](30) NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_FeedBacks] PRIMARY KEY CLUSTERED 
(
	[FeedBackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InputTypes]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InputTypes](
	[InputTypeId] [tinyint] IDENTITY(1,1) NOT NULL,
	[InputTypeName] [nvarchar](50) NULL,
	[InputTypeDesc] [nvarchar](50) NULL,
	[DisplayOrder] [tinyint] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_InputTypes] PRIMARY KEY CLUSTERED 
(
	[InputTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Languages]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[LanguageId] [tinyint] IDENTITY(1,1) NOT NULL,
	[LanguageName] [nvarchar](50) NULL,
	[LanguageDesc] [nvarchar](50) NULL,
	[LanguageCode] [nvarchar](50) NULL,
	[IconPath] [nvarchar](255) NULL,
	[DisplayOrder] [tinyint] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuItems]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuItems](
	[MenuItemId] [int] IDENTITY(1,1) NOT NULL,
	[MenuId] [int] NULL,
	[ItemName] [nvarchar](200) NULL,
	[ItemDesc] [nvarchar](200) NULL,
	[IconPath] [nvarchar](200) NULL,
	[Url] [nvarchar](200) NULL,
	[ParentItemId] [int] NULL,
	[ItemLevel] [tinyint] NULL,
	[DisplayOrder] [int] NULL,
	[ReviewStatusId] [tinyint] NULL,
	[CrUserId] [int] NULL,
	[MetaTitle] [nvarchar](200) NULL,
	[MetaDesc] [nvarchar](500) NULL,
	[MetaKeywords] [nvarchar](500) NULL,
	[CanonicalTag] [nvarchar](255) NULL,
	[H1Tag] [nvarchar](255) NULL,
	[SeoFooter] [nvarchar](2000) NULL,
	[SeoHeader] [nvarchar](2000) NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_MenuItems] PRIMARY KEY CLUSTERED 
(
	[MenuItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menus]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[MenuId] [smallint] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](200) NULL,
	[MenuDesc] [nvarchar](200) NULL,
	[SiteId] [smallint] NULL,
	[CategoryId] [smallint] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Seos]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seos](
	[SeoId] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [smallint] NULL,
	[SeoName] [nvarchar](200) NULL,
	[Url] [nvarchar](200) NULL,
	[MetaTitle] [nvarchar](200) NULL,
	[MetaDesc] [nvarchar](500) NULL,
	[MetaKeywords] [nvarchar](500) NULL,
	[CanonicalTag] [nvarchar](255) NULL,
	[H1Tag] [nvarchar](255) NULL,
	[SeoFooter] [nvarchar](255) NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_Seos] PRIMARY KEY CLUSTERED 
(
	[SeoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sites]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sites](
	[SiteId] [smallint] IDENTITY(1,1) NOT NULL,
	[SiteName] [nvarchar](255) NULL,
	[SiteDesc] [nvarchar](500) NULL,
	[MetaTitle] [nvarchar](255) NULL,
	[MetaDesc] [nvarchar](500) NULL,
	[MetaKeywords] [nvarchar](500) NULL,
	[MetaTagAll] [nvarchar](max) NULL,
	[CanonicalTag] [nvarchar](255) NULL,
	[H1Tag] [nvarchar](255) NULL,
	[SeoFooter] [nvarchar](255) NULL,
	[DisplayOrder] [smallint] NULL,
	[BuildIn] [tinyint] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_Sites] PRIMARY KEY CLUSTERED 
(
	[SiteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TagLanguages]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TagLanguages](
	[TagLanguageId] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [tinyint] NULL,
	[TagId] [int] NULL,
	[TagName] [nvarchar](100) NULL,
	[ReviewStatusId] [tinyint] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_TagLanguages] PRIMARY KEY CLUSTERED 
(
	[TagLanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tags]    Script Date: 06/07/2018 1:25:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[TagId] [int] IDENTITY(1,1) NOT NULL,
	[TagName] [nvarchar](100) NULL,
	[ReviewStatusId] [tinyint] NULL,
	[CrUserId] [int] NULL,
	[CrDateTime] [datetime] NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AdvertPositionAdverts] ADD  CONSTRAINT [DF_AdvertPositionAdverts_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[AdvertPositions] ADD  CONSTRAINT [DF_AdvertPositions_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[Adverts] ADD  CONSTRAINT [DF_Adverts_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[AdvertViewLogs] ADD  CONSTRAINT [DF_AdvertViewLogs_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[ArticleCategories] ADD  CONSTRAINT [DF_ArticleCategories_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[ArticleComments] ADD  CONSTRAINT [DF_ArticleComments_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[ArticleDisplays] ADD  CONSTRAINT [DF_ArticleDisplays_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[ArticleEventStreams] ADD  CONSTRAINT [DF_ArticleEventStreams_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[ArticleFeatures] ADD  CONSTRAINT [DF_ArticleFeatures_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[ArticleLanguages] ADD  CONSTRAINT [DF_ArticleLanguages_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[ArticleLocations] ADD  CONSTRAINT [DF_Table_1_CrDatetime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[ArticleMedias] ADD  CONSTRAINT [DF_ArticleMedias_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[ArticleRateDetails] ADD  CONSTRAINT [DF_ArticleRateDetails_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[ArticleRelates] ADD  CONSTRAINT [DF_ArticleRelates_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[Articles] ADD  CONSTRAINT [DF_Articles_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[ArticleTags] ADD  CONSTRAINT [DF_ArticleTags_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[ArticleTypes] ADD  CONSTRAINT [DF_ArticleTypes_DateTime]  DEFAULT (getdate()) FOR [DateTime]
GO
ALTER TABLE [dbo].[ArticleViewCount] ADD  CONSTRAINT [DF_ArticleViewCount_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[ArticleViewLogs] ADD  CONSTRAINT [DF_ArticleViewLogs_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [DF_Categories_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[CategoryDisplays] ADD  CONSTRAINT [DF_CategoryDisplays_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[CategoryLanguages] ADD  CONSTRAINT [DF_CategoryLanguages_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[DataDictionaries] ADD  CONSTRAINT [DF_DataDictionaries_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[DataDictionaryTypes] ADD  CONSTRAINT [DF_DataDictionaryTypes_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[DataSources] ADD  CONSTRAINT [DF_DataSources_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[DisplayTypes] ADD  CONSTRAINT [DF_DisplayTypes_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[EventStreamLanguages] ADD  CONSTRAINT [DF_EventStreamLanguages_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[EventStreams] ADD  CONSTRAINT [DF_EventStreams_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[FeatureGroups] ADD  CONSTRAINT [DF_FeatureGroups_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[Features] ADD  CONSTRAINT [DF_Features_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[FeedBackGroups] ADD  CONSTRAINT [DF_FeedBackGroups_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[FeedBacks] ADD  CONSTRAINT [DF_FeedBacks_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[InputTypes] ADD  CONSTRAINT [DF_InputTypes_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[Languages] ADD  CONSTRAINT [DF_Languages_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[MenuItems] ADD  CONSTRAINT [DF_MenuItems_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[Menus] ADD  CONSTRAINT [DF_Menus_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[Seos] ADD  CONSTRAINT [DF_Seos_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[Sites] ADD  CONSTRAINT [DF_Sites_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[TagLanguages] ADD  CONSTRAINT [DF_TagLanguages_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[Tags] ADD  CONSTRAINT [DF_Tags_CrDateTime]  DEFAULT (getdate()) FOR [CrDateTime]
GO
ALTER TABLE [dbo].[AdvertPositionAdverts]  WITH CHECK ADD  CONSTRAINT [FK_AdvertPositionAdverts_AdvertPositions] FOREIGN KEY([AdvertPositionId])
REFERENCES [dbo].[AdvertPositions] ([AdvertPositionId])
GO
ALTER TABLE [dbo].[AdvertPositionAdverts] CHECK CONSTRAINT [FK_AdvertPositionAdverts_AdvertPositions]
GO
ALTER TABLE [dbo].[AdvertPositionAdverts]  WITH CHECK ADD  CONSTRAINT [FK_AdvertPositionAdverts_Adverts] FOREIGN KEY([AdvertId])
REFERENCES [dbo].[Adverts] ([AdvertId])
GO
ALTER TABLE [dbo].[AdvertPositionAdverts] CHECK CONSTRAINT [FK_AdvertPositionAdverts_Adverts]
GO
ALTER TABLE [dbo].[AdvertPositions]  WITH CHECK ADD  CONSTRAINT [FK_AdvertPositions_AdvertDisplayTypes] FOREIGN KEY([AdvertDisplayTypeId])
REFERENCES [dbo].[AdvertDisplayTypes] ([AdvertDisplayTypeId])
GO
ALTER TABLE [dbo].[AdvertPositions] CHECK CONSTRAINT [FK_AdvertPositions_AdvertDisplayTypes]
GO
ALTER TABLE [dbo].[Adverts]  WITH CHECK ADD  CONSTRAINT [FK_Adverts_AdvertContentTypes] FOREIGN KEY([AdvertContentTypeId])
REFERENCES [dbo].[AdvertContentTypes] ([AdvertContentTypeId])
GO
ALTER TABLE [dbo].[Adverts] CHECK CONSTRAINT [FK_Adverts_AdvertContentTypes]
GO
ALTER TABLE [dbo].[Adverts]  WITH CHECK ADD  CONSTRAINT [FK_Adverts_AdvertStatus] FOREIGN KEY([AdvertStatusId])
REFERENCES [dbo].[AdvertStatus] ([AdvertStatusId])
GO
ALTER TABLE [dbo].[Adverts] CHECK CONSTRAINT [FK_Adverts_AdvertStatus]
GO
ALTER TABLE [dbo].[ArticleCategories]  WITH CHECK ADD  CONSTRAINT [FK_ArticleCategories_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[ArticleCategories] CHECK CONSTRAINT [FK_ArticleCategories_Articles]
GO
ALTER TABLE [dbo].[ArticleCategories]  WITH CHECK ADD  CONSTRAINT [FK_ArticleCategories_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[ArticleCategories] CHECK CONSTRAINT [FK_ArticleCategories_Categories]
GO
ALTER TABLE [dbo].[ArticleComments]  WITH CHECK ADD  CONSTRAINT [FK_ArticleComments_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[ArticleComments] CHECK CONSTRAINT [FK_ArticleComments_Articles]
GO
ALTER TABLE [dbo].[ArticleDisplays]  WITH CHECK ADD  CONSTRAINT [FK_ArticleDisplays_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[ArticleDisplays] CHECK CONSTRAINT [FK_ArticleDisplays_Articles]
GO
ALTER TABLE [dbo].[ArticleEventStreams]  WITH CHECK ADD  CONSTRAINT [FK_ArticleEventStreams_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[ArticleEventStreams] CHECK CONSTRAINT [FK_ArticleEventStreams_Articles]
GO
ALTER TABLE [dbo].[ArticleEventStreams]  WITH CHECK ADD  CONSTRAINT [FK_ArticleEventStreams_EventStreams] FOREIGN KEY([EventStreamId])
REFERENCES [dbo].[EventStreams] ([EventStreamId])
GO
ALTER TABLE [dbo].[ArticleEventStreams] CHECK CONSTRAINT [FK_ArticleEventStreams_EventStreams]
GO
ALTER TABLE [dbo].[ArticleFeatures]  WITH CHECK ADD  CONSTRAINT [FK_ArticleFeatures_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[ArticleFeatures] CHECK CONSTRAINT [FK_ArticleFeatures_Articles]
GO
ALTER TABLE [dbo].[ArticleFeatures]  WITH CHECK ADD  CONSTRAINT [FK_ArticleFeatures_Features] FOREIGN KEY([FeatureId])
REFERENCES [dbo].[Features] ([FeatureId])
GO
ALTER TABLE [dbo].[ArticleFeatures] CHECK CONSTRAINT [FK_ArticleFeatures_Features]
GO
ALTER TABLE [dbo].[ArticleLanguages]  WITH CHECK ADD  CONSTRAINT [FK_ArticleLanguages_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[ArticleLanguages] CHECK CONSTRAINT [FK_ArticleLanguages_Articles]
GO
ALTER TABLE [dbo].[ArticleLocations]  WITH CHECK ADD  CONSTRAINT [FK_ArticleLocations_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[ArticleLocations] CHECK CONSTRAINT [FK_ArticleLocations_Articles]
GO
ALTER TABLE [dbo].[ArticleMedias]  WITH CHECK ADD  CONSTRAINT [FK_ArticleMedias_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[ArticleMedias] CHECK CONSTRAINT [FK_ArticleMedias_Articles]
GO
ALTER TABLE [dbo].[ArticlePublics]  WITH CHECK ADD  CONSTRAINT [FK_ArticlePublics_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[ArticlePublics] CHECK CONSTRAINT [FK_ArticlePublics_Articles]
GO
ALTER TABLE [dbo].[ArticleRateDetails]  WITH CHECK ADD  CONSTRAINT [FK_ArticleRateDetails_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[ArticleRateDetails] CHECK CONSTRAINT [FK_ArticleRateDetails_Articles]
GO
ALTER TABLE [dbo].[ArticleRelates]  WITH CHECK ADD  CONSTRAINT [FK_ArticleRelates_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[ArticleRelates] CHECK CONSTRAINT [FK_ArticleRelates_Articles]
GO
ALTER TABLE [dbo].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_Articles_ArticleTypes] FOREIGN KEY([ArticleTypeId])
REFERENCES [dbo].[ArticleTypes] ([ArticleTypeId])
GO
ALTER TABLE [dbo].[Articles] CHECK CONSTRAINT [FK_Articles_ArticleTypes]
GO
ALTER TABLE [dbo].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_Articles_Sites] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Sites] ([SiteId])
GO
ALTER TABLE [dbo].[Articles] CHECK CONSTRAINT [FK_Articles_Sites]
GO
ALTER TABLE [dbo].[ArticleTags]  WITH CHECK ADD  CONSTRAINT [FK_ArticleTags_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[ArticleTags] CHECK CONSTRAINT [FK_ArticleTags_Articles]
GO
ALTER TABLE [dbo].[ArticleTags]  WITH CHECK ADD  CONSTRAINT [FK_ArticleTags_Tags] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([TagId])
GO
ALTER TABLE [dbo].[ArticleTags] CHECK CONSTRAINT [FK_ArticleTags_Tags]
GO
ALTER TABLE [dbo].[ArticleViewCount]  WITH CHECK ADD  CONSTRAINT [FK_ArticleViewCount_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[ArticleViewCount] CHECK CONSTRAINT [FK_ArticleViewCount_Articles]
GO
ALTER TABLE [dbo].[ArticleViewLogs]  WITH CHECK ADD  CONSTRAINT [FK_ArticleViewLogs_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[ArticleViewLogs] CHECK CONSTRAINT [FK_ArticleViewLogs_Articles]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Categories_Sites] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Sites] ([SiteId])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_Sites]
GO
ALTER TABLE [dbo].[CategoryLanguages]  WITH CHECK ADD  CONSTRAINT [FK_CategoryLanguages_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[CategoryLanguages] CHECK CONSTRAINT [FK_CategoryLanguages_Categories]
GO
ALTER TABLE [dbo].[DataDictionaries]  WITH CHECK ADD  CONSTRAINT [FK_DataDictionaries_DataDictionaryTypes] FOREIGN KEY([DataDictionaryTypeId])
REFERENCES [dbo].[DataDictionaryTypes] ([DataDictionaryTypeId])
GO
ALTER TABLE [dbo].[DataDictionaries] CHECK CONSTRAINT [FK_DataDictionaries_DataDictionaryTypes]
GO
ALTER TABLE [dbo].[DisplayTypes]  WITH CHECK ADD  CONSTRAINT [FK_DisplayTypes_DataTypes] FOREIGN KEY([DataTypeId])
REFERENCES [dbo].[DataTypes] ([DataTypeId])
GO
ALTER TABLE [dbo].[DisplayTypes] CHECK CONSTRAINT [FK_DisplayTypes_DataTypes]
GO
ALTER TABLE [dbo].[EventStreamLanguages]  WITH CHECK ADD  CONSTRAINT [FK_EventStreamLanguages_EventStreams] FOREIGN KEY([EventStreamId])
REFERENCES [dbo].[EventStreams] ([EventStreamId])
GO
ALTER TABLE [dbo].[EventStreamLanguages] CHECK CONSTRAINT [FK_EventStreamLanguages_EventStreams]
GO
ALTER TABLE [dbo].[Features]  WITH CHECK ADD  CONSTRAINT [FK_Features_DataDictionaryTypes] FOREIGN KEY([DataDictionaryTypeId])
REFERENCES [dbo].[DataDictionaryTypes] ([DataDictionaryTypeId])
GO
ALTER TABLE [dbo].[Features] CHECK CONSTRAINT [FK_Features_DataDictionaryTypes]
GO
ALTER TABLE [dbo].[Features]  WITH CHECK ADD  CONSTRAINT [FK_Features_FeatureGroups] FOREIGN KEY([FeatureGroupId])
REFERENCES [dbo].[FeatureGroups] ([FeatureGroupId])
GO
ALTER TABLE [dbo].[Features] CHECK CONSTRAINT [FK_Features_FeatureGroups]
GO
ALTER TABLE [dbo].[Features]  WITH CHECK ADD  CONSTRAINT [FK_Features_InputTypes] FOREIGN KEY([InputTypeId])
REFERENCES [dbo].[InputTypes] ([InputTypeId])
GO
ALTER TABLE [dbo].[Features] CHECK CONSTRAINT [FK_Features_InputTypes]
GO
ALTER TABLE [dbo].[TagLanguages]  WITH CHECK ADD  CONSTRAINT [FK_TagLanguages_Tags] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([TagId])
GO
ALTER TABLE [dbo].[TagLanguages] CHECK CONSTRAINT [FK_TagLanguages_Tags]
GO
USE [master]
GO
ALTER DATABASE [hCMS] SET  READ_WRITE 
GO
