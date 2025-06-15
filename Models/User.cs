using System.ComponentModel.DataAnnotations;
namespace Greenhouse.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }
    public string? ResetCode { get; set; }
    public DateTime? ResetCodeExpiry { get; set; }
     public ICollection<SensorReading> SensorReadings { get; set; } = new List<SensorReading>();
     public ICollection<Message> Messages { get; set; } = new List<Message>();


    public void SetPassword(string password)
    {
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
    }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
}
