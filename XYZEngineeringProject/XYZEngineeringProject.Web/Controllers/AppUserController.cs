using Microsoft.AspNetCore.Mvc;

namespace XYZEngineeringProject.Web.Controllers
{
    public class AppUserController : Controller
    {
        private readonly ILogger<TaskController> _logger;

        public AppUserController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateNewUser()
        {
            return View();
        }
    }
}
