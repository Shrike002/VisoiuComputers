using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VisoiuComputers.Database.Context;
using VisoiuComputers.Database.Repositories;

namespace VisoiuComputers.Database
{
    public static class DIConfig
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddDbContext<VisoiuComputersDatabaseContext>();
            services.AddScoped<DbContext, VisoiuComputersDatabaseContext>();

            services.AddScoped<CategoryRepository>();
            services.AddScoped<ComponentRepository>();

            return services;
        }
    }
}
