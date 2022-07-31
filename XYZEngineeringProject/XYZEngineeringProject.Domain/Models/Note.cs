using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models.EntityUtils;

namespace XYZEngineeringProject.Domain.Models
{
    public class Note : ISoftDataEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public NoteStatus NoteStatus { get; set; }
        public DateTime Date { get; set; }

        //relations

        public IList<NoteToUser>? NoteToUsers { get; set; }

        // ISoftDataEntity
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public UseStatusEntity UseStatus { get; set; }
        public Guid CompanyId { get; set; }
    }
}
