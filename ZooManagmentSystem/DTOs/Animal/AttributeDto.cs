using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.Models.Enums;

namespace ZooManagmentSystem.DTOs.Animal
{
    public class AttributeDto
    {
        public int Id { get; set; }
        public string AttributeName { get; set; }
        public string AnimalType { get; set; }
        public AttributeTypeEnum AttributeType { get; set; } = AttributeTypeEnum.String;
    }
    public class AttributeCreateDto
    {
        public string AttributeName { get; set; }
        public int? AnimalTypeId { get; set; }
        public AttributeTypeEnum AttributeType { get; set; } = AttributeTypeEnum.String;
    }
    public class AttributeUpdateDto
    {
        public int Id { get; set; }
        public string AttributeName { get; set; }
        public int? AnimalTypeId { get; set; }
        public AttributeTypeEnum AttributeType { get; set; } = AttributeTypeEnum.String;
    }
}
