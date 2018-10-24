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
        public virtual DbSet<T_OD_REF_THREAT_FACTORS> T_OD_REF_THREAT_FACTORS { get; set; }
        public virtual DbSet<T_OD_REF_WASTE_TYPE> T_OD_REF_WASTE_TYPE { get; set; }
        public virtual DbSet<T_OD_DUMP_ASSESSMENT_CONTENT> T_OD_DUMP_ASSESSMENT_CONTENT { get; set; }
        public virtual DbSet<T_OD_REF_DISPOSAL> T_OD_REF_DISPOSAL { get; set; }

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

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

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

                entity.Property(e => e.ModifyUserId).HasColumnName("MODIFY_USER_ID");

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
            modelBuilder.Entity<T_OD_DUMP_ASSESSMENT_CONTENT>(entity =>
            {
                entity.HasKey(e => e.DUMP_ASSESSMENTS_CONTENT_IDX);

                entity.Property(e => e.DUMP_ASSESSMENTS_CONTENT_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.WASTE_AMT).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.WASTE_DISPOSAL_DIST)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.HasOne(d => d.DUMP_ASSESSMENTS_IDXNavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENT_CONTENT)
                    .HasForeignKey(d => d.DUMP_ASSESSMENTS_IDX)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESS_CNT_A");

                entity.HasOne(d => d.REF_WASTE_TYPE_IDXNavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENT_CONTENT)
                    .HasForeignKey(d => d.REF_WASTE_TYPE_IDX)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESS_CNT_D");
            });

            modelBuilder.Entity<T_OD_DUMP_ASSESSMENT_DOCS>(entity =>
            {
                entity.HasKey(e => new { e.DUMP_ASSESSMENTS_IDX, e.DOC_IDX });

                entity.HasOne(d => d.DUMP_ASSESSMENTS_IDXNavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENT_DOCS)
                    .HasForeignKey(d => d.DUMP_ASSESSMENTS_IDX)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESS_DOCS_A");
            });

            modelBuilder.Entity<T_OD_DUMP_ASSESSMENTS>(entity =>
            {
                entity.HasKey(e => e.DUMP_ASSESSMENTS_IDX);

                entity.Property(e => e.DUMP_ASSESSMENTS_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AREA_ACRES).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.ASSESSED_BY).HasMaxLength(100);

                entity.Property(e => e.ASSESSMENT_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.CREATE_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.CREATE_USER_ID).HasMaxLength(450);

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.VOLUME_CU_YD).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.ASSESSMENT_TYPE_IDXNavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENTS)
                    .HasForeignKey(d => d.ASSESSMENT_TYPE_IDX)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESSMENTS_D");

                entity.HasOne(d => d.HF_ACCESS_CONTROLNavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENTSHF_ACCESS_CONTROLNavigation)
                    .HasForeignKey(d => d.HF_ACCESS_CONTROL)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESSMENTS_HFA");

                entity.HasOne(d => d.HF_BURNINGNavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENTSHF_BURNINGNavigation)
                    .HasForeignKey(d => d.HF_BURNING)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESSMENTS_HFB");

                entity.HasOne(d => d.HF_DRAINAGENavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENTSHF_DRAINAGENavigation)
                    .HasForeignKey(d => d.HF_DRAINAGE)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESSMENTS_HFD");

                entity.HasOne(d => d.HF_FENCINGNavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENTSHF_FENCINGNavigation)
                    .HasForeignKey(d => d.HF_FENCING)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESSMENTS_HFFN");

                entity.HasOne(d => d.HF_FLOODINGNavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENTSHF_FLOODINGNavigation)
                    .HasForeignKey(d => d.HF_FLOODING)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESSMENTS_HFF");

                entity.HasOne(d => d.HF_PUBLIC_CONCERNNavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENTSHF_PUBLIC_CONCERNNavigation)
                    .HasForeignKey(d => d.HF_PUBLIC_CONCERN)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESSMENTS_HFP");

                entity.HasOne(d => d.HF_RAINFALLNavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENTSHF_RAINFALLNavigation)
                    .HasForeignKey(d => d.HF_RAINFALL)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESSMENTS_HFR");

                entity.HasOne(d => d.SITE_IDXNavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENTS)
                    .HasForeignKey(d => d.SITE_IDX)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESSMENTS_S");
            });

            modelBuilder.Entity<T_OD_REF_CLEANUP_ASSETS>(entity =>
            {
                entity.HasKey(e => e.REF_ASSET_NAME);

                entity.Property(e => e.REF_ASSET_NAME)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<T_OD_REF_DATA>(entity =>
            {
                entity.HasKey(e => e.REF_DATA_IDX);

                entity.Property(e => e.REF_DATA_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CREATE_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.CREATE_USER_ID).HasMaxLength(450);

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.ORG_ID)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.REF_DATA_CAT_NAME)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.REF_DATA_DESC)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.REF_DATA_VAL)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.REF_DATA_CAT_NAMENavigation)
                    .WithMany(p => p.T_OD_REF_DATA)
                    .HasForeignKey(d => d.REF_DATA_CAT_NAME)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__T_OD_REF___REF_D__1CA7377D");
            });

            modelBuilder.Entity<T_OD_REF_DATA_CATEGORIES>(entity =>
            {
                entity.HasKey(e => e.REF_DATA_CAT_NAME);

                entity.Property(e => e.REF_DATA_CAT_NAME)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.REF_DATA_CAT_DESC)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_OD_REF_THREAT_FACTORS>(entity =>
            {
                entity.HasKey(e => e.THREAT_FACTOR_IDX);

                entity.Property(e => e.THREAT_FACTOR_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.THREAT_FACTOR_NAME)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.THREAT_FACTOR_TYPE)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_OD_REF_WASTE_TYPE>(entity =>
            {
                entity.HasKey(e => e.REF_WASTE_TYPE_IDX);

                entity.Property(e => e.REF_WASTE_TYPE_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.REF_WASTE_TYPE_CAT)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.REF_WASTE_TYPE_NAME)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_OD_REF_WASTE_TYPE_CAT>(entity =>
            {
                entity.HasKey(e => e.REF_WASTE_TYPE_CAT);

                entity.Property(e => e.REF_WASTE_TYPE_CAT)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<T_OD_REF_WASTE_TYPE_CAT_CLEANUP>(entity =>
            {
                entity.HasKey(e => e.REF_WASTE_TYPE_CAT_CLEANUP_IDX);

                entity.Property(e => e.REF_WASTE_TYPE_CAT_CLEANUP_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ASSET_HOURLY_RATE).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ORG_ID)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PROCESS_RATE_PER_HR).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PROCESS_RATE_UNIT)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.REF_ASSET_NAME)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.REF_WASTE_TYPE_CAT)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.REF_ASSET_NAMENavigation)
                    .WithMany(p => p.T_OD_REF_WASTE_TYPE_CAT_CLEANUP)
                    .HasForeignKey(d => d.REF_ASSET_NAME)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_OD_REF_WASTE_TYPE_CAT_CLEANUP_A");

                entity.HasOne(d => d.REF_WASTE_TYPE_CATNavigation)
                    .WithMany(p => p.T_OD_REF_WASTE_TYPE_CAT_CLEANUP)
                    .HasForeignKey(d => d.REF_WASTE_TYPE_CAT)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_OD_REF_WASTE_TYPE_CAT_CLEANUP_C");
            });

            modelBuilder.Entity<T_OD_SITES>(entity =>
            {
                entity.HasKey(e => e.SITE_IDX);

                entity.Property(e => e.SITE_IDX).ValueGeneratedNever();

                entity.Property(e => e.REPORTED_BY)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.REPORTED_ON).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.COMMUNITY_IDXNavigation)
                    .WithMany(p => p.T_OD_SITESCOMMUNITY_IDXNavigation)
                    .HasForeignKey(d => d.COMMUNITY_IDX)
                    .HasConstraintName("FK_T_OD_SITE_DTL_C");

                entity.HasOne(d => d.PF_AQUIFER_VERT_DISTNavigation)
                    .WithMany(p => p.T_OD_SITESPF_AQUIFER_VERT_DISTNavigation)
                    .HasForeignKey(d => d.PF_AQUIFER_VERT_DIST)
                    .HasConstraintName("FK_T_OD_SITE_DTL_AD");

                entity.HasOne(d => d.PF_HOMES_DISTNavigation)
                    .WithMany(p => p.T_OD_SITESPF_HOMES_DISTNavigation)
                    .HasForeignKey(d => d.PF_HOMES_DIST)
                    .HasConstraintName("FK_T_OD_SITE_DTL_HD");

                entity.HasOne(d => d.PF_SURF_WATER_HORIZ_DISTNavigation)
                    .WithMany(p => p.T_OD_SITESPF_SURF_WATER_HORIZ_DISTNavigation)
                    .HasForeignKey(d => d.PF_SURF_WATER_HORIZ_DIST)
                    .HasConstraintName("FK_T_OD_SITE_DTL_SD");

                entity.HasOne(d => d.SITE_SETTING_IDXNavigation)
                    .WithMany(p => p.T_OD_SITESSITE_SETTING_IDXNavigation)
                    .HasForeignKey(d => d.SITE_SETTING_IDX)
                    .HasConstraintName("FK_T_OD_SITE_DTL_SS");
            });


            modelBuilder.Entity<T_OD_REF_DISPOSAL>(entity =>
            {
                entity.HasKey(e => e.REF_DISPOSAL_IDX);

                entity.Property(e => e.REF_DISPOSAL_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DISPOSAL_NAME)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ORG_ID)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PRICE_PER_TON).HasColumnType("decimal(10, 2)");
            });

            /*************** TABLE COLUMNS END   *******************/

        }
    }
}
