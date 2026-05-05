using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models.Enums;

namespace ZooManagmentSystem.Controllers.Animals
{
    [Route("AnimalTypes")]
    [ApiController]
    public class AnimalTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnimalTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AnimalTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalTypeModel>>> GetAnimalType()
        {
            var animalTypes = await _context.AnimalType.ToListAsync();
            return Ok(animalTypes);
        }

        // GET: AnimalTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalTypeModel>> GetAnimalType(int id)
        {
            var animalType = await _context.AnimalType.FindAsync(id);

            if (animalType == null)
            {
                return NotFound();
            }

            return Ok(animalType);
        }

        // PUT: AnimalTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimalType(int id, AnimalTypeModel animalType)
        {
            if (id != animalType.id)
            {
                return BadRequest();
            }

            _context.Entry(animalType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Animal type edited successfully!" });
        }

        // POST: AnimalTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnimalTypeModel>> PostAnimalType(AnimalTypeModel animalType)
        {
            _context.AnimalType.Add(animalType);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Animal type created successfully!" });
        }

        // DELETE: AnimalTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimalType(int id)
        {
            var animalType = await _context.AnimalType.FindAsync(id);
            if (animalType == null)
            {
                return NotFound();
            }

            _context.AnimalType.Remove(animalType);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Animal type deleted successfully!" });
        }

        private bool AnimalTypeExists(int id)
        {
            return _context.AnimalType.Any(e => e.id == id);
        }
    }
}
