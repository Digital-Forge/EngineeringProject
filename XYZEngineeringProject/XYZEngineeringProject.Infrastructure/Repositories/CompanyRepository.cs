using Microsoft.AspNetCore.Http;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models.EntityUtils;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly Context _context;
        private readonly InfrastructureUtils _utils;

        public CompanyRepository(Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _utils = new InfrastructureUtils(context, httpContextAccessor);
        }

        public Guid? CreateCompany(LogicCompany company)
        {
            _context.LogicCompanies.Add(company);
            _context.SaveChanges();
            return company.Id;
        }

        public LogicCompany? GetCompanyByUser(Guid id)
        {
            if (id == null || id == Guid.Empty) return null;

            var user = _context.AppUsers.FirstOrDefault(x => x.Id == id);

            if (user == null || user.CompanyId == null) return null;

            return _context.LogicCompanies.FirstOrDefault(x => x.Id == user.CompanyId);
        }

        public List<LogicCompany> GetCompanyList()
        {
            return _context.LogicCompanies.ToList();
        }

        public bool UpdateCompany(LogicCompany company)
        {
            var org = _context.LogicCompanies.FirstOrDefault(x => x.Id == company.Id);
            org.Name = company.Name;
            _context.SaveChanges();
            return true;
        }
        public bool DeleteCompany(Guid companyId)
        {
            if (companyId == null || companyId == Guid.Empty) return false;

            // AppUser
            var users = _context.AppUsers.Where(x => x.CompanyId == companyId).ToList();
            foreach (var item in users)
            {
                item.PasswordHash = null;
                item.LockoutEnabled = true;
            }
            _context.SaveChanges();

            foreach (var item in users)
            {
                _context.Remove(item);
            }

            // Address
            var adderses = _context.UserAddresses.Where(x => x.CompanyId == companyId).ToList();
            foreach (var item in adderses)
            {
                _context.Remove(item);
            }

            // Clients Contact
            var clientContacts = _context.ClientContacts.Where(x => x.CompanyId == companyId).ToList();
            foreach (var item in clientContacts)
            {
                _context.Remove(item);
            }

            // Clients
            var clients = _context.Clients.Where(x => x.CompanyId == companyId).ToList();
            foreach (var item in users)
            {
                _context.Remove(item);
            }

            // Note
            var notes = _context.Note.Where(x => x.CompanyId == companyId).ToList();
            foreach (var item in users)
            {
                _context.Remove(item);
            }

            // ForumMessage
            var forumMessage = _context.ForumMessages.Where(x => x.CompanyId == companyId).ToList();
            foreach (var item in users)
            {
                _context.Remove(item);
            }

            // Forum
            var forum = _context.Forums.Where(x => x.CompanyId == companyId).ToList();
            foreach (var item in users)
            {
                _context.Remove(item);
            }

            // Task
            var task = _context.Tasks.Where(x => x.CompanyId == companyId).ToList();
            foreach (var item in users)
            {
                _context.Remove(item);
            }

            // Task List
            var taskList = _context.ListTasks.Where(x => x.CompanyId == companyId).ToList();
            foreach (var item in users)
            {
                _context.Remove(item);
            }

            // File
            var files = _context.Files.Where(x => x.CompanyId == companyId).ToList();
            foreach (var item in users)
            {
                _context.Remove(item);
            }

            // Directory
            var directories = _context.Directories.Where(x => x.CompanyId == companyId).ToList();
            foreach (var item in users)
            {
                _context.Remove(item);
            }

            // Email
            var emails = _context.EmailConfigs.Where(x => x.CompanyId == companyId).ToList();
            foreach (var item in users)
            {
                _context.Remove(item);
            }

            // Position
            var positions = _context.Positions.Where(x => x.CompanyId == companyId).ToList();
            foreach (var item in users)
            {
                _context.Remove(item);
            }


            // WARRNING 
            _context.SaveChanges();
            return true;
        }

    }
}
