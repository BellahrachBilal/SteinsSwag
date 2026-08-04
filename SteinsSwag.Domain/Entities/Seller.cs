using SteinsSwag.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteinsSwag.Domain.Entities
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ContactHandle { get; set; } //discord tiktok email handle
        public PricingModel PricingModel { get; set; } = PricingModel.FixedRate;
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Item> Items { get; set; } = new List<Item>();
        public ICollection<PlacementSlot> PlacementSlots { get; set; } = new List<PlacementSlot>();

    }
}
