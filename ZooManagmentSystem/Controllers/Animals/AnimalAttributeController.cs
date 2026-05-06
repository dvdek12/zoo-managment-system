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

namespace ZooManagmentSystem.Controllers.Animals
{
    [Route("Animal/Attribute")]
    [ApiController]
    public class AnimalAttributeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnimalAttributeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Animal/Attributes/
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AnimalAttributeModel>>> GetAnimalAttributes()
        {
            var animalAttributes = await _context.AnimalAttributes.ToListAsync();
            return Ok(await _context.AnimalAttributes.ToListAsync());
        }

        // GET: Animal/Attribute/5
        // Get all attributes for a specific animal
        [Route("/forAnimal/{id}")]
        [HttpGet]
        public async Task<ActionResult<AnimalAttributeModel>> GetAnimalAttributeModel(int id)
        {
            var animalAttributeModel = await _context.AnimalAttributes.Where(t => t.AnimalId == id).ToListAsync();

            if (animalAttributeModel == null || animalAttributeModel.Count == 0)
            {
                return NotFound();
            }

            Dictionary<string, string> attributeDetails = new Dictionary<string, string>();

            foreach (var attribute in animalAttributeModel)
            {
                var attributeInfo = await _context.Attributes.FindAsync(attribute.AttributeId);
                if (attributeInfo != null)
                {
                    attributeDetails.Add(attributeInfo.AttributeName, attribute.AttributeValue);
                }
            }
                return Ok( new { animalAttributes = animalAttributeModel, details = attributeDetails });
        }

        // PUT: Animal/Attribute/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimalAttributeModel(int id, AnimalAttributeModel animalAttributeModel)
        {
            if (id != animalAttributeModel.id)
            {
                return BadRequest();
            }

            _context.Entry(animalAttributeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalAttributeModelExists(id))
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnimalAttributeModel>> PostAnimalAttributeModel(AnimalAttributeDto animalAttributeModel)
        {
            var newAnimalAttribute = new AnimalAttributeModel
            {
                AnimalId = animalAttributeModel.AnimalId,
                AttributeId = animalAttributeModel.AttributeId,
                AttributeValue = animalAttributeModel.AttributeValue
            };

            _context.AnimalAttributes.Add(newAnimalAttribute);
            await _context.SaveChangesAsync();
            return Ok( new { message = "Animal attribute added successfully!" });    
        }

        // DELETE: Animal/Attribute/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimalAttributeModel(int id)
        {
            var animalAttributeModel = await _context.AnimalAttributes.FindAsync(id);
            if (animalAttributeModel == null)
            {
                return NotFound();
            }

            _context.AnimalAttributes.Remove(animalAttributeModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnimalAttributeModelExists(int id)
        {
            return _context.AnimalAttributes.Any(e => e.id == id);
        }
    }
}
