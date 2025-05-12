     using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SYSVETE.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Permisos = new HashSet<Permiso>();
            Usuarios = new HashSet<Usuario>();
        }
        public int IdRol { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; } = null!;
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public bool Activo { get; set; }
        [JsonIgnore]
        public bool Borrado { get; set; }
        [JsonIgnore]
        public virtual ICollection<Permiso> Permisos { get; set; }
        [JsonIgnore]
        public virtual ICollection<Usuario> Usuarios { get; set; }

    }

    public enum AccionesDefinidas
    {
        Consultar = 1,
        Agregar = 2, 
        Editar = 3,
        Borrar = 4
    }
}
