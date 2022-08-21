using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Application.Interfaces
{
    public interface ITaskService
    {
        public List<TaskVM> GetAllTasks();
    }
}
