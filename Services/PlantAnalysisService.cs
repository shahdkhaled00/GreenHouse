using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.IO;

   

   // Services/PlantAnalysisService.cs
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Greenhouse.Services
{
public class PlantAnalysisService : IPlantAnalysisService
{
    private readonly HttpClient _httpClient;
    private const string ApiUrl = "https://medical-chatbot-backend-production-732f.up.railway.app/handle_analysis"; 

    public PlantAnalysisService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(string resultText, string assistantReply)> AnalyzeImageAsync(string imageUrl)
    {
        var requestData = new
        {
            image_url = imageUrl
        };

        var json = JsonSerializer.Serialize(requestData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(ApiUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("تحليل الصورة فشل: " + response.StatusCode);
        }

        var responseString = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(responseString);
        var root = doc.RootElement;

        var resultText = root.GetProperty("result_text").GetString();
        var assistantReply = root.GetProperty("assistant_reply").GetString();

        return (resultText, assistantReply);
    }
}
}