using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;
using ZooManagmentSystem.Data;

namespace ZooManagmentSystem.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly AppDbContext _db;
        public NotificationHub(AppDbContext db) => _db = db;
        public override async Task OnConnectedAsync()
        {
            var userEmail = Context.UserIdentifier;

            int userId = 0;
            if (!string.IsNullOrEmpty(userEmail))
            {
                var employeeUser = await _db.Employees.SingleOrDefaultAsync(u => u.Email == userEmail);
                if (employeeUser != null)
                {
                    userId = employeeUser.id;
                    await Groups.AddToGroupAsync(Context.ConnectionId, "E" + userId.ToString());
                }
                var clientUser = await _db.Clients.SingleOrDefaultAsync(u => u.Email == userEmail);
                if (clientUser != null)
                {
                    userId = clientUser.id;
                    await Groups.AddToGroupAsync(Context.ConnectionId, "C" + userId.ToString());
                }
            }
            Console.WriteLine($"Połączono, userId: {userId}");

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userEmail = Context.UserIdentifier;

            int userId = 0;
            if (!string.IsNullOrEmpty(userEmail))
            {
                var employeeUser = await _db.Employees.SingleOrDefaultAsync(u => u.Email == userEmail);
                if (employeeUser != null)
                {
                    userId = employeeUser.id;
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, "E" + userId.ToString());
                }
                var clientUser = await _db.Clients.SingleOrDefaultAsync(u => u.Email == userEmail);
                if (clientUser != null)
                {
                    userId = clientUser.id;
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, "C" + userId.ToString());
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
