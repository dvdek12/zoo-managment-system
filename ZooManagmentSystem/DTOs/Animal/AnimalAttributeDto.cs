using ZooManagmentSystem.Models;
using ZooManagmentSystem.Models.Animal;

namespace ZooManagmentSystem.DTOs.Animal
{
    public class AnimalAttributeCreateDto
    {
        public int AttributeId { get; set; }
        public string AttributeValue { get; set; }
    }

    public class AnimalAttributeDto
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
