using System.Net;
using System.Text;
using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.AppUserDtos;
using GlowAl.Application.Shared;
using GlowAl.Application.Shared.Responses;
using GlowAl.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace GlowAl.Persistence.Services;

public class AuthService : IAuthService
{
    private UserManager<AppUser> _usermanager { get; set; }
    public AuthService(UserManager<AppUser> userManage)
    {
        _usermanager = userManage;
    }
    public async Task<BaseResponse<string>> Register(AppUserRegisterDto dto)
    {
        var existedEmail = await _usermanager.FindByEmailAsync(dto.Email);
        if (existedEmail is not null)
        {
            return new BaseResponse<string>("This account already exist", HttpStatusCode.BadRequest);
        }

        AppUser newUser = new()
        {
            Email = dto.Email,
            FulName = dto.FulName,
            UserName = dto.Email,
           // RefreshToken = GenerateRefreshToken(),
            ExpiryDate = DateTime.UtcNow.AddHours(2)
        };

        IdentityResult identityResult = await _usermanager.CreateAsync(newUser, dto.Password);
        if (!identityResult.Succeeded)
        {
            var errors = identityResult.Errors;
            StringBuilder errorMessage = new();
            foreach (var error in errors)
            {
                errorMessage.Append(error.Description + " ");
            }

            return new BaseResponse<string>(errorMessage.ToString(), HttpStatusCode.BadRequest);
        }

        //string confirmEmailLink = await GetEmailConfirmlink(newUser);
        //await _emailService.SendEmailAsync(new List<string> { newUser.Email }, "Email Confirmation",
        //    confirmEmailLink);
        return new("Successfully created", HttpStatusCode.Created);
    }
    public Task<BaseResponse<TokenResponse>> Login(AppUserLoginDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        throw new NotImplementedException();
    }

   
}
