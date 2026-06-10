using FluentValidation;
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
using ZooManagmentSystem.Services;
using Microsoft.AspNetCore.Authorization;

namespace ZooManagmentSystem.Controllers.Employees
{
    [Route("task")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly NotificationService _notification;
        private readonly IValidator<TaskCreateDto> _validator;

        public TasksController(AppDbContext context, NotificationService notification, IValidator<TaskCreateDto> validator)
        {
            _context = context;
            _notification = notification;
            _validator = validator;
        }

        // GET: task
        [Authorize(Roles = "Manager")]
        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            var taskDtos = tasks.Select(t => new TaskDto
            {
                Id = t.id,
                Name = t.Name,
                Description = t.Description,
                Deadline = t.Deadline,
                IsCompleted = t.IsCompleted,
                CategoryId = t.CategoryId,
                AssignedEmployeeId = t.AssignedEmployeeId,
                RoleId = t.RoleId,
                EnclosureId = t.EnclosureId,
                AnimalId = t.AnimalId,
                NotificationSent = t.NotificationSent
            }).ToList();
            return Ok(taskDtos);
        }

        // GET: task/5
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<TaskDto>> GetTaskModel(int id)
        {
            var taskModel = await _context.Tasks.FindAsync(id);
            if (taskModel == null)
            {
                return NotFound();
            }

            var taskDto = new TaskDto
            {
                Id = taskModel.id,
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
            return Ok(taskDto);
        }

        // GET: api/Tasks/5
        [Route("forEmployee/{employeeId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksForEmployee(int employeeId)
        {
            var employeeTasks = await _context.Tasks.Where(t => t.AssignedEmployeeId == employeeId).ToListAsync();
            if (employeeTasks == null)
            {
                return NotFound();
            }

            var taskDtos = employeeTasks.Select(t => new TaskDto
            {
                Id = t.id,
                Name = t.Name,
                Description = t.Description,
                Deadline = t.Deadline,
                IsCompleted = t.IsCompleted,
                CategoryId = t.CategoryId,
                AssignedEmployeeId = t.AssignedEmployeeId,
                RoleId = t.RoleId,
                EnclosureId = t.EnclosureId,
                AnimalId = t.AnimalId
            }).ToList();

            return Ok(taskDtos);
        }

        [Authorize(Roles = "Manager")]
        [Route("forRole/{roleId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksForRole(int roleId)
        {
            var roleTasks = await _context.Tasks.Where(t => t.RoleId == roleId).ToListAsync();
            if (roleTasks == null)
            {
                return NotFound();
            }

            var taskDtos = roleTasks.Select(t => new TaskDto
                {
                    Id = t.id,
                    Name = t.Name,
                    Description = t.Description,
                    Deadline = t.Deadline,
                    IsCompleted = t.IsCompleted,
                    CategoryId = t.CategoryId,
                    AssignedEmployeeId = t.AssignedEmployeeId,
                    RoleId = t.RoleId,
                    EnclosureId = t.EnclosureId,
                    AnimalId = t.AnimalId
                }).ToList();

            return Ok(taskDtos);
        }

        // PUT: task/5
        [HttpPut("{id}")]
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

            // Notify employee if assigned
            if (taskModel.AssignedEmployeeId != null)
                await _notification.SendToEmployeeAsync(taskModel.AssignedEmployeeId ?? 0,
                    "New Task!", "You have new task to do: " + existingTask.Name);
                existingTask.NotificationSent = false;

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

        // POST: task
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<ActionResult<TaskModel>> PostTaskModel(TaskCreateDto taskModel)
        {
            var validation = await _validator.ValidateAsync(taskModel);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

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

            await _notification.SendToEmployeeAsync(taskModel.AssignedEmployeeId ?? 0,
                "New Task!", "You have new task to do: " + newTask.Name);

            return Ok( new { message = "Created task." });
        }

        // DELETE: task/5
        [Authorize(Roles = "Manager")]
        [Route("{id}")]
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
