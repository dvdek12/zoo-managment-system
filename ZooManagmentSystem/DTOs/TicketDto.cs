namespace ZooManagmentSystem.DTOs
{
    public class TicketDto
    {
        public int ClientId { get; set; }
        public Dictionary<int, int> EntryTypeIds { get; set; } = new Dictionary<int, int>();
    }

    public class TicketDetailsDto
    {
        public int ClientId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ValidUntil { get; set; }
        public decimal Price { get; set; }
        public Dictionary<string, int> EntryTypes { get; set; } = new Dictionary<string, int>();
    }

    public class TicketsAllDto
    {
        public int ClientId { get; set; }
        public int Id { get; set; }
        public Dictionary<string, int> EntryTypes { get; set; } = new Dictionary<string, int>();
    }

    public class TicketsForClientDto
    {
        public int Id { get; set; }
        public Dictionary<string, int> EntryTypes { get; set; } = new Dictionary<string, int>();
    }
}
