using Greenhouse.DTOs;
namespace Greenhouse.Services;
// Services/IChatService.cs
using System.Threading.Tasks;

public interface IChatService
{
    Task<string> GetChatReplyAsync(string userInput, string currentOutput);
}
