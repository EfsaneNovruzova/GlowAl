using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.CareProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class CareProductsController : ControllerBase
{
    private readonly ICareProductService _service;
    private readonly ICareProductAIService _careProductAIService;

    public CareProductsController(ICareProductService service, ICareProductAIService careProductAIService)
    {
        _service = service;
        _careProductAIService = careProductAIService;
    }

    // Create
    [HttpPost]
    [Authorize(Roles = "Seller,Admin")]
    public async Task<IActionResult> Create([FromBody] CareProductCreateDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _service.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
    [HttpPost("ai-recommendation")]
    [Authorize] // optional
    public async Task<IActionResult> GetAIRecommendations([FromBody] AIQueryDto dto)
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Guid? userId = null;
        if (!string.IsNullOrEmpty(userIdStr))
            userId = Guid.Parse(userIdStr);

        var result = await _careProductAIService.GetRecommendationsAsync(dto.Query, userId);
        return Ok(result);
    }

    // Update
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Seller,Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CareProductUpdateDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _service.UpdateAsync(id, dto, userId);
        return Ok(result);
    }

    // Delete (hard delete)
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Seller,Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        await _service.DeleteAsync(id, userId);
        return NoContent();
    }

    // Get by Id
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    // Get all with filter, pagination, sort
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CareProductFilter filter)
    {
        var result = await _service.GetAllAsync(filter);
        return Ok(result);
    }
    [HttpPost("by-skin-problem")]
    [ProducesResponseType(typeof(List<CareProductGetDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBySkinProblems([FromBody] SkinProblemQueryDto dto)
    {
        var products = await _service.GetBySkinProblemsAsync(dto);
        return Ok(products);
    }


    // Optional: My Products (for current seller)
    [HttpGet("my")]
    [Authorize(Roles = "Seller,Admin")]
    public async Task<IActionResult> GetMyProducts([FromQuery] CareProductFilter filter)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var allProducts = await _service.GetAllAsync(filter);
        var myProducts = allProducts.Items.Where(p => p.CreatedByUserId == userId).ToList();

        return Ok(new PagedResult<CareProductGetDto>
        {
            Items = myProducts,
            TotalCount = myProducts.Count,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        });
    }


}


