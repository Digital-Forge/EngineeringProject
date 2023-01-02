using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models.EntityUtils
{
    public class LogicCompany : ISoftDataEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Relation

        public ICollection<AppUser> AppUsers { get; set; }
        public ICollection<Client> Clients { get; set; }

        //ISoftDataEntity
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public UseStatusEntity UseStatus { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
