using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class Venta
    {
        public Venta()
        {
            VentaDetalles = new HashSet<VentaDetalle>();
            PagoVentas = new HashSet<PagoVenta>();

        }
        public int IdVenta { get; set; }
        public int NroBoleta { get; set; }
        public bool Facturado { get; set; }
        public int IdCliente { get; set; }
        public DateTime? FechaVenta { get; set; }
        public bool Borrado { get; set; }
        public bool Finalizado { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public virtual Cliente? IdClienteNavigation { get; set; } = null;
        [JsonIgnore]
        public virtual ICollection<VentaDetalle> VentaDetalles { get; set; }
        [JsonIgnore]
        public virtual ICollection<PagoVenta>? PagoVentas { get; set; } = null;
    }
}
