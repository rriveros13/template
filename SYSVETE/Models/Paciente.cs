using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class Paciente
    {
        public Paciente()
        {
            HistorialClinicos = new HashSet<HistorialClinico>();
        }
        public int IdPaciente { get; set; }
        public int? IdCliente { get; set; }
        public int? IdRaza { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Edad { get; set; } 
        public string Sexo { get; set; } = null!;
        public decimal Peso { get; set; } 
        public bool Activo { get; set; }
        public bool Borrado { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public virtual Cliente? IdClienteNavigation { get; set; } = null!;
        public virtual Raza? IdRazaNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<HistorialClinico> HistorialClinicos { get; set; }


    }
}
