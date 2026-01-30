using Microsoft.EntityFrameworkCore;
using Domain;
using System.Collections.Generic;

namespace Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; } = null!;
    }
}
