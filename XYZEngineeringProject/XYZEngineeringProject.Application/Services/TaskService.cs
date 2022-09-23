using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
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

        public bool EditTask(TaskVM editTaskRequest)
        {
            var task = _taskRepository.GetTaskById(editTaskRequest.Id);
            task.Title = editTaskRequest.Title;
            task.Description = editTaskRequest.Description;
            task.Priority = editTaskRequest.Priority;
            task.Deadline = editTaskRequest.Deadline;

            return _taskRepository.Update(task);
        }
    }
}
