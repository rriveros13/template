using SYSVETE.Models.DTOs;

namespace SYSVETE.Models
{
    public class CompraDetalleDto
    {
        public int IdCompraDetalle { get; set; }
        public int IdCompra { get; set; }
        public int IdInsumo { get; set; }
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public InsumoDto? Insumo { get; set; }
    }
}
