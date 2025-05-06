using Microsoft.Extensions.DependencyInjection;
using VisoiuComputers.Core.Services;

namespace VisoiuComputers.Core
{
    public static class DIConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<CategoryService>();
            services.AddScoped<ComponentService>();
            return services;
        }
    }
}
