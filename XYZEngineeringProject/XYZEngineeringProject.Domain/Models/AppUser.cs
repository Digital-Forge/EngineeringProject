using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models.EntityUtils;

namespace XYZEngineeringProject.Domain.Models
{
    public class AppUser : IdentityUser, ISoftDataEntity
    {
        [Key]
        public Guid UserId { get; set; }
        public override string Id { get => UserId.ToString(); set => UserId = new Guid(value); }

        public string FullName { get; set; }
        public int PESEL { get; set; }

        //relations

        public Guid? PositionId { get; set; }
        public Position? Position { get; set; }

        public Guid? AddressId { get; set; }
        public Address? Address { get; set; }

        public ICollection<UsersToDepartments>? UsersToDepartments { get; set; }

        public ICollection<UsersToClientsGroups>? UsersClientsGroups { get; set; }

        public ICollection<ListOfTasks>? ListTasks { get; set; }

        public ICollection<UserTask>? AsignerTasks { get; set; }

        public ICollection<UserTask>? AsigneeTasks { get; set; }

        public ICollection<UsersToPositions>? UsersToPositions { get; set; }

        public ICollection<NoteToUser>? NoteToUser { get; set; }

        // ISoftDataEntity
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public UseStatusEntity UseStatus { get; set; }
        public Guid CompanyId { get; set; }
        public LogicCompany? Company { get; set; } // bonus relation
    }
}
