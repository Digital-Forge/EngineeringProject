using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Domain.Interfaces
{
    public interface IClientRepository
    {
        // Clients

        Guid AddClient(Client client);
        bool RemoveClient(Client client);
        bool RemoveClient(Guid clientById);
        bool __RemoveHardClient(Client client);
        bool __RemoveHardClient(Guid clientById);
        bool UpdateClient(Client client);
        Client? GetClientById(Guid clientId);
        IQueryable<Client> GetClientByIdAsQuerable(Guid clientId);
        IQueryable<Client>? GetAllClients();
        IQueryable<Client> _GetEveryOneClients();
        IQueryable<Client>? GetClientByCompany();
        IQueryable<Client>? GetClientByCompany(Guid companyId);


        // Client Contacts

        Guid AddClientContact(ClientContact contact);
        bool RemoveClientContact(ClientContact contact);
        bool RemoveClientContact(Guid contactById);
        bool __RemoveHardClientContact(ClientContact contact);
        bool __RemoveHardClientContact(Guid contactById);
        bool UpdateClientContact(ClientContact contact);
        ClientContact? GetClientContactById(Guid contactId);
        IQueryable<ClientContact> GetClientContactByIdAsQuerable(Guid contactId);
        IQueryable<ClientContact>? GetAllClientContacts(Guid clientId);
        IQueryable<ClientContact> _GetEveryOneClientContacts(Guid clientId);


        // relation
        bool AddClientToUser(Guid clientId, Guid userId);
        bool AddClientToUser(Client client, AppUser user);
        bool RemoveClientFromUser(Guid clientId, Guid userId);
        bool RemoveClientFromUser(Client client, AppUser user);
    }
}
