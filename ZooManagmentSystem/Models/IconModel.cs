namespace ZooManagmentSystem.Models
{
    public class IconModel : ModelPrototype
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] ImageData { get; set; }
    }
}
