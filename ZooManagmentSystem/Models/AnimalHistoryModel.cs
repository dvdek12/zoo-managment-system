using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.Models.Dictionaries;

namespace ZooManagmentSystem.Models
{
    public class AnimalHistoryModel : ModelPrototype
    {
        public int AnimalId { get; set; }
        public AnimalModel? Animal { get; set; }
        public int? ConditionId { get; set; }
        public AnimalConditionModel? Condition { get; set; }
        public float Temperature { get; set; }
        public float Weight { get; set; }
        public bool IsVacinated { get; set; }
        public DateTime? DateOfLastCheckup { get; set; }
    }
}
