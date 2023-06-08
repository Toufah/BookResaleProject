using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookResale.Models.Dtos
{
    public class UpdatePasswordDto
    {
        public int userId { get; set; }
        public string? newPassword { get; set; }
    }
}
