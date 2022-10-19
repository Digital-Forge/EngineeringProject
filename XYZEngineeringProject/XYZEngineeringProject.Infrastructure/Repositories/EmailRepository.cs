using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Infrastructure.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace XYZEngineeringProject.Infrastructure.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly Context _context;
        private readonly InfrastructureUtils _infrastructureUtils;
        private readonly Logger _logger;

        public EmailRepository(Context context, IHttpContextAccessor httpContextAccessor, Logger logger)
        {
            _context = context;
            _infrastructureUtils = new InfrastructureUtils(context, httpContextAccessor);
            _logger = logger;
        }

        public EmailConfig? GetCurrentEmailConfig()
        {
            var company = _infrastructureUtils.GetCompany();
            if (company == null) return null;

            return _context.EmailConfigs
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .FirstOrDefault(x => x.CompanyId == company.Id);
        }

        public EmailConfig? GetEmailConfigByCompany(Guid companyId)
        {
            return _context.EmailConfigs
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .FirstOrDefault(x => x.CompanyId == companyId);
        }

        public EmailConfig? GetEmailConfigByUser(Guid userId)
        {
            var companyId = _context.AppUsers.FirstOrDefault(x => x.Id == userId)?.CompanyId;
            if (companyId == null) return null;

            return _context.EmailConfigs
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .FirstOrDefault(x => x.CompanyId == companyId);
        }

        public EmailConfig? GetEmailConfigByUser(AppUser user)
        {
            if (user == null) return null;

            return _context.EmailConfigs
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .FirstOrDefault(x => x.CompanyId == user.CompanyId);
        }
    }
}
