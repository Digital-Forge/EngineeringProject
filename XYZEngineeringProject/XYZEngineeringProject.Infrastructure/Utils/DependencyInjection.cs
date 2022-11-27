using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Infrastructure.Repositories;

namespace XYZEngineeringProject.Infrastructure.Utils
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //Other
            services.AddHttpContextAccessor();
            services.AddTransient<Logger>();
            //services.AddTransient<InfrastructureUtils>(); // Circular Dependency Injection Error

            // Dependency Injection
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<INoteRepository, NoteRepository>();
            services.AddTransient<IPositionRepository, PositionRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<IUtilsRepository, UtilsRepository>();
            services.AddTransient<IEmailRepository, EmailRepository>();
            services.AddTransient<IFileRepository, FileRepository>();
            //services.AddTransient<I, >();


            return services;
        }
    }
}
