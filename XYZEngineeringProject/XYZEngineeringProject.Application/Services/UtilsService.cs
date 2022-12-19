using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Application.Services
{
    public class UtilsService : IUtilsService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUtilsRepository _utilsRepository;
        private readonly Logger _logger;
        private readonly IDepartmentRepository _departmentRepository;

        public UtilsService(IUserRepository userRepository, IUtilsRepository utilsRepository, Logger logger, IDepartmentRepository departmentRepository)
        {
            _userRepository = userRepository;
            _utilsRepository = utilsRepository;
            _logger = logger;
            _departmentRepository = departmentRepository;
        }

        public void InitWorld()
        {
            var firstAdmin = new AppUser
            {
                Firstname = "admin",
                Surname = "admin",
                UserName = "admin",
                PasswordHash = "admin"
            };

            _utilsRepository.InitHelloWorld();
            var adminId = _utilsRepository.CreateAdmin(firstAdmin);
            _utilsRepository.InitHelloWorld2();
            _userRepository.AddRole(adminId.Value, "ADM");

            var dep = _departmentRepository._GetEveryOne().FirstOrDefault();
            _departmentRepository.AddUserToDepartment(adminId.Value, dep.Id);
        }

        public bool isVoid()
        {
            return _userRepository._GetEveryOne().Count() == 0;
        }
    }
}
