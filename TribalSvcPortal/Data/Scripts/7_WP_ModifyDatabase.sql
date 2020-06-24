/*
   25 May 202013:27:42
   User: 
   Server: QUBITS-ASUS-DUO
   Database: TRIBAL_SVC_PORTAL
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.T_PRT_USERS ADD
	PasswordEncrypt nvarchar(MAX) NULL,
	WordPressUserId int NULL
GO
ALTER TABLE dbo.T_PRT_USERS SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.T_PRT_USERS', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.T_PRT_USERS', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.T_PRT_USERS', 'Object', 'CONTROL') as Contr_Per 





ALTER TABLE dbo.[T_PRT_ORGANIZATIONS] ADD WORDPRESS_URL varchar(200);
GO
update [T_PRT_ORGANIZATIONS] set WORDPRESS_URL='http://40.77.28.231.xip.io/mcn/' where ORG_ID='MCNCREEK';
update [T_PRT_ORGANIZATIONS] set WORDPRESS_URL='http://40.77.28.231.xip.io/kickapootribe/' where ORG_ID='KICKAPOO';
update [T_PRT_ORGANIZATIONS] set WORDPRESS_URL='http://40.77.28.231.xip.io/sfnoes/' where ORG_ID='SFNOES';
update [T_PRT_ORGANIZATIONS] set WORDPRESS_URL='http://40.77.28.231.xip.io/abshawnee/' where ORG_ID='ABSHAWNEE';
