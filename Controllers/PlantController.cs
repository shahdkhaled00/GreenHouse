using Microsoft.AspNetCore.Mvc;
using Greenhouse.DTOs;
using Greenhouse.Services;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Greenhouse.Controllers
{
   using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PlantController : ControllerBase
{
    private readonly IPlantAnalysisService _plantService;

    public PlantController(IPlantAnalysisService plantService)
    {
        _plantService = plantService;
    }

    [HttpPost("analyze")]
    public async Task<IActionResult> Analyze([FromBody] AnalyzeRequest request)
    {
        if (string.IsNullOrEmpty(request.ImageUrl))
            return BadRequest("Image URL is required.");

        try
        {
            var (resultText, assistantReply) = await _plantService.AnalyzeImageAsync(request.ImageUrl);
            return Ok(new
            {
                result_text = resultText,
                assistant_reply = assistantReply
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}

public class AnalyzeRequest
{
    public string ImageUrl { get; set; }
}

}