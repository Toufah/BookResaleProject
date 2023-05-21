using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookResale.Models.Dtos
{
    public class StateDto
    {
        public int Id { get; set; }
        public string State { get; set; } = string.Empty;//new - second-hand - old
    }
}
