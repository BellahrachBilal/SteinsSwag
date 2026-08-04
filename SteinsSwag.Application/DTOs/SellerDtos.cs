using SteinsSwag.Domain.Enums;
using System;
using System.Collections.Generic;
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
    string Name,
    string? ContactHandle,
    PricingModel PricingModel,
    string? Notes
);

public record PlacementSlotDto(
    int Id,
    int SellerId,
    string SellerName,
    int Position,
    decimal Price,
    DateTime StartDate,
    DateTime? EndDate,
    bool IsActive
);

public record CreatePlacementSlotDto(
    int SellerId,
    int Position,
    decimal Price,
    DateTime StartDate,
    DateTime? EndDate
);
