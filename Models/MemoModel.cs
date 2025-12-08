namespace Gestion2.Models
{
    public class MemoModel
    {
        public int Id { get; set; }

        public int Folio { get; set; }
        public int Año { get; set; }

        public string De { get; set; }
        public string Para { get; set; }
        public string Asunto { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string Estatus { get; set; } // Activo / Cancelado

        public string UsuarioRegistro { get; set; }

        public ICollection<CancelacionModel>? Cancelaciones { get; set; }
    }
}
