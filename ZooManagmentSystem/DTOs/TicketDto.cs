namespace ZooManagmentSystem.DTOs
{
    public class TicketDto
    {
        public int ClientId { get; set; }
        public Dictionary<int, int> EntryTypeIds { get; set; } = new Dictionary<int, int>();
    }
}
