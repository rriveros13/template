using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class HistorialMovimiento
    {
        public int IdHistorial { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int? IdCompraDetalle { get; set; }
        public int? IdVentaDetalle { get; set; }
        public bool Ajuste { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public bool Borrado { get; set; }
        public virtual CompraDetalle? IdCompraDetalleNavigation { get; set; } = null!;
        public virtual VentaDetalle? IdVentaDetalleNavigation { get; set; } = null!;

    }
}
