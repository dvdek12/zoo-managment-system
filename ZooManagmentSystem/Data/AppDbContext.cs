using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.Models.Employee;
using ZooManagmentSystem.Models.Enums;
using ZooManagmentSystem.Models.Client;
using ZooManagmentSystem.Models.Report;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ZooManagmentSystem.Models;
using ZooManagmentSystem.Models.Dictionaries;

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
        public DbSet<AnimalAttributeModel> AnimalAttributes { get; set; }

        //Employees and Tasks
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<RoleModel> Roles { get; set; }

        //Reports
        public DbSet<ReportModel> Reports { get; set; }

        // Clients
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<TicketModel> Tickets { get; set; }
        public DbSet<TicketEntryTypeModel> TicketEntryTypes { get; set; }
        public DbSet<EntryTypeModel> EntryTypes { get; set; }

        // Dictionaries
        public DbSet<AnimalConditionModel> AnimalConditions { get; set; }
        public DbSet<AnimalTypeModel> AnimalType { get; set; }
        public DbSet<EnclosureTypeModel> EnclosureTypes { get; set; }
        public DbSet<FoodTypeModel> FoodTypes { get; set; }
        public DbSet<TaskCategoryModel> TaskCategories { get; set; }

        // Icon
        public DbSet<IconModel> Icons { get; set; }
        // Notifications
        public DbSet<NotificationModel> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TicketEntryTypeModel>()
                .HasOne(te => te.Ticket)
                .WithMany(t => t.EntryTypes)
                .HasForeignKey(te => te.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AnimalHistoryModel>()
                .HasOne(h => h.Animal)
                .WithMany(a => a.AnimalHistories)
                .HasForeignKey(h => h.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AttributeModel>()
                .HasOne(a => a.AnimalType)
                .WithMany()
                .HasForeignKey(a => a.AnimalTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeModel>()
                .HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TaskModel>()
                .HasOne(t => t.Role)
                .WithMany()
                .HasForeignKey(t => t.RoleId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }

}
