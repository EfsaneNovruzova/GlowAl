using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.RoleDTOs;
using GlowAl.Application.Shared;
using GlowAl.Application.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlowAl.WebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;


    namespace YourNamespace.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class RolesController : ControllerBase
        {
            private readonly IRoleService _roleService;

            public RolesController(IRoleService roleService)
            {
                _roleService = roleService;
            }

            // POST: api/roles
            [HttpPost]
            [Authorize(Policy = Permissions.Role.Create)]
            public async Task<IActionResult> Create([FromBody] RoleCreateDto dto)
            {
                var result = await _roleService.CreateRole(dto);
                return StatusCode((int)result.StatusCode, result);
            }

            // GET: api/roles/permissions
            [HttpGet("permissions")]
            public IActionResult GetAllPermissions()
            {
                var permissions = PermissionHelper.GetAllPermissions();
                return Ok(permissions);
            }

            // GET: api/roles/all
            [HttpGet("all")]
            public async Task<IActionResult> GetAllRoles()
            {
                var result = await _roleService.GetAllRoles();
                return StatusCode((int)result.StatusCode, result);
            }

            // DELETE: api/roles/{roleName}
            [HttpDelete("{roleName}")]
            [Authorize(Policy = Permissions.Role.Delete)]
            public async Task<IActionResult> DeleteRole(string roleName)
            {
                var result = await _roleService.DeleteRole(roleName);
                return StatusCode((int)result.StatusCode, result);
            }

            // PUT: api/roles/{roleName}
            [HttpPut("{roleName}")]
            [Authorize(Policy = Permissions.Role.Update)]
            public async Task<IActionResult> UpdateRole(string roleName, [FromBody] RoleUpdateDto dto)
            {
                var result = await _roleService.UpdateRoleAsync(roleName, dto);
                return StatusCode((int)result.StatusCode, result);
            }
        }
    }
}