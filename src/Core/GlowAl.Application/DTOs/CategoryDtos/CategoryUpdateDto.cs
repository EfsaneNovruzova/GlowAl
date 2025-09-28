﻿namespace GlowAl.Application.DTOs.CategoryDtos;

public class CategoryUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
}
