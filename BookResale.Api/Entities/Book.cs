namespace BookResale.Api.Entities
{
    public class Book
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StateId { get; set; }
        public int Qty { get; set; }
        public int CategoryId { get; set; }
        public int approvalStatus {get; set;}
        public int sellerId { get; set; }
    }
}
