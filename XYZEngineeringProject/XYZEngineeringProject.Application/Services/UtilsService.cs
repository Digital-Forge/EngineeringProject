﻿using System;
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

        public UtilsService(IUserRepository userRepository, IUtilsRepository utilsRepository, Logger logger)
        {
            _userRepository = userRepository;
            _utilsRepository = utilsRepository;
            _logger = logger;
        }

        public void InitWorld()
        {
            var firstAdmin = new AppUser
            {
                FullName = "Jan Kowalski",
                UserName = "admin",
                PasswordHash = "admin"
            };

            _utilsRepository.InitHelloWorld();
            _utilsRepository.CreateAdmin(firstAdmin);
        }

        public bool isVoid()
        {
            return _userRepository._GetEveryOne().Count() == 0;
        }
    }
}