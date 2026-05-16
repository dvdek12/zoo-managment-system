using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models.Dictionaries;
using ZooManagmentSystem.Models.Enums;

namespace ZooManagmentSystem.Controllers.Animals
{
    [Route("foodType")]
    [ApiController]
    public class FoodTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FoodTypeController(AppDbContext context)
        {
            _context = context;
        }

        [Route("getAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodTypeModel>>> GetFoodTypes()
        {
            return Ok(await _context.FoodTypes.ToListAsync());
        }


        [Route("getOne/{id}")]
        [HttpGet]
        public async Task<ActionResult<FoodTypeModel>> GetFoodTypeModel(int id)
        {
            var foodTypeModel = await _context.FoodTypes.FindAsync(id);

            if (foodTypeModel == null)
            {
                return NotFound();
            }

            return Ok(foodTypeModel);
        }

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

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("new")]
        [HttpPost]
        public async Task<ActionResult<FoodTypeModel>> PostFoodTypeModel(FoodTypeModel foodTypeModel)
        {
            _context.FoodTypes.Add(foodTypeModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Food type created successfully!" });
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteFoodTypeModel(int id)
        {
            var foodTypeModel = await _context.FoodTypes.FindAsync(id);
            if (foodTypeModel == null)
            {
                return NotFound();
            }

            _context.FoodTypes.Remove(foodTypeModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Food type deleted successfully!" });
        }

        private bool FoodTypeModelExists(int id)
        {
            return _context.FoodTypes.Any(e => e.id == id);
        }
    }
}
