using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AIController : ControllerBase
{
    private readonly IAIService _aiService;

    public AIController(IAIService aiService)
    {
        _aiService = aiService;
    }

    [HttpPost("chat")]
    [Authorize]
    public async Task<IActionResult> Chat([FromBody] string prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt))
            return BadRequest("Prompt boş ola bilməz.");

        // Burada yalnız nameid götürürük
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        Guid? userId = Guid.TryParse(userIdString, out Guid guid) ? guid : (Guid?)null;

        if (!userId.HasValue)
            return Unauthorized();

        var response = await _aiService.SendMessageAsync(prompt, userId);
        return Ok(new { message = response });
    }

    [HttpGet("history")]
    [Authorize]
    public async Task<IActionResult> MyHistory([FromServices] IAIQueryHistoryService historyService)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdString, out Guid userId))
            return Unauthorized();

        var history = await historyService.GetByUserIdAsync(userId);
        return Ok(history);
    }
}
