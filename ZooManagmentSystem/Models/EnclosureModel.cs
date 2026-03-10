namespace ZooManagmentSystem.Models
{
    public class EnclosureModel : ModelPrototype
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public List<AnimalModel>? animals { get; set; }
    }
}
