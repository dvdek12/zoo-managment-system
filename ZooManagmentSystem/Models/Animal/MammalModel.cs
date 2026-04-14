using System.ComponentModel.DataAnnotations.Schema;

namespace ZooManagmentSystem.Models.Animal
{
    [Table("Mammals")]
    public class MammalModel : AnimalModel
    {
        public string Gender {  get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
