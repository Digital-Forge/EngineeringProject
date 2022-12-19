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
                ListOfTasksId= taskRequest.ListOfTasksId,    
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
                IsComplete = x.IsComplete,
                CreateDate = x.CreateDate,
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
                Project = listOfTasksRequest.Project,
                Id = Guid.Empty
            };

            _taskRepository.Add(list);

            foreach (TaskVM task in listOfTasksRequest.Tasks)
            {
                UserTask userTask = new UserTask()
                {
                    Id = Guid.Empty,
                    Title = task.Title,
                    Description = task.Description,
                    Deadline = task.Deadline,
                    Priority = task.Priority,
                    ListOfTasksId = list.Id
                };
                _taskRepository.Add(userTask);
            }

            return true;
        }

        public List<ListOfTasksVM> GetAllListOfTasks()
        {
            List<ListOfTasksVM> lists =  _taskRepository.GetAllListsOfTasks().Select(x => new ListOfTasksVM
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status,
                Project=x.Project,
                CreateDate = x.CreateDate,
            }).ToList();

            foreach (ListOfTasksVM list in lists)
            {
                list.Tasks = GetTasksByList(list.Id);
            }

            return lists;
        }

        public bool EditListOfTasks(ListOfTasksVM editListOfTasksRequest)
        {
            var list = _taskRepository.GetListOfTasksById(editListOfTasksRequest.Id);
            list.Name = editListOfTasksRequest.Name;
            list.Status = editListOfTasksRequest.Status;
            list.Project= editListOfTasksRequest.Project;

            foreach (var taskVM in editListOfTasksRequest.Tasks)
            {
                var task = _taskRepository.GetTaskById(taskVM.Id);
                if (task==null)
                {
                    AddTask(taskVM);
                }
                else
                {
                    EditTask(taskVM);
                }
                
            }

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
