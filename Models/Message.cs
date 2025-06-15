using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Greenhouse.Models;
public class Message
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserMessage { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string BotResponse { get; set; }
    public DateTime Timestamp { get; set; }
    public User User { get; set; }
}