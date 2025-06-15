using Greenhouse.DbContexts;
   using Greenhouse.Models;
   using Greenhouse.DTOs;
   using Microsoft.EntityFrameworkCore;
   using System.Text.Json;
   using Microsoft.Extensions.Logging;
    using System.Diagnostics;
    using System.Text.Json;

   // Services/ChatService.cs
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace Greenhouse.Services;

public class ChatService : IChatService
{
    private readonly HttpClient _httpClient;
    private const string ApiUrl = "https://medical-chatbot-backend-production-732f.up.railway.app/handle_chat"; // غيّريه لو السيرفر شغال على رابط تاني

    public ChatService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetChatReplyAsync(string userInput, string currentOutput)
    {
        var data = new
        {
            user_input = userInput,
            current_output = currentOutput
        };

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(ApiUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("فشل في الاتصال بـ chatbot API");
        }

        var responseString = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(responseString);

        var assistantReply = doc.RootElement.GetProperty("assistant_reply").GetString();
        return assistantReply;
    }
}
