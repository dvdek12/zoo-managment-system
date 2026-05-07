using System.ComponentModel.DataAnnotations;

namespace ZooManagmentSystem.DTOs.Employee
{
    public class RoleDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(500)]
        public string Description { get; set; }
        public bool IsManagerial { get; set; } = false;
    }
}
