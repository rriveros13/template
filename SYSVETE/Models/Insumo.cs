using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class Insumo
    {
        public Insumo()
        {
            VentaDetalles = new HashSet<VentaDetalle>();
            StockInsumos = new HashSet<StockInsumo>();

        }
        public int IdInsumo { get; set; }
        public int? IdTipoInsumo { get; set; }
        public int? IdPresentacion { get; set; }
        public int? IdImpuesto { get; set; }
        public string codigo { get; set; } = null!;
        public string descripcion { get; set; } = null!;
        public  bool Activo { get; set; }
        public bool Borrado { get; set; }

        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public virtual TipoInsumo? IdTipoInsumoNavigation { get; set; } = null!;
        public virtual Impuesto? IdImpuestoNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<VentaDetalle> VentaDetalles { get; set; }
        [JsonIgnore]
        public virtual ICollection<StockInsumo> StockInsumos { get; set; }
    }
}
