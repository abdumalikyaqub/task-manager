using Microsoft.EntityFrameworkCore;

namespace Users.Microservice.Models
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usermanagerdb;Username=postgres;Password=malik");
        }
    }
}
