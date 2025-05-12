using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class Lote
    {
        public Lote()
        {
            StockInsumos = new HashSet<StockInsumo>();

        }
        public int IdLote { get; set; }
        public string CodigoLote { get; set; } = null;
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaFabricacion { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public bool Borrado { get; set; }
        [JsonIgnore]
        public virtual ICollection<StockInsumo> StockInsumos { get; set; }
    }
}
