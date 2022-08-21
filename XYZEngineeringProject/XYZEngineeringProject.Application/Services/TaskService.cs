using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Domain.Interfaces;

namespace XYZEngineeringProject.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public List<TaskVM> GetAllTasks()
        {
            return _taskRepository.GetAll().Select(x => new TaskVM
            {
                Id = x.Id,
                AssigneeUserId = x.AssigneeUserId,
                Deadline = x.Deadline,
                Description = x.Description,
                ListOfTasks = x.ListOfTasks,
                ListOfTasksId = x.ListOfTasksId,
                Priority = x.Priority,
                Title = x.Title
            }).ToList();
        }
    }
}
