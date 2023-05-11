using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookResale.Models.Dtos
{
    public class BookDto
    {
        public long Id { get; set; }
        public string Title{ get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public string AuthorFirstName { get; set; } = string.Empty;
        public string AuthorLastName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int StateId { get; set; }
        public string State { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Qty { get; set; }
    }
}
