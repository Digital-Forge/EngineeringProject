using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public int PESEL { get; set; }
        public int? PositionId { get; set; }
        public Positions? Position { get; set; }
        public Address? Address { get; set; }
        public IList<UsersToDepartments>? UsersToDepartments { get; set; }
        public IList<UsersClientsGroups>? UsersClientsGroups { get; set; }
        public ICollection<ListOfTasks>? ListTasks { get; set; }
        public ICollection<Tasks>? AsignerTasks { get; set; }
        public ICollection<Tasks>? AsigneeTasks { get; set; }
        public IList<UsersToPositions>? UsersToPositions { get; set; }
        public IList<NoteToUser>? NoteToUser { get; set; }

    }
}
