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
        private readonly IConfiguration _config;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config) 
            : base(options) //add each time
        {
            _config = config ?? throw new System.ArgumentNullException(nameof(config));
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
        public virtual DbSet<T_PRT_REF_EMAIL_TEMPLATE> T_PRT_REF_EMAIL_TEMPLATE { get; set; }
        public virtual DbSet<T_PRT_REF_SHARE_TYPE> T_PRT_REF_SHARE_TYPE { get; set; }
        public virtual DbSet<T_PRT_REF_UNITS> T_PRT_REF_UNITS { get; set; }
        public virtual DbSet<T_PRT_SITE_INTERESTS> T_PRT_SITE_INTERESTS { get; set; }
        public virtual DbSet<T_PRT_SITES> T_PRT_SITES { get; set; }
        public virtual DbSet<T_PRT_SYS_EMAIL_LOG> T_PRT_SYS_EMAIL_LOG { get; set; }
        public virtual DbSet<T_PRT_SYS_LOG> T_PRT_SYS_LOG { get; set; }


        public virtual DbSet<T_OD_ASSESSMENT_CONTENT> T_OD_ASSESSMENT_CONTENT { get; set; }
        public virtual DbSet<T_OD_ASSESSMENT_DOCS> T_OD_ASSESSMENT_DOCS { get; set; }
        public virtual DbSet<T_OD_ASSESSMENTS> T_OD_ASSESSMENTS { get; set; }
        public virtual DbSet<T_OD_CLEANUP_ACTIVITIES> T_OD_CLEANUP_ACTIVITIES { get; set; }
        public virtual DbSet<T_OD_CLEANUP_CLEANUP_DTL> T_OD_CLEANUP_CLEANUP_DTL { get; set; }
        public virtual DbSet<T_OD_CLEANUP_TRANSPORT_DTL> T_OD_CLEANUP_TRANSPORT_DTL { get; set; }
        public virtual DbSet<T_OD_CLEANUP_DISPOSAL_DTL> T_OD_CLEANUP_DISPOSAL_DTL { get; set; }
        public virtual DbSet<T_OD_CLEANUP_DOCS> T_OD_CLEANUP_DOCS { get; set; }
        public virtual DbSet<T_OD_CLEANUP_PROJECT> T_OD_CLEANUP_PROJECT { get; set; }
        public virtual DbSet<T_OD_REF_CLEANUP_ASSETS> T_OD_REF_CLEANUP_ASSETS { get; set; }
        public virtual DbSet<T_OD_REF_DATA> T_OD_REF_DATA { get; set; }
        public virtual DbSet<T_OD_REF_DATA_CATEGORIES> T_OD_REF_DATA_CATEGORIES { get; set; }
        public virtual DbSet<T_OD_REF_DISPOSAL> T_OD_REF_DISPOSAL { get; set; }
        public virtual DbSet<T_OD_REF_THREAT_FACTORS> T_OD_REF_THREAT_FACTORS { get; set; }
        public virtual DbSet<T_OD_REF_WASTE_TYPE> T_OD_REF_WASTE_TYPE { get; set; }
        public virtual DbSet<T_OD_REF_WASTE_TYPE_CAT> T_OD_REF_WASTE_TYPE_CAT { get; set; }
        public virtual DbSet<T_OD_REF_WASTE_TYPE_CAT_CLEANUP> T_OD_REF_WASTE_TYPE_CAT_CLEANUP { get; set; }
        public virtual DbSet<T_OD_REF_WASTE_TYPE_UNITS> T_OD_REF_WASTE_TYPE_UNITS { get; set; }
        public virtual DbSet<T_OD_SITE_PARCELS> T_OD_SITE_PARCELS { get; set; }
        public virtual DbSet<T_OD_SITES> T_OD_SITES { get; set; }

        public virtual DbSet<V_OD_SITES> V_OD_SITES { get; set; }
        public virtual DbSet<V_OD_ASSESSMENTS> V_OD_ASSESSMENTS { get; set; }

        public virtual DbSet<T_LAW_LAWS> T_LAW_LAWS { get; set; }
        public virtual DbSet<V_INSP_ASSESSMENTS> V_INSP_ASSESSMENTS { get; set; }


        public virtual DbSet<T_INSP_ASSESS_BUILDING> T_INSP_ASSESS_BUILDING { get; set; }
        public virtual DbSet<T_INSP_ASSESS_FIRE> T_INSP_ASSESS_FIRE { get; set; }
        public virtual DbSet<T_INSP_ASSESS_ELECTRICAL> T_INSP_ASSESS_ELECTRICAL { get; set; }
        public virtual DbSet<T_INSP_ASSESS_MECHANICAL> T_INSP_ASSESS_MECHANICAL { get; set; }

        public virtual DbSet<T_INSP_ASSESS_PLUMBING> T_INSP_ASSESS_PLUMBING { get; set; }
        public virtual DbSet<T_INSP_ASSESS_LIFE> T_INSP_ASSESS_LIFE { get; set; }

        public virtual DbSet<T_INSP_SITES> T_INSP_SITES { get; set; }
        //**************** END TABLES *******************************************************


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) //add each time
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
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

                entity.HasOne(d => d.CLIENT)
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

                entity.HasOne(d => d.ORG)
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

                entity.HasOne(d => d.ORG)
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

                entity.HasOne(d => d.ORG)
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

                entity.Property(e => e.WORDPRESS_URL)
                    .HasMaxLength(200);

                entity.Property(e => e.ORG_SEAL)
                .HasMaxLength(50);
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

            modelBuilder.Entity<T_PRT_REF_EMAIL_TEMPLATE>(entity =>
            {
                entity.HasKey(e => e.EMAIL_TEMPLATE_ID);

                entity.Property(e => e.EMAIL_TEMPLATE_NAME)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.EMAIL_TEMPLATE_DESC)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.MSG).IsUnicode(false);

                entity.Property(e => e.SUBJ)
                    .HasMaxLength(200)
                    .IsUnicode(false);
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

                entity.Property(e => e.LAND_STATUS).HasMaxLength(30);

                entity.Property(e => e.LATITUDE).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.LONGITUDE).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.ORG_ID)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RANGE).HasMaxLength(4);

                entity.Property(e => e.SITE_ADDRESS).HasMaxLength(400);

                entity.Property(e => e.SITE_NAME)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TWP).HasMaxLength(3);

                entity.HasOne(d => d.ORG)
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
            modelBuilder.Entity<T_OD_ASSESSMENT_CONTENT>(entity =>
            {
                entity.HasKey(e => e.ASSESSMENT_CONTENT_IDX);

                entity.Property(e => e.ASSESSMENT_CONTENT_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.WASTE_AMT).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.WASTE_WEIGHT_LBS).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.WASTE_DISPOSAL_DIST)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.HasOne(d => d.ASSESSMENT_IDXNavigation)
                    .WithMany(p => p.T_OD_ASSESSMENT_CONTENT)
                    .HasForeignKey(d => d.ASSESSMENT_IDX)
                    .HasConstraintName("FK_T_OD_ASSESS_CNT_A");

                entity.HasOne(d => d.REF_WASTE_TYPE_IDXNavigation)
                    .WithMany(p => p.T_OD_ASSESSMENT_CONTENT)
                    .HasForeignKey(d => d.REF_WASTE_TYPE_IDX)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_OD_ASSESS_CNT_D");
            });

            modelBuilder.Entity<T_OD_ASSESSMENT_DOCS>(entity =>
            {
                entity.HasKey(e => e.DOC_IDX);

                entity.Property(e => e.DOC_IDX).ValueGeneratedNever();

                entity.HasOne(d => d.ASSESSMENT_IDXNavigation)
                    .WithMany(p => p.T_OD_ASSESSMENT_DOCS)
                    .HasForeignKey(d => d.ASSESSMENT_IDX)
                    .HasConstraintName("FK_T_OD_ASSESSMENT_DOCS_A");

            });

            modelBuilder.Entity<T_OD_ASSESSMENTS>(entity =>
            {
                entity.HasKey(e => e.ASSESSMENT_IDX);

                entity.Property(e => e.ASSESSMENT_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AREA_ACRES).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.ASSESSED_BY).HasMaxLength(100);

                entity.Property(e => e.ASSESSMENT_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.CLEANED_CLOSED_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.CREATE_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.CREATE_USER_ID).HasMaxLength(450);

                entity.Property(e => e.CURRENT_SITE_STATUS)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.VOLUME_CU_YD).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.ASSESSMENT_TYPE_IDXNavigation)
                    .WithMany(p => p.T_OD_ASSESSMENTS)
                    .HasForeignKey(d => d.ASSESSMENT_TYPE_IDX)
                    .HasConstraintName("FK_T_OD_ASSESSMENTS_D");

                entity.HasOne(d => d.HF_ACCESS_CONTROLNavigation)
                    .WithMany(p => p.T_OD_ASSESSMENTSHF_ACCESS_CONTROLNavigation)
                    .HasForeignKey(d => d.HF_ACCESS_CONTROL)
                    .HasConstraintName("FK_T_OD_ASSESSMENTS_HFA");

                entity.HasOne(d => d.HF_BURNINGNavigation)
                    .WithMany(p => p.T_OD_ASSESSMENTSHF_BURNINGNavigation)
                    .HasForeignKey(d => d.HF_BURNING)
                    .HasConstraintName("FK_T_OD_ASSESSMENTS_HFB");

                entity.HasOne(d => d.HF_DRAINAGENavigation)
                    .WithMany(p => p.T_OD_ASSESSMENTSHF_DRAINAGENavigation)
                    .HasForeignKey(d => d.HF_DRAINAGE)
                    .HasConstraintName("FK_T_OD_ASSESSMENTS_HFD");

                entity.HasOne(d => d.HF_FENCINGNavigation)
                    .WithMany(p => p.T_OD_ASSESSMENTSHF_FENCINGNavigation)
                    .HasForeignKey(d => d.HF_FENCING)
                    .HasConstraintName("FK_T_OD_ASSESSMENTS_HFFN");

                entity.HasOne(d => d.HF_FLOODINGNavigation)
                    .WithMany(p => p.T_OD_ASSESSMENTSHF_FLOODINGNavigation)
                    .HasForeignKey(d => d.HF_FLOODING)
                    .HasConstraintName("FK_T_OD_ASSESSMENTS_HFF");

                entity.HasOne(d => d.HF_PUBLIC_CONCERNNavigation)
                    .WithMany(p => p.T_OD_ASSESSMENTSHF_PUBLIC_CONCERNNavigation)
                    .HasForeignKey(d => d.HF_PUBLIC_CONCERN)
                    .HasConstraintName("FK_T_OD_ASSESSMENTS_HFP");

                entity.HasOne(d => d.HF_RAINFALLNavigation)
                    .WithMany(p => p.T_OD_ASSESSMENTSHF_RAINFALLNavigation)
                    .HasForeignKey(d => d.HF_RAINFALL)
                    .HasConstraintName("FK_T_OD_ASSESSMENTS_HFR");

                entity.HasOne(d => d.SITE_IDXNavigation)
                    .WithMany(p => p.T_OD_ASSESSMENTS)
                    .HasForeignKey(d => d.SITE_IDX)
                    .HasConstraintName("FK_T_OD_ASSESSMENTS_S");
            });

            modelBuilder.Entity<T_OD_CLEANUP_ACTIVITIES>(entity =>
            {
                entity.HasKey(e => e.CLEANUP_ACTIVITY_IDX);

                entity.Property(e => e.CLEANUP_ACTIVITY_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CLEANUP_ACTIVITY)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CLEANUP_CAT)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CLEANUP_COST).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CLEANUP_UNIT_COST).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CREATE_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.CREATE_USER_ID).HasMaxLength(450);

                entity.Property(e => e.MODIFY_DT).HasColumnType("datetime2(0)");

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.QUANTITY)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.QUANTITY_UNIT)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.CLEANUP_PROJECT_IDXNavigation)
                    .WithMany(p => p.T_OD_CLEANUP_ACTIVITIES)
                    .HasForeignKey(d => d.CLEANUP_PROJECT_IDX)
                    .HasConstraintName("FK_T_OD_CLEANUP_ACTIVITIES");
            });

            modelBuilder.Entity<T_OD_CLEANUP_CLEANUP_DTL>(entity =>
            {
                entity.HasKey(e => e.CLEANUP_CLEANUP_DTL_IDX);

                entity.Property(e => e.CLEANUP_CLEANUP_DTL_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CLEANUP_COST).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.REF_ASSET_NAME)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.REF_WASTE_TYPE_CAT)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CLEANUP_PROJECT_IDXNavigation)
                    .WithMany(p => p.T_OD_CLEANUP_CLEANUP_DTL)
                    .HasForeignKey(d => d.CLEANUP_PROJECT_IDX)
                    .HasConstraintName("FK_T_OD_CLEANUP_CLEANUP_DTL");
            });

            modelBuilder.Entity<T_OD_CLEANUP_DISPOSAL_DTL>(entity =>
            {
                entity.HasKey(e => e.CLEANUP_DISPOSAL_DTL_IDX);

                entity.Property(e => e.CLEANUP_DISPOSAL_DTL_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DISPOSAL_COST).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PRICE_PER_TON).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.DISPOSAL_WEIGHT_LBS).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.CLEANUP_PROJECT_IDXNavigation)
                    .WithMany(p => p.T_OD_CLEANUP_DISPOSAL_DTL)
                    .HasForeignKey(d => d.CLEANUP_PROJECT_IDX)
                    .HasConstraintName("FK_T_OD_CLEANUP_DISPOSA_DTL");
            });

            modelBuilder.Entity<T_OD_CLEANUP_DOCS>(entity =>
            {
                entity.HasKey(e => e.DOC_IDX);

                entity.Property(e => e.DOC_IDX).ValueGeneratedNever();

                entity.HasOne(d => d.CLEANUP_PROJECT_IDXNavigation)
                    .WithMany(p => p.T_OD_CLEANUP_DOCS)
                    .HasForeignKey(d => d.CLEANUP_PROJECT_IDX)
                    .HasConstraintName("FK_T_OD_CLEANUP_DOCS_A");
            });

            modelBuilder.Entity<T_OD_CLEANUP_PROJECT>(entity =>
            {
                entity.HasKey(e => e.CLEANUP_PROJECT_IDX);

                entity.Property(e => e.CLEANUP_PROJECT_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CLEANUP_BY)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CLEANUP_BY_TITLE)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.COMPLETION_DATE).HasColumnType("datetime2(0)");

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

                entity.Property(e => e.PROJECT_DESCRIPTION).IsUnicode(false);

                entity.Property(e => e.PROJECT_TYPE)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.START_DATE).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.ASSESSMENT_IDXNavigation)
                    .WithMany(p => p.T_OD_CLEANUP_PROJECT)
                    .HasForeignKey(d => d.ASSESSMENT_IDX)
                    .HasConstraintName("FK_T_OD_CLEANUP_PROJECT_A");
            });

            modelBuilder.Entity<T_OD_CLEANUP_TRANSPORT_DTL>(entity =>
            {
                entity.HasKey(e => e.CLEANUP_TRANSPORT_DTL_IDX);

                entity.Property(e => e.CLEANUP_TRANSPORT_DTL_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.HOURLY_RATE).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.HOURS_LOAD).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TRANSPORT_COST).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.CLEANUP_PROJECT_IDXNavigation)
                    .WithMany(p => p.T_OD_CLEANUP_TRANSPORT_DTL)
                    .HasForeignKey(d => d.CLEANUP_PROJECT_IDX)
                    .HasConstraintName("FK_T_OD_CLEANUP_TRANSPORT_DTL");
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
                    .HasConstraintName("FK__T_OD_REF___REF_D__0ECE1972");
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

            modelBuilder.Entity<T_OD_SITE_PARCELS>(entity =>
            {
                entity.HasKey(e => e.SITE_PARCEL_IDX);

                entity.Property(e => e.SITE_PARCEL_IDX).ValueGeneratedNever();

                entity.Property(e => e.ACRES)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OWNER)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PARCEL_NUM)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_OD_SITES>(entity =>
            {
                entity.HasKey(e => e.SITE_IDX);

                entity.Property(e => e.SITE_IDX).ValueGeneratedNever();

                entity.Property(e => e.CURRENT_SITE_STATUS)
                    .HasMaxLength(30)
                    .IsUnicode(false);

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

            modelBuilder.Entity<V_OD_SITES>(entity =>
            {
                entity.HasKey(e => e.SITE_IDX);
                entity.Property(e => e.SITE_IDX).ValueGeneratedNever();
            });

            modelBuilder.Entity<V_OD_ASSESSMENTS>(entity =>
            {
                entity.HasKey(e => e.ASSESSMENT_IDX);
                entity.Property(e => e.ASSESSMENT_IDX).ValueGeneratedNever();
            });

            /*************** TABLE COLUMNS END   *******************/


            /*************** INSP COLUMNS START *******************/
            modelBuilder.Entity<T_INSP_ASSESS_BUILDING>(entity =>
            {
                entity.HasKey(e => e.ASSESSMENT_IDX);

                entity.Property(e => e.ASSESSMENT_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ASSESSED_BY).HasMaxLength(100);

                entity.Property(e => e.ASSESSMENT_DT).HasPrecision(0);

                entity.Property(e => e.Ceiling)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Closures)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Doors)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Exterior)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Floor)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Foundation)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MODIFY_DT).HasPrecision(0);

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.Panic)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Roof)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Stairs)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Walkway)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Walls)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Windows)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.SITE_IDXNavigation)
                    .WithMany(p => p.T_INSP_ASSESS_BUILDING)
                    .HasForeignKey(d => d.SITE_IDX)
                    .HasConstraintName("FK_T_INSP_ASSESSMENTS_B");
            });


            modelBuilder.Entity<T_INSP_ASSESS_FIRE>(entity =>
            {
                entity.HasKey(e => e.ASSESSMENT_IDX);

                entity.Property(e => e.ASSESSMENT_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AED)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AED_Expiration).HasColumnType("datetime");

                entity.Property(e => e.ASSESSED_BY).HasMaxLength(100);

                entity.Property(e => e.ASSESSMENT_DT).HasPrecision(0);

                entity.Property(e => e.Ceiling_Condition)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ceiling_Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Compliance)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Doc_EmergLight)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Doc_EmergPwr)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Doc_EvacDrill)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Doc_FireAlarm)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Doc_FireSprinkler5)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Doc_FireSprinklerAnn)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Doc_Generator)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Doc_KitchenHoodSys)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ELec_Covers)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Egress_DoorsKey)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Egress_DoorsOpen)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Egress_Evac)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Egress_Latch)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Egress_Path)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Egress_Signs)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Egress_Storage)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Adapters)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Appliances)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Clearance)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Cords)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_GFI)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Heater)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_NoCord)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Panels)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FacContact)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e=>e.Facility_Use).HasMaxLength(100).IsUnicode(false);

                entity.Property(e => e.FireDept_Access)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FireDept_AccessKeys)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FireDept_Address)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FireDept_Valves)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FireExt_Collar)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FireExt_Doc)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FireExt_Mounted)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FireExt_Sys)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fuel_Extinguisher)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fuel_Materials)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fuel_Signs)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fuel_Valves)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hazmat_704)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hazmat_EmptyTanks)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hazmat_Liquids)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hazmat_Quantities)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hazmat_SDS)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hazmat_Shelving)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hazmat_Tanks)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MODIFY_DT).HasPrecision(0);

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.Risers_Deficiencies)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Risers_Gauges)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Risers_Inspected)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Risers_Plate)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Risers_Sprinkler)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Risers_Tagged)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sprinkler_Cond)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Storage_Combust)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Storage_Cylinders)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Storage_Food)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Storage_Housekeeping)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Storage_OilRags)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Storage_Sprinklers)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vent_BrandDate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vent_Caps)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Vent_Cleanliness)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Vent_Exhaust)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Vent_Extinguishers)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Vent_GasValve)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Vent_Gauge)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Vent_Hydro)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Vent_PullStation)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Vent_ServiceDate).HasColumnType("datetime");

                entity.HasOne(d => d.SITE_IDXNavigation)
    .WithMany(p => p.T_INSP_ASSESS_FIRE)
    .HasForeignKey(d => d.SITE_IDX)
    .HasConstraintName("FK_T_INSP_ASSESSMENTS_F");
            });



            modelBuilder.Entity<T_INSP_ASSESS_ELECTRICAL>(entity =>
            {
                entity.HasKey(e => e.ASSESSMENT_IDX);

                entity.Property(e => e.ASSESSMENT_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ASSESSED_BY).HasMaxLength(100);

                entity.Property(e => e.ASSESSMENT_DT).HasPrecision(0);

                entity.Property(e => e.Comp_Codes).HasMaxLength(10);

                entity.Property(e => e.Comp_Records).HasMaxLength(10);

                entity.Property(e => e.Comp_Signage).HasMaxLength(10);

                entity.Property(e => e.Compliance)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Contact)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Label)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Rust)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Space)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Gen_Address)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Gen_Hazard)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Gen_Site)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ground_Connections).HasMaxLength(10);

                entity.Property(e => e.Ground_Corrosion).HasMaxLength(10);

                entity.Property(e => e.Ground_Electrode).HasMaxLength(10);

                entity.Property(e => e.Light_Exit).HasMaxLength(10);

                entity.Property(e => e.Light_Fixtures).HasMaxLength(10);

                entity.Property(e => e.Light_Ill).HasMaxLength(10);

                entity.Property(e => e.MODIFY_DT).HasPrecision(0);

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.Outlet_Func)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Outlet_GFCI).HasMaxLength(10);

                entity.Property(e => e.Outlet_Tamper).HasMaxLength(10);

                entity.Property(e => e.Surge_Func).HasMaxLength(10);

                entity.Property(e => e.Surge_Protectors).HasMaxLength(10);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Wiring_Damage)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Wiring_Secured)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Wiring_Separate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.SITE_IDXNavigation)
                    .WithMany(p => p.T_INSP_ASSESS_ELECTRICAL)
                    .HasForeignKey(d => d.SITE_IDX)
                    .HasConstraintName("FK_T_INSP_ASSESSMENTS_E");
            });

            modelBuilder.Entity<T_INSP_ASSESS_MECHANICAL>(entity =>
            {
                entity.HasKey(e => e.ASSESSMENT_IDX);

                entity.Property(e => e.ASSESSMENT_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ASSESSED_BY).HasMaxLength(100);

                entity.Property(e => e.ASSESSMENT_DT).HasPrecision(0);

                entity.Property(e => e.Comp_Codes).HasMaxLength(10);

                entity.Property(e => e.Comp_Records).HasMaxLength(10);

                entity.Property(e => e.Comp_Signage).HasMaxLength(10);

                entity.Property(e => e.Compliance)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Contact)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Duct_Insulation)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Duct_Leak)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Duct_Obstruction)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Equip_Doc).HasMaxLength(10);

                entity.Property(e => e.Equip_Guards).HasMaxLength(10);

                entity.Property(e => e.Equip_Operational)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HVAC_Clean)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HVAC_Filters)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HVAC_Therm)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MODIFY_DT).HasPrecision(0);

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.Piping_Insulation)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Piping_Leaks)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Piping_Valves)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vent_Air).HasMaxLength(10);

                entity.Property(e => e.Vent_Airflow).HasMaxLength(10);

                entity.Property(e => e.Vent_Exhaust).HasMaxLength(10);

                entity.HasOne(d => d.SITE_IDXNavigation)
                    .WithMany(p => p.T_INSP_ASSESS_MECHANICAL)
                    .HasForeignKey(d => d.SITE_IDX)
                    .HasConstraintName("FK_T_INSP_ASSESSMENTS_M");
            });


            modelBuilder.Entity<T_INSP_ASSESS_PLUMBING>(entity =>
            {
                entity.HasKey(e => e.ASSESSMENT_IDX);

                entity.Property(e => e.ASSESSMENT_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ASSESSED_BY).HasMaxLength(100);

                entity.Property(e => e.ASSESSMENT_DT).HasPrecision(0);

                entity.Property(e => e.Backflow)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dishwasher)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Drains)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Heater_Type)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.MODIFY_DT).HasPrecision(0);

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.Meter_Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sewer_Loc)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Shower_Tub)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Sink_Faucet)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Supply_Lines)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Toilet_Urinal)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.WaterPSI)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Water_Heater)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.SITE_IDXNavigation)
                    .WithMany(p => p.T_INSP_ASSESS_PLUMBING)
                    .HasForeignKey(d => d.SITE_IDX)
                    .HasConstraintName("FK_T_INSP_ASSESSMENTS_S");
            });

            modelBuilder.Entity<T_INSP_ASSESS_LIFE>(entity =>
            {
                entity.HasKey(e => e.ASSESSMENT_IDX);

                entity.Property(e => e.ASSESSMENT_IDX).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ASSESSED_BY).HasMaxLength(100);

                entity.Property(e => e.ASSESSMENT_DT).HasPrecision(0);

                entity.Property(e => e.AddressVisible)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingType)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Ceiling)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Closures)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Doors)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Ground)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Hazard)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Label)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Lighting)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Notes).IsUnicode(false);

                entity.Property(e => e.Elec_Obstruct)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Outlet)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Panel)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Sign)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Elec_Surge)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Exterior)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_EmergLight)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_Evac)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_EvacDrill)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_Exit)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_ExtinAnnual)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_Extinguisher)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_FireAlarm)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_Food)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_Hazard)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_HoodSup)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_KitchenHood)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_Notes).IsUnicode(false);

                entity.Property(e => e.Fire_Piled)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_Plate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_SDS)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_Sprinklers1)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_Sprinklers5)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fire_Storage)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Floor)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Foundation)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MODIFY_DT).HasPrecision(0);

                entity.Property(e => e.MODIFY_USER_ID).HasMaxLength(450);

                entity.Property(e => e.Mech_Duct)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Mech_Equip)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Mech_Filters)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Mech_Notes).IsUnicode(false);

                entity.Property(e => e.Mech_Pipe)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Mech_Register)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Mech_Therm)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Mech_VentAir)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Mech_VentSyst)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.Panic)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Plum_Backflow)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Plum_Dish)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Plum_Drain)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Plum_Heater)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Plum_Notes).IsUnicode(false);

                entity.Property(e => e.Plum_Sink)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Plum_Toilet)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Plum_Tub)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Plum_Water)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Roof)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Stairs)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Walkway)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Walls)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Windows)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.SITE_IDXNavigation)
                    .WithMany(p => p.T_INSP_ASSESS_LIFE)
                    .HasForeignKey(d => d.SITE_IDX)
                    .HasConstraintName("FK_T_INSP_ASSESSMENTS_LF");
            });

            modelBuilder.Entity<T_INSP_SITES>(entity =>
            {
                entity.HasKey(e => e.SITE_IDX)
                    .HasName("PK_T_INSP_SITE_DTL");

                entity.Property(e => e.SITE_IDX).ValueGeneratedNever();

                entity.Property(e => e.LAST_UPDATE_BY)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LAST_UPDATE_DT).HasPrecision(0);


            });


            modelBuilder.Entity<V_INSP_ASSESSMENTS>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_INSP_ASSESSMENTS");

                entity.Property(e => e.ASSESSED_BY).HasMaxLength(100);

                entity.Property(e => e.ASSESSMENT_DT).HasPrecision(0);

                entity.Property(e => e.INSP_TYP)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.SITE_ID)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SITE_NAME)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BUILDING_TYPE).HasMaxLength(256);

            });
            /*************** INSP COLUMNS END   *******************/

        }
    }
}
