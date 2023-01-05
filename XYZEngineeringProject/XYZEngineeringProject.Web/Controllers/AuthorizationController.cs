using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XYZEngineeringProject.Application.ViewModels.Authorization;

namespace XYZEngineeringProject.Web.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly Application.Interfaces.IAuthorizationService _authorizationService;

        public AuthorizationController(Application.Interfaces.IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginVM input)
        {
            IActionResult result = Unauthorized();

            if (_authorizationService.AuthenticateUser(input))
            {
                result = Ok(new { token = _authorizationService.GenerateJsonWebToken(input) });
            }

            return result;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Logout()
        {
            _authorizationService.Logout();
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllRoles()
        {
            return Ok(_authorizationService.GetAllRoles());
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChangePassword(Guid userId, string newPassword)
        {
            return Ok(_authorizationService.ChangePassword(userId, newPassword));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult CheckNick(string name)
        {
            return Ok(_authorizationService.CheckNick(name));
        }
    }
}
