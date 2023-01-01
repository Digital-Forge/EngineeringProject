using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Web.Controllers;

[Authorize]
public class DepartmentController : Controller
{
    private readonly ILogger<DepartmentController> _logger;
    private readonly IDepartmentService _departmentService;
    private readonly IUserRepository _userRepository;
    public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IUserRepository userRepository)
    {
        _departmentService = departmentService;
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult GetAllDepartments()
    {
        return Ok(_departmentService.GetAllDepartments().ToList());
    }

    [HttpGet]
    public IActionResult GetDepartmentById(string id)
    {
        return Ok(_departmentService.GetAllDepartments().FirstOrDefault(x => x.Id == id));
    }

    [HttpGet]
    public IActionResult GetDepartmentUsers(string id)
    {
        return Ok(_departmentService.GetDepartmentUsers(Guid.Parse(id)));
    }

    [HttpPost]
    public IActionResult AddDepartment([FromBody] DepartmentVM department)
    {
        return Ok(_departmentService.AddDepartment(department));
    }

    [HttpPut]
    public IActionResult EditDepartment([FromBody] DepartmentVM department)
    {
        return Ok(_departmentService.EditDepartment(department));
    }

    [HttpPut]
    public IActionResult DeleteDepartment([FromBody] DepartmentVM department)
    {
        return Ok(_departmentService.DeleteDepartment(department));
    }

    [HttpGet]
    public IActionResult GetAllCompanyDepartmentsByUser(Guid userId)
    {
        var user = _userRepository.GetUserById(userId);

        if (user == null) return BadRequest();

        return Ok(_departmentService.GetAllCompanyDepartmentsByCompany(user.CompanyId.Value));
    }
    
    [HttpGet]
    public IActionResult GetAllCompanyDepartmentsByCompany(Guid companyId)
    {
        return Ok(_departmentService.GetAllCompanyDepartmentsByCompany(companyId));
    }
}
