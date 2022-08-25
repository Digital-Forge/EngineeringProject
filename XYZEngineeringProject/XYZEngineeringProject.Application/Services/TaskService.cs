using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public bool AddTask(TaskVM taskRequest)
        {
            UserTask task = new UserTask
            {
                AssigneeUserId = null,
                Id = Guid.Empty,
                Deadline = taskRequest.Deadline,
                Description = taskRequest.Description,
                Priority = taskRequest.Priority,
                Title = taskRequest.Title
            };

            _taskRepository.Add(task);

            return true;
        }

        public List<TaskVM> GetAllTasks()
        {
            return _taskRepository.GetAll().ToList().Select(x => new TaskVM
            {
                Id = x.Id,
                AssigneeUserId = Guid.Empty,
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
