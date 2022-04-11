using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public Priority Priority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignerUserId { get; set; }
        public AppUser AssignerUser { get; set; }
        public string AssigneeUserId { get; set; }
        public AppUser AssigneeUser { get; set; }
        public int ListOfTasksId { get; set; }
        public ListOfTasks ListOfTasks { get; set; }
    }
}
