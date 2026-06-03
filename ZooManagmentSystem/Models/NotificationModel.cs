namespace ZooManagmentSystem.Models
{
    public class NotificationModel : ModelPrototype
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public int? UserId { get; set; }

    }
}
