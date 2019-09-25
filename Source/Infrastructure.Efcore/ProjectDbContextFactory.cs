using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infra.Efcore
{
    public class ProjectDbContextFactory : IDesignTimeDbContextFactory<ProjectDbContext>
    {
        public static ProjectDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();

            var cnnBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "(localdb)\\mssqllocaldb;Database",
                InitialCatalog = "WebsiteProject",
                IntegratedSecurity = true,
            };

            optionsBuilder.UseSqlServer(cnnBuilder.ConnectionString);

            return new ProjectDbContext(optionsBuilder.Options);
        }

        public ProjectDbContext CreateDbContext(string[] args)
        {
            return CreateDbContext();
        }
    }
}
