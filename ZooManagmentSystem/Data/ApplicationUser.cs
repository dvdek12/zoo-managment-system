using Microsoft.AspNetCore.Identity;
using ZooManagmentSystem.Models.Employee;
using ZooManagmentSystem.Models.Client;

namespace ZooManagmentSystem.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ClientModel? Client { get; set; }
        public EmployeeModel? Employee { get; set; }
    }
}
