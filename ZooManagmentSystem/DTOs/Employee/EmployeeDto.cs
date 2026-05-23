using System.ComponentModel.DataAnnotations;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models.Employee;

namespace ZooManagmentSystem.DTOs.Employee
{
    public class EmployeeRegisterDto
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [Required, StringLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDay { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? SupervisorId { get; set; }
        public int? RoleId { get; set; }
        public int? IconId { get; set; }


        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? SupervisorId { get; set; }
        public int? RoleId { get; set; }
        public int? IconId { get; set; }
    }
}
