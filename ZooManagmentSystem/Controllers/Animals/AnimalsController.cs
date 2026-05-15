using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.ViewModels;
using ZooManagmentSystem.DTOs.Animal;
using ZooManagmentSystem.Models;
using ZooManagmentSystem.Enums;

namespace ZooManagmentSystem.Controllers.Animals
{
    [ApiController]
    [Route("animals")] 
    //[Authorize(Roles = ("Employee"))]
    public class AnimalsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnimalsController(AppDbContext context)
        {
            _context = context;
        }

        [Route("getAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var animals = await _context.Animals.ToListAsync();
            return Ok(animals);
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AnimalDto animal)
        {
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
                    IconId = animal.IconId
                };

                var animalHistory = new AnimalHistoryModel
                {
                    Animal = newAnimal,
                    ConditionAdmission = AnimalConditionEnum.Unknown,
                    Temperature = 0,
                    Weight = 0,
                    IsVacinated = false,
                    DateOfLastCheckup = DateTime.Now
                };

                _context.Animals.Add(newAnimal);
                _context.AnimalHistories.Add(animalHistory);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Animal added successfuly!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getOne/{id}")]
        public IActionResult GetOne(int id)
        {
            var animal = _context.Animals.Find(id);
            if (animal == null) return NotFound();
            return Ok(animal);
        }

        [HttpGet]
        [Route("getHistory/{id}")]
        public IActionResult GetAnimalHistory(int id)
        {
            var animalHistory = _context.AnimalHistories
                .Where(ah => ah.Animal != null && ah.Animal.id == id)
                .ToList();
            if (animalHistory == null || animalHistory.Count == 0) return NotFound();
            return Ok(animalHistory);
        }

        [HttpPost]
        [Route("addHistory/{id}")]
        public IActionResult AddAnimalHistory(int id, AnimalHistoryDto animalHistoryDto)
        {
            var animal = _context.Animals.Find(id);
            if (animal == null) return NotFound();
            var animalHistory = new AnimalHistoryModel
            {
                AnimalId = animal.id,
                Animal = animal,
                ConditionAdmission = animalHistoryDto.ConditionAdmission,
                Temperature = animalHistoryDto.Temperature,
                Weight = animalHistoryDto.Weight,
                IsVacinated = animalHistoryDto.IsVacinated,
                DateOfLastCheckup = animalHistoryDto.DateOfLastCheckup
            };
            _context.AnimalHistories.Add(animalHistory);
            _context.SaveChanges();
            return Ok(new { message = "Animal history added successfuly!" });
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, AnimalModel animal)
        {
            if(id != animal.id) return BadRequest();

            _context.Animals.Update(animal);
            _context.SaveChanges();
            return Ok("Animal edited!");
        }

        [Route("delete")]
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
