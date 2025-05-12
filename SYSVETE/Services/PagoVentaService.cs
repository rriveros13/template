using SYSVETE.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Models.DTOs;
namespace SYSVETE.Services
{
    public interface IPagoVentaService
    {
        Task<List<PagoVenta>> ObtenerPagoVenta();
        Task<PagoVenta> ObtenerPagoVentaPorId(int idPago);
        Task<List<PagoVenta>> ObtenerPagoPorVenta(int idVenta);

        Task AgregarPagoVenta(PagoVenta pago, int idUsuario);
        Task UpdatePagoVenta(PagoVentaDto pago, int idUsuario);
        Task BorrarPagoVenta(int idPago, int idUsuario);
    }
    public class PagoVentaService : IPagoVentaService
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public PagoVentaService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<List<PagoVenta>> ObtenerPagoVenta()
        {
            try
            {
                var pago = await _context.PagoVentas.Include( x => x.IdVentaNavigation)
                    .ToListAsync();
                return pago;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PagoVenta> ObtenerPagoVentaPorId(int idPago)
        {
            try
            {
                var pago = await _context.PagoVentas.Include(x => x.IdVentaNavigation)
                    .Where(u => u.IdPago == idPago)
                    .FirstOrDefaultAsync();
                return pago;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<PagoVenta>> ObtenerPagoPorVenta(int idVenta)
        {
            try
            {
                var pago = await _context.PagoVentas.Include(x => x.IdVentaNavigation)
                    .Where(u => u.IdVenta == idVenta)
                    .ToListAsync();
                return pago;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task AgregarPagoVenta(PagoVenta pago, int idUsuario)
        {
            try
            {
                pago.IdUsuarioInserto = idUsuario;
                _context.PagoVentas.Add(pago);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdatePagoVenta(PagoVentaDto dto, int idUsuario)
        {

            var deuda = await _context.PagoVentas
                .Where(mm => mm.IdPago == dto.IdPago)
                .FirstOrDefaultAsync();

            if (deuda == null)
            {
                throw new Exception($"No se puede encontrar el pago {dto.IdPago}");
            }
            else
            {
                deuda.IdPago = dto.IdPago;
                deuda.FechaPago = dto.FechaPago;
                deuda.MontoPagado = dto.MontoPagado;
                deuda.MetodoPago = dto.MetodoPago;
                deuda.IdUsuarioModifico = idUsuario;
                deuda.FechaModificado = DateTime.Now;
            }
            await _context.SaveChangesAsync();
        }
        public async Task BorrarPagoVenta(int idPAgo, int idUsuario)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idPAgo),
                        new SqlParameter("@tabla", "PagoVenta"));

                    var pago = await _context.PagoVentas.Where(r => r.IdPago == idPAgo)
                        .SingleOrDefaultAsync();

                    if (pago == null)
                    {
                        throw new Exception("No existe el pago!");
                    }

                    pago.Borrado = true;
                    pago.IdUsuarioModifico = idUsuario;
                    pago.FechaBorrado = DateTime.Now;
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
