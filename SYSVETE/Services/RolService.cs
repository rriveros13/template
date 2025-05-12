using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;

namespace SYSVETE.Services
{
    public interface IRol
    {
        Task<List<Rol>> ObtenerRoles();
        Task<Rol> ObtenerRolPorId(int idRol);
        Task AgregarRol(Rol rol, int idUsuario);
        Task UpdateRol(Rol rol, int idUsuario);
        Task BorrarRol(int idRol, int idUsuario);
    }

    public class RolService : IRol
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public RolService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<Rol> ObtenerRolPorId(int idRol)
        {
            try
            {
                var rol = await _context.Roles
                    .Where(u => u.IdRol == idRol)
                    .FirstOrDefaultAsync();
                return rol;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<Rol>> ObtenerRoles()
        {
            try
            {
                var roles = await _context.Roles
                    .ToListAsync();
                return roles;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task AgregarRol(Rol rol, int idUsuario)
        {
            try
            {
                if (await RolRepetido(rol))
                {
                    throw new Exception("Ya exite un rol con las mismas caracteristicas");
                }
                rol.IdUsuarioInserto = idUsuario;
                _context.Roles.Add(rol);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateRol(Rol rol, int idUsuario)
        {
            try
            {
                var existeRol = await _context.Roles
                .Where(mm => mm.IdRol == rol.IdRol)
                .FirstOrDefaultAsync();

                if (existeRol == null)
                {
                    throw new Exception($"No se puede encontrar el modulo {rol.IdRol}");
                }

                if (await RolRepetido(rol))
                {
                    throw new Exception("Ya exite un rol con las mismas caracteristicas");
                }

                existeRol.FechaModificado = DateTime.Now;
                existeRol.Codigo = rol.Codigo;
                existeRol.Descripcion = rol.Descripcion;
                existeRol.IdUsuarioModifico = idUsuario;

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Busca si ya existe un Rol con las misma caracteristica
        /// </summary>
        private async Task<bool> RolRepetido(Rol rol)
        {
            var check = await _context.Roles
                .CountAsync(data => (data.Codigo== rol.Codigo|| data.Descripcion == rol.Descripcion) && data.IdRol != rol.IdRol);

            return check > 0;
        }

        public async Task BorrarRol(int idRol, int idUsuario)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla", 
                        new SqlParameter("@id",idRol),
                        new SqlParameter("@tabla", "Rol"));

                    var rol = await _context.Roles.Where(r => r.IdRol == idRol)
                        .SingleOrDefaultAsync();

                    if (rol == null)
                    {
                        throw new Exception("No existe el rol!");
                    }

                    rol.Borrado = true;
                    rol.IdUsuarioModifico = idUsuario;
                    rol.FechaBorrado = DateTime.Now;
                    await _context.SaveChangesAsync();
                    await scope.CommitAsync();

                }
                catch (Exception)
                {
                    await scope.RollbackAsync();
                    throw;
                }

            }
        }
    }
}
