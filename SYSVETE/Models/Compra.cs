using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class Compra
    {
        public Compra()
        {
            DeudaProveedores = new HashSet<DeudaProveedor>();
        }
        public int IdCompra { get; set; }
        public string NroBoleta { get; set; } = null!;
        public int IdProveedor { get; set; }
        public bool Facturado { get; set; }
        public string TipoCompra { get; set; } = null!;
        public bool Finalizado { get; set; }

        public DateTime? FechaCompra { get; set; }
        public bool Borrado { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public virtual Proveedor? Proveedor { get; set; } = null;
        [JsonIgnore]
        public virtual ICollection<DeudaProveedor> DeudaProveedores { get; set; }
    }
}
