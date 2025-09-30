using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.FildeDtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly IFileService _fileService;

    public FilesController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] FileUploadRequestDto dto)
    {
        var url = await _fileService.UploadFileAsync(dto);
        return Ok(new { Url = url });
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] string publicId)
    {
        var result = await _fileService.DeleteFileAsync(publicId);
        return Ok(new { Deleted = result });
    }
}
