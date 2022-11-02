using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Application.Services
{
    public class MeService : IMeService
    {
        private readonly IUserRepository _userRepository;
        private readonly InfrastructureUtils _infrastructureUtils;
        //private readonly Context _context;
        //private readonly IHttpContextAccessor _contextAccessor;

        public MeService(IUserRepository userRepository, Context context, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _infrastructureUtils = new InfrastructureUtils(context, httpContextAccessor);
        }

        public MyDataVM GetMyData()
        {
            var me = _userRepository.GetUserByIdAsQuerable(_infrastructureUtils.GetUserIdFormHttpContext().Value);

            return me.Select(x => new MyDataVM
            {
                Id = x.Id,
                Name = x.Firstname,
                Surname = x.Surname,
                Email = x.Email,
                Phone = x.PhoneNumber
            }).FirstOrDefault();
        }

        public Guid? MeId()
        {
            return _infrastructureUtils.GetUserIdFormHttpContext();
        }
    }
}
