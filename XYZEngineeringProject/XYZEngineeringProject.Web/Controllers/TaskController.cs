﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Web.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskService _taskService;

        public TaskController(ILogger<TaskController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            return Ok(_taskService.GetAllTasks().ToList());
        }

        [HttpPost]
        public IActionResult AddTask([FromBody] TaskVM taskRequest)
        {
            return Ok(_taskService.AddTask(taskRequest));
        }

        [HttpGet]
        public IActionResult EditTask(string id)
        {
            return Ok(_taskService.GetAllTasks().FirstOrDefault(x => x.Id == Guid.Parse(id)));
        }

        [HttpPut]
        public IActionResult EditTask([FromBody] TaskVM editTaskRequest)
        {
            return Ok(_taskService.EditTask(editTaskRequest));
        }

        [HttpGet]
        [Route("Task/GeUsersListsOfTasks/{userId}")]
        public IActionResult GeUsersListsOfTasks(string userId)
        {
            return Ok(_taskService.GetAllListOfTasks().Where(x  => x.CreateBy == Guid.Parse(userId)).ToList());
        }

        [HttpPost]
        public IActionResult AddListOfTasks([FromBody] ListOfTasksVM listOfTasksRequest)
        {
            return Ok(_taskService.AddListOfTasks(listOfTasksRequest));
        }

        [HttpGet]
        public IActionResult GetListOfTasksById(string id)
        {
            return Ok(_taskService.GetAllListOfTasks().FirstOrDefault(x => x.Id == Guid.Parse(id)));
        }

        [HttpPut]
        public IActionResult EditListOfTasks([FromBody] ListOfTasksVM listOfTasksRequest)
        {
            return Ok(_taskService.EditListOfTasks(listOfTasksRequest));
        }

        [HttpGet]
        public IActionResult GetTaskByList(string id)
        {
            var list = _taskService.GetTasksByList(new Guid(id));

            return list != null ? Ok(list) : BadRequest();
        }

        [HttpPut]
        public IActionResult DeleteTaskById([FromBody] TaskVM task)
        {
            return Ok(_taskService.DeleteTaskById(task.Id));
        }

        [HttpPut]
        public IActionResult DeleteTaskListById([FromBody] ListOfTasksVM list)
        {
            return Ok(_taskService.DeleteTaskListById(list.Id));
        }
    }
}
