using Microsoft.AspNetCore.Mvc;
using ZooManagmentSystem.Enums;
namespace ZooManagmentSystem.Controllers
{
    [ApiController]
    [Route("enums")]
    public class EnumsController : Controller
    {
        [HttpGet]
        [Route("animals-condition")]
        public IActionResult GetAnimalConditionEnum()
        {
            var enumValues = Enum.GetValues(typeof(AnimalConditionEnum))
                .Cast<AnimalConditionEnum>()
                .Select(e => new
                {
                    Name = e.ToString(),
                    Value = (int)e
                })
                .ToList();
            return Ok(enumValues);
        }
    }
}
