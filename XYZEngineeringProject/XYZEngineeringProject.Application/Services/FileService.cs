using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels.File;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMeService _meService;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly Context _context;
        private readonly Logger _logger;

        public FileService(IFileRepository fileRepository, IMeService meService, IDepartmentRepository departmentRepository, Context context, Logger logger)
        {
            _fileRepository = fileRepository;
            _meService = meService;
            _departmentRepository = departmentRepository;
            _context = context;
            _logger = logger;
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
                (new FileInfo(fileDB.Path.Replace($"\\{file.Id}", ""))).Directory.Create();

                using (MemoryStream fileStream = new MemoryStream(Convert.FromBase64String(file.ObjectBase64)))
                {
                    var save = new FileStream(fileDB.Path, FileMode.CreateNew);

                    fileStream.WriteTo(save);
                    fileStream.Close();
                    try
                    {
                        save.Close();
                        save.Dispose();

                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception e)
            {
                _fileRepository.DeleteFile(fileId.Value);
                throw e;
            }
            return true;
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

        public FileVM Download(Guid id)
        {
            var file = _context.Files.FirstOrDefault(x => x.Id == id);
            var obj = GetFile(id);

            if (file == null || obj == null || !obj.CanRead) return new FileVM
            {
                Id = id,
                Format = string.Empty,
                Path = string.Empty,
                Name = string.Empty,
                DirectoryId = string.Empty,
                ObjectBase64 = string.Empty,
            };

            var vm = new FileVM
            {
                Id = file.Id,
                DirectoryId = file.DirectoryId.ToString(),
                Name = file.Name,
                Path = file.PathMask,
                Format = file.Format
            };

            using (var stream = new MemoryStream())
            {
                obj.CopyTo(stream);
                vm.ObjectBase64 = Convert.ToBase64String(stream.ToArray());
            }

            try
            {
                obj.Close();
                obj.Dispose();
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Service, Logger.InfoType.Error, $"Nie można zamknąć lub zwolinć zasobu - {e.Message}");
            }

            return vm;
        }

        public List<FileStructureVM> GetAllCompanyDirectoryByCompany(Guid id)
        {
            if (id == null || id == Guid.Empty) return new List<FileStructureVM>();

            return _context.Directories
                .Where(x => x.CompanyId == id && x.DeepLevel == 1)
                .Select(s => new FileStructureVM
                {
                    Id = s.Id,
                    Name = s.Name,
                    Path = s.Path
                }).ToList();
        }
    }
}
