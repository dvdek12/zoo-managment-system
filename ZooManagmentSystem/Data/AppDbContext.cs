using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.Models.Employee;
using ZooManagmentSystem.Models.Raport;
using ZooManagmentSystem.Models.Enums;
using ZooManagmentSystem.Models.Client;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ZooManagmentSystem.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Models.EnclosureModel> Enclosures { get; set; }
        public DbSet<Models.AnimalHistoryModel> AnimalHistories { get; set; }

        //Animals
        public DbSet<AnimalModel> Animals { get; set; }
        public DbSet<AttributeModel> Attributes { get; set; }

        //Employees and Tasks
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<RoleModel> Roles { get; set; }

        //Raports
        public DbSet<FeedingPlanRaportModel> FeedingPlanRaports { get; set; }
        public DbSet<AnimalHistoryRaportModel> AnimalHistoryRaports { get; set; }
        public DbSet<WorkFlowRaportModel> WorkFlowRaports { get; set; }

        // Clients
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<TicketModel> Tickets { get; set; }

        // Enums
        public DbSet<AnimalBreedModel> AnimalBreeds { get; set; }
        public DbSet<EnclosureTypeModel> EnclosureTypes { get; set; }
        public DbSet<FoodTypeModel> FoodTypes { get; set; }
        public DbSet<TaskCategoryModel> TaskCategories { get; set; }
        public DbSet<EntryTypeModel> EntryTypes { get; set; }



    }
}
