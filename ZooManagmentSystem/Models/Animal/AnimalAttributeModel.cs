namespace ZooManagmentSystem.Models.Animal
{
    public class AnimalAttributeModel
    {
        public int AnimalId { get; set; }
        public AnimalModel? Animal { get; set; }
        public int AttributeId { get; set; }
        public AttributeModel? Attribute { get; set; }
        public string AttributeValue { get; set; }
    }
}
