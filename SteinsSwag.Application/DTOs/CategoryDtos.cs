using System;
using System.Collections.Generic;
using System.Text;

namespace SteinsSwag.Application.DTOs;

public record CategoryDto(
    int Id,
    string Name,
    int ItemCount
);
public record CreateCategoryDto(string Name);

