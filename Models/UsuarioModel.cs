using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion2.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NumEmpleado { get; set; } = string.Empty; // Usado como nombre de usuario

        [Required]
        public string PasswordHash { get; set; } = string.Empty; // Contraseña encriptada

        [Required]
        public string NombreCompleto { get; set; } = string.Empty;

        public string Rol { get; set; } = "Registrador"; // Administrador, Registrador, etc.
    }
}