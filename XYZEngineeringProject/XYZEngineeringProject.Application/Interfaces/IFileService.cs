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
        void ChangeDirectoryName(Guid id, string name);
        void ChangeFileName(Guid id, string name);
        bool SaveFile(FileVM file, Guid directoryId);
        bool SaveFile(MemoryStream file, Guid directoryId, string name, string format);
        void DeleteFile(Guid id);
        void DeleteDirectory(Guid id);
        Guid? CreateDirectory(Guid paretntDictionaryId, string name);
        FileStream? GetFile(Guid id);
    }
}
