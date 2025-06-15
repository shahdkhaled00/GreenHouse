 namespace Greenhouse.Models;
 public class OpenAiRequest
       {
           public string Model { get; set; }
           public List<OpenAiMessage> Messages { get; set; }
       }