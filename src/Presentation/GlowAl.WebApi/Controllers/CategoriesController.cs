using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.CategoryDtos;
using GlowAl.Application.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using GlowAl.Application.Shared;

namespace GlowAl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Category.Create)]
        [ProducesResponseType(typeof(BaseResponse<CategoryGetDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BaseResponse<CategoryGetDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<CategoryGetDto>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CategoryCreateDto dto)
        {
            var response = await _categoryService.AddAsync(dto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Permissions.Category.Update)]
        [ProducesResponseType(typeof(BaseResponse<CategoryGetDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<CategoryGetDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<CategoryGetDto>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoryUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new BaseResponse<string>("Id mismatch", HttpStatusCode.BadRequest));

            var response = await _categoryService.UpdateAsync(dto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Category.Delete)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _categoryService.DeleteAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<CategoryGetDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<CategoryGetDto>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("by-name/{search}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<CategoryGetDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<CategoryGetDto>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByName(string search)
        {
            var response = await _categoryService.GetByNameAsync(search);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<List<CategoryGetDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<List<CategoryGetDto>>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
