namespace ZooManagmentSystem.DTOs
{
    public class TicketNewDto
    {
        public int ClientId { get; set; }
        public Dictionary<int, int> EntryTypeIds { get; set; } = new Dictionary<int, int>();
    }

    public class TicketDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ValidUntil { get; set; }
        public decimal Price { get; set; }
        public Dictionary<string, int> EntryTypes { get; set; } = new Dictionary<string, int>();
    }
}
