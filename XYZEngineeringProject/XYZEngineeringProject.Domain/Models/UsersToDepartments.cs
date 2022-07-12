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

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Departments { get; set; }
    }
}
