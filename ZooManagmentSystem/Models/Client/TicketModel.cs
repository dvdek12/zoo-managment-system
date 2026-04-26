namespace ZooManagmentSystem.Models.Client
{
    public class TicketModel : ModelPrototype
    {
        public int ClientId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ValidUntil { get; set; }
        public decimal Price { get; set; }
        public List<TicketEntryTypeModel> EntryTypes { get; set; }
    }
}
