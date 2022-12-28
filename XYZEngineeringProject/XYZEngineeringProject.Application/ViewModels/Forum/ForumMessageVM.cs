using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Application.ViewModels.Forum
{
    public class ForumMessageVM
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public Guid? AuthorId { get; set; }
        public DateTime Date { get; set; }
    }
}
