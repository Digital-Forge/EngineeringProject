using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class ListOfTasks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TaskId { get; set; }
        public ICollection<Tasks>? Task { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
    }
}
