using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models.EntityUtils;

namespace XYZEngineeringProject.Domain.Models.Forum
{
    public class ForumMessage : ISoftDataEntity
    {
        public Guid Id { get; set; }
        public string Content { get; set; }

        //Relation
        public Guid ForumId { get; set; }
        public virtual Forum Forum { get; set; }

        //ISoftDataEntity
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public UseStatusEntity UseStatus { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
