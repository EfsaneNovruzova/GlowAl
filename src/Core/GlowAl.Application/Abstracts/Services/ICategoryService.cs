using GlowAl.Application.DTOs.CategoryDtos;
using GlowAl.Application.Shared.Responses;

namespace GlowAl.Application.Abstracts.Services;

public interface ICategoryService
{
    Task<BaseResponse<CategoryGetDto>> AddAsync(CategoryCreateDto dto);

   
    Task<BaseResponse<string>> DeleteAsync(Guid id);

   
    Task<BaseResponse<CategoryGetDto>> UpdateAsync(CategoryUpdateDto dto);

   
    Task<BaseResponse<CategoryGetDto>> GetByIdAsync(Guid id);


    Task<BaseResponse<CategoryGetDto>> GetByNameAsync(string search);

    
    Task<BaseResponse<List<CategoryGetDto>>> GetAllAsync();
}

