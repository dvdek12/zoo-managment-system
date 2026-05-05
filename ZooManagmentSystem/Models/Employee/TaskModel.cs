using ZooManagmentSystem.Enums;
using ZooManagmentSystem.Models.Animal;

namespace ZooManagmentSystem.Models.Employee
{
    public class TaskModel : ModelPrototype
    {
        // Basic information about the task
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public TaskCategoriesEnum? Category { get; set; }

        // Assaignment details
        public int? AssignedEmployeeId { get; set; }
        public EmployeeModel? AssignedEmployee { get; set; }
        public int? RoleId { get; set; }
        public RoleModel? Role { get; set; }

        // Optional animals or enclosures related to the task
        public int? EnclosureId { get; set; }
        public EnclosureModel? Enclosure { get; set; }
        public int? AnimalId { get; set; }
        public AnimalModel? Animal { get; set; }
    }
}
