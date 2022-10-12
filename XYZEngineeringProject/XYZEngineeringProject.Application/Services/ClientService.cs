using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        public bool AddClient(ClientVM clientVM)
        {
            Client client = new Client()
            {
                Name = clientVM.Name,
                //Surname = clientVM.Surname
            };

            _clientRepository.AddClient(client);
            return true;
        }

        public bool EditClient(ClientVM clientVM)
        {
            var client = _clientRepository.GetClientById(clientVM.Id);
            if (client != null)
            {
                client.Name = clientVM.Name;
                //client.Surname = clientVM.Surname;
                return _clientRepository.UpdateClient(client);
            }
            return false;
        }

        public List<ClientVM> GetAllClients()
        {
            return _clientRepository.GetAllClients().Select(x => new ClientVM
            {
                Id = x.Id,
                Name = x.Name,
                //Surname = x.Surname
            }).ToList();
        }
    }
}
