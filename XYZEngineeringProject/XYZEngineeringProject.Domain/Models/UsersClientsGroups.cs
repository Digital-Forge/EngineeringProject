using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class UsersClientsGroups
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int ClientId { get; set; }
        public Clients Client { get; set; }
        public int? GroupId { get; set; }
        public Groups? Group { get; set; }

    }
}
