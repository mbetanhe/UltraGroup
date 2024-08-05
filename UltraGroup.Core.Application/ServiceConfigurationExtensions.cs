using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UltraGroup.Core.Application.Validators;

namespace UltraGroup.Core.Application
{
    public static class ServiceConfigurationExtensions
    {
        public static void AddFromApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
