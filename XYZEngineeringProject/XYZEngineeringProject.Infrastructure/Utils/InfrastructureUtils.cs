using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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

        public string? GetUserIdFormHttpContext()
        {
            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    return userIdClaim.Value;
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
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    return _context.AppUsers.Where(x => x.Id.ToString() == userIdClaim.Value).FirstOrDefault();
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
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    return _context.AppUsers.Where(x => x.Id.ToString() == userIdClaim.Value).FirstOrDefault()?.Company;
                }
            }
            return null;
        }
    }
}
