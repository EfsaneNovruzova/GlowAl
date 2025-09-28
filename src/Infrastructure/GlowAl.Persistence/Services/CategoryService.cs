using GlowAl.Application.Abstracts.Repositories;
using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.CategoryDtos;
using GlowAl.Application.Shared.Responses;
using GlowAl.Domain.Entities;
using GlowAl.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GlowAl.Persistence.Services;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<BaseResponse<CategoryGetDto>> AddAsync(CategoryCreateDto dto)
    {
        var exists = await _categoryRepository
            .GetByFiltered(c => c.Name.ToLower().Trim() == dto.Name.ToLower().Trim())
            .FirstOrDefaultAsync();

        if (exists != null)
        {
            return new BaseResponse<CategoryGetDto>("This category already exists", HttpStatusCode.BadRequest);
        }

        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description,
            ParentId = dto.ParentId
        };

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangeAsync();

        var categoryDto = new CategoryGetDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ParentId = category.ParentId,
            SubCategoriesCount = 0,
            ProductsCount = 0,
            Children = new List<CategoryGetDto>()
        };

        return new BaseResponse<CategoryGetDto>(
            "Category created successfully",
            categoryDto,
            HttpStatusCode.Created);
    }

    public async Task<BaseResponse<CategoryGetDto>> UpdateAsync(CategoryUpdateDto dto)
    {
        var categoryDb = await _categoryRepository.GetByIdAsync(dto.Id);
        if (categoryDb == null)
            return new BaseResponse<CategoryGetDto>("Category not found", HttpStatusCode.NotFound);

        var exists = await _categoryRepository
            .GetByFiltered(c => c.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && c.Id != dto.Id)
            .FirstOrDefaultAsync();

        if (exists != null)
            return new BaseResponse<CategoryGetDto>("This category already exists", HttpStatusCode.BadRequest);

        categoryDb.Name = dto.Name;
        categoryDb.Description = dto.Description;
        categoryDb.ParentId = dto.ParentId;

        _categoryRepository.Update(categoryDb);
        await _categoryRepository.SaveChangeAsync();

        var updatedDto = new CategoryGetDto
        {
            Id = categoryDb.Id,
            Name = categoryDb.Name,
            Description = categoryDb.Description,
            ParentId = categoryDb.ParentId
        };

        return new BaseResponse<CategoryGetDto>("Category updated successfully", updatedDto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            return new BaseResponse<string>("Category not found", HttpStatusCode.NotFound);

        _categoryRepository.Delete(category);
        await _categoryRepository.SaveChangeAsync();

        return new BaseResponse<string>("Category deleted successfully", HttpStatusCode.OK);
    }

    public async Task<BaseResponse<CategoryGetDto>> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            return new BaseResponse<CategoryGetDto>("Category not found", HttpStatusCode.NotFound);

        var dto = new CategoryGetDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ParentId = category.ParentId,
            SubCategoriesCount = category.SubCategories?.Count ?? 0,
            ProductsCount = category.Products?.Count ?? 0,
            Children = category.SubCategories?
                        .Select(sc => new CategoryGetDto
                        {
                            Id = sc.Id,
                            Name = sc.Name,
                            Description = sc.Description,
                            ParentId = sc.ParentId,
                            SubCategoriesCount = sc.SubCategories?.Count ?? 0,
                            ProductsCount = sc.Products?.Count ?? 0,
                            Children = new List<CategoryGetDto>()
                        }).ToList()
        };

        return new BaseResponse<CategoryGetDto>("Category retrieved successfully", dto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<CategoryGetDto>>> GetAllAsync()
    {
        var allCategories = await _categoryRepository.GetAll().ToListAsync();
        if (allCategories == null || allCategories.Count == 0)
            return new BaseResponse<List<CategoryGetDto>>("No categories found", HttpStatusCode.NotFound);

        var mainCategories = allCategories
            .Where(c => c.ParentId == null)
            .Select(c => MapWithChildren(c, allCategories))
            .ToList();

        return new BaseResponse<List<CategoryGetDto>>(
            "Nested categories retrieved successfully",
            mainCategories,
            HttpStatusCode.OK);
    }

    private CategoryGetDto MapWithChildren(Category category, List<Category> allCategories)
    {
        var children = allCategories.Where(c => c.ParentId == category.Id).ToList();

        return new CategoryGetDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ParentId = category.ParentId,
            SubCategoriesCount = children.Count,
            ProductsCount = category.Products?.Count ?? 0,
            Children = children.Select(child => MapWithChildren(child, allCategories)).ToList()
        };
    }

    public async Task<BaseResponse<CategoryGetDto>> GetByNameAsync(string search)
    {
        var category = await _categoryRepository
            .GetByFiltered(c => c.Name.ToLower().Trim() == search.ToLower().Trim())
            .FirstOrDefaultAsync();

        if (category == null)
            return new BaseResponse<CategoryGetDto>("Category not found", HttpStatusCode.NotFound);

        var dto = new CategoryGetDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ParentId = category.ParentId,
            SubCategoriesCount = category.SubCategories?.Count ?? 0,
            ProductsCount = category.Products?.Count ?? 0,
            Children = new List<CategoryGetDto>()
        };

        return new BaseResponse<CategoryGetDto>("Category retrieved successfully", dto, HttpStatusCode.OK);
    }


}