using Microsoft.AspNetCore.Mvc;

namespace ZooManagmentSystem.Controllers
{
    public class AnimalsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
