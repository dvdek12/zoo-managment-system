using ZooManagmentSystem.Enums;
using ZooManagmentSystem.Models.Animal;

namespace ZooManagmentSystem.Models
{
    public class AnimalHistoryModel : ModelPrototype
    {
        public int AnimalId { get; set; }
        public AnimalModel? Animal { get; set; }
        public AnimalConditionEnum ConditionAdmission { get; set; }
        public float Temperature { get; set; }
        public float Weight { get; set; }
        public bool IsVacinated { get; set; }
        public DateTime? DateOfLastCheckup { get; set; }
    }
}
