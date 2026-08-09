using SteinsSwag.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SteinsSwag.Application.DTOs;

public record SellerDto(
    int Id,
    string Name,
    string? ContactHandle,
    PricingModel PricingModel,
    string? Notes,
    DateTime CreatedAt,
    int ItemCount
);

public record CreateSellerDto(
    [Required, StringLength(200, MinimumLength =1)]string Name,
    string? ContactHandle,
    PricingModel PricingModel,
    string? Notes
);

public record PlacementSlotDto(
    int Id,
    int SellerId,
    string SellerName,
    int Position,
    [Range(0.01,100000)]decimal Price,
    DateTime StartDate,
    DateTime? EndDate,
    bool IsActive
);

public record CreatePlacementSlotDto(
    int SellerId,
    [Range(1, int.MaxValue, ErrorMessage = "Position must be at least 1.")] int Position,
    [Range(0.01, 100000)] decimal Price,
    DateTime StartDate,
    DateTime? EndDate
);
