using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.ViewModels;
using ZooManagmentSystem.DTOs.Animal;
using ZooManagmentSystem.Models;
using ZooManagmentSystem.Models.Enums;

namespace ZooManagmentSystem.Controllers.Animals
{
    [ApiController]
    [Route("animals")]
    //[Authorize(Roles = "Employee")]
    public class AnimalsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IValidator<AnimalCreateDto> _validator;

        public AnimalsController(AppDbContext context, IValidator<AnimalCreateDto> validator)
        {
            _context = context;
            _validator = validator;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var animals = await _context.Animals.ToListAsync();
            var animalDtos = animals.Select(a => new AnimalDto
            {
                Id = a.id,
                Name = a.Name,
                RaceName = a.RaceName,
                Description = a.Description,
                Origin = a.Origin,
                DateOfArrival = a.DateOfArrival,
                EnclosureId = a.EnclosureId,
                FoodId = a.FoodId,
                IconId = a.IconId
            }).ToList();
            return Ok(animalDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOne(int id)
        {
            var animal = _context.Animals.Include(a => a.Attributes).Include(a => a.AnimalHistories).Include(a => a.Food).FirstOrDefault(a => a.id == id);
            if (animal == null) return NotFound();

            var animalDto = new AnimalDetailsDto
            {
                Id = animal.id,
                Name = animal.Name,
                RaceName = animal.RaceName,
                Description = animal.Description,
                Origin = animal.Origin,
                DateOfArrival = animal.DateOfArrival,
                EnclosureId = animal.EnclosureId,
                FeedingEmployeeId = animal.FeedingEmployeeId,
                FoodId = animal.FoodId,
                FeedingsPerDay = animal.FeedingsPerDay,
                AmountPerFeeding = animal.AmountPerFeeding,
                IconId = animal.IconId
            };
            // Setting attributes
            foreach (var attribute in animal.Attributes)
            {
                string attributeName = _context.Attributes.Find(attribute.AttributeId)?.AttributeName ?? "Unknown";
                animalDto.Attributes.TryAdd(attributeName, attribute.AttributeValue);
            }
            // Setting histories
            foreach (var history in animal.AnimalHistories)
            {
                animalDto.History.Add(new AnimalHistoryDto
                {
                    Id = history.id,
                    AnimalId = history.AnimalId,
                    Condition = _context.AnimalConditions.Find(history.ConditionId)?.Condition ?? "Unknown",
                    Temperature = history.Temperature,
                    Weight = history.Weight,
                    IsVacinated = history.IsVacinated,
                    DateOfLastCheckup = history.DateOfLastCheckup
                });
            }

            // Setting enclosure
            var enclosure = _context.Enclosures.Find(animal.EnclosureId);
            animalDto.Enclosure = enclosure != null ? new DTOs.EnclosureDto
            {
                Name = enclosure.Name,
                Description = enclosure.Description
            } : null;

            //setting food and feeding employee
            var feedingEmployee = _context.Employees.Find(animal.FeedingEmployeeId);
            animalDto.FeedingEmployeeName = feedingEmployee != null ? feedingEmployee.Email : null;
            var food = _context.FoodTypes.Find(animal.FoodId);
            animalDto.Food = food != null ? food.FoodName : null;

            return Ok(animalDto);
        }

        //[Authorize(Roles = "Manager")]
        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AnimalCreateDto animal)
        {
            var validation = await _validator.ValidateAsync(animal);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            try
            {
                var newAnimal = new AnimalModel
                {
                    Name = animal.Name,
                    RaceName = animal.RaceName,
                    Description = animal.Description,
                    Origin = animal.Origin,
                    DateOfArrival = animal.DateOfArrival,
                    EnclosureId = animal.EnclosureId,
                    FeedingEmployeeId = animal.FeedingEmployeeId,
                    FoodId = animal.FoodId,
                    FeedingsPerDay = animal.FeedingsPerDay,
                    AmountPerFeeding = animal.AmountPerFeeding,
                    IconId = animal.IconId
                };

                _context.Animals.Add(newAnimal);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Animal added successfuly!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}/getHistory")]
        public IActionResult GetAnimalHistory(int id)
        {
            var animalHistory = _context.AnimalHistories
                .Where(ah => ah.Animal != null && ah.Animal.id == id)
                .ToList();
            if (animalHistory == null || animalHistory.Count == 0) return NotFound();
            var animalHistoryDtos = animalHistory.Select(ah => new AnimalHistoryDto
            {
                Id = ah.id,
                AnimalId = ah.AnimalId,
                Condition = _context.AnimalConditions.Find(ah.ConditionId)?.Condition ?? "Unknown",
                Temperature = ah.Temperature,
                Weight = ah.Weight,
                IsVacinated = ah.IsVacinated,
                DateOfLastCheckup = ah.DateOfLastCheckup
            }).ToList();
            return Ok(animalHistoryDtos);
        }

        //[Authorize(Roles = "Manager")]
        [HttpPost]
        [Route("{id}/addHistory")]
        public IActionResult AddAnimalHistory(int id, AnimalHistoryCreateDto animalHistoryDto)
        {
            var animal = _context.Animals.Find(id);
            if (animal == null) return NotFound();
            var animalHistory = new AnimalHistoryModel
            {
                AnimalId = animal.id,
                Animal = animal,
                ConditionId = animalHistoryDto.ConditionId,
                Temperature = animalHistoryDto.Temperature,
                Weight = animalHistoryDto.Weight,
                IsVacinated = animalHistoryDto.IsVacinated,
                DateOfLastCheckup = animalHistoryDto.DateOfLastCheckup
            };

           

            _context.AnimalHistories.Add(animalHistory);
            _context.SaveChanges();
            return Ok(new { message = "Animal history added successfuly!" });
        }

        //[Authorize(Roles = "Manager")]
        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit(int id, AnimalUpdateDto animal)
        {
            var existingAnimal = _context.Animals.Find(id);
            if (existingAnimal == null) return NotFound();

            existingAnimal.Name = animal.Name ?? existingAnimal.Name;
            existingAnimal.RaceName = animal.RaceName ?? existingAnimal.RaceName;
            existingAnimal.Description = animal.Description ?? existingAnimal.Description;
            existingAnimal.Origin = animal.Origin ?? existingAnimal.Origin;
            existingAnimal.DateOfArrival = animal.DateOfArrival ?? existingAnimal.DateOfArrival;
            existingAnimal.EnclosureId = animal.EnclosureId ?? existingAnimal.EnclosureId;
            existingAnimal.FeedingEmployeeId = animal.FeedingEmployeeId ?? existingAnimal.FeedingEmployeeId;
            existingAnimal.FoodId = animal.FoodId ?? existingAnimal.FoodId;
            existingAnimal.FeedingsPerDay = animal.FeedingsPerDay ?? existingAnimal.FeedingsPerDay;
            existingAnimal.AmountPerFeeding = animal.AmountPerFeeding ?? existingAnimal.AmountPerFeeding;
            existingAnimal.IconId = animal.IconId ?? existingAnimal.IconId;

            _context.Animals.Update(existingAnimal);
            _context.SaveChanges();
            return Ok("Animal updated!");
        }

        //[Authorize(Roles = "Manager")]
        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var animal = _context.Animals.Find(id);
            if (animal == null) return NotFound();

            _context.Animals.Remove(animal);
            _context.SaveChanges();
            return Ok("Animal deleted!");
        }
    }
}
