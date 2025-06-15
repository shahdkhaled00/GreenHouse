using Greenhouse.DTOs;
using Greenhouse.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// Controllers/ChatController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace Greenhouse.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] ChatRequest request)
    {
        if (string.IsNullOrEmpty(request.UserInput))
            return BadRequest("User input is required.");

        try
        {
            var reply = await _chatService.GetChatReplyAsync(request.UserInput, request.CurrentOutput ?? "");
            return Ok(new { assistant_reply = reply });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}

