using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
                Description = clientVM.Description,
                Address = clientVM.Address,
                Comments = clientVM.Comments,
                NIP = clientVM.NIP,
                ClientContacts = clientVM.Contacts.Select(x => new ClientContact
                {
                    Firstname = x.Firstname,
                    Surname = x.Surname,
                    Email = x.Email,
                    Phone = x.Phone
                }).ToList()
            };

            _clientRepository.AddClient(client);
            return true;
        }

        public bool EditClient(ClientVM clientVM)
        {
            var client = _clientRepository.GetClientByIdAsQuerable(clientVM.Id).Include(x => x.ClientContacts).FirstOrDefault();
            if (client != null)
            {
                client.Name = clientVM.Name;
                client.Description = clientVM.Description;
                client.Address = clientVM.Address;
                client.Comments = clientVM.Comments;
                client.NIP = clientVM.NIP;
                clientVM.Contacts.Where(x => x.Id == Guid.Empty).ToList().ForEach(x => client.ClientContacts.Add(new ClientContact
                {
                    Firstname = x.Firstname,
                    Surname = x.Surname,
                    Phone = x.Phone,
                    Id = x.Id,
                    Email = x.Email,
                    ClientId = clientVM.Id
                }));
                clientVM.Contacts.Where(x => x.Id != Guid.Empty).ToList().ForEach(x =>
                {
                    var contact = client.ClientContacts.FirstOrDefault(y => y.Id == x.Id);
                    contact.Phone = x.Phone;
                    contact.Firstname = x.Firstname;
                    contact.Surname = x.Surname;
                    contact.Email = x.Email;
                    _clientRepository.UpdateClientContact(contact);
                });
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
                Description= x.Description,
                NIP= x.NIP,
                Address = x.Address,
                Comments= x.Comments,
                Contacts = x.ClientContacts.Where(x=>x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Select(y => new ClientContactVM
                {
                    Firstname = y.Firstname,
                    Id = y.Id,
                    Email= y.Email,
                    Phone= y.Phone,
                    Surname= y.Surname
                }).ToList()
            }).ToList();
        }

        public bool DeleteClient(ClientVM client)
        {
            return _clientRepository.RemoveClient(client.Id);
        }

        public bool DeleteClientContact(ClientContactVM contact)
        {
            return _clientRepository.RemoveClientContact(contact.Id);
        }
    }
}
