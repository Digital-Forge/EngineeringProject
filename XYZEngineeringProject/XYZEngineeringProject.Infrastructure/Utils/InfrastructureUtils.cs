using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Domain.Models.EntityUtils;

namespace XYZEngineeringProject.Infrastructure.Utils
{
    public class InfrastructureUtils
    {
        private readonly Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InfrastructureUtils(Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? GetUserIdFormHttpContext()
        {
            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == "id")?.Value;

                if (!String.IsNullOrEmpty(userIdClaim))
                {
                    return _context.AppUsers.FirstOrDefault(x => x.UseStatus != UseStatusEntity.Delete && x.Id.ToString() == userIdClaim)?.Id;
                }
            }
            return null;
        }
        
        public AppUser? GetUserFormHttpContext()
        {
            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == "id")?.Value;

                if (userIdClaim != null)
                {
                    return _context.AppUsers.FirstOrDefault(x => x.UseStatus != UseStatusEntity.Delete && x.Id.ToString() == userIdClaim);
                }
            }
            return null;
        }

        public LogicCompany? GetCompany()
        {
            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == "id")?.Value;

                if (userIdClaim != null)
                {
                    return _context.AppUsers.Include(i => i.Company).FirstOrDefault(x => x.UseStatus != UseStatusEntity.Delete && x.Id.ToString() == userIdClaim)?.Company;
                }
            }
            return null;
        }
    }
}
