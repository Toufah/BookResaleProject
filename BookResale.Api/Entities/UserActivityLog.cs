namespace BookResale.Api.Entities
{
    public class UserActivityLog
    {
        public int id {  get; set; }
        public int userId { get; set; }
        public long bookId { get; set; }
        public DateTime? visitTime { get; set; }
    }
}
