public class CareProductGetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Ingredients { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string ImageUrl { get; set; } = null!;
    public double Rating { get; set; }
    public int SalesCount { get; set; }
    public Guid? SkinTypeId { get; set; }
    public Guid CategoryId { get; set; }
    public string CreatedByUserId { get; set; } = null!;
}


