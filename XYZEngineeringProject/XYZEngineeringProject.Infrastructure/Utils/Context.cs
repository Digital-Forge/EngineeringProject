using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Domain.Models.EntityUtils;

namespace XYZEngineeringProject.Infrastructure.Utils
{
    public class Context : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        private readonly InfrastructureUtils _infrastructureUtils;

        // entry
        public DbSet<Address> UserAddresses { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientContact> ClientContacts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ListOfTasks> ListTasks { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<NoteToUser> NoteToUser { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<UserTask> Tasks { get; set; }
        public DbSet<UsersToClients> UsersToClients { get; set; }
        public DbSet<UsersToDepartments> UsersToDepartments { get; set; }
        public DbSet<UsersToPositions> UsersToPositions { get; set; }
        public DbSet<LogicCompany> LogicCompanies { get; set; }

        //logger
        public DbSet<Log> Logs { get; set; }

        public Context(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _infrastructureUtils = new InfrastructureUtils(this, httpContextAccessor);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //////////////////////////////////////// AppUser one to one Address
            builder.Entity<AppUser>()
                .HasOne(u => u.Address)
                .WithOne(ad => ad.User)
                .HasForeignKey<Address>(ad => ad.AppUserId);

            //////////////////////////////////////// ListOfTasks many to one AppUser
            builder.Entity<ListOfTasks>()
                .HasOne(u => u.User)
                .WithMany(u => u.ListTasks)
                .HasForeignKey(u => u.UserId);

            //////////////////////////////////////// Tasks many to one ListOfTasks
            builder.Entity<UserTask>()
                .HasOne(u => u.ListOfTasks)
                .WithMany(u => u.Task)
                .HasForeignKey(u => u.ListOfTasksId);

            //////////////////////////////////////// Tasks many to one AppUser
            builder.Entity<UserTask>()
                .HasOne(u => u.AssignerUser)
                .WithMany(u => u.AsignerTasks)
                .HasForeignKey(u => u.AssignerUserId);

            //////////////////////////////////////// Tasks many to one AppUser
            builder.Entity<UserTask>()
                .HasOne(u => u.AssigneeUser)
                .WithMany(u => u.AsigneeTasks)
                .HasForeignKey(u => u.AssigneeUserId);


            //////////////////////////////////////// AppUser many to one LogicCompany
            builder.Entity<AppUser>()
                .HasOne(s => s.Company)
                .WithMany(g => g.AppUsers)
                .HasForeignKey(s => s.CompanyId);

            //////////////////////////////////////// Client many to many AppUser (UsersClientsGroups)
            builder.Entity<UsersToClients>().HasKey(x => new { x.UserId, x.ClientId });

            builder.Entity<UsersToClients>()
                .HasOne(s => s.Client)
                .WithMany(s => s.ClientsToUsers)
                .HasForeignKey(s => s.ClientId);

            builder.Entity<UsersToClients>()
                .HasOne(s => s.User)
                .WithMany(s => s.UsersToClientsGroups)
                .HasForeignKey(s => s.UserId);

            //////////////////////////////////////// UsersToClientsGroups many to one Groups
            builder.Entity<ClientContact>()
                .HasOne(s => s.Client)
                .WithMany(g => g.ClientContacts)
                .HasForeignKey(s => s.ClientId);

            //////////////////////////////////////// AppUser many to many Departments (UsersToDepartments)
            builder.Entity<UsersToDepartments>().HasKey(x => new { x.UserId, x.DepartmentId });

            builder.Entity<UsersToDepartments>()
                .HasOne(s => s.User)
                .WithMany(s => s.UsersToDepartments)
                .HasForeignKey(s => s.UserId);

            builder.Entity<UsersToDepartments>()
                .HasOne(s => s.Departments)
                .WithMany(s => s.UsersToDepartments)
                .HasForeignKey(s => s.DepartmentId);

            //////////////////////////////////////// AppUser many to many Positions (UsersToPositions)
            builder.Entity<UsersToPositions>().HasKey(x => new { x.UserId, x.PositionId });

            builder.Entity<UsersToPositions>()
                .HasOne(u => u.User)
                .WithMany(u => u.UsersToPositions)
                .HasForeignKey(u => u.UserId);

            builder.Entity<UsersToPositions>()
                .HasOne(u => u.Position)
                .WithMany(u => u.UsersToPositions)
                .HasForeignKey(u => u.PositionId);

            //////////////////////////////////////// AppUser many to many Note (NoteToUser)
            builder.Entity<NoteToUser>().HasKey(x => new { x.UserId, x.NoteId });

            builder.Entity<NoteToUser>()
                .HasOne(u => u.User)
                .WithMany(u => u.NoteToUser)
                .HasForeignKey(u => u.UserId);

            builder.Entity<NoteToUser>()
                .HasOne(u => u.Note)
                .WithMany(u => u.NoteToUsers)
                .HasForeignKey(u => u.NoteId);

        }

        public override int SaveChanges()
        {
            softDataCheckChanges();
            return base.SaveChanges();
        }
        /*
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            softDataCheckChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            softDataCheckChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            softDataCheckChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }*/

        public int SaveChangesHard()
        {
            softDataCheckChanges(true);
            return base.SaveChanges();
        }

        public int _ClearSaveChanges()
        {
            return base.SaveChanges();
        }

        private void softDataCheckChanges(bool hardMode = false)
        {
            var currentUser = _infrastructureUtils?.GetUserFormHttpContext();

            foreach (var entry in ChangeTracker.Entries<ISoftDataEntity>())
            {
                if (entry.Entity.UseStatus == UseStatusEntity.SolidConst && !hardMode)
                {
                    entry.State = EntityState.Unchanged;
                }

                switch (entry.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        entry.Entity.UpdateDate = DateTime.Now;
                        entry.Entity.UpdateBy = currentUser?.Id;
                        entry.Entity.UseStatus = UseStatusEntity.Delete;
                        entry.State = hardMode ? EntityState.Deleted : EntityState.Modified;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateDate = DateTime.Now;
                        entry.Entity.UpdateBy = currentUser?.Id;
                        entry.Entity.UseStatus = UseStatusEntity.Update;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreateDate = DateTime.Now;
                        entry.Entity.UpdateDate = DateTime.Now;
                        entry.Entity.CreateBy = currentUser?.Id ?? Guid.Empty;
                        entry.Entity.UpdateBy = currentUser?.Id;
                        entry.Entity.CompanyId = currentUser?.CompanyId ?? entry.Entity.CompanyId;
                        entry.Entity.UseStatus = UseStatusEntity.Create;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}