using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class UsersClientsGroups
    {
        //relations

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public Guid? GroupId { get; set; }
        public Group? Group { get; set; }

    }
}
