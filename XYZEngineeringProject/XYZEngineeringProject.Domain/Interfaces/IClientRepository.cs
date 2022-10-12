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

        Guid AddClientContract(ClientContact contact);
        bool RemoveClientContract(ClientContact contact);
        bool RemoveClientContract(Guid contactById);
        bool __RemoveHardClientContract(ClientContact contact);
        bool __RemoveHardClientContract(Guid contactById);
        bool UpdateClientContract(ClientContact contact);
        ClientContact? GetClientContractById(Guid contactId);
        IQueryable<ClientContact> GetClientContractByIdAsQuerable(Guid contactId);
        IQueryable<ClientContact>? GetAllClientContracts(Guid clientId);
        IQueryable<ClientContact> _GetEveryOneClientContracts(Guid clientId);


        // relation
        bool AddClientToUser(Guid clientId, Guid userId);
        bool AddClientToUser(Client client, AppUser user);
        bool RemoveClientFromUser(Guid clientId, Guid userId);
        bool RemoveClientFromUser(Client client, AppUser user);
    }
}
