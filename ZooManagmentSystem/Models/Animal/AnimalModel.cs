using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZooManagmentSystem.Models.Animal
{
    [Table("Animals")]
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
