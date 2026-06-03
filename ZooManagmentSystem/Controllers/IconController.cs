using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models;

namespace ZooManagmentSystem.Controllers
{
    [Route("Icon")]
    [Authorize(Roles = "Employee")]
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
            if (file == null || file.Length == 0) return BadRequest("No file uploaded.");

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

            return Ok(new { icon.id, message = "Icon uploaded successfully!" });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIcon(int id, IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("No file uploaded.");
            
            var icon = await _context.Icons.FindAsync(id);

            if (icon == null) return NotFound($"Ikonka o ID {id} nie istnieje.");
            
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            icon.ImageData = ms.ToArray();
            icon.ContentType = file.ContentType;
            icon.Name = file.FileName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Błąd podczas aktualizacji bazy danych.");
            }

            return Ok(new { message = "Ikonka została zaktualizowana pomyślnie" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIcon(int id)
        {
            var icon = await _context.Icons.FindAsync(id);
            if (icon == null) return NotFound();
            return File(icon.ImageData, icon.ContentType);
        }
    }
}
