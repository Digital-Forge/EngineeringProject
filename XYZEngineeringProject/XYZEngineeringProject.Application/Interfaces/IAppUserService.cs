using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.ViewModels;

namespace XYZEngineeringProject.Application.Interfaces
{
    public interface IAppUserService
    {
        bool AddNewUser(AppUserVM appUser);
        bool AddUserForNewCompany(AppUserVM appUserVM, Guid id);
        bool AddUserRole(Guid id, string roleName);
        public List<AppUserVM> GetAllUsers();
        bool RemoveUserRole(Guid id, string roleName);
        bool UpdateUser(AppUserVM appUser);
    }
}
