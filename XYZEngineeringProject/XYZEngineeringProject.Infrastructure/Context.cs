using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Address> UserAddresses { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<UsersToDepartments> UsersToDepartments { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //////////////////////////////////////// one to one
            builder.Entity<AppUser>()
                .HasOne<Address>( u => u.Address)
                .WithOne(ad => ad.User)
                .HasForeignKey<Address>( ad => ad.AddressOfAppUserId);

            ////////////////////////////////////////  many to one 
            builder.Entity<Positions>()
                .HasMany<AppUser> ( u => u.Users)
                .WithOne(u => u.Position)
                .HasForeignKey( u => u.PositionId);

            //////////////////////////////////////// many to many
            builder.Entity<UsersToDepartments>().HasKey( utd => new { utd.AppUserId , utd.DepartmentId });
            builder.Entity<UsersClientsGroups>().HasKey( ucg => new {ucg.AppUserId, ucg.ClientId, ucg.GroupId });

        }
    }
}