using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;

namespace SYSVETE.Services
{
    public interface IPermiso
    {
        Task<List<Permiso>> ObtenerPermisos();
        Task<Permiso> ObtenerPermisoPorID(int idPermiso);
        Task AgregarPermiso(PermisoDto permiso, int idUsuario);
        Task UpdatePermiso(PermisoDto permiso, int idUsuario);
        Task BorrarPermiso(int idPermiso, int idUsuario);

    }
    public class PermisoService : IPermiso
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public PermisoService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }

        public async Task<Permiso> ObtenerPermisoPorID(int idPermiso)
        {
            try
            {
                var permiso = await _context.Permisos
                    .Where(u => u.IdPermiso == idPermiso)
                    .FirstOrDefaultAsync();
                return permiso;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Permiso>> ObtenerPermisos()
        {
            try
            {
                var permiso = await _context.Permisos.Include(u => u.IdRolNavigation).Include( p => p.IdModuloNavigation)
                    .ToListAsync();
                return permiso;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task AgregarPermiso(PermisoDto permiso, int idUsuario)
        {
            try
            {
                Permiso nuevoPermiso = new Permiso()
                {
                    IdRol = permiso.IdRol,
                    IdModulo = permiso.IdModulo,
                    Agregar = permiso.Agregar,
                    Consultar = permiso.Consultar,
                    Editar = permiso.Editar,
                    Borrar = permiso.Borrar,
                    IdUsuarioInserto = idUsuario
                };

                _context.Permisos.Add(nuevoPermiso);
                await _context.SaveChangesAsync();
            }
            catch ( Exception e)
            {
                throw e;
            }
            
        }
        public async Task UpdatePermiso(PermisoDto permiso, int idUsuario)
        {
            

            var existePermiso = await _context.Permisos.Where(p => p.IdPermiso == permiso.IdPermiso)
                .FirstOrDefaultAsync();

            if (existePermiso == null)
            {
                throw new Exception($"No se puede encontrar el permiso {permiso.IdPermiso}");
            }
            else
            {

                existePermiso.IdRol = permiso.IdRol;
                existePermiso.IdModulo = permiso.IdModulo;
                existePermiso.Agregar = permiso.Agregar;
                existePermiso.Consultar = permiso.Consultar;
                existePermiso.Editar = permiso.Editar;
                existePermiso.Borrar = permiso.Borrar;
                existePermiso.IdUsuarioModifico = idUsuario;
                existePermiso.FechaModificado = DateTime.Now;
            }    
            

            await _context.SaveChangesAsync();
        }

        public async Task BorrarPermiso(int idPermiso, int idUsuario)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idPermiso),
                        new SqlParameter("@tabla", "Permiso"));

                    var permiso = await _context.Permisos.Where(r => r.IdPermiso == idPermiso)
                        .SingleOrDefaultAsync();

                    if (permiso == null)
                    {
                        throw new Exception("No existe el permiso!");
                    }

                    permiso.Borrado = true;
                    permiso.IdUsuarioModifico = idUsuario;
                    permiso.FechaModificado = DateTime.Now;
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
