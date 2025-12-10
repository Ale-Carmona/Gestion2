using System.ComponentModel.DataAnnotations;

namespace Gestion2.Models
{
    // DTO para manejar la entrada del login
    public class LoginModel
    {
        [Required]
        public string User { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}