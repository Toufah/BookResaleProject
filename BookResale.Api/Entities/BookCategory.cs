namespace BookResale.Api.Entities
{
    public class BookCategory
    {
        public int Id { get; set; }

        public string CategoryName { get; set; } = string.Empty;
        public ICollection<Book>? Books { get; set; }
    }
}
