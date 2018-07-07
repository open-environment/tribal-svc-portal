USE [TRIBAL_SVC_PORTAL];
GO

--THIS SCRIPT POPULATES THE DATABASE WITH INITIAL DATA
INSERT INTO [AspNetRoles] ([Id], ConcurrencyStamp, [Name],[NormalizedName]) 
  VALUES ('e6884ad7-2135-4cd3-843c-d6eaa0edcbcd', '51518535-6538-4fd2-b6ed-8b4af39ddb60','PortalAdmin','PORTALADMIN')

GO
insert into AspNetUserRoles (UserId, RoleId)
select [Id], 'e6884ad7-2135-4cd3-843c-d6eaa0edcbcd' from [AspNetUsers]




INSERT INTO T_PRT_CLIENTS (CLIENT_ID, CLIENT_NAME, CLIENT_GRANT_TYPE, CLIENT_REDIRECT_URI, CLIENT_POST_LOGOUT_URI, CLIENT_URL)
values ('open_waters','Open Waters', 'IMPLICIT', 'http://localhost:59412/signinoidc','http://localhost:59412/signoutcallbackoidc','http://localhost:59412/App_Pages/Secure/Dashboard.aspx');

INSERT INTO T_PRT_CLIENTS (CLIENT_ID, CLIENT_NAME, CLIENT_GRANT_TYPE, CLIENT_REDIRECT_URI, CLIENT_POST_LOGOUT_URI, CLIENT_URL)
values ('open_dump','Open Dump', 'IMPLICIT', 'http://localhost:1245/signinoidc','http://localhost:1245/signoutcallbackoidc','http://localhost:1245/Dashboard/Index');

INSERT INTO T_PRT_CLIENTS (CLIENT_ID, CLIENT_NAME, CLIENT_GRANT_TYPE, CLIENT_REDIRECT_URI, CLIENT_POST_LOGOUT_URI, CLIENT_URL)
values ('emergency_hound','Emergency Hound Web', 'IMPLICIT', 'http://localhost:1244/signinoidc','http://localhost:1244/signoutcallbackoidc','http://localhost:1244/Dashboard/Index');

--INSERT INTO T_PRT_CLIENTS (CLIENT_ID, CLIENT_NAME, CLIENT_GRANT_TYPE, CLIENT_REDIRECT_URI, CLIENT_POST_LOGOUT_URI)
--values ('emergency_hound','Emergency Hound Mobile', 'IMPLICIT', 'http://localhost:1244/signinoidc','http://localhost:1244/signoutcallbackoidc','');



INSERT INTO [T_PRT_TENANTS] ([TENANT_ID], [TENANT_NAME]) values ('MCNCREEK','Muscogee Creek Nation');
INSERT INTO [T_PRT_TENANTS] ([TENANT_ID], [TENANT_NAME]) values ('ABSHAWNEE','Absentee Shawnee Tribe of Oklahoma');
INSERT INTO [T_PRT_TENANTS] ([TENANT_ID], [TENANT_NAME]) values ('CHOCNAT','Choctaw Nation');
INSERT INTO [T_PRT_TENANTS] ([TENANT_ID], [TENANT_NAME]) values ('DELAWARENATION','Delaware Nation');
INSERT INTO [T_PRT_TENANTS] ([TENANT_ID], [TENANT_NAME]) values ('KICKAPOO','Kickapoo Tribe of Oklahoma');
INSERT INTO [T_PRT_TENANTS] ([TENANT_ID], [TENANT_NAME]) values ('KIOWA','Kiowa Tribe of Oklahoma');
INSERT INTO [T_PRT_TENANTS] ([TENANT_ID], [TENANT_NAME]) values ('OSAGENTN','Osage Nation');
INSERT INTO [T_PRT_TENANTS] ([TENANT_ID], [TENANT_NAME]) values ('O_MTRIBE','Otoe Missouria Tribe of Oklahoma');
INSERT INTO [T_PRT_TENANTS] ([TENANT_ID], [TENANT_NAME]) values ('POTAWATOMI','Potawatomi Nation');
INSERT INTO [T_PRT_TENANTS] ([TENANT_ID], [TENANT_NAME]) values ('SFNOES','Sac and Fox Nation');
INSERT INTO [T_PRT_TENANTS] ([TENANT_ID], [TENANT_NAME]) values ('SNEPO','Seminole Nation');
INSERT INTO [T_PRT_TENANTS] ([TENANT_ID], [TENANT_NAME]) values ('TONKAWA','Tonkawa Tribe of Oklahoma');
INSERT INTO [T_PRT_TENANTS] ([TENANT_ID], [TENANT_NAME]) values ('WDEP','Wichita Department of Environmental Programs');



INSERT INTO [T_PRT_TENANT_CLIENT_ALIAS]([TENANT_ID], [CLIENT_ID], [TENANT_CLIENT_ALIAS]) values ('CHOCNAT','open_waters','CHOCNATWQX');
INSERT INTO [T_PRT_TENANT_CLIENT_ALIAS]([TENANT_ID], [CLIENT_ID], [TENANT_CLIENT_ALIAS]) values ('KIOWA','open_waters','KIOWA_WQX');
INSERT INTO [T_PRT_TENANT_CLIENT_ALIAS]([TENANT_ID], [CLIENT_ID], [TENANT_CLIENT_ALIAS]) values ('MCNCREEK','open_waters','MCNCREEK_WQX');
INSERT INTO [T_PRT_TENANT_CLIENT_ALIAS]([TENANT_ID], [CLIENT_ID], [TENANT_CLIENT_ALIAS]) values ('OSAGENTN','open_waters','OSAGENTN_WQX');
INSERT INTO [T_PRT_TENANT_CLIENT_ALIAS]([TENANT_ID], [CLIENT_ID], [TENANT_CLIENT_ALIAS]) values ('O_MTRIBE','open_waters','O_MTRIBE_WQX');
INSERT INTO [T_PRT_TENANT_CLIENT_ALIAS]([TENANT_ID], [CLIENT_ID], [TENANT_CLIENT_ALIAS]) values ('SFNOES','open_waters','SFNOES_WQX');
INSERT INTO [T_PRT_TENANT_CLIENT_ALIAS]([TENANT_ID], [CLIENT_ID], [TENANT_CLIENT_ALIAS]) values ('SNEPO','open_waters','SNEPO_WQX');
INSERT INTO [T_PRT_TENANT_CLIENT_ALIAS]([TENANT_ID], [CLIENT_ID], [TENANT_CLIENT_ALIAS]) values ('TONKAWA','open_waters','TONKAWA1');
INSERT INTO [T_PRT_TENANT_CLIENT_ALIAS]([TENANT_ID], [CLIENT_ID], [TENANT_CLIENT_ALIAS]) values ('WDEP','open_waters','WDEP_WQX');
INSERT INTO [T_PRT_TENANT_CLIENT_ALIAS]([TENANT_ID], [CLIENT_ID], [TENANT_CLIENT_ALIAS]) values ('POTAWATOMI','emergency_hound','Potawatomi Nation');
INSERT INTO [T_PRT_TENANT_CLIENT_ALIAS]([TENANT_ID], [CLIENT_ID], [TENANT_CLIENT_ALIAS]) values ('SNEPO','emergency_hound','Seminole Nation');
INSERT INTO [T_PRT_TENANT_CLIENT_ALIAS]([TENANT_ID], [CLIENT_ID], [TENANT_CLIENT_ALIAS]) values ('KICKAPOO','emergency_hound','Kickapoo Tribe');
INSERT INTO [T_PRT_TENANT_CLIENT_ALIAS]([TENANT_ID], [CLIENT_ID], [TENANT_CLIENT_ALIAS]) values ('MCNCREEK','emergency_hound','Muscogee Creek Nation');