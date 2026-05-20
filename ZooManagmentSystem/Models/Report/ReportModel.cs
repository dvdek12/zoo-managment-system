using ZooManagmentSystem.Models.Employee;

namespace ZooManagmentSystem.Models.Report
{
    public class ReportModel : ModelPrototype
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public ReportType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AuthorId { get; set; }
        public EmployeeModel? Author { get; set; }
    }

    public enum ReportType
    {
        WorkersPerformance,
        Financial,
        VisitorStatistics,
        FeedingPlan,
        AnimalsCondition
    }
}
