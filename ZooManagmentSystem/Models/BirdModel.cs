namespace ZooManagmentSystem.Models
{
    public class BirdModel : AnimalModel
    {
        public string Color { get; set; }
        public bool CanFly { get; set; }
        public DateTime? HatchedDate { get; set; }
    }
}
