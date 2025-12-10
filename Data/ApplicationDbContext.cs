using Gestion2.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        public DbSet<MemoModel> Memos { get; set; }
        public DbSet<CancelacionModel> Cancelaciones { get; set; }

        public DbSet<UsuarioModel> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ----------------------------------------------------
            // Data Seeding para Usuarios
            // ----------------------------------------------------
            // Inserta el usuario de prueba para el login
            modelBuilder.Entity<UsuarioModel>().HasData(
                new UsuarioModel
                {
                    // Es crucial usar un Id fijo para el seeding para que EF Core
                    // pueda rastrear y actualizar este registro en futuras migraciones.
                    Id = 1,
                    NumEmpleado = "admin",
                    // En la simulación de login, la contraseña es igual al hash: '12345'
                    PasswordHash = "admin",
                    NombreCompleto = "Prueba Admin",
                    Rol = "Administrador" // Define el rol si lo usas para permisos
                }
            );

            // Si tuvieras que agregar configuraciones adicionales o seeding para otras tablas,
            // irían aquí abajo.
        }
    }
}
