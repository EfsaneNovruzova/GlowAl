using GlowAl.Application.DTOs.AppUserDtos;
using GlowAl.Application.Shared;
using GlowAl.Application.Shared.Responses;
using GlowAl.Domain.Entities;

namespace GlowAl.Application.Abstracts.Services;

public interface IAuthService
{
    Task<BaseResponse<string>> Register(AppUserRegisterDto dto);
    Task<BaseResponse<TokenResponse>> Login(AppUserLoginDto dto);
    Task<BaseResponse<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<BaseResponse<string>> ConfirmEmail(string userId, string token);
}
