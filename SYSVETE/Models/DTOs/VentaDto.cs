using SYSVETE.Models.DTOs;

namespace SYSVETE.Models
{
    public class VentaDto
    {
        public int IdVenta { get; set; }
        public int NroBoleta { get; set; } 
        public int IdCliente { get; set; }
        public string TipoCompra { get; set; } = null!;
        public decimal MontoTotal { get; set; }        
        public decimal SaldoPendiente { get; set; }
        public decimal MontoAbonado { get; set; }
        public bool Facturado { get; set; }
        public bool Finalizado { get; set; }

        public DateTime? FechaVenta { get; set; }
    }
}
