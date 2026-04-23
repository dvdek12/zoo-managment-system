namespace ZooManagmentSystem.Models.Employee
{
    public class RoleModel : ModelPrototype
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TaskModel>? Tasks { get; set; }
    }
}
