using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;  //add each time
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;  //add each time
using Microsoft.Extensions.Configuration;  //add each time
using System.IO; //add each time
using Microsoft.AspNetCore.Hosting;

namespace TribalSvcPortal.Data.Models
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>  //modify each time
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) //add each time
        {
        }

        //*************** TABLES GO HERE **************************************************
        public virtual DbSet<T_PRT_APP_SETTINGS> T_PRT_APP_SETTINGS { get; set; }
        public virtual DbSet<T_PRT_APP_SETTINGS_CUSTOM> T_PRT_APP_SETTINGS_CUSTOM { get; set; }
        public virtual DbSet<T_PRT_CLIENT_ROLES> T_PRT_CLIENT_ROLES { get; set; }
        public virtual DbSet<T_PRT_CLIENTS> T_PRT_CLIENTS { get; set; }
        public virtual DbSet<T_PRT_DOCUMENTS> T_PRT_DOCUMENTS { get; set; }
        public virtual DbSet<T_PRT_ORG_CLIENT_ALIAS> T_PRT_ORG_CLIENT_ALIAS { get; set; }
        public virtual DbSet<T_PRT_ORG_EMAIL_RULE> T_PRT_ORG_EMAIL_RULE { get; set; }
        public virtual DbSet<T_PRT_ORG_USER_CLIENT> T_PRT_ORG_USER_CLIENT { get; set; }
        public virtual DbSet<T_PRT_ORG_USERS> T_PRT_ORG_USERS { get; set; }
        public virtual DbSet<T_PRT_ORGANIZATIONS> T_PRT_ORGANIZATIONS { get; set; }
        public virtual DbSet<T_PRT_REF_DOC_STATUS_TYPE> T_PRT_REF_DOC_STATUS_TYPE { get; set; }
        public virtual DbSet<T_PRT_REF_DOC_TYPE> T_PRT_REF_DOC_TYPE { get; set; }
        public virtual DbSet<T_PRT_REF_SHARE_TYPE> T_PRT_REF_SHARE_TYPE { get; set; }
        public virtual DbSet<T_PRT_REF_UNITS> T_PRT_REF_UNITS { get; set; }
        public virtual DbSet<T_PRT_SITE_INTERESTS> T_PRT_SITE_INTERESTS { get; set; }
        public virtual DbSet<T_PRT_SITES> T_PRT_SITES { get; set; }
        public virtual DbSet<T_PRT_SYS_EMAIL_LOG> T_PRT_SYS_EMAIL_LOG { get; set; }
        public virtual DbSet<T_PRT_SYS_LOG> T_PRT_SYS_LOG { get; set; }

        public virtual DbSet<T_OD_DUMP_ASSESSMENT_CLEANUP> T_OD_DUMP_ASSESSMENT_CLEANUP { get; set; }
        public virtual DbSet<T_OD_DUMP_ASSESSMENT_CONTENT> T_OD_DUMP_ASSESSMENT_CONTENT { get; set; }
        public virtual DbSet<T_OD_DUMP_ASSESSMENT_DOCS> T_OD_DUMP_ASSESSMENT_DOCS { get; set; }
        public virtual DbSet<T_OD_DUMP_ASSESSMENT_RESTORE> T_OD_DUMP_ASSESSMENT_RESTORE { get; set; }
        public virtual DbSet<T_OD_DUMP_ASSESSMENTS> T_OD_DUMP_ASSESSMENTS { get; set; }
        public virtual DbSet<T_OD_REF_CLEANUP_ASSETS> T_OD_REF_CLEANUP_ASSETS { get; set; }
        public virtual DbSet<T_OD_REF_DATA> T_OD_REF_DATA { get; set; }
        public virtual DbSet<T_OD_REF_DATA_CATEGORIES> T_OD_REF_DATA_CATEGORIES { get; set; }
        public virtual DbSet<T_OD_REF_DISPOSAL> T_OD_REF_DISPOSAL { get; set; }
        public virtual DbSet<T_OD_REF_THREAT_FACTORS> T_OD_REF_THREAT_FACTORS { get; set; }
        public virtual DbSet<T_OD_REF_WASTE_TYPE> T_OD_REF_WASTE_TYPE { get; set; }
        public virtual DbSet<T_OD_REF_WASTE_TYPE_CAT> T_OD_REF_WASTE_TYPE_CAT { get; set; }
        public virtual DbSet<T_OD_REF_WASTE_TYPE_CAT_CLEANUP> T_OD_REF_WASTE_TYPE_CAT_CLEANUP { get; set; }
        public virtual DbSet<T_OD_REF_WASTE_TYPE_UNITS> T_OD_REF_WASTE_TYPE_UNITS { get; set; }
        public virtual DbSet<T_OD_SITES> T_OD_SITES { get; set; }

        //**************** END TABLES *******************************************************


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) //add each time
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    //                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
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
                entity.HasKey(e => e.SETTING_IDX);

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.SETTING_DESC).HasMaxLength(500);

                entity.Property(e => e.SETTING_NAME)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SETTING_VALUE).HasMaxLength(200);
            });

            modelBuilder.Entity<T_PRT_APP_SETTINGS_CUSTOM>(entity =>
            {
                entity.HasKey(e => e.SETTING_CUSTOM_IDX);

                entity.Property(e => e.ANNOUNCEMENTS).IsUnicode(false);

                entity.Property(e => e.TERMS_AND_CONDITIONS).IsUnicode(false);
            });

            modelBuilder.Entity<T_PRT_CLIENT_ROLES>(entity =>
            {
                entity.HasKey(e => e.CLIENT_ROLES_IDX);

                entity.Property(e => e.CLIENT_ID)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CLIENT_ROLE_NAME).HasMaxLength(100);

                entity.HasOne(d => d.CLIENT_)
                    .WithMany(p => p.T_PRT_CLIENT_ROLES)
                    .HasForeignKey(d => d.CLIENT_ID)
                    .HasConstraintName("FK_T_PRT_CLIENT_ROLES_C");
            });

            modelBuilder.Entity<T_PRT_CLIENTS>(entity =>
            {
                entity.HasKey(e => e.CLIENT_ID);

                entity.Property(e => e.CLIENT_ID)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CLIENT_GRANT_TYPE)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CLIENT_NAME)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CLIENT_POST_LOGOUT_URI)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CLIENT_REDIRECT_URI)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CLIENT_URL)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_PRT_DOCUMENTS>(entity =>
            {
                entity.HasKey(e => e.DOC_IDX);

                entity.Property(e => e.DOC_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CREATE_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.CREATE_USER_ID).HasMaxLength(450);

                entity.Property(e => e.DOC_AUTHOR)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DOC_COMMENT)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DOC_FILE_TYPE)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.DOC_NAME)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DOC_STATUS_TYPE)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DOC_TYPE)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.ORG_ID)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SHARE_TYPE)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.DOC_STATUS_TYPENavigation)
                    .WithMany(p => p.T_PRT_DOCUMENTS)
                    .HasForeignKey(d => d.DOC_STATUS_TYPE)
                    .HasConstraintName("FK__T_PRT_DOC__DOC_S__33F4B129");

                entity.HasOne(d => d.DOC_TYPENavigation)
                    .WithMany(p => p.T_PRT_DOCUMENTS)
                    .HasForeignKey(d => d.DOC_TYPE)
                    .HasConstraintName("FK__T_PRT_DOC__DOC_T__320C68B7");

                entity.HasOne(d => d.ORG_)
                    .WithMany(p => p.T_PRT_DOCUMENTS)
                    .HasForeignKey(d => d.ORG_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__T_PRT_DOC__ORG_I__3118447E");

                entity.HasOne(d => d.SHARE_TYPENavigation)
                    .WithMany(p => p.T_PRT_DOCUMENTS)
                    .HasForeignKey(d => d.SHARE_TYPE)
                    .HasConstraintName("FK__T_PRT_DOC__SHARE__33008CF0");
            });

            modelBuilder.Entity<T_PRT_ORG_CLIENT_ALIAS>(entity =>
            {
                entity.HasKey(e => new { e.ORG_ID, e.CLIENT_ID });

                entity.Property(e => e.ORG_ID)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CLIENT_ID).HasMaxLength(20);

                entity.Property(e => e.ORG_CLIENT_ALIAS)
                    .IsRequired()
                    .HasMaxLength(30);
            });


            modelBuilder.Entity<T_PRT_ORG_EMAIL_RULE>(entity =>
            {
                entity.HasKey(e => new { e.ORG_ID, e.EMAIL_STRING });

                entity.Property(e => e.ORG_ID)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.EMAIL_STRING)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.ORG_)
                    .WithMany(p => p.T_PRT_ORG_EMAIL_RULE)
                    .HasForeignKey(d => d.ORG_ID)
                    .HasConstraintName("FK__T_PRT_ORG__ORG_I__2F1AED73");
            });


            modelBuilder.Entity<T_PRT_ORG_USER_CLIENT>(entity =>
            {
                entity.HasKey(e => e.ORG_USER_CLIENT_IDX);

                entity.Property(e => e.CLIENT_ID)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CREATE_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.CREATE_USER_ID).HasMaxLength(450);

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.STATUS_IND)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.HasOne(d => d.CLIENT_)
                    .WithMany(p => p.T_PRT_ORG_USER_CLIENT)
                    .HasForeignKey(d => d.CLIENT_ID)
                    .HasConstraintName("FK_T_PRT_ORG_USER_CLIENT_ROLES_C");

                entity.HasOne(d => d.ORG_USER_IDXNavigation)
                    .WithMany(p => p.T_PRT_ORG_USER_CLIENT)
                    .HasForeignKey(d => d.ORG_USER_IDX)
                    .HasConstraintName("FK_T_PRT_ORG_USER_CLIENT_ROLES_U");
            });

            modelBuilder.Entity<T_PRT_ORG_USERS>(entity =>
            {
                entity.HasKey(e => e.ORG_USER_IDX);

                entity.Property(e => e.CREATE_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.CREATE_USER_ID).HasMaxLength(450);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.ORG_ID)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.STATUS_IND)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.HasOne(d => d.ORG_)
                    .WithMany(p => p.T_PRT_ORG_USERS)
                    .HasForeignKey(d => d.ORG_ID)
                    .HasConstraintName("FK_T_PRT_ORG_USERS_T");
            });

            modelBuilder.Entity<T_PRT_ORGANIZATIONS>(entity =>
            {
                entity.HasKey(e => e.ORG_ID);

                entity.Property(e => e.ORG_ID)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ORG_NAME)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<T_PRT_REF_DOC_STATUS_TYPE>(entity =>
            {
                entity.HasKey(e => e.DOC_STATUS_TYPE);

                entity.Property(e => e.DOC_STATUS_TYPE)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CREATE_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.CREATE_USER_ID).HasMaxLength(450);

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);
            });

            modelBuilder.Entity<T_PRT_REF_DOC_TYPE>(entity =>
            {
                entity.HasKey(e => e.DOC_TYPE);

                entity.Property(e => e.DOC_TYPE)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CREATE_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.CREATE_USER_ID).HasMaxLength(450);

                entity.Property(e => e.DOC_TYPE_DESC)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);
            });

            modelBuilder.Entity<T_PRT_REF_SHARE_TYPE>(entity =>
            {
                entity.HasKey(e => e.SHARE_TYPE);

                entity.Property(e => e.SHARE_TYPE)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ACT_IND)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SHARE_DESC)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_PRT_REF_UNITS>(entity =>
            {
                entity.HasKey(e => e.UNIT_MSR_IDX);

                entity.Property(e => e.UNIT_MSR_IDX).ValueGeneratedNever();

                entity.Property(e => e.CREATE_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.CREATE_USER_ID).HasMaxLength(450);

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.UNIT_CONVERSION).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.UNIT_MSR_CAT)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UNIT_MSR_CD)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_PRT_SITE_INTERESTS>(entity =>
            {
                entity.HasKey(e => e.SITE_INTEREST_IDX);

                entity.Property(e => e.SITE_INTEREST_IDX).ValueGeneratedNever();

                entity.Property(e => e.CREATE_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.CREATE_USER_ID).HasMaxLength(450);

                entity.Property(e => e.INTEREST_NAME)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.HasOne(d => d.SITE_IDXNavigation)
                    .WithMany(p => p.T_PRT_SITE_INTERESTS)
                    .HasForeignKey(d => d.SITE_IDX)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_PRT_SITES_INTERESTS_S");
            });

            modelBuilder.Entity<T_PRT_SITES>(entity =>
            {
                entity.HasKey(e => e.SITE_IDX);

                entity.Property(e => e.SITE_IDX).ValueGeneratedNever();

                entity.Property(e => e.CREATE_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.CREATE_USER_ID).HasMaxLength(450);

                entity.Property(e => e.EPA_ID)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LATITUDE).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.LONGITUDE).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.ORG_ID)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SITE_ADDRESS).HasMaxLength(400);

                entity.Property(e => e.SITE_NAME)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ORG_)
                    .WithMany(p => p.T_PRT_SITES)
                    .HasForeignKey(d => d.ORG_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_PRT_SITES_O");
            });

            modelBuilder.Entity<T_PRT_SYS_EMAIL_LOG>(entity =>
            {
                entity.HasKey(e => e.EMAIL_LOG_ID);

                entity.Property(e => e.EMAIL_TYPE)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LOG_CC)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LOG_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.LOG_FROM)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LOG_MSG)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.LOG_SUBJ)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LOG_TO)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_PRT_SYS_LOG>(entity =>
            {
                entity.HasKey(e => e.SYS_LOG_ID);

                entity.Property(e => e.LOG_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.LOG_MSG).HasMaxLength(2000);

                entity.Property(e => e.LOG_TYPE)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LOG_USER_ID).HasMaxLength(450);
            });

            /*************** TABLE COLUMNS END   *******************/


            /*************** OD TABLE COLUMNS START   *******************/
            modelBuilder.Entity<T_OD_DUMP_ASSESSMENT_CLEANUP>(entity =>
            {
                entity.HasKey(e => e.DUMP_ASSESSMENT_CLEANUP_IDX);

                entity.Property(e => e.DUMP_ASSESSMENT_CLEANUP_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CLEANUP_COST).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.REF_ASSET_NAME)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.REF_WASTE_TYPE_CAT)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.DUMP_ASSESSMENTS_IDXNavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENT_CLEANUP)
                    .HasForeignKey(d => d.DUMP_ASSESSMENTS_IDX)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESS_CLEAN_A");
            });

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
                    .HasConstraintName("FK_T_OD_DUMP_ASSESS_DOCS_A");
            });

            modelBuilder.Entity<T_OD_DUMP_ASSESSMENT_RESTORE>(entity =>
            {
                entity.HasKey(e => e.DUMP_ASSESSMENT_RESTORE_IDX);

                entity.Property(e => e.DUMP_ASSESSMENT_RESTORE_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.RESTORE_ACTIVITY)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RESTORE_CAT)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RESTORE_COST).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.DUMP_ASSESSMENTS_IDXNavigation)
                    .WithMany(p => p.T_OD_DUMP_ASSESSMENT_RESTORE)
                    .HasForeignKey(d => d.DUMP_ASSESSMENTS_IDX)
                    .HasConstraintName("FK_T_OD_DUMP_ASSESS_RESTORE_A");
            });

            modelBuilder.Entity<T_OD_DUMP_ASSESSMENTS>(entity =>
            {
                entity.HasKey(e => e.DUMP_ASSESSMENTS_IDX);

                entity.Property(e => e.DUMP_ASSESSMENTS_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AREA_ACRES).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.ASSESSED_BY).HasMaxLength(100);

                entity.Property(e => e.ASSESSMENT_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.COST_CLEANUP_AMT).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.COST_DISPOSAL_AMT).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.COST_RESTORE_AMT).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.COST_SURVEIL_AMT).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.COST_TOTAL_AMT).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.COST_TRANSPORT_AMT).HasColumnType("decimal(12, 2)");

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
                    .HasConstraintName("FK__T_OD_REF___REF_D__13DCE752");
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

                entity.Property(e => e.DENSITY_LBS_CUYD).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.DENSITY_LBS_UNIT).HasColumnType("decimal(18, 5)");

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
                    .HasConstraintName("FK_T_OD_REF_WASTE_TYPE_CAT_CLEANUP_A");

                entity.HasOne(d => d.REF_WASTE_TYPE_CATNavigation)
                    .WithMany(p => p.T_OD_REF_WASTE_TYPE_CAT_CLEANUP)
                    .HasForeignKey(d => d.REF_WASTE_TYPE_CAT)
                    .HasConstraintName("FK_T_OD_REF_WASTE_TYPE_CAT_CLEANUP_C");
            });

            modelBuilder.Entity<T_OD_REF_WASTE_TYPE_UNITS>(entity =>
            {
                entity.HasKey(e => new { e.REF_WASTE_TYPE_IDX, e.UNIT_MSR_IDX });

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.HasOne(d => d.REF_WASTE_TYPE_IDXNavigation)
                    .WithMany(p => p.T_OD_REF_WASTE_TYPE_UNITS)
                    .HasForeignKey(d => d.REF_WASTE_TYPE_IDX)
                    .HasConstraintName("FK_T_OD_REF_WASTE_TYPE_UNITS_W");
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

            /*************** TABLE COLUMNS END   *******************/

        }
    }
}
