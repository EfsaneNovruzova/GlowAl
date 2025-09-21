using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.AppUserDtos;
using GlowAl.Application.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GlowAl.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Hər endpoint üçün giriş tələb olunur
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        // Bütün istifadəçiləri gətir (Admin)
        [HttpGet]
        [Authorize(Policy = "Account.GetAllUsers")]
        public async Task<ActionResult<List<AppUserGetDto>>> GetAllUsers()
        {
            var users = await _appUserService.GetAllUsersAsync();
            return Ok(users);
        }

        // İstifadəçini ID ilə gətir (Admin / Moderator)
        [HttpGet("{userId}")]
        [Authorize(Policy = "Account.GetAllUsers")]
        public async Task<ActionResult<AppUserGetDto>> GetUserById(string userId)
        {
            var user = await _appUserService.GetUserByIdAsync(userId);
            if (user == null) return NotFound("User not found");
            return Ok(user);
        }

        // Hazırda daxil olmuş istifadəçini gətir
        [HttpGet("current")]
        public async Task<ActionResult<AppUserGetDto>> GetCurrentUser()
        {
            var userId = User.FindFirst("sub")?.Value;
            if (userId == null) return Unauthorized();
            var user = await _appUserService.GetCurrentUserAsync(userId);
            return Ok(user);
        }

        // İstifadəçinin rollarını göstər (Admin / Moderator)
        [HttpGet("{userId}/roles")]
        [Authorize(Policy = "Account.GetAllUsers")]
        public async Task<ActionResult<BaseResponse<List<string>>>> GetUserRoles(string userId)
        {
            var roles = await _appUserService.GetUserRolesAsync(userId);
            return StatusCode((int)roles.StatusCode, roles);
        }

        // İstifadəçiyə rol əlavə et (Admin)
        [HttpPost("add-role")]
        [Authorize(Policy = "Account.AddRole")]
        public async Task<ActionResult<BaseResponse<string>>> AddRoleToUser(AppUserAddRoleDto dto)
        {
            var result = await _appUserService.AddRole(dto);
            return StatusCode((int)result.StatusCode, result);
        }

        // İstifadəçiyə bir rol əlavə et (Admin)
        [HttpPost("{userId}/role/{role}")]
        [Authorize(Policy = "Account.AddRole")]
        public async Task<ActionResult> AddSingleRoleToUser(string userId, string role)
        {
            var success = await _appUserService.AddRoleToUserAsync(userId, role);
            if (!success) return BadRequest("Could not add role");
            return Ok("Role added");
        }

        // İstifadəçidən rol sil (Admin)
        [HttpDelete("{userId}/role/{role}")]
        [Authorize(Policy = "Account.AddRole")]
        public async Task<ActionResult> RemoveRoleFromUser(string userId, string role)
        {
            var success = await _appUserService.RemoveUserRoleAsync(userId, role);
            if (!success) return BadRequest("Could not remove role");
            return Ok("Role removed");
        }

        // İstifadəçini sil (Admin)
        [HttpDelete("{userId}")]
        [Authorize(Policy = "Account.Create")] // delete user icazəsi Admina
        public async Task<ActionResult> DeleteUser(string userId)
        {
            var success = await _appUserService.DeleteUserAsync(userId);
            if (!success) return BadRequest("Could not delete user");
            return Ok("User deleted");
        }
    }
}

