using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class CompraDetalle
    {
        public CompraDetalle()
        {
            HistorialMovimientos = new HashSet<HistorialMovimiento>();

        }
        public int IdCompraDetalle { get; set; }
        public int IdCompra { get; set; }
        public int IdInsumo { get; set; }
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public bool Borrado { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public virtual Compra? Compra { get; set; } = null;
        public virtual Insumo? Insumo { get; set; } = null;
        [JsonIgnore]
        public virtual ICollection<HistorialMovimiento> HistorialMovimientos { get; set; }
    }
}
