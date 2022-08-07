using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class UsersToClientsGroups
    {
        //relations

        public string UserId { get; set; }
        public virtual AppUser User { get; set; }

        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }

        public Guid? GroupId { get; set; }
        public virtual Group Group { get; set; }

    }
}
