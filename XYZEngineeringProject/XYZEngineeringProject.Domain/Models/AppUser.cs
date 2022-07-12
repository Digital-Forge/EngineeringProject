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

        //relations

        public Guid? PositionId { get; set; }
        public Position? Position { get; set; }

        public Guid? AddressId { get; set; }
        public Address? Address { get; set; }

        public IList<UsersToDepartments>? UsersToDepartments { get; set; }

        public IList<UsersClientsGroups>? UsersClientsGroups { get; set; }

        public ICollection<ListOfTasks>? ListTasks { get; set; }

        public ICollection<Task>? AsignerTasks { get; set; }

        public ICollection<Task>? AsigneeTasks { get; set; }

        public IList<UsersToPositions>? UsersToPositions { get; set; }

        public IList<NoteToUser>? NoteToUser { get; set; }

    }
}
