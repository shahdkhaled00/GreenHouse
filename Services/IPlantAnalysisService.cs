using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Greenhouse.Services
{
public interface IPlantAnalysisService
{
    Task<(string resultText, string assistantReply)> AnalyzeImageAsync(string imageUrl);
}

}