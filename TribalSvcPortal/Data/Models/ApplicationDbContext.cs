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
        public virtual DbSet<TOeSysLog> TOeSysLog { get; set; }
        public virtual DbSet<TPrtClientRoles> TPrtClientRoles { get; set; }
        public virtual DbSet<TPrtClients> TPrtClients { get; set; }
        public virtual DbSet<TPrtTenantClientAlias> TPrtTenantClientAlias { get; set; }
        public virtual DbSet<TPrtTenants> TPrtTenants { get; set; }
        public virtual DbSet<TPrtTenantUserClient> TPrtTenantUserClient { get; set; }
        public virtual DbSet<TPrtTenantUsers> TPrtTenantUsers { get; set; }
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
            modelBuilder.Entity<IdentityRole>().ToTable("T_PRT_ROLES"); //add each time
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("T_PRT_USER_TOKENS"); //add each time
            modelBuilder.Entity<ApplicationUser>().ToTable("T_PRT_USERS"); //add each time
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("T_PRT_ROLE_CLAIMS"); //add each time
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("T_PRT_USER_CLAIMS"); //add each time
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("T_PRT_USER_LOGINS"); //add each time
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("T_PRT_USER_ROLES"); //add each time


            /*************** TABLE COLUMNS START *******************/
            modelBuilder.Entity<TOeSysLog>(entity =>
            {
                entity.HasKey(e => e.SysLogId);

                entity.ToTable("T_OE_SYS_LOG");

                entity.Property(e => e.SysLogId).HasColumnName("SYS_LOG_ID");

                entity.Property(e => e.LogDt)
                    .HasColumnName("LOG_DT")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.LogMsg)
                    .HasColumnName("LOG_MSG")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.LogType)
                    .HasColumnName("LOG_TYPE")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LogUserId)
                    .HasColumnName("LOG_USER_ID")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<TPrtClientRoles>(entity =>
            {
                entity.HasKey(e => e.ClientRolesIdx);

                entity.ToTable("T_PRT_CLIENT_ROLES");

                entity.Property(e => e.ClientRolesIdx).HasColumnName("CLIENT_ROLES_IDX");

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasColumnName("CLIENT_ID")
                    .HasMaxLength(20);

                entity.Property(e => e.ClientRoleName)
                    .HasColumnName("CLIENT_ROLE_NAME")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TPrtClientRoles)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_T_PRT_CLIENT_ROLES_C");
            });

            modelBuilder.Entity<TPrtClients>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.ToTable("T_PRT_CLIENTS");

                entity.Property(e => e.ClientId)
                    .HasColumnName("CLIENT_ID")
                    .HasMaxLength(20)
                    .ValueGeneratedNever();

                entity.Property(e => e.ClientGrantType)
                    .IsRequired()
                    .HasColumnName("CLIENT_GRANT_TYPE")
                    .HasMaxLength(20);

                entity.Property(e => e.ClientImage).HasColumnName("CLIENT_IMAGE");

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasColumnName("CLIENT_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.ClientPostLogoutUri)
                    .HasColumnName("CLIENT_POST_LOGOUT_URI")
                    .HasMaxLength(250);

                entity.Property(e => e.ClientRedirectUri)
                    .HasColumnName("CLIENT_REDIRECT_URI")
                    .HasMaxLength(250);

                entity.Property(e => e.ClientUrl)
                    .HasColumnName("CLIENT_URL")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<TPrtTenantClientAlias>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.ClientId });

                entity.ToTable("T_PRT_TENANT_CLIENT_ALIAS");

                entity.Property(e => e.TenantId)
                    .HasColumnName("TENANT_ID")
                    .HasMaxLength(30);

                entity.Property(e => e.ClientId)
                    .HasColumnName("CLIENT_ID")
                    .HasMaxLength(20);

                entity.Property(e => e.TenantClientAlias)
                    .IsRequired()
                    .HasColumnName("TENANT_CLIENT_ALIAS")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<TPrtTenants>(entity =>
            {
                entity.HasKey(e => e.TenantId);

                entity.ToTable("T_PRT_TENANTS");

                entity.Property(e => e.TenantId)
                    .HasColumnName("TENANT_ID")
                    .HasMaxLength(30)
                    .ValueGeneratedNever();

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasColumnName("TENANT_NAME")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TPrtTenantUserClient>(entity =>
            {
                entity.HasKey(e => e.TenantUserClientIdx);

                entity.ToTable("T_PRT_TENANT_USER_CLIENT");

                entity.Property(e => e.TenantUserClientIdx).HasColumnName("TENANT_USER_CLIENT_IDX");

                entity.Property(e => e.AdminInd).HasColumnName("ADMIN_IND");

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasColumnName("CLIENT_ID")
                    .HasMaxLength(20);

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

                entity.Property(e => e.StatusInd)
                    .IsRequired()
                    .HasColumnName("STATUS_IND")
                    .HasMaxLength(1);

                entity.Property(e => e.TenantUserIdx).HasColumnName("TENANT_USER_IDX");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TPrtTenantUserClient)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_T_PRT_TENANT_USER_CLIENT_ROLES_C");

                entity.HasOne(d => d.TenantUserIdxNavigation)
                    .WithMany(p => p.TPrtTenantUserClient)
                    .HasForeignKey(d => d.TenantUserIdx)
                    .HasConstraintName("FK_T_PRT_TENANT_USER_CLIENT_ROLES_U");
            });

            modelBuilder.Entity<TPrtTenantUsers>(entity =>
            {
                entity.HasKey(e => e.TenantUserIdx);

                entity.ToTable("T_PRT_TENANT_USERS");

                entity.Property(e => e.TenantUserIdx).HasColumnName("TENANT_USER_IDX");

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

                entity.Property(e => e.StatusInd)
                    .IsRequired()
                    .HasColumnName("STATUS_IND")
                    .HasMaxLength(1);

                entity.Property(e => e.TenantAdminInd).HasColumnName("TENANT_ADMIN_IND");

                entity.Property(e => e.TenantId)
                    .IsRequired()
                    .HasColumnName("TENANT_ID")
                    .HasMaxLength(30);

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.TPrtTenantUsers)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_T_PRT_TENANT_USERS_T");
            });
            /*************** TABLE COLUMNS END   *******************/

        }
    }
}
