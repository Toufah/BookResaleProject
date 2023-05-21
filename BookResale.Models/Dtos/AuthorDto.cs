using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookResale.Models.Dtos
{
    public class AuthorDto
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
