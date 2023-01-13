using Microsoft.EntityFrameworkCore;
using UniversityService.Models;

namespace UniversityService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<University> Universitys { get; set; }
    }
}