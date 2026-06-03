using Microsoft.AspNetCore.SignalR;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Hubs;
using ZooManagmentSystem.Models;

namespace ZooManagmentSystem.Services
{
    public class NotificationService
    {
        private readonly AppDbContext _db;
        private readonly IHubContext<NotificationHub> _hub;

        public NotificationService(AppDbContext db, IHubContext<NotificationHub> hub)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _hub = hub ?? throw new ArgumentNullException(nameof(hub));
        }

        public async Task SendToEmployeeAsync(int userId, string title, string message)
        {
            var notification = new NotificationModel
            {
                Title = title,
                Message = message,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };
            _db.Notifications.Add(notification);
            await _db.SaveChangesAsync();

            await _hub.Clients.Group("E" + userId.ToString())
                .SendAsync("ReceiveNotification", new
                {
                    notification.id,
                    notification.Title,
                    notification.Message,
                    notification.CreatedAt
                });
        }
    }
}
