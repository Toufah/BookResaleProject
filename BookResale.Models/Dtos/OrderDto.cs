using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookResale.Models.Dtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string? BooksId { get; set; }
        public int UserId { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public int ItemsCount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? OrderDate { get; set; }
        public int Method { get; set; }
        public string? Address { get; set; }
        public string? city { get; set; }
        public string? phoneNumber { get; set; }
        public int ApprovalStatus { get; set; }
        public string? ApprovalStatusTitle { get;  set; }
    }
}
