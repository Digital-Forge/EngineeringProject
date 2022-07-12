﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class ListOfTasks
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //relations

        public string? UserId { get; set; }
        public AppUser? User { get; set; }

        public ICollection<Task>? Task { get; set; }
    }
}