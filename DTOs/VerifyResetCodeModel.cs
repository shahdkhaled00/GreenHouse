using System.ComponentModel.DataAnnotations;

namespace Greenhouse.DTOs
{
    public class VerifyResetCodeModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
