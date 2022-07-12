using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public NoteStatus NoteStatus { get; set; }
        public DateTime Date { get; set; }

        //relations

        public IList<NoteToUser>? NoteToUsers { get; set; }


    }
}
