using Greenhouse.DTOs;
namespace Greenhouse.Services;
public interface IChatBotService
{
    Task<ChatResponseDto> SendMessageAsync(int userId, ChatRequestDto request);
    Task<List<ConversationDto>> GetConversationAsync(int userId);
}