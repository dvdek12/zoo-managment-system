namespace ZooManagmentSystem.Models.Animal
{
    public class AttributeModel : ModelPrototype
    {
        public int AnimalId { get; set; }
        public AnimalModel Animal { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
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
