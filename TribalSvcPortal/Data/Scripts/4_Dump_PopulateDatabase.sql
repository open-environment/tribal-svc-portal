﻿--******REF DOC TYPES
INSERT INTO T_PRT_REF_DOC_TYPE (DOC_TYPE, DOC_TYPE_DESC, ACT_IND,CREATE_DT) VALUES ('Open Dump-Assess File', 'Files associated with an Open Dump Assessment', 1, GetDate());
INSERT INTO T_PRT_REF_DOC_TYPE (DOC_TYPE, DOC_TYPE_DESC, ACT_IND,CREATE_DT) VALUES ('Open Dump-Assess Photo', 'Photos associated with an Open Dump Assessment', 1, GetDate());


--******REFERENCE DATA CATEGORIES
INSERT INTO T_OD_REF_DATA_CATEGORIES (REF_DATA_CAT_NAME, REF_DATA_CAT_DESC) VALUES ('Site Setting', 'Indicates the setting of the site');
INSERT INTO T_OD_REF_DATA_CATEGORIES (REF_DATA_CAT_NAME, REF_DATA_CAT_DESC) VALUES ('Assessment Type', 'Type of Assessment/Inspaction');
INSERT INTO T_OD_REF_DATA_CATEGORIES (REF_DATA_CAT_NAME, REF_DATA_CAT_DESC) VALUES ('Community', 'Dump site community');


--***** REFERENCE DATA: SITE SETTING
INSERT INTO T_OD_REF_DATA (REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('Urban', 'Site Setting', GETDATE());
INSERT INTO T_OD_REF_DATA (REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('Rural', 'Site Setting', GETDATE());


--***** REFERENCE DATA: ASSESSMENT TYPE
INSERT INTO T_OD_REF_DATA (REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('Initial Assessment', 'Assessment Type', GETDATE());
INSERT INTO T_OD_REF_DATA (REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('Annual Review', 'Assessment Type', GETDATE());
INSERT INTO T_OD_REF_DATA (REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('Final Review', 'Assessment Type', GETDATE());


--***** REFERENCE DATA: COMMUNITY
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'BACONE (OK51671)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'BEARDEN (OK54705)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'BEGGS (OK56732)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'BIXBY (OK72936)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'BOLEY (OK54706)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'BOWDEN (OK19275)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'BOYNTON (OK51672)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'BRISTOW (OK19276)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'BROKEN ARROW (OK72937)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'CASTLE (OK54707)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'CHECOTAH (OK46612)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'CLARKSVILLE (OK73153)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'CLEARVIEW (OK54708)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'COUNCIL HILL (OK51675)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'COWETA (OK73950)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'CREEK-CO (OK19999)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'CROMWELL (OK67886)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'DEPEW (OK19277)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'DEWAR (OK56733)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'DRUMRIGHT (OK19278)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'DUSTIN (OK32424)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'EUFAULA (OK46613)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'EUFAULA BIA (OK46614)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'FAME (OK46615)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'GLENPOOL (OK72939)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'HANNA (OK46616)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'HASKELL (OK51677)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'HENRYETTA (OK56734)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'HITCHITA (OK46617)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'HOFFMAN (OK56735)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'HOLDENVILLE (OK32426)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'HUGHES-CO (OK32999)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'INOLA (OK66880)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'JENKS (OK72940)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'KELLYVILLE (OK19279)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'KIEFER (OK19280)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'LAMAR (OK32427)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'LENNA (OK46618)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'MANNFORD (OK19281)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'MASON (OK54709)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'MAYES-CO (OK49999)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'MAZIE (OK49655)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'MCINTOSH-CO (OK46999)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'MILFAY (OK19282)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'MORRIS (OK56736)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'MOUNDS (OK19283)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'MUSKOGEE (OK51679)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'MUSKOGEE-CO (OK51999)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'OAKHURST (OK72942)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'OILTON (OK19284)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'OKEMAH (OK54710)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'OKFUSKEE-CO (OK54999)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'OKLA STATE (OK99999)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'OKMULGEE (OK56737)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'OKMULGEE-CO (OK56999)c','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'OKTAHA (OK51680)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'PADEN (OK54711)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'PHAROAH (OK54712)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'PIERCE (OK46619)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'PORTER (OK73952)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'PRESTON (OK56738)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'RED BIRD (OK73953)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'RENITESVILLE (OK46620)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'ROGERS-CO (OK66999)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'SAND SPRINGS (OK72944)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'SAPULPA (OK19285)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'SCHULTER (OK56739)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'SEMINOLE-CO (OK67999)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'SHAMROCK (OK19286)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'SLICK (OK19287)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'SPAULDING (OK32428)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'STIDHAM (OK46621)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'TAFT (OK51682)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'TULLAHASSEE (OK73954)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'TULSA - RURAL (OK72948)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'TULSA - URBAN (OK72947)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'TULSA-CO (OK72999)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'TWIN HILLS (OK56741)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'VERNON (OK46622)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'WAGONER (OK73955)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'WAGONER-CO (OK73999)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'WAINRIGHT (OK51683)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'WELEETKA (OK54713)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'WELTY (OK54714)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'WETUMKA (OK32430)','Community', GETDATE());
INSERT INTO T_OD_REF_DATA (ORG_ID, REF_DATA_VAL, REF_DATA_CAT_NAME, CREATE_DT) VALUES ('MCNCREEK', 'YEAGER (OK32431)','Community', GETDATE());



--******FACTORS
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Rainfall','Low (<10 in/yr)',1);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Rainfall','Medium (10-25 in/yr)',2);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Rainfall','High (>25 in/yr)',3);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Drainage','Site drainage protects ground or surface water',1);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Drainage','Limited ponding, drainage effects are largely neutral',3);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Drainage','Site drainage increases ground or surface water contamination',6);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Flooding','No potential for flooding',1);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Flooding','Debris movement from flooding unlikely',2);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Flooding','Debris movement from flooding likely',3);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Burning','Burning does not occur',1);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Burning','Burning less frequently than weekly',2);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Burning','Burning more frequently than weekly',4);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Access','Effectively controlled access',1);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Access','Ineffective controls or poorly restricted access',2);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Access','Unrestricted access',4);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Concern','No concern voiced',1);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Concern','Little concern voiced by the public',2);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Concern','Concern frequently voiced by the public',3);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Aquifer','Greater than 600 feet',1);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Aquifer','51-599 feet',3);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Aquifer','Less than 50 feet',6);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Surface Water','Greater than 1,000 feet',1);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Surface Water','51-1,000 feet',2);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Surface Water','Less than 50 feet',4);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Homes','Greater than 5,000 feet',1);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Homes','1,000-5,000 feet',3);
INSERT INTO T_OD_REF_THREAT_FACTORS (THREAT_FACTOR_TYPE, THREAT_FACTOR_NAME, THREAT_FACTOR_SCORE) values ('Homes','Less than 1,000 feet',6);



--******CLEANUP ASSETS
INSERT INTO T_OD_REF_CLEANUP_ASSETS (REF_ASSET_NAME) VALUES ('Labor - Supervisor');
INSERT INTO T_OD_REF_CLEANUP_ASSETS (REF_ASSET_NAME) VALUES ('Labor - Equipment Operator');
INSERT INTO T_OD_REF_CLEANUP_ASSETS (REF_ASSET_NAME) VALUES ('Labor - Driver');
INSERT INTO T_OD_REF_CLEANUP_ASSETS (REF_ASSET_NAME) VALUES ('Labor - Other');
INSERT INTO T_OD_REF_CLEANUP_ASSETS (REF_ASSET_NAME) VALUES ('Equipment - Dump Truck');
INSERT INTO T_OD_REF_CLEANUP_ASSETS (REF_ASSET_NAME) VALUES ('Equipment - Front-End Loader (4-cy)');
INSERT INTO T_OD_REF_CLEANUP_ASSETS (REF_ASSET_NAME) VALUES ('Equipment - Compactor');
INSERT INTO T_OD_REF_CLEANUP_ASSETS (REF_ASSET_NAME) VALUES ('Equipment - Shredder');
INSERT INTO T_OD_REF_CLEANUP_ASSETS (REF_ASSET_NAME) VALUES ('Equipment - 40-cy Container');
INSERT INTO T_OD_REF_CLEANUP_ASSETS (REF_ASSET_NAME) VALUES ('Equipment - Other');


--******WASTE CATEGORIES
INSERT INTO T_OD_REF_WASTE_TYPE_CAT(REF_WASTE_TYPE_CAT) VALUES ('Regular');
INSERT INTO T_OD_REF_WASTE_TYPE_CAT(REF_WASTE_TYPE_CAT) VALUES ('Appliances');
INSERT INTO T_OD_REF_WASTE_TYPE_CAT(REF_WASTE_TYPE_CAT) VALUES ('Tires');


--******WASTE CATEGORY CLEANUP DEFAULTS
INSERT INTO T_OD_REF_WASTE_TYPE_CAT_CLEANUP(REF_WASTE_TYPE_CAT,REF_ASSET_NAME,PROCESS_RATE_PER_HR,PROCESS_RATE_UNIT,ASSET_HOURLY_RATE,ASSET_COUNT) values ('Regular','Labor - Supervisor',28,'cu-yd/hr',23.45,1);
INSERT INTO T_OD_REF_WASTE_TYPE_CAT_CLEANUP(REF_WASTE_TYPE_CAT,REF_ASSET_NAME,PROCESS_RATE_PER_HR,PROCESS_RATE_UNIT,ASSET_HOURLY_RATE,ASSET_COUNT) values ('Regular','Labor - Equipment Operator',28,'cu-yd/hr',27.20,1);
INSERT INTO T_OD_REF_WASTE_TYPE_CAT_CLEANUP(REF_WASTE_TYPE_CAT,REF_ASSET_NAME,PROCESS_RATE_PER_HR,PROCESS_RATE_UNIT,ASSET_HOURLY_RATE,ASSET_COUNT) values ('Regular','Labor - Driver',28,'cu-yd/hr',22.10,2);
INSERT INTO T_OD_REF_WASTE_TYPE_CAT_CLEANUP(REF_WASTE_TYPE_CAT,REF_ASSET_NAME,PROCESS_RATE_PER_HR,PROCESS_RATE_UNIT,ASSET_HOURLY_RATE,ASSET_COUNT) values ('Regular','Equipment - Dump Truck',28,'cu-yd/hr',59.06,2);
INSERT INTO T_OD_REF_WASTE_TYPE_CAT_CLEANUP(REF_WASTE_TYPE_CAT,REF_ASSET_NAME,PROCESS_RATE_PER_HR,PROCESS_RATE_UNIT,ASSET_HOURLY_RATE,ASSET_COUNT) values ('Regular','Equipment - Front-End Loader (4-cy)',28,'cu-yd/hr',72.11,1);
INSERT INTO T_OD_REF_WASTE_TYPE_CAT_CLEANUP(REF_WASTE_TYPE_CAT,REF_ASSET_NAME,PROCESS_RATE_PER_HR,PROCESS_RATE_UNIT,ASSET_HOURLY_RATE,ASSET_COUNT) values ('Appliances','Labor - Supervisor',12,'units/hr',23.45,1);
INSERT INTO T_OD_REF_WASTE_TYPE_CAT_CLEANUP(REF_WASTE_TYPE_CAT,REF_ASSET_NAME,PROCESS_RATE_PER_HR,PROCESS_RATE_UNIT,ASSET_HOURLY_RATE,ASSET_COUNT) values ('Appliances','Labor - Equipment Operator',12,'units/hr',27.20,1);
INSERT INTO T_OD_REF_WASTE_TYPE_CAT_CLEANUP(REF_WASTE_TYPE_CAT,REF_ASSET_NAME,PROCESS_RATE_PER_HR,PROCESS_RATE_UNIT,ASSET_HOURLY_RATE,ASSET_COUNT) values ('Appliances','Labor - Other',12,'units/hr',21.45,1);
INSERT INTO T_OD_REF_WASTE_TYPE_CAT_CLEANUP(REF_WASTE_TYPE_CAT,REF_ASSET_NAME,PROCESS_RATE_PER_HR,PROCESS_RATE_UNIT,ASSET_HOURLY_RATE,ASSET_COUNT) values ('Appliances','Equipment - Front-End Loader (4-cy)',12,'cu-yd/hr',72.11,1);
INSERT INTO T_OD_REF_WASTE_TYPE_CAT_CLEANUP(REF_WASTE_TYPE_CAT,REF_ASSET_NAME,PROCESS_RATE_PER_HR,PROCESS_RATE_UNIT,ASSET_HOURLY_RATE,ASSET_COUNT,PER_UNIT_IND) values ('Appliances','Equipment - 40-cy Container',50,'units/container',200,1,1);


--***** WASTE TYPES
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Abandoned automobiles', 'Hazard Factor', 5, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Abandoned trailers', 'Hazard Factor', 1, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Animal carcasses', 'Hazard Factor', 5, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Appliances/white goods', 'Hazard Factor', 5, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Construction and demolition wastes', 'Hazard Factor', 1, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Drums/containers of unknowns/pesticide containers', 'Hazard Factor', 10, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Electronics', 'Hazard Factor', 5, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Fluorescent light bulbs', 'Hazard Factor', 5, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Furniture', 'Hazard Factor', 1, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Lead acid batteries', 'Hazard Factor', 10, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Medical wastes', 'Hazard Factor', 10, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Meth-lab wastes', 'Hazard Factor', 10, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Municipal solid waste', 'Hazard Factor', 5, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Scrap tires', 'Hazard Factor', 1, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Sewage sludge/septic-tank pumpings', 'Hazard Factor', 5, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Suspected asbestos or lead containing materials', 'Hazard Factor', 10, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Suspected RCRA Subtitle C hazardous wastes (treated wood, paints, solvents)', 'Hazard Factor', 10, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Waste oils/oily wastes', 'Hazard Factor', 5, GETDATE());
INSERT INTO T_OD_REF_WASTE_TYPE (REF_WASTE_TYPE_NAME, REF_WASTE_TYPE_CAT, REF_WASTE_HAZFACT_SUBSCORE, MODIFY_DT) VALUES ('Yard/green wastes', 'Hazard Factor', 1, GETDATE());

