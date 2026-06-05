using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZooManagmentSystem.Models.Enums;

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

        // Attributes
        public List<AnimalAttributeModel> Attributes { get; set; } = new List<AnimalAttributeModel>();

        // Feeding
        public int? FoodId { get; set; }
        public FoodTypeModel? Food { get; set; }
        public int? FeedingsPerDay { get; set; }
        public decimal? AmountPerFeeding { get; set; }

        // Enclosure
        public int? EnclosureId { get; set; }
        public EnclosureModel? Enclosure { get; set; }

        // Histories
        public ICollection<AnimalHistoryModel> AnimalHistories { get; set; } = new List<AnimalHistoryModel>();


        // Icons
        public int? IconId { get; set; }
        public IconModel? Icon { get; set; }
    }
}
