using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Domain.Interfaces
{
    public interface IFileRepository
    {
        string GetFilesRootPath();
        string GetFilePath(Guid id);
        Models.File.Directory? GetDirectory(Guid id);
        IQueryable<Models.File.Directory> GetDirectoryAsQuerable(Guid id);
        Models.File.File? GetFile(Guid id);
        IQueryable<Models.File.File> GetFileAsQuerable(Guid id);
        void ChangeDirectoryName(Guid id, string name);
        void ChangeFileName(Guid id, string name);
        Guid? CreateFile(Guid dictionaryId, string name, string format);
        void DeleteFile(Guid id);
        void DeleteDirectory(Guid id);
        Guid? CreateDirectory(Guid paretntDictionaryId, string name);
        bool AccessToDirectory(Guid id);
        List<Models.File.Directory>? GetUserDirectories();

        void _CreateDepartmentDirectory(Department department);
    }
}
