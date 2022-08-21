﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Application.ViewModels
{
    public class AppUserVM
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public int PESEL { get; set; }

        public Address? Address { get; set; }

    }
}