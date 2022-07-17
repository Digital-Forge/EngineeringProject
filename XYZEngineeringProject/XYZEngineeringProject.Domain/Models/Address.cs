using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models.EntityUtils;

namespace XYZEngineeringProject.Domain.Models
{
    public class Address : ISoftDataEntity
    {
        public Guid Id { get; set; }
        public int Phone { get; set; }
        public string AddressPost { get; set; }
        public string AddressHome { get; set; }

        //relations

        public string? AppUserId { get; set; }
        public AppUser? User { get; set; }

        // ISoftDataEntity
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public UseStatusEntity UseStatus { get; set; }
        public Guid CompanyId { get; set; }
    }
}
