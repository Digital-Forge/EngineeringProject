using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public int Phone { get; set; }
        public string AddressPost { get; set; }
        public string AddressHome { get; set; }

        //relations

        public string? AppUserId { get; set; }
        public AppUser? User { get; set; }

    }
}
