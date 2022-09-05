using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models.EntityUtils;

namespace XYZEngineeringProject.Domain.Models
{
    public class ClientAdress : ISoftDataEntity
    {
        public Guid Id { get; set; }
        public TypeAddress? TypeAddress { get; set; }
        public string DataAddress { get; set; }

        //relations

        public Guid? ClientId { get; set; }
        public virtual Client Client { get; set; }

        // ISoftDataEntity
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public UseStatusEntity UseStatus { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
