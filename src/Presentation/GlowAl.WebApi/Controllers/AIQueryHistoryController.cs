using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GlowAl.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class AIQueryHistoryController : ControllerBase
{
    private readonly IAIQueryHistoryService _historyService;

    public AIQueryHistoryController(IAIQueryHistoryService historyService)
    {
        _historyService = historyService;
    }

    [HttpGet("my")]
    [Authorize] // yalnız login olan istifadəçilər görə bilər
    public async Task<IActionResult> GetMyHistory()
    {
        var userIdString = User.FindFirst("id")?.Value;
        if (!Guid.TryParse(userIdString, out Guid userId))
            return Unauthorized();

        var history = await _historyService.GetByUserIdAsync(userId);
        return Ok(history);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddHistory([FromBody] AIQueryHistory history)
    {
        var userIdString = User.FindFirst("id")?.Value;
        if (!Guid.TryParse(userIdString, out Guid userId))
            return Unauthorized();

        history.UserId = userId;
        var added = await _historyService.AddAsync(history);
        return Ok(added);
    }
}
