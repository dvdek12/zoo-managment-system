using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.Models.Enums;

namespace ZooManagmentSystem.DTOs.Animal
{
    public class AttributeDto
    {
        public string AttributeName { get; set; }
        public int? AnimalTypeId { get; set; }
        public AttributeTypeEnum AttributeType { get; set; } = AttributeTypeEnum.String;
    }
}
