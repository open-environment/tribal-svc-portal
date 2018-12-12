using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TribalSvcPortal.Data.Models
{
    public partial class ApplicationDbContextTemp : DbContext
    {
        public ApplicationDbContextTemp()
        {
        }

        public ApplicationDbContextTemp(DbContextOptions<ApplicationDbContextTemp> options)
            : base(options)
        {
        }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=TRIBAL_SVC_PORTAL;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                    .HasConstraintName("FK__T_PRT_DOC__DOC_S__5FF32EF8");

                entity.HasOne(d => d.DOC_TYPENavigation)
                    .WithMany(p => p.T_PRT_DOCUMENTS)
                    .HasForeignKey(d => d.DOC_TYPE)
                    .HasConstraintName("FK__T_PRT_DOC__DOC_T__5E0AE686");

                entity.HasOne(d => d.ORG_)
                    .WithMany(p => p.T_PRT_DOCUMENTS)
                    .HasForeignKey(d => d.ORG_ID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__T_PRT_DOC__ORG_I__5D16C24D");

                entity.HasOne(d => d.SHARE_TYPENavigation)
                    .WithMany(p => p.T_PRT_DOCUMENTS)
                    .HasForeignKey(d => d.SHARE_TYPE)
                    .HasConstraintName("FK__T_PRT_DOC__SHARE__5EFF0ABF");
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

                entity.Property(e => e.UNIT_MSR_IDX).HasDefaultValueSql("(newid())");

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
        }
    }
}
