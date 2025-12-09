using System.ComponentModel.DataAnnotations;

namespace Gestion2.Models
{
    public class MemoModel
    {
        public int Id { get; set; }

        public int Folio { get; set; }
        public int Año { get; set; }


        [Required(ErrorMessage = "El campo 'De' es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string De { get; set; }


        [Required(ErrorMessage = "El campo 'Para' es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Para { get; set; }


        [Required(ErrorMessage = "El campo Asunto es obligatorio")]
        [StringLength(500, ErrorMessage = "Máximo 100 caracteres")]
        public string Asunto { get; set; }


        [Required(ErrorMessage = "El contenido del memorándum es obligatorio")]
        [StringLength(5000, ErrorMessage = "Máximo 5000 caracteres")]
        public string Contenido { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; }

        public string Estatus { get; set; } // Activo / Cancelado

        public string UsuarioRegistro { get; set; }

        public ICollection<CancelacionModel>? Cancelaciones { get; set; }
    }
}
