using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class Cliente
    {
        public Cliente()
        {
            Pacientes = new HashSet<Paciente>();
            Ventas = new HashSet<Venta>();
        }
        public int IdCliente { get; set; }
        public int? IdPersona { get; set; }
        public string RUC     { get; set; } = null;
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public bool Borrado { get; set; }

        //[JsonIgnore]
        public virtual Persona? IdPersonaNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Paciente> Pacientes { get; set; }
        [JsonIgnore]
        public virtual ICollection<Venta> Ventas { get; set; }
    }
}
