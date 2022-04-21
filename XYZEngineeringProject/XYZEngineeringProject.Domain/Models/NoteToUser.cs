using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class NoteToUser
    {
        //relations

        public string UserId { get; set; }
        public AppUser User { get; set; }
        public Guid NoteId { get; set; }
        public Note Note { get; set; }
    }
}
