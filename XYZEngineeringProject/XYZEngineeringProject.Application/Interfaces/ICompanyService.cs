using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.ViewModels;

namespace XYZEngineeringProject.Application.Interfaces
{
    public interface ICompanyService
    {
        CompanyVM? GetCompanyByUser(Guid id);
        List<CompanyVM> GetCompanyList();
        Guid? CreateCompany(CompanyVM company);
        bool UpdateCompany(CompanyVM company);
        bool DeleteCompany(Guid companyId);
    }
}
