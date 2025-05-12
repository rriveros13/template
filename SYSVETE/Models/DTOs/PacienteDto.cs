namespace SYSVETE.Models.DTOs
{
    public class PacienteDto
    {

        public int IdPaciente { get; set; }
        public int? IdCliente { get; set; }
        public int? IdRaza { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Edad { get; set; }
        public string Sexo { get; set; } = null!;
        public decimal Peso { get; set; } 
        public bool Activo { get; set; }
    }
}
