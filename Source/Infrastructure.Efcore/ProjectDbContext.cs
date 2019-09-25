using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infra.Efcore
{
    public class ProjectDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public ProjectDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
