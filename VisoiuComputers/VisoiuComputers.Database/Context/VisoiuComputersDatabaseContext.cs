using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using VisoiuComputers.Database.Entities;
using VisoiuComputers.Infrastructure.Config;

namespace VisoiuComputers.Database.Context
{
    public class VisoiuComputersDatabaseContext : DbContext
    {
        public VisoiuComputersDatabaseContext(DbContextOptions<VisoiuComputersDatabaseContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Component>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Components)
                .HasForeignKey(c => c.CategoryId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppConfig.ConnectionStrings?.VisoiuComputersDataBase);

            if (AppConfig.ConsoleLogQueries)
            {
                optionsBuilder.LogTo(Console.WriteLine);
            }
        }

        public DbSet<Component> Components { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
