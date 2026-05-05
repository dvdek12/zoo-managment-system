using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.ViewModels;
using ZooManagmentSystem.DTOs;

namespace ZooManagmentSystem.Controllers.Animals
{
    [ApiController]
    [Route("animals")] 
    //[Authorize(Roles = ("Employee"))]
    public class AnimalsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnimalsController(AppDbContext context)
        {
            _context = context;
        }

        [Route("getAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var animals = await _context.Animals.ToListAsync();
            return Ok(animals);
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AnimalDto animal)
        {
            try
            {
                var newAnimal = new AnimalModel
                {
                    Name = animal.Name,
                    RaceName = animal.RaceName,
                    Description = animal.Description,
                    Origin = animal.Origin,
                    DateOfArrival = animal.DateOfArrival,
                    EnclosureId = animal.EnclosureId
                };

                _context.Animals.Add(newAnimal);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Animal added successfuly!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getOne/{id}")]
        public IActionResult GetOne(int id)
        {
            var animal = _context.Animals.Find(id);
            if (animal == null) return NotFound();
            return Ok(animal);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, AnimalModel animal)
        {
            if(id != animal.id) return BadRequest();

            _context.Animals.Update(animal);
            _context.SaveChanges();
            return Ok("Animal edited!");
        }

        [Route("delete")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var animal = _context.Animals.Find(id);
            if (animal == null) return NotFound();

            _context.Animals.Remove(animal);
            _context.SaveChanges();
            return Ok("Animal deleted!");
        }
    }
}
