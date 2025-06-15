namespace Greenhouse.DTOs;
public class SensorReadingDto
{
    public float? Temperature { get; set; }
    public float? Humidity { get; set; }
    public string Light { get; set; } = null!;
    public string SoilMoisture { get; set; } = null!;
    public float? CO2Level { get; set; }
    public DateTime RecordedAt { get; set; }
}
