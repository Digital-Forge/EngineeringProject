using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class Departments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdDepartmentUp { get; set; }

        public IList<UsersToDepartments> UsersToDepartments { get; set; }
    }
}
