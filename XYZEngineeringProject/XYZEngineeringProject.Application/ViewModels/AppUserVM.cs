using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Application.ViewModels
{
    public class AppUserVM
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public string PESEL { get; set; }

        public AddressVM? Address { get; set; }

    }
}
