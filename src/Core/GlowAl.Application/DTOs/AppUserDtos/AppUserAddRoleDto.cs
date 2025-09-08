namespace GlowAl.Application.DTOs.AppUserDtos;

public class AppUserAddRoleDto
{
    public Guid UserId { get; set; }
    public List<Guid> RolesId { get; set; }
}
