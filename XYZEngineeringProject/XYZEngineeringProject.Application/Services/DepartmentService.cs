using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Application.Services;
public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly Context _context;

    public DepartmentService(IDepartmentRepository departmentRepository, Context context)
    {
        _departmentRepository = departmentRepository;
        _context = context;
    }

    public List<DepartmentVM> GetAllDepartments()
    {
        return _departmentRepository.GetAll().Select(x => new DepartmentVM
        {
            Id = x.Id.ToString(),
            Name = x.Name,
            ManagerId = x.Manager.ToString().ToUpper()
        }).ToList();
    }

    public List<AppUserVM> GetDepartmentUsers(Guid departmentId)
    {
        return _departmentRepository.GetDepartmentUsers(departmentId).Select(x => new AppUserVM
        {
            Id = x.Id.ToString().ToUpper(),
            Name = x.Firstname,
            Surname= x.Surname,
            UserName= x.UserName,
        }).ToList();
    }


    public bool AddDepartment(DepartmentVM departmentVM)
    {
        var department = new Department
        {
            Name = departmentVM.Name,
        };
        if(departmentVM.ManagerId.Length>0) department.Manager = Guid.Parse(departmentVM.ManagerId.ToString());
        Guid newDepartmentId = _departmentRepository.Add(department);
        foreach (AppUserVM userVM in departmentVM.Users)
        {
            _departmentRepository.AddUserToDepartment(Guid.Parse(userVM.Id),newDepartmentId);
        }
        return true;
    }

    public bool EditDepartment(DepartmentVM departmentVM)
    {
        var department = _departmentRepository.GetDepartmentById(Guid.Parse(departmentVM.Id));
        department.Name = departmentVM.Name;
        department.Manager = Guid.Parse(departmentVM.ManagerId);
        _departmentRepository.Update(department);
        var users = GetDepartmentUsers(Guid.Parse(departmentVM.Id));
        foreach (AppUserVM userVM in users)
        {
            if (!departmentVM.Users.Contains(userVM))
            {
                _departmentRepository.RemoveUserFromDepartment(Guid.Parse(userVM.Id), Guid.Parse(departmentVM.Id));
            }
        }
        foreach (AppUserVM userVM in departmentVM.Users)
        {
            if (!users.Contains(userVM))
            {
                _departmentRepository.AddUserToDepartment(Guid.Parse(userVM.Id),Guid.Parse(departmentVM.Id));
            }
        }
        return true;
    }

    public bool DeleteDepartment(DepartmentVM department)
    {
        var users = _departmentRepository.GetDepartmentUsers(Guid.Parse(department.Id));
        foreach (var user in users)
        {
            _departmentRepository.RemoveUserFromDepartment(user.Id,Guid.Parse(department.Id));
        }
        _departmentRepository.Remove(Guid.Parse(department.Id));
        return true;
    }

    public List<DepartmentVM> GetAllCompanyDepartmentsByCompany(Guid id)
    {
        if (id == null || id == Guid.Empty) return new List<DepartmentVM>();

        return _context.Departments.Where(x => x.CompanyId == id).Select(s => new DepartmentVM
        {
            Id = s.Id.ToString(),
            ManagerId = s.Manager.ToString(),
            Name = s.Name
        }).ToList();
    }

    public List<DepartmentVM> GetAllDepartmentsByUser(Guid userId)
    {
        return _context.UsersToDepartments
            .Include(i => i.Departments)
            .Where(x => x.UserId == userId)
            .Select(s => s.Departments)
            .Select(m => new DepartmentVM
            {
                Id = m.Id.ToString(),
                ManagerId = m.Manager != null ? m.Manager.ToString() : string.Empty,
                Name = m.Name
            }).ToList();
    }
}
