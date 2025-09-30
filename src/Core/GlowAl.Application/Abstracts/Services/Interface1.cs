using GlowAl.Application.DTOs.FildeDtos;

namespace GlowAl.Application.Abstracts.Services;

public interface IFileService
{
    Task<string> UploadFileAsync(FileUploadRequestDto dto);
    Task<bool> DeleteFileAsync(string publicId);
}
