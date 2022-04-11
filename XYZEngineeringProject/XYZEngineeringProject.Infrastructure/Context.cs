using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<Address> UserAddresses { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<ClientsAdresses> ClientsAdresses { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<ListOfTasks> ListTasks { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<NoteToUser> NoteToUser { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<UsersClientsGroups> UsersClientsGroups { get; set; }
        public DbSet<UsersToDepartments> UsersToDepartments { get; set; }
        public DbSet<UsersToPositions> UsersToPositions { get; set; }
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
            builder.Entity<ListOfTasks>()
                .HasOne<AppUser>(u => u.User)
                .WithMany( u => u.ListTasks)
                .HasForeignKey( u => u.TaskId);

            ////////////////////////////////////////  many to one 
            builder.Entity<Tasks>()
                .HasOne<ListOfTasks>(u => u.ListOfTasks)
                .WithMany(u => u.Task)
                .HasForeignKey(u => u.ListOfTasksId);

            ////////////////////////////////////////  many to one 
            builder.Entity<Tasks>()
                .HasOne<AppUser>( u => u.AssignerUser)
                .WithMany( u => u.AsignerTasks)
                .HasForeignKey( u => u.AssignerUserId);

            ////////////////////////////////////////  many to one 
            builder.Entity<Tasks>()
                .HasOne<AppUser>(u => u.AssigneeUser)
                .WithMany(u => u.AsigneeTasks)
                .HasForeignKey(u => u.AssigneeUserId);

            ////////////////////////////////////////  many to one 
            builder.Entity<UsersClientsGroups>()
                .HasOne<Groups>(s => s.Group)
                .WithMany(g => g.UsersClientsGroups)
                .HasForeignKey(s => s.GroupId);

            //////////////////////////////////////// many to many
            builder.Entity<UsersClientsGroups>().HasKey( x => new {x.UserId, x.ClientId });

            ////////////////////////////////////////  many to one 
            builder.Entity<UsersClientsGroups>()
                .HasOne<Groups>(s => s.Group)
                .WithMany( s => s.UsersClientsGroups)
                .HasForeignKey( s => s.GroupId);
            ////////////////////////////////////////  many to one 
            builder.Entity<UsersClientsGroups>()
                .HasOne<AppUser>(s => s.User)
                .WithMany(s => s.UsersClientsGroups)
                .HasForeignKey(s => s.UserId);

            //////////////////////////////////////// many to many
            builder.Entity<UsersToDepartments>().HasKey( x => new { x.UserId , x.DepartmentId });

            ////////////////////////////////////////  many to one 
            builder.Entity<UsersToDepartments>()
                .HasOne<AppUser>(s => s.User)
                .WithMany(s => s.UsersToDepartments)
                .HasForeignKey(s => s.UserId);

            ////////////////////////////////////////  many to one 
            builder.Entity<UsersToDepartments>()
                .HasOne<Departments>( s => s.Departments)
                .WithMany( s => s.UsersToDepartments)
                .HasForeignKey( s=> s.DepartmentId);

            //////////////////////////////////////// many to many
            builder.Entity<UsersToPositions>().HasKey( x => new {x.UserId, x.PositionId});

            ////////////////////////////////////////  many to one 
            builder.Entity<UsersToPositions>()
                .HasOne<AppUser>( u => u.User)
                .WithMany( u => u.UsersToPositions)
                .HasForeignKey( u => u.UserId);

            ////////////////////////////////////////  many to one 
            builder.Entity<UsersToPositions>()
                .HasOne<Positions>( u => u.Position)
                .WithMany( u => u.UsersToPositions)
                .HasForeignKey( u => u.PositionId);

            //////////////////////////////////////// many to many
            builder.Entity<NoteToUser>().HasKey( x => new {x.UserId, x.NoteId });

            ////////////////////////////////////////  many to one 
            builder.Entity<NoteToUser>()
                .HasOne<AppUser>( u => u.User)
                .WithMany( u => u.NoteToUser)
                .HasForeignKey( u => u.UserId);

            ////////////////////////////////////////  many to one 
            builder.Entity<NoteToUser>()
                .HasOne<Note>( u => u.Note)
                .WithMany( u => u.NoteToUsers)
                .HasForeignKey( u => u.NoteId);

        }
    }
}