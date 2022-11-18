using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Application.ViewModels.File
{
    public class FileStructureVM
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<string> Files { get; set; }
        public List<FileStructureVM> Directores { get; set; }
    }
}
