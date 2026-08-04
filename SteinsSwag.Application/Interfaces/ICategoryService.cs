using SteinsSwag.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteinsSwag.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
    }
}
