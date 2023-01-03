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
    public class AppUser : IdentityUser<Guid>, ISoftDataEntity
    {

        //public Guid UserId { get; set; }
        //public override string Id { get => UserId.ToString(); set => UserId = new Guid(value); }

        public string Firstname { get; set; }
        public string Surname { get; set; }

        public string PESEL { get; set; }

        //relations

        public Guid? PositionId { get; set; }
        public Position Position { get; set; }

        public Guid? AddressId { get; set; }
        public Address Address { get; set; }

        public virtual ICollection<UsersToDepartments> UsersToDepartments { get; set; }

        public virtual ICollection<UsersToClients> UsersToClientsGroups { get; set; }

        public virtual ICollection<ListOfTasks> ListTasks { get; set; }

        public virtual ICollection<UserTask> AsignerTasks { get; set; }

        public virtual ICollection<UserTask> AsigneeTasks { get; set; }

        public virtual ICollection<UsersToPositions> UsersToPositions { get; set; }

        public virtual ICollection<NoteToUser> NoteToUser { get; set; }

        // ISoftDataEntity
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public UseStatusEntity UseStatus { get; set; }
        public Guid? CompanyId { get; set; }
        public LogicCompany? Company { get; set; } // bonus relation
    }
}
