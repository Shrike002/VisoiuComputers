using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisoiuComputers.Infrastructure.Config;

namespace VisoiuComputers.Database.Context
{
    public class VisoiuComputersDatabaseContextFactory : IDesignTimeDbContextFactory<VisoiuComputersDatabaseContext>
    {
        public VisoiuComputersDatabaseContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddJsonFile("appsettings.json", optional: true); // fallback option

            var configuration = builder.Build();
            AppConfig.Init(configuration);

            var optionsBuilder = new DbContextOptionsBuilder<VisoiuComputersDatabaseContext>();
            optionsBuilder.UseSqlServer(AppConfig.ConnectionStrings?.VisoiuComputersDataBase);

            if (AppConfig.ConsoleLogQueries)
            {
                optionsBuilder.LogTo(Console.WriteLine);
            }

            return new VisoiuComputersDatabaseContext(optionsBuilder.Options);
        }
    }
}
