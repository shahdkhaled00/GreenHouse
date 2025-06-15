using System.ComponentModel.DataAnnotations;

namespace Greenhouse.DTOs
{
    public class SendResetCodeModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}