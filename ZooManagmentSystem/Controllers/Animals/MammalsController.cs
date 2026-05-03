using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.ViewModels;

namespace ZooManagmentSystem.Controllers.Animals
{
    [ApiController]
    [Route("mammals")]
    //[Authorize(Roles = ("Employee"))]
    public class MammalsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MammalsController(AppDbContext context)
        {
            _context = context;
        }

        [Route("getAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mammals = await _context.Mammals.ToListAsync();
            return Ok(mammals);
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create(MammalModel mammal)
        {
            try
            {
                _context.Mammals.Add(mammal);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Animal added successfuly!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var mammal = _context.Mammals.Find(id);
            if (mammal == null) return NotFound();
            return Ok(mammal);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, MammalModel mammal)
        {
            if (id != mammal.id) return BadRequest();

            _context.Mammals.Update(mammal);
            _context.SaveChanges();
            return Ok("Animal edited!");
        }

        [Route("delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var mammal = _context.Mammals.Find(id);
            if (mammal == null) return NotFound();

            _context.Mammals.Remove(mammal);
            _context.SaveChanges();
            return Ok("Animal deleted!");
        }
    }
}
