using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models.EntityUtils
{
    public class Log
    {
        public Guid Id { get; set; }
        public string Info { get; set; }
        public string InfoType { get; set; }
        public string Source { get; set; }
        public string? UserId { get; set; }
        public DateTime DataStamp { get; set; }
    }
}
