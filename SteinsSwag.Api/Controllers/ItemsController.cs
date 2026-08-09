using Microsoft.AspNetCore.Mvc;
using SteinsSwag.Application.DTOs;
using SteinsSwag.Application.Interfaces;
using SteinsSwag.Domain.Enums;

namespace SteinsSwag.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }

    // GET /api/items?categoryId=1&status=Available
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetAll(
        [FromQuery] int? categoryId, [FromQuery] ItemStatus? status)
    {
        return Ok(await _itemService.GetAllAsync(categoryId, status));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ItemDto>> GetById(int id)
    {
        var item = await _itemService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> Create(CreateItemDto dto)
    {
        var created = await _itemService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateItemDto dto)
    {
        await _itemService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _itemService.DeleteAsync(id);
        return NoContent();
    }

}
