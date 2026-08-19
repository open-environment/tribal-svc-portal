/*
--BUILD SCAFFOLDING script (Open Packages Manager (under tools) and run this command after navigating to project directory:)
dotnet ef dbcontext scaffold "Data Source=.\SQLEXPRESS;Initial Catalog=TRIBAL_SVC_PORTAL;Trusted_Connection=true" Microsoft.EntityFrameworkCore.SqlServer --output-dir Data/Models --use-database-names --no-pluralize -t T_OD_ASSESSMENTS -t T_OD_ASSESSMENT_CONTENT, -t T_OD_ASSESSMENT_DOCS,  -t T_PRT_SITES -t T_INSP_SITES -t T_INSP_ASSESS_PLUMBING -t T_LAW_LAWS -f -c ApplicationDbContextTemp

dotnet ef dbcontext scaffold "Data Source=.\SQLEXPRESS;Initial Catalog=TRIBAL_SVC_PORTAL;Trusted_Connection=true" Microsoft.EntityFrameworkCore.SqlServer --output-dir Data/Models --use-database-names --no-pluralize  -t T_INSP_SITES -t T_INSP_ASSESS_PLUMBING -t T_INSP_ASSESS_LIFE -t  -c ApplicationDbContextTemp2
*/

INSERT INTO T_PRT_CLIENTS (CLIENT_ID, CLIENT_NAME, CLIENT_GRANT_TYPE, CLIENT_REDIRECT_URI, CLIENT_POST_LOGOUT_URI, CLIENT_URL, CLIENT_LOCAL_IND)
values ('inspection','Inspections', 'IMPLICIT', 'http://localhost:1244/signinoidc','http://localhost:1244/signoutcallbackoidc','http://localhost:1244/Dashboard/Index', 1);

INSERT INTO T_PRT_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[MODIFY_USER_ID],[MODIFY_DT]) VALUES ('ARCGIS_USER','recman_geomcn','The username used to call ESRI API.',0,GetDate());
INSERT INTO T_PRT_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[MODIFY_USER_ID],[MODIFY_DT]) VALUES ('ARCGIS_PWD','Mvskoke2023!!','The password used to call ESRI API.',0,GetDate());

ALTER TABLE T_PRT_SITES ADD [SITE_ID] varchar(30) NULL;

CREATE TABLE [T_INSP_SITES] (
	[SITE_IDX] uniqueidentifier NOT NULL,
	[LAST_UPDATE_BY] varchar(50) NULL,
	[LAST_UPDATE_DT] datetime2(0) NULL,
    CONSTRAINT [PK_T_INSP_SITE_DTL] PRIMARY KEY ([SITE_IDX]),
    CONSTRAINT [FK_T_INSP_SITE_DTL_S] FOREIGN KEY ([SITE_IDX]) REFERENCES [T_PRT_SITES] ([SITE_IDX]) ON UPDATE CASCADE ON DELETE CASCADE
);



CREATE TABLE [T_INSP_ASSESS_PLUMBING] 
(
	[ASSESSMENT_IDX] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[SITE_IDX] uniqueidentifier NOT NULL,
	[ASSESSMENT_DT] datetime2(0) NOT NULL,
	[ASSESSED_BY] nvarchar(100) NULL,
	[OBJECTID] int NULL,
	[GlobalID] uniqueidentifier NULL,

	[Title] varchar(50) NULL,
	[WaterPSI] varchar(50) NULL,
	[Meter_Location] varchar(50) NULL,
	[Supply_Lines] varchar(12) NULL,
	[Drains] varchar(12) NULL,
	[Sewer_Loc] varchar(50) NULL,
	[Sink_Faucet] varchar(12) NULL,
	[Toilet_Urinal] varchar(12) NULL,
	[Shower_Tub] varchar(12) NULL,
	[Dishwasher] varchar(12) NULL,
	[Water_Heater] varchar(12) NULL,
	[Heater_Type] varchar(12) NULL,
	[Backflow] varchar(50) NULL,
	[Repairs] nvarchar(max) NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
    CONSTRAINT [PK_T_INSP_ASSESS_PLUMBING] PRIMARY KEY ([ASSESSMENT_IDX]),
    CONSTRAINT [FK_T_INSP_ASSESSMENTS_S] FOREIGN KEY ([SITE_IDX]) REFERENCES [T_INSP_SITES] ([SITE_IDX]) ON UPDATE CASCADE ON DELETE CASCADE
);


CREATE TABLE [T_INSP_ASSESS_ELECTRICAL] 
(
	[ASSESSMENT_IDX] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[SITE_IDX] uniqueidentifier NOT NULL,
	[ASSESSMENT_DT] datetime2(0) NOT NULL,
	[ASSESSED_BY] nvarchar(100) NULL,
	[OBJECTID] int NULL,
	[GlobalID] uniqueidentifier NULL,
	[Title] varchar(50) NULL,

	[Contact] varchar(80) NULL,
	[Compliance] varchar(10) NULL,
	[Gen_Site] varchar(10) NULL,
	[Gen_Hazard] varchar(10) NULL,
	[Gen_Address] varchar(10) NULL,
	[Elec_Rust] varchar(10) NULL,
	[Elec_Label] varchar(10) NULL,
	[Elec_Space] varchar(10) NULL,
	[Wiring_Secured] varchar(10) NULL,
	[Wiring_Damage] varchar(10) NULL,
	[Wiring_Separate] varchar(10) NULL,
	[Outlet_Func] varchar(10) NULL,
	[Outlet_GFCI] nvarchar(10) NULL,
	[Outlet_Tamper] nvarchar(10) NULL,
	[Light_Fixtures] nvarchar(10) NULL,
	[Light_Exit] nvarchar(10) NULL,
	[Light_Ill] nvarchar(10) NULL,
	[Ground_Connections] nvarchar(10) NULL,
	[Ground_Corrosion] nvarchar(10) NULL,
	[Ground_Electrode] nvarchar(10) NULL,
	[Surge_Protectors] nvarchar(10) NULL,
	[Surge_Func] nvarchar(10) NULL,
	[Comp_Codes] nvarchar(10) NULL,
	[Comp_Signage] nvarchar(10) NULL,
	[Comp_Records] nvarchar(10) NULL,
	[Corrective] nvarchar(max) NULL,
	[BuildingType] varchar(256) NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
    CONSTRAINT [PK_T_INSP_ASSESS_ELECTRICAL] PRIMARY KEY ([ASSESSMENT_IDX]),
    CONSTRAINT [FK_T_INSP_ASSESSMENTS_E] FOREIGN KEY ([SITE_IDX]) REFERENCES [T_INSP_SITES] ([SITE_IDX]) ON UPDATE CASCADE ON DELETE CASCADE
);
--alter table [T_INSP_ASSESS_ELECTRICAL] add [BuildingType] varchar(256) NULL;



CREATE TABLE [T_INSP_ASSESS_MECHANICAL] 
(
	[ASSESSMENT_IDX] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[SITE_IDX] uniqueidentifier NOT NULL,
	[ASSESSMENT_DT] datetime2(0) NOT NULL,
	[ASSESSED_BY] nvarchar(100) NULL,
	[OBJECTID] int NULL,
	[GlobalID] uniqueidentifier NULL,
	[Title] varchar(50) NULL,

	[Contact] varchar(80) NULL,
	[Compliance] varchar(10) NULL,

	[HVAC_Clean] varchar(10) NULL,
	[HVAC_Filters] varchar(10) NULL,
	[HVAC_Therm] varchar(10) NULL,
	[Duct_Leak] varchar(10) NULL,
	[Duct_Insulation] varchar(10) NULL,
	[Duct_Obstruction] varchar(10) NULL,
	[Piping_Leaks] varchar(10) NULL,
	[Piping_Insulation] varchar(10) NULL,
	[Piping_Valves] varchar(10) NULL,
	[Equip_Operational] varchar(10) NULL,
	[Equip_Doc] nvarchar(10) NULL,
	[Equip_Guards] nvarchar(10) NULL,
	[Vent_Airflow] nvarchar(10) NULL,
	[Vent_Exhaust] nvarchar(10) NULL,
	[Vent_Air] nvarchar(10) NULL,
	[Comp_Codes] nvarchar(10) NULL,
	[Comp_Signage] nvarchar(10) NULL,
	[Comp_Records] nvarchar(10) NULL,
	[Corrective] nvarchar(max) NULL,
    [BuildingType] varchar(256) NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
    CONSTRAINT [PK_T_INSP_ASSESS_MECHANICAL] PRIMARY KEY ([ASSESSMENT_IDX]),
    CONSTRAINT [FK_T_INSP_ASSESSMENTS_M] FOREIGN KEY ([SITE_IDX]) REFERENCES [T_INSP_SITES] ([SITE_IDX]) ON UPDATE CASCADE ON DELETE CASCADE
);
--alter table [T_INSP_ASSESS_MECHANICAL] add [BuildingType] varchar(256) NULL;



CREATE TABLE [T_INSP_ASSESS_FIRE] 
(
	[ASSESSMENT_IDX] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[SITE_IDX] uniqueidentifier NOT NULL,
	[ASSESSMENT_DT] datetime2(0) NOT NULL,
	[ASSESSED_BY] nvarchar(100) NULL,
	[OBJECTID] int NULL,
	[GlobalID] uniqueidentifier NULL,
	[Title] varchar(50) NULL,

	[FacContact] varchar(80) NULL,
	[Compliance] varchar(10) NULL,
	[Corrective] nvarchar(max) NULL,

	[Doc_FireAlarm] varchar(10) NULL,
	[Doc_FireSprinkler5] varchar(10) NULL,
	[Doc_FireSprinklerAnn] varchar(10) NULL,
	[Doc_EmergPwr] varchar(10) NULL,
	[Doc_EmergLight] varchar(10) NULL,
	[Doc_KitchenHoodSys] varchar(10) NULL,
	[Doc_Generator] varchar(10) NULL,
	[Doc_EvacDrill] varchar(10) NULL,
	
	[FireDept_Address] varchar(10) NULL,
	[FireDept_AccessKeys] varchar(10) NULL,
	[FireDept_Access] varchar(10) NULL,
	[FireDept_Valves] varchar(10) NULL,
	[Egress_Path] varchar(10) NULL,
	[Egress_Signs] varchar(10) NULL,
	[Egress_Latch] varchar(10) NULL,
	[Egress_DoorsOpen] varchar(10) NULL,
	[Egress_DoorsKey] varchar(10) NULL,
	[Egress_Evac] varchar(10) NULL,
	[Egress_Storage] varchar(10) NULL,
	[Storage_Housekeeping] varchar(10) NULL,
	[Storage_Cylinders] varchar(10) NULL,
	[Storage_Sprinklers] varchar(10) NULL,
	[Storage_Combust] varchar(10) NULL,
	[Storage_OilRags] varchar(10) NULL,
	[Storage_Food] varchar(10) NULL,
	[ELec_Covers] varchar(10) NULL,
	[Elec_Panels] varchar(10) NULL,
	[Elec_Clearance] varchar(10) NULL,
	[Elec_Cords] varchar(10) NULL,
	[Elec_NoCord] varchar(10) NULL,
	[Elec_Appliances] varchar(10) NULL,
	[Elec_Adapters] varchar(10) NULL,
	[Elec_GFI] varchar(10) NULL,
	[Elec_Heater] varchar(10) NULL,
	[Hazmat_Quantities] varchar(10) NULL,
	[Hazmat_Liquids] varchar(10) NULL,
	[Hazmat_Shelving] varchar(10) NULL,
	[Hazmat_Tanks] varchar(10) NULL,
	[Hazmat_EmptyTanks] varchar(10) NULL,
	[Hazmat_SDS] varchar(10) NULL,
	[Hazmat_704] varchar(10) NULL,
	[Fuel_Valves] varchar(10) NULL,
	[Fuel_Extinguisher] varchar(10) NULL,
	[Fuel_Signs] varchar(10) NULL,
	[Fuel_Materials] varchar(10) NULL,
	[Vent_Caps] varchar(10) NULL,
	[Vent_PullStation] varchar(10) NULL,
	[Vent_Exhaust] varchar(10) NULL,
	[Vent_GasValve] varchar(10) NULL,
	[Vent_Gauge] varchar(10) NULL,
	[Vent_Hydro] varchar(100) NULL,
	[Vent_BrandDate] varchar(50) NULL,
	[Vent_Cleanliness] varchar(10) NULL,
	[Vent_Extinguishers] varchar(10) NULL,
	[Vent_ServiceDate] datetime NULL,
	[FireExt_Doc] varchar(10) NULL,
	[FireExt_Mounted] varchar(10) NULL,
	[FireExt_Collar] varchar(10) NULL,
	[FireExt_Sys] varchar(10) NULL,
	[Risers_Inspected] varchar(100) NULL,
	[Risers_Tagged] varchar(100) NULL,
	[Risers_Sprinkler] varchar(100) NULL,
	[Risers_Deficiencies] varchar(100) NULL,
	[Risers_Plate] varchar(100) NULL,
	[Risers_Gauges] varchar(100) NULL,
	[Sprinkler_Cond] varchar(20) NULL,
	[Ceiling_Type] varchar(20) NULL,
	[Ceiling_Condition] varchar(20) NULL,
	[AED] varchar(10) NULL,
	[AED_Expiration] datetime NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
	[Facility_Use] VARCHAR(100) NULL,
    CONSTRAINT [PK_T_INSP_ASSESS_FIRE] PRIMARY KEY ([ASSESSMENT_IDX]),
    CONSTRAINT [FK_T_INSP_ASSESSMENTS_F] FOREIGN KEY ([SITE_IDX]) REFERENCES [T_INSP_SITES] ([SITE_IDX]) ON UPDATE CASCADE ON DELETE CASCADE
);

--ALTER TABLE [T_INSP_ASSESS_FIRE] ALTER COLUMN [Vent_BrandDate] VARCHAR(50) NULL;
--ALTER TABLE [T_INSP_ASSESS_FIRE] add [Facility_Use] VARCHAR(100) NULL;
--ALTER TABLE [T_INSP_ASSESS_FIRE] alter column [Vent_Hydro] VARCHAR(200) NULL;
--ALTER TABLE [T_INSP_ASSESS_FIRE] alter column [Ceiling_Type] VARCHAR(50) NULL;

CREATE TABLE [T_INSP_ASSESS_BUILDING] 
(
	[ASSESSMENT_IDX] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[SITE_IDX] uniqueidentifier NOT NULL,
	[ASSESSMENT_DT] datetime2(0) NOT NULL,
	[ASSESSED_BY] nvarchar(100) NULL,
	[OBJECTID] int NULL,
	[GlobalID] uniqueidentifier NULL,
	[Title] varchar(50) NULL,

	[Walkway] varchar(10) NULL,
	[Windows] varchar(10) NULL,
	[Doors] varchar(10) NULL,
	[Roof] varchar(10) NULL,
	[Foundation] varchar(10) NULL,
	[Exterior] varchar(10) NULL,
	[Floor] varchar(10) NULL,
	[Walls] varchar(10) NULL,
	[Ceiling] varchar(10) NULL,
	[Stairs] varchar(10) NULL,
	[Panic] varchar(10) NULL,
	[Closures] varchar(10) NULL,
	[Notes] varchar(max) NULL,
	[BuildingType] varchar(256) NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
    CONSTRAINT [PK_T_INSP_ASSESS_BUILDING] PRIMARY KEY ([ASSESSMENT_IDX]),
    CONSTRAINT [FK_T_INSP_ASSESSMENTS_B] FOREIGN KEY ([SITE_IDX]) REFERENCES [T_INSP_SITES] ([SITE_IDX]) ON UPDATE CASCADE ON DELETE CASCADE
);

GO

--alter table [T_INSP_ASSESS_BUILDING] add [BuildingType] varchar(256) NULL;




CREATE TABLE [T_INSP_ASSESS_LIFE] 
(
	[ASSESSMENT_IDX] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[SITE_IDX] uniqueidentifier NOT NULL,
	[ASSESSMENT_DT] datetime2(0) NOT NULL,
	[ASSESSED_BY] nvarchar(100) NULL,
	[OBJECTID] int NULL,
	[GlobalID] uniqueidentifier NULL,
	[Title] varchar(50) NULL,

	[Walkway] varchar(10) NULL,
	[Windows] varchar(10) NULL,
	[Doors] varchar(10) NULL,
	[Roof] varchar(10) NULL,
	[Foundation] varchar(10) NULL,
	[Exterior] varchar(10) NULL,
	[AddressVisible] varchar(10) NULL,
	[Floor] varchar(10) NULL,
	[Walls] varchar(10) NULL,
	[Ceiling] varchar(10) NULL,
	[Stairs] varchar(10) NULL,
	[Panic] varchar(10) NULL,
	[Closures] varchar(10) NULL,
	[Notes] varchar(max) NULL,
	[BuildingType] varchar(256) NULL,
	Fire_FireAlarm varchar(10) NULL,
	Fire_Sprinklers1 varchar(10) NULL,
	Fire_Sprinklers5 varchar(10) NULL,
	Fire_Plate varchar(10) NULL,
	Fire_EmergLight varchar(10) NULL,
	Fire_Exit varchar(10) NULL,
	Fire_KitchenHood varchar(10) NULL,
	Fire_HoodSup varchar(10) NULL,
	Fire_EvacDrill varchar(10) NULL,
	Fire_Evac varchar(10) NULL,
	Fire_Storage varchar(10) NULL,
	Fire_Piled varchar(10) NULL,
	Fire_Food varchar(10) NULL,
	Fire_Hazard varchar(10) NULL,
	Fire_SDS varchar(10) NULL,
	Fire_Extinguisher varchar(10) NULL,
	Fire_ExtinAnnual varchar(10) NULL,
	Fire_Notes varchar(max) NULL,
	Plum_Water varchar(10) NULL,
	Plum_Drain varchar(10) NULL,
	Plum_Sink varchar(10) NULL,
	Plum_Toilet varchar(10) NULL,
	Plum_Tub varchar(10) NULL,
	Plum_Dish varchar(10) NULL,
	Plum_Heater varchar(10) NULL,
	Plum_Backflow varchar(10) NULL,
	Plum_Notes varchar(max) NULL,
	Elec_Hazard varchar(10) NULL,
	Elec_Panel varchar(10) NULL,
	Elec_Label varchar(10) NULL,
	Elec_Obstruct varchar(10) NULL,
	Elec_Outlet varchar(10) NULL,
	Elec_Lighting varchar(10) NULL,
	Elec_Surge varchar(10) NULL,
	Elec_Ground varchar(10) NULL,
	Elec_Sign varchar(10) NULL,
	Elec_Notes varchar(max) NULL,
	Mech_Filters varchar(10) NULL,
	Mech_Register varchar(10) NULL,
	Mech_Equip varchar(10) NULL,
	Mech_VentAir varchar(10) NULL,
	Mech_VentSyst varchar(10) NULL,
	Mech_Pipe varchar(10) NULL,
	Mech_Therm varchar(10) NULL,
	Mech_Duct varchar(10) NULL,
	Mech_Notes varchar(max) NULL,
	[MODIFY_USER_ID] nvarchar(450) NULL,
	[MODIFY_DT] datetime2(0) NULL,
    CONSTRAINT [PK_T_INSP_ASSESS_LIFE] PRIMARY KEY ([ASSESSMENT_IDX]),
    CONSTRAINT [FK_T_INSP_ASSESSMENTS_LF] FOREIGN KEY ([SITE_IDX]) REFERENCES [T_INSP_SITES] ([SITE_IDX]) ON UPDATE CASCADE ON DELETE CASCADE
);

GO

CREATE OR ALTER VIEW V_INSP_ASSESSMENTS as
select YEAR(ASS.[ASSESSMENT_DT]) as YR, S.SITE_IDX, S.SITE_ID, S.SITE_NAME, 'PLUMBING' as INSP_TYP, ASS.ASSESSMENT_IDX, ASS.OBJECTID, ASS.[ASSESSMENT_DT], ASS.ASSESSED_BY , '' as BUILDING_TYPE
from T_PRT_SITES S
inner join T_INSP_SITES I on S.SITE_IDX=I.SITE_IDX
inner join T_INSP_ASSESS_PLUMBING ASS on I.SITE_IDX=ASS.SITE_IDX
UNION ALL
select YEAR(ASS.[ASSESSMENT_DT]) as YR, S.SITE_IDX, S.SITE_ID, S.SITE_NAME, 'ELECTRICAL' as INSP_TYP, ASS.ASSESSMENT_IDX, ASS.OBJECTID, ASS.[ASSESSMENT_DT], ASS.ASSESSED_BY, ASS.BuildingType as BUILDING_TYPE
from T_PRT_SITES S
inner join T_INSP_SITES I on S.SITE_IDX=I.SITE_IDX
inner join T_INSP_ASSESS_ELECTRICAL ASS on I.SITE_IDX=ASS.SITE_IDX
UNION ALL
select YEAR(ASS.[ASSESSMENT_DT]) as YR, S.SITE_IDX, S.SITE_ID, S.SITE_NAME, 'MECHANICAL' as INSP_TYP, ASS.ASSESSMENT_IDX, ASS.OBJECTID, ASS.[ASSESSMENT_DT], ASS.ASSESSED_BY, ASS.BuildingType as BUILDING_TYPE 
from T_PRT_SITES S
inner join T_INSP_SITES I on S.SITE_IDX=I.SITE_IDX
inner join T_INSP_ASSESS_MECHANICAL ASS on I.SITE_IDX=ASS.SITE_IDX
UNION ALL
select YEAR(ASS.[ASSESSMENT_DT]) as YR, S.SITE_IDX, S.SITE_ID, S.SITE_NAME, 'FIRE' as INSP_TYP, ASS.ASSESSMENT_IDX, ASS.OBJECTID, ASS.[ASSESSMENT_DT], ASS.ASSESSED_BY, ASS.Facility_Use as BUILDING_TYPE
from T_PRT_SITES S
inner join T_INSP_SITES I on S.SITE_IDX=I.SITE_IDX
inner join T_INSP_ASSESS_FIRE ASS on I.SITE_IDX=ASS.SITE_IDX
UNION ALL
select YEAR(ASS.[ASSESSMENT_DT]) as YR, S.SITE_IDX, S.SITE_ID, S.SITE_NAME, 'BUILDING' as INSP_TYP, ASS.ASSESSMENT_IDX, ASS.OBJECTID, ASS.[ASSESSMENT_DT], ASS.ASSESSED_BY, ASS.BuildingType as BUILDING_TYPE
from T_PRT_SITES S
inner join T_INSP_SITES I on S.SITE_IDX=I.SITE_IDX
inner join T_INSP_ASSESS_BUILDING ASS on I.SITE_IDX=ASS.SITE_IDX
UNION ALL
select YEAR(ASS.[ASSESSMENT_DT]) as YR, S.SITE_IDX, S.SITE_ID, S.SITE_NAME, 'LIFE' as INSP_TYP, ASS.ASSESSMENT_IDX, ASS.OBJECTID, ASS.[ASSESSMENT_DT], ASS.ASSESSED_BY, ASS.BuildingType as BUILDING_TYPE
from T_PRT_SITES S
inner join T_INSP_SITES I on S.SITE_IDX=I.SITE_IDX
inner join T_INSP_ASSESS_LIFE ASS on I.SITE_IDX=ASS.SITE_IDX