using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;

namespace XYZEngineeringProject.Web.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly ILogger<NoteController> _logger;
        private readonly INoteService _noteService;

        public NoteController(ILogger<NoteController> logger, INoteService noteService)
        {
            _logger = logger;
            _noteService = noteService;
        }

        [HttpGet]
        public IActionResult GetAllNotes()
        {
            return Ok(_noteService.GetAllNotes().ToList());
        }

        [HttpPost]
        public IActionResult AddNote([FromBody] NoteVM noteRequest)
        {
            return Ok(_noteService.AddNote(noteRequest));
        }

        [HttpGet]
        public IActionResult EditNote(string id)
        {
            return Ok(_noteService.GetAllNotes().FirstOrDefault(x => x.Id == Guid.Parse(id)));
        }

        [HttpPut]
        public IActionResult EditNote([FromBody] NoteVM editNoteRequest)
        {
            return Ok(_noteService.EditNote(editNoteRequest));
        }
    }
}
