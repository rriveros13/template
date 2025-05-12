using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class DeudaProveedor
    {
        public int IdDeuda { get; set; }
        public int? IdCompra { get; set; }
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
        public virtual Compra? IdCompraNavigation { get; set; } = null!;



    }
}
