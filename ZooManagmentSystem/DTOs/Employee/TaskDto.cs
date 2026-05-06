using System.ComponentModel.DataAnnotations;
using ZooManagmentSystem.Enums;
using ZooManagmentSystem.Models;
using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.Models.Employee;

namespace ZooManagmentSystem.DTOs.Employee
{
    public class TaskDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public TaskCategoriesEnum? Category { get; set; }

        // Assaignment details
        public int? AssignedEmployeeId { get; set; }
        public int? RoleId { get; set; }

        // Optional animals or enclosures related to the task
        public int? EnclosureId { get; set; }
        public int? AnimalId { get; set; }
    }
}
