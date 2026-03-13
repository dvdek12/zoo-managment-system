namespace ZooManagmentSystem.Models
{
    public class MammalModel : AnimalModel
    {
        public string Gender {  get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
