using XYZEngineeringProject.Application.ViewModels;

namespace XYZEngineeringProject.Application.Interfaces
{
    public interface IDepartmentService
    {
        public List<DepartmentVM> GetAllDepartments();
    }
}
