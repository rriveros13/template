using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public int? IdRol { get; set; }
        public string Nombre { get; set; } = "";
        public string Alias { get; set; } = null!;
        [JsonIgnore]
        public string Contrasena { get; set; } = null!;
        public bool Activo { get; set; }
        [JsonIgnore]
        public bool Borrado { get; set; }
        public virtual Rol IdRolNavigation { get; set; } = null!;

    }
}
