using Microsoft.EntityFrameworkCore;

namespace ZooManagmentSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Models.AnimalModel> Animals { get; set; }
        public DbSet<Models.BirdModel> Birds { get; set; }
        public DbSet<Models.Employee> Employees { get; set; }
        public DbSet<Models.MammalModel> Mammals { get; set; }
    }
}
