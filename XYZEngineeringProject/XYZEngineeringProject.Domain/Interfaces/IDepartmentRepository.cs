using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Domain.Interfaces
{
    public interface IDepartmentRepository
    {
        Guid Add(Department department);
        bool Remove(Department department);
        bool Remove(Guid departmentById);
        bool __RemoveHard(Department department);
        bool __RemoveHard(Guid departmentById);
        bool Update(Department department);
        Department? GetDepartmentById(Guid departmentId);
        IQueryable<Department> GetDepartmentByIdAsQuerable(Guid departmentId);
        IQueryable<Department>? GetAll();
        IQueryable<Department> _GetEveryOne();
        IQueryable<Department>? GetDepartmentByCompany();
        IQueryable<Department>? GetDepartmentByCompany(Guid companyId);

    }
}
