using Microsoft.AspNetCore.Mvc;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels.Forum;

namespace XYZEngineeringProject.Web.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;

        public ForumController(IForumService forumService)
        {
            _forumService = forumService;
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

        [HttpGet]
        public IActionResult GetUserForums(Guid userId)
        {
            return Ok(_forumService.GetUserForums(userId));
        }
    }
}
