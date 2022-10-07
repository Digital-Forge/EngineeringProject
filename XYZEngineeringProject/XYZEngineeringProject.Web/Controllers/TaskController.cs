using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Web.Controllers
{
    [Authorize]
    public class TaskController: Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskService _taskService;

        public TaskController(ILogger<TaskController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            return View();
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
        public IActionResult GetAllListOfTasks()
        {
            return Ok(_taskService.GetAllListOfTasks().ToList());
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
    }
}
