using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookResale.Models.Dtos
{
    public class CartItemDto
    {
        public long BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string BookImageURL { get; set;} = string.Empty;
        public decimal Price { get; set; }
        public int Qty { get; set; }
    }
}
