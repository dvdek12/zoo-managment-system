using ZooManagmentSystem.Models;
using ZooManagmentSystem.Models.Animal;

namespace ZooManagmentSystem.DTOs.Animal
{
    public class AnimalHistoryCreateDto
    {
        public int ConditionId { get; set; }
        public float Temperature { get; set; }
        public float Weight { get; set; }
        public bool IsVacinated { get; set; }
        public DateTime? DateOfLastCheckup { get; set; }
    }

    public class AnimalHistoryDto
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public string Condition { get; set; }
        public float Temperature { get; set; }
        public float Weight { get; set; }
        public bool IsVacinated { get; set; }
        public DateTime? DateOfLastCheckup { get; set; }
    }
}
