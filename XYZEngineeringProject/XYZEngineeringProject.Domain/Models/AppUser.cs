using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public int PESEL { get; set; }
        public int PositionId { get; set; }
        public Positions Position { get; set; }

        public IList<UsersToDepartments> UsersToDepartments { get; set; }

    }
}
