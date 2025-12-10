using Gestion2.Data;
using Gestion2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion2.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // 1. Buscar el usuario por NumEmpleado (User en DTO)
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NumEmpleado == loginDto.User);

            if (usuario == null)
            {
                return Unauthorized(new { message = "Credenciales inválidas." });
            }

            // 2. Verificación de contraseña (simulación)
            // En producción usar bcrypt o PBKDF2
            bool passwordEsValida = loginDto.Password == usuario.PasswordHash;

            if (!passwordEsValida)
            {
                return Unauthorized(new { message = "Credenciales inválidas." });
            }

            // 3. Autenticación exitosa
            return Ok(new
            {
                NumEmpleado = usuario.NumEmpleado,
                NombreCompleto = usuario.NombreCompleto,
                Rol = usuario.Rol,
                Token = "SIMULATED_TOKEN_XYZ123" // Aquí iría un JWT real
            });
        }
    }
}
