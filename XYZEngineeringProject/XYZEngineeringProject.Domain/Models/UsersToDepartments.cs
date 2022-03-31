using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class UsersToDepartments
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int DepartmentId { get; set; }
        public Departments Departments { get; set; }
    }
}
