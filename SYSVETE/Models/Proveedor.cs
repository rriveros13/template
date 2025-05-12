using System;
using System.Collections.Generic;

namespace SYSVETE.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
        }

        public int IdProveedor { get; set; }
        public int IdPersona { get; set; }
        public string Ruc { get; set; } = null!;
        public string? Telefono { get; set; } = null;
        public string? Email { get; set; } = null;
        public bool Borrado { get; set; }
        public DateTime? FechaInsertado { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaBorrado { get; set; }
        public int? IdUsuarioInserto { get; set; }
        public int? IdUsuarioModifico { get; set; }
        public virtual Persona? IdPersonaNavigation { get; set; } = null;

    }
}
