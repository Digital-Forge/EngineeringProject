﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class UsersToDepartments
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int DepartmentId { get; set; }
        public Departments Departments { get; set; }
    }
}
