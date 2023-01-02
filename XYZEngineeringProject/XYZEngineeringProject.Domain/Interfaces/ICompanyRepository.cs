using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models.EntityUtils;

namespace XYZEngineeringProject.Domain.Interfaces
{
    public interface ICompanyRepository
    {
        LogicCompany? GetCompanyByUser(Guid id);
        List<LogicCompany> GetCompanyList();
        Guid? CreateCompany(LogicCompany company);
        bool UpdateCompany(LogicCompany company);
        bool DeleteCompany(Guid companyId);
    }
}
