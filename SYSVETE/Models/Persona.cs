using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace SYSVETE.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Clientes = new HashSet<Cliente>();

        }

        public int IdPersona { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public bool Borrado { get; set; }
        [JsonIgnore]
        public virtual ICollection<Cliente> Clientes { get; set; }

    }
}
