using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Application.ViewModels
{
    public class TaskVM
    {
        public Guid Id { get; set; }
        public DateTime Deadline { get; set; }
        public Priority Priority { get; set; }
        public string Title { get; set; }

        public string AssigneeUserId { get; set; }

        public string Description { get; set; }

        public Guid? ListOfTasksId { get; set; }
        public ListOfTasks? ListOfTasks { get; set; }
    }
}
