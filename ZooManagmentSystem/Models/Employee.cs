namespace ZooManagmentSystem.Models
{
    public class Employee : ModelPrototype
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Title { get; set; }
        public Employee? Supervisor { get; set; }
    }
}
