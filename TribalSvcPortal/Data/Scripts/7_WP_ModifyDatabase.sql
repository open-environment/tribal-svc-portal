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



INSERT [dbo].[T_PRT_REF_EMAIL_TEMPLATE] ([EMAIL_TEMPLATE_NAME], [EMAIL_TEMPLATE_DESC], [SUBJ], [MSG], [MODIFY_USER_ID], [MODIFY_DT]) VALUES (N'WP_ADMIN', N'Admin rights to the Tribal Portal Wordpress site ', N'Admin Access Granted to Tribal Service Portal', N'This notice confirms that your Tribal Service Portal account has been approved with Admin rights for the <b>{Tribe}</b>. <br/><br/>
This also grants you Admin rights to the Tribal Portal WordPress site located at: <b>{link}.</b><br/><br/>
This email has been sent to <b>{recipient}</b>.
', N'86b45014-e2d2-457e-874c-42c94b203c79', CAST(N'2020-05-13T18:40:35.0000000' AS DateTime2))
GO

INSERT INTO T_PRT_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[ENCRYPT_IND],[MODIFY_USER_ID],[MODIFY_DT]) VALUES ('WORDPRESS_URI','http://40.77.28.231.xip.io','Base URI of WordPress site.',0,0,GetDate());
GO
INSERT INTO T_PRT_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[ENCRYPT_IND],[MODIFY_USER_ID],[MODIFY_DT]) VALUES ('WORDPRESS_USERNAME','user','Netword Admin user name of WordPress.',0,0,GetDate());
GO
INSERT INTO T_PRT_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[ENCRYPT_IND],[MODIFY_USER_ID],[MODIFY_DT]) VALUES ('WORDPRESS_PWD','ckUL1vnu8PC03j9orQiemmGTPJ+AtCAsI++CAssEKKs=','Netword Admin password of WordPress.',1,0,GetDate());
GO



ALTER TABLE dbo.[T_PRT_ORGANIZATIONS] ADD WORDPRESS_URL varchar(200);
GO
update [T_PRT_ORGANIZATIONS] set WORDPRESS_URL='http://40.77.28.231.xip.io/mcn/' where ORG_ID='MCNCREEK';
update [T_PRT_ORGANIZATIONS] set WORDPRESS_URL='http://40.77.28.231.xip.io/kickapootribe/' where ORG_ID='KICKAPOO';
update [T_PRT_ORGANIZATIONS] set WORDPRESS_URL='http://40.77.28.231.xip.io/sfnoes/' where ORG_ID='SFNOES';
update [T_PRT_ORGANIZATIONS] set WORDPRESS_URL='http://40.77.28.231.xip.io/abshawnee/' where ORG_ID='ABSHAWNEE';

