using ZooManagmentSystem.Models.Employee;
using ZooManagmentSystem.Models.Report;

namespace ZooManagmentSystem.DTOs.Reports
{
    public class ReportDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public ReportType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AuthorId { get; set; }
    }

    public class ReportCreateDto
    {
        public string Title { get; set; }
        public ReportType Type { get; set; }
        public int AuthorId { get; set; }
        public int? EmployeeId { get; set; }
        public int? AnimalId { get; set; }
        public int? EnclosureId { get; set; }
    }
}
