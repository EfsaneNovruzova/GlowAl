using GlowAl.Application.DTOs.RoleDTOs;
using GlowAl.Application.Shared.Responses;
using Microsoft.AspNetCore.Identity;

namespace GlowAl.Application.Abstracts.Services;

public interface IRoleService
{
    Task<BaseResponse<string?>> CreateRole(RoleCreateDto dto);
    Task<BaseResponse<List<IdentityRole>>> GetAllRoles();

    Task<BaseResponse<string?>> DeleteRole(string roleName);
    Task<BaseResponse<string?>> UpdateRoleAsync(string roleName, RoleUpdateDto dto);
}

