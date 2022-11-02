using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models.EntityUtils;

namespace XYZEngineeringProject.Domain.Models
{
    public class UserTask : ISoftDataEntity
    {
        public Guid Id { get; set; }
        public DateTime Deadline { get; set; }
        public Priority Priority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }

        //relations

        public Guid AssignFromUserId { get; set; }
        public virtual AppUser AssignFromUser { get; set; }

        public Guid? AssignToUserId { get; set; }
        public virtual AppUser? AssignToUser { get; set; }

        public Guid? ListOfTasksId { get; set; }
        public virtual ListOfTasks? ListOfTasks { get; set; }

        // ISoftDataEntity
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public UseStatusEntity UseStatus { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
