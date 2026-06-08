using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models;
using ZooManagmentSystem.Models.Enums;

namespace ZooManagmentSystem.Controllers
{
    [Route("notifications")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class NotificationController : Controller
    {
        private readonly AppDbContext _context;

        public NotificationController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Manager")]
        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationModel>>> GetAllNotifications()
        {
            return Ok(await _context.Notifications.ToListAsync());
        }


        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<NotificationModel>> GetNotification(int id)
        {
            var notificationModel = await _context.Notifications.FindAsync(id);

            if (notificationModel == null)
            {
                return NotFound();
            }

            return Ok(notificationModel);
        }

        [Route("forEmployee/")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationModel>>> GetNotificationsForUser()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value ?? "0");

            var notificationModel = await _context.Notifications.Where(n => n.UserId == id).ToListAsync();

            if (notificationModel == null || !notificationModel.Any())
            {
                return NotFound();
            }

            return Ok(notificationModel);
        }

        [Route("{id}/read")]
        [HttpPut]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            int employeeId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value ?? "0");

            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null || notification.UserId != employeeId)
                return NotFound();

            notification.IsRead = true;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Route("read-all")]
        [HttpPut]
        public async Task<IActionResult> MarkAllAsRead()
        {
            int employeeId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value ?? "0");

            var unread = await _context.Notifications
                .Where(n => n.UserId == employeeId && !n.IsRead)
                .ToListAsync();

            unread.ForEach(n => n.IsRead = true);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
