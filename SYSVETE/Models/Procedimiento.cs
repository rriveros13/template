using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class Procedimiento
    {
        public Procedimiento()
        {
            HistorialClinicos = new HashSet<HistorialClinico>();
        }
        public int IdProcedimiento { get; set; }
        public string Descripcion { get; set; } = null;
        public decimal Costo { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public bool Borrado { get; set; }
        [JsonIgnore]
        public virtual ICollection<HistorialClinico> HistorialClinicos { get; set; }

    }
}
