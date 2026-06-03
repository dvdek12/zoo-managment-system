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
using Microsoft.AspNetCore.Authorization;

namespace ZooManagmentSystem.Controllers.Employees
{
    [Route("employee")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _context.Employees.Select(e => new EmployeeDto
            {
                Id = e.id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                BirthDay = e.BirthDay,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                SupervisorId = e.SupervisorId,
                RoleId = e.RoleId,
                IconId = e.IconId
            }).ToListAsync();
            return Ok(employees);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeModel(int id)
        {
            var employeeModel = await _context.Employees.FindAsync(id);

            if (employeeModel == null)
            {
                return NotFound();
            }
            var employeeDto = new EmployeeDto
            {
                Id = employeeModel.id,
                FirstName = employeeModel.FirstName,
                LastName = employeeModel.LastName,
                BirthDay = employeeModel.BirthDay,
                Email = employeeModel.Email,
                PhoneNumber = employeeModel.PhoneNumber,
                SupervisorId = employeeModel.SupervisorId,
                RoleId = employeeModel.RoleId,
                IconId = employeeModel.IconId
            };

            return Ok(employeeDto);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeModel(int id, UpdateEmployeeDto employeeModel)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id").Value ?? "0");
            if (id != employeeModel.Id || id != userId)
            {
                return BadRequest();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            if (employeeModel.FirstName != null)
                employee.FirstName = employeeModel.FirstName;
            if (employeeModel.LastName != null)
                employee.LastName = employeeModel.LastName;
            if (employeeModel.PhoneNumber != null)
                employee.PhoneNumber = employeeModel.PhoneNumber;
            if (employeeModel.BirthDay != null)
                employee.BirthDay = (DateTime)employeeModel.BirthDay;

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Profile edited!" });
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("{id}/asManager")]
        public async Task<IActionResult> PutEmployeeAsManagerModel(int id, UpdateEmployeeManagerDto employeeModel)
        {
            if (id != employeeModel.Id)
            {
                return BadRequest();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            if (employeeModel.FirstName != null)
                employee.FirstName = employeeModel.FirstName;
            if (employeeModel.LastName != null)
                employee.LastName = employeeModel.LastName;
            if (employeeModel.PhoneNumber != null)
                employee.PhoneNumber = employeeModel.PhoneNumber;
            if (employeeModel.BirthDay != null)
                employee.BirthDay = (DateTime)employeeModel.BirthDay;
            if (employeeModel.Email != null)
                employee.Email = employeeModel.Email;
            if (employeeModel.RoleId != null)
                employee.RoleId = employeeModel.RoleId;
            if (employeeModel.SupervisorId != null)
                employee.SupervisorId = employeeModel.SupervisorId;

                _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Profile edited!" });
        }

        [HttpGet]
        [Route("{id}/role")]
        public IActionResult GetEmployeeRole(int id)
        {
            var roleId = _context.Employees.Where(e => e.id == id).Select(e => e.RoleId).FirstOrDefault();
            var role = _context.Roles.Where(r => r.id == roleId).Select(r => new
            {
                r.id,
                r.Name,
                r.Description,
                r.IsManagerial
            }).FirstOrDefault();
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpGet]
        [Route("roles")]
        public IActionResult GetAllRoles()
        {
            var roles = _context.Roles.Select(r => new
            {
                r.id,
                r.Name,
                r.Description,
                r.IsManagerial
            }).ToList();
            return Ok(roles);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [Route("roles")]
        public IActionResult AddNewRole([FromBody] RoleDto roleDto)
        {
            var isExists = _context.Roles.Any(r => r.Name == roleDto.Name);

            if (!isExists)
            {
                var newRole = new RoleModel
                {
                    Name = roleDto.Name,
                    Description = roleDto.Description,
                    IsManagerial = roleDto.IsManagerial
                };

                _context.Roles.Add(newRole);
                _context.SaveChanges();
                return Ok(new { message = "New role added successfuly!" });
            }

            return BadRequest();

        }

        [Authorize(Roles = "Manager")]
        [HttpDelete]
        [Route("roles/{id}")]
        public IActionResult DeleteRole(int id)
        {
            var role = _context.Roles.Find(id);
            if (role == null) return NotFound();
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return Ok(new { message = "Role deleted successfuly!" });
        }

        //First we need to understand how to remove claims and identity
        /* DELETE: api/Employees/5 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeModel(int id)
        {
            var employeeModel = await _context.Employees.FindAsync(id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employeeModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */

        private bool EmployeeModelExists(int id)
        {
            return _context.Employees.Any(e => e.id == id);
        }
    }
}
