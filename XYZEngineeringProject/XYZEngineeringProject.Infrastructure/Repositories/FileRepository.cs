using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models.File;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly Context _context;
        private readonly Logger _logger;
        private readonly InfrastructureUtils _infrastructureUtils;
        private readonly IConfiguration _configuration;

        private static string? rootPathBuffor = null;

        public FileRepository(Context context, Logger logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _infrastructureUtils = new InfrastructureUtils(context, httpContextAccessor);
            _configuration = configuration;
        }

        public void ChangeDirectoryName(Guid id, string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return;

            var directory = _context.Directories
                .FirstOrDefault(x => 
                    x.Id == id 
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete 
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.SolidConst);

            if (directory == null) return;

            directory.Name = name;
            _context.SaveChanges();
        }

        public void ChangeFileName(Guid id, string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return;

            var file = _context.Files
                .FirstOrDefault(x =>
                    x.Id == id
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.SolidConst);

            if (file == null) return;

            file.Name = name;
            _context.SaveChanges();
        }

        public Guid? CreateDirectory(Guid paretntDictionaryId, string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            var user = _infrastructureUtils.GetUserFormHttpContext();

            var directoryParent = _context.Directories
                .FirstOrDefault(x =>
                    x.Id == paretntDictionaryId
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete
                    && x.CompanyId == user.CompanyId);

            if (directoryParent == null) return null;

            var directory = new Domain.Models.File.Directory
            {
                Name = name,
                ParentDirectoryId = directoryParent.ParentDirectoryId,
                PathMask = directoryParent.PathMask + "\\" + name,
            };

            _context.Directories.Add(directory);
            _context.SaveChanges();

            directory.Path = directoryParent.Path + "\\" + directory.Id;

            _context.SaveChanges();

            return directory.Id;
        }

        public Guid? CreateFile(Guid dictionaryId, string name, string format)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            var user = _infrastructureUtils.GetUserFormHttpContext();

            var directoryParent = _context.Directories
                .FirstOrDefault(x =>
                    x.Id == dictionaryId
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete
                    && x.CompanyId == user.CompanyId);

            if (directoryParent == null) return null;

            var file = new Domain.Models.File.File
            {
                Name = name,
                Format = format,
                DirectoryId = directoryParent.Id,
                PathMask = directoryParent.PathMask + "\\" + name,
            };

            _context.Files.Add(file);
            _context.SaveChanges();

            file.Path = directoryParent.Path + "\\" + file.Id;

            _context.SaveChanges();

            return file.Id;
        }

        public void DeleteDirectory(Guid id)
        {
            var user = _infrastructureUtils.GetUserFormHttpContext();

            var directory = _context.Directories
                .Include(i => i.ChildDirectories)
                .Include(i => i.Files)
                .FirstOrDefault(x =>
                    x.Id == id
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete
                    && x.CompanyId == user.CompanyId);

            if (directory == null) return;

            if (directory.Files != null)
            {
                foreach (var item in directory.Files)
                {
                    DeleteFile(item.Id);
                }
            }
            
            if (directory.ChildDirectories != null)
            {
                foreach (var item in directory.ChildDirectories)
                {
                    DeleteDirectory(item.Id);
                }
            }            

            _context.Directories.Remove(directory);
            _context.SaveChanges();
        }

        public void DeleteFile(Guid id)
        {
            var user = _infrastructureUtils.GetUserFormHttpContext();

            var file = _context.Files
                .FirstOrDefault(x =>
                    x.Id == id
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete
                    && x.CompanyId == user.CompanyId);

            if (file == null) return;

            _context.Files.Remove(file);
            _context.SaveChanges();
        }

        public string GetFilesRootPath()
        {
            if (rootPathBuffor != null)
            {
                return rootPathBuffor;
            }

            try
            {
                var path = _configuration.GetValue<string>("FilesRootPath");
               
                if(string.IsNullOrWhiteSpace(path))
                {
                    return "\\ProgramFiles";
                }
                else
                {
                    rootPathBuffor = path;
                    return path;
                }
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, "Appsettings value missing - " + e.Message);
                return "\\ProgramFiles";
            }
        }

        public string GetFilePath(Guid id)
        {
            if (id == Guid.Empty) return string.Empty;

            var file = _context.Files
                .Include(i => i.Directory)
                .FirstOrDefault(x => 
                    x.Id == id 
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete);

            if (file == null) return string.Empty;

            if (!AccessToDirectory(file.DirectoryId)) return string.Empty;

            return GetFilesRootPath() + file.Path;
        }

        public bool AccessToDirectory(Guid id)
        {
            var userId = _infrastructureUtils.GetUserIdFormHttpContext();

            if (userId == null || userId == Guid.Empty) return false;

            var dictionary = _context.Directories.Where(x => x.Id == id && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete);

            var currentDictionary = dictionary.FirstOrDefault();
            if (currentDictionary == null || currentDictionary.DeepLevel == 0) return false;

            var user = _context.Users
               .Include(i => i.UsersToDepartments)
               .FirstOrDefault(x => x.Id == userId);

            if (user == null || user.UsersToDepartments == null) return false;

            for (int deep = 0; deep < currentDictionary.DeepLevel - 1; deep++)
            {
                dictionary = dictionary.Select(x => x.ParentDirectory);
            }

            var baseDirectory = dictionary.Include(i => i.Departments).FirstOrDefault();

            if (baseDirectory?.Departments.Any(x => user.UsersToDepartments.Any(y => y.DepartmentId == x.DepartmentId)) ?? false)
            {
                return true;
            }

            return false;
        }

        public Domain.Models.File.Directory? GetDirectory(Guid id)
        {
            if (!AccessToDirectory(id)) return null;

            var user = _infrastructureUtils.GetUserFormHttpContext();

            var directory = _context.Directories
                .FirstOrDefault(x =>
                    x.Id == id
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete
                    && x.CompanyId == user.CompanyId);

            return directory;
        }

        public IQueryable<Domain.Models.File.Directory> GetDirectoryAsQuerable(Guid id)
        {
            if (!AccessToDirectory(id)) return null;

            var user = _infrastructureUtils.GetUserFormHttpContext();

            var directory = _context.Directories
                .Where(x =>
                    x.Id == id
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete
                    && x.CompanyId == user.CompanyId);

            return directory;
        }

        public Domain.Models.File.File? GetFile(Guid id)
        {
            var user = _infrastructureUtils.GetUserFormHttpContext();

            if (user == null) return null;

            var file = _context.Files
                .FirstOrDefault(x =>
                    x.Id == id
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete
                    && x.CompanyId == user.CompanyId);

            return file;
        }

        public IQueryable<Domain.Models.File.File> GetFileAsQuerable(Guid id)
        {
            var user = _infrastructureUtils.GetUserFormHttpContext();

            var file = _context.Files
                .Where(x =>
                    x.Id == id
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete
                    && x.CompanyId == user.CompanyId);

            return file;
        }
    }
}
