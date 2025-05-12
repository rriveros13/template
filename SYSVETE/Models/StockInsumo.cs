using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class StockInsumo
    {
        public int IdStock { get; set; }
        public int IdInsumo { get; set; }
        public int? IdLote { get; set; }
        public decimal CantidadActual { get; set; } 
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public bool Borrado { get; set; }
        public virtual Insumo? IdInsumoNavigation { get; set; } = null!;
        public virtual Lote? IdLoteNavigation { get; set; } = null!;

    }
}
