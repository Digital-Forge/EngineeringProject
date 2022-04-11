using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class ClientsAdresses
    {
        public int Id { get; set; }
        public TypeAddress? TypeAddress { get; set; }
        public string Data { get; set; }
    }
}
