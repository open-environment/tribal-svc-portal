//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;

//namespace TribalSvcPortal.Data.Models
//{
//    public partial class ApplicationDbContextTemp : DbContext
//    {
//        public virtual DbSet<T_OD_DUMP_ASSESSMENTS> T_OD_DUMP_ASSESSMENTS { get; set; }
//        public virtual DbSet<T_OD_REF_DATA> T_OD_REF_DATA { get; set; }
//        public virtual DbSet<T_OD_REF_DATA_CATEGORIES> T_OD_REF_DATA_CATEGORIES { get; set; }
//        public virtual DbSet<T_OD_SITES> T_OD_SITES { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=TRIBAL_SVC_PORTAL;Trusted_Connection=True;");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<T_OD_DUMP_ASSESSMENTS>(entity =>
//            {
//                entity.HasKey(e => e.DumpAssessmentsIdx);

//                entity.Property(e => e.DumpAssessmentsIdx)
//                    .HasColumnName("DUMP_ASSESSMENTS_IDX")
//                    .HasDefaultValueSql("(newid())");

//                entity.Property(e => e.ActiveSiteInd).HasColumnName("ACTIVE_SITE_IND");

//                entity.Property(e => e.AssessmentDt)
//                    .HasColumnName("ASSESSMENT_DT")
//                    .HasColumnType("datetime2(0)");

//                entity.Property(e => e.BurningTakenPlaceInd).HasColumnName("BURNING_TAKEN_PLACE_IND");

//                entity.Property(e => e.CreateDt)
//                    .HasColumnName("CREATE_DT")
//                    .HasColumnType("datetime2(0)");

//                entity.Property(e => e.CreateUserId)
//                    .HasColumnName("CREATE_USER_ID")
//                    .HasMaxLength(450);

//                entity.Property(e => e.InspectedBy)
//                    .HasColumnName("INSPECTED_BY")
//                    .HasMaxLength(450);

//                entity.Property(e => e.InspectionTypeIdx).HasColumnName("INSPECTION_TYPE_IDX");

//                entity.Property(e => e.MaintainedInd).HasColumnName("MAINTAINED_IND");

//                entity.Property(e => e.ModifyDt)
//                    .HasColumnName("MODIFY_DT")
//                    .HasColumnType("datetime2(0)");

//                entity.Property(e => e.ModifyUserId)
//                    .HasColumnName("MODIFY_USER_ID")
//                    .HasMaxLength(450);

//                entity.Property(e => e.NoOfStructures)
//                    .HasColumnName("NO_OF_STRUCTURES")
//                    .HasMaxLength(100)
//                    .IsUnicode(false);

//                entity.Property(e => e.Notes)
//                    .HasColumnName("NOTES")
//                    .HasMaxLength(400);

//                entity.Property(e => e.Signage)
//                    .HasColumnName("SIGNAGE")
//                    .HasMaxLength(100)
//                    .IsUnicode(false);

//                entity.Property(e => e.SiteAccess)
//                    .HasColumnName("SITE_ACCESS")
//                    .HasMaxLength(100)
//                    .IsUnicode(false);

//                entity.Property(e => e.SiteDescription)
//                    .HasColumnName("SITE_DESCRIPTION")
//                    .HasMaxLength(450);

//                entity.Property(e => e.SiteGeography)
//                    .HasColumnName("SITE_GEOGRAPHY")
//                    .HasMaxLength(100)
//                    .IsUnicode(false);

//                entity.Property(e => e.SiteIdx).HasColumnName("SITE_IDX");

//                entity.HasOne(d => d.InspectionTypeIdxNavigation)
//                    .WithMany(p => p.T_OD_DUMP_ASSESSMENTS)
//                    .HasForeignKey(d => d.InspectionTypeIdx)
//                    .HasConstraintName("FK_T_OD_DUMP_ASSESSMENTS_D");

//                entity.HasOne(d => d.SiteIdxNavigation)
//                    .WithMany(p => p.T_OD_DUMP_ASSESSMENTS)
//                    .HasForeignKey(d => d.SiteIdx)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_T_OD_DUMP_ASSESSMENTS_S");
//            });

//            modelBuilder.Entity<T_OD_REF_DATA>(entity =>
//            {
//                entity.HasKey(e => e.DataIdx);

//                entity.Property(e => e.DataIdx)
//                    .HasColumnName("DATA_IDX")
//                    .HasDefaultValueSql("(newid())");

//                entity.Property(e => e.CreateDt)
//                    .HasColumnName("CREATE_DT")
//                    .HasColumnType("datetime2(0)");

//                entity.Property(e => e.CreateUserId)
//                    .HasColumnName("CREATE_USER_ID")
//                    .HasMaxLength(450);

//                entity.Property(e => e.DataCatName)
//                    .IsRequired()
//                    .HasColumnName("DATA_CAT_NAME")
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.DataName)
//                    .IsRequired()
//                    .HasColumnName("DATA_NAME")
//                    .HasMaxLength(100)
//                    .IsUnicode(false);

//                entity.Property(e => e.ModifyDt)
//                    .HasColumnName("MODIFY_DT")
//                    .HasColumnType("datetime2(0)");

//                entity.Property(e => e.ModifyUserId)
//                    .HasColumnName("MODIFY_USER_ID")
//                    .HasMaxLength(450);

//                entity.Property(e => e.OrgId)
//                    .HasColumnName("ORG_ID")
//                    .HasMaxLength(30)
//                    .IsUnicode(false);

//                entity.Property(e => e.UserCreateInd).HasColumnName("USER_CREATE_IND");

//                entity.HasOne(d => d.DataCatNameNavigation)
//                    .WithMany(p => p.T_OD_REF_DATA)
//                    .HasForeignKey(d => d.DataCatName)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__T_OD_REF___DATA___703EA55A");
//            });

//            modelBuilder.Entity<T_OD_REF_DATA_CATEGORIES>(entity =>
//            {
//                entity.HasKey(e => e.DataCatName);

//                entity.Property(e => e.DataCatName)
//                    .HasColumnName("DATA_CAT_NAME")
//                    .HasMaxLength(50)
//                    .IsUnicode(false)
//                    .ValueGeneratedNever();

//                entity.Property(e => e.CreateDt)
//                    .HasColumnName("CREATE_DT")
//                    .HasColumnType("datetime2(0)");

//                entity.Property(e => e.CreateUserId)
//                    .HasColumnName("CREATE_USER_ID")
//                    .HasMaxLength(450);

//                entity.Property(e => e.DataCatDescription)
//                    .HasColumnName("DATA_CAT_DESCRIPTION")
//                    .HasMaxLength(200)
//                    .IsUnicode(false);
//            });

//            modelBuilder.Entity<T_OD_SITES>(entity =>
//            {
//                entity.HasKey(e => e.SiteIdx);

//                entity.Property(e => e.SiteIdx)
//                    .HasColumnName("SITE_IDX")
//                    .ValueGeneratedNever();

//                entity.Property(e => e.CommunityIdx).HasColumnName("COMMUNITY_IDX");

//                entity.Property(e => e.ReportedBy)
//                    .HasColumnName("REPORTED_BY")
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.ReportedOn)
//                    .HasColumnName("REPORTED_ON")
//                    .HasColumnType("datetime2(0)");

//                entity.Property(e => e.ResponseAction)
//                    .IsRequired()
//                    .HasColumnName("RESPONSE_ACTION")
//                    .HasMaxLength(100)
//                    .IsUnicode(false);

//                entity.Property(e => e.SiteSettingIdx).HasColumnName("SITE_SETTING_IDX");

//                entity.HasOne(d => d.CommunityIdxNavigation)
//                    .WithMany(p => p.T_OD_SITESCommunityIdxNavigation)
//                    .HasForeignKey(d => d.CommunityIdx)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_T_OD_SITE_DTL_C");

//                entity.HasOne(d => d.SiteSettingIdxNavigation)
//                    .WithMany(p => p.T_OD_SITESSiteSettingIdxNavigation)
//                    .HasForeignKey(d => d.SiteSettingIdx)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_T_OD_SITE_DTL_SS");
//            });
//        }
//    }
//}
