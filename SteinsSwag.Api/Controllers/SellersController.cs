using Microsoft.AspNetCore.Mvc;
using SteinsSwag.Application.DTOs;
using SteinsSwag.Application.Interfaces;

namespace SteinsSwag.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class SellersController : ControllerBase
{
    private readonly ISellerService _sellerService;
    public SellersController(ISellerService sellerService)
    {
        _sellerService = sellerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SellerDto>>> GetAll()
        => Ok(await _sellerService.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SellerDto>> GetById(int id)
    {
        var seller = await _sellerService.GetByIdAsync(id);
        return seller is null ? NotFound() : Ok(seller);
    }

    [HttpPost]
    public async Task<ActionResult<SellerDto>> Create(CreateSellerDto dto)
    {
        var created = await _sellerService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _sellerService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
    // GET /api/sellers/5/placement-slots
    [HttpGet("{sellerId:int}/placement-slots")]
    public async Task<ActionResult<IEnumerable<PlacementSlotDto>>> GetPlacementSlots(int sellerId)
        => Ok(await _sellerService.GetPlacementSlotsAsync(sellerId));

    [HttpPost("placement-slots")]
    public async Task<ActionResult<PlacementSlotDto>> CreatePlacementSlot(CreatePlacementSlotDto dto)
    {
        var created = await _sellerService.CreatePlacementSlotAsync(dto);
        return CreatedAtAction(nameof(GetPlacementSlots), new { sellerId = created.SellerId }, created);
    }

}
