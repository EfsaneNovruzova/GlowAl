using Microsoft.AspNetCore.Http;

namespace GlowAl.Application.DTOs.FildeDtos;

public class FileUploadRequestDto
{
    public IFormFile File { get; set; } = null!;
    public string? Folder { get; set; } // optional, faylı hansı qovluğa yükləyək
}
