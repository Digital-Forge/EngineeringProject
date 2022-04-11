using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class NoteToUser
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
