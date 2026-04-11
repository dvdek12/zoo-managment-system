using ZooManagmentSystem.Enums;
using ZooManagmentSystem.Models.Animal;

namespace ZooManagmentSystem.Models.Employee
{
    public class TaskModel : ModelPrototype
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public TaskCategoriesEnum Category { get; set; }
        public EnclosureModel? Enclosure { get; set; }
        public AnimalModel? Animal { get; set; }
        
        public EmployeeModel? AssignedEmployee { get; set; }
    }
}
