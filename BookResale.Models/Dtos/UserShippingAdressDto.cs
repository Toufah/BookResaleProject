using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookResale.Models.Dtos
{
    public class UserShippingAdressDto
    {
        public int userId { get; set; }
        public string? Address { get; set; }
        public string? city { get; set; }
        public string? phoneNumber { get; set; }
    }
}
