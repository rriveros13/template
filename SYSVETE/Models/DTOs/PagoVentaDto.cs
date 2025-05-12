using SYSVETE.Models.DTOs;

namespace SYSVETE.Models
{
    public class PagoVentaDto
    {
        public int IdPago { get; set; }
        public int? IdVenta { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal MontoPagado { get; set; }
        public string MetodoPago { get; set; } = null!;
     
    }
}
