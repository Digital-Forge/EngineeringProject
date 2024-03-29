﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Application.ViewModels
{
    public class ListOfTasksVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Project { get; set; }
        public StatusListOfTask Status { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<TaskVM> Tasks { get; set; }
        public Guid CreateBy { get; set; }
    }
}
