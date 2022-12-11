using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Application.ViewModels.File
{
    public class FileStructureVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public List<FileVM> Files { get; set; }
        public List<FileStructureVM> Directories { get; set; }
    }
}
