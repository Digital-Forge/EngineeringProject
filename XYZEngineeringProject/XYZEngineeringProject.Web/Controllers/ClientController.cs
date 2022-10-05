using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;

namespace XYZEngineeringProject.Web.Controllers
{
    [Authorize]
    public class ClientController: Controller
    {
        private ILogger<ClientController> _logger;
        private IClientService _clientService;
        public ClientController(ILogger<ClientController> logger, IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult GetAllClients()
        {
            return Ok(_clientService.GetAllClients());
        }

        [HttpPost]
        public IActionResult AddClient([FromBody] ClientVM clientRequest)
        {
            return Ok(_clientService.AddClient(clientRequest));
        }

        [HttpGet]
        public IActionResult EditClient(string id)
        {
            return Ok(_clientService.GetAllClients().FirstOrDefault(x => x.Id == Guid.Parse(id)));
        }

        [HttpPut]
        public IActionResult EditClient([FromBody] ClientVM editClientRequest)
        {
            return Ok(_clientService.EditClient(editClientRequest));
        }
    }
}
