using Microsoft.AspNetCore.Mvc;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;

namespace XYZEngineeringProject.Web.Controllers
{
    public class AppUserController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly IAppUserService _appUserService;

        public AppUserController(ILogger<TaskController> logger, IAppUserService appUserService)
        {
            _logger = logger;
            _appUserService = appUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateNewUser()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_appUserService.GetAllUsers().ToList());
        }

        [HttpPost]
        public IActionResult AddNewUser([FromBody] AppUserVM appUserRequest)
        {
            return Ok(_appUserService.AddNewUser(appUserRequest));
        }
    }
}
