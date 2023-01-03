using XYZEngineeringProject.Application.ViewModels;

namespace XYZEngineeringProject.Application.Interfaces
{
    public interface IDepartmentService
    {
        bool AddDepartment(DepartmentVM departmentVM);
        bool DeleteDepartment(DepartmentVM department);
        bool EditDepartment(DepartmentVM departmentVM);
        public List<DepartmentVM> GetAllDepartments();
        public List<DepartmentVM> GetAllDepartmentsByUser(Guid userId);
        List<AppUserVM> GetDepartmentUsers(Guid departmentId);
        List<DepartmentVM> GetAllCompanyDepartmentsByCompany(Guid id);
    }
}
