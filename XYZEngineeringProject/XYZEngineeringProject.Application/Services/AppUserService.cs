using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Application.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IUserRepository _userRepository;

        public AppUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public List<AppUserVM> GetAllUsers()
        {
            return _userRepository.GetAll().Select(x => new AppUserVM
            {
                UserName = x.UserName,
                PasswordHash = x.PasswordHash,
                Address = x.Address,
                FullName = x.FullName,
                PESEL = x.PESEL,
                Id = x.Id.ToString()
            }).ToList();
        }

        public bool AddNewUser(AppUserVM appUser)
        {
            AppUser user = new AppUser
            {
                Id = new Guid(appUser.Id),
                UserName = appUser.UserName,
                PasswordHash = appUser.PasswordHash,
                FullName = appUser.FullName,
                PESEL= appUser.PESEL,
                Address = appUser.Address
            };

            _userRepository.Add(user);

            return true;
        }
    }
}
