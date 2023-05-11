using Microsoft.VisualBasic;

namespace BookResale.Api.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Born { get; set; } = string.Empty;
        public string Died { get; set; } = string.Empty;
        public string Birthplace { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
    }
}
