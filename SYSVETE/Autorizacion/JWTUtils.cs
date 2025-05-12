using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SYSVETE.Helpers;
using SYSVETE.Models;

namespace SYSVETE.Autorizacion
{
    public interface IJWTUtils
    {
        public string GenerarToken(Usuario user);
        public int? ValidarToken(string token);
    }

    public class JWTUtils : IJWTUtils
    {
        private readonly AppSettings _appSettings;

        public JWTUtils(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerarToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UsuarioSYSVETE", user.IdUsuario.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int? ValidarToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "UsuarioSYSVETE").Value);

                // Retorna el ID de usuario si la validacion es correcta
                return userId;
            }
            catch
            {
                // Retorna NULL si la validacion falla
                return null;
            }
        }
    }
}
