namespace ZooManagmentSystem.Models.Client
{
    public class TicketEntryTypeModel : ModelPrototype
    {
        public int TicketId { get; set; }
        public TicketModel Ticket { get; set; }
        public int EntryTypeId { get; set; }
        public EntryTypeModel EntryType { get; set; }
        public int Quantity { get; set; }
    }
}
