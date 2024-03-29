﻿using Microsoft.AspNetCore.Http;
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
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Domain.Models.EntityUtils;
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

        public Guid? CreateDirectory(Guid parentDirectoryId, string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            var user = _infrastructureUtils.GetUserFormHttpContext();

            var directoryParent = _context.Directories
                .FirstOrDefault(x =>
                    x.Id == parentDirectoryId
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete
                    && x.CompanyId == user.CompanyId);

            if (directoryParent == null) return null;

            var directory = new Domain.Models.File.Directory
            {
                Name = name,
                ParentDirectoryId = directoryParent.Id,
                PathMask = directoryParent.PathMask + "\\" + name,
                DeepLevel = directoryParent.DeepLevel + 1,
                Path = string.Empty,
            };

            _context.Directories.Add(directory);
            _context.SaveChanges();

            directory.Path = directoryParent.Path + "\\" + directory.Id;

            _context.SaveChanges();

            return directory.Id;
        }

        public Guid? CreateFile(Guid directoryId, string name, string format)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            var user = _infrastructureUtils.GetUserFormHttpContext();

            var directoryParent = _context.Directories
                .FirstOrDefault(x =>
                    x.Id == directoryId
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete
                    && x.CompanyId == user.CompanyId);

            if (directoryParent == null) return null;

            var file = new Domain.Models.File.File
            {
                Name = name,
                Format = format,
                DirectoryId = directoryParent.Id,
                PathMask = directoryParent.PathMask + "\\" + name,
                Path = string.Empty,
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

            if (directory == null || directory.DeepLevel < 2) return;

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

                if (string.IsNullOrWhiteSpace(path))
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

            var directory = _context.Directories
                .Where(x => x.Id == id && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete);

            var currentDirectory = directory.FirstOrDefault();
            if (currentDirectory == null || currentDirectory.DeepLevel == 0) return false;

            var deepInclude = string.Concat(Enumerable.Repeat("ParentDirectory.", currentDirectory.DeepLevel - 1)) + "Departments";

            directory = _context.Directories
                .Include(deepInclude)
                .Where(x => x.Id == id && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete); ;

            var user = _context.Users
               .Include(i => i.UsersToDepartments)
               .FirstOrDefault(x => x.Id == userId);

            if (user == null || user.UsersToDepartments == null) return false;

            for (int deep = 0; deep < currentDirectory.DeepLevel - 1; deep++)
            {
                directory = directory.Select(x => x.ParentDirectory);
            }

            var baseDirectory = directory.FirstOrDefault();

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
            file.Path = GetFilesRootPath() + file.Path;
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

        public List<Domain.Models.File.Directory>? GetUserDirectories()
        {
            var user = _infrastructureUtils.GetUserFormHttpContext();

            if (user == null) return null;

            var directoryList = _context.AppUsers
                .Include(i => i.UsersToDepartments)
                .ThenInclude(i => i.Departments)
                .ThenInclude(i => i.Directories)
                .ThenInclude(i => i.Directory)
                .ThenInclude(i => i.ChildDirectories.Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete))
                .ThenInclude(i => i.Files.Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete))
                .Include(i => i.UsersToDepartments)
                .ThenInclude(i => i.Departments)
                .ThenInclude(i => i.Directories)
                .ThenInclude(i => i.Directory)
                .ThenInclude(i => i.Files.Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete))
                .Where(x => x.Id == user.Id && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .SelectMany(s => s.UsersToDepartments)
                .Select(s => s.Departments)
                .SelectMany(s => s.Directories)
                .Select(s => s.Directory)
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .ToList();

            if (directoryList == null) return null;

            foreach (var dir_1 in directoryList)
            {
                foreach (var dir_2 in dir_1.ChildDirectories)
                {
                    dir_2.ChildDirectories = GetUserDirectories(dir_2).ChildDirectories?.Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete).ToList();
                }
            }

            return directoryList;
        }

        private Domain.Models.File.Directory? GetUserDirectories(Domain.Models.File.Directory? directory)
        {
            if (directory == null) return null;

            var child_dir = _context.Directories
                .Include(i => i.ChildDirectories.Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete))
                .ThenInclude(i => i.Files.Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete))
                .Where(x => x.Id == directory.Id && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Select(x => x.ChildDirectories).FirstOrDefault();

            if (child_dir == null) return directory;

            for (int i = 0; i < child_dir.Count; i++)
            {
                child_dir[i] = GetUserDirectories(child_dir[i]);
                child_dir[i].ParentDirectory = directory;
            }

            directory.ChildDirectories = child_dir.Count == 0 ? null : child_dir;
            return directory;
        }

        public void _CreateDepartmentDirectory(Department department, LogicCompany? company = null)
        {
            if (company == null)
            {
                company = _infrastructureUtils.GetCompany();
            }

            var dir = new Domain.Models.File.Directory
            {
                Name = department.Name,
                DeepLevel = 1,
                Path = $"\\{company.Id}\\",
                PathMask = $"\\{company.Id}\\{department.Name}",
                CompanyId= company.Id,
            };

            _context.Directories.Add(dir);
            _context.SaveChanges();

            dir.Path += dir.Id.ToString();

            _context.AccessDirectories.Add(new AccessDirectory
            {
                DepartmentId = department.Id,
                DirectoryId = dir.Id
            });

            _context.SaveChanges();
        }
    }
}
