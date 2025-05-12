
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using SYSVETE.Models;

namespace SYSVETE.Autorizacion
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AutorizarAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<AccionesDefinidas> _rolesDefinidos;

        public AutorizarAttribute(params AccionesDefinidas[] rolesDefinidos)
        {
            _rolesDefinidos = rolesDefinidos ?? new AccionesDefinidas[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Usuario? usuarioSYSVETE = context.HttpContext.Items["UsuarioSYSVETE"] as Usuario;
            if (usuarioSYSVETE == null)
            {
                context.Result = new JsonResult(new { mensaje = "Sin Autorizacion para esta accion" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }
            string? modulo = context.HttpContext.Request.RouteValues["controller"].ToString();
            if (modulo == null)
            {
                context.Result = new JsonResult(new { mensaje = "Sin Autorizacion para esta accion" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            var db = context.HttpContext.RequestServices.GetRequiredService<SYSVETEContext>();
            string? method = context.HttpContext.Request.Method.ToString();

            if (!VerificarPermisos(db, modulo, usuarioSYSVETE, method))
            {
                context.Result = new JsonResult(new { mensaje = "Sin Autorizacion para esta accion" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }

        private bool VerificarPermisos(SYSVETEContext db, string modulo, Usuario usuarioActual, string method)
        {
            bool result = false;
            var permisos = db.Permisos.Where(p => p.IdPermiso == 1).SingleOrDefault();

            var objetoModulo = db.Modulos.Where(p => p.Nombre == modulo).SingleOrDefault();
            switch (method)
            {
                case "GET" :
                  permisos = db.Permisos
                .Where(p => p.IdRol == usuarioActual.IdRol && p.IdModulo == objetoModulo.IdModulo && p.Consultar && !p.Borrado).SingleOrDefault();
                    break;
                case "POST":
                     permisos = db.Permisos
                   .Where(p => p.IdRol == usuarioActual.IdRol && p.IdModulo == objetoModulo.IdModulo && p.Agregar && !p.Borrado).SingleOrDefault();
                    break;
                case "DELETE":
                    permisos = db.Permisos
                   .Where(p => p.IdRol == usuarioActual.IdRol && p.IdModulo == objetoModulo.IdModulo && p.Borrar && !p.Borrado).SingleOrDefault();
                    break;
                case "PUT":
                    permisos = db.Permisos
                   .Where(p => p.IdRol == usuarioActual.IdRol && p.IdModulo == objetoModulo.IdModulo && p.Editar).SingleOrDefault();
                    break;
            }

            if ( permisos != null)
            {
                result = true;
            }
            return result;
        }

    }
}
