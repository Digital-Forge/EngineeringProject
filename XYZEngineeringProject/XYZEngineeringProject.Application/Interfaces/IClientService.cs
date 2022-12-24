using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.ViewModels;

namespace XYZEngineeringProject.Application.Interfaces
{
    public interface IClientService
    {
        public List<ClientVM> GetAllClients();
        public bool AddClient (ClientVM clientVM);
        public bool EditClient(ClientVM clientVM);
        bool DeleteClient(ClientVM client);
        bool DeleteClientContact(ClientContactVM contact);
    }
}
