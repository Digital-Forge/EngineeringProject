﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Application.ViewModels.Authorization;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Application.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IUtilsService _utilsService;
        private readonly Context _context;

        public AuthorizationService(SignInManager<AppUser> signInManager, IConfiguration config, IUtilsService utilsService, Context context, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _config = config;
            _utilsService = utilsService;
            _context = context;
            _userManager = userManager;
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
            var user = _context.AppUsers.First(x => x.UserName == input.Email);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>() { new Claim("id", user.Id.ToString())};

            var roles = _context.UserRoles.Where(x => x.UserId == user.Id).ToList();
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, _context.Roles.First(x => x.Id == role.RoleId).Name));
            }         

            var token = new JwtSecurityToken(_config["JWT:Issuer"], _config["JWT:Issuer"], claims, null, DateTime.Now.AddMinutes(600), credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void Logout()
        {
            _signInManager.SignOutAsync().Wait();
        }

        public List<string> GetAllRoles()
        {
            return _context.Roles.Select(x => x.Name).ToList();
        }

        public bool ChangePassword(Guid userId, ChangePasswordVM passwordVM)
        {
            var user = _context.AppUsers.FirstOrDefault(x => x.Id == userId);
            if (user == null) return false;

            var result =_userManager.ChangePasswordAsync(user, passwordVM.OldPassword, passwordVM.NewPassword).Result;
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckNick(string name)
        {
            return _context.AppUsers.Any(x => x.NormalizedUserName == name.ToUpper());
        }
    }
}
