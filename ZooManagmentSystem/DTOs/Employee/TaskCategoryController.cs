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

namespace ZooManagmentSystem.Controllers.Employees
{
    [Route("taskCategory")]
    [ApiController]
    public class TaskCategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskCategoryController(AppDbContext context)
        {
            _context = context;
        }

        [Route("getAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskCategoryModel>>> GetTaskCategories()
        {
            return Ok(await _context.TaskCategories.ToListAsync());
        }


        [Route("getOne/{id}")]
        [HttpGet]
        public async Task<ActionResult<TaskCategoryModel>> GetTaskCategoryModel(int id)
        {
            var taskCategoryModel = await _context.TaskCategories.FindAsync(id);

            if (taskCategoryModel == null)
            {
                return NotFound();
            }

            return Ok(taskCategoryModel);
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
        public async Task<ActionResult<TaskCategoryModel>> PostTaskCategoryModel(TaskCategoryModel taskCategoryModel)
        {
            _context.TaskCategories.Add(taskCategoryModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Task category created successfully!" });
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTaskCategoryModel(int id)
        {
            var taskCategoryModel = await _context.TaskCategories.FindAsync(id);
            if (taskCategoryModel == null)
            {
                return NotFound();
            }

            _context.TaskCategories.Remove(taskCategoryModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Task category deleted successfully!" });
        }

        private bool TaskCategoryModelExists(int id)
        {
            return _context.TaskCategories.Any(e => e.id == id);
        }
    }
}
