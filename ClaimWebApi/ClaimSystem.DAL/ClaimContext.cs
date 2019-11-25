using ClaimSystem.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ClaimSystem.DAL
{
   
    public class ClaimContext : IdentityDbContext<ApplicationUser>
    {
        // Connection string name
        public ClaimContext() : base("ClaimContext")
        {

        }
        public DbSet<ReimbursementClaim> ReimbursementClaim { get; set; }

        public DbSet<ClaimDetails> ClaimDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
            //AspNetUsers -> User
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("User")
                .Ignore(u => u.EmailConfirmed)
                .Ignore(u => u.PasswordHash)
                .Ignore(u => u.SecurityStamp)
                .Ignore(u => u.PhoneNumber)
                .Ignore(u => u.PhoneNumberConfirmed)
                .Ignore(u => u.TwoFactorEnabled)
                 .Ignore(u => u.LockoutEnabled)
                  .Ignore(u => u.LockoutEndDateUtc)
                   .Ignore(u => u.AccessFailedCount);

            //AspNetRoles -> Role
            modelBuilder.Entity<IdentityRole>()
                .ToTable("Role");
            //AspNetUserRoles -> UserRole
            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("UserRole");

            
            //AspNetUserClaims -> UserClaim
            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("UserClaim");
            //AspNetUserLogins -> UserLogin
            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("UserLogin");

                        

            modelBuilder.Entity<ClaimDetails>().HasKey(a => a.ClaimDetailId).HasRequired(a => a.ReimbursementClaim).WithRequiredDependent();

                //.HasKey(a => a.ClaimDetailId)
                //.HasRequired(a => a.ReimbursementClaim).;

          //  modelBuilder.Entity<ReimbursementClaim>().HasKey(a => a.ClaimId).HasRequired(a => a.ClaimOwnerId);

            //modelBuilder.Entity<ClaimFileDetails>().HasKey(a => a.ClaimFileId)
            //   .HasRequired(a => a.ReimbursementClaim);



        }
    }
}