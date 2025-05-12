using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class Raza
    {
        public Raza()
        {
            Pacientes = new HashSet<Paciente>();
        }
        public int IdRaza { get; set; }
        public int IdEspecie { get; set; }
        public string Nombre { get; set; } = null;
        public bool Activo { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        [JsonIgnore]
        public bool Borrado { get; set; }
        [JsonIgnore]
        public virtual Especie IdEspecieNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Paciente> Pacientes { get; set; }

    }
}
