using XYZEngineeringProject.Application.ViewModels;

namespace XYZEngineeringProject.Application.Interfaces
{
    public interface IDepartmentService
    {
        bool AddDepartment(DepartmentVM departmentVM);
        bool DeleteDepartment(DepartmentVM department);
        bool EditDepartment(DepartmentVM departmentVM);
        public List<DepartmentVM> GetAllDepartments();
        List<AppUserVM> GetDepartmentUsers(Guid departmentId);
        List<DepartmentVM> GetAllCompanyDepartmentsByCompany(Guid id);
    }
}
