using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Domain.Models.EntityUtils;

namespace XYZEngineeringProject.Domain.Interfaces
{
    public interface IDepartmentRepository
    {
        Guid Add(Department department);
        Guid _Add(Department department, LogicCompany company);
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

        void AddUserToDepartment(AppUser user, Department department);
        void AddUserToDepartment(Guid userId, Guid departmentId);
        void RemoveUserFromDepartment(AppUser user, Department department);
        void RemoveUserFromDepartment(Guid userId, Guid departmentId);
    }
}
