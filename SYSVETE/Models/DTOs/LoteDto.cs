namespace SYSVETE.Models
{
    public class LoteDto
    {
        public int IdLote { get; set; } 
        public string CodigoLote { get; set; } = null;
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaFabricacion { get; set; }
    }
}
