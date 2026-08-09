using Microsoft.EntityFrameworkCore;
using SteinsSwag.Application.DTOs;
using SteinsSwag.Application.Interfaces;
using SteinsSwag.Domain.Entities;
using SteinsSwag.Domain.Exceptions;
using SteinsSwag.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteinsSwag.Application.Services
{
    public class SellerService : ISellerService
    {
        private readonly SteinsSwagDbContext _context;

        public SellerService(SteinsSwagDbContext context)
        {
            _context = context;
        }

        public async Task<SellerDto> CreateAsync(CreateSellerDto dto)
        {
            var seller = new Seller
            {
                Name = dto.Name,
                ContactHandle = dto.ContactHandle,
                PricingModel = dto.PricingModel,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow
            };
            _context.Sellers.Add(seller);
            await _context.SaveChangesAsync();
            return (await GetByIdAsync(seller.Id))!;
        }

        public async Task<PlacementSlotDto> CreatePlacementSlotAsync(CreatePlacementSlotDto dto)
        {
            if (dto.EndDate.HasValue && dto.EndDate.Value <= dto.StartDate)
                throw new ValidationException("EndDate must be after StartDate.");

            var slot = new PlacementSlot
            {
                SellerId = dto.SellerId,
                Position = dto.Position,
                Price = dto.Price,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = true
            };

            _context.PlacementSlots.Add(slot);
            await _context.SaveChangesAsync();

            var seller = await _context.Sellers.FindAsync(dto.SellerId);
            return new PlacementSlotDto(
                slot.Id, slot.SellerId, seller?.Name ?? string.Empty, slot.Position,
                slot.Price, slot.StartDate, slot.EndDate, slot.IsActive);

        }

        public async Task DeleteAsync(int id)
        {
            var seller = await _context.Sellers.FindAsync(id);
            if (seller is null)
                throw new NotFoundException($"Seller with id {id} not found.");

            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SellerDto>> GetAllAsync()
        {
            return await _context.Sellers
                   .Select(s => new SellerDto(
                       s.Id, s.Name, s.ContactHandle, s.PricingModel, s.Notes, s.CreatedAt,
                       s.Items.Count))
                   .ToListAsync();
        }

        public async Task<SellerDto?> GetByIdAsync(int id)
        {
            return await _context.Sellers
                        .Where(s => s.Id == id)
                        .Select(s => new SellerDto(
                            s.Id, s.Name, s.ContactHandle, s.PricingModel, s.Notes, s.CreatedAt,
                            s.Items.Count))
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PlacementSlotDto>> GetPlacementSlotsAsync(int sellerId)
        {
            return await _context.PlacementSlots
                        .Include(p => p.Seller)
                        .Where(p => p.SellerId == sellerId)
                        .Select(p => new PlacementSlotDto(
                            p.Id, p.SellerId, p.Seller.Name, p.Position, p.Price,
                            p.StartDate, p.EndDate, p.IsActive))
                        .ToListAsync();
        }
    }
}
