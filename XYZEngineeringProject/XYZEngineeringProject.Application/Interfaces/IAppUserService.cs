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
        public List<AppUserVM> GetAllUsers();
    }
}
