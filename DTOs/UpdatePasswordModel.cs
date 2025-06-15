using System.ComponentModel.DataAnnotations;

namespace Greenhouse.DTOs
{
    public class UpdatePasswordModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
