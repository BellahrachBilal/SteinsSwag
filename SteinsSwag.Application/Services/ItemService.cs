using Microsoft.EntityFrameworkCore;
using SteinsSwag.Application.DTOs;
using SteinsSwag.Application.Interfaces;
using SteinsSwag.Domain.Entities;
using SteinsSwag.Domain.Enums;
using SteinsSwag.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteinsSwag.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly SteinsSwagDbContext _context;

        public ItemService(SteinsSwagDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ItemDto>> GetAllAsync(int? categoryId = null, ItemStatus? status = null)
        {
            var query = _context.Items
                .Include(x => x.Category)
                .Include(x => x.Seller)
                .AsQueryable();
            if (categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == categoryId.Value);
            }
            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);
            return await query
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => ToDto(x))
                .ToListAsync();
        }

        public async Task<ItemDto> CreateAsync(CreateItemDto dto)
        {
            var item = new Item
            {
                Name = dto.Name,
                Description = dto.Description,
                Brand = dto.Brand,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId,
                SellerId = dto.SellerId,
                SourcePlatform = dto.SourcePlatform,
                Condition = dto.Condition,
                Status = ItemStatus.Available,
                CreatedAt = DateTime.UtcNow
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return (await GetByIdAsync(item.Id))!;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if(item is null) return false;

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<ItemDto?> GetByIdAsync(int id)
        {
            var item =await _context.Items
                .Include(x => x.Category)
                .Include(x => x.Seller)
                .FirstOrDefaultAsync(x => x.Id == id);
            return item is null ? null : ToDto(item);
        }

        public async Task<bool> UpdateAsync(int id, UpdateItemDto dto)
        {
            var item = await _context.Items.FindAsync(id);
            if (item is null) return false;

            item.Name = dto.Name;
            item.Description = dto.Description;
            item.Brand= dto.Brand;
            item.Price = dto.Price;
            item.ImageUrl = dto.ImageUrl;
            item.CategoryId = dto.CategoryId;
            item.SellerId = dto.SellerId;
            item.SourcePlatform = dto.SourcePlatform;
            item.Condition = dto.Condition;
            item.Status = dto.Status;
            item.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
        private static ItemDto ToDto(Item i) => new(
           i.Id, i.Name, i.Description, i.Brand, i.Price, i.ImageUrl,
           i.CategoryId, i.Category.Name,
           i.SellerId, i.Seller?.Name,
           i.SourcePlatform, i.Condition, i.Status, i.CreatedAt
        );
    }
}
