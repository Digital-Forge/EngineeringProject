using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //relations

        public Guid? IdDepartmentUp { get; set; }

        public ICollection<UsersToDepartments>? UsersToDepartments { get; set; }
    }
}
