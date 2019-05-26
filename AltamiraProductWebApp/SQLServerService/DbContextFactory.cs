using AltamiraShared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLServerService
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ProductContext>, IDisposable
    {
        private static string _connectionString;

        public ProductContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public ProductContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<ProductContext>();
            builder.UseSqlServer(_connectionString);

            return new ProductContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Dispose()
        {
            
        }
    }
}
