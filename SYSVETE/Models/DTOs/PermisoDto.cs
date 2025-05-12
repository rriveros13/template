using System.Text.Json.Serialization;

namespace SYSVETE.Models.DTOs
{
    public class PermisoDto
    {
        public int? IdPermiso{ get; set; }
        public int IdRol { get; set; }
        public int IdModulo { get; set; }

        public bool Consultar { get; set; }
        public bool Agregar { get; set; }
        public bool Editar { get; set; }
        public bool Borrar { get; set; }

        [JsonIgnore]
        public bool Borrado { get; set; }
    }
}
