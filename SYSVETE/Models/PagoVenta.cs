using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class PagoVenta
    {
        public int IdPago { get; set; }
        public int? IdVenta { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal MontoPagado { get; set; }
        public string MetodoPago { get; set; } = null!;
        public bool Borrado { get; set; }

        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        [JsonIgnore]
        public virtual Venta? IdVentaNavigation { get; set; } = null!;



    }
}
