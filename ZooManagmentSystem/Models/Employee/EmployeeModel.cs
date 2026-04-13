namespace ZooManagmentSystem.Models.Employee
{
    public class EmployeeModel : ModelPrototype
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Title { get; set; }
        public EmployeeModel? Supervisor { get; set; }
        public RoleModel Role { get; set; }
        public List<TaskModel> Tasks { get; set; }
    }
}
