using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface IPresentacionService
    {
        Task<List<Presentacion>> ObtenerPresentacion();
        Task<Presentacion> ObtenerPresentacionPorId(int idPresentacion);
        Task AgregarPresentacion(PresentacionDto dto, int idUsuario);
        Task UpdatePresentacion(PresentacionDto dto,int idUsuario);
        Task BorrarPresentacion(int idPresentacion, int idUsuario);
    }
    public class PresentacionService : IPresentacionService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public PresentacionService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<List<Presentacion>> ObtenerPresentacion()
        {
            try
            {
                var presentacion = await _context.Presentaciones.Include(u => u.IdUnidadNavigation)
                    .ToListAsync();
                return presentacion;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Presentacion> ObtenerPresentacionPorId(int idPresentacion)
        {
            try
            {
                var presentaciones = await _context.Presentaciones
                    .Where(u => u.IdPresentacion == idPresentacion)
                    .FirstOrDefaultAsync();
                return presentaciones;
            }
            catch (Exception)
            {

                throw;

            }
        }
        public async Task AgregarPresentacion(PresentacionDto presentacion, int idUsuario)
        {
            try
            {
                Presentacion nuevapresentacion = new Presentacion()
                {
                    IdUnidad = presentacion.IdUnidad,
                    CantidadPresentacion = presentacion.CantidadPresentacion,
                    Descripcion = presentacion.Descripcion,
                    Activo = presentacion.Activo,
                    IdUsuarioInserto = idUsuario

                };
                _context.Presentaciones.Add(nuevapresentacion);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdatePresentacion(PresentacionDto dto, int idUsuario)
        {

            var presentacion = await _context.Presentaciones
                .Where(mm => mm.IdPresentacion == dto.IdPresentacion)
                .FirstOrDefaultAsync();

            if (presentacion == null)
            {
                throw new Exception($"No se puede encontrar la presentacion {dto.IdPresentacion}");
            }
            else
            {
                presentacion.IdUnidad = dto.IdUnidad;
                presentacion.CantidadPresentacion = dto.CantidadPresentacion;
                presentacion.Descripcion = dto.Descripcion;
                presentacion.Activo = dto.Activo;
                presentacion.IdUsuarioModifico = idUsuario;
                presentacion.FechaModificado = DateTime.Now;
            }
           

            await _context.SaveChangesAsync();
        }

        public async Task BorrarPresentacion(int idPresentacion, int idUsuario)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idPresentacion),
                        new SqlParameter("@tabla", "Presentacion"));

                    var rol = await _context.Presentaciones.Where(r => r.IdPresentacion == idPresentacion)
                        .SingleOrDefaultAsync();

                    if (rol == null)
                    {
                        throw new Exception("No existe la presentacion!");
                    }

                    rol.Borrado = true;
                    rol.FechaBorrado = DateTime.Now;
                    rol.IdUsuarioModifico = idUsuario;

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
