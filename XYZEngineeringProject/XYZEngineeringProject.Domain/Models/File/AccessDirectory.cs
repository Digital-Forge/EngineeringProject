using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models.File
{
    public class AccessDirectory
    {
        // relation
        public Guid DirectoryId { get; set; }
        public Directory Directory { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
