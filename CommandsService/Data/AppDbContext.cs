using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt ) : base(opt)
        {
            
        }

        public DbSet<University> Universitys { get; set; }
        public DbSet<Command> Commands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<University>()
                .HasMany(p => p.Commands)
                .WithOne(p=> p.University!)
                .HasForeignKey(p => p.UniversityId);

            modelBuilder
                .Entity<Command>()
                .HasOne(p => p.University)
                .WithMany(p => p.Commands)
                .HasForeignKey(p =>p.UniversityId);
        }
    }
}