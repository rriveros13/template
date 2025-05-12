using SYSVETE.Models.DTOs;

namespace SYSVETE.Models
{
    public class CompraDto
    {
        public int IdCompra { get; set; }
        public string NroBoleta { get; set; } = null!;
        public int IdProveedor { get; set; }
        public string TipoCompra { get; set; } = null!;
        public decimal MontoTotal { get; set; }
        public decimal SaldoPendiente { get; set; }
        public decimal MontoAbonado { get; set; }
        public bool Facturado { get; set; }
        public bool Finalizado { get; set; }
        public DateTime? FechaCompra { get; set; }
        public ProveedorDto? proveedorDto { get; set; }
    }
}
