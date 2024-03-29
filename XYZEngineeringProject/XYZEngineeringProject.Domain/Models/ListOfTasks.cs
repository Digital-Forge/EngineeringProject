﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models.EntityUtils;

namespace XYZEngineeringProject.Domain.Models
{
    public class ListOfTasks : ISoftDataEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public StatusListOfTask Status { get; set; }

        public string Project { get; set; }

        //relations

        public Guid? UserId { get; set; }
        public virtual AppUser User { get; set; }

        public virtual ICollection<UserTask> Task { get; set; }

        // ISoftDataEntity
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public UseStatusEntity UseStatus { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
