using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.Models.Dictionaries;
using ZooManagmentSystem.Models.Enums;

namespace ZooManagmentSystem.Models.Employee
{
    public class TaskModel : ModelPrototype
    {
        // Basic information about the task
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public int? CategoryId { get; set; }
        public TaskCategoryModel? Category { get; set; }
        // Flag to track if notification has been sent for this task
        public bool NotificationSent { get; set; } = false;

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
