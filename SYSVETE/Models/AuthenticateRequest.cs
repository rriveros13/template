using System.ComponentModel.DataAnnotations;

namespace SYSVETE.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Contrasena { get; set; }
    }
}
