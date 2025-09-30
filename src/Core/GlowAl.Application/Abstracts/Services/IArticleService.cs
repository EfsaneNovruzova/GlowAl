using GlowAl.Application.Shared.Responses;

namespace GlowAl.Application.Abstracts.Services;



public interface IArticleService
{
    Task<ArticleGetDto> CreateAsync(ArticleCreateDto dto);
    Task<ArticleGetDto> UpdateAsync(Guid id, ArticleUpdateDto dto);
    Task DeleteAsync(Guid id);
    Task<ArticleGetDto> GetByIdAsync(Guid id);
    Task<List<ArticleGetDto>> GetAllAsync();

    // Optional filter: SkinProblem, SkinType, Product
    Task<List<ArticleGetDto>> GetByFilterAsync(Guid? skinProblemId = null, Guid? skinTypeId = null, Guid? productId = null);
}

