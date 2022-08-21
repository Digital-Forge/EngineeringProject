using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels.Authorization;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Application.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly IUtilsService _utilsService;

        public AuthorizationService(SignInManager<AppUser> signInManager, IConfiguration config, IUtilsService utilsService)
        {
            _signInManager = signInManager;
            _config = config;
            _utilsService = utilsService;
        }

        public bool AuthenticateUser(LoginVM input)
        {
            if (input == null) return false;
            if (string.IsNullOrEmpty(input.Email) || string.IsNullOrEmpty(input.Password)) return false;

            if (input.Email.ToLower() == "admin" && input.Password.ToLower() == "admin")
            {
                if (_utilsService.isVoid())
                {
                    _utilsService.InitWorld();
                    input.Email = input.Email.ToLower();
                    input.Password = input.Password.ToLower();
                }
            }

            return _signInManager.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, false).Result.Succeeded;
        }

        public string GenerateJsonWebToken(LoginVM input)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["JWT:Issuer"], _config["JWT:Issuer"], null, null, DateTime.Now.AddMinutes(120), credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void Logout()
        {
            _signInManager.SignOutAsync().Wait();
        }
    }
}
