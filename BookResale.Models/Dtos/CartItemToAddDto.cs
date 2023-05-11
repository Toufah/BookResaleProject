using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookResale.Models.Dtos
{
    public class CartItemToAddDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Qty { get; set; }
    }
}
