using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;

namespace XYZEngineeringProject.Web.Controllers;

[Authorize]
public class DepartmentController : Controller
{
    private readonly ILogger<DepartmentController> _logger;
    private readonly IDepartmentService _departmentService;
    public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
    {
        _departmentService = departmentService;
        _logger = logger;
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
}
