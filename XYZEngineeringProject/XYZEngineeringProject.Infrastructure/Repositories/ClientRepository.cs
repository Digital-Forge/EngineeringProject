using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly Context _context;
        private readonly IActionContextAccessor _actionContextAccessor;

        public ClientRepository(Context context, IActionContextAccessor actionContextAccessor)
        {
            _context = context;
            _actionContextAccessor = actionContextAccessor;
        }

        public Guid Add(Client client)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Client> GetAll()
        {
            throw new NotImplementedException();
        }

        public Client GetClientById(Guid clientId)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Client client)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Guid clientById)
        {
            throw new NotImplementedException();
        }

        public bool Update(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
