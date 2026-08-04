using System;
using System.Collections.Generic;
using System.Text;

namespace SteinsSwag.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? DiscordHandle { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
