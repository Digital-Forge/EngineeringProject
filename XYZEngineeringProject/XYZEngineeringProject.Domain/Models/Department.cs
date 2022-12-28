using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models.EntityUtils;
using XYZEngineeringProject.Domain.Models.File;

namespace XYZEngineeringProject.Domain.Models
{
    public class Department : ISoftDataEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? Manager { get; set; }
        public Guid? Forum { get; set; }

        //relations
        public Guid? IdDepartmentUp { get; set; }
        public virtual ICollection<UsersToDepartments> UsersToDepartments { get; set; }
        public virtual List<AccessDirectory> Directories { get; set; }

        // ISoftDataEntity
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public UseStatusEntity UseStatus { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
