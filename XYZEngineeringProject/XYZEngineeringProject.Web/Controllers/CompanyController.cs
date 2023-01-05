using Microsoft.AspNetCore.Mvc;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;

namespace XYZEngineeringProject.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public IActionResult GetAllCompany()
        {
            return Ok(_companyService.GetCompanyList());
        }

        [HttpGet]
        [Route("Company/GetCompanyByUserId/{userId}")]
        public IActionResult GetCompanyByUser(string userId)
        {
            var model = _companyService.GetCompanyByUser(Guid.Parse(userId));
            return model != null ? Ok(model) : NotFound();
        }

        [HttpPut]
        public IActionResult Create([FromBody] CompanyVM model)
        {
            var result = _companyService.CreateCompany(model);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPut]
        public IActionResult Update(CompanyVM model)
        {
            return _companyService.UpdateCompany(model) ? Ok() : BadRequest();
        }

        [HttpDelete]
        [Route("Company/Delete/{id}")]
        public IActionResult Delete(string id)
        {
            return _companyService.DeleteCompany(Guid.Parse(id)) ? Ok() : BadRequest();
        }
    }
}
