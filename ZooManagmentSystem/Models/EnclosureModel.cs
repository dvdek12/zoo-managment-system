using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.Models.Enums;

namespace ZooManagmentSystem.Models
{
    public class EnclosureModel : ModelPrototype
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? TypeId { get; set; }
        public EnclosureTypeModel? Type { get; set; }
        public List<AnimalModel>? Animals { get; set; }
    }
}
