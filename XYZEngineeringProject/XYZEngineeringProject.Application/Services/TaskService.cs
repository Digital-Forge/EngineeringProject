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
                AssignToUserId = null,
                Id = Guid.Empty,
                Deadline = taskRequest.Deadline,
                Description = taskRequest.Description,
                Priority = taskRequest.Priority,
                Title = taskRequest.Title,
                IsComplete = taskRequest.IsComplete,
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
                //ListOfTasks = x.ListOfTasks,
                ListOfTasksId = x.ListOfTasksId,
                Priority = x.Priority,
                Title = x.Title,
                IsComplete=x.IsComplete,
            }).ToList();
        }

        public bool EditTask(TaskVM editTaskRequest)
        {
            var task = _taskRepository.GetTaskById(editTaskRequest.Id);
            task.Title = editTaskRequest.Title;
            task.Description = editTaskRequest.Description;
            task.Priority = editTaskRequest.Priority;
            task.Deadline = editTaskRequest.Deadline;
            task.IsComplete = editTaskRequest.IsComplete;

            return _taskRepository.Update(task);
        }

        
        public bool AddListOfTasks(ListOfTasksVM listOfTasksRequest)
        {
            ListOfTasks list = new ListOfTasks()
            {
                Name = listOfTasksRequest.Name,
                Status = listOfTasksRequest.Status,
                Id = Guid.Empty
            };

            _taskRepository.Add(list);

            return true;
        }

        public List<ListOfTasksVM> GetAllListOfTasks()
        {
            return _taskRepository.GetAllListsOfTasks().Select(x => new ListOfTasksVM
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status,
            }).ToList();
        }

        public bool EditListOfTasks(ListOfTasksVM editListOfTasksRequest)
        {
            var list = _taskRepository.GetListOfTasksById(editListOfTasksRequest.Id);
            list.Name = editListOfTasksRequest.Name;
            list.Status = editListOfTasksRequest.Status;

            return _taskRepository.Update(list);
        }

        public List<TaskVM> GetTasksByList(Guid id)
        {
            return _taskRepository.GetListOfTasksByIdAsQuerable(id)
                .SelectMany(x => x.Task)
                .Select(e => new TaskVM 
                { 
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Priority = e.Priority,
                    CreateDate = e.CreateDate,
                    Deadline = e.Deadline,
                    AssigneeUserId = e.AssignToUserId,
                    AssignerUserId = e.AssignFromUserId,
                    ListOfTasksId = e.ListOfTasksId,
                }).ToList();
        }
    }
}
