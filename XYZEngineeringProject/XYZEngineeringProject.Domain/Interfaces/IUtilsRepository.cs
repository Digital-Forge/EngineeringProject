using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Domain.Interfaces
{
    public interface IUtilsRepository
    {
        Guid? CreateAdmin(AppUser appUser);
        void InitHelloWorld();
        bool AddRole(string name);
    }
}
