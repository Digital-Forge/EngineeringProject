using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models.EntityUtils;

namespace XYZEngineeringProject.Domain.Models.Forum
{
    public class Forum : ISoftDataEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //Relations
        public virtual ICollection<ForumMessage> ForumMessages { get; set; }

        //ISoftDataEntity
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public UseStatusEntity UseStatus { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
