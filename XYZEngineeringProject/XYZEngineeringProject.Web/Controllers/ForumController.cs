using Microsoft.AspNetCore.Mvc;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels.Forum;
using XYZEngineeringProject.Domain.Interfaces;

namespace XYZEngineeringProject.Web.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;
        private readonly IUserRepository _userRepository;

        public ForumController(IForumService forumService, IUserRepository userRepository)
        {
            _forumService = forumService;
            _userRepository = userRepository;
        }

        [HttpGet]
        public ForumVM GetForum(Guid id, int? take, int? skip)
        {
            return _forumService.GetForum(id, take ?? 20, skip ?? 0);
        }

        [HttpGet]
        public List<ForumMessageVM> GetForumContent(Guid id, int? take, int? skip)
        {
            return _forumService.GetForumContent(id, take ?? 20, skip ?? 0);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostForumMessageVM post)
        {
            _forumService.PostMessage(post);
            return Ok();
        }

        [HttpGet]
        public IActionResult AddForum(string name)
        {
            var result = _forumService.AddForum(name);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpDelete]
        public IActionResult DeleteForum(Guid id)
        {
            _forumService.DeleteForum(id);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteForumMessage(Guid id)
        {
            _forumService.DeleteForumMessage(id);
            return Ok();
        }

        [HttpGet("Forum/GetUserForums/{userId}")]
        public IActionResult GetUserForums([FromRoute] Guid userId)
        {
            return Ok(_forumService.GetUserForums(userId));
        }

        [HttpGet("Forum/GetAllCompanyForumsByUser/{userId}")]
        public IActionResult GetAllCompanyForumsByUser(Guid userId)
        {
            var user = _userRepository.GetUserById(userId);

            if (user == null) return BadRequest();

            return Ok(_forumService.GetAllCompanyForumsByCompany(user.CompanyId.Value));
        }

        [HttpGet]
        public IActionResult GetAllCompanyForumsByCompany(Guid companyId)
        {
            return Ok(_forumService.GetAllCompanyForumsByCompany(companyId));
        }
    }
}
