using GlowAl.Application.Shared.Helpers;
using GlowAl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GlowAl.Application;

public static class SeedData
{


    public static async Task CreateAdminUser(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string adminEmail = "novruzovafsan88@gmail.com";
        string adminPassword = "Efsane0608@";

        // 1. Admin rolunu yoxla, yoxdursa yarat
        var adminRole = await roleManager.FindByNameAsync("Admin");
        if (adminRole == null)
        {
            adminRole = new IdentityRole("Admin");
            await roleManager.CreateAsync(adminRole);
        }

        // 2. Admin istifadəçini yoxla, yoxdursa yarat
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new AppUser
            {
                Email = adminEmail,
                UserName = adminEmail,
                FulName = "Administrator"
            };
            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Admin user creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        // 3. Admin istifadəçiyə Admin rolunu əlavə et
        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        // 4. Admin roluna lazımi icazələri əlavə et (claims)
        var claims = await roleManager.GetClaimsAsync(adminRole);

        var permissions = PermissionHelper.GetAllPermissionList();  // bütün icazələri al

        foreach (var permission in permissions)
        {
            if (!claims.Any(c => c.Type == "Permission" && c.Value == permission))
            {
                await roleManager.AddClaimAsync(adminRole, new Claim("Permission", permission));
            }
        }
    }

}


