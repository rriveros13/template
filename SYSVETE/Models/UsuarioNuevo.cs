using System.ComponentModel.DataAnnotations;

namespace SYSVETE.Models
{
    public class UsuarioNuevo
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = "";
        public string Alias { get; set; } = "";
        public string Contrasena { get; set; } = null!;
        public bool Activo { get; set; }
        public int IdRol { get; set; }

    }
}
