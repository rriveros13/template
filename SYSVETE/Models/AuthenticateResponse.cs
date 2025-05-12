namespace SYSVETE.Models
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(Usuario usuario, string token)
        {
            IdUsuario = usuario.IdUsuario;
            Token = token;
            Nombre = usuario.Nombre;
            Alias = usuario.Alias;
        }
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Alias { get; set; }
        public string Token { get; set; }
    }
}
