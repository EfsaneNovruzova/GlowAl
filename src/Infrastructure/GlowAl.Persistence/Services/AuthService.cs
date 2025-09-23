using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.AppUserDtos;
using GlowAl.Application.Shared;
using GlowAl.Application.Shared.Responses;
using GlowAl.Application.Shared.Settings;
using GlowAl.Domain.Entities;
using GlowAl.Infrastructure.Services;
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
    private IEmailService _emailService { get; }
    private JWTSettings _jwtsettings { get; }
    private RoleManager<IdentityRole> _roleManager { get; }
    public AuthService(UserManager<AppUser> userManage,
        SignInManager<AppUser>signInManager,
       IOptions <JWTSettings> jwtsettings,
       RoleManager<IdentityRole> roleManager,
       IEmailService emailService)
    {
        _usermanager = userManage;
        _signInManager = signInManager;
        _jwtsettings = jwtsettings.Value;
        _roleManager = roleManager;
        _emailService = emailService;
        
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
            RefreshToken = GenerateRefreshToken(),
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

        string confirmEmailLink = await GetEmailConfirmlink(newUser);
        await _emailService.SendEmailAsync(new List<string> { newUser.Email }, "Email Confirmation",
            confirmEmailLink);
        return new("Successfully created", HttpStatusCode.Created);
    }
    public async Task<BaseResponse<TokenResponse>> Login(AppUserLoginDto dto)
    {
        var existedUser = await _usermanager.FindByEmailAsync(dto.Email);
        if (existedUser is null)
        {
            return new("Emaail or Password is wrong.", null, HttpStatusCode.NotFound);
        }

        if (!existedUser.EmailConfirmed)
        {
            return new("Pleace confirm your email", HttpStatusCode.BadRequest);
        }


        SignInResult signInResult = await _signInManager.PasswordSignInAsync
            (dto.Email, dto.Password, true, true);

        if (!signInResult.Succeeded)
        {

            return new("Emaail or Password is wrong.", null, HttpStatusCode.NotFound);
        }
        var token = await GenerateTokensAsync(existedUser);
        return new("Token generated", token, HttpStatusCode.OK);
    }
    public async Task<BaseResponse<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var principal = GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal == null)
            return new BaseResponse<TokenResponse>("Invalid access token", null, HttpStatusCode.BadRequest);

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _usermanager.FindByIdAsync(userId!);
        if (user == null)
        {
            return new BaseResponse<TokenResponse>("User not found", null, HttpStatusCode.NotFound);
        }
        if (user.RefreshToken is null || user.RefreshToken != request.RefreshToken ||
            user.ExpiryDate < DateTime.UtcNow)
        {
            return new("invalid refresh token", null, HttpStatusCode.BadRequest);
        }
        var tokenResponse = await GenerateTokensAsync(user);
        return new("Refreshed", tokenResponse, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> AddRole(AppUserAddRoleDto dto)
    {
        var user = await _usermanager.FindByIdAsync(dto.UserId.ToString());
        if (user == null)
        {
            return new BaseResponse<string>("User not Found.", HttpStatusCode.NotFound);
        }
        var roleNames = new List<string>();

        foreach (var roleId in dto.RolesId.Distinct())
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                return new BaseResponse<string>($"Role with Id '{roleId}' not found.",
                   HttpStatusCode.NotFound);
            }
            if (!await _usermanager.IsInRoleAsync(user, role.Name!))
            {
                var result = await _usermanager.AddToRoleAsync(user, role.Name!);
                if (!result.Succeeded)
                {
                    var errors = string.Join(";", result.Errors.Select(e => e.Description));
                    return new BaseResponse<string>($"Failed to add role ;{role.Name} to user:{errors}", HttpStatusCode.BadRequest);
                }
                roleNames.Add(role.Name!);
            }
        }
        return new BaseResponse<string>
            (
            $"Successfully added roles :{string.Join(",", roleNames)} to user.", HttpStatusCode.OK
            );
    }
    private async Task<TokenResponse> GenerateTokensAsync(AppUser appuser)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtsettings.SecretKey);

        var claims = new List<Claim>
        {
           new Claim(ClaimTypes.NameIdentifier, appuser.Id),
           new Claim(ClaimTypes.Email, appuser.Email ?? string.Empty)
        };

        var roles = await _usermanager.GetRolesAsync(appuser);

        foreach (var roleName in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, roleName));

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                var permissionClaims = roleClaims
                    .Where(c => c.Type == "Permission")
                    .Distinct()
                    .ToList();

                foreach (var permissionClaim in permissionClaims)
                {
                    if (!claims.Any(c => c.Type == "Permission" && c.Value == permissionClaim.Value))
                    {
                        claims.Add(new Claim("Permission", permissionClaim.Value));
                    }
                }
            }
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtsettings.ExpiresInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _jwtsettings.Issuer,
            Audience = _jwtsettings.Audience
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiryDate = DateTime.UtcNow.AddHours(2);

        appuser.RefreshToken = refreshToken;
        appuser.ExpiryDate = refreshTokenExpiryDate;
        await _usermanager.UpdateAsync(appuser);

        return new TokenResponse
        {
            Token = jwt,
            RefreshToken = refreshToken,
            ExpireDate = tokenDescriptor.Expires
        };
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            ValidIssuer = _jwtsettings.Issuer,
            ValidAudience = _jwtsettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtsettings.SecretKey))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,
                out var securityToken);


            if (securityToken is JwtSecurityToken jwtSecurityToken &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }
        }
        catch
        {
            return null;
        }
        return null;
    }
    private async Task<string> GetEmailConfirmlink(AppUser appuser)
    {
        var token = await _usermanager.GenerateEmailConfirmationTokenAsync(appuser);
        var link = $"https://localhost:7241/api/Auth/ConfirmEmail?userId={appuser.Id}&token={HttpUtility.UrlEncode(token)}";
        Console.WriteLine("Confirm emial"+link);
        return link;
    }


    public async Task<BaseResponse<string>> ConfirmEmail(string userId, string token)
    {
        var existedUser = await _usermanager.FindByIdAsync(userId);
        if (existedUser is null)
        {
            return new("Email Confirmation Failed.", HttpStatusCode.NotFound);
        }
        var result = await _usermanager.ConfirmEmailAsync(existedUser, token);
        if (!result.Succeeded)
        {
            return new("Email Confirmation Failed.", HttpStatusCode.BadRequest);
        }
        return new("Email Confirmation successfully.", HttpStatusCode.OK);
    }
}
