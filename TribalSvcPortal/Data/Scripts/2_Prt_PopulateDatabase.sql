USE [TRIBAL_SVC_PORTAL];
GO

--THIS SCRIPT POPULATES THE DATABASE WITH INITIAL DATA

--****************GENERAL APP SETTINGS  *****************************************************************************************
INSERT INTO T_PRT_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[MODIFY_USER_ID],[MODIFY_DT]) VALUES ('EMAIL_FROM','donotreply@mcn-nsn.gov','The email address in the FROM line when sending emails from this application.',0,GetDate());
INSERT INTO T_PRT_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[MODIFY_USER_ID],[MODIFY_DT]) VALUES ('EMAIL_SERVER','smtp.sendgrid.net','The SMTP email server used to allow this application to send emails.',0,GetDate());
INSERT INTO T_PRT_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[MODIFY_USER_ID],[MODIFY_DT]) VALUES ('EMAIL_PORT','25','The port used to access the SMTP email server.',0,GetDate());
INSERT INTO T_PRT_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[MODIFY_USER_ID],[MODIFY_DT]) VALUES ('EMAIL_SECURE_USER','smtp@change.me','If the SMTP server requires authentication, this is the SMTP server username.',0,GetDate());
INSERT INTO T_PRT_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[ENCRYPT_IND],[MODIFY_USER_ID],[MODIFY_DT]) VALUES ('EMAIL_SECURE_PWD','change.me','If the SMTP server requires authentication, this is the SMTP server password or API KEY.',1,0,GetDate());

INSERT INTO T_PRT_APP_SETTINGS_CUSTOM ([TERMS_AND_CONDITIONS],[ANNOUNCEMENTS]) values ('<p>The access and use of the Tribal Services Portal requires the creation of a user ID and password that I must maintain and keep confidential.</p>	
<p>By proceeding, you acknowledge that you fully understand and consent to all of the following:</p>	
<ul><li>Any communications or information used, transmitted, or stored on the Tribal Services Portal may be used or disclosed for any lawful government purpose, including but not limited to, administrative purposes, penetration testing, communication security monitoring, personnel misconduct measures, law enforcement, and counterintelligence inquiries</li>	
<li>At any time, parties may for any lawful government purpose, without notice, monitor, intercept, search, and seize any authorized or unauthorized communication or information used or stored on the Tribal Services Portal</li>	
</ul><p>&nbsp;</p>	
<p><strong>Privacy Statement</strong><br> Personal identifying information you provide will be used for the expressed purpose of registration to this site and for updating and correcting agency information as necessary. This information will not be made available for other purposes unless required by law. Your information will not be sold or otherwise transferred to an outside third party.</p>	
<p>&nbsp;</p>','');


--****************ROLES *****************************************************************************************
INSERT INTO [T_PRT_ROLES] ([Id], ConcurrencyStamp, [Name],[NormalizedName],ROLE_DESC) 
  VALUES ('e6884ad7-2135-4cd3-843c-d6eaa0edcbcd', '51518535-6538-4fd2-b6ed-8b4af39ddb60','PortalAdmin','PORTALADMIN','Global administration role for the tribal portal, spanning all organizations.');

GO

--****************USER ROLES *****************************************************************************************
insert into [T_PRT_USER_ROLES] (UserId, RoleId)
select [Id], 'e6884ad7-2135-4cd3-843c-d6eaa0edcbcd' from [T_PRT_USERS]



--****************CLIENTS *****************************************************************************************
INSERT INTO T_PRT_CLIENTS (CLIENT_ID, CLIENT_NAME, CLIENT_GRANT_TYPE, CLIENT_REDIRECT_URI, CLIENT_POST_LOGOUT_URI, CLIENT_URL, CLIENT_LOCAL_IND)
values ('open_waters','Open Waters', 'IMPLICIT', 'http://localhost:59412/signinoidc','http://localhost:59412/signoutcallbackoidc','http://localhost:59412/App_Pages/Secure/Dashboard.aspx', 0);

INSERT INTO T_PRT_CLIENTS (CLIENT_ID, CLIENT_NAME, CLIENT_GRANT_TYPE, CLIENT_REDIRECT_URI, CLIENT_POST_LOGOUT_URI, CLIENT_URL, CLIENT_LOCAL_IND)
values ('open_dump','Open Dump', 'IMPLICIT', 'http://localhost:1245/signinoidc','http://localhost:1245/signoutcallbackoidc','http://localhost:1245/Dashboard/Index',1);

INSERT INTO T_PRT_CLIENTS (CLIENT_ID, CLIENT_NAME, CLIENT_GRANT_TYPE, CLIENT_REDIRECT_URI, CLIENT_POST_LOGOUT_URI, CLIENT_URL, CLIENT_LOCAL_IND)
values ('emergency_hound','Emergency Hound Web', 'IMPLICIT', 'http://localhost:1244/signinoidc','http://localhost:1244/signoutcallbackoidc','http://localhost:1244/Dashboard/Index', 0);

--INSERT INTO T_PRT_CLIENTS (CLIENT_ID, CLIENT_NAME, CLIENT_GRANT_TYPE, CLIENT_REDIRECT_URI, CLIENT_POST_LOGOUT_URI)
--values ('emergency_hound','Emergency Hound Mobile', 'IMPLICIT', 'http://localhost:1244/signinoidc','http://localhost:1244/signoutcallbackoidc','');


--****************ORGANIZATIONS *****************************************************************************************
INSERT INTO [T_PRT_ORGANIZATIONS] ([ORG_ID], [ORG_NAME]) values ('MCNCREEK','Muscogee Creek Nation');
INSERT INTO [T_PRT_ORGANIZATIONS] ([ORG_ID], [ORG_NAME]) values ('ABSHAWNEE','Absentee Shawnee Tribe of Oklahoma');
INSERT INTO [T_PRT_ORGANIZATIONS] ([ORG_ID], [ORG_NAME]) values ('CHOCNAT','Choctaw Nation');
INSERT INTO [T_PRT_ORGANIZATIONS] ([ORG_ID], [ORG_NAME]) values ('DELAWARENATION','Delaware Nation');
INSERT INTO [T_PRT_ORGANIZATIONS] ([ORG_ID], [ORG_NAME]) values ('KICKAPOO','Kickapoo Tribe of Oklahoma');
INSERT INTO [T_PRT_ORGANIZATIONS] ([ORG_ID], [ORG_NAME]) values ('KIOWA','Kiowa Tribe of Oklahoma');
INSERT INTO [T_PRT_ORGANIZATIONS] ([ORG_ID], [ORG_NAME]) values ('OSAGENTN','Osage Nation');
INSERT INTO [T_PRT_ORGANIZATIONS] ([ORG_ID], [ORG_NAME]) values ('O_MTRIBE','Otoe Missouria Tribe of Oklahoma');
INSERT INTO [T_PRT_ORGANIZATIONS] ([ORG_ID], [ORG_NAME]) values ('POTAWATOMI','Potawatomi Nation');
INSERT INTO [T_PRT_ORGANIZATIONS] ([ORG_ID], [ORG_NAME]) values ('SFNOES','Sac and Fox Nation');
INSERT INTO [T_PRT_ORGANIZATIONS] ([ORG_ID], [ORG_NAME]) values ('SNEPO','Seminole Nation');
INSERT INTO [T_PRT_ORGANIZATIONS] ([ORG_ID], [ORG_NAME]) values ('TONKAWA','Tonkawa Tribe of Oklahoma');
INSERT INTO [T_PRT_ORGANIZATIONS] ([ORG_ID], [ORG_NAME]) values ('WDEP','Wichita Department of Environmental Programs');


INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('CHOCNAT','open_waters','CHOCNATWQX');
INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('KIOWA','open_waters','KIOWA_WQX');
INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('MCNCREEK','open_waters','MCNCREEK_WQX');
INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('OSAGENTN','open_waters','OSAGENTN_WQX');
INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('O_MTRIBE','open_waters','O_MTRIBE_WQX');
INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('SFNOES','open_waters','SFNOES_WQX');
INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('SNEPO','open_waters','SNEPO_WQX');
INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('TONKAWA','open_waters','TONKAWA1');
INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('WDEP','open_waters','WDEP_WQX');
INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('ABSHAWNEE','open_waters','ABSHAWNEE');
INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('DELAWARENATION','open_waters','DELAWARENATION');
INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('KICKAPOO','open_waters','KICKAPOO');
INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('POTAWATOMI','open_waters','POTAWATOMI');

INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('POTAWATOMI','emergency_hound','Potawatomi Nation');
INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('SNEPO','emergency_hound','Seminole Nation');
INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('KICKAPOO','emergency_hound','Kickapoo Tribe');
INSERT INTO [T_PRT_ORG_CLIENT_ALIAS]([ORG_ID], [CLIENT_ID], [ORG_CLIENT_ALIAS]) values ('MCNCREEK','emergency_hound','Muscogee Creek Nation');

--****************T_PRT_ORG_EMAIL ************************************************************************************
INSERT INTO [T_PRT_ORG_EMAIL_RULE]([ORG_ID],[EMAIL_STRING]) values ('ABSHAWNEE','astribe.com');
INSERT INTO [T_PRT_ORG_EMAIL_RULE]([ORG_ID],[EMAIL_STRING]) values ('CHOCNAT','choctaw.org');
INSERT INTO [T_PRT_ORG_EMAIL_RULE]([ORG_ID],[EMAIL_STRING]) values ('CHOCNAT','choctawnation.com');
INSERT INTO [T_PRT_ORG_EMAIL_RULE]([ORG_ID],[EMAIL_STRING]) values ('DELAWARENATION','delawarenation.com');
INSERT INTO [T_PRT_ORG_EMAIL_RULE]([ORG_ID],[EMAIL_STRING]) values ('KICKAPOO','kickapootribeofoklahoma.com');
INSERT INTO [T_PRT_ORG_EMAIL_RULE]([ORG_ID],[EMAIL_STRING]) values ('KICKAPOO','okkt.net');
INSERT INTO [T_PRT_ORG_EMAIL_RULE]([ORG_ID],[EMAIL_STRING]) values ('MCNCREEK','mcn-nsn.gov');
INSERT INTO [T_PRT_ORG_EMAIL_RULE]([ORG_ID],[EMAIL_STRING]) values ('OSAGENTN','osagenation-nsn.gov');
INSERT INTO [T_PRT_ORG_EMAIL_RULE]([ORG_ID],[EMAIL_STRING]) values ('SFNOES','sacandfoxnation-nsn.gov');
INSERT INTO [T_PRT_ORG_EMAIL_RULE]([ORG_ID],[EMAIL_STRING]) values ('SNEPO','sno-nsn.gov');


--****************T_PRT_REF_SHARE_TYPE ************************************************************************************
INSERT INTO T_PRT_REF_SHARE_TYPE (SHARE_TYPE,SHARE_DESC,ACT_IND) values ('Jurisdiction', 'Only allow users of same jurisdiction to view', 1);
INSERT INTO T_PRT_REF_SHARE_TYPE (SHARE_TYPE,SHARE_DESC,ACT_IND) values ('All Jurisdictions', 'Share with all jurisdictions', 1);
INSERT INTO T_PRT_REF_SHARE_TYPE (SHARE_TYPE,SHARE_DESC,ACT_IND) values ('Public', 'Share with the Public', 1);

--****************T_PRT_REF_DOC_TYPE ************************************************************************************
INSERT INTO T_PRT_REF_DOC_TYPE (DOC_TYPE,DOC_TYPE_DESC,ACT_IND,CREATE_DT) VALUES ('SOP', 'SOP', 1, GetDate());
INSERT INTO T_PRT_REF_DOC_TYPE (DOC_TYPE,DOC_TYPE_DESC,ACT_IND,CREATE_DT) VALUES ('Users Manual', 'Users Manual', 1, GetDate());
INSERT INTO T_PRT_REF_DOC_TYPE (DOC_TYPE,DOC_TYPE_DESC,ACT_IND,CREATE_DT) VALUES ('Report', 'Report', 1, GetDate());
INSERT INTO T_PRT_REF_DOC_TYPE (DOC_TYPE,DOC_TYPE_DESC,ACT_IND,CREATE_DT) VALUES ('Assessment', 'Assessment', 1, GetDate());

--****************T_PRT_REF_DOC_STATUS_TYPE ************************************************************************************
INSERT INTO T_PRT_REF_DOC_STATUS_TYPE (DOC_STATUS_TYPE,ACT_IND) VALUES ('Current', 1);
INSERT INTO T_PRT_REF_DOC_STATUS_TYPE (DOC_STATUS_TYPE,ACT_IND) VALUES ('Archive', 1);
INSERT INTO T_PRT_REF_DOC_STATUS_TYPE (DOC_STATUS_TYPE,ACT_IND) VALUES ('Historical', 1);


--****************T_PRT_REF_UNITS ************************************************************************************
INSERT INTO T_PRT_REF_UNITS (UNIT_MSR_CD,UNIT_MSR_CAT, STD_UNIT_IND, UNIT_CONVERSION) VALUES ('units', 'units',1,0);
INSERT INTO T_PRT_REF_UNITS (UNIT_MSR_CD,UNIT_MSR_CAT, STD_UNIT_IND, UNIT_CONVERSION) VALUES ('cu-yds', 'volume',1,0);
INSERT INTO T_PRT_REF_UNITS (UNIT_MSR_CD,UNIT_MSR_CAT, STD_UNIT_IND, UNIT_CONVERSION) VALUES ('tons', 'weight',1,0);


--****************T_PRT_REF_EMAIL_TEMPLATE ************************************************************************************
INSERT INTO T_PRT_REF_EMAIL_TEMPLATE ([EMAIL_TEMPLATE_NAME],[EMAIL_TEMPLATE_DESC], [SUBJ], [MSG]) VALUES ('EMAIL_CONFIRM','Sent to users to allow them to confirm their email address as part of activating their account.','Tribal Portal - Verify Your Email','Please verify your Tribal Portal account by clicking this link: <a href=''{callbackUrl}''>Verify Account</a>');
INSERT INTO T_PRT_REF_EMAIL_TEMPLATE ([EMAIL_TEMPLATE_NAME],[EMAIL_TEMPLATE_DESC], [SUBJ], [MSG]) VALUES ('RESET_PASSWORD','Reset a user''s password','Tribal Portal - Reset Password','Please reset your Tribal Portal password by clicking here: <a href=''{callbackUrl}''>Reset Password</a>');
INSERT INTO T_PRT_REF_EMAIL_TEMPLATE ([EMAIL_TEMPLATE_NAME],[EMAIL_TEMPLATE_DESC], [SUBJ], [MSG]) VALUES ('ACCESS_REQUEST','Request additional access rights to Tribal Portal','Tribal Portal - Access Request','The following user: {userName} is requesting access to the {client} module for the {orgID} organization in the Tribal Services Portal. Please log in to grant or deny access rights.');
