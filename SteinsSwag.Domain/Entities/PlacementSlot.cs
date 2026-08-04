using System;
using System.Collections.Generic;
using System.Text;

namespace SteinsSwag.Domain.Entities
{
    public class PlacementSlot
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public Seller Seller { get; set; } = null;

        public int Position { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
