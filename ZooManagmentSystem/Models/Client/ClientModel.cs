namespace ZooManagmentSystem.Models.Client
{
    public class ClientModel : ModelPrototype
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
