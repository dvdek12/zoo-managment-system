using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.DTOs;
using ZooManagmentSystem.Models;
using ZooManagmentSystem.Models.Enums;

namespace ZooManagmentSystem.Controllers
{
    [Route("enclosure")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class EnclosuresController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IValidator<EnclosureDto> _validator;

        public EnclosuresController(AppDbContext context, IValidator<EnclosureDto> validator)
        {
            _context = context;
            _validator = validator;
        }

        // GET: enclosure
        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnclosureModel>>> GetEnclosures()
        {
            var enclosures = await _context.Enclosures
                .Include(e => e.Type)
                .Include(e => e.Animals)
                .ToListAsync();
            return Ok(enclosures);
        }

        // GET: enclosure/5
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<EnclosureModel>> GetEnclosureModel(int id)
        {
            var enclosureModel = await _context.Enclosures.FindAsync(id);

            if (enclosureModel == null)
            {
                return NotFound();
            }

            return Ok(enclosureModel);
        }

        // PUT: enclosure/5
        [Authorize(Roles = "Manager")]
        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> PutEnclosureModel(int id, EnclosureUpdateDto enclosureModel)
        {
            var existingEnclosure = await _context.Enclosures.FindAsync(id);
            if (existingEnclosure == null)
            {
                return NotFound();
            }

            if (enclosureModel.Name != null)
            {
                existingEnclosure.Name = enclosureModel.Name;
            }

            if (enclosureModel.MapKey != null)
            {
                existingEnclosure.MapKey = enclosureModel.MapKey;
            }

            if (enclosureModel.Description != null)
            {
                existingEnclosure.Description = enclosureModel.Description;
            }

            if (enclosureModel.TypeId.HasValue)
            {
                existingEnclosure.TypeId = enclosureModel.TypeId.Value;
            }

            _context.Entry(existingEnclosure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnclosureModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Updated enclosure." });
        }

        // POST: enclosure
        [Authorize(Roles = "Manager")]
        [Route("")]
        [HttpPost]
        public async Task<ActionResult<EnclosureModel>> PostEnclosureModel(EnclosureDto enclosureModel)
        {
            var validation = await _validator.ValidateAsync(enclosureModel);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var newEnclosure = new EnclosureModel
            {
                Name = enclosureModel.Name,
                Description = enclosureModel.Description,
                TypeId = enclosureModel.TypeId,
                MapKey = enclosureModel.MapKey,
            };

            _context.Enclosures.Add(newEnclosure);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Created new enclosure." });
        }

        // DELETE: enclosure/5
        [Authorize(Roles = "Manager")]
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEnclosureModel(int id)
        {
            var enclosureModel = await _context.Enclosures.FindAsync(id);
            if (enclosureModel == null)
            {
                return NotFound();
            }

            _context.Enclosures.Remove(enclosureModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted enclosure." });
        }

        [Route("type")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnclosureTypeModel>>> GetEnclosureTypes()
        {
            return Ok(await _context.EnclosureTypes.ToListAsync());
        }

        [Authorize(Roles = "Manager")]
        [Route("type")]
        [HttpPost]
        public async Task<ActionResult<EnclosureTypeModel>> PostEnclosureTypeModel(EnclosureTypeModel enclosureTypeModel)
        {
            try
            {
                _context.EnclosureTypes.Add(enclosureTypeModel);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Created new enclosure type." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error creating enclosure type.", error = ex.Message });
            }
        }

        [Authorize(Roles = "Manager")]
        [Route("type/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutEnclosureTypeModel(int id, EnclosureTypeModel enclosureModel)
        {
            var existingType = await _context.EnclosureTypes.FindAsync(id);
            if (existingType == null)
            {
                return NotFound();
            }
            existingType.TypeName = enclosureModel.TypeName;
            _context.Entry(existingType).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnclosureModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { message = "Updated enclosure type." });
        }

        [Authorize(Roles = "Manager")]
        [Route("{EnclosureId}/assignAnimal/{AnimalId}")]
        [HttpPut]
        public async Task<IActionResult> AssignAnimal(int EnclosureId, int AnimalId)
        {
            var enclosure = await _context.Enclosures
                .FirstOrDefaultAsync(e => e.id == EnclosureId);
            if (enclosure == null)
                return NotFound(new { message = "Enclosure not found." });

            var animal = await _context.Animals
                .FirstOrDefaultAsync(a => a.id == AnimalId);

            if (animal == null)
                return NotFound(new { message = "Animal not found." });

            animal.EnclosureId = EnclosureId;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Animal has been assigned successfully." });
        }

        [Authorize(Roles = "Manager")]
        [Route("{EnclosureId}/assignAnimal/{AnimalId}")]
        [HttpDelete]
        public async Task<IActionResult> UnassignAnimal(int EnclosureId, int AnimalId)
        {
            var animal = await _context.Animals
                .FirstOrDefaultAsync(a => a.id == AnimalId && a.EnclosureId == EnclosureId);

            if (animal == null)
                return NotFound(new { message = "Zwierzę nie jest przypisane do tego wybiegu." });

            animal.EnclosureId = null;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Zwierzę zostało odpięte od wybiegu." });
        }

        [Authorize(Roles = "Manager")]
        [Route("type/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEnclosureTypeModel(int id)
        {
            var enclosureTypeModel = await _context.EnclosureTypes.FindAsync(id);
            if (enclosureTypeModel == null)
            {
                return NotFound();
            }

            _context.EnclosureTypes.Remove(enclosureTypeModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted enclosure type." });
        }


        private bool EnclosureModelExists(int id)
        {
            return _context.Enclosures.Any(e => e.id == id);
        }
    }
}
