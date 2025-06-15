using Greenhouse.DTOs;
using Greenhouse.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatBackController : ControllerBase
    {
        private readonly IChatBotService _chaService;

        public ChatBackController(IChatBotService chaService)
        {
            _chaService = chaService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ChatRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Message))
            {
                return BadRequest("Message is required.");
            }

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("User not authenticated or invalid User ID.");
            }

            try
            {
                var response = await _chaService.SendMessageAsync(userId, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("conversation")]
        public async Task<IActionResult> GetConversation()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("User not authenticated or invalid User ID.");
            }

            try
            {
                var conversation = await _chaService.GetConversationAsync(userId);
                return Ok(new { Conversation = conversation });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}