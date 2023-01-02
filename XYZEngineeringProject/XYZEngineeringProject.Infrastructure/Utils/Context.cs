using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Domain.Models.EntityUtils;
using XYZEngineeringProject.Domain.Models.Forum;

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
        public DbSet<EmailConfig> EmailConfigs { get; set; }

        //Files
        public DbSet<Domain.Models.File.File> Files { get; set; }
        public DbSet<Domain.Models.File.Directory> Directories { get; set; }
        public DbSet<Domain.Models.File.AccessDirectory> AccessDirectories { get; set; }

        //Forum
        public DbSet<Forum> Forums { get; set; }
        public DbSet<ForumMessage> ForumMessages { get; set; }

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
                .HasOne(u => u.AssignFromUser)
                .WithMany(u => u.AsignerTasks)
                .HasForeignKey(u => u.AssignFromUserId);

            //////////////////////////////////////// Tasks many to one AppUser
            builder.Entity<UserTask>()
                .HasOne(u => u.AssignToUser)
                .WithMany(u => u.AsigneeTasks)
                .HasForeignKey(u => u.AssignToUserId);

            //////////////////////////////////////// File many to one Directory
            builder.Entity<Domain.Models.File.File>()
                .HasOne(u => u.Directory)
                .WithMany(u => u.Files)
                .HasForeignKey(u => u.DirectoryId);

            //////////////////////////////////////// Directory many to one Directory
            builder.Entity<Domain.Models.File.Directory>()
                .HasOne(u => u.ParentDirectory)
                .WithMany(u => u.ChildDirectories)
                .HasForeignKey(u => u.ParentDirectoryId);

            //////////////////////////////////////// ForumMessage many to one Forum
            builder.Entity<ForumMessage>()
                .HasOne(u => u.Forum)
                .WithMany(u => u.ForumMessages)
                .HasForeignKey(u => u.ForumId);

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

            //////////////////////////////////////// Directories many to many Departments (AccessDirectory)
            builder.Entity<Domain.Models.File.AccessDirectory>().HasKey(x => new { x.DirectoryId, x.DepartmentId });

            builder.Entity<Domain.Models.File.AccessDirectory>()
                .HasOne(u => u.Directory)
                .WithMany(u => u.Departments)
                .HasForeignKey(u => u.DirectoryId);

            builder.Entity<Domain.Models.File.AccessDirectory>()
                .HasOne(u => u.Department)
                .WithMany(u => u.Directories)
                .HasForeignKey(u => u.DepartmentId);

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
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Detached:
                    case EntityState.Deleted:
                        entry.Entity.UpdateDate = DateTime.Now;
                        entry.Entity.UpdateBy = currentUser?.Id;
                        entry.Entity.UseStatus = UseStatusEntity.Delete;
                        entry.State = hardMode ? EntityState.Deleted : EntityState.Modified;
                        break;
                    case EntityState.Modified:
                        if (entry.Entity.UseStatus != UseStatusEntity.Delete)
                        {
                            entry.Entity.UpdateDate = DateTime.Now;
                            entry.Entity.UpdateBy = currentUser?.Id;
                            entry.Entity.UseStatus = UseStatusEntity.Update;
                        }
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