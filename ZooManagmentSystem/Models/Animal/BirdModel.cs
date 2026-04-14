using System.ComponentModel.DataAnnotations.Schema;

namespace ZooManagmentSystem.Models.Animal
{
    [Table("Birds")]
    public class BirdModel : AnimalModel
    {
        public string Color { get; set; }
        public bool CanFly { get; set; }
        public DateTime? HatchedDate { get; set; }
    }
}
