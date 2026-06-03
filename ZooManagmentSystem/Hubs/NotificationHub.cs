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

            if (!string.IsNullOrEmpty(userEmail))
            {
                var roles = Context.User.Claims.Select(c => c.Value);
                if (roles.Contains("Employee"))
                {
                    string userId = Context.User.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value ?? "0";
                    await Groups.AddToGroupAsync(Context.ConnectionId, "E" + userId);
                    Console.WriteLine($"Połączono, userId: {userId}");
                }
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userEmail = Context.UserIdentifier;

            if (!string.IsNullOrEmpty(userEmail))
            {
                var roles = Context.User.Claims.Select(c => c.Value);
                if (roles.Contains("Employee"))
                {
                    string userId = Context.User.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value ?? "0";
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, "E" + userId);
                    Console.WriteLine($"rozłączono, userId: {userId}");
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
