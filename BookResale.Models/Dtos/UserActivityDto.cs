using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookResale.Models.Dtos
{
    public class UserActivityDto
    {
        public int userId { get; set; }
        public long bookId { get; set; }
        public UserActivityDto(int userId, long bookId)
        {
            this.userId = userId;
            this.bookId = bookId;
        }
    }
}
