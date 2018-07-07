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
	drop table [T_PRT_USERS];


	drop table [T_PRT_TENANT_USER_CLIENT];
	drop table [T_PRT_TENANT_CLIENT_ALIAS];
	drop table [T_PRT_CLIENT_ROLES];
	drop table [T_PRT_CLIENTS];
	drop table [T_PRT_TENANT_USERS];
	drop table [T_PRT_TENANTS];
	drop table [T_OE_SYS_LOG];

--BUILD SCAFFOLDING script (Open Packages Manager (under tools) and run this command:)
Scaffold-DbContext "Server=.\SQLEXPRESS;Database=TRIBAL_SVC_PORTAL;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data/Models -t T_PRT_CLIENT_ROLES, T_PRT_CLIENTS, T_PRT_TENANT_CLIENT_ALIAS, T_PRT_TENANT_USER_CLIENT, T_PRT_TENANT_USERS, T_PRT_TENANTS, T_OE_SYS_LOG -f -Context "ApplicationDbContextTemp"
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




CREATE TABLE [T_PRT_CLIENTS] (
    [CLIENT_ID] nvarchar(20) NOT NULL,
    [CLIENT_NAME] nvarchar(50) NOT NULL,
    [CLIENT_GRANT_TYPE] nvarchar(20) NOT NULL,
    [CLIENT_REDIRECT_URI] nvarchar(250) NULL,
    [CLIENT_POST_LOGOUT_URI] nvarchar(250) NULL,
    [CLIENT_URL] nvarchar(250) NULL,
    [CLIENT_IMAGE] varbinary(max) NULL,
    CONSTRAINT [PK_T_PRT_CLIENTS] PRIMARY KEY ([CLIENT_ID])
);
GO


CREATE TABLE [T_PRT_CLIENT_ROLES] (
	[CLIENT_ROLES_IDX] int IDENTITY NOT NULL,
    [CLIENT_ROLE_NAME] nvarchar(100),
    [CLIENT_ID] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_T_PRT_CLIENT_ROLES] PRIMARY KEY ([CLIENT_ROLES_IDX]),
    CONSTRAINT [FK_T_PRT_CLIENT_ROLES_C] FOREIGN KEY ([CLIENT_ID]) REFERENCES [T_PRT_CLIENTS] ([CLIENT_ID]) ON DELETE CASCADE
);
GO


CREATE TABLE [T_PRT_TENANTS] (
	[TENANT_ID] nvarchar(30) NOT NULL,
	[TENANT_NAME] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_T_PRT_TENANTS] PRIMARY KEY ([TENANT_ID])
);
GO

CREATE TABLE [T_PRT_TENANT_CLIENT_ALIAS] (
	[TENANT_ID] nvarchar(30) NOT NULL,
    [CLIENT_ID] nvarchar(20) NOT NULL,
    [TENANT_CLIENT_ALIAS] nvarchar(30) NOT NULL,
    CONSTRAINT [PK_T_PRT_TENANT_CLIENT_ALIAS] PRIMARY KEY ([TENANT_ID],[CLIENT_ID])
);
GO


CREATE TABLE [T_PRT_TENANT_USERS] (
	[TENANT_USER_IDX] int IDENTITY NOT NULL,
	[TENANT_ID] nvarchar(30) NOT NULL,
	[Id] nvarchar(450) NOT NULL,
	[TENANT_ADMIN_IND] bit NOT NULL,
	[STATUS_IND] nvarchar(1) NOT NULL,
	[CREATE_USER_ID] nvarchar(450) NULL,
	[CREATE_DT] datetime2(0) NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
    CONSTRAINT [PK_T_PRT_TENANT_USERS] PRIMARY KEY ([TENANT_USER_IDX]),
    CONSTRAINT [FK_T_PRT_TENANT_USERS_T] FOREIGN KEY ([TENANT_ID]) REFERENCES [T_PRT_TENANTS] ([TENANT_ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_T_PRT_TENANT_USERS_U] FOREIGN KEY ([Id]) REFERENCES [T_PRT_USERS] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [T_PRT_TENANT_USER_CLIENT] (
	[TENANT_USER_CLIENT_IDX] int IDENTITY NOT NULL,
	[TENANT_USER_IDX] int NOT NULL,
	[CLIENT_ID] nvarchar(20) NOT NULL,
	[ADMIN_IND] bit NOT NULL,
	[STATUS_IND] nvarchar(1) NOT NULL,
	[CREATE_USER_ID] nvarchar(450) NULL,
	[CREATE_DT] datetime2(0) NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
    CONSTRAINT [PK_T_PRT_TENANT_USER_CLIENT_ROLES] PRIMARY KEY ([TENANT_USER_CLIENT_IDX]),
    CONSTRAINT [FK_T_PRT_TENANT_USER_CLIENT_ROLES_U] FOREIGN KEY ([TENANT_USER_IDX]) REFERENCES [T_PRT_TENANT_USERS] ([TENANT_USER_IDX]) ON DELETE CASCADE,
    CONSTRAINT [FK_T_PRT_TENANT_USER_CLIENT_ROLES_C] FOREIGN KEY ([CLIENT_ID]) REFERENCES [T_PRT_CLIENTS] ([CLIENT_ID]) ON DELETE CASCADE
);
GO




CREATE TABLE [dbo].[T_OE_SYS_LOG](
	[SYS_LOG_ID] [int] IDENTITY(1,1) NOT NULL,
	[LOG_DT] [datetime2](0) NOT NULL,
	[LOG_USER_ID] nvarchar(450) NULL,
	[LOG_TYPE] [varchar](15) NULL,
	[LOG_MSG] [varchar](2000) NULL,
 CONSTRAINT [PK_T_OE_SYS_LOG] PRIMARY KEY CLUSTERED  ([SYS_LOG_ID] ASC)
) ON [PRIMARY]

GO

