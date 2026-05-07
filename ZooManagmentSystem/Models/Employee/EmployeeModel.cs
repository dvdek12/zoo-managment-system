using ZooManagmentSystem.Data;

namespace ZooManagmentSystem.Models.Employee
{
    public class EmployeeModel : ModelPrototype
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? SupervisorId { get; set; }
        public EmployeeModel? Supervisor { get; set; }
        public int? RoleId { get; set; }
        public RoleModel? Role { get; set; }
        public List<TaskModel>? Tasks { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
