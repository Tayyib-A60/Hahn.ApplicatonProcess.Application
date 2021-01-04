using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Hahn.ApplicatonProcess.December2020.Data.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../Hahn.ApplicatonProcess.December2020.Web/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            string connectionString = configuration.GetConnectionString("DbConnectionString");
            builder.UseSqlServer(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
