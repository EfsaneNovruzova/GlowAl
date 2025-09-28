using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.CareProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class CareProductsController : ControllerBase
{
    private readonly ICareProductService _service;

    public CareProductsController(ICareProductService service)
    {
        _service = service;
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


