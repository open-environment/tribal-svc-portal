/***************************************************************** */
/*************DROP EXISTING DATABASE (only use if refreshing DB*** */
/***************************************************************** */
/*
	USE [TRIBAL_SVC_PORTAL];
	drop table [T_PRT_USER_ROLES];
	drop table [T_PRT_ROLE_CLAIMS];
	drop table [T_PRT_USER_CLAIMS];
	drop table [T_PRT_USER_LOGINS];
	drop table [T_PRT_ROLES];
	drop table [T_PRT_USER_TOKENS];

	drop table [T_PRT_ORG_USER_CLIENT]; 
	drop table [T_PRT_ORG_CLIENT_ALIAS];  
	drop table [T_PRT_CLIENT_ROLES];  
	drop table [T_PRT_CLIENTS];  
	drop table [T_PRT_ORG_USERS]; 

	drop table [T_PRT_APP_SETTINGS];
	drop table [T_PRT_APP_SETTINGS_CUSTOM];
	drop table [T_PRT_SYS_LOG];
	drop table [T_PRT_SYS_EMAIL_LOG];

	drop table [T_PRT_DOCUMENTS];
	drop table [T_PRT_REF_SHARE_TYPE];
	drop table [T_PRT_REF_DOC_TYPE];
	drop table [T_PRT_REF_DOC_STATUS_TYPE];


	drop table [T_PRT_SITE_INTERESTS];
	drop table [T_PRT_SITES];
	drop table [T_PRT_ORGANIZATIONS];  
	drop table [T_PRT_USERS];


--BUILD SCAFFOLDING script (Open Packages Manager (under tools) and run this command:)
Scaffold-DbContext -UseDatabaseNames "Server=.\SQLEXPRESS;Database=TRIBAL_SVC_PORTAL;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data/Models -t T_PRT_APP_SETTINGS, T_PRT_APP_SETTINGS_CUSTOM, T_PRT_CLIENT_ROLES, T_PRT_CLIENTS, T_PRT_DOCUMENTS, T_PRT_ORG_CLIENT_ALIAS, T_PRT_ORG_USER_CLIENT, T_PRT_ORG_USERS, T_PRT_ORGANIZATIONS, T_PRT_REF_DOC_STATUS_TYPE, T_PRT_REF_DOC_TYPE, T_PRT_REF_SHARE_TYPE, T_PRT_SITES, T_PRT_SITE_INTERESTS, T_PRT_SYS_EMAIL_LOG, T_PRT_SYS_LOG -f -Context "ApplicationDbContextTemp"
*/

/***************************************************************** */
/*************DROP EXISTING DATABASE (only use if refreshing DB*** */
/***************************************************************** */
/*
	  EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'TRIBAL_SVC_PORTAL'
	  GO
	  USE [master]
	  GO
	  ALTER DATABASE [TRIBAL_SVC_PORTAL] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
	  GO
	  USE [master]
	  GO
	  DROP DATABASE [TRIBAL_SVC_PORTAL]
	  GO
*/

CREATE DATABASE [TRIBAL_SVC_PORTAL];
GO

/************************************************************************* */
/*************CREATE USER AND GRANT RIGHTS******************************** */
/************************************************************************* */
IF EXISTS (SELECT * FROM sys.server_principals WHERE name = N'portal_svc_login')
DROP LOGIN [portal_svc_login]

use [TRIBAL_SVC_PORTAL]


Create login portal_svc_login with password='F$GHWjpN!17h';
EXEC sp_defaultdb @loginame='portal_svc_login', @defdb='TRIBAL_SVC_PORTAL' 
Create user [portal_svc_user] for login [portal_svc_login]; 
exec sp_addrolemember 'db_owner', 'portal_svc_user'; 



/************************************************************ */
/*************CREATE TABLES  ******************************** */
/************************************************************ */
USE [TRIBAL_SVC_PORTAL];
GO

/*******************  START IDENTITY TABLES ***********************************************/
CREATE TABLE [T_PRT_ROLES] (
    [Id] nvarchar(450) NOT NULL,
    [ConcurrencyStamp] nvarchar(max),
    [Name] nvarchar(256),
    [NormalizedName] nvarchar(256),
    CONSTRAINT [PK_T_PRT_ROLES] PRIMARY KEY ([Id])
);

GO


CREATE TABLE [T_PRT_USER_TOKENS] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max),
    CONSTRAINT [PK_T_PRT_USER_TOKENS] PRIMARY KEY ([UserId], [LoginProvider], [Name])
);


GO

CREATE TABLE [T_PRT_USERS] (
    [Id] nvarchar(450) NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [ConcurrencyStamp] nvarchar(max),
    [Email] nvarchar(256),
    [EmailConfirmed] bit NOT NULL,
    [LockoutEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset,
    [NormalizedEmail] nvarchar(256),
    [NormalizedUserName] nvarchar(256),
    [PasswordHash] nvarchar(max),
    [PhoneNumber] nvarchar(max),
    [PhoneNumberConfirmed] bit NOT NULL,
    [SecurityStamp] nvarchar(max),
    [TwoFactorEnabled] bit NOT NULL,
    [UserName] nvarchar(256),
	[FIRST_NAME] varchar(40),
	[LAST_NAME] varchar(40),
    CONSTRAINT [PK_T_PRT_USERS] PRIMARY KEY ([Id])
);

GO


CREATE TABLE [T_PRT_ROLE_CLAIMS] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max),
    [ClaimValue] nvarchar(max),
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_T_PRT_ROLE_CLAIMS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [T_PRT_ROLES] ([Id]) ON DELETE CASCADE
);


GO


CREATE TABLE [T_PRT_USER_CLAIMS] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max),
    [ClaimValue] nvarchar(max),
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_T_PRT_USER_CLAIMS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [T_PRT_USERS] ([Id]) ON DELETE CASCADE
);

GO


CREATE TABLE [T_PRT_USER_LOGINS] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max),
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_T_PRT_USER_LOGINS] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [T_PRT_USERS] ([Id]) ON DELETE CASCADE
);

GO


CREATE TABLE [T_PRT_USER_ROLES] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_T_PRT_USER_ROLES] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [T_PRT_ROLES] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [T_PRT_USERS] ([Id]) ON DELETE CASCADE
);


GO


CREATE INDEX [RoleNameIndex] ON [T_PRT_ROLES] ([NormalizedName]);
CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [T_PRT_ROLE_CLAIMS] ([RoleId]);
CREATE INDEX [IX_AspNetUserClaims_UserId] ON [T_PRT_USER_CLAIMS] ([UserId]);
CREATE INDEX [IX_AspNetUserLogins_UserId] ON [T_PRT_USER_LOGINS] ([UserId]);
CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [T_PRT_USER_ROLES] ([RoleId]);
CREATE INDEX [IX_AspNetUserRoles_UserId] ON [T_PRT_USER_ROLES] ([UserId]);
CREATE INDEX [EmailIndex] ON [T_PRT_USERS] ([NormalizedEmail]);
CREATE UNIQUE INDEX [UserNameIndex] ON [T_PRT_USERS] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;


GO
/*******************  END IDENTITY TABLES ***********************************************/


/*******************  START PORTAL ORGANIZATION TABLES ***********************************************/
CREATE TABLE [T_PRT_CLIENTS] (
    [CLIENT_ID] varchar(20) NOT NULL,
    [CLIENT_NAME] varchar(50) NOT NULL,
    [CLIENT_GRANT_TYPE] varchar(20) NOT NULL,
    [CLIENT_REDIRECT_URI] varchar(250) NULL,
    [CLIENT_POST_LOGOUT_URI] varchar(250) NULL,
    [CLIENT_URL] varchar(250) NULL,
    [CLIENT_IMAGE] varbinary(max) NULL,
    [CLIENT_LOCAL_IND] bit NOT NULL default 0,
    CONSTRAINT [PK_T_PRT_CLIENTS] PRIMARY KEY ([CLIENT_ID])
);
GO


CREATE TABLE [T_PRT_CLIENT_ROLES] (
	[CLIENT_ROLES_IDX] int IDENTITY NOT NULL,
    [CLIENT_ROLE_NAME] nvarchar(100),
    [CLIENT_ID] varchar(20) NOT NULL,
    CONSTRAINT [PK_T_PRT_CLIENT_ROLES] PRIMARY KEY ([CLIENT_ROLES_IDX]),
    CONSTRAINT [FK_T_PRT_CLIENT_ROLES_C] FOREIGN KEY ([CLIENT_ID]) REFERENCES [T_PRT_CLIENTS] ([CLIENT_ID]) ON DELETE CASCADE
);
GO


CREATE TABLE [T_PRT_ORGANIZATIONS] (
	[ORG_ID] varchar(30) NOT NULL,
	[ORG_NAME] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_T_PRT_ORGANIZATIONS] PRIMARY KEY ([ORG_ID])
);
GO

CREATE TABLE [T_PRT_ORG_CLIENT_ALIAS] (
	[ORG_ID] varchar(30) NOT NULL,
    [CLIENT_ID] nvarchar(20) NOT NULL,
    [ORG_CLIENT_ALIAS] nvarchar(30) NOT NULL,
    CONSTRAINT [PK_T_PRT_ORG_CLIENT_ALIAS] PRIMARY KEY ([ORG_ID],[CLIENT_ID])
);
GO


CREATE TABLE [T_PRT_ORG_USERS] (
	[ORG_USER_IDX] int IDENTITY NOT NULL,
	[ORG_ID] varchar(30) NOT NULL,
	[Id] nvarchar(450) NOT NULL,
	[ORG_ADMIN_IND] bit NOT NULL,
	[STATUS_IND] nvarchar(1) NOT NULL,
	[CREATE_USER_ID] nvarchar(450) NULL,
	[CREATE_DT] datetime2(0) NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
    CONSTRAINT [PK_T_PRT_ORG_USERS] PRIMARY KEY ([ORG_USER_IDX]),
    CONSTRAINT [FK_T_PRT_ORG_USERS_T] FOREIGN KEY ([ORG_ID]) REFERENCES [T_PRT_ORGANIZATIONS] ([ORG_ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_T_PRT_ORG_USERS_U] FOREIGN KEY ([Id]) REFERENCES [T_PRT_USERS] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [T_PRT_ORG_USER_CLIENT] (
	[ORG_USER_CLIENT_IDX] int IDENTITY NOT NULL,
	[ORG_USER_IDX] int NOT NULL,
	[CLIENT_ID] varchar(20) NOT NULL,
	[ADMIN_IND] bit NOT NULL,
	[STATUS_IND] nvarchar(1) NOT NULL,
	[CREATE_USER_ID] nvarchar(450) NULL,
	[CREATE_DT] datetime2(0) NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
    CONSTRAINT [PK_T_PRT_ORG_USER_CLIENT_ROLES] PRIMARY KEY ([ORG_USER_CLIENT_IDX]),
    CONSTRAINT [FK_T_PRT_ORG_USER_CLIENT_ROLES_U] FOREIGN KEY ([ORG_USER_IDX]) REFERENCES [T_PRT_ORG_USERS] ([ORG_USER_IDX]) ON DELETE CASCADE,
    CONSTRAINT [FK_T_PRT_ORG_USER_CLIENT_ROLES_C] FOREIGN KEY ([CLIENT_ID]) REFERENCES [T_PRT_CLIENTS] ([CLIENT_ID]) ON DELETE CASCADE
);
GO

/******************************************************************************************/
/*******************  START REF DATA TABLES ***********************************************/
/******************************************************************************************/
CREATE TABLE [T_PRT_REF_SHARE_TYPE](
	[SHARE_TYPE] [varchar](20) NOT NULL,
	[SHARE_DESC] [varchar](50) NOT NULL,
	[ACT_IND] [bit] NOT NULL DEFAULT 1,
 CONSTRAINT [PK_T_PRT_REF_SHARE_TYPE] PRIMARY KEY CLUSTERED (SHARE_TYPE)
);
GO

CREATE TABLE [dbo].[T_PRT_REF_DOC_TYPE](
	[DOC_TYPE] [varchar](50) NOT NULL,
	[DOC_TYPE_DESC] [varchar](200) NULL,
	[ACT_IND] [bit] NOT NULL,
	[CREATE_USER_ID] nvarchar(450) NULL,
	[CREATE_DT] datetime2(0) NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
 CONSTRAINT [PK_T_PRT_REF_DOC_TYPE] PRIMARY KEY CLUSTERED (DOC_TYPE ASC)
);


CREATE TABLE [dbo].[T_PRT_REF_DOC_STATUS_TYPE](
	[DOC_STATUS_TYPE] [varchar](10) NOT NULL,
	[ACT_IND] [bit] NOT NULL,
	[CREATE_USER_ID] nvarchar(450) NULL,
	[CREATE_DT] datetime2(0) NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
 CONSTRAINT [PK_T_PRT_REF_DOC_STATUS_TYPE] PRIMARY KEY CLUSTERED (DOC_STATUS_TYPE ASC)
);

/******************************************************************************************/
/*******************  START SITE MANAGEMENT TABLES ***********************************************/
/******************************************************************************************/
CREATE TABLE [T_PRT_SITES] (
	[SITE_IDX] UNIQUEIDENTIFIER NOT NULL,
	[ORG_ID] varchar(30) NOT NULL,
	[SITE_NAME] varchar(100) NOT NULL,
	[EPA_ID] varchar(50) NULL,
	[LATITUDE] decimal(18,5) NULL,
	[LONGITUDE] decimal(18,5) NULL,
	[SITE_ADDRESS] nvarchar(400) NULL,
	[CREATE_USER_ID] nvarchar(450) NULL,
	[CREATE_DT] datetime2(0) NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
    CONSTRAINT [PK_T_PRT_SITES] PRIMARY KEY ([SITE_IDX]),
    CONSTRAINT [FK_T_PRT_SITES_O] FOREIGN KEY ([ORG_ID]) REFERENCES [T_PRT_ORGANIZATIONS] ([ORG_ID])
);
GO

CREATE TABLE [T_PRT_SITE_INTERESTS] (
	[SITE_INTEREST_IDX] UNIQUEIDENTIFIER NOT NULL,
	[SITE_IDX] UNIQUEIDENTIFIER NOT NULL,
	[INTEREST_NAME] varchar(50) NOT NULL,
	[CREATE_USER_ID] nvarchar(450) NULL,
	[CREATE_DT] datetime2(0) NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
    CONSTRAINT [PK_T_PRT_SITE_INTERESTS] PRIMARY KEY ([SITE_INTEREST_IDX]),
    CONSTRAINT [FK_T_PRT_SITES_INTERESTS_S] FOREIGN KEY ([SITE_IDX]) REFERENCES [T_PRT_SITES] ([SITE_IDX])
);
GO


/******************************************************************************************/
/*******************  START DOCUMENT MANAGEMENT TABLES ***********************************************/
/******************************************************************************************/
CREATE TABLE [dbo].[T_PRT_DOCUMENTS](
	[DOC_IDX] [uniqueidentifier] NOT NULL DEFAULT newid(),
	[ORG_ID] varchar(30) NOT NULL,
	[DOC_CONTENT] [varbinary](max) NULL,
	[DOC_NAME] [varchar](100) NULL,
	[DOC_TYPE] [varchar](50) NULL,
	[DOC_FILE_TYPE] [varchar](75) NULL,
	[DOC_SIZE] [int] NULL,
	[DOC_COMMENT] [varchar](200) NULL,
	[DOC_AUTHOR] [varchar](100) NULL,
	[SHARE_TYPE] [varchar](20) NULL,
	[DOC_STATUS_TYPE] [varchar](10) NULL,
	[ACT_IND] [bit] NOT NULL,
	[CREATE_USERIDX] [int] NULL,
	[CREATE_DT] [datetime2](0) NULL,
	[MODIFY_USERIDX] [int] NULL,
	[MODIFY_DT] [datetime2](0) NULL,
 CONSTRAINT [PK_T_PRT_DOCUMENTS] PRIMARY KEY CLUSTERED (DOC_IDX ASC),
 FOREIGN KEY ([ORG_ID]) references T_PRT_ORGANIZATIONS ([ORG_ID]) ON UPDATE CASCADE ,
 FOREIGN KEY (DOC_TYPE) references T_PRT_REF_DOC_TYPE (DOC_TYPE) ON UPDATE CASCADE ,
 FOREIGN KEY (SHARE_TYPE) references [T_PRT_REF_SHARE_TYPE] (SHARE_TYPE) ON UPDATE CASCADE, 
 FOREIGN KEY (DOC_STATUS_TYPE) references [T_PRT_REF_DOC_STATUS_TYPE] (DOC_STATUS_TYPE) ON UPDATE CASCADE
) ON [PRIMARY];





/*******************  START ADMINISTRATION TABLES ***********************************************/
CREATE TABLE [dbo].[T_PRT_APP_SETTINGS](
	[SETTING_IDX] int IDENTITY(1,1) NOT NULL,
	[SETTING_NAME] varchar(100) NOT NULL,
	[SETTING_DESC] nvarchar(500) NULL,
	[SETTING_VALUE] nvarchar(200) NULL,
	[ENCRYPT_IND] bit NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
 CONSTRAINT [PK_T_PRT_APP_SETTINGS] PRIMARY KEY CLUSTERED ([SETTING_IDX] ASC)
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[T_PRT_APP_SETTINGS_CUSTOM](
	[SETTING_CUSTOM_IDX] [int] IDENTITY(1,1) NOT NULL,
	[TERMS_AND_CONDITIONS] [varchar](max) NULL,
	[ANNOUNCEMENTS] [varchar](max) NULL,
 CONSTRAINT [PK_T_PRT_APP_SETTINGS_CUSTOM] PRIMARY KEY CLUSTERED ([SETTING_CUSTOM_IDX] ASC)
) ON [PRIMARY]
GO



CREATE TABLE [dbo].[T_PRT_SYS_LOG](
	[SYS_LOG_ID] int IDENTITY(1,1) NOT NULL,
	[LOG_DT] datetime2(0) NOT NULL,
	[LOG_USER_ID] nvarchar(450) NULL,
	[LOG_TYPE] varchar(15) NULL,
	[LOG_MSG] nvarchar(2000) NULL,
 CONSTRAINT [PK_T_PRT_SYS_LOG] PRIMARY KEY CLUSTERED  ([SYS_LOG_ID] ASC)
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[T_PRT_SYS_EMAIL_LOG](
	[EMAIL_LOG_ID] [int] IDENTITY(1,1) NOT NULL,
	[LOG_DT] [datetime2](0) NULL,
	[LOG_FROM] [varchar](200) NULL,
	[LOG_TO] [varchar](200) NULL,
	[LOG_CC] [varchar](200) NULL,
	[LOG_SUBJ] [varchar](200) NULL,
	[LOG_MSG] [varchar](2000) NULL,
	[EMAIL_TYPE] [varchar](15) NULL,
 CONSTRAINT [PK_T_PRT_SYS_EMAIL_LOG] PRIMARY KEY CLUSTERED  ([EMAIL_LOG_ID] ASC)
) ON [PRIMARY]

GO
