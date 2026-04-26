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
    [Route("birds")]
    //[Authorize(Roles = ("Employee"))]
    public class BirdsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BirdsController(AppDbContext context)
        {
            _context = context;
        }

        [Route("getAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var birds = await _context.Birds.ToListAsync();
            return Ok(birds);
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create(BirdModel bird)
        {
            try
            {
                _context.Birds.Add(bird);
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
            var bird = _context.Birds.Find(id);
            if (bird == null) return NotFound();
            return Ok(bird);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, BirdModel bird)
        {
            if (id != bird.id) return BadRequest();

            _context.Birds.Update(bird);
            _context.SaveChanges();
            return Ok("Animal edited!");
        }

        [Route("delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var bird = _context.Birds.Find(id);
            if (bird == null) return NotFound();

            _context.Birds.Remove(bird);
            _context.SaveChanges();
            return Ok("Animal deleted!");
        }
    }
}
