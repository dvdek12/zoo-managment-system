using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.DTOs.Animal;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;

namespace ZooManagmentSystem.Controllers.Animals
{
    [Route("animals")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class AnimalAttributeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnimalAttributeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Animal/Attributes/
        [HttpGet("{animalId}/attribute/{attributeId}")]
        public async Task<ActionResult<AnimalAttributeDto>> GetAnimalAttributes(int animalId, int attributeId)
        {
            var animalAttribute = await _context.AnimalAttributes.FindAsync(attributeId);
            if (animalAttribute == null)
            {
                return NotFound();
            }
            if(animalAttribute.AnimalId != animalId)
            {
                return BadRequest();
            }   

            var attributeInfo = await _context.Attributes.FindAsync(animalAttribute.AttributeId);
            string attributeName = attributeInfo != null ? attributeInfo.AttributeName : "Unknown Attribute";

            var animalAttributeDto = new AnimalAttributeDto
            {
                Id = animalAttribute.id,
                Name = attributeName,
                Value = animalAttribute.AttributeValue
            };

            return Ok(animalAttributeDto);
        }

        // GET: Animal/Attribute/5
        // Get all attributes for a specific animal
        [Route("{id}/attributes")]
        [HttpGet]

        public async Task<ActionResult<IEnumerable<AnimalAttributeDto>>> GetAnimalAttributeModel(int id)
        {
            var animalAttributeModel = await _context.AnimalAttributes.Where(t => t.AnimalId == id).ToListAsync();

            if (animalAttributeModel == null || animalAttributeModel.Count == 0)
            {
                return Ok(new List<AnimalAttributeDto>());
            }

            var attributeDetails = new List<AnimalAttributeDto>();

            foreach (var attribute in animalAttributeModel)
            {
                var attributeInfo = await _context.Attributes.FindAsync(attribute.AttributeId);
                if (attributeInfo != null)
                {
                    attributeDetails.Add(new AnimalAttributeDto
                    {
                        Id = attribute.id,
                        Name = attributeInfo.AttributeName,
                        Value = attribute.AttributeValue
                    });
                }
                else
                {
                    attributeDetails.Add(new AnimalAttributeDto
                    {
                        Id = attribute.AttributeId,
                        Name = "Unknown Attribute",
                        Value = attribute.AttributeValue
                    });
                }
            }
            return Ok(attributeDetails);
        }

        // PUT: Animal/5/Attribute/5
        [Authorize(Roles = "Manager")]
        [HttpPut("{animalId}/attribute/{attributeId}")]
        public async Task<IActionResult> PutAnimalAttributeModel(int animalId, int attributeId, AnimalAttributeUpdateDto animalAttributeModel)
        {
            if (attributeId != animalAttributeModel.Id)
            {
                return BadRequest();
            }

            var existingAnimalAttribute = await _context.AnimalAttributes.FindAsync(attributeId);
            if (existingAnimalAttribute == null)
            {
                return NotFound();
            }
            if(existingAnimalAttribute.AnimalId != animalId)
            {
                return BadRequest();
            }

            existingAnimalAttribute.AttributeValue = animalAttributeModel.Value;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalAttributeModelExists(attributeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Animal attribute updated successfully!" });
        }

        // POST: Animal/Attribute
        [Authorize(Roles = "Manager")]
        [Route("{animalId}/attribute")]
        [HttpPost]
        public async Task<ActionResult<AnimalAttributeModel>> PostAnimalAttributeModel(int animalId, AnimalAttributeCreateDto animalAttributeModel)
        {
            var newAnimalAttribute = new AnimalAttributeModel
            {
                AnimalId = animalId,
                AttributeId = animalAttributeModel.AttributeId,
                AttributeValue = animalAttributeModel.AttributeValue
            };

            _context.AnimalAttributes.Add(newAnimalAttribute);
            await _context.SaveChangesAsync();
            return Ok( new { message = "Animal attribute added successfully!" });    
        }

        // DELETE: Animal/5/Attribute/5
        [Authorize(Roles = "Manager")]
        [HttpDelete("{animalId}/attribute/{attributeId}")]
        public async Task<IActionResult> DeleteAnimalAttributeModel(int animalId, int attributeId)
        {
            var animalAttribute = await _context.AnimalAttributes.FindAsync(attributeId);
            if (animalAttribute == null)
            {
                return NotFound();
            }
            if (animalAttribute.AnimalId != animalId)
            {
                return BadRequest();
            }

            _context.AnimalAttributes.Remove(animalAttribute);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Animal attribute deleted successfully!" });
        }

        private bool AnimalAttributeModelExists(int id)
        {
            return _context.AnimalAttributes.Any(e => e.id == id);
        }
    }
}
