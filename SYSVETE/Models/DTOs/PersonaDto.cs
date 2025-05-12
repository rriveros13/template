namespace SYSVETE.Models.DTOs
{
    public class PersonaDto
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; } = null;
        public string Apellido { get; set; } = null;
        public string Cedula { get; set; } = null;
        public DateTime? FechaNacimiento { get; set; } = null;
    }
}
