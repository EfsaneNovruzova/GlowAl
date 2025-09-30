public class ArticleGetDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public Guid? SkinProblemId { get; set; }
    public string? SkinProblemName { get; set; }
    public Guid? SkinTypeId { get; set; }
    public string? SkinTypeName { get; set; }
    public Guid? ProductId { get; set; }
    public string? ProductName { get; set; }
}