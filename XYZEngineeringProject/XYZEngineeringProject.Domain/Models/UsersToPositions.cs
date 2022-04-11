using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class UsersToPositions
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int PositionId { get; set; }
        public Positions Position { get; set; }
    }
}
