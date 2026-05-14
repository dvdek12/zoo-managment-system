using System.ComponentModel.DataAnnotations;

namespace ZooManagmentSystem.DTOs
{
    public class ClientRegisterDto
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [Required, StringLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDay { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class ClientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
    public class ClientUpdateDto
    {
        public int id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
