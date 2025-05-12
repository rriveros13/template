using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class VentaDetalle
    {
        public VentaDetalle()
        {
            HistorialMovimientos = new HashSet<HistorialMovimiento>();
        }
        public int IdVentaDetalle { get; set; }
        public int IdVenta { get; set; }
        public int? IdInsumo { get; set; }
        public int? IdHistorial { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public bool Borrado { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public virtual Venta? IdVentaNavigation { get; set; } = null;
        public virtual Insumo? IdInsumoNavigation { get; set; } = null;
        public virtual HistorialClinico? IdHistorialNavigation { get; set; } = null;
        [JsonIgnore]
        public virtual ICollection<HistorialMovimiento> HistorialMovimientos { get; set; }
    }
}
