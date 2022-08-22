using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class UsersToDepartments
    {
        //relations

        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; }

        public Guid DepartmentId { get; set; }
        public virtual Department Departments { get; set; }
    }
}
