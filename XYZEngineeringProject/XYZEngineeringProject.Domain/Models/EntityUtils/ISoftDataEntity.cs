using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models.EntityUtils
{
    public interface ISoftDataEntity
    {
        // Data modyfication 
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        // SoftDelete
        public UseStatusEntity UseStatus { get; set; }

        // Date per logic instance
        public Guid CompanyId { get; set; }

    }
}
