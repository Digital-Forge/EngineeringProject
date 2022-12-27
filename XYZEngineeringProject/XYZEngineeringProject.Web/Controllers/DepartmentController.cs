using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XYZEngineeringProject.Application.Interfaces;

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
}
