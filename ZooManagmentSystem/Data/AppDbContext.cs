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
        public DbSet<Models.TaskModel> Tasks { get; set; }
        public DbSet<Models.EnclosureModel> Enclosures { get; set; }

        public DbSet<Models.AnimalHistoryModel> AnimalHistories { get; set; }

        public DbSet<Models.FeedingPlanRaportModel> FeedingPlanRaports { get; set; }
        public DbSet<Models.AnimalHistoryRaportModel> AnimalHistoryRaports { get; set; }
    }
}
