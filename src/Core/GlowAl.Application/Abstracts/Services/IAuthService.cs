using GlowAl.Application.DTOs.AppUserDtos;
using GlowAl.Application.Shared;
using GlowAl.Application.Shared.Responses;

namespace GlowAl.Application.Abstracts.Services;

public interface IAuthService
{
    Task<BaseResponse<string>> Register(AppUserRegisterDto dto);
    Task<BaseResponse<TokenResponse>> Login(AppUserLoginDto dto);
    Task<BaseResponse<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
}
