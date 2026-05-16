using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models.Dictionaries;

namespace ZooManagmentSystem.Controllers.Animals
{
    [Route("animalCondition")]
    [ApiController]
    public class AnimalConditionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnimalConditionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AnimalConditions
        [Route("getAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalConditionModel>>> GetAnimalConditions()
        {
            return Ok(await _context.AnimalConditions.ToListAsync());
        }

        // GET: api/AnimalConditions/5
        [Route("getOne/{id}")]
        [HttpGet]
        public async Task<ActionResult<AnimalConditionModel>> GetAnimalConditionModel(int id)
        {
            var animalConditionModel = await _context.AnimalConditions.FindAsync(id);

            if (animalConditionModel == null)
            {
                return NotFound();
            }

            return Ok(animalConditionModel);
        }

        // PUT: api/AnimalConditions/5
        /* not needed for now, but can be implemented if necessary
         * To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
         *
        [Route("update/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutAnimalConditionModel(int id, AnimalConditionModel animalConditionModel)
        {
            if (id != animalConditionModel.id)
            {
                return BadRequest();
            }

            _context.Entry(animalConditionModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalConditionModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Animal condition updated successfully!" });
        }
        */

        // POST: api/AnimalConditions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("new")]
        [HttpPost]
        public async Task<ActionResult<AnimalConditionModel>> PostAnimalConditionModel(AnimalConditionModel animalConditionModel)
        {
            _context.AnimalConditions.Add(animalConditionModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Animal condition created successfully!" });
        }

        // DELETE: api/AnimalConditions/5
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAnimalConditionModel(int id)
        {
            var animalConditionModel = await _context.AnimalConditions.FindAsync(id);
            if (animalConditionModel == null)
            {
                return NotFound();
            }

            _context.AnimalConditions.Remove(animalConditionModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Animal condition deleted successfully!" });
        }

        private bool AnimalConditionModelExists(int id)
        {
            return _context.AnimalConditions.Any(e => e.id == id);
        }
    }
}
