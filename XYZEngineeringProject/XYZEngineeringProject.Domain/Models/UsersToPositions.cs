using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class UsersToPositions
    {
        //relations

        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; }

        public Guid PositionId { get; set; }
        public virtual Position Position { get; set; }
    }
}
