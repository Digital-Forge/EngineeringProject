using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models.EntityUtils;

namespace XYZEngineeringProject.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Guid? CreateCompany(CompanyVM company)
        {
            if (company == null) return null;

            var obj = new LogicCompany
            {
                Name = company.Name,
            };

            return _companyRepository.CreateCompany(obj);
        }

        public bool DeleteCompany(Guid companyId)
        {
            return _companyRepository.DeleteCompany(companyId);
        }

        public CompanyVM? GetCompanyByUser(Guid id)
        {
            if (id == null || id == Guid.Empty) return null;

            var obj = _companyRepository.GetCompanyByUser(id);

            if (obj == null) return null;

            return new CompanyVM
            {
                Id = obj.Id,
                Name = obj.Name,
                Delete = obj.UseStatus == UseStatusEntity.Delete
            };
        }

        public List<CompanyVM> GetCompanyList()
        {
            return _companyRepository.GetCompanyList().Select(m => new CompanyVM
            {
                Id = m.Id,
                Name = m.Name,
                Delete = m.UseStatus == UseStatusEntity.Delete
            }).ToList();
        }

        public bool UpdateCompany(CompanyVM company)
        {
            if (company == null) return false;

            var obj = new LogicCompany
            {
                Id = company.Id,
                Name = company.Name,
            };

            return _companyRepository.UpdateCompany(obj);
        }
    }
}
