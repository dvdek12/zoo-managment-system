using ZooManagmentSystem.Models.Animal;

namespace ZooManagmentSystem.DTOs
{
    public class AnimalAttributeDto
    {
        public int AnimalId { get; set; }
        public int AttributeId { get; set; }
        public string AttributeValue { get; set; }
    }
}
