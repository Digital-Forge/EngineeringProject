using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Domain.Models.EntityUtils
{
    public class LogicCompany
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Relation

        public ICollection<AppUser> AppUsers { get; set; }
        public ICollection<Client> Clients { get; set; }

    }
}
