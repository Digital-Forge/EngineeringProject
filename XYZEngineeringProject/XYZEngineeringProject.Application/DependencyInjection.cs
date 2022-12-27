using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.Services;

namespace XYZEngineeringProject.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Services Dependency Injection
            services.AddTransient<IUtilsService, UtilsService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IMeService, MeService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            //services.AddTransient<I, >();
            
            // FluentValidation Dependency Injection
            
            // AutoMapper Dependency Injection
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}