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

        public virtual DbSet<T_OD_REF_DISPOSAL> T_OD_REF_DISPOSAL { get; set; }

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
        }
    }
}
