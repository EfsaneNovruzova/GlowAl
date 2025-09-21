using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.AppUserDtos;
using GlowAl.Application.Shared.Responses;
using GlowAl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace GlowAl.Persistence.Services;

public class AppUserService : IAppUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AppUserService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<BaseResponse<string>> AddRole(AppUserAddRoleDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId.ToString());
        if (user == null)
            return new BaseResponse<string>("User not found", HttpStatusCode.NotFound);

        foreach (var roleId in dto.RolesId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                continue;

            if (!await _userManager.IsInRoleAsync(user, role.Name))
            {
                await _userManager.AddToRoleAsync(user, role.Name);
            }
        }

        return new BaseResponse<string>("Roles added successfully", HttpStatusCode.OK);
    }

    public async Task<List<AppUserGetDto>> GetAllUsersAsync()
    {
        var users = _userManager.Users.ToList();
        return users.Select(u => new AppUserGetDto
        {
            Id = u.Id,
            Email = u.Email!,
            FulName = u.FulName
        }).ToList();
    }

    public async Task<AppUserGetDto?> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return null;

        return new AppUserGetDto
        {
            Id = user.Id,
            Email = user.Email!,
            FulName = user.FulName
        };
    }

    public async Task<AppUserGetDto?> GetCurrentUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return null;

        return new AppUserGetDto
        {
            Id = user.Id,
            Email = user.Email!,
            FulName = user.FulName
        };
    }

    public async Task<BaseResponse<List<string>>> GetUserRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return new BaseResponse<List<string>>("User not found", null, HttpStatusCode.NotFound);

        var roles = await _userManager.GetRolesAsync(user);
        return new BaseResponse<List<string>>("User roles retrieved", roles.ToList(), HttpStatusCode.OK);
    }

    public async Task<bool> AddRoleToUserAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        var result = await _userManager.AddToRoleAsync(user, role);
        return result.Succeeded;
    }

    public async Task<bool> RemoveUserRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        var result = await _userManager.RemoveFromRoleAsync(user, role);
        return result.Succeeded;
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }
}

