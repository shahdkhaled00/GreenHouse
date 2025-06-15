using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; 
using Greenhouse.DbContexts;
using Greenhouse.DTOs;
using Greenhouse.Models;
using System.Security.Claims;

namespace Greenhouse.Services
{
    public class SensorService : ISensorService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SensorService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddReadingAsync(CreateSensorReadingDto dto)
        {
            // الحصول على UserId من التوكن
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                throw new UnauthorizedAccessException("User not authenticated.");

            var reading = new SensorReading
            {
                Temperature = dto.Temperature,
                Humidity = dto.Humidity,
                Light = dto.Light,
                SoilMoisture = dto.SoilMoisture,
                CO2Level = dto.CO2Level,
                RecordedAt = DateTime.UtcNow,
                UserId = int.Parse(userId) // ربط القراءة بالـ UserId
            };

            _context.SensorReadings.Add(reading);
            await _context.SaveChangesAsync();
        }

        public async Task<SensorReadingDto?> GetLatestReadingAsync()
        {
            var latest = await _context.SensorReadings
                .OrderByDescending(r => r.RecordedAt)
                .FirstOrDefaultAsync();

            if (latest == null) return null;

            return new SensorReadingDto
            {
                Temperature = latest.Temperature,
                Humidity = latest.Humidity,
                Light = latest.Light,
                SoilMoisture = latest.SoilMoisture,
                CO2Level = latest.CO2Level,
                RecordedAt = latest.RecordedAt
            };
        }
    }
}
