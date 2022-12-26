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
        private readonly IDepartmentRepository _departmentRepository;

        public UtilsRepository(Context context, UserManager<AppUser> userManager, Logger logger, IDepartmentRepository departmentRepository)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _departmentRepository = departmentRepository;
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
            AddRole("ADM");
            AddRole("MANAGEMENT");
            AddRole("MANAGER");
            AddRole("MODERATOR");
            AddRole("EMPLOYEE");

            _context.LogicCompanies.Add(new Domain.Models.EntityUtils.LogicCompany { Name = "CompanyName" });
            _context.SaveChanges();
        }

        public void InitHelloWorld2()
        {
            _departmentRepository._Add(new Department
            {
                Name = "FirstDepartment"
            },
            _context.LogicCompanies.FirstOrDefault());
            _context.SaveChanges();
        }

        public bool AddRole(string name)
        {
            try
            {
                _context.Roles.Add(new IdentityRole<Guid>
                {
                    Name = name.ToUpper(),
                    NormalizedName = name.ToUpper()
                });
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
