using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Domain.Models.EntityUtils;

namespace XYZEngineeringProject.Application.Interfaces
{
    public interface IMeService
    {
        Guid? MeId();

        MyDataVM GetMyData();

        List<Department> GetMyDepartments();
        LogicCompany? GetMyCompany();
    }
}
