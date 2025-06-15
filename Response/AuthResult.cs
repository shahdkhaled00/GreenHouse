namespace Greenhouse.Response
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int UserId { get; set; }
        public string? Token { get; set; }
    }
}