using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class ClientAdress
    {
        public Guid Id { get; set; }
        public TypeAddress? TypeAddress { get; set; }
        public string DataAddress { get; set; }

        //relations

        public Guid? ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
