namespace BookResale.Api.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public string? BooksId { get; set; }
        public int UserId { get; set; }
        public int ItemsCount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? OrderDate { get; set; }
        public int Method { get; set; }
        public string? Address { get; set; }
        public string? city { get; set; }
        public string? phoneNumber { get; set; }
        public int ApprovalStatus { get; set; }
    }
}
