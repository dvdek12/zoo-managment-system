using ZooManagmentSystem.Models.Enums;

namespace ZooManagmentSystem.Models.Animal
{
    public class AttributeModel : ModelPrototype
    {
        public string AttributeName { get; set; }
        public int? AnimalTypeId { get; set; }
        public AnimalType? AnimalType { get; set; }
        public AttributeTypeEnum AttributeType { get; set; } = AttributeTypeEnum.String;
    }

    public enum AttributeTypeEnum
    {
        String,
        Number,
        Boolean,
        Date
    }
}
