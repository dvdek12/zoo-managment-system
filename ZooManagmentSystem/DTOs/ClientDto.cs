using System.ComponentModel.DataAnnotations;

namespace ZooManagmentSystem.DTOs
{
    public class ClientDto
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
}
