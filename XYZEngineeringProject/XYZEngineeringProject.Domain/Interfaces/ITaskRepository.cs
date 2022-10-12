using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Guid Add(UserTask task);
        bool Remove(UserTask task);
        bool Remove(Guid taskById);
        bool __RemoveHard(UserTask task);
        bool __RemoveHard(Guid taskById);
        bool Update(UserTask task);
        UserTask? GetTaskById(Guid taskId);
        IQueryable<UserTask> GetTaskByIdAsQuerable(Guid taskId);
        IQueryable<UserTask>? GetAll();
        IQueryable<UserTask> _GetEveryOne();
        IQueryable<UserTask>? GetTaskByCompany();
        IQueryable<UserTask>? GetTaskByCompany(Guid companyId);
        Guid Add(ListOfTasks listOfTasks);
        IQueryable<ListOfTasks>? GetAllListsOfTasks();
        IQueryable<ListOfTasks>? GetListsOfTasksByCompany();
        IQueryable<ListOfTasks>? GetListsOfTasksByCompany(Guid companyId);
        ListOfTasks? GetListOfTasksById(Guid listOfTasksId);
        IQueryable<ListOfTasks> GetListOfTasksByIdAsQuerable(Guid listOfTasksId);
        bool Remove(ListOfTasks listOfTasks);
        bool RemoveListOfTasksById(Guid listOfTasksId);
        bool Update(ListOfTasks listOfTasks);
        IQueryable<ListOfTasks>? GetEveryListOfTasks();
        bool __RemoveHard(ListOfTasks listOfTasks);
        bool __RemoveHardListOfTasksById(Guid listOfTasksId);
    }
}
