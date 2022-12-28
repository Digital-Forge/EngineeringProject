using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        #region UserTask
        public Guid Add(UserTask task)
        {
            task.AssignFromUserId = _infrastructureUtils.GetUserIdFormHttpContext().Value;
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
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying to remove task by an empty guid");
                return false;
            }

            try
            {
                var buff = GetTaskById(taskById);
                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying to remove task, that doesn't exist or is deleted");
                    return false;
                }

                _context.Tasks.Remove(buff);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove task - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed to remove task - {taskById} - [{e.Message}]");
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
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed to update task - {task.Id} - [{e.Message}]");
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
        #endregion

        #region ListOfTasks
        public Guid Add(ListOfTasks listOfTasks)
        {
            listOfTasks.UserId = _infrastructureUtils.GetUserIdFormHttpContext().Value;
            _context.ListTasks.Add(listOfTasks);
            _context.SaveChanges();

            _logger.Log(Logger.Source.Repository, Logger.InfoType.Info, $"Add ListOfTasks - {listOfTasks.Id}");
            return listOfTasks.Id;
        }

        public IQueryable<ListOfTasks>? GetAllListsOfTasks()
        {
            var currentUser = _infrastructureUtils.GetUserFormHttpContext();

            if (currentUser?.CompanyId == null || currentUser?.CompanyId == Guid.Empty)
                return null;
            else
                return _context.ListTasks
                    .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                    .Where(x => x.CompanyId == currentUser.CompanyId);
        }

        public IQueryable<ListOfTasks>? GetListsOfTasksByCompany()
        {
            var companyId = _infrastructureUtils.GetCompany()?.Id;

            if (companyId == null) return null;

            return _context.ListTasks
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Where(x => x.CompanyId == companyId);
        }

        public IQueryable<ListOfTasks>? GetListsOfTasksByCompany(Guid companyId)
        {
            return _context.ListTasks
                 .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                 .Where(x => x.CompanyId == companyId);
        }

        public ListOfTasks? GetListOfTasksById(Guid listOfTasksId)
        {
            return _context.ListTasks
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .FirstOrDefault(x => x.Id == listOfTasksId);
        }

        public IQueryable<ListOfTasks> GetListOfTasksByIdAsQuerable(Guid listOfTasksId)
        {
            return _context.ListTasks
               .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
               .Where(x => x.Id == listOfTasksId);
        }
        
        public bool Remove(ListOfTasks listOfTasks)
        {
            if (listOfTasks == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove null list of tasks");
                return false;
            }

            try
            {
                _context.ListTasks.Remove(listOfTasks);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove list of tasks - {listOfTasks.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed remove list of tasks - {listOfTasks.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool RemoveListOfTasksById(Guid listOfTasksId)
        {
            if (listOfTasksId == Guid.Empty)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying to remove list of tasks by an empty guid");
                return false;
            }

            try
            {
                //var buff = GetListOfTasksById(listOfTasksId);
                var buff = _context.ListTasks
                    .Include(i => i.Task.Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete))
                    .FirstOrDefault(x =>
                        x.Id == listOfTasksId
                        && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete);

                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying to remove list of tasks, that doesn't exist or is deleted");
                    return false;
                }

                if (buff.Task != null)
                    foreach (var item in buff.Task)
                    {
                        _context.Tasks.Remove(item);
                    }

                _context.ListTasks.Remove(buff);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove list of tasks - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed to remove list of tasks - {listOfTasksId} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool Update(ListOfTasks listOfTasks)
        {
            if (listOfTasks == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying to update a null list of tasks");
                return false;
            }

            try
            {
                _context.ListTasks.Update(listOfTasks);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Update list of tasks - {listOfTasks.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed to update list of tasks - {listOfTasks.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public IQueryable<ListOfTasks>? GetEveryListOfTasks()
        {
            return _context.ListTasks;
        }

        public bool __RemoveHard(ListOfTasks listOfTasks)
        {
            try
            {
                _context.ListTasks.Remove(listOfTasks);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove list of tasks - {listOfTasks.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed to hard remove list of tasks - {listOfTasks.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool __RemoveHardListOfTasksById(Guid listOfTasksId)
        {
            try
            {
                var buff = _context.ListTasks.FirstOrDefault(x => x.Id == listOfTasksId);
                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying hard remove list of tasks, that doesn't exist");
                    return false;
                }

                _context.ListTasks.Remove(buff);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove list of task - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed to hard remove list of tasks - {listOfTasksId} - [{e.Message}]");
                return false;
            }
            return true;
        }
        #endregion

    }
}
