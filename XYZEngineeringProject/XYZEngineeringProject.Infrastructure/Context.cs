using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }


        
        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ////////////////////////////////////////  many to one 

        }
    }
}