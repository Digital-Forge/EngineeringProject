using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace XYZEngineeringProject.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Services Dependency Injection
            //services.AddTransient<I, >();
            
            // FluentValidation Dependency Injection
            
            // AutoMapper Dependency Injection
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}