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

namespace XYZEngineeringProject.Infrastructure.Utils
{
    public class InfrastructureUtils
    {
        private readonly Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public InfrastructureUtils(Context context, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
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

    }
}
