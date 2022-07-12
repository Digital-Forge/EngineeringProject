using Microsoft.AspNetCore.Mvc;

namespace XYZEngineeringProject.Web.Controllers
{
    public class TaskController: Controller
    {
        private readonly ILogger<TaskController> _logger;

        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
