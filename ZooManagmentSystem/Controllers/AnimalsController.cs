using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.ViewModels;

namespace ZooManagmentSystem.Controllers
{
    [Route("animals")]
    public class AnimalsController : Controller
    {
        private readonly AppDbContext _context;

        public AnimalsController(AppDbContext context)
        {
            _context = context;
        }

        [Route("all")]
        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new AnimalPageViewModel
            {
                NewAnimal = new AnimalModel(),
                Animals = _context.Animals.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnimalModel animal)
        {
            try
            {
                _context.Animals.Add(animal);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Animal created successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred while creating the animal: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var animal = _context.Animals.Find(id);
            if (animal == null) return NotFound();
            return View(animal);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, AnimalModel animal)
        {
            if(id != animal.id) return BadRequest();

            _context.Animals.Update(animal);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var animal = _context.Animals.Find(id);
            if (animal == null) return NotFound();

            _context.Animals.Remove(animal);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
