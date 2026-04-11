using ZooManagmentSystem.Models.Enums;

namespace ZooManagmentSystem.Models.Client
{
    public class TicketModel
    {
        public ClientModel Client { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ValidUntil { get; set; }
        public decimal Price { get; set; }
        public List<EntryTypeModel> EntryTypes { get; set; }
    }
}
