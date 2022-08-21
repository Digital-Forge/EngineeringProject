using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Domain.Interfaces
{
    public interface IUserRepository
    {
        Guid? Add(AppUser user);
        bool Remove(AppUser user);
        bool Remove(Guid userById);
        bool __RemoveHard(AppUser user);
        bool __RemoveHard(Guid userById);
        bool Update(AppUser user);
        AppUser? GetUserById(Guid userId);
        IQueryable<AppUser> GetUserByIdAsQuerable(Guid userId);
        IQueryable<AppUser>? GetAll();
        IQueryable<AppUser> _GetEveryOne();
        IQueryable<AppUser>? GetUserByCompany();
        IQueryable<AppUser>? GetUserByCompany(Guid companyId);
    }
}
