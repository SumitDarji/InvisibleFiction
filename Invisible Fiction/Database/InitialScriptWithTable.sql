USE JewellerShop
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'TAdminUser') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE TAdminUser
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'TCode') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE TCode
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'TCodeType') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE TCodeType
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'TCompany') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE TCompany
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'TBranch') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE TBranch
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'TMaterialCategory') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE TMaterialCategory
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'TCategory') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE TCategory
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'TOrnamentPosition') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE TOrnamentPosition
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'TPosition') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE TPosition
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'TImages') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE TImages
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'TOrnament') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE TOrnament
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'ImageList') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP Type ImageList
GO

/****** Object:  Table [TAdminUser]    Script Date: 08/15/2020 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [TAdminUser](
	[AdminUserID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[Mobile] [nvarchar](20) NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](100) NULL,
	[Status] [int] NOT NULL,
	[LastLogged] [smalldatetime] NULL,
	[LoginBeforeLastLogged] [smalldatetime] NULL,
	[Created] [smalldatetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [smalldatetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
 CONSTRAINT [TAdminUser_pk] PRIMARY KEY CLUSTERED 
(
	[AdminUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [TCode]    Script Date:Script Date: 08/15/2020 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [TCode](
	[CodeID] [int] NOT NULL,
	[CodeTypeId] [int] NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[Ref] [nvarchar](255) NULL,
 CONSTRAINT [TCode_pk] PRIMARY KEY CLUSTERED 
(
	[CodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [TCodeType]  Script Date: 08/15/2020 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [TCodeType](
	[CodeTypeID] [int] NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
 CONSTRAINT [TCodeType_pk] PRIMARY KEY CLUSTERED 
(
	[CodeTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [TAdminUser]    Script Date: 08/15/2020 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [TCompany](
	[CompanyID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Address] [nvarchar](MAX) NULL,
	[Email] [nvarchar](255) NULL,
	[Mobile] [nvarchar](20) NULL,
	[LogoImage] [nvarchar](255) NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](100) NULL,
	[Status] [int] NOT NULL,
	[LastLogged] [smalldatetime] NULL,
	[LoginBeforeLastLogged] [smalldatetime] NULL,
	[Created] [smalldatetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [smalldatetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[AdminUserID] [int] NOT NULL
 CONSTRAINT [TCompany_pk] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [TAdminUser]    Script Date: 08/15/2020 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [TBranch](
	[BranchID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Address] [nvarchar](MAX) NULL,
	[Location] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[Mobile] [nvarchar](20) NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](100) NULL,
	[Status] [int] NOT NULL,
	[LastLogged] [smalldatetime] NULL,
	[LoginBeforeLastLogged] [smalldatetime] NULL,
	[Created] [smalldatetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [smalldatetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[CompanyID] [int] NOT NULL,
	[ModifiedSourceCode][int] NOT NULL
 CONSTRAINT [TBranch_pk] PRIMARY KEY CLUSTERED 
(
	[BranchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [TAdminUser]    Script Date: 08/15/2020 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [TCategory](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[LogoImage] [nvarchar](255) NULL,
	[Created] [smalldatetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [smalldatetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[CreatedSourceCode][int] NOT NULL,
	[ModifiedSourceCode][int] NOT NULL
 CONSTRAINT [TCategory_pk] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [TAdminUser]    Script Date: 08/15/2020 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [TPosition](
	[PositionID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[LogoImage] [nvarchar](255) NULL,
	[Created] [smalldatetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [smalldatetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[CreatedSourceCode][int] NOT NULL,
	[ModifiedSourceCode][int] NOT NULL,
	[CodeID] [int] NOT NULL
 CONSTRAINT [TPosition_pk] PRIMARY KEY CLUSTERED 
(
	[PositionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [TOrnament]    Script Date: 24/08/2020 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [TOrnament](
	[OrnamentID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[CategoryID] [INT] NULL,
	[PositionID] [INT] NULL,
	[Weight] [nvarchar](255) NULL,
	[Cost] [decimal](22,2) NULL,
	[Created] [smalldatetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Modified] [smalldatetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[CreatedSourceCode][int] NOT NULL,
	[ModifiedSourceCode][int] NOT NULL
 CONSTRAINT [TOrnament_pk] PRIMARY KEY CLUSTERED 
(
	[OrnamentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE TImages(
	ImageID int IDENTITY(1,1) NOT NULL,
	OrnamentID int NOT NULL,	
	ImgPath varchar(255),
	Created	smalldatetime  NOT NULL,
	CreatedBy int  NOT NULL,
	Modified smalldatetime  NOT NULL,
	ModifiedBy int  NOT NULL,
	CONSTRAINT TImages_pk primary key(ImageID),		
	CONSTRAINT TImages_OrnamentID_fk foreign key(OrnamentID) references TOrnament(OrnamentID)
)
GO

--CREATE TYPE ImageList AS TABLE 
--(
--	Id int, 
--	ImgPath varchar(255)
--);

SET IDENTITY_INSERT [TAdminUser] ON 

INSERT [TAdminUser] ([AdminUserID], [FirstName], [LastName], [Email], [Mobile], [Username], [Password], [Status], [LastLogged], [LoginBeforeLastLogged], [Created], [CreatedBy], [Modified], [ModifiedBy]) 
VALUES (1, N'Master', N'Admin', N'sumitdarji1991@gmail.com', N'9586069494', N'admin', N'M//EekBeJOtNs7V+Sp0xBA{a61}{a61}', 6, CAST(N'2020-08-22 18:06:00' AS SmallDateTime), CAST(N'2020-08-24 12:07:00' AS SmallDateTime), CAST(N'2020-08-22 18:30:00' AS SmallDateTime), 1, CAST(N'2020-08-22 18:30:00' AS SmallDateTime), 1)
SET IDENTITY_INSERT [TAdminUser] OFF

SET IDENTITY_INSERT [TAdminUser] OFF

INSERT [TCodeType] ([CodeTypeID], [Description]) VALUES (0, N'N/A')
INSERT [TCodeType] ([CodeTypeID], [Description]) VALUES (1, N'Gender Type Code')
INSERT [TCodeType] ([CodeTypeID], [Description]) VALUES (2, N'Admin User Privileges Code')
INSERT [TCodeType] ([CodeTypeID], [Description]) VALUES (3, N'Admin/User Account Status Code')
INSERT [TCodeType] ([CodeTypeID], [Description]) VALUES (4, N'Application User Type Code')
INSERT [TCodeType] ([CodeTypeID], [Description]) VALUES (5, N'Category')

INSERT [TCode] ([CodeID], [CodeTypeId], [Description], [Ref]) VALUES (0, 0, N'N/A', NULL)
INSERT [TCode] ([CodeID], [CodeTypeId], [Description], [Ref]) VALUES (1, 1, N'Female', NULL)
INSERT [TCode] ([CodeID], [CodeTypeId], [Description], [Ref]) VALUES (2, 1, N'Male', NULL)
INSERT [TCode] ([CodeID], [CodeTypeId], [Description], [Ref]) VALUES (3, 2, N'Administrator', NULL)
INSERT [TCode] ([CodeID], [CodeTypeId], [Description], [Ref]) VALUES (4, 2, N'Basic User', NULL)
INSERT [TCode] ([CodeID], [CodeTypeId], [Description], [Ref]) VALUES (5, 2, N'Super Administrator', NULL)
INSERT [TCode] ([CodeID], [CodeTypeId], [Description], [Ref]) VALUES (6, 3, N'Active', NULL)
INSERT [TCode] ([CodeID], [CodeTypeId], [Description], [Ref]) VALUES (7, 3, N'Deleted', NULL)
INSERT [TCode] ([CodeID], [CodeTypeId], [Description], [Ref]) VALUES (8, 3, N'InActive', NULL)
INSERT [TCode] ([CodeID], [CodeTypeId], [Description], [Ref]) VALUES (9, 3, N'Suspended', NULL)
INSERT [TCode] ([CodeID], [CodeTypeId], [Description], [Ref]) VALUES (10, 4, N'Admin User', NULL)
INSERT [TCode] ([CodeID], [CodeTypeId], [Description], [Ref]) VALUES (11, 4, N'Application User', NULL)
INSERT [TCode] ([CodeID], [CodeTypeId], [Description], [Ref]) VALUES (17, 5, N'Marker Base', NULL)
INSERT [TCode] ([CodeID], [CodeTypeId], [Description], [Ref]) VALUES (18, 5, N'Marker Less', NULL)