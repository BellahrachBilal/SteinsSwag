using SteinsSwag.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteinsSwag.Application.Interfaces
{
    public interface ISellerService
    {
        Task<IEnumerable<SellerDto>> GetAllAsync();
        Task<SellerDto?> GetByIdAsync(int id);
        Task<SellerDto> CreateAsync(CreateSellerDto dto);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<PlacementSlotDto>> GetPlacementSlotsAsync(int sellerId);
        Task<PlacementSlotDto> CreatePlacementSlotAsync(CreatePlacementSlotDto dto);
    }
}
