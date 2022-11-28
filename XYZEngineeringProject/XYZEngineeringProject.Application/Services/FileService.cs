﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels.File;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Infrastructure.Repositories;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMeService _meService;

        public FileService(IFileRepository fileRepository, IMeService meService)
        {
            _fileRepository = fileRepository;
            _meService = meService;
        }

        public void ChangeDirectoryName(Guid id, string name)
        {
            _fileRepository.ChangeDirectoryName(id, name);
        }

        public void ChangeFileName(Guid id, string name)
        {
            _fileRepository.ChangeFileName(id, name);
        }

        public Guid? CreateDirectory(Guid paretntDictionaryId, string name)
        {
            var parent = _fileRepository.GetDirectory(paretntDictionaryId);
            if (parent == null) return null;

            return _fileRepository.CreateDirectory(paretntDictionaryId, name);
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

            if (structureVM.Directores == null)
            {
                structureVM.Directores = new List<FileStructureVM>();
            }

            var structureDB = _fileRepository.GetUserDirectories();

            foreach (var dir in structureDB)
            {
                if (dir == null) continue;

                structureVM.Directores.Add(mapStructureToVM(dir));
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
                Directores = new List<FileStructureVM>()
            };

            if (dir.ChildDirectories != null)
            {
                foreach (var item in dir.ChildDirectories)
                {
                    if (item == null) continue;

                    dirVM.Directores.Add(mapStructureToVM(item));
                }
            }

            return dirVM;
        }

        public bool SaveFile(FileVM file, Guid directoryId)
        {
            if (string.IsNullOrWhiteSpace(file.ObjectBase64)) return false;

            var fileId = _fileRepository.CreateFile(directoryId, file.Name, file.Format);

            if (fileId == null || fileId == Guid.Empty) return false;

            var fileDB = _fileRepository.GetFile(fileId.Value);

            try
            {
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
    }
}
