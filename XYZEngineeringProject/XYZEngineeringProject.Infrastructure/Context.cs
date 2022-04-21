using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<Address> UserAddresses { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientAdress> ClientsAdresses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ListOfTasks> ListTasks { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<NoteToUser> NoteToUser { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Domain.Models.Task> Tasks { get; set; }
        public DbSet<UsersClientsGroups> UsersClientsGroups { get; set; }
        public DbSet<UsersToDepartments> UsersToDepartments { get; set; }
        public DbSet<UsersToPositions> UsersToPositions { get; set; }
        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //////////////////////////////////////// AppUser one to one Address
            builder.Entity<AppUser>()
                .HasOne<Address>( u => u.Address)
                .WithOne(ad => ad.User)
                .HasForeignKey<Address>( ad => ad.AppUserId);

            //////////////////////////////////////// ListOfTasks many to one AppUser
            builder.Entity<ListOfTasks>()
                .HasOne<AppUser>(u => u.User)
                .WithMany( u => u.ListTasks)
                .HasForeignKey( u => u.UserId);

            //////////////////////////////////////// Tasks many to one ListOfTasks
            builder.Entity<Domain.Models.Task>()
                .HasOne<ListOfTasks>(u => u.ListOfTasks)
                .WithMany(u => u.Task)
                .HasForeignKey(u => u.ListOfTasksId);

            //////////////////////////////////////// Tasks many to one AppUser
            builder.Entity<Domain.Models.Task>()
                .HasOne<AppUser>( u => u.AssignerUser)
                .WithMany( u => u.AsignerTasks)
                .HasForeignKey( u => u.AssignerUserId);

            //////////////////////////////////////// Tasks many to one AppUser
            builder.Entity<Domain.Models.Task>()
                .HasOne<AppUser>(u => u.AssigneeUser)
                .WithMany(u => u.AsigneeTasks)
                .HasForeignKey(u => u.AssigneeUserId);

            //////////////////////////////////////// UsersClientsGroups many to one Groups
            builder.Entity<UsersClientsGroups>()
                .HasOne<Group>(s => s.Group)
                .WithMany(g => g.UsersClientsGroups)
                .HasForeignKey(s => s.GroupId);

            //////////////////////////////////////// ClientsAddress many to one Client
            builder.Entity<ClientAdress>()
                .HasOne<Client>(s => s.Client)
                .WithMany(g => g.ClientAdresses)
                .HasForeignKey(s => s.ClientId);

            //////////////////////////////////////// Client many to many AppUser (UsersClientsGroups)
            builder.Entity<UsersClientsGroups>().HasKey( x => new {x.UserId, x.ClientId });
            
            builder.Entity<UsersClientsGroups>()
                .HasOne<Client>(s => s.Client)
                .WithMany( s => s.UsersClientsGroups)
                .HasForeignKey( s => s.ClientId);
            
            builder.Entity<UsersClientsGroups>()
                .HasOne<AppUser>(s => s.User)
                .WithMany(s => s.UsersClientsGroups)
                .HasForeignKey(s => s.UserId);

            //////////////////////////////////////// AppUser many to many Departments (UsersToDepartments)
            builder.Entity<UsersToDepartments>().HasKey( x => new { x.UserId , x.DepartmentId });
                        
            builder.Entity<UsersToDepartments>()
                .HasOne<AppUser>(s => s.User)
                .WithMany(s => s.UsersToDepartments)
                .HasForeignKey(s => s.UserId);
            
            builder.Entity<UsersToDepartments>()
                .HasOne<Department>( s => s.Departments)
                .WithMany( s => s.UsersToDepartments)
                .HasForeignKey( s=> s.DepartmentId);

            //////////////////////////////////////// AppUser many to many Positions (UsersToPositions)
            builder.Entity<UsersToPositions>().HasKey( x => new {x.UserId, x.PositionId});
                        
            builder.Entity<UsersToPositions>()
                .HasOne<AppUser>( u => u.User)
                .WithMany( u => u.UsersToPositions)
                .HasForeignKey( u => u.UserId);
                        
            builder.Entity<UsersToPositions>()
                .HasOne<Position>( u => u.Position)
                .WithMany( u => u.UsersToPositions)
                .HasForeignKey( u => u.PositionId);

            //////////////////////////////////////// AppUser many to many Note (NoteToUser)
            builder.Entity<NoteToUser>().HasKey( x => new {x.UserId, x.NoteId });
                    
            builder.Entity<NoteToUser>()
                .HasOne<AppUser>( u => u.User)
                .WithMany( u => u.NoteToUser)
                .HasForeignKey( u => u.UserId);
                        
            builder.Entity<NoteToUser>()
                .HasOne<Note>( u => u.Note)
                .WithMany( u => u.NoteToUsers)
                .HasForeignKey( u => u.NoteId);

        }
    }
}