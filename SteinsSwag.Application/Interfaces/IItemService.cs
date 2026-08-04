using System;
using System.Collections.Generic;
using System.Text;
using SteinsSwag.Application.DTOs;
using SteinsSwag.Domain.Enums;

namespace SteinsSwag.Application.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetAllAsync(int? categoryId = null, ItemStatus? status = null);
        Task<ItemDto?> GetByIdAsync(int id);
        Task<ItemDto> CreateAsync(CreateItemDto dto);
        Task<bool> UpdateAsync(int id, UpdateItemDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
