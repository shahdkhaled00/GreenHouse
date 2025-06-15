using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Greenhouse.Models;
public class SensorReading
{
    public int Id { get; set; }

    public float? Temperature { get; set; }
    public float? Humidity { get; set; }
    public string? Light { get; set; }
    public string? SoilMoisture { get; set; }
    public float? CO2Level { get; set; }
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }

    
}
