using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.FildeDtos;
using GlowAl.Settings;
using Microsoft.Extensions.Options;

public class FileService : IFileService
{
    private readonly Cloudinary _cloudinary;

    public FileService(IOptions<CloudinarySettings> options)
    {
        var acc = new Account(
            options.Value.CloudName,
            options.Value.ApiKey,
            options.Value.ApiSecret
        );

        _cloudinary = new Cloudinary(acc); 
        _cloudinary.Api.Secure = true;
    }

    public async Task<string> UploadFileAsync(FileUploadRequestDto dto)
    {
        if (dto.File.Length <= 0)
            throw new Exception("File is empty");

        await using var stream = dto.File.OpenReadStream();

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(dto.File.FileName, stream),
            Folder = dto.Folder
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception(result.Error?.Message);

        return result.SecureUrl.AbsoluteUri;
    }

    public async Task<bool> DeleteFileAsync(string publicId)
    {
        var deletionParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deletionParams);

        return result.Result == "ok" || result.Result == "not found";
    }
}

