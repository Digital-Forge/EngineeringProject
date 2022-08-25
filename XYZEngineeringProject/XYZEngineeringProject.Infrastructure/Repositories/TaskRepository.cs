using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly Context _context;
        private readonly InfrastructureUtils _infrastructureUtils;
        private readonly Logger _logger;

        public TaskRepository(Context context, IHttpContextAccessor httpContextAccessor, Logger logger)
        {
            _context = context;
            _infrastructureUtils = new InfrastructureUtils(context, httpContextAccessor);
            _logger = logger;
        }

        public Guid Add(UserTask task)
        {
            task.AssignerUserId = _infrastructureUtils.GetUserIdFormHttpContext().Value;
            _context.Tasks.Add(task);
            _context.SaveChanges();

            _logger.Log(Logger.Source.Repository, Logger.InfoType.Info, $"Add task - {task.Id}");
            return task.Id;
        }

        public IQueryable<UserTask>? GetAll()
        {
            var currentUser = _infrastructureUtils.GetUserFormHttpContext();

            if (currentUser?.CompanyId == null || currentUser?.CompanyId == Guid.Empty)
                return null;
            else
                return _context.Tasks
                    .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                    .Where(x => x.CompanyId == currentUser.CompanyId);
        }

        public IQueryable<UserTask>? GetTaskByCompany()
        {
            var companyId = _infrastructureUtils.GetCompany()?.Id;

            if (companyId == null) return null;

            return _context.Tasks
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Where(x => x.CompanyId == companyId);
        }

        public IQueryable<UserTask>? GetTaskByCompany(Guid companyId)
        {
            return _context.Tasks
                 .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                 .Where(x => x.CompanyId == companyId);
        }

        public UserTask? GetTaskById(Guid taskId)
        {
            return _context.Tasks
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .FirstOrDefault(x => x.Id == taskId);
        }

        public IQueryable<UserTask> GetTaskByIdAsQuerable(Guid taskId)
        {
            return _context.Tasks
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Where(x => x.Id == taskId);
        }

        public bool Remove(UserTask task)
        {
            if (task == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove null task");
                return false;
            }

            try
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove task - {task.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed remove task - {task.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool Remove(Guid taskById)
        {
            if (taskById == Guid.Empty)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove task by empty guid");
                return false;
            }

            try
            {
                var buff = GetTaskById(taskById);
                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove task, who don't exist or deleted");
                    return false;
                }

                _context.Tasks.Remove(buff);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove task - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed remove task - {taskById} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool Update(UserTask task)
        {
            if (task == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying update null task");
                return false;
            }

            try
            {
                _context.Tasks.Update(task);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Update task - {task.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed update task - {task.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public IQueryable<UserTask> _GetEveryOne()
        {
            return _context.Tasks;
        }

        public bool __RemoveHard(UserTask task)
        {
            try
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove task - {task.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed hard remove task - {task.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool __RemoveHard(Guid taskById)
        {
            try
            {
                var buff = _context.Tasks.FirstOrDefault(x => x.Id == taskById);
                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying hard remove task, who don't exist");
                    return false;
                }

                _context.Tasks.Remove(buff);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove task - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed hard remove task - {taskById} - [{e.Message}]");
                return false;
            }
            return true;
        }
    }
}
