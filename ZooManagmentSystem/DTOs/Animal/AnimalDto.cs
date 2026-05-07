using ZooManagmentSystem.Models;
using ZooManagmentSystem.Models.Animal;

namespace ZooManagmentSystem.DTOs.Animal
{
    public class AnimalDto
    {
        public string Name { get; set; }
        public string RaceName { get; set; }
        public string Description { get; set; }
        public string? Origin { get; set; }
        public DateTime? DateOfArrival { get; set; }
        public int? EnclosureId { get; set; }
    }
}
