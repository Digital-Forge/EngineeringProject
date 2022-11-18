using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.ViewModels.File;

namespace XYZEngineeringProject.Application.Interfaces
{
    public interface IFileService
    {
        FileStructureVM GetStructure();
    }
}
