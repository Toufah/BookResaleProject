﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookResale.Models.Dtos
{
    public class InboxDto
    {
        public int Id { get; set; }
        public int RecepientId { get; set; }
        public string? RecepientName { get; set; }
        public int SenderId { get; set; }
        public string? SenderName { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }
        public DateTime Timestamp { get; set; }
        public int ReadStatus { get; set; }
    }
}
