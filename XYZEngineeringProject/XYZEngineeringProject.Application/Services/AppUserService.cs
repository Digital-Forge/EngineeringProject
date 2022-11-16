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
                Name = x.Firstname,
                Surname = x.Surname,
                Id = x.Id.ToString(),
                Address = new AddressVM 
                {
                    Id=x.Address.Id.ToString(),
                    AddressHome= x.Address.AddressHome,
                    AddressPost = x.Address.AddressPost,
                    Phone = x.Address.Phone
                },
            }).ToList();
        }

        public bool AddNewUser(AppUserVM appUser)
        {
            AppUser user = new AppUser
            {
                Id = Guid.Empty,
                UserName = appUser.UserName,
                PasswordHash = appUser.PasswordHash,
                Firstname = appUser.Name,
                Surname= appUser.Surname
            };

            _userRepository.Add(user);

            return true;
        }
    }
}
