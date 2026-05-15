using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models.Employee;
using ZooManagmentSystem.DTOs.Employee;

namespace ZooManagmentSystem.Controllers.Employees
{
    [Route("task")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [Route("getAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks()
        {
            return Ok(await _context.Tasks.ToListAsync());
        }

        // GET: api/Tasks/5
        [Route("getOne/{id}")]
        [HttpGet]
        public async Task<ActionResult<TaskModel>> GetTaskModel(int id)
        {
            var taskModel = await _context.Tasks.FindAsync(id);

            if (taskModel == null)
            {
                return NotFound();
            }

            return Ok(taskModel);
        }

        // GET: api/Tasks/5
        [Route("getForEmployee/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksForEmployee(int id)
        {
            var employeeTasks = await _context.Tasks.Where(t => t.AssignedEmployeeId == id).ToListAsync();

            if (employeeTasks == null)
            {
                return NotFound();
            }

            return Ok(employeeTasks);
        }

        [Route("getForRole/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksForRole(int id)
        {
            var roleTasks = await _context.Tasks.Where(t => t.RoleId == id).ToListAsync();
            if (roleTasks == null)
            {
                return NotFound();
            }

            return Ok(roleTasks);
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutTaskModel(int id, TaskUpdateDto taskModel)
        {

             var existingTask = await _context.Tasks.FindAsync(id);
            if(existingTask == null)
            {
                return NotFound();
            }

            existingTask.Name = taskModel.Name ?? existingTask.Name;
            existingTask.Description = taskModel.Description ?? existingTask.Description;
            existingTask.CategoryId = taskModel.CategoryId ?? existingTask.CategoryId;
            existingTask.AssignedEmployeeId = taskModel.AssignedEmployeeId ?? existingTask.AssignedEmployeeId;
            existingTask.RoleId = taskModel.RoleId ?? existingTask.RoleId;
            existingTask.EnclosureId = taskModel.EnclosureId ?? existingTask.EnclosureId;
            existingTask.AnimalId = taskModel.AnimalId ?? existingTask.AnimalId;
            if(taskModel.Deadline != null)
                existingTask.Deadline = (DateTime)taskModel.Deadline;
            if(taskModel.IsCompleted != null)
                existingTask.IsCompleted = (bool)taskModel.IsCompleted;
            

            _context.Entry(existingTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Task edited." });
        }

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("new")]
        [HttpPost]
        public async Task<ActionResult<TaskModel>> PostTaskModel(TaskDto taskModel)
        {

            TaskModel newTask = new TaskModel {
                Name = taskModel.Name,
                Description = taskModel.Description,
                Deadline = taskModel.Deadline,
                IsCompleted = taskModel.IsCompleted,
                CategoryId = taskModel.CategoryId,
                AssignedEmployeeId = taskModel.AssignedEmployeeId,
                RoleId = taskModel.RoleId,
                EnclosureId = taskModel.EnclosureId,
                AnimalId = taskModel.AnimalId
            }; 

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            return Ok( new { message = "Created task." });
        }

        // DELETE: api/Tasks/5
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTaskModel(int id)
        {
            var taskModel = await _context.Tasks.FindAsync(id);
            if (taskModel == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(taskModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted task." });
        }

        private bool TaskModelExists(int id)
        {
            return _context.Tasks.Any(e => e.id == id);
        }
    }
}
