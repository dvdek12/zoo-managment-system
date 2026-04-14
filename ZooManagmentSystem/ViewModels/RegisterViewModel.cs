using System.ComponentModel.DataAnnotations;

namespace ZooManagmentSystem.ViewModels
{
    public class RegisterViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsEmployee { get; set; }
    }
}
