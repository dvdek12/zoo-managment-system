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
}
