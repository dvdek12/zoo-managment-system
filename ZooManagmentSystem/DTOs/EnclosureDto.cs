namespace ZooManagmentSystem.DTOs
{
    public class EnclosureDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? TypeId { get; set; }
    }

    public class EnclosureUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? TypeId { get; set; }
    }
}
