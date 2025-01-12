using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace ClassroomManagement.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

            var builder = new DbContextOptionsBuilder<RepositoryContext>()
        
           //.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("ClassroomManagement"));
           .UseSqlite(configuration.GetConnectionString("sqliteConnection"), b => b.MigrationsAssembly("ClassroomManagement"));
           
            return new RepositoryContext(builder.Options);
        }
    }
}