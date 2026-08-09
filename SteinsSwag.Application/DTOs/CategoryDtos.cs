using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SteinsSwag.Application.DTOs;

public record CategoryDto(
    int Id,
    string Name,
    int ItemCount
);
public record CreateCategoryDto([Required, StringLength(200, MinimumLength = 1)] string Name);

