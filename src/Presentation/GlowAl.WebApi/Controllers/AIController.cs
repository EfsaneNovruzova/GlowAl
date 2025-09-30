using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GlowAl.Application.AI;

namespace GlowAl.WebApi.Controllers
{
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
        public async Task<IActionResult> Chat([FromBody] string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                return BadRequest("Prompt boş ola bilməz.");

            var response = await _aiService.SendMessageAsync(prompt);
            return Ok(new { message = response });
        }
    }
}



