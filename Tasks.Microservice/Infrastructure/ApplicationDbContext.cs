using Microsoft.EntityFrameworkCore;
using Tasks.Microservice.Domain;

namespace Tasks.Microservice.Infrastructure
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<TaskModel> Tasks { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public ApplicationDbContext()
        {
           // Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=taskmanagerdb;Username=postgres;Password=malik");
        }
    }
}
