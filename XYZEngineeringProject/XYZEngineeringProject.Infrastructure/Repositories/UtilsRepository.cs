using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Infrastructure.Repositories
{
    public class UtilsRepository : IUtilsRepository
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly Logger _logger;

        public UtilsRepository(Context context, UserManager<AppUser> userManager, Logger logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public Guid? CreateAdmin(AppUser admin)
        {
            admin.Id = Guid.Empty;
            admin.CompanyId = _context.LogicCompanies.FirstOrDefault()?.Id;
            admin.CreateDate = DateTime.Now;
            admin.UpdateDate = DateTime.Now;
            admin.CreateBy = Guid.Empty;
            admin.UpdateBy = Guid.Empty;

            string buffPassword = admin.PasswordHash;
            admin.PasswordHash = string.Empty;

            var result = _userManager.CreateAsync(admin, buffPassword).Result;

            if (result.Succeeded)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Info, $"Add admin - {admin.Id}");
                return admin.Id;
            }
            _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed add admin - {result.Errors.First().Description}");
            return null;
        }

        public void InitHelloWorld()
        {
            _context.LogicCompanies.Add(new Domain.Models.EntityUtils.LogicCompany { Name = "Nicość" });
            _context.SaveChanges();
        }
    }
}
