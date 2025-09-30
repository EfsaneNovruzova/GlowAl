using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace GlowAl.Application.DTOs.CareProductDtos;

public class CareProductCreateDto
{
    [Required, MaxLength(150)]
    public string Name { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Brand { get; set; } = null!;

    [Required, MaxLength(1000)]
    public string Description { get; set; } = null!;

    [Required, MaxLength(500)]
    public string Ingredients { get; set; } = null!;

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; } = 0;

    [Required]
    public string ImageUrl { get; set; } = null!;

    public Guid? SkinTypeId { get; set; }

    [Required]
    public Guid CategoryId { get; set; }
    public List<Guid> SkinProblemIds { get; set; } = new();
}