// DTOs/LoginModel.cs
using System.ComponentModel.DataAnnotations;

namespace Greenhouse.DTOs
{
    public class LoginModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
