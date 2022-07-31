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
        Guid Add(Client client);
        bool Remove(Client client);
        bool Remove(Guid clientById);
        bool __RemoveHard(Client client);
        bool __RemoveHard(Guid clientById);
        bool Update(Client client);
        Client? GetClientById(Guid clientId);
        IQueryable<Client> GetClientByIdAsQuerable(Guid clientId);
        IQueryable<Client> GetAll();
        IQueryable<Client>? GetClientByCompany();
        IQueryable<Client>? GetClientByCompany(Guid companyId);
    }
}
