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

        public Guid AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();

            _logger.Log(Logger.Source.Repository, Logger.InfoType.Info, $"Add client - {client.Id}");
            return client.Id;
        }

        public IQueryable<Client>? GetAllClients()
        {
            var currentUser = _infrastructureUtils.GetUserFormHttpContext();

            if (currentUser?.CompanyId == null || currentUser?.CompanyId == Guid.Empty)
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

        public bool RemoveClient(Client client)
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

        public bool RemoveClient(Guid clientById)
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

        public bool UpdateClient(Client client)
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

        public bool __RemoveHardClient(Client client)
        {
            try
            {
                _context.Clients.Remove(client);
                _context._ClearSaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove clients - {client.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed hard remove client - {client.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool __RemoveHardClient(Guid clientById)
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
                _context._ClearSaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove clients - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed hard remove client - {clientById} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public IQueryable<Client> _GetEveryOneClients()
        {
            return _context.Clients;
        }

        public Guid AddClientContact(ClientContact contact)
        {
            _context.ClientContacts.Add(contact);
            _context.SaveChanges();

            _logger.Log(Logger.Source.Repository, Logger.InfoType.Info, $"Add client contact - {contact.Id}");
            return contact.Id;
        }

        public bool RemoveClientContact(ClientContact contact)
        {
            if (contact == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove null client contact");
                return false;
            }

            try
            {
                _context.ClientContacts.Remove(contact);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove client contact - {contact.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed remove client contact - {contact.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool RemoveClientContact(Guid contactById)
        {
            if (contactById == Guid.Empty)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove client by empty guid");
                return false;
            }

            try
            {
                var buff = GetClientContactById(contactById);
                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove client contact, who don't exist or deleted");
                    return false;
                }

                _context.ClientContacts.Remove(buff);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove client contact - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed remove client contact - {contactById} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool __RemoveHardClientContact(ClientContact contact)
        {
            try
            {
                _context.ClientContacts.Remove(contact);
                _context._ClearSaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove client contact - {contact.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed hard remove client contact - {contact.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool __RemoveHardClientContact(Guid contactById)
        {
            try
            {
                var buff = _context.ClientContacts.FirstOrDefault(x => x.Id == contactById);
                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying hard remove client contact, who don't exist");
                    return false;
                }

                _context.ClientContacts.Remove(buff);
                _context._ClearSaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove client contact - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed hard remove client contact - {contactById} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool UpdateClientContact(ClientContact contact)
        {
            if (contact == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying update null client contact");
                return false;
            }

            try
            {
                _context.ClientContacts.Update(contact);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Update client contact - {contact.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed update client contact - {contact.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public ClientContact? GetClientContactById(Guid contactId)
        {
            return _context.ClientContacts
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .FirstOrDefault(x => x.Id == contactId);
        }

        public IQueryable<ClientContact> GetClientContactByIdAsQuerable(Guid contactId)
        {
            return _context.ClientContacts
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Where(x => x.Id == contactId);
        }

        public IQueryable<ClientContact>? GetAllClientContracts()
        {
            var currentUser = _infrastructureUtils.GetUserFormHttpContext();

            if (currentUser?.CompanyId == null || currentUser?.CompanyId == Guid.Empty)
                return null;
            else
                return _context.ClientContacts
                    .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                    .Where(x => x.CompanyId == currentUser.CompanyId);
        }

        public IQueryable<ClientContact>? GetAllClientContacts(Guid clientId)
        {
            if (clientId == null || clientId == Guid.Empty)
            {
                return null;
            }

            var currentUser = _infrastructureUtils.GetUserFormHttpContext();

            if (currentUser?.CompanyId == null || currentUser?.CompanyId == Guid.Empty)
                return null;
            else
                return _context.ClientContacts
                    .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                    .Where(x => x.CompanyId == currentUser.CompanyId)
                    .Where(x => x.ClientId == clientId);
        }

        public IQueryable<ClientContact> _GetEveryOneClientContacts(Guid clientId)
        {
            if (clientId == null || clientId == Guid.Empty)
            {
                return null;
            }
            return _context.ClientContacts
                .Where(x => x.ClientId == clientId);
        }

        public bool AddClientToUser(Guid clientId, Guid userId)
        {
            try
            {
                _context.UsersToClients.Add(new UsersToClients { ClientId = clientId, UserId = userId });
                _context.SaveChanges();
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Info, $"Add client ({clientId}) to user ({userId})");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Exception in client repository in AddClientToUser : {e.Message}");
                return false;
            }
            return true;
        }

        public bool AddClientToUser(Client client, AppUser user)
        {
            return AddClientToUser(client.Id, user.Id);
        }

        public bool RemoveClientFromUser(Guid clientId, Guid userId)
        {
            var buff = _context.UsersToClients.FirstOrDefault(x => x.UserId == userId && x.ClientId == clientId);

            if (buff == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"No find relation between client ({clientId}) and user ({userId})");
                return false;
            }

            try
            {
                _context.UsersToClients.Remove(buff);
                _context.SaveChanges();
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Info, $"Remove relation between client ({clientId}) and user ({userId})");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Exception in client repository in RemoveClientFromUser : {e.Message}");
                return false;
            }
            return true;
        }

        public bool RemoveClientFromUser(Client client, AppUser user)
        {
            return RemoveClientFromUser(client.Id, user.Id);
        }
    }
}
