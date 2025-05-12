using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface IHistorialMovientoService
    {
        Task<List<HistorialMovimiento>> ObtenerHistorialMoviento();
        Task<HistorialMovimiento> ObtenerHistorialMovientoPorId(int idHistorial);
        Task AgregarHistorialMoviento(HistorialMovimiento hitorial, int idUsuario);
        Task UpdateHistorialMoviento(HistorialMovimiento hitorial, int idUsuario);
        Task BorrarHistorialMoviento(int idHistorial, int idUsuario);
    }
    public class HistorialMovientoService : IHistorialMovientoService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public HistorialMovientoService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<List<HistorialMovimiento>> ObtenerHistorialMoviento()
        {
            try
            {
                var tratamientos = await _context.HistorialMovimientos
                    .Include( x => x.IdCompraDetalleNavigation)
                    .ToListAsync();
                return tratamientos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<HistorialMovimiento> ObtenerHistorialMovientoPorId(int idHistorial)
        {
            try
            {
                var historial = await _context.HistorialMovimientos
                                        .Include(x => x.IdCompraDetalleNavigation)
                    .Where(u => u.IdHistorial == idHistorial)
                    .FirstOrDefaultAsync();
                return historial;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task AgregarHistorialMoviento(HistorialMovimiento historial, int idUsuario)
        {
            try
            {
                historial.IdUsuarioInserto = idUsuario;
                _context.HistorialMovimientos.Add(historial);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateHistorialMoviento(HistorialMovimiento dto, int idUsuario)
        {

            var historial = await _context.HistorialMovimientos
                .Where(mm => mm.IdHistorial == dto.IdHistorial)
                .FirstOrDefaultAsync();

            if (historial == null)
            {
                throw new Exception($"No se puede encontrar el historial {dto.IdHistorial}");
            }
            else
            {
                historial.Fecha = dto.Fecha;
                historial.Cantidad = dto.Cantidad;
                historial.Precio = dto.Precio;
                historial.IdCompraDetalle = dto.IdCompraDetalle;
                historial.IdVentaDetalle = dto.IdVentaDetalle;
                historial.Ajuste = dto.Ajuste;
                historial.IdUsuarioModifico = idUsuario;
                historial.FechaModificado = DateTime.Now;
            }
            await _context.SaveChangesAsync();
        }
        public async Task BorrarHistorialMoviento(int idHistorial, int idUsuario)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idHistorial),
                        new SqlParameter("@tabla", "Tratamiento"));

                    var histo = await _context.HistorialMovimientos.Where(r => r.IdHistorial == idHistorial)
                        .SingleOrDefaultAsync();

                    if (histo == null)
                    {
                        throw new Exception("No existe el historial!");
                    }

                    histo.Borrado = true;
                    histo.IdUsuarioModifico = idUsuario;
                    histo.FechaBorrado = DateTime.Now;
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
