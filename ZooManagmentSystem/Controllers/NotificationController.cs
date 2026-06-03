using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models;
using ZooManagmentSystem.Models.Enums;

namespace ZooManagmentSystem.Controllers
{
    [Route("notifications")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly AppDbContext _context;

        public NotificationController(AppDbContext context)
        {
            _context = context;
        }

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

        [Route("user/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationModel>>> GetNotificationsForUser(int id)
        {
            var notificationModel = await _context.Notifications.Where(n => n.UserId == id).ToListAsync();

            if (notificationModel == null || !notificationModel.Any())
            {
                return NotFound();
            }

            return Ok(notificationModel);
        }
    }
}
