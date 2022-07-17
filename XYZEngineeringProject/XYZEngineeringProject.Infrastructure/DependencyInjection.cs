using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Infrastructure.Repositories;

namespace XYZEngineeringProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<INoteRepository, NoteRepository>();
            services.AddTransient<IPositionRepository, PositionRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            //services.AddTransient<I, >();
            
            return services;
        }
    }
}
