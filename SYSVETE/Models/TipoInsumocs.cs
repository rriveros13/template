using System.Text.Json.Serialization;
namespace SYSVETE.Models
{
    public class TipoInsumo
    {
        public TipoInsumo()
        {
            Insumos = new HashSet<Insumo>();
        }
        public int IdTipoInsumo { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool Borrado { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public string Nombre { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Insumo> Insumos { get; set; }
        //  [JsonIgnore]
        //  public bool Borrado { get; set; }
    }
}
