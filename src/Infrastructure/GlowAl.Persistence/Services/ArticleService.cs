using GlowAl.Application.Abstracts.Services;
using GlowAl.Domain.Entities;
using GlowAl.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

public class ArticleService : IArticleService
{
    private readonly GlowAlDbContext _context;

    public ArticleService(GlowAlDbContext context)
    {
        _context = context;
    }

    public async Task<ArticleGetDto> CreateAsync(ArticleCreateDto dto)
    {
        var article = new Article
        {
            Title = dto.Title,
            Content = dto.Content,
            SkinProblemId = dto.SkinProblemId,
            SkinTypeId = dto.SkinTypeId,
            ProductId = dto.ProductId
        };

        _context.Articles.Add(article);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(article.Id);
    }

    public async Task<ArticleGetDto> UpdateAsync(Guid id, ArticleUpdateDto dto)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article == null) throw new Exception("Article not found");

        article.Title = dto.Title;
        article.Content = dto.Content;
        article.SkinProblemId = dto.SkinProblemId;
        article.SkinTypeId = dto.SkinTypeId;
        article.ProductId = dto.ProductId;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(article.Id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article == null) throw new Exception("Article not found");

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();
    }

    public async Task<ArticleGetDto> GetByIdAsync(Guid id)
    {
        var article = await _context.Articles
            .Include(a => a.SkinProblem)
            .Include(a => a.SkinType)
            .Include(a => a.Product)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (article == null) throw new Exception("Article not found");

        return new ArticleGetDto
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content,
            SkinProblemId = article.SkinProblemId,
            SkinProblemName = article.SkinProblem?.Name,
            SkinTypeId = article.SkinTypeId,
            SkinTypeName = article.SkinType?.Name,
            ProductId = article.ProductId,
            ProductName = article.Product?.Name
        };
    }

    public async Task<List<ArticleGetDto>> GetAllAsync()
    {
        return await _context.Articles
            .Include(a => a.SkinProblem)
            .Include(a => a.SkinType)
            .Include(a => a.Product)
            .Select(a => new ArticleGetDto
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                SkinProblemId = a.SkinProblemId,
                SkinProblemName = a.SkinProblem!.Name,
                SkinTypeId = a.SkinTypeId,
                SkinTypeName = a.SkinType!.Name,
                ProductId = a.ProductId,
                ProductName = a.Product!.Name
            })
            .ToListAsync();
    }

    public async Task<List<ArticleGetDto>> GetByFilterAsync(Guid? skinProblemId = null, Guid? skinTypeId = null, Guid? productId = null)
    {
        var query = _context.Articles
            .Include(a => a.SkinProblem)
            .Include(a => a.SkinType)
            .Include(a => a.Product)
            .AsQueryable();

        if (skinProblemId.HasValue) query = query.Where(a => a.SkinProblemId == skinProblemId.Value);
        if (skinTypeId.HasValue) query = query.Where(a => a.SkinTypeId == skinTypeId.Value);
        if (productId.HasValue) query = query.Where(a => a.ProductId == productId.Value);

        return await query.Select(a => new ArticleGetDto
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            SkinProblemId = a.SkinProblemId,
            SkinProblemName = a.SkinProblem!.Name,
            SkinTypeId = a.SkinTypeId,
            SkinTypeName = a.SkinType!.Name,
            ProductId = a.ProductId,
            ProductName = a.Product!.Name
        }).ToListAsync();
    }
}

