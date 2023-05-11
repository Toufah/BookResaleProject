using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookResale.Models.Dtos
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string BookDescription { get; set; } = string.Empty;
        public string BookImageURL { get; set;} = string.Empty;
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public int Qty { get; set; }
    }
}
