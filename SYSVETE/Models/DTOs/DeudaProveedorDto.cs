using SYSVETE.Models.DTOs;

namespace SYSVETE.Models
{
    public class DeudaProveedorDto
    {
        public int IdDeuda { get; set; }
        public int? IdCompra { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal MontoPagado { get; set; }
        public string MetodoPago { get; set; } = null!;
     
    }
}
