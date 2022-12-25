using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Infrastructure.Utils;
using XYZEngineeringProject.Web.Models;

namespace XYZEngineeringProject.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly Infrastructure.Utils.InfrastructureUtils _utilsRepository;

        public HomeController(ILogger<HomeController> logger, IDepartmentRepository departmentRepository, Context context, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
            _utilsRepository = new Infrastructure.Utils.InfrastructureUtils(context, contextAccessor);
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Test1()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        [Authorize]
        public IActionResult RoleCheck()
        {
            var department = _departmentRepository.Add(new Domain.Models.Department
            {
                Name = "department"         
            });

            var user = _utilsRepository.GetUserIdFormHttpContext();


            _departmentRepository.AddUserToDepartment(user.Value, department);

            return Ok();
        }
    }
}