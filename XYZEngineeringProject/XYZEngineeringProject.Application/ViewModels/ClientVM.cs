using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Application.ViewModels
{
    public class ClientVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string NIP { get; set; }
        public string Address { get; set; }
        public ClientContactVM[] Contacts{ get; set; }
    }
}
