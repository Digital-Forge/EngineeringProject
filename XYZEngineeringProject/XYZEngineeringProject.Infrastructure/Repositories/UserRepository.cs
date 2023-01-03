using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        private readonly InfrastructureUtils _infrastructureUtils;
        private readonly Logger _logger;
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(Context context, IHttpContextAccessor httpContextAccessor, Logger logger, UserManager<AppUser> userManager)
        {
            _context = context;
            _infrastructureUtils = new InfrastructureUtils(context, httpContextAccessor);
            _logger = logger;
            _userManager = userManager;
        }

        public Guid? Add(AppUser user)
        {
            var currentUser = _infrastructureUtils.GetUserFormHttpContext();

            if(currentUser == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, "Failed add user, becouse httpcontext null");
                return null;
            }

            user.CreateDate = DateTime.Now;
            user.UpdateDate = DateTime.Now;
            user.CreateBy = currentUser.Id;
            user.UpdateBy = currentUser.Id;
            user.CompanyId = currentUser.CompanyId;

            var buffPassword = user.PasswordHash;
            user.PasswordHash = String.Empty;

            var result = _userManager.CreateAsync(user, buffPassword).Result;

            if (result.Succeeded)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Info, $"Add user - {user.Id}");
                return user.Id;
            }
            _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed add user - {result.Errors.First().Description}");
            return null;
        }

        public IQueryable<AppUser>? GetAll()
        {
            var currentUser = _infrastructureUtils.GetUserFormHttpContext();

            if (currentUser?.CompanyId == null || currentUser?.CompanyId == Guid.Empty)
                return null;
            else
                return _context.AppUsers
                    .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                    .Where(x => x.CompanyId == currentUser.CompanyId);
        }

        public IQueryable<AppUser>? GetUserByCompany()
        {
            var companyId = _infrastructureUtils.GetCompany()?.Id;

            if (companyId == null) return null;

            return _context.AppUsers
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Where(x => x.CompanyId == companyId);
        }

        public IQueryable<AppUser>? GetUserByCompany(Guid companyId)
        {
            return _context.AppUsers
                 .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                 .Where(x => x.CompanyId == companyId);
        }

        public AppUser? GetUserById(Guid userId)
        {
            return _context.AppUsers
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .FirstOrDefault(x => x.Id == userId);
        }

        public IQueryable<AppUser> GetUserByIdAsQuerable(Guid userId)
        {
            return _context.AppUsers
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Where(x => x.Id == userId);
        }

        public bool Remove(AppUser user)
        {
            if (user == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove null user");
                return false;
            }

            try
            {
                _context.AppUsers.Remove(user);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove user - {user.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed remove user - {user.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool Remove(Guid userById)
        {
            if (userById == Guid.Empty)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove user by empty guid Id");
                return false;
            }

            try
            {
                var buff = GetUserById(userById);
                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove user, who don't exist or deleted");
                    return false;
                }

                _context.AppUsers.Remove(buff);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove user - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed remove user - {userById} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool Update(AppUser user)
        {
            if (user == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying update null user");
                return false;
            }

            try
            {
                _context.AppUsers.Update(user);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Update user - {user.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed update user - {user.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public IQueryable<AppUser> _GetEveryOne()
        {
            return _context.AppUsers;
        }

        public bool __RemoveHard(AppUser user)
        {
            try
            {
                _context.AppUsers.Remove(user);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove user - {user.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed hard remove user - {user.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool __RemoveHard(Guid userById)
        {
            try
            {
                var buff = _context.AppUsers.FirstOrDefault(x => x.Id == userById);
                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying hard remove user, who don't exist");
                    return false;
                }

                _context.AppUsers.Remove(buff);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove user - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed hard remove user - {userById} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public void AddRole(AppUser user, string role)
        {
            var buffRole = _context.Roles.FirstOrDefault(x => x.NormalizedName == role.ToUpper());
            AddRole(user.Id, buffRole.Id);
        }

        public void AddRole(Guid userId, string role)
        {
            var buffRole = _context.Roles.FirstOrDefault(x => x.NormalizedName == role.ToUpper());
            AddRole(userId, buffRole.Id);
        }

        public void AddRole(Guid userId, Guid roleId)
        {
            _context.UserRoles.Add(new IdentityUserRole<Guid>
            {
                UserId = userId,
                RoleId = roleId
            });
            _context.SaveChanges();
        }

        public void RemoveRole(AppUser user, string role)
        {
            var buffRole = _context.Roles.FirstOrDefault(x => x.NormalizedName == role.ToUpper());
            RemoveRole(user.Id, buffRole.Id);
        }

        public void RemoveRole(Guid userId, string role)
        {
            var buffRole = _context.Roles.FirstOrDefault(x => x.NormalizedName == role.ToUpper());
            RemoveRole(userId, buffRole.Id);
        }

        public void RemoveRole(Guid userId, Guid roleId)
        {
            var buff = _context.UserRoles.FirstOrDefault(x => x.UserId == userId && x.RoleId == roleId);
            _context.UserRoles.Remove(buff);
            _context.SaveChanges();
        }

        public List<string> GetUserRoles(Guid? userId)
        {
            if (userId == null || userId == Guid.Empty) return new List<string>();

            var roles = _context.UserRoles.Where(x => x.UserId == userId).ToList();
            return _context.Roles.ToList().Where(x => roles.Any(a => a.RoleId == x.Id)).Select(x => x.Name).ToList();
        }

        public void RemoveUserAddress(AppUser user)
        {
            var buff = _context.UserAddresses.Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete).FirstOrDefault();
            _context.UserAddresses.Remove(buff);
            _context.SaveChanges();
        }

        public void AddUserAddress(Address address)
        {
            _context.UserAddresses.Add(address);
            _context.SaveChanges();
        }
    }
}
