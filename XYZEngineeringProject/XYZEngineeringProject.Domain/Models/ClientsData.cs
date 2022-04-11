using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class ClientsData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Data { get; set; }
        public Clients? Client { get; set; }
    }
}
