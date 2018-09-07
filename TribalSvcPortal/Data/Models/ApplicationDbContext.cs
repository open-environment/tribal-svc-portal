using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity;  //add each time
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;  //add each time
using Microsoft.Extensions.Configuration;  //add each time
using System.IO; //add each time

namespace TribalSvcPortal.Data.Models
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>  //modify each time
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) //add each time
            : base(options)
        {
        }

        //*************** TABLES GO HERE **************************************************
        public virtual DbSet<T_PRT_APP_SETTINGS> T_PRT_APP_SETTINGS { get; set; }
        public virtual DbSet<T_PRT_APP_SETTINGS_CUSTOM> T_PRT_APP_SETTINGS_CUSTOM { get; set; }
        public virtual DbSet<T_PRT_CLIENT_ROLES> T_PRT_CLIENT_ROLES { get; set; }
        public virtual DbSet<T_PRT_CLIENTS> T_PRT_CLIENTS { get; set; }
        public virtual DbSet<T_PRT_DOCUMENTS> T_PRT_DOCUMENTS { get; set; }
        public virtual DbSet<T_PRT_ORG_CLIENT_ALIAS> T_PRT_ORG_CLIENT_ALIAS { get; set; }
        public virtual DbSet<T_PRT_ORG_USER_CLIENT> T_PRT_ORG_USER_CLIENT { get; set; }
        public virtual DbSet<T_PRT_ORG_USERS> T_PRT_ORG_USERS { get; set; }
        public virtual DbSet<T_PRT_ORGANIZATIONS> T_PRT_ORGANIZATIONS { get; set; }
        public virtual DbSet<T_PRT_REF_DOC_STATUS_TYPE> T_PRT_REF_DOC_STATUS_TYPE { get; set; }
        public virtual DbSet<T_PRT_REF_DOC_TYPE> T_PRT_REF_DOC_TYPE { get; set; }
        public virtual DbSet<T_PRT_REF_SHARE_TYPE> T_PRT_REF_SHARE_TYPE { get; set; }
        public virtual DbSet<T_PRT_SITE_INTERESTS> T_PRT_SITE_INTERESTS { get; set; }
        public virtual DbSet<T_PRT_SITES> T_PRT_SITES { get; set; }
        public virtual DbSet<T_PRT_SYS_EMAIL_LOG> T_PRT_SYS_EMAIL_LOG { get; set; }
        public virtual DbSet<T_PRT_SYS_LOG> T_PRT_SYS_LOG { get; set; }  

        public virtual DbSet<T_OD_DUMP_ASSESSMENT_DOCS> T_OD_DUMP_ASSESSMENT_DOCS { get; set; }
        public virtual DbSet<T_OD_DUMP_ASSESSMENTS> T_OD_DUMP_ASSESSMENTS { get; set; }
        public virtual DbSet<T_OD_REF_DATA> T_OD_REF_DATA { get; set; }
        public virtual DbSet<T_OD_REF_DATA_CATEGORIES> T_OD_REF_DATA_CATEGORIES { get; set; }
        public virtual DbSet<T_OD_SITES> T_OD_SITES { get; set; }
        //**************** END TABLES *******************************************************


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) //add each time
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  //add each time

            // configure so ASP.NET Identity code knows the new Identity table names
            modelBuilder.Entity<IdentityRole>().ToTable("T_PRT_ROLES"); //add each time
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("T_PRT_USER_TOKENS"); //add each time
            modelBuilder.Entity<ApplicationUser>().ToTable("T_PRT_USERS"); //add each time
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("T_PRT_ROLE_CLAIMS"); //add each time
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("T_PRT_USER_CLAIMS"); //add each time
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("T_PRT_USER_LOGINS"); //add each time
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("T_PRT_USER_ROLES"); //add each time


            /*************** TABLE COLUMNS START *******************/
            modelBuilder.Entity<T_PRT_APP_SETTINGS>(entity =>
            {
                entity.HasKey(e => e.SettingIdx);

                entity.Property(e => e.SettingIdx).HasColumnName("SETTING_IDX");

                entity.Property(e => e.EncryptInd).HasColumnName("ENCRYPT_IND");

                entity.Property(e => e.ModifyDt)
                    .HasColumnName("MODIFY_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifyUserId)
                    .HasColumnName("MODIFY_USER_ID")
                    .HasMaxLength(450);

                entity.Property(e => e.SettingDesc)
                    .HasColumnName("SETTING_DESC")
                    .HasMaxLength(500);

                entity.Property(e => e.SettingName)
                    .IsRequired()
                    .HasColumnName("SETTING_NAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SettingValue)
                    .HasColumnName("SETTING_VALUE")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<T_PRT_APP_SETTINGS_CUSTOM>(entity =>
            {
                entity.HasKey(e => e.SettingCustomIdx);

                entity.Property(e => e.SettingCustomIdx).HasColumnName("SETTING_CUSTOM_IDX");

                entity.Property(e => e.Announcements)
                    .HasColumnName("ANNOUNCEMENTS")
                    .IsUnicode(false);

                entity.Property(e => e.TermsAndConditions)
                    .HasColumnName("TERMS_AND_CONDITIONS")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_PRT_CLIENT_ROLES>(entity =>
            {
                entity.HasKey(e => e.ClientRolesIdx);

                entity.Property(e => e.ClientRolesIdx).HasColumnName("CLIENT_ROLES_IDX");

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasColumnName("CLIENT_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ClientRoleName)
                    .HasColumnName("CLIENT_ROLE_NAME")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.T_PRT_CLIENT_ROLES)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_T_PRT_CLIENT_ROLES_C");
            });

            modelBuilder.Entity<T_PRT_CLIENTS>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.Property(e => e.ClientId)
                    .HasColumnName("CLIENT_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ClientGrantType)
                    .IsRequired()
                    .HasColumnName("CLIENT_GRANT_TYPE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ClientImage).HasColumnName("CLIENT_IMAGE");

                entity.Property(e => e.ClientLocalInd).HasColumnName("CLIENT_LOCAL_IND");

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasColumnName("CLIENT_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientPostLogoutUri)
                    .HasColumnName("CLIENT_POST_LOGOUT_URI")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ClientRedirectUri)
                    .HasColumnName("CLIENT_REDIRECT_URI")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ClientUrl)
                    .HasColumnName("CLIENT_URL")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_PRT_DOCUMENTS>(entity =>
            {
                entity.HasKey(e => e.DocIdx);

                entity.Property(e => e.DocIdx)
                    .HasColumnName("DOC_IDX")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ActInd).HasColumnName("ACT_IND");

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.CreateUseridx).HasColumnName("CREATE_USERIDX");

                entity.Property(e => e.DocAuthor)
                    .HasColumnName("DOC_AUTHOR")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DocComment)
                    .HasColumnName("DOC_COMMENT")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DocContent).HasColumnName("DOC_CONTENT");

                entity.Property(e => e.DocFileType)
                    .HasColumnName("DOC_FILE_TYPE")
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.DocName)
                    .HasColumnName("DOC_NAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DocSize).HasColumnName("DOC_SIZE");

                entity.Property(e => e.DocStatusType)
                    .HasColumnName("DOC_STATUS_TYPE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DocType)
                    .HasColumnName("DOC_TYPE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyDt)
                    .HasColumnName("MODIFY_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifyUseridx).HasColumnName("MODIFY_USERIDX");

                entity.Property(e => e.OrgId)
                    .IsRequired()
                    .HasColumnName("ORG_ID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ShareType)
                    .HasColumnName("SHARE_TYPE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.DocStatusTypeNavigation)
                    .WithMany(p => p.T_PRT_DOCUMENTS)
                    .HasForeignKey(d => d.DocStatusType)
                    .HasConstraintName("FK__T_PRT_DOC__DOC_S__33F4B129");

                entity.HasOne(d => d.DocTypeNavigation)
                    .WithMany(p => p.T_PRT_DOCUMENTS)
                    .HasForeignKey(d => d.DocType)
                    .HasConstraintName("FK__T_PRT_DOC__DOC_T__320C68B7");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.T_PRT_DOCUMENTS)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__T_PRT_DOC__ORG_I__3118447E");

                entity.HasOne(d => d.ShareTypeNavigation)
                    .WithMany(p => p.T_PRT_DOCUMENTS)
                    .HasForeignKey(d => d.ShareType)
                    .HasConstraintName("FK__T_PRT_DOC__SHARE__33008CF0");
            });

            modelBuilder.Entity<T_PRT_ORG_CLIENT_ALIAS>(entity =>
            {
                entity.HasKey(e => new { e.OrgId, e.ClientId });

                entity.Property(e => e.OrgId)
                    .HasColumnName("ORG_ID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ClientId)
                    .HasColumnName("CLIENT_ID")
                    .HasMaxLength(20);

                entity.Property(e => e.OrgClientAlias)
                    .IsRequired()
                    .HasColumnName("ORG_CLIENT_ALIAS")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<T_PRT_ORG_USER_CLIENT>(entity =>
            {
                entity.HasKey(e => e.OrgUserClientIdx);

                entity.Property(e => e.OrgUserClientIdx).HasColumnName("ORG_USER_CLIENT_IDX");

                entity.Property(e => e.AdminInd).HasColumnName("ADMIN_IND");

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasColumnName("CLIENT_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.CreateUserId)
                    .HasColumnName("CREATE_USER_ID")
                    .HasMaxLength(450);

                entity.Property(e => e.ModifyDt)
                    .HasColumnName("MODIFY_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifyUserId)
                    .HasColumnName("MODIFY_USER_ID")
                    .HasMaxLength(450);

                entity.Property(e => e.OrgUserIdx).HasColumnName("ORG_USER_IDX");

                entity.Property(e => e.StatusInd)
                    .IsRequired()
                    .HasColumnName("STATUS_IND")
                    .HasMaxLength(1);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.T_PRT_ORG_USER_CLIENT)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_T_PRT_ORG_USER_CLIENT_ROLES_C");

                entity.HasOne(d => d.OrgUserIdxNavigation)
                    .WithMany(p => p.T_PRT_ORG_USER_CLIENT)
                    .HasForeignKey(d => d.OrgUserIdx)
                    .HasConstraintName("FK_T_PRT_ORG_USER_CLIENT_ROLES_U");
            });

            modelBuilder.Entity<T_PRT_ORG_USERS>(entity =>
            {
                entity.HasKey(e => e.OrgUserIdx);

                entity.Property(e => e.OrgUserIdx).HasColumnName("ORG_USER_IDX");

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.CreateUserId)
                    .HasColumnName("CREATE_USER_ID")
                    .HasMaxLength(450);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.ModifyDt)
                    .HasColumnName("MODIFY_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifyUserId)
                    .HasColumnName("MODIFY_USER_ID")
                    .HasMaxLength(450);

                entity.Property(e => e.OrgAdminInd).HasColumnName("ORG_ADMIN_IND");

                entity.Property(e => e.OrgId)
                    .IsRequired()
                    .HasColumnName("ORG_ID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.StatusInd)
                    .IsRequired()
                    .HasColumnName("STATUS_IND")
                    .HasMaxLength(1);

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.T_PRT_ORG_USERS)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("FK_T_PRT_ORG_USERS_T");
            });

            modelBuilder.Entity<T_PRT_ORGANIZATIONS>(entity =>
            {
                entity.HasKey(e => e.OrgId);

                entity.Property(e => e.OrgId)
                    .HasColumnName("ORG_ID")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.OrgName)
                    .IsRequired()
                    .HasColumnName("ORG_NAME")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<T_PRT_REF_DOC_STATUS_TYPE>(entity =>
            {
                entity.HasKey(e => e.DocStatusType);

                entity.Property(e => e.DocStatusType)
                    .HasColumnName("DOC_STATUS_TYPE")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ActInd).HasColumnName("ACT_IND");

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.CreateUserId)
                    .HasColumnName("CREATE_USER_ID")
                    .HasMaxLength(450);

                entity.Property(e => e.ModifyDt)
                    .HasColumnName("MODIFY_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifyUserId)
                    .HasColumnName("MODIFY_USER_ID")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<T_PRT_REF_DOC_TYPE>(entity =>
            {
                entity.HasKey(e => e.DocType);

                entity.Property(e => e.DocType)
                    .HasColumnName("DOC_TYPE")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ActInd).HasColumnName("ACT_IND");

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.CreateUserId)
                    .HasColumnName("CREATE_USER_ID")
                    .HasMaxLength(450);

                entity.Property(e => e.DocTypeDesc)
                    .HasColumnName("DOC_TYPE_DESC")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyDt)
                    .HasColumnName("MODIFY_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifyUserId)
                    .HasColumnName("MODIFY_USER_ID")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<T_PRT_REF_SHARE_TYPE>(entity =>
            {
                entity.HasKey(e => e.ShareType);

                entity.Property(e => e.ShareType)
                    .HasColumnName("SHARE_TYPE")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ActInd)
                    .HasColumnName("ACT_IND")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ShareDesc)
                    .IsRequired()
                    .HasColumnName("SHARE_DESC")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_PRT_SITE_INTERESTS>(entity =>
            {
                entity.HasKey(e => e.SiteInterestIdx);

                entity.Property(e => e.SiteInterestIdx)
                    .HasColumnName("SITE_INTEREST_IDX")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.CreateUserId)
                    .HasColumnName("CREATE_USER_ID")
                    .HasMaxLength(450);

                entity.Property(e => e.InterestName)
                    .IsRequired()
                    .HasColumnName("INTEREST_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyDt)
                    .HasColumnName("MODIFY_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifyUserId)
                    .HasColumnName("MODIFY_USER_ID")
                    .HasMaxLength(450);

                entity.Property(e => e.SiteIdx).HasColumnName("SITE_IDX");

                entity.HasOne(d => d.SiteIdxNavigation)
                    .WithMany(p => p.T_PRT_SITE_INTERESTS)
                    .HasForeignKey(d => d.SiteIdx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_PRT_SITES_INTERESTS_S");
            });

            modelBuilder.Entity<T_PRT_SITES>(entity =>
            {
                entity.HasKey(e => e.SiteIdx);

                entity.Property(e => e.SiteIdx)
                    .HasColumnName("SITE_IDX")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.CreateUserId)
                    .HasColumnName("CREATE_USER_ID")
                    .HasMaxLength(450);

                entity.Property(e => e.EpaId)
                    .HasColumnName("EPA_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Latitude)
                    .HasColumnName("LATITUDE")
                    .HasColumnType("decimal(18, 5)");

                entity.Property(e => e.Longitude)
                    .HasColumnName("LONGITUDE")
                    .HasColumnType("decimal(18, 5)");

                entity.Property(e => e.ModifyDt)
                    .HasColumnName("MODIFY_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifyUserId)
                    .HasColumnName("MODIFY_USER_ID")
                    .HasMaxLength(450);

                entity.Property(e => e.OrgId)
                    .IsRequired()
                    .HasColumnName("ORG_ID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SiteAddress)
                    .HasColumnName("SITE_ADDRESS")
                    .HasMaxLength(400);

                entity.Property(e => e.SiteName)
                    .IsRequired()
                    .HasColumnName("SITE_NAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.T_PRT_SITES)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_PRT_SITES_O");
            });

            modelBuilder.Entity<T_PRT_SYS_EMAIL_LOG>(entity =>
            {
                entity.HasKey(e => e.EmailLogId);

                entity.Property(e => e.EmailLogId).HasColumnName("EMAIL_LOG_ID");

                entity.Property(e => e.EmailType)
                    .HasColumnName("EMAIL_TYPE")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LogCc)
                    .HasColumnName("LOG_CC")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LogDt)
                    .HasColumnName("LOG_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.LogFrom)
                    .HasColumnName("LOG_FROM")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LogMsg)
                    .HasColumnName("LOG_MSG")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.LogSubj)
                    .HasColumnName("LOG_SUBJ")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LogTo)
                    .HasColumnName("LOG_TO")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_PRT_SYS_LOG>(entity =>
            {
                entity.HasKey(e => e.SysLogId);

                entity.Property(e => e.SysLogId).HasColumnName("SYS_LOG_ID");

                entity.Property(e => e.LogDt)
                    .HasColumnName("LOG_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.LogMsg)
                    .HasColumnName("LOG_MSG")
                    .HasMaxLength(2000);

                entity.Property(e => e.LogType)
                    .HasColumnName("LOG_TYPE")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LogUserId)
                    .HasColumnName("LOG_USER_ID")
                    .HasMaxLength(450);
            });

            /*************** TABLE COLUMNS END   *******************/

            
            /*************** OD TABLE COLUMNS START   *******************/
            modelBuilder.Entity<T_OD_DUMP_ASSESSMENT_DOCS>(entity =>
            {
                entity.HasKey(e => new { e.DumpAssessmentsIdx, e.DocIdx });

                entity.Property(e => e.DumpAssessmentsIdx).HasColumnName("DUMP_ASSESSMENTS_IDX");

                entity.Property(e => e.DocIdx).HasColumnName("DOC_IDX");

                entity.HasOne(d => d.DumpAssessmentsIdxNavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENT_DOCS)
                    .HasForeignKey(d => d.DumpAssessmentsIdx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESS_DOCS_A");
            });

            modelBuilder.Entity<T_OD_DUMP_ASSESSMENTS>(entity =>
            {
                entity.HasKey(e => e.DumpAssessmentsIdx);

                entity.Property(e => e.DumpAssessmentsIdx)
                    .HasColumnName("DUMP_ASSESSMENTS_IDX")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ActiveSiteInd).HasColumnName("ACTIVE_SITE_IND");

                entity.Property(e => e.AssessmentDt)
                    .HasColumnName("ASSESSMENT_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.BurningTakenPlaceInd).HasColumnName("BURNING_TAKEN_PLACE_IND");

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.CreateUserId)
                    .HasColumnName("CREATE_USER_ID")
                    .HasMaxLength(450);

                entity.Property(e => e.InspectedBy)
                    .HasColumnName("INSPECTED_BY")
                    .HasMaxLength(450);

                entity.Property(e => e.InspectionTypeIdx).HasColumnName("INSPECTION_TYPE_IDX");

                entity.Property(e => e.MaintainedInd).HasColumnName("MAINTAINED_IND");

                entity.Property(e => e.ModifyDt)
                    .HasColumnName("MODIFY_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifyUserId)
                    .HasColumnName("MODIFY_USER_ID")
                    .HasMaxLength(450);

                entity.Property(e => e.NoOfStructures)
                    .HasColumnName("NO_OF_STRUCTURES")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasColumnName("NOTES")
                    .HasMaxLength(400);

                entity.Property(e => e.Signage)
                    .HasColumnName("SIGNAGE")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteAccess)
                    .HasColumnName("SITE_ACCESS")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteDescription)
                    .HasColumnName("SITE_DESCRIPTION")
                    .HasMaxLength(450);

                entity.Property(e => e.SiteGeography)
                    .HasColumnName("SITE_GEOGRAPHY")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteIdx).HasColumnName("SITE_IDX");

                entity.HasOne(d => d.InspectionTypeIdxNavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENTS)
                    .HasForeignKey(d => d.InspectionTypeIdx)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESSMENTS_D");

                entity.HasOne(d => d.SiteIdxNavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENTS)
                    .HasForeignKey(d => d.SiteIdx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESSMENTS_S");
            });

            modelBuilder.Entity<T_OD_REF_DATA>(entity =>
            {
                entity.HasKey(e => e.RefDataIdx);

                entity.Property(e => e.RefDataIdx)
                    .HasColumnName("REF_DATA_IDX")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.CreateUserId)
                    .HasColumnName("CREATE_USER_ID")
                    .HasMaxLength(450);

                entity.Property(e => e.ModifyDt)
                    .HasColumnName("MODIFY_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifyUserId)
                    .HasColumnName("MODIFY_USER_ID")
                    .HasMaxLength(450);

                entity.Property(e => e.OrgId)
                    .HasColumnName("ORG_ID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RefDataCatName)
                    .IsRequired()
                    .HasColumnName("REF_DATA_CAT_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RefDataDesc)
                    .HasColumnName("REF_DATA_DESC")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.RefDataName)
                    .IsRequired()
                    .HasColumnName("REF_DATA_NAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserCreateInd).HasColumnName("USER_CREATE_IND");

                entity.HasOne(d => d.RefDataCatNameNavigation)
                    .WithMany(p => p.T_OD_REF_DATA)
                    .HasForeignKey(d => d.RefDataCatName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__T_OD_REF___REF_D__52793849");
            });

            modelBuilder.Entity<T_OD_REF_DATA_CATEGORIES>(entity =>
            {
                entity.HasKey(e => e.RefDataCatName);

                entity.Property(e => e.RefDataCatName)
                    .HasColumnName("REF_DATA_CAT_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.RefDataCatDesc)
                    .HasColumnName("REF_DATA_CAT_DESC")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_OD_SITES>(entity =>
            {
                entity.HasKey(e => e.SiteIdx);

                entity.Property(e => e.SiteIdx)
                    .HasColumnName("SITE_IDX")
                    .ValueGeneratedNever();

                entity.Property(e => e.CommunityIdx).HasColumnName("COMMUNITY_IDX");

                entity.Property(e => e.ReportedBy)
                    .HasColumnName("REPORTED_BY")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReportedOn)
                    .HasColumnName("REPORTED_ON")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.ResponseAction)
                    .IsRequired()
                    .HasColumnName("RESPONSE_ACTION")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteSettingIdx).HasColumnName("SITE_SETTING_IDX");

                entity.HasOne(d => d.CommunityIdxNavigation)
                    .WithMany(p => p.T_OD_SITESCommunityIdxNavigation)
                    .HasForeignKey(d => d.CommunityIdx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_OD_SITE_DTL_C");

                entity.HasOne(d => d.SiteSettingIdxNavigation)
                    .WithMany(p => p.T_OD_SITESSiteSettingIdxNavigation)
                    .HasForeignKey(d => d.SiteSettingIdx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_OD_SITE_DTL_SS");
            });
        
            /*************** TABLE COLUMNS END   *******************/

        }
    }
}
