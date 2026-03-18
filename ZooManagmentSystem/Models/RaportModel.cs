namespace ZooManagmentSystem.Models
{
    public abstract class RaportModel : ModelPrototype
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Employee? Author { get; set; }
    }
}
