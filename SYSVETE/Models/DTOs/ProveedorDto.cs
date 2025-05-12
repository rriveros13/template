namespace SYSVETE.Models.DTOs
{
    public class ProveedorDto
    {
        public int IdProveedor { get; set; }
        public int IdPersona { get; set; }
        public string Ruc { get; set; } = null;
        public string Telefono { get; set; } = null;
        public string Email { get; set; } = null;
        public PersonaDto? Persona { get; set; } = null;
    }
}
