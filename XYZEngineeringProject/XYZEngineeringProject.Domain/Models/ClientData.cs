using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class ClientData
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Data { get; set; }

        //relations

        public Guid? ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
