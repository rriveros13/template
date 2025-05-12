using System.Text.Json.Serialization;
namespace SYSVETE.Models
{
    public class Impuesto
    {
        public Impuesto()
        {
            Insumos = new HashSet<Insumo>();
        }
        public int idImpuesto { get; set; }
        public string Descripcion { get; set; } = null;
        public decimal Valor { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public bool Borrado { get; set; }

        [JsonIgnore]
        public virtual ICollection<Insumo> Insumos { get; set; }

    }
}
