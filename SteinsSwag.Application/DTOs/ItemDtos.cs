using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required, StringLength(200, MinimumLength =1)] string Name,
        string? Description,
        string? Brand,
        [Range(0.01,100000)]decimal Price,
        string? ImageUrl,
        [Range(1, int.MaxValue,ErrorMessage ="A valid CategoryId is required.")]int CategoryId,
        int? SellerId,
        SourcePlatform SourcePlatform,
        ItemCondition Condition
    );

    public record UpdateItemDto(
        [Required, StringLength(200, MinimumLength = 1)] string Name,
        string? Description,
        string? Brand,
        [Range(0.01, 100000)] decimal Price,
        string? ImageUrl,
        [Range(1, int.MaxValue, ErrorMessage = "A valid CategoryId is required.")] int CategoryId,
        int? SellerId,
        SourcePlatform SourcePlatform,
        ItemCondition Condition,
        ItemStatus Status
    );

