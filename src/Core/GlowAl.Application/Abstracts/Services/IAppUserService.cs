using GlowAl.Application.DTOs.AppUserDtos;
using GlowAl.Application.Shared.Responses;

namespace GlowAl.Application.Abstracts.Services;

public interface IAppUserService
{
    Task<BaseResponse<string>> AddRole(AppUserAddRoleDto dto);
    Task<List<AppUserGetDto>> GetAllUsersAsync();
    Task<AppUserGetDto> GetUserByIdAsync(string userId);
    Task<AppUserGetDto> GetCurrentUserAsync(string userId);
    Task<BaseResponse<List<string>>> GetUserRolesAsync(string userId);

    Task<bool> AddRoleToUserAsync(string userId, string role);
    Task<bool> RemoveUserRoleAsync(string userId, string role);

    Task<bool> DeleteUserAsync(string userId);
}
