using ZooManagmentSystem.Models;
using ZooManagmentSystem.Models.Animal;

namespace ZooManagmentSystem.DTOs.Animal
{
    public class AnimalAttributeDto
    {
        public int AnimalId { get; set; }
        public int AttributeId { get; set; }
        public string AttributeValue { get; set; }
    }

    public class AttributesForAnimalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class AnimalAttributeUpdateDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
