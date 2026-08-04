using Microsoft.AspNetCore.Mvc;
using SteinsSwag.Application.DTOs;
using SteinsSwag.Application.Interfaces;

namespace SteinsSwag.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService; 

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        => Ok(await _categoryService.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Create(CreateCategoryDto dto)
        => Ok(await _categoryService.CreateAsync(dto));
}
