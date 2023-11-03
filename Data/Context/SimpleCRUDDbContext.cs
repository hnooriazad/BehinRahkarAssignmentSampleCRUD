using Microsoft.EntityFrameworkCore;
using SampleCRUD.Models;
using System.Data;

namespace SampleCRUD.Data.Context
{
    public class SimpleCRUDDbContext : DbContext
    {
        private readonly IDbConnection _dbConnection;

        public DbSet<Device> Device { get; set; }
        public SimpleCRUDDbContext(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("SimpleCRUDConnectionString"));
            }
        }

    }
}
