using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models;
using ZooManagmentSystem.Models.Enums;
using ZooManagmentSystem.DTOs;

namespace ZooManagmentSystem.Controllers
{
    [Route("enclosure")]
    [ApiController]
    public class EnclosuresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnclosuresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Enclosures
        [Route("getAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnclosureModel>>> GetEnclosures()
        {
            return Ok(await _context.Enclosures.ToListAsync());
        }

        // GET: api/Enclosures/5
        [Route("getOne/{id}")]
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

        // PUT: api/Enclosures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("update/{id}")]
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

        // POST: api/Enclosures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("new")]
        [HttpPost]
        public async Task<ActionResult<EnclosureModel>> PostEnclosureModel(EnclosureDto enclosureModel)
        {
            var newEnclosure = new EnclosureModel
            {
                Name = enclosureModel.Name,
                Description = enclosureModel.Description,
                TypeId = enclosureModel.TypeId
            };

            _context.Enclosures.Add(newEnclosure);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Created new enclosure." });
        }

        // DELETE: api/Enclosures/5
        [Route("delete/{id}")]
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

        [Route("type/getAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnclosureTypeModel>>> GetEnclosureTypes()
        {
            return Ok(await _context.EnclosureTypes.ToListAsync());
        }

        [Route("type/new")]
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

        [Route("type/update/{id}")]
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


        [Route("type/delete/{id}")]
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
