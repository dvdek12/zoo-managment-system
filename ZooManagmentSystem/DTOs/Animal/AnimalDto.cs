using ZooManagmentSystem.Models;
using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.Models.Enums;

namespace ZooManagmentSystem.DTOs.Animal
{
    public class AnimalCreateDto
    {
        public string Name { get; set; }
        public string RaceName { get; set; }
        public string Description { get; set; }
        public string? Origin { get; set; }
        public DateTime? DateOfArrival { get; set; }
        public int? EnclosureId { get; set; }
        public int? FoodId { get; set; }
        public int? FeedingsPerDay { get; set; }
        public decimal? AmountPerFeeding { get; set; }
        public int? IconId { get; set; }
    }

    public class AnimalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RaceName { get; set; }
        public string Description { get; set; }
        public string? Origin { get; set; }
        public DateTime? DateOfArrival { get; set; }
        public int? EnclosureId { get; set; }
        public int? FoodId { get; set; }
        public int? IconId { get; set; }
    }

    public class AnimalUpdateDto
    {
        public string? Name { get; set; }
        public string? RaceName { get; set; }
        public string? Description { get; set; }
        public string? Origin { get; set; }
        public DateTime? DateOfArrival { get; set; }
        public int? EnclosureId { get; set; }
        public int? FoodId { get; set; }
        public int? FeedingsPerDay { get; set; }
        public decimal? AmountPerFeeding { get; set; }
        public int? IconId { get; set; }
    }

    public class AnimalDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RaceName { get; set; }
        public string Description { get; set; }
        public string? Origin { get; set; }
        public DateTime? DateOfArrival { get; set; }
        public int? EnclosureId { get; set; }
        public int? FoodId { get; set; }
        public int? IconId { get; set; }
        // Navigation properties
        public EnclosureDto? Enclosure { get; set; }
        public string? Food { get; set; }
        public int? FeedingsPerDay { get; set; }
        public decimal? AmountPerFeeding { get; set; }
        public List<AnimalHistoryDto> History { get; set; } = new List<AnimalHistoryDto>();
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
    }
}
