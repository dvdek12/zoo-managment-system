using System.ComponentModel.DataAnnotations;
using ZooManagmentSystem.Models;
using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.Models.Employee;
using ZooManagmentSystem.Models.Enums;

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
        public bool IsCompleted { get; set; } = false;
        public int? CategoryId { get; set; }

        // Assaignment details
        public int? AssignedEmployeeId { get; set; }
        public int? RoleId { get; set; }

        // Optional animals or enclosures related to the task
        public int? EnclosureId { get; set; }
        public int? AnimalId { get; set; }
    }

    public class TaskUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public bool? IsCompleted { get; set; }
        public int? CategoryId { get; set; }

        // Assaignment details
        public int? AssignedEmployeeId { get; set; }
        public int? RoleId { get; set; }

        // Optional animals or enclosures related to the task
        public int? EnclosureId { get; set; }
        public int? AnimalId { get; set; }
    }
}
