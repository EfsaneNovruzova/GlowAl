using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.AppUserDtos;
using GlowAl.Application.Shared;
using GlowAl.Application.Shared.Responses;
using GlowAl.Application.Shared.Settings;
using GlowAl.Domain.Entities;
using GlowAl.Persistence.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TokenResponse = GlowAl.Application.Shared.Responses.TokenResponse;

namespace GlowAl.Persistence.Services;

public class AuthService : IAuthService
{
    private UserManager<AppUser> _usermanager { get;  }
    private SignInManager<AppUser> _signInManager { get; }
    private JWTSettings _jwtsettings { get; }
    private RoleManager<IdentityRole> _roleManager { get; }
    public AuthService(UserManager<AppUser> userManage,
        SignInManager<AppUser>signInManager,
       IOptions <JWTSettings> jwtsettings,
       RoleManager<IdentityRole> roleManager)
    {
        _usermanager = userManage;
        _signInManager = signInManager;
        _jwtsettings = jwtsettings.Value;
        _roleManager = roleManager;
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
            //RefreshToken = GenerateRefreshToken(),
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
    public async Task<BaseResponse<TokenResponse>> Login(AppUserLoginDto dto)
    {
        var existedUser = await _usermanager.FindByEmailAsync(dto.Email);
        if (existedUser is null)
        {
            return new("Emaail or Password is wrong.", null, HttpStatusCode.NotFound);
        }

        //if (!existedUser.EmailConfirmed)
        //{
        //    return new("Pleace confirm your email", HttpStatusCode.BadRequest);
        //}


        SignInResult signInResult = await _signInManager.PasswordSignInAsync
            (dto.Email, dto.Password, true, true);

        if (!signInResult.Succeeded)
        {

            return new("Emaail or Password is wrong.", null, HttpStatusCode.NotFound);
        }
        var token = GenerateJwtToken(dto.Email);
        var expires = DateTime.UtcNow.AddMinutes(_jwtsettings.ExpiresInMinutes);
        TokenResponse tokenResponse = new()
        {
             Token = token,
            ExpireDate = expires,
        };
        return new("Token generated", tokenResponse, HttpStatusCode.OK);
    }

    public Task<BaseResponse<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        throw new NotImplementedException();
    }

    private string GenerateJwtToken(string userEmail)
    {
        var claims = new[]
        {
        new Claim(ClaimTypes.Email, userEmail),
        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtsettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtsettings.Issuer,
            audience: _jwtsettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtsettings.ExpiresInMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


}
