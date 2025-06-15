using Greenhouse.DbContexts;
   using Greenhouse.Models;
   using Greenhouse.DTOs;
   using Microsoft.EntityFrameworkCore;
   using System.Text.Json;
   using Microsoft.Extensions.Logging;
    using System.Diagnostics;
    using System.Text.Json;

   namespace Greenhouse.Services
{


     public class ChatBotService : IChatBotService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ChatService> _logger;

        public ChatBotService(AppDbContext context, ILogger<ChatService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ChatResponseDto> SendMessageAsync(int userId, ChatRequestDto request)
        {
            // Check if message already exists in database (caching)
            var existingMessage = await _context.Messages
                .FirstOrDefaultAsync(m => m.UserId == userId && m.UserMessage == request.Message);
            if (existingMessage != null)
            {
                _logger.LogInformation($"Returning cached response for message: {request.Message}");
                return new ChatResponseDto { Response = existingMessage.BotResponse };
            }

            try
{
    // Call the Python script
    var processStartInfo = new ProcessStartInfo
    {
        FileName = "python",
        Arguments = $"tinyllama_chat.py \"{request.Message}\"",
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true,
        WorkingDirectory = @"D:\back_plants\Greenhouse"
    };

    using var process = new Process { StartInfo = processStartInfo };
    process.Start();

    // Read the output
    string output = await process.StandardOutput.ReadToEndAsync();
    string error = await process.StandardError.ReadToEndAsync();

    await process.WaitForExitAsync();

    // Log any stderr output for debugging, but don't treat it as an error
    if (!string.IsNullOrEmpty(error))
    {
        _logger.LogWarning($"Python script stderr output: {error}");
    }

    // Check if stdout has a valid JSON response
    if (string.IsNullOrEmpty(output))
    {
        _logger.LogError("Python script returned no output.");
        throw new Exception("Python script returned no output.");
    }

    // Parse the JSON output
    var jsonResponse = JsonSerializer.Deserialize<Dictionary<string, string>>(output);
    if (jsonResponse == null || !jsonResponse.ContainsKey("response"))
    {
        _logger.LogError($"Invalid response from Python script: {output}");
        throw new Exception("Invalid response from Python script.");
    }

    var botResponse = jsonResponse["response"];

    // Log the response
    _logger.LogInformation($"Received response from TinyLlama: {botResponse}");

    // Save to database
    var message = new Message
    {
        UserId = userId,
        UserMessage = request.Message,
        BotResponse = botResponse,
        Timestamp = DateTime.UtcNow
    };
    _context.Messages.Add(message);
    await _context.SaveChangesAsync();

    _logger.LogInformation($"Message sent successfully for user {userId}: {request.Message}");
    return new ChatResponseDto { Response = botResponse };
}
catch (Exception ex)
{
    _logger.LogError(ex, "Failed to send message to TinyLlama.");
    throw new Exception($"Failed to send message: {ex.Message}");
}
        }

        public async Task<List<ConversationDto>> GetConversationAsync(int userId)
        {
            var messages = await _context.Messages
                .Where(m => m.UserId == userId)
                .OrderBy(m => m.Timestamp)
                .Select(m => new ConversationDto
                {
                    UserMessage = m.UserMessage,
                    BotResponse = m.BotResponse,
                    Timestamp = m.Timestamp
                })
                .ToListAsync();

            _logger.LogInformation($"Retrieved {messages.Count} messages for user {userId}.");
            return messages;
        }
    }
}