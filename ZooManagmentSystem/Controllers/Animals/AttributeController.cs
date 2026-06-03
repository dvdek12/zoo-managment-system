using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.DTOs.Animal;
using ZooManagmentSystem.Models.Animal;

namespace ZooManagmentSystem.Controllers.Animals
{

    [Route("attributes")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class AttributeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AttributeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: attributes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttributeDto>>> GetAttributes()
        {
            var attributes = await _context.Attributes.ToListAsync();
            var attributeDtos = attributes.Select(a => new AttributeDto
            {
                Id = a.id,
                AttributeName = a.AttributeName,
                AnimalType = _context.AnimalType.Find(a.AnimalTypeId)?.AnimalTypeName ?? "Unknown",
                AttributeType = a.AttributeType
            }).ToList();
            return Ok(attributeDtos);
        }

        // GET: attributes/forType/5
        [Route("forType/{typeId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttributeDto>>> GetAttributesForType(int typeId)
        {
            var attributes = await _context.Attributes.Where(a => a.AnimalTypeId == typeId).ToListAsync();
            var attributeDtos = attributes.Select(a => new AttributeDto
            {
                Id = a.id,
                AttributeName = a.AttributeName,
                AnimalType = _context.AnimalType.Find(a.AnimalTypeId)?.AnimalTypeName ?? "Unknown",
                AttributeType = a.AttributeType
            }).ToList();
            return Ok(attributeDtos);
        }


        // GET: attributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttributeDto>> GetAttributeModel(int id)
        {
            var attributeModel = await _context.Attributes.FindAsync(id);

            if (attributeModel == null)
            {
                return NotFound();
            }
            
            var attributeDto = new AttributeDto
            {
                Id = attributeModel.id,
                AttributeName = attributeModel.AttributeName,
                AnimalType = _context.AnimalType.Find(attributeModel.AnimalTypeId)?.AnimalTypeName ?? "Unknown",
                AttributeType = attributeModel.AttributeType
            };

            return Ok(attributeDto);
        }

        // PUT: attributes/5
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttributeModel(int id, AttributeUpdateDto attributeDto)
        {
            if (id != attributeDto.Id)
            {
                return BadRequest();
            }

            var attributeModel = await _context.Attributes.FindAsync(id);
            if (attributeModel == null)
            {
                return NotFound();
            }

            attributeModel.AttributeName = attributeDto.AttributeName;
            attributeModel.AnimalTypeId = attributeDto.AnimalTypeId;
            attributeModel.AttributeType = attributeDto.AttributeType;

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
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<ActionResult<AttributeModel>> PostAttributeModel(AttributeCreateDto attributeModel)
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
        [Authorize(Roles = "Manager")]
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
