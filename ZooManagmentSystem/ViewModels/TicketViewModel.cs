namespace ZooManagmentSystem.ViewModels
{
    public class TicketViewModel
    {
        public int ClientId { get; set; }
        public Dictionary<int, int> EntryTypeIds { get; set; } = new Dictionary<int, int>();
    }
}
