using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Application.ViewModels.Forum
{
    public class ForumVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ForumMessageVM> Content { get; set; }
    }
}
