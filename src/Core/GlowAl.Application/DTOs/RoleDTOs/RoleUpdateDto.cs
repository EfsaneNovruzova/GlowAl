namespace GlowAl.Application.DTOs.RoleDTOs;

public class RoleUpdateDto
{
    public string? NewName { get; set; }
    public List<string> PermissionList { get; set; } = new();
}
