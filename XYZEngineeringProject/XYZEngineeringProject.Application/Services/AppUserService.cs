using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Domain.Models.EntityUtils;

namespace XYZEngineeringProject.Application.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;

        public AppUserService(IUserRepository userRepository, ICompanyRepository companyRepository)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
        }
        public List<AppUserVM> GetAllUsers()
        {
            var test = _userRepository.GetAll().Select(x => new AppUserVM
            {
                UserName = x.UserName,
                PasswordHash = x.PasswordHash,
                Name = x.Firstname,
                Surname = x.Surname,
                Id = x.Id.ToString(),
                PESEL = x.PESEL,
                Address = new AddressVM
                {
                    Id = x.Address.Id.ToString(),
                    AddressHome = x.Address.AddressHome,
                    AddressPost = x.Address.AddressPost,
                    Phone = x.Address.Phone.ToString(),
                },
            }).ToList();

            var list = _userRepository.GetAll().Where(x => x.Address!=null).Select(x => new AppUserVM
            {
                UserName = x.UserName,
                PasswordHash = x.PasswordHash,
                Name = x.Firstname,
                Surname = x.Surname,
                Id = x.Id.ToString(),
                PESEL =(string?) x.PESEL,
                Address = new AddressVM
                {
                    Id = x.Address.Id.ToString(),
                    AddressHome = x.Address.AddressHome,
                    AddressPost = x.Address.AddressPost,
                    Phone = x.Address.Phone.ToString(),
                },
            }).ToList();
            var listWithoutAddress = _userRepository.GetAll().Where(x=> x.Address == null).Select(x => new AppUserVM
            {
                UserName = x.UserName,
                PasswordHash = x.PasswordHash,
                Name = x.Firstname,
                Surname = x.Surname,
                Id = x.Id.ToString(),
                PESEL = x.PESEL,
            }).ToList();

            foreach (var item in listWithoutAddress)
            {
                list.Add(item);
            }
            return _userRepository.GetAll().Select(x => new AppUserVM
            {
                UserName = x.UserName,
                PasswordHash = x.PasswordHash,
                Name = x.Firstname,
                Surname = x.Surname,
                Id = x.Id.ToString(),
                PESEL = x.PESEL,
                Address = new AddressVM
                {
                    Id = x.Address.Id.ToString(),
                    AddressHome = x.Address.AddressHome,
                    AddressPost = x.Address.AddressPost,
                    Phone = x.Address.Phone.ToString(),
                },
            }).ToList(); ;
        }

        public bool AddNewUser(AppUserVM appUser)
        {
            AppUser user = new AppUser
            {
                Id = Guid.Empty,
                UserName = appUser.UserName,
                PasswordHash = appUser.PasswordHash,
                Firstname = appUser.Name,
                Surname= appUser.Surname,
                PESEL= appUser.PESEL,
            };

            var userId = _userRepository.Add(user);
            user.Id= userId.Value;
            user.Address = new Address
            {
                Id = Guid.Empty,
                AddressHome = appUser.Address.AddressHome,
                AppUserId = userId.Value,
                AddressPost = appUser.Address.AddressPost,
                CompanyId = user.CompanyId,
                Phone = appUser.Address.Phone,
                User = user
            };
            _userRepository.Update(user);

            return true;
        }

        public bool UpdateUser(AppUserVM appUser)
        {
            var user = _userRepository.GetUserById(Guid.Parse(appUser.Id));
            user.Address = _userRepository.GetUserAddress(user);
            if (user == null)
            {
                return false;
            }
            user.UserName = appUser.UserName;
            user.Firstname = appUser.Name;
            user.Surname= appUser.Surname;
            user.PESEL= appUser.PESEL;
            if (user.Address != null && appUser.Address != null)
            {
                user.Address.AddressPost = appUser.Address.AddressPost;
                user.Address.AddressHome = appUser.Address.AddressHome;
                user.Address.Phone = appUser.Address.Phone;
            }
            else if (user.Address == null && appUser.Address != null)
            {
                user.Address = new Address
                {
                    AddressHome = appUser.Address.AddressHome,
                    AddressPost = appUser.Address.AddressPost,
                    AppUserId = user.Id,
                    CompanyId = user.CompanyId,
                    Phone = appUser.Address.Phone,
                    Id = Guid.Empty
                };
            }
            else if (user.Address != null && appUser.Address == null)
            {
                //in current state there is no option for this to happen
                //_userRepository.RemoveUserAddress(user);
                //user.Address = null;
            }
            return _userRepository.Update(user);
        }

        public bool AddUserRole(Guid id, string roleName)
        {
            _userRepository.AddRole(_userRepository.GetUserById(id), roleName);
            return true;
        }

        public bool RemoveUserRole(Guid id, string roleName)
        {
            _userRepository.RemoveRole(_userRepository.GetUserById(id),roleName);
            return true;
        }

        public bool AddUserForNewCompany(AppUserVM appUserVM, Guid id)
        {
            LogicCompany company = _companyRepository.GetCompanyList().Where(x => x.Id == id).FirstOrDefault();
            AppUser user = new AppUser
            {
                CompanyId = id,
                Company = company,
                Firstname = appUserVM.Name,
                Surname = appUserVM.Surname,
                PESEL = appUserVM.PESEL,
                PasswordHash = appUserVM.PasswordHash,
                UserName = appUserVM.UserName,
            };
            var userId = _userRepository.AddUserForNewCompany(user);
            if (userId != null)
            {
                user = _userRepository.GetUserById((Guid)userId);
                var address = new Address
                {
                    AppUserId= user.Id,
                    CompanyId = id,
                    Phone = appUserVM.Address.Phone,
                    AddressHome= appUserVM.Address.AddressHome,
                    AddressPost = appUserVM.Address.AddressPost,
                    CreateBy = user.Id,
                    CreateDate= DateTime.Now,
                    UpdateBy= user.Id,
                    UpdateDate= DateTime.Now,
                    User = user
                };

                _userRepository.AddUserAddress(address);
                address = _userRepository.GetUserAddress(user);

                user.Address = address;
                user.AddressId = address.Id;
                _userRepository.Update(user);
                AddUserRole(user.Id, "MODERATOR");
            }
            return true;
        }
    }
}
