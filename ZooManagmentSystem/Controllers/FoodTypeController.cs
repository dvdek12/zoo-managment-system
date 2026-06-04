using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models.Dictionaries;
using ZooManagmentSystem.Models.Enums;

namespace ZooManagmentSystem.Controllers.Animals
{
    [Route("foodType")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class FoodTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FoodTypeController(AppDbContext context)
        {
            _context = context;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodTypeModel>>> GetFoodTypes()
        {
            return Ok(await _context.FoodTypes.ToListAsync());
        }


        [Route("{id}")]
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

        [Authorize(Roles = "Manager")]
        [Route("")]
        [HttpPost]
        public async Task<ActionResult<FoodTypeModel>> PostFoodTypeModel(FoodTypeModel foodTypeModel)
        {
            _context.FoodTypes.Add(foodTypeModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Food type created successfully!" });
        }

        [Authorize(Roles = "Manager")]
        [Route("{id}")]
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
