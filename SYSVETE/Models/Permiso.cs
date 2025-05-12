using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public partial class Permiso
    {
      
        public int IdPermiso  { get; set; }
        public int? IdRol { get; set; }
        public int? IdModulo { get; set; }
        public bool Consultar { get; set; }
        public bool Agregar { get; set; }
        public bool Editar { get; set; }
        public bool Borrar { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        [JsonIgnore]
        public bool Borrado { get; set; }
        public virtual Rol IdRolNavigation { get; set; } = null!;
        public virtual Modulo IdModuloNavigation { get; set; } = null!;

    }
}