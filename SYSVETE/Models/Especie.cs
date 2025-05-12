using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class Especie
    {
        public Especie()
        {
            Razas = new HashSet<Raza>();
        }
        public int IdEspecie { get; set; }
        public string Nombre     { get; set; } = null;
        public bool Activo { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public bool Borrado { get; set; }

        [JsonIgnore]
        public virtual ICollection<Raza> Razas { get; set; }


    }
}
