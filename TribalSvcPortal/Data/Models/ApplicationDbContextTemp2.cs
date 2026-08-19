using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TribalSvcPortal.Data.Models
{
    public partial class ApplicationDbContextTemp2 : DbContext
    {
        public ApplicationDbContextTemp2()
        {
        }

        public ApplicationDbContextTemp2(DbContextOptions<ApplicationDbContextTemp2> options)
            : base(options)
        {
        }

        public virtual DbSet<T_INSP_ASSESS_LIFE> T_INSP_ASSESS_LIFE { get; set; }
        public virtual DbSet<T_INSP_SITES> T_INSP_SITES { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=TRIBAL_SVC_PORTAL;Trusted_Connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
