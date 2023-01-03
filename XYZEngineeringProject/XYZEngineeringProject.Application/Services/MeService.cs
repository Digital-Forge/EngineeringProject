using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Domain.Models.EntityUtils;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Application.Services
{
    public class MeService : IMeService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly InfrastructureUtils _infrastructureUtils;
        //private readonly Context _context;
        //private readonly IHttpContextAccessor _contextAccessor;

        public MeService(IUserRepository userRepository, IDepartmentRepository departmentRepository, Context context, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
            _infrastructureUtils = new InfrastructureUtils(context, httpContextAccessor);
            //_context = context;
        }

        public LogicCompany? GetMyCompany()
        {
            var me = _userRepository.GetUserByIdAsQuerable(_infrastructureUtils.GetUserIdFormHttpContext().Value);
            var company = me.Select(x => x.Company).FirstOrDefault();

            return company;
        }

        public MyDataVM GetMyData()
        {
            var me = _userRepository.GetUserByIdAsQuerable(_infrastructureUtils.GetUserIdFormHttpContext().Value).Select(x => new MyDataVM
            {
                Id = x.Id,
                Name = x.Firstname,
                Surname = x.Surname,
                Email = x.Email,
                Phone = x.Address.Phone,
                PESEL = x.PESEL
            }).FirstOrDefault();

            me.Roles = _userRepository.GetUserRoles(me?.Id);

            return me;
        }

        public List<Department> GetMyDepartments()
        {
            var me = _userRepository.GetUserByIdAsQuerable(_infrastructureUtils.GetUserIdFormHttpContext().Value);

            return me.SelectMany(x => x.UsersToDepartments).Select(x => x.Departments).ToList();
        }

        public Guid? MeId()
        {
            return _infrastructureUtils.GetUserIdFormHttpContext();
        }
    }
}
