using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int Phone { get; set; }
        public string AddressPost { get; set; }
        public string AddressHome { get; set; }
        public int AddressOfAppUserId { get; set; }
        public AppUser User { get; set; }

    }
}
