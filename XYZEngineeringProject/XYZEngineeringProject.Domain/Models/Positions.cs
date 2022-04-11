﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class Positions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<UsersToPositions> UsersToPositions { get; set; }
    }
}
