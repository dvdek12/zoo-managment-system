using System.Collections.Generic;

namespace ZooManagmentSystem.DTOs.Reports
{
    public class VisitorStatisticsDto
    {
        public string Month { get; set; }
        public Dictionary<string, int> EntryTypeCounts { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, decimal> Percentages { get; set; } = new Dictionary<string, decimal>();
        public int TotalVisitors { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
