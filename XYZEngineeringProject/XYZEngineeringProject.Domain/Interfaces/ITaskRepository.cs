using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        IQueryable<UserTask> GetAll();
        IQueryable<UserTask>? GetTaskByCompany();
        IQueryable<UserTask>? GetTaskByCompany(Guid companyId);
    }
}
