using SteinsSwag.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SteinsSwag.Application.DTOs;
using SteinsSwag.Domain.Entities;
using SteinsSwag.Infrastructure.Data;
using SteinsSwag.Domain.Exceptions;

namespace SteinsSwag.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly SteinsSwagDbContext _context;

        public CategoryService(SteinsSwagDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryDto(c.Id, c.Name, c.Items.Count))
                .ToListAsync();
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var exists = await _context.Categories.AnyAsync(c => c.Name == dto.Name);
            if (exists)
                throw new ValidationException($"A category named '{dto.Name}' already exists.");

            var category = new Category { Name = dto.Name };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return new CategoryDto(category.Id, category.Name, 0);
        }
    }
}
