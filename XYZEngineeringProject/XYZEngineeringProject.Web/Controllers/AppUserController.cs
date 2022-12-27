using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Domain.Interfaces;

namespace XYZEngineeringProject.Web.Controllers
{
    [Authorize]
    public class AppUserController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly IAppUserService _appUserService;
        private readonly IUserRepository _userRepository;

        public AppUserController(ILogger<TaskController> logger, IAppUserService appUserService, IUserRepository userRepository)
        {
            _logger = logger;
            _appUserService = appUserService;
            _userRepository = userRepository;
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

        [HttpGet]
        public IActionResult GetUser(string id)
        {
            return Ok(_appUserService.GetAllUsers().ToList().FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        public IActionResult GetUserRoles([FromBody] AppUserVM user)
        {
            return Ok(_userRepository.GetUserRoles(Guid.Parse(user.Id)));
        }

        [HttpGet]
        [Route("AppUser/AddUserRole/{id}/{roleName}")]
        public IActionResult AddUserRole(string id, string roleName)
        {
            return Ok(_appUserService.AddUserRole(Guid.Parse(id), roleName));
        }
        [HttpGet]
        [Route("AppUser/DeleteUserRole/{id}/{roleName}")]
        public IActionResult DeleteUserRole(string id,string roleName)
        {
            return Ok(_appUserService.RemoveUserRole(Guid.Parse(id),roleName));
        }
    }
}
