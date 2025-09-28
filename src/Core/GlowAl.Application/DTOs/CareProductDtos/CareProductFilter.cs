namespace GlowAl.Application.DTOs.CareProductDtos;

public class CareProductFilter
{
    public string? Name { get; set; }
    public Guid? CategoryId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; } = "Name";
    public bool SortAsc { get; set; } = true;
}

