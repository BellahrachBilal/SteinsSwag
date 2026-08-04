using System;
using System.Collections.Generic;
using System.Text;
using SteinsSwag.Domain.Enums;

namespace SteinsSwag.Application.DTOs;

    public record ItemDto(
    int Id,
    string Name,
    string? Description,
    string? Brand,
    decimal Price,
    string? ImageUrl,
    int CategoryId,
    string CategoryName,
    int? SellerId,
    string? SellerName,
    SourcePlatform SourcePlatform,
    ItemCondition Condition,
    ItemStatus Status,
    DateTime CreatedAt
);

    public record CreateItemDto(
        string Name,
        string? Description,
        string? Brand,
        decimal Price,
        string? ImageUrl,
        int CategoryId,
        int? SellerId,
        SourcePlatform SourcePlatform,
        ItemCondition Condition
    );

    public record UpdateItemDto(
        string Name,
        string? Description,
        string? Brand,
        decimal Price,
        string? ImageUrl,
        int CategoryId,
        int? SellerId,
        SourcePlatform SourcePlatform,
        ItemCondition Condition,
        ItemStatus Status
    );

