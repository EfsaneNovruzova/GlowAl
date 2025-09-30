using System.ComponentModel.DataAnnotations;

public class ArticleCreateDto
{
    [Required, MaxLength(200)]
    public string Title { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;

    public Guid? SkinProblemId { get; set; }
    public Guid? SkinTypeId { get; set; }
    public Guid? ProductId { get; set; }
}
