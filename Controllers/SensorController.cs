using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Greenhouse.DbContexts;
using Greenhouse.Models;
using Greenhouse.Services;
using Greenhouse.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Greenhouse.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpPost("reading")]
       
        public async Task<IActionResult> AddReading([FromBody] CreateSensorReadingDto dto)
        {
            try
            {
                await _sensorService.AddReadingAsync(dto);
                return Ok(new { message = "Reading saved successfully" });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest()
        {
            var reading = await _sensorService.GetLatestReadingAsync();
            if (reading == null) return NotFound();
            return Ok(reading);
        }

    }
}
