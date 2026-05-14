using Microsoft.AspNetCore.Mvc;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models;

namespace ZooManagmentSystem.Controllers
{
    [Route("Icon")]
    public class IconController : Controller
    {
        private readonly AppDbContext _context;

        public IconController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if(file == null || file.Length == 0) return BadRequest("No file uploaded.");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            var icon = new IconModel
            {
                Name = file.FileName,
                ContentType = file.ContentType,
                ImageData = stream.ToArray()
            };

            _context.Icons.Add(icon);
            await _context.SaveChangesAsync();

            return Ok(new {icon.id, message = "Icon uploaded successfully!"});

        }
    }
}
