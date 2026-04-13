using System.ComponentModel.DataAnnotations;

namespace ZooManagmentSystem.Models.Animal
{
    public class AnimalModel : ModelPrototype
    {
        [Required]
        public string Name { get; set; }
        public string RaceName { get; set; }
        public string Description { get; set; }
        public string? Origin { get; set; }
        public DateTime? DateOfArrival { get; set; }
    }
}
