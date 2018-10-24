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

        public virtual DbSet<T_OD_DUMP_ASSESSMENT_CONTENT> T_OD_DUMP_ASSESSMENT_CONTENT { get; set; }
        public virtual DbSet<T_OD_DUMP_ASSESSMENT_DOCS> T_OD_DUMP_ASSESSMENT_DOCS { get; set; }
        public virtual DbSet<T_OD_DUMP_ASSESSMENTS> T_OD_DUMP_ASSESSMENTS { get; set; }
        public virtual DbSet<T_OD_REF_CLEANUP_ASSETS> T_OD_REF_CLEANUP_ASSETS { get; set; }
        public virtual DbSet<T_OD_REF_DATA> T_OD_REF_DATA { get; set; }
        public virtual DbSet<T_OD_REF_DATA_CATEGORIES> T_OD_REF_DATA_CATEGORIES { get; set; }
        public virtual DbSet<T_OD_REF_DISPOSAL> T_OD_REF_DISPOSAL { get; set; }
        public virtual DbSet<T_OD_REF_THREAT_FACTORS> T_OD_REF_THREAT_FACTORS { get; set; }
        public virtual DbSet<T_OD_REF_WASTE_TYPE> T_OD_REF_WASTE_TYPE { get; set; }
        public virtual DbSet<T_OD_REF_WASTE_TYPE_CAT> T_OD_REF_WASTE_TYPE_CAT { get; set; }
        public virtual DbSet<T_OD_REF_WASTE_TYPE_CAT_CLEANUP> T_OD_REF_WASTE_TYPE_CAT_CLEANUP { get; set; }
        public virtual DbSet<T_OD_SITES> T_OD_SITES { get; set; }

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
                    .HasConstraintName("FK__T_OD_REF___REF_D__4E3E9311");
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
        }
    }
}
