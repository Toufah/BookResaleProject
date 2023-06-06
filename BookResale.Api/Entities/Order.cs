namespace BookResale.Api.Entities
{
    public class Order
    {
        public int orderId { get; set; }
        public string? booksId { get; set; }
        public int sellerId { get; set; }
        public decimal totalPrice { get; set; }
        public DateTime? orderDate { get; set; }
        public int method { get; set; }
    }
}
