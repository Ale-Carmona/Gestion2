using System.ComponentModel.DataAnnotations;

namespace Gestion2.Models
{
    public class CancelacionModel
    {
        public int Id { get; set; }

        public int MemoId { get; set; }           // Relación con el memo
        public MemoModel Memo { get; set; }       // Navegación

        public string UsuarioCancelo { get; set; } = string.Empty;


        [Required(ErrorMessage = "El campo Motivo de Cancelacion es obligatorio")]
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string MotivoCancelacion { get; set; } = string.Empty;


        public DateTime FechaCancelacion { get; set; }
    }
}
