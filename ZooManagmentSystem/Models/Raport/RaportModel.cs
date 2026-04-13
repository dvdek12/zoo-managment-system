using ZooManagmentSystem.Models.Employee;

namespace ZooManagmentSystem.Models.Raport
{
    public abstract class RaportModel : ModelPrototype
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public EmployeeModel? Author { get; set; }
    }
}
