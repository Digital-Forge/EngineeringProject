using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models.EntityUtils;

namespace XYZEngineeringProject.Domain.Models.File
{
    public class Directory : ISoftDataEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string PathMask { get; set; }
        public int DeepLevel { get; set; }

        // relation
        public Guid? ParentDirectoryId { get; set; }
        public virtual Directory? ParentDirectory { get; set; }
        public virtual List<Directory> ChildDirectories { get; set; }
        public virtual List<File> Files { get; set; }
        public virtual List<AccessDirectory> Departments { get; set; }

        // ISoftDataEntity
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public UseStatusEntity UseStatus { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
