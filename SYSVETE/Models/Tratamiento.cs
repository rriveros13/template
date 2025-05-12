using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class Tratamiento
    {
        public Tratamiento()
        {
            HistorialClinicos = new HashSet<HistorialClinico>();
        }
        public int IdTratamiento { get; set; }
        public decimal Costo { get; set; }
        public string Nombre { get; set; } = null!;
        [JsonIgnore]
        public bool Borrado { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int IdUsuarioInserto  { get; set; }
        public int? IdUsuarioModifico { get; set; }
        [JsonIgnore]
        public virtual ICollection<HistorialClinico> HistorialClinicos { get; set; }



    }
}
