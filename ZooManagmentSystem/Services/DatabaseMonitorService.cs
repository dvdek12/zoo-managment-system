using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Hubs;
using ZooManagmentSystem.Models.Employee;

namespace ZooManagmentSystem.Services
{
    public class DatabaseMonitorService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<NotificationHub> _hubContext;

        public DatabaseMonitorService(IServiceScopeFactory scopeFactory, IHubContext<NotificationHub> hubContext)
        {
            _scopeFactory = scopeFactory;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckDatesAndNotify();
                await DeleteExpiredNotifications();

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); 
            }
        }

        private async Task CheckDatesAndNotify()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var notification = scope.ServiceProvider.GetRequiredService<NotificationService>();

            var warningDate = DateTime.Now.AddHours(1); // ile przed deadline

            Console.WriteLine($"Checking for tasks with deadlines before {warningDate}...");
            Console.WriteLine($"time now: {DateTime.Now}");
            var expiringTasks = await context.Tasks
                .Where(r => r.Deadline <= warningDate && r.Deadline >= DateTime.Now && !r.IsCompleted)
                .ToListAsync();
            Console.WriteLine($"Founded tasks: {expiringTasks.Count}.");


            foreach (var task in expiringTasks)
            {
                if (task.NotificationSent)
                {
                    Console.WriteLine($"Task '{task.Name}' already has a notification sent. Skipping...");
                    continue;
                }
                else
                {
                    Console.WriteLine($"Task '{task.Name}' is expiring at {task.Deadline}. Sending notification...");
                    await notification.SendToEmployeeAsync(task.AssignedEmployeeId ?? 0,
                        "Deadline for task is close!", "The deadline for your task is approaching: " + task.Name);
                    task.NotificationSent = true;
                    context.Entry(task).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
        }

        private async Task DeleteExpiredNotifications()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var cutoffDate = DateTime.Now.AddDays(-7);

            var toDelete = await context.Notifications
                .Where(r => r.CreatedAt <= cutoffDate)
                .ToListAsync();

            if (toDelete.Any())
            {
                Console.WriteLine($"Deleting {toDelete.Count} expired notifications.");
                context.Notifications.RemoveRange(toDelete);
                await context.SaveChangesAsync();
            }
        }
    }
}
