using ZooManagmentSystem.Enums;
using ZooManagmentSystem.Models;
using ZooManagmentSystem.Models.Animal;

namespace ZooManagmentSystem.DTOs.Animal
{
    public class AnimalHistoryDto
    {
        public AnimalConditionEnum ConditionAdmission { get; set; }
        public float Temperature { get; set; }
        public float Weight { get; set; }
        public bool IsVacinated { get; set; }
        public DateTime? DateOfLastCheckup { get; set; }
    }
}
