using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Domain.Models.EntityUtils;
using XYZEngineeringProject.Infrastructure.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace XYZEngineeringProject.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly Context _context;
        private readonly InfrastructureUtils _infrastructureUtils;
        private readonly Logger _logger;
        private readonly IFileRepository _fileRepository;
        private readonly IForumRepository _forumRepository;

        public DepartmentRepository(
            Context context, 
            IHttpContextAccessor 
            httpContextAccessor, 
            Logger logger, 
            IFileRepository fileRepository,
            IForumRepository forumRepository)
        {
            _context = context;
            _infrastructureUtils = new InfrastructureUtils(context, httpContextAccessor);
            _logger = logger;
            _fileRepository = fileRepository;
            _forumRepository = forumRepository;
        }

        public Guid Add(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();

            _logger.Log(Logger.Source.Repository, Logger.InfoType.Info, $"Add department - {department.Id}");

            //create forum
            department.Forum = _forumRepository.AddForum(new Domain.Models.Forum.Forum
            {
                Name = department.Name,
            });
            _context.SaveChanges();

            //create directory from departmnet
            _fileRepository._CreateDepartmentDirectory(_context.Departments.FirstOrDefault(x => x.Id == department.Id));

            return department.Id;
        }

        public Guid _Add(Department department, LogicCompany company)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();

            _logger.Log(Logger.Source.Repository, Logger.InfoType.Info, $"Add department - {department.Id}");

            //create forum
            department.Forum = _forumRepository.AddForum(new Domain.Models.Forum.Forum
            {
                Name = department.Name,
                CompanyId = company.Id,
            });
            _context.SaveChanges();

            //create directory from departmnet
            _fileRepository._CreateDepartmentDirectory(_context.Departments.FirstOrDefault(x => x.Id == department.Id), company);

            return department.Id;
        }

        public IQueryable<Department>? GetAll()
        {
            var currentUser = _infrastructureUtils.GetUserFormHttpContext();

            if (currentUser?.CompanyId == null || currentUser?.CompanyId == Guid.Empty)
                return null;
            else
                return _context.Departments
                    .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                    .Where(x => x.CompanyId == currentUser.CompanyId);
        }

        public IQueryable<Department>? GetDepartmentByCompany()
        {
            var companyId = _infrastructureUtils.GetCompany()?.Id;

            if (companyId == null) return null;

            return _context.Departments
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Where(x => x.CompanyId == companyId);
        }

        public IQueryable<Department>? GetDepartmentByCompany(Guid companyId)
        {
            return _context.Departments
                 .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                 .Where(x => x.CompanyId == companyId);
        }

        public Department? GetDepartmentById(Guid departmentId)
        {
            return _context.Departments
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .FirstOrDefault(x => x.Id == departmentId);
        }

        public IQueryable<Department> GetDepartmentByIdAsQuerable(Guid departmentId)
        {
            return _context.Departments
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Where(x => x.Id == departmentId);
        }

        public bool Remove(Department department)
        {
            if (department == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove null department");
                return false;
            }

            try
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove department - {department.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed remove department - {department.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool Remove(Guid departmentById)
        {
            if (departmentById == Guid.Empty)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove department by empty guid");
                return false;
            }

            try
            {
                var buff = GetDepartmentById(departmentById);
                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove department, who don't exist or deleted");
                    return false;
                }

                _context.Departments.Remove(buff);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove department - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed remove department - {departmentById} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool Update(Department department)
        {
            if (department == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying update null department");
                return false;
            }

            try
            {
                _context.Departments.Update(department);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Update department - {department.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed update department - {department.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public IQueryable<Department> _GetEveryOne()
        {
            return _context.Departments;
        }

        public bool __RemoveHard(Department department)
        {
            try
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove department - {department.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed hard remove department - {department.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool __RemoveHard(Guid departmentById)
        {
            try
            {
                var buff = _context.Departments.FirstOrDefault(x => x.Id == departmentById);
                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying hard remove department, who don't exist");
                    return false;
                }

                _context.Departments.Remove(buff);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove department - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed hard remove department - {departmentById} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public void AddUserToDepartment(AppUser user, Department department)
        {
            AddUserToDepartment(user.Id, department.Id);
        }

        public void AddUserToDepartment(Guid userId, Guid departmentId)
        {
            _context.UsersToDepartments.Add(new UsersToDepartments
            {
                DepartmentId = departmentId,
                UserId = userId
            });
            _context.SaveChanges();
        }

        public void RemoveUserFromDepartment(AppUser user, Department department)
        {
            RemoveUserFromDepartment(user.Id, department.Id);
        }

        public void RemoveUserFromDepartment(Guid userId, Guid departmentId)
        {
            var buff = _context.UsersToDepartments.FirstOrDefault(x => x.UserId == userId && x.DepartmentId == departmentId);

            if (buff == null) return;

            _context.UsersToDepartments.Remove(buff);
            _context.SaveChanges();
        }

        public ICollection<AppUser> GetDepartmentUsers(Guid departmentId)
        {
            return _context.UsersToDepartments
                .Include(i => i.User)
                .Where(x => x.DepartmentId == (Guid)departmentId)
                .Select(x => x.User)
                .ToList();
        }
    }
}
