using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels.File;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Infrastructure.Repositories;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMeService _meService;
        private readonly IDepartmentRepository _departmentRepository;

        public FileService(IFileRepository fileRepository, IMeService meService, IDepartmentRepository departmentRepository)
        {
            _fileRepository = fileRepository;
            _meService = meService;
            _departmentRepository = departmentRepository;
        }

        public void ChangeDirectoryName(Guid id, string name)
        {
            _fileRepository.ChangeDirectoryName(id, name);
        }

        public void ChangeFileName(Guid id, string name)
        {
            _fileRepository.ChangeFileName(id, name);
        }

        public Guid? CreateDirectory(Guid parentDirectoryId, string name)
        {
            var parent = _fileRepository.GetDirectory(parentDirectoryId);
            if (parent == null) return null;

            return _fileRepository.CreateDirectory(parentDirectoryId, name);
        }

        public void DeleteDirectory(Guid id)
        {
            _fileRepository.DeleteDirectory(id);
        }

        public void DeleteFile(Guid id)
        {
            _fileRepository.DeleteFile(id);
        }

        public FileStream? GetFile(Guid id)
        {
            var fileDB = _fileRepository.GetFile(id);

            if (fileDB == null) return null;

            return new FileStream(fileDB.Path, FileMode.Open, FileAccess.Read);
        }
        public FileStructureVM GetStructure()
        {
            var company = _meService.GetMyCompany();

            var structureVM = new FileStructureVM()
            {
                Name = company.Name                
            };

            if (structureVM.Directories == null)
            {
                structureVM.Directories = new List<FileStructureVM>();
            }

            var structureDB = _fileRepository.GetUserDirectories();

            foreach (var dir in structureDB)
            {
                if (dir == null) continue;

                structureVM.Directories.Add(mapStructureToVM(dir));
            }

            return structureVM;
        }

        private FileStructureVM mapStructureToVM(Domain.Models.File.Directory dir)
        {
            var dirVM = new FileStructureVM()
            {
                Id = dir.Id,
                Name = dir.Name,
                Path = dir.PathMask,
                Files = dir.Files.Select(x => new FileVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Path = x.PathMask,
                    Format = x.Format,
                    ObjectBase64 = null
                }).ToList(),
                Directories = new List<FileStructureVM>()
            };

            if (dir.ChildDirectories != null)
            {
                foreach (var item in dir.ChildDirectories)
                {
                    if (item == null) continue;

                    dirVM.Directories.Add(mapStructureToVM(item));
                }
            }

            return dirVM;
        }

        public bool SaveFile(FileVM file)
        {
            if (string.IsNullOrWhiteSpace(file.ObjectBase64)) return false;

            var fileId = _fileRepository.CreateFile(Guid.Parse(file.DirectoryId), file.Name, file.Format);

            if (fileId == null || fileId == Guid.Empty) return false;

            var fileDB = _fileRepository.GetFile(fileId.Value);

            try
            {
                (new FileInfo(fileDB.Path.Replace($"\\{file.Id}",""))).Directory.Create(); 

                using (MemoryStream fileStream = new MemoryStream(Convert.FromBase64String(file.ObjectBase64)))
                {
                    var save = new FileStream(fileDB.Path, FileMode.CreateNew);

                    fileStream.WriteTo(save);
                    fileStream.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                _fileRepository.DeleteFile(fileId.Value);
                throw e;
            }
        }

        public bool SaveFile(MemoryStream file, Guid directoryId, string name, string format)
        {
            if (file == null) return false;

            var fileId = _fileRepository.CreateFile(directoryId, name, format);

            if (fileId == null || fileId == Guid.Empty) return false;

            var fileDB = _fileRepository.GetFile(fileId.Value);

            try
            {
                var save = new FileStream(fileDB.Path, FileMode.CreateNew);
                file.WriteTo(save);
                save.Close();
                return true;

            }
            catch (Exception e)
            {
                _fileRepository.DeleteFile(fileId.Value);
                throw e;
            }
        }

        public void _AddDepartmentDirectory(Department department)
        {
            _fileRepository._CreateDepartmentDirectory(department);
        }

        public void _AddDepartmentDirectory(Guid departmentId)
        {
            var department = _departmentRepository.GetDepartmentById(departmentId);

            if (department == null) return;

            _fileRepository._CreateDepartmentDirectory(department);
        }
    }
}
