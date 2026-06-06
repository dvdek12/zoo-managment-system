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

    public class FeedingPlanDto
    {
        public string date { get; set; }
        public Dictionary<string, decimal> FoodNeeded = new Dictionary<string, decimal>();
        public int AnimalCount { get; set; }
        public List<FeedingDetails> FeedingDetails { get; set; } = new List<FeedingDetails>();

    }

    public class FeedingDetails
    {
        public string animalName;
        public string enclosureName;
        public int quantity;
        public decimal serving;
        public string foodType;
    }
}
