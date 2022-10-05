using System;
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
        public StatusListOfTask Status { get; set; }
    }
}
