namespace BookResale.Api.Entities
{
    public class Inbox
    {
        public int Id { get; set; }
        public int RecepientId { get; set; }
        public int SenderId { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }
        public DateTime Timestamp { get; set; }
        public int ReadStatus { get; set; }
    }
}
