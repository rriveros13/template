using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public class HistorialClinico
    {
        public HistorialClinico()
        {
            VentaDetalles = new HashSet<VentaDetalle>();
        }

        public int IdHistorial { get; set; }
        public int? IdTratamiento { get; set; }
        public int? IdPaciente { get; set; }
        public int? IdPatologia { get; set; }
        public int? IdVacuna { get; set; }
        public int? IdProcedimiento { get; set; }
        public string Descripcion { get; set; } = null;
        public bool Facturado { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public bool Borrado { get; set; }
        public virtual Tratamiento? IdTratamientoNavigation { get; set; } = null;
        public virtual Paciente? IdPacienteNavigation { get; set; } = null;
        public virtual Patologia? IdPatologiaNavigation { get; set; } = null;
        public virtual Vacuna? IdVacunaNavigation { get; set; } = null;
        public virtual Procedimiento? IdProcedimientoNavigation { get; set; } = null;
        [JsonIgnore]
        public virtual ICollection<VentaDetalle> VentaDetalles { get; set; }
    }




}
