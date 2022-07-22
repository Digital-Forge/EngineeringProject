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
        public Guid Add(Client client);
        public bool Remove(Client client);
        public bool Remove(Guid clientById);
        public bool Update(Client client);
        public Client GetClientById(Guid clientId);
        public IQueryable<Client> GetAll();
    }
}
