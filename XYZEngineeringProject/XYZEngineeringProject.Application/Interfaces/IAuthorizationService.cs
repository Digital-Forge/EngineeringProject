﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Application.ViewModels.Authorization;

namespace XYZEngineeringProject.Application.Interfaces
{
    public interface IAuthorizationService
    {
        bool AuthenticateUser(LoginVM input);
        string GenerateJsonWebToken(LoginVM input);

        void Logout();

        List<string> GetAllRoles();
        bool ChangePassword(Guid userId, ChangePasswordVM passwordVM);

        bool CheckNick(string name);
    }
}
