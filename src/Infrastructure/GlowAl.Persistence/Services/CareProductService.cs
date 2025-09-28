using AutoMapper;
using GlowAl.Application.Abstracts.Repositories;
using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.CareProductDtos;
using GlowAl.Domain.Entities;
using Microsoft.Extensions.Logging;

public class CareProductService : ICareProductService
{
    private readonly ICareProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CareProductService> _logger;

    public CareProductService(ICareProductRepository repository, IMapper mapper, ILogger<CareProductService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CareProductGetDto> CreateAsync(CareProductCreateDto dto, string userId)
    {
        var entity = _mapper.Map<CareProduct>(dto);
        entity.Id = Guid.NewGuid();
        entity.CreatedByUserId = userId;

        await _repository.AddAsync(entity);
        await _repository.SaveChangeAsync();

        _logger.LogInformation("CareProduct {Id} created by user {UserId}", entity.Id, userId);

        return _mapper.Map<CareProductGetDto>(entity);
    }

    public async Task<CareProductGetDto> UpdateAsync(Guid id, CareProductUpdateDto dto, string userId)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("CareProduct tapılmadı.");
        if (entity.CreatedByUserId != userId) throw new UnauthorizedAccessException("Update icazəniz yoxdur.");

        _mapper.Map(dto, entity);

        _repository.Update(entity);
        await _repository.SaveChangeAsync();

        _logger.LogInformation("CareProduct {Id} updated by user {UserId}", entity.Id, userId);

        return _mapper.Map<CareProductGetDto>(entity);
    }

    public async Task DeleteAsync(Guid id, string userId)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("CareProduct tapılmadı.");
        if (entity.CreatedByUserId != userId) throw new UnauthorizedAccessException("Delete icazəniz yoxdur.");

        // Hard delete
        _repository.Delete(entity);
        await _repository.SaveChangeAsync();

        _logger.LogInformation("CareProduct {Id} deleted by user {UserId}", entity.Id, userId);
    }

    public async Task<CareProductGetDto> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("CareProduct tapılmadı.");

        return _mapper.Map<CareProductGetDto>(entity);
    }

    public async Task<PagedResult<CareProductGetDto>> GetAllAsync(CareProductFilter filter)
    {
        var query = _repository.GetAllFiltered();

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(p => p.Name.Contains(filter.Name));

        if (filter.CategoryId.HasValue)
            query = query.Where(p => p.CategoryId == filter.CategoryId);

        if (filter.MinPrice.HasValue)
            query = query.Where(p => p.Price >= filter.MinPrice.Value);

        if (filter.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= filter.MaxPrice.Value);

        // Sorting
        query = filter.SortBy?.ToLower() switch
        {
            "price" => filter.SortAsc ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price),
            "name" => filter.SortAsc ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name),
            "rating" => filter.SortAsc ? query.OrderBy(p => p.Rating) : query.OrderByDescending(p => p.Rating),
            _ => query.OrderBy(p => p.Name)
        };

        var totalCount = query.Count();

        var items = query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(p => _mapper.Map<CareProductGetDto>(p))
            .ToList();

        return new PagedResult<CareProductGetDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }
}

