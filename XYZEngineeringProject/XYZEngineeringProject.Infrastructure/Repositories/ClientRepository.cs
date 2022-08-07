using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly Context _context;
        private readonly InfrastructureUtils _infrastructureUtils;
        private readonly Logger _logger;

        public ClientRepository(Context context, IHttpContextAccessor httpContextAccessor, Logger logger)
        {
            _context = context;
            _infrastructureUtils = new InfrastructureUtils(context, httpContextAccessor);
            _logger = logger;
        }

        public Guid Add(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();

            _logger.Log(Logger.Source.Repository, Logger.InfoType.Info, $"Add client - {client.Id}");
            return client.Id;
        }

        public IQueryable<Client>? GetAll()
        {
            var currentUser = _infrastructureUtils.GetUserFormHttpContext();

            if (currentUser?.Company == null)
                return null;
            else 
                return _context.Clients
                    .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                    .Where(x => x.CompanyId == currentUser.CompanyId);
        }

        public Client? GetClientById(Guid clientId)
        {
            return _context.Clients
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .FirstOrDefault(x => x.Id == clientId);
        }

        public IQueryable<Client> GetClientByIdAsQuerable(Guid clientId)
        {
            return _context.Clients
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Where(x => x.Id == clientId);
        }

        public IQueryable<Client>? GetClientByCompany()
        {
            var companyId = _infrastructureUtils.GetCompany()?.Id;

            if (companyId == null) return null;

            return _context.Clients
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Where(x => x.CompanyId == companyId);
        }
        
        public IQueryable<Client>? GetClientByCompany(Guid companyId)
        {
            return _context.Clients
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Where(x => x.CompanyId == companyId);
        }

        public bool Remove(Client client)
        {
            if(client == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove null client");
                return false;
            }

            try
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove clients - {client.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed remove client - {client.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool Remove(Guid clientById)
        {
            if (clientById == Guid.Empty)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove client by empty guid");
                return false;
            }

            try
            {
                var buff = GetClientById(clientById);
                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove client, who don't exist or deleted");
                    return false;
                }

                _context.Clients.Remove(buff);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove clients - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed remove client - {clientById} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool Update(Client client)
        {
            if (client == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying update null client");
                return false;
            }

            try
            {
                _context.Clients.Update(client);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Update clients - {client.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed update client - {client.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool __RemoveHard(Client client)
        {
            try
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove clients - {client.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed hard remove client - {client.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool __RemoveHard(Guid clientById)
        {
            try
            {
                var buff = _context.Clients.FirstOrDefault(x => x.Id == clientById);
                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying hard remove client, who don't exist");
                    return false;
                }

                _context.Clients.Remove(buff);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove clients - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed hard remove client - {clientById} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public IQueryable<Client> _GetEveryOne()
        {
            return _context.Clients;
        }
    }
}
