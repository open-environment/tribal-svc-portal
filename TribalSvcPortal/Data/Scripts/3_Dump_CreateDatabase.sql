﻿/***************************************************************** */
/*************DROP EXISTING DATABASE (only use if refreshing DB*** */
/***************************************************************** */
/*
	USE [TRIBAL_SVC_PORTAL];

	drop table [T_OD_DUMP_ASSESSMENT_CONTENT];
	drop table [T_OD_DUMP_ASSESSMENT_DOCS];
	drop table [T_OD_DUMP_ASSESSMENTS] ;
	drop table [T_OD_SITES];
	drop table [T_OD_REF_WASTE_TYPE];
	drop table [T_OD_REF_WASTE_TYPE_CAT_CLEANUP];
	drop table [T_OD_REF_WASTE_TYPE_CAT];
	drop table [T_OD_REF_CLEANUP_ASSETS];
	drop table [T_OD_REF_THREAT_FACTORS];
	drop table [T_OD_REF_DATA];  
	drop table [T_OD_REF_DATA_CATEGORIES];


--BUILD SCAFFOLDING script (Open Packages Manager (under tools) and run this command:)
Scaffold-DbContext -UseDatabaseNames "Server=.\SQLEXPRESS;Database=TRIBAL_SVC_PORTAL;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data/Models -t T_OD_DUMP_ASSESSMENT_CONTENT, T_OD_DUMP_ASSESSMENT_DOCS, T_OD_DUMP_ASSESSMENTS, T_OD_SITES, T_OD_REF_WASTE_TYPE, T_OD_REF_WASTE_TYPE_CAT_CLEANUP, T_OD_REF_WASTE_TYPE_CAT, T_OD_REF_CLEANUP_ASSETS, T_OD_REF_THREAT_FACTORS, T_OD_REF_DATA, T_OD_REF_DATA_CATEGORIES  -f -Context "ApplicationDbContextTemp"
*/

use [TRIBAL_SVC_PORTAL]


CREATE TABLE [dbo].[T_OD_REF_DATA_CATEGORIES](
	[REF_DATA_CAT_NAME] [VARCHAR](50) NOT NULL,
	[REF_DATA_CAT_DESC] [varchar](200) NULL,	
 CONSTRAINT [PK_T_OD_REF_DATA_CATEGORIES] PRIMARY KEY CLUSTERED  ([REF_DATA_CAT_NAME] ASC)
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[T_OD_REF_DATA](
	[REF_DATA_IDX] [UNIQUEIDENTIFIER] NOT NULL DEFAULT NEWID(),
	[REF_DATA_CAT_NAME] [VARCHAR](50) NOT NULL,
	[REF_DATA_VAL] [VARCHAR](100) NOT NULL,
	[REF_DATA_DESC] [VARCHAR](300) NULL,
	[ORG_ID] varchar(30) NULL,
	[CREATE_USER_ID] nvarchar(450) NULL,
	[CREATE_DT] datetime2(0) NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
 CONSTRAINT [PK_T_OD_REF_DATA] PRIMARY KEY CLUSTERED  ([REF_DATA_IDX] ASC),
 FOREIGN KEY ([REF_DATA_CAT_NAME]) REFERENCES [T_OD_REF_DATA_CATEGORIES] ([REF_DATA_CAT_NAME])
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[T_OD_REF_THREAT_FACTORS](
	[THREAT_FACTOR_IDX] [UNIQUEIDENTIFIER] NOT NULL DEFAULT NEWID(),
	[THREAT_FACTOR_TYPE] [VARCHAR](50) NOT NULL,
	[THREAT_FACTOR_NAME] [VARCHAR](75) NOT NULL,
	[THREAT_FACTOR_SCORE] [INT] NULL,
 CONSTRAINT [PK_T_OD_REF_THREAT_FACTORS] PRIMARY KEY CLUSTERED  ([THREAT_FACTOR_IDX] ASC)
) ON [PRIMARY]

GO

--stores the types of cleanup assets
CREATE TABLE [dbo].[T_OD_REF_CLEANUP_ASSETS](
	[REF_ASSET_NAME] [VARCHAR](100) NOT NULL
 CONSTRAINT [PK_T_OD_REF_CLEANUP_ASSETS] PRIMARY KEY CLUSTERED  ([REF_ASSET_NAME] ASC)
) ON [PRIMARY]


--stores the categories of wastes found at a dump
CREATE TABLE [dbo].[T_OD_REF_WASTE_TYPE_CAT](
	[REF_WASTE_TYPE_CAT] [VARCHAR](100) NOT NULL,
 CONSTRAINT [PK_T_OD_REF_WASTE_TYPE_CAT] PRIMARY KEY CLUSTERED  ([REF_WASTE_TYPE_CAT] ASC)
) ON [PRIMARY]


--stores the default assets and cleanup rates for a category of waste
CREATE TABLE [dbo].[T_OD_REF_WASTE_TYPE_CAT_CLEANUP](
	[REF_WASTE_TYPE_CAT_CLEANUP_IDX] [UNIQUEIDENTIFIER] NOT NULL DEFAULT NEWID(),
	[REF_WASTE_TYPE_CAT] [VARCHAR](100) NOT NULL,
	[REF_ASSET_NAME] [VARCHAR](100) NOT NULL,
	[PROCESS_RATE_PER_HR] [DECIMAL](10,2) NULL,
	[PROCESS_RATE_UNIT] [VARCHAR](20)  NULL,
	[ASSET_HOURLY_RATE] [DECIMAL](10,2) NULL,
	[ASSET_COUNT] [int] NULL,
	[PER_UNIT_IND] [bit] NULL DEFAULT 0, 
	[ORG_ID] varchar(30) NULL,
 CONSTRAINT [PK_T_OD_REF_WASTE_TYPE_CAT_CLEANUP] PRIMARY KEY CLUSTERED  ([REF_WASTE_TYPE_CAT_CLEANUP_IDX] ASC),
 CONSTRAINT [FK_T_OD_REF_WASTE_TYPE_CAT_CLEANUP_C] FOREIGN KEY ([REF_WASTE_TYPE_CAT]) REFERENCES [T_OD_REF_WASTE_TYPE_CAT] ([REF_WASTE_TYPE_CAT]),
 CONSTRAINT [FK_T_OD_REF_WASTE_TYPE_CAT_CLEANUP_A] FOREIGN KEY ([REF_ASSET_NAME]) REFERENCES [T_OD_REF_CLEANUP_ASSETS] ([REF_ASSET_NAME])
) ON [PRIMARY]



CREATE TABLE [dbo].[T_OD_REF_WASTE_TYPE](
	[REF_WASTE_TYPE_IDX] [UNIQUEIDENTIFIER] NOT NULL DEFAULT NEWID(),
	[REF_WASTE_TYPE_NAME] [VARCHAR](100) NOT NULL,
	[REF_WASTE_TYPE_CAT] [VARCHAR](100) NOT NULL,
	[REF_WASTE_HAZFACT_SUBSCORE] [INT] NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
 CONSTRAINT [PK_T_OD_REF_WASTE_TYPE] PRIMARY KEY CLUSTERED  ([REF_WASTE_TYPE_IDX] ASC)
) ON [PRIMARY]

GO


CREATE TABLE [T_OD_SITES] (
	[SITE_IDX] uniqueidentifier NOT NULL,
	[REPORTED_BY] varchar(50) NULL,
	[REPORTED_ON] datetime2(0) NULL,
	[COMMUNITY_IDX] uniqueidentifier NULL,
	[SITE_SETTING_IDX] uniqueidentifier NULL,
	[PF_AQUIFER_VERT_DIST] uniqueidentifier NULL,
	[PF_SURF_WATER_HORIZ_DIST] uniqueidentifier NULL,
	[PF_HOMES_DIST] uniqueidentifier NULL,
    CONSTRAINT [PK_T_OD_SITE_DTL] PRIMARY KEY ([SITE_IDX]),
    CONSTRAINT [FK_T_OD_SITE_DTL_S] FOREIGN KEY ([SITE_IDX]) REFERENCES [T_PRT_SITES] ([SITE_IDX]) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT [FK_T_OD_SITE_DTL_C] FOREIGN KEY ([COMMUNITY_IDX]) REFERENCES [T_OD_REF_DATA] ([REF_DATA_IDX]),
    CONSTRAINT [FK_T_OD_SITE_DTL_SS] FOREIGN KEY ([SITE_SETTING_IDX]) REFERENCES [T_OD_REF_DATA] ([REF_DATA_IDX]),
    CONSTRAINT [FK_T_OD_SITE_DTL_AD] FOREIGN KEY ([PF_AQUIFER_VERT_DIST]) REFERENCES [T_OD_REF_THREAT_FACTORS] ([THREAT_FACTOR_IDX]),
    CONSTRAINT [FK_T_OD_SITE_DTL_SD] FOREIGN KEY ([PF_SURF_WATER_HORIZ_DIST]) REFERENCES [T_OD_REF_THREAT_FACTORS] ([THREAT_FACTOR_IDX]),
    CONSTRAINT [FK_T_OD_SITE_DTL_HD] FOREIGN KEY ([PF_HOMES_DIST]) REFERENCES [T_OD_REF_THREAT_FACTORS] ([THREAT_FACTOR_IDX])
);
GO



CREATE TABLE [T_OD_DUMP_ASSESSMENTS] 
(
	[DUMP_ASSESSMENTS_IDX] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[SITE_IDX] uniqueidentifier NOT NULL,
	[ASSESSMENT_DT] datetime2(0) NOT NULL,
	[ASSESSED_BY] nvarchar(100) NULL,
	[ASSESSMENT_TYPE_IDX] uniqueidentifier NULL,

	[ACTIVE_SITE_IND] bit NOT NULL,
--	[SITE_GEOGRAPHY] varchar(100) NULL,
--	[NO_OF_STRUCTURES] varchar(100) NULL,
--	[SIGNAGE] varchar(100) NULL,
--	[MAINTAINED_IND] bit NOT NULL DEFAULT 0,

	[AREA_ACRES] decimal(8,2) NULL,
	[VOLUME_CU_YD] decimal(10,2) NULL,

	[HF_RAINFALL] uniqueidentifier NULL,
	[HF_DRAINAGE] uniqueidentifier NULL,
	[HF_FLOODING] uniqueidentifier NULL,
	[HF_BURNING] uniqueidentifier NULL,
	[HF_FENCING] uniqueidentifier NULL,
	[HF_ACCESS_CONTROL] uniqueidentifier NULL,
	[HF_PUBLIC_CONCERN] uniqueidentifier NULL,
	[HEALTH_THREAT_SCORE] int NULL,



	[SITE_DESCRIPTION] nvarchar(max) NULL,
	[ASSESSMENT_NOTES] nvarchar(max) NULL,
	[CREATE_USER_ID] nvarchar(450) NULL,
	[CREATE_DT] datetime2(0) NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
    CONSTRAINT [PK_T_OD_DUMP_ASSESSMENTS] PRIMARY KEY ([DUMP_ASSESSMENTS_IDX]),
    CONSTRAINT [FK_T_OD_DUMP_ASSESSMENTS_S] FOREIGN KEY ([SITE_IDX]) REFERENCES [T_OD_SITES] ([SITE_IDX]),
   -- CONSTRAINT [FK_T_OD_DUMP_ASSESSMENTS_I] FOREIGN KEY ([ASSESSED_BY]) REFERENCES [T_PRT_USERS] ([Id]),
    CONSTRAINT [FK_T_OD_DUMP_ASSESSMENTS_D] FOREIGN KEY ([ASSESSMENT_TYPE_IDX]) REFERENCES [T_OD_REF_DATA] ([REF_DATA_IDX]),
    CONSTRAINT [FK_T_OD_DUMP_ASSESSMENTS_HFR] FOREIGN KEY ([HF_RAINFALL]) REFERENCES [T_OD_REF_THREAT_FACTORS] ([THREAT_FACTOR_IDX]),
    CONSTRAINT [FK_T_OD_DUMP_ASSESSMENTS_HFD] FOREIGN KEY ([HF_DRAINAGE]) REFERENCES [T_OD_REF_THREAT_FACTORS] ([THREAT_FACTOR_IDX]),
    CONSTRAINT [FK_T_OD_DUMP_ASSESSMENTS_HFF] FOREIGN KEY ([HF_FLOODING]) REFERENCES [T_OD_REF_THREAT_FACTORS] ([THREAT_FACTOR_IDX]),
    CONSTRAINT [FK_T_OD_DUMP_ASSESSMENTS_HFB] FOREIGN KEY ([HF_BURNING]) REFERENCES [T_OD_REF_THREAT_FACTORS] ([THREAT_FACTOR_IDX]),
    CONSTRAINT [FK_T_OD_DUMP_ASSESSMENTS_HFFN] FOREIGN KEY ([HF_FENCING]) REFERENCES [T_OD_REF_THREAT_FACTORS] ([THREAT_FACTOR_IDX]),
    CONSTRAINT [FK_T_OD_DUMP_ASSESSMENTS_HFA] FOREIGN KEY ([HF_ACCESS_CONTROL]) REFERENCES [T_OD_REF_THREAT_FACTORS] ([THREAT_FACTOR_IDX]),
    CONSTRAINT [FK_T_OD_DUMP_ASSESSMENTS_HFP] FOREIGN KEY ([HF_PUBLIC_CONCERN]) REFERENCES [T_OD_REF_THREAT_FACTORS] ([THREAT_FACTOR_IDX])
);

 --ALTER TABLE [T_OD_DUMP_ASSESSMENTS] DROP CONSTRAINT FK_T_OD_DUMP_ASSESSMENTS_I;
 --ALTER TABLE [T_OD_DUMP_ASSESSMENTS] ALTER COLUMN [ASSESSED_BY] nvarchar(100) NULL;

CREATE TABLE [T_OD_DUMP_ASSESSMENT_DOCS] 
(
	[DUMP_ASSESSMENTS_IDX] uniqueidentifier NOT NULL,
	[DOC_IDX] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_T_OD_DUMP_ASSESS_DOCS] PRIMARY KEY ([DUMP_ASSESSMENTS_IDX], [DOC_IDX]),
    CONSTRAINT [FK_T_OD_DUMP_ASSESS_DOCS_A] FOREIGN KEY ([DUMP_ASSESSMENTS_IDX]) REFERENCES [T_OD_DUMP_ASSESSMENTS] ([DUMP_ASSESSMENTS_IDX]),
    CONSTRAINT [FK_T_OD_DUMP_ASSESS_DOCS_D] FOREIGN KEY ([DOC_IDX]) REFERENCES [T_PRT_DOCUMENTS] ([DOC_IDX])
);


CREATE TABLE [T_OD_DUMP_ASSESSMENT_CONTENT] 
(
	[DUMP_ASSESSMENTS_CONTENT_IDX] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[DUMP_ASSESSMENTS_IDX] uniqueidentifier NOT NULL,
	[REF_WASTE_TYPE_IDX] uniqueidentifier NOT NULL,
	[WASTE_AMT] decimal(10,2) NULL,
	[WASTE_UNIT_MSR] uniqueidentifier NULL,
	[WASTE_DISPOSAL_METHOD] uniqueidentifier NULL,
	[WASTE_DISPOSAL_DIST] varchar(4) NULL,
    CONSTRAINT [PK_T_OD_DUMP_ASSESS_CONTENT] PRIMARY KEY ([DUMP_ASSESSMENTS_CONTENT_IDX]),
    CONSTRAINT [FK_T_OD_DUMP_ASSESS_CNT_A] FOREIGN KEY ([DUMP_ASSESSMENTS_IDX]) REFERENCES [T_OD_DUMP_ASSESSMENTS] ([DUMP_ASSESSMENTS_IDX]),
    CONSTRAINT [FK_T_OD_DUMP_ASSESS_CNT_D] FOREIGN KEY ([REF_WASTE_TYPE_IDX]) REFERENCES [T_OD_REF_WASTE_TYPE] ([REF_WASTE_TYPE_IDX])
);