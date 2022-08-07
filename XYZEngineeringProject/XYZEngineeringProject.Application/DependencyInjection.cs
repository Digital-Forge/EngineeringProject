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
            //services.AddTransient<I, >();
            
            // FluentValidation Dependency Injection
            
            // AutoMapper Dependency Injection
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}