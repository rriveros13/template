namespace SYSVETE.Models.DTOs
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public int? IdPersona { get; set; }
        public string RUC { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Activo { get; set; }
    }
}
