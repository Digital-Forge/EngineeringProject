using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        //relations

        public ICollection<UsersClientsGroups>? UsersClientsGroups { get; set; }
        public ICollection<ClientAdress>? ClientAdresses { get; set; }
    }
}
