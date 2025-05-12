
using System.Net.Http.Headers;
using SYSVETE.Services;

namespace SYSVETE.Autorizacion
{
    public class AutorizarMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public AutorizarMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context, IUsuario service, IJWTUtils jWTUtils)
        {
            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var idUsuario = jWTUtils.ValidarToken(token);
                if (idUsuario != null)
                {
                    //Puede ser que ocurra una excepcion o el usuario ya no este activo
                    var usuarioValido = await service.ObtenerUsuarioPorID(idUsuario.Value);
                    if (usuarioValido != null)
                    {
                        context.Items["UsuarioSYSVETE"] = usuarioValido;
                    }

                }

                await _requestDelegate(context);
            }
            catch (Exception)
            {
                //Si llega hasta aca no hace nada, ningun usuario es agregado al contexto
                throw;
            }
        }
    }
}
