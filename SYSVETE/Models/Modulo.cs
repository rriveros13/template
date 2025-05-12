using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace SYSVETE.Models
{
    public partial class Modulo
    {

        public Modulo()
        {
            Permisos = new HashSet<Permiso>();
        }
        public int IdModulo { get; set; }
        public string Nombre { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Permiso> Permisos { get; set; }

    }
}