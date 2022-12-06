using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using System.Runtime.InteropServices;
using XYZEngineeringProject.Application.Interfaces;

namespace XYZEngineeringProject.Web.Controllers
{
    [Authorize]
    public class MeController : Controller
    {
        private readonly IMeService _meService;

        public MeController(IMeService meService)
        {
            _meService = meService;
        }

        [HttpGet]
        public IActionResult GetMyId()
        {
            var buff = _meService.MeId();

            return buff == null ? BadRequest() : Ok(buff);
        }

        [HttpGet]
        public bool IsAuthorized()
        {
            var buff = _meService.MeId();

            bool result = (buff != null);

            return result;
        }

        [HttpGet]
        public IActionResult GetMyData()
        {
            var buff = _meService.GetMyData();

            return buff == null ? BadRequest() : Ok(buff);
        }
    }
}
