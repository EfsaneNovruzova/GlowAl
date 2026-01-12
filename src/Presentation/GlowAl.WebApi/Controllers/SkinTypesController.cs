using Microsoft.AspNetCore.Mvc;
using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.SkinTypeDtos;
using GlowAl.Application.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using GlowAl.Application.Shared;

namespace GlowAl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkinTypesController : ControllerBase
    {
        private readonly ISkinTypeService _skinTypeService;

        public SkinTypesController(ISkinTypeService skinTypeService)
        {
            _skinTypeService = skinTypeService;
        }

        [HttpPost]
        [Authorize(Policy = Permissions.SkinType.Create)]
        [ProducesResponseType(typeof(BaseResponse<SkinTypeGetDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BaseResponse<SkinTypeGetDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<SkinTypeGetDto>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Add([FromBody] SkinTypeCreateDto dto)
        {
            var response = await _skinTypeService.AddAsync(dto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Permissions.SkinType.Update)]
        [ProducesResponseType(typeof(BaseResponse<SkinTypeGetDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<SkinTypeGetDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<SkinTypeGetDto>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] SkinTypeUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new BaseResponse<string>("Id mismatch", HttpStatusCode.BadRequest));

            var response = await _skinTypeService.UpdateAsync(dto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.SkinType.Delete)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _skinTypeService.DeleteAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<SkinTypeGetDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<SkinTypeGetDto>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _skinTypeService.GetByIdAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("by-name/{name}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<SkinTypeGetDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<SkinTypeGetDto>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByName(string name)
        {
            var response = await _skinTypeService.GetByNameAsync(name);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<List<SkinTypeGetDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<List<SkinTypeGetDto>>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _skinTypeService.GetAllAsync();
            return StatusCode((int)response.StatusCode, response);
        }
    }
}

