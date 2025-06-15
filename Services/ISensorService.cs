using Greenhouse.DTOs;
namespace Greenhouse.Services;
public interface ISensorService
{
    Task AddReadingAsync(CreateSensorReadingDto dto);
    Task<SensorReadingDto?> GetLatestReadingAsync();
}
