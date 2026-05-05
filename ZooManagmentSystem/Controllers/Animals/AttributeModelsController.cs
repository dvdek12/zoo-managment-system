using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.DTOs;

namespace ZooManagmentSystem.Controllers.Animals
{
    [Route("attributes")]
    [ApiController]
    public class AttributeModelsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AttributeModelsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: attributes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttributeModel>>> GetAttributes()
        {
            return Ok(await _context.Attributes.ToListAsync());
        }

        // GET: attributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttributeModel>> GetAttributeModel(int id)
        {
            var attributeModel = await _context.Attributes.FindAsync(id);

            if (attributeModel == null)
            {
                return NotFound();
            }
            return Ok(attributeModel);
        }

        // PUT: attributes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttributeModel(int id, AttributeModel attributeModel)
        {
            if (id != attributeModel.id)
            {
                return BadRequest();
            }

            _context.Entry(attributeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttributeModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Attribute edited successfully!" });
        }

        // POST: attributes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AttributeModel>> PostAttributeModel(AttributeDto attributeModel)
        {
            AttributeModel newAttributeModel = new AttributeModel
            {
                AttributeName = attributeModel.AttributeName,
                AnimalTypeId = attributeModel.AnimalTypeId,
                AttributeType = attributeModel.AttributeType
            };

            _context.Attributes.Add(newAttributeModel);
            await _context.SaveChangesAsync();

        return Ok(new { message = "Attribute created successfully!" });
        }

        // DELETE: attributes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttributeModel(int id)
        {
            var attributeModel = await _context.Attributes.FindAsync(id);
            if (attributeModel == null)
            {
                return NotFound();
            }

            _context.Attributes.Remove(attributeModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Attribute deleted successfully!" });
        }

        private bool AttributeModelExists(int id)
        {
            return _context.Attributes.Any(e => e.id == id);
        }
    }
}
